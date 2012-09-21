from glob import glob
import re

pattern = re.compile(r': (\d+)? => (\d+)$')
transitions = {}

for file_name in glob('*.txt'):
	with open(file_name, 'r') as f:
		for line in f:
			match = pattern.search(line)
			if not match: continue
			transition = match.groups()
			transitions[transition] = transitions[transition]+1 if transition in transitions else 1


with open('out.dot', 'w') as f:
	f.write('digraph G {')
	for transition, times in transitions.iteritems():
		f.write('\t{0} -> {1} [label="{2}"];'.format(transition[0], transition[1], times))
	f.write('}')

print('dot -Tpng out.dot > output.png')