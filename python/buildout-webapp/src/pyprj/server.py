from wsgiref.simple_server import make_server
from app import build_app

def app_server():
    app = build_app()
    server = make_server('', 8080, app)
    server.serve_forever()
