package com.github.gimmi.spikeboon;

import static org.boon.Boon.*;
import static org.boon.Lists.*;
import static org.boon.Maps.*;

import java.util.List;
import java.util.Map;

public class Main {

	public static void main(String[] args) {
		puts("Hello world!");
		List<Integer> list = list(1, 2, 3);
		
		puts("list =", list);
		
		
		Map<String, Object> map = map(
			"a", 1,
			"b", "2",
			"c", map(
				"d", list(4, 5, 6),
				"e", true
			)
		);
		
		puts("map =", map);
		puts(toJson(map));
	}
}
