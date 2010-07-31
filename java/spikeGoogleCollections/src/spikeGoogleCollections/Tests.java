package spikeGoogleCollections;

import static org.junit.Assert.assertEquals;

import java.util.Arrays;
import java.util.Comparator;

import org.junit.Test;

import com.google.common.base.Function;
import com.google.common.base.Predicate;
import com.google.common.collect.Iterables;
import com.google.common.collect.Ordering;

public class Tests {
	@Test
	public void linq_where_clause() {
		String[] ary = { "One", "Two", "Three" };
		Iterable<String> iterable = Arrays.asList(ary);
		Iterable<String> filter = Iterables.filter(iterable,
				new Predicate<String>() {
					@Override
					public boolean apply(String input) {
						return input.equals("Two");
					}
				});
		assertEquals(1, Iterables.size(filter));
		assertEquals("Two", filter.iterator().next());
	}

	@Test
	public void linq_select_clause() {
		Integer[] ary = { 1, 2, 3 };
		Iterable<Integer> iterable = Arrays.asList(ary);
		Iterable<String> actual = Iterables.transform(iterable,
				new Function<Integer, String>() {
					@Override
					public String apply(Integer from) {
						return from.toString();
					}
				});
		assertEquals(3, Iterables.size(actual));
		assertEquals("1", Iterables.get(actual, 0));
		assertEquals("2", Iterables.get(actual, 1));
		assertEquals("3", Iterables.get(actual, 2));
	}
	
	@Test
	public void linq_sort_clause() {
		Integer[] ary = { 1, 2, 3 };
		Iterable<Integer> iterable = Arrays.asList(ary);
		Iterable<Integer> actual = Ordering.from(new Comparator<Integer>() {
			@Override
			public int compare(Integer o1, Integer o2) {
				return o2.compareTo(o1);
			}
		}).sortedCopy(iterable);
		assertEquals(3, Iterables.size(actual));
		assertEquals((Integer)3, Iterables.get(actual, 0));
		assertEquals((Integer)2, Iterables.get(actual, 1));
		assertEquals((Integer)1, Iterables.get(actual, 2));
	}
}
