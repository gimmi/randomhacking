import devwiki.WikiWsgiApp

from wsgiref.simple_server import make_server
import static

rootdir = 'c:\\users\\gimmi\\temp\\wiki'
make_server('localhost', 8000, devwiki.WikiWsgiApp.WikiWsgiApp(rootdir, wsgiapp=static.Cling(rootdir))).serve_forever()



#for directory, dirnames, filenames in os.walk(wikirootpath):
#    for filename in fnmatch.filter(filenames, '*.py'):
#        print(os.path.join(directory, filename))
#
#print(markdown(u'ciao'))
