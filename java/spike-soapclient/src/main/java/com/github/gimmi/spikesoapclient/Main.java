package com.github.gimmi.spikesoapclient;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import javax.xml.soap.*;

public class Main {
    private static Logger logger = LoggerFactory.getLogger(Main.class);


    public static void main(String args[]) throws Exception {
        // Create SOAP Connection
        SOAPConnectionFactory soapConnectionFactory = SOAPConnectionFactory.newInstance();
        SOAPConnection soapConnection = soapConnectionFactory.createConnection();

        // Send SOAP Message to SOAP Server
        String url = "http://ws.cdyne.com/emailverify/Emailvernotestemail.asmx";
        SOAPMessage soapRequest = createSOAPRequest();

        System.out.print("Request SOAP Message:");
        soapRequest.writeTo(System.out);
        System.out.println();

        SOAPMessage soapResponse = soapConnection.call(soapRequest, url);

        // print SOAP Response
        System.out.print("Response SOAP Message:");
        soapResponse.writeTo(System.out);
        System.out.println();

        soapConnection.close();
    }

    private static SOAPMessage createSOAPRequest() throws Exception {
        MessageFactory messageFactory = MessageFactory.newInstance();
        SOAPMessage soapMessage = messageFactory.createMessage();

        SOAPPart soapPart = soapMessage.getSOAPPart();

        String xmlns = "http://ws.cdyne.com/";

        SOAPEnvelope envelope = soapPart.getEnvelope();

        SOAPBody soapBody = envelope.getBody();
        SOAPElement soapBodyElem = soapBody.addChildElement("VerifyEmail", "", xmlns);
        soapBodyElem.addChildElement("email").addTextNode("mutantninja@gmail.com");
        soapBodyElem.addChildElement("LicenseKey").addTextNode("123");

//        MimeHeaders headers = soapMessage.getMimeHeaders();
//        headers.addHeader("SOAPAction", xmlns + "VerifyEmail");

        soapMessage.saveChanges();

        return soapMessage;
    }
}
