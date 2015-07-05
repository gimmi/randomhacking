package com.github.gimmi.gradlesimple;

import java.util.Arrays;
import java.util.List;
import org.eclipse.jetty.server.Server;

public class Main {

	public static void main(String[] args) throws Exception {
		List<Integer> numbers = Arrays.asList(1, 2, 3, 4, 5);
		System.out.println(Arrays.toString(numbers.stream().map(n -> n * 2).toArray()));
		
		
		Server server = new Server(8080);
        server.start();
        server.dumpStdErr();
        server.join();
	}

}
