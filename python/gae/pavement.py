import os
import unittest
from paver.easy import *
import distutils.sysconfig

options(
	gae_sdk_path='C:\Program Files (x86)\Google\google_appengine',
	gae_libs = [ 'webapp2-2.5.2', 'webob-1.1.1' ]
)

def reference_gae_lib(*path):
	path = (options.gae_sdk_path, 'lib') + path
	path = os.path.join(*path)
	sys.path.insert(0, path)

@task
def run_tests():
	suite = unittest.TestLoader().discover(os.path.join(os.getcwd(), 'app'))
	unittest.TextTestRunner().run(suite)

@task
def create_env():
	gae_pth_filename = os.path.join(distutils.sysconfig.get_python_lib(), 'gae.pth')
	with open(gae_pth_filename, 'w') as gae_pth_file:
		for gae_lib in options.gae_libs:
			gae_lib_path = os.path.join(options.gae_sdk_path, 'lib', gae_lib)
			gae_pth_file.write(gae_lib_path + '\n')

# See http://stackoverflow.com/questions/6260149
def symlink(source, link_name):
	import os
	os_symlink = getattr(os, "symlink", None)
	if callable(os_symlink):
		os_symlink(source, link_name)
	else:
		import ctypes
		csl = ctypes.windll.kernel32.CreateSymbolicLinkW
		csl.argtypes = (ctypes.c_wchar_p, ctypes.c_wchar_p, ctypes.c_uint32)
		csl.restype = ctypes.c_ubyte
		flags = 1 if os.path.isdir(source) else 0
		if csl(link_name, source, flags) == 0:
			raise ctypes.WinError()

