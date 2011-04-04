package com.github.gimmi.spikemavengwt.client;

import com.google.gwt.user.client.rpc.AsyncCallback;

import java.util.List;

public interface GreetingServiceAsync {
	void greetServer(String input, AsyncCallback<String> callback)
			throws IllegalArgumentException;

	void getDtos(int start, int length, AsyncCallback<PaginatedResults<Dto>> async);
}
