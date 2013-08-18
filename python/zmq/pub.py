import zmq
import random
import time
import sys

context = zmq.Context()
socket = context.socket(zmq.PUB)

port = sys.argv[1]
print("Binding to port %s" % port)
socket.bind("tcp://*:" + sys.argv[1])

count = 0
while True:
	message = '%s:%d' % (port, count)
	socket.send_unicode(message)
	print("Sent %s" % message)
	count = count + 1
	time.sleep(0.5)
