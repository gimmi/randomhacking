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

