from markdown import markdown
import os, codecs
from webob.dec import wsgify
from webob import exc
from webob import Response

class WikiWsgiApp:
    def __init__(self, rootdir=os.getcwd(), encoding='utf-8', wikiext='.wiki', pagetemplate='<!DOCTYPE html><html><head></head><body>{0}</body></html>', wsgiapp=exc.HTTPNotFound()):
        self.rootdir = rootdir
        self.encoding = encoding
        self.wikiext = wikiext
        self.pagetemplate = unicode(pagetemplate)
        self.wsgiapp = wsgiapp

    @wsgify
    def __call__(self, req):
        if req.method != 'GET':
            raise exc.HTTPMethodNotAllowed()
        file = os.path.join(self.rootdir, req.path.lstrip('/'))
        if os.path.isfile(file) and file.endswith(self.wikiext):
            res = Response(content_type='text/html', charset=self.encoding)
            wikitext = codecs.open(file, encoding=self.encoding).read()
            wikitext = markdown(wikitext)
            res.text = self.pagetemplate.format(wikitext)
            return res
        return req.get_response(self.wsgiapp)
