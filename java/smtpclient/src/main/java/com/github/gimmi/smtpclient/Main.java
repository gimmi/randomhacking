package com.github.gimmi.smtpclient;

import org.subethamail.smtp.server.SMTPServer;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class Main {

    static final Logger Log = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws InterruptedException {
        Log.info("Application started");

        MyMessageHandlerFactory myFactory = new MyMessageHandlerFactory() ;
        SMTPServer smtpServer = new SMTPServer(myFactory);
        smtpServer.setPort(25000);
        smtpServer.start();
    }
}
