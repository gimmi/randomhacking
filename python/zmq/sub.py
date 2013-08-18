import sys
import zmq
import time

#  Socket to talk to server
context = zmq.Context()
socket = context.socket(zmq.SUB)

ports = sys.argv[1:]
for port in ports:
	print("Connecting to %s" % port)
	socket.connect("tcp://localhost:" + port)

socket.setsockopt_string(zmq.SUBSCRIBE, '')

while True:
	message = socket.recv_string()
	print('Received %s' % message)
