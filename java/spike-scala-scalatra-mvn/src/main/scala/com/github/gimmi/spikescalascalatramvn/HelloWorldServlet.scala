package com.github.gimmi.spikescalascalatramvn

import org.scalatra._

class HelloWorldServlet extends ScalatraServlet {
	get("/") {
		<html>
			<body>
				<h1>Hello, world!</h1>
			</body>
		</html>
	}
}
