from markdown import markdown
import os, codecs

# TODO use http://lukearno.com/projects/static/
class NotFoundWsgiApp:
    def __call__(self, environ, start_response):
        start_response('404 Not Found', [])
        return []


class WikiWsgiApp:
    def __init__(self, rootdir=os.getcwd(), encoding='utf-8', wikiext='.wiki', pagetemplate='<html><body>{0}</body></html>', wsgiapp=NotFoundWsgiApp()):
        self.rootdir = rootdir
        self.encoding = encoding
        self.wikiext = wikiext
        self.pagetemplate = unicode(pagetemplate)
        self.wsgiapp = wsgiapp

    def __call__(self, environ, start_response):
        file = environ.get('PATH_INFO', '/')
        file = os.path.join(self.rootdir, file.lstrip('/'))
        if os.path.isfile(file) and file.endswith(self.wikiext):
            start_response('200 OK', [('Content-Type', 'text/html; charset=' + self.encoding)])
            wikitext = codecs.open(file, encoding=self.encoding).read()
            wikitext = markdown(wikitext)
            body = self.pagetemplate.format(wikitext)
            return [body.encode(self.encoding)]
        return self.wsgiapp(environ, start_response)
