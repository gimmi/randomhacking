package com.github.gimmi.spikemavengwt.server;

import com.github.gimmi.spikemavengwt.client.Dto;
import com.github.gimmi.spikemavengwt.client.GreetingService;
import com.github.gimmi.spikemavengwt.client.PaginatedResults;
import com.github.gimmi.spikemavengwt.shared.FieldVerifier;
import com.google.gwt.user.server.rpc.RemoteServiceServlet;

import java.util.ArrayList;
import java.util.List;

@SuppressWarnings("serial")
public class GreetingServiceImpl extends RemoteServiceServlet implements GreetingService {

	public String greetServer(String input) throws IllegalArgumentException {
		if (!FieldVerifier.isValidName(input)) {
			throw new IllegalArgumentException("Name must be at least 4 characters long");
		}

		String serverInfo = getServletContext().getServerInfo();
		String userAgent = getThreadLocalRequest().getHeader("User-Agent");

		// Escape data from the client to avoid cross-site script vulnerabilities.
		input = escapeHtml(input);
		userAgent = escapeHtml(userAgent);

		return "Hello, " + input + "!<br><br>I am running " + serverInfo
				+ ".<br><br>It looks like you are using:<br>" + userAgent;
	}

	@Override
	public PaginatedResults<Dto> getDtos(int start, int length) {
		List<Dto> DATA = new ArrayList<Dto>();
		for (int i = 1; i <= 100; i++) {
			DATA.add(new Dto("Name " + i, "Address " + i));
		}
		// See http://code.google.com/p/google-web-toolkit/issues/detail?id=2369
		return new PaginatedResults<Dto>(new ArrayList<Dto>(DATA.subList(start, start + length)), DATA.size());
	}

	private String escapeHtml(String html) {
		if (html == null) {
			return null;
		}
		return html.replaceAll("&", "&amp;")
				.replaceAll("<", "&lt;")
				.replaceAll(">", "&gt;");
	}
}
