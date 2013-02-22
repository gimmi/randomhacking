import os
import unittest
from paver.easy import *

options(
	gae_sdk_path='C:\Program Files (x86)\Google\google_appengine'
)

def reference_gae_lib(*path):
	path = (options.gae_sdk_path, 'lib') + path
	path = os.path.join(*path)
	sys.path.insert(0, path)
	
@task
def run_tests():
	reference_gae_lib('webapp2-2.5.2')
	reference_gae_lib('webob-1.1.1')

	suite = unittest.TestLoader().discover(os.path.join(os.getcwd(), 'app'))
	unittest.TextTestRunner().run(suite)
