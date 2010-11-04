package spikeAppEngine;

import java.io.IOException;
import java.util.HashSet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.appengine.api.users.UserService;
import com.google.appengine.api.users.UserServiceFactory;

public class LoginServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		boolean isLoggedIn = request.getUserPrincipal() != null;
		if (isLoggedIn) {
			String destinationUrl = request.getParameter("destinationUrl");
			UserService userService = UserServiceFactory.getUserService();
			response.sendRedirect(userService.createLogoutURL(destinationUrl));
		} else {
			request.getRequestDispatcher("/Login.jsp").forward(request, response);
		}
	}

	public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
		String federatedIdentity = request.getParameter("federatedIdentity");
		String destinationUrl = request.getParameter("destinationUrl");
		String authDomain = request.getServerName();

		UserService userService = UserServiceFactory.getUserService();
		String loginUrl = userService.createLoginURL(destinationUrl, authDomain, federatedIdentity, new HashSet<String>());
		response.sendRedirect(loginUrl);
	}
}
