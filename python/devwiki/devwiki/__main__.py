import devwiki.WikiWsgiApp

from wsgiref.simple_server import make_server
import pkg_resources, webob.static

rootdir = 'c:\\users\\gimmi\\temp\\wiki'
encoding='utf-8'

pagetemplate = pkg_resources.resource_string(__name__, 'resources/template.html').decode(encoding)

app = webob.static.DirectoryApp(rootdir)
app = devwiki.WikiWsgiApp.WikiWsgiApp(rootdir, wsgiapp=app, encoding=encoding, pagetemplate=pagetemplate)

make_server('localhost', 8000, app).serve_forever()



#for directory, dirnames, filenames in os.walk(wikirootpath):
#    for filename in fnmatch.filter(filenames, '*.py'):
#        print(os.path.join(directory, filename))
#
#print(markdown(u'ciao'))
