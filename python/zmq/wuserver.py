#
#   Weather update server
#   Binds PUB socket to tcp://*:5556
#   Publishes random weather updates
#

import zmq
import random
import time
import sys

context = zmq.Context()
socket = context.socket(zmq.PUB)
socket.bind("tcp://*:5556")

while True:
	zipcode = random.randrange(10000,10003)
	temperature = random.randrange(1,215) - 80
	relhumidity = random.randrange(1,50) + 10

	socket.send_unicode("%d %d %d" % (zipcode, temperature, relhumidity))

	print(zipcode)
	time.sleep(2)
