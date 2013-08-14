import fnmatch
import os
import xml.etree.ElementTree as ET
import logging
import re
import json
import sys

class Module(object):
	def __init__(self, name):
		self.name = name
		self.references = set()
		self.sources = set()
		self.output_type = 'Unknown'

	def __eq__(self, other):
		return self.name == other.name

	def __hash__(self):
		return hash(self.name)

	def add_reference(self, module):
		self.references.add(module)

	def remove_reference(self, module):
		self.references.discard(module)

	def add_source(self, source):
		self.sources.add(source)

class Modules(object):
	def __init__(self):
		self.dict = {}

	def create(self, name):
		if name not in self.dict:
			self.dict[name] = Module(name)
		return self.dict[name]

	def remove_by_pattern(self, pattern):
		removed_modules = []
		for name in self.dict:
			if re.search(pattern, name):
				logging.info('Discarding %s', name)
				removed_modules.append(self.dict[name])

		for removed_module in removed_modules:
			for module in self.dict.values():
				module.remove_reference(removed_module)
			del self.dict[removed_module.name]

def parse_asm_name(assembly_definition):
	assembly_props = re.split('\s*,\s*', assembly_definition)
	assembly_name = assembly_props.pop(0)
	assembly_props = { k: v for k, v in [re.split('\s*=\s*', x) for x in assembly_props] }
	return (assembly_name, assembly_props)

def create_module_from_msbuild_proj(modules, proj_path):
	tree = ET.parse(proj_path)

	assembly_name = tree.findtext('.//{http://schemas.microsoft.com/developer/msbuild/2003}AssemblyName')
	assembly_name = assembly_name or tree.findtext('.//{http://schemas.microsoft.com/developer/msbuild/2003}RootNamespace')
	assembly_name = assembly_name or os.path.basename(proj_path)
	module = modules.create(assembly_name)
	if proj_path not in module.sources:
		logging.info('Processing %s from file %s', assembly_name, proj_path)

		module.add_source(proj_path)

		module.output_type = (
			tree.findtext('.//{http://schemas.microsoft.com/developer/msbuild/2003}OutputType') or
			tree.findtext('.//{http://schemas.microsoft.com/developer/msbuild/2003}ConfigurationType') or
			module.output_type
		)

		for project_reference_el in tree.findall('.//{http://schemas.microsoft.com/developer/msbuild/2003}ProjectReference[@Include]'):
			ref_prj_path = os.path.abspath(os.path.join(os.path.dirname(proj_path), project_reference_el.attrib['Include']))
			ref_module = create_module_from_msbuild_proj(modules, ref_prj_path)
			module.add_reference(ref_module)

		for reference_el in tree.findall('.//{http://schemas.microsoft.com/developer/msbuild/2003}Reference'):
			ref_assembly_name, ref_assembly_props = parse_asm_name(reference_el.attrib['Include'])
			ref_module = modules.create(ref_assembly_name)
			module.add_reference(ref_module)

	return module

def pasre_dir(base_dir):
	logging.info('Parsing %s', base_dir)

	modules = Modules()

	for directory, directories, files in os.walk(base_dir):
		for csproj_file in fnmatch.filter(files, '*.csproj') + fnmatch.filter(files, '*.vcxproj'):
			csproj_path = os.path.join(directory, csproj_file)
			create_module_from_msbuild_proj(modules, csproj_path)

	return modules

def write_graphml(modules, filename):
	logging.info('Writing %s', filename)

	graphml_el = ET.Element('graphml')
	ET.SubElement(graphml_el, 'key', { 'id': 'module_name', 'for': 'node', 'attr.name': 'Module Name', 'attr.type': 'string' })
	ET.SubElement(graphml_el, 'key', { 'id': 'module_type', 'for': 'node', 'attr.name': 'Module Type', 'attr.type': 'string' })
	graph_el = ET.SubElement(graphml_el, 'graph', { 'id': 'G', 'edgedefault': 'directed' })
	for module in modules.dict.values():
		node_el = ET.SubElement(graph_el, 'node', { 'id': module.name })
		ET.SubElement(node_el, 'data', { 'key': 'module_name' }).text = module.name
		ET.SubElement(node_el, 'data', { 'key': 'module_type' }).text = module.output_type
		for ref_module in module.references:
			ET.SubElement(graph_el, 'edge', { 'id': module.name + ':' + ref_module.name, 'source': module.name, 'target': ref_module.name })
	ET.ElementTree(graphml_el).write(filename, encoding='utf-8', xml_declaration=True)

def main():
	logging.basicConfig(level=logging.DEBUG)
	base_dir = sys.argv[1]

	modules = pasre_dir(base_dir)

	modules.remove_by_pattern('^System')
	modules.remove_by_pattern('^Microsoft')
	modules.remove_by_pattern('Tests$')
	modules.remove_by_pattern('Test$')
	modules.remove_by_pattern('^ServiceStack')
	modules.remove_by_pattern('^log4net')
	modules.remove_by_pattern('^nunit')
	modules.remove_by_pattern('^pnunit')
	modules.remove_by_pattern('^Moq')
	modules.remove_by_pattern('^Presentation')
	modules.remove_by_pattern('^WindowsBase$')
	modules.remove_by_pattern('^Watin')
	modules.remove_by_pattern('^syncfusion')
	modules.remove_by_pattern('^zlib.net$')
	modules.remove_by_pattern('^protobuf-net$')
	modules.remove_by_pattern('^mscorlib$')
	modules.remove_by_pattern('^Tutorial')
	modules.remove_by_pattern('^UIAutomation')
	modules.remove_by_pattern('^TypeMock')
	modules.remove_by_pattern('^ADODB$')

	write_graphml(modules, 'out.graphml')

if __name__ == '__main__':
	main()
