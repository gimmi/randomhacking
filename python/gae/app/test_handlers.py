import unittest
import handlers
import webtest
import wsgiapp

class MainHandlerUnitTest(unittest.TestCase):
	def test_one(self):
		self.assertEqual(1, 2)

class MainHandlerWebTest(unittest.TestCase):
	def setUp(self):
		self.testapp = webtest.TestApp(wsgiapp.app)

	def test_handler(self):
		response = self.testapp.get('/')
		self.assertEqual(response.status_int, 200)
		self.assertEqual(response.normal_body, 'Hello World!')
		self.assertEqual(response.content_type, 'text/plain')