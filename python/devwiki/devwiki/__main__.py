import devwiki.WikiWsgiApp

from wsgiref.simple_server import make_server

app = devwiki.WikiWsgiApp.WikiWsgiApp(u'c:\\users\\gimmi\\temp\\wiki')
srv = make_server('localhost', 8000, app)
srv.serve_forever()


#for directory, dirnames, filenames in os.walk(wikirootpath):
#    for filename in fnmatch.filter(filenames, '*.py'):
#        print(os.path.join(directory, filename))
#
#print(markdown(u'ciao'))
