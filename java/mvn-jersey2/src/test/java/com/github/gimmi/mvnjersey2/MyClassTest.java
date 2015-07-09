package com.github.gimmi.mvnjersey2;

import static org.junit.Assert.*;
import org.junit.Test;

public class MyClassTest {
    @Test
    public void shouldEcho(){
        MyClass sut = new MyClass();
        
        assertEquals(sut.echo("Val"), "Val");
    }
}
