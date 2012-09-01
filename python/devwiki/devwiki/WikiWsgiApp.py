from markdown import markdown
import os, codecs

class HttpStatusWsgiApp:
    def __init__(self, status):
        self.status = status

    def __call__(self, environ, start_response):
        start_response(self.status, [('Content-Type', 'text/plain')])
        return [self.status]


class FaviconWsgiApp:
    def __init__(self, wsgiapp):
        self.wsgiapp = wsgiapp

    def __call__(self, environ, start_response):
        if environ['REQUEST_METHOD'] == 'GET' and environ.get('PATH_INFO', '/') == '/favicon.ico':
            return HttpStatusWsgiApp('404 Not Found')(environ, start_response)
        return self.wsgiapp(environ, start_response)


class WikiWsgiApp:
    def __init__(self, rootdir=os.getcwd(), encoding='utf-8', wikiext='.wiki', pagetemplate='<!DOCTYPE html><html><head></head><body>{0}</body></html>', wsgiapp=HttpStatusWsgiApp('404 Not Found')):
        self.rootdir = rootdir
        self.encoding = encoding
        self.wikiext = wikiext
        self.pagetemplate = unicode(pagetemplate)
        self.wsgiapp = wsgiapp

    def __call__(self, environ, start_response):
        if environ['REQUEST_METHOD'] != 'GET':
            return HttpStatusWsgiApp('405 Method Not Allowed')(environ, start_response)
        file = environ.get('PATH_INFO', '/')
        file = os.path.join(self.rootdir, file.lstrip('/'))
        if os.path.isfile(file) and file.endswith(self.wikiext):
            start_response('200 OK', [('Content-Type', 'text/html; charset=' + self.encoding)])
            wikitext = codecs.open(file, encoding=self.encoding).read()
            wikitext = markdown(wikitext)
            body = self.pagetemplate.format(wikitext)
            return [body.encode(self.encoding)]
        return self.wsgiapp(environ, start_response)
