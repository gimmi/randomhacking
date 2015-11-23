package com.github.gimmi.spikesoapclient;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.w3c.dom.*;

import javax.xml.namespace.NamespaceContext;
import javax.xml.soap.*;
import javax.xml.xpath.XPath;
import javax.xml.xpath.XPathConstants;
import javax.xml.xpath.XPathExpressionException;
import javax.xml.xpath.XPathFactory;
import java.io.*;
import java.util.HashMap;
import java.util.Iterator;
import java.util.Map;

public class Main {
	private static Logger logger = LoggerFactory.getLogger(Main.class);

	public static void main(String args[]) throws Exception {
		logger.info("Application started");
		try {
			SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
			SOAPConnection soapConnection = soapConnectionFactory.createConnection();

			SOAPMessage soapRequest = MessageFactory.newInstance().createMessage();
			soapRequest.getMimeHeaders().addHeader("SOAPAction", "http://ws.cdyne.com/VerifyEmail");
			SOAPBody requestEl = soapRequest.getSOAPBody();
			SOAPElement soapBodyElem = requestEl.addChildElement("VerifyEmail", "", "http://ws.cdyne.com/");

			soapBodyElem.addChildElement("email").addTextNode("mutantninja@gmail.com");
			soapBodyElem.addChildElement("LicenseKey").addTextNode("123");

			soapRequest.saveChanges();

			saveMessage("input.xml", soapRequest);

			SOAPMessage soapResponse = soapConnection.call(soapRequest, "http://ws.cdyne.com/emailverify/Emailvernotestemail.asmx");
			saveMessage("output.xml", soapResponse);
			Element responseEl = soapResponse.getSOAPBody();

			logger.info("ResponseText: {}", xpath(responseEl, "//tns:ResponseText"));
			logger.info("ResponseCode: {}", xpath(responseEl, "//tns:ResponseCode"));
			logger.info("LastMailServer: {}", xpath(responseEl, "//tns:LastMailServer"));

			soapConnection.close();
			logger.info("Done.");
		} catch (Exception e) {
			logger.error("Exception thrown", e);
		}
	}

	private static String xpath(Element el, String expression) {
		XPathFactory xPathfactory = XPathFactory.newInstance();
		XPath xpath = xPathfactory.newXPath();
		xpath.setNamespaceContext(new SimpleNamespaceContext()
			.put("tns", "http://ws.cdyne.com/")
		);
		try {
			return xpath.compile(expression).evaluate(el);
		} catch (XPathExpressionException e) {
			throw new RuntimeException(e);
		}
	}

	private static void saveMessage(String filePath, SOAPMessage msg) throws Exception {
		File reqFile = new File(filePath).getCanonicalFile();
		logger.info("Writing SOAP message to {}", reqFile);
		OutputStream reqOs = new FileOutputStream(reqFile);
		msg.writeTo(reqOs);
	}

	private static class SimpleNamespaceContext implements NamespaceContext {
		private final Map<String, String> prefixes = new HashMap<>();

		public SimpleNamespaceContext put(String prefix, String namespaceURI) {
			prefixes.put(prefix, namespaceURI);
			return this;
		}

		public String getNamespaceURI(String prefix) {
			return prefixes.get(prefix);
		}

		public String getPrefix(String uri) {
			throw new UnsupportedOperationException();
		}

		public Iterator getPrefixes(String uri) {
			throw new UnsupportedOperationException();
		}
	}
}
