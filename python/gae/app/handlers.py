import webapp2
import sys

class MainHandler(webapp2.RequestHandler):
	def get(self):
		self.response.write('<br>'.join(sys.path))
