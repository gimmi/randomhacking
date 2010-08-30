package spikeAppEngine;

import java.io.IOException;
import java.util.HashSet;

import javax.servlet.ServletException;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import com.google.appengine.api.users.UserService;
import com.google.appengine.api.users.UserServiceFactory;

public class IndexServlet extends HttpServlet {
	private static final long serialVersionUID = 1L;

	public void doGet(HttpServletRequest request, HttpServletResponse response) throws ServletException, IOException {
		request.setAttribute("userName", request.getUserPrincipal() != null ? request.getUserPrincipal() : "Login");
		request.setAttribute("destinationUrl", request.getRequestURI());
		request.getRequestDispatcher("/Index.jsp").forward(request, response);
	}

	public void doPost(HttpServletRequest request, HttpServletResponse response) throws IOException {
		String federatedIdentity = request.getParameter("id");
		String destinationURL = request.getRequestURI();
		String authDomain = request.getServerName();

		UserService userService = UserServiceFactory.getUserService();
		String loginURL = userService.createLoginURL(destinationURL, authDomain, federatedIdentity, new HashSet<String>());
		response.sendRedirect(loginURL);
	}
}
