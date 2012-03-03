from pyramid.response import Response

def hello_world(request):
    return Response('Hello %(name)s!' % request.matchdict)
