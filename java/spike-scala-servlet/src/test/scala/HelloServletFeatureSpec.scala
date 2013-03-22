
import com.github.gimmi.spikescalaservlet.HelloServlet
import org.eclipse.jetty.server.Server
import org.eclipse.jetty.servlet.{ServletContextHandler, ServletHolder}
import org.scalatest.FeatureSpec

class HelloServletFeatureSpec extends FeatureSpec {
	feature("Navigating to /hello web page") {
		scenario("User go to /hello") {
			val server = new Server(8080)

			val context = new ServletContextHandler(ServletContextHandler.SESSIONS)
			context.setContextPath("/")
			context.addServlet(new ServletHolder(new HelloServlet()), "/*")
			server.setHandler(context)

			server.start()
			server.join()
		}
	}
}
