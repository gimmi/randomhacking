from pyramid.config import Configurator
import mod

def build_app():
    config = Configurator()
    config.add_route('hello', '/hello/{name}')
    config.add_view(mod.hello_world, route_name='hello')
    return config.make_wsgi_app()
