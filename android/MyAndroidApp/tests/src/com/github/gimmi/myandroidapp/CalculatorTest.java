package com.github.gimmi.myandroidapp;

import junit.framework.TestCase;

public class CalculatorTest extends TestCase {
    private Calculator sut;

    @Override
    protected void setUp() throws Exception {
        super.setUp();
        sut = new Calculator();
    }

    public void testSum() {
        int actual = sut.sum(1, 2);
        assertEquals(3, actual);
    }
}
