package com.github.gimmi.spikesoapclient;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.xml.soap.*;
import java.io.*;
import java.nio.charset.Charset;
import java.nio.charset.StandardCharsets;

public class Main {
	private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String args[]) throws Exception {
		logger.info("Application started");
		try {
			SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
			SOAPConnection soapConnection = soapConnectionFactory.createConnection();

			String url = "http://ws.cdyne.com/emailverify/Emailvernotestemail.asmx";
			SOAPMessage soapMessage = MessageFactory.newInstance().createMessage();
			soapMessage.getMimeHeaders().addHeader("SOAPAction", "http://ws.cdyne.com/VerifyEmail");
			SOAPElement soapBodyElem = soapMessage.getSOAPPart().getEnvelope().getBody().addChildElement("VerifyEmail", "", "http://ws.cdyne.com/");

			soapBodyElem.addChildElement("email").addTextNode("mutantninja@gmail.com");
			soapBodyElem.addChildElement("LicenseKey").addTextNode("123");

			soapMessage.saveChanges();

			File inFile = new File("input.xml").getCanonicalFile();
			logger.info("Request file: {}", inFile);
			OutputStream reqOs = new FileOutputStream(inFile);
			soapMessage.writeTo(reqOs);

			SOAPMessage soapResponse = soapConnection.call(soapMessage, url);

			File outFile = new File("output.xml").getCanonicalFile();
			logger.info("Response file: {}", outFile);
			OutputStream respOs = new FileOutputStream(outFile);
			soapResponse.writeTo(respOs);

			soapConnection.close();
			logger.info("Done.");
		} catch (Exception e) {
			logger.error("Exception thrown", e);
		}
	}
}
