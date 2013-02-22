import webapp2
import handlers

app = webapp2.WSGIApplication([
    ('/', handlers.MainHandler)
], debug=True)
