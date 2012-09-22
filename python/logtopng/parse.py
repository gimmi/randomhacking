from glob import glob
import re

pattern = re.compile(r': (\d+)? => (\d+)$')
transitions = {}
states = {
	None: 'New',
	100: 'Error'
}

for file_name in glob('*.txt'):
	with open(file_name, 'r') as f:
		for line in f:
			match = pattern.search(line)
			if not match: continue
			transition = match.groups()
			if transition[0] not in states:
				states[transition[0]] = transition[0] + ': unknown'
			transitions[transition] = transitions[transition]+1 if transition in transitions else 1


with open('out.dot', 'w') as f:
	f.write('digraph G {')
	for state, descr in states.iteritems():
		f.write('\t{0} [label="{0} - {1}"];'.format(state, descr))
	for transition, times in transitions.iteritems():
		f.write('\t{0} -> {1} [label="{2}"];'.format(transition[0], transition[1], times))
	f.write('}')

print('dot -Tpng out.dot > output.png')