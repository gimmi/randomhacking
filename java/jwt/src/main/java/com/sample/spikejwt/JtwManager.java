package com.sample.spikejwt;

import org.jose4j.jws.AlgorithmIdentifiers;
import org.jose4j.jws.JsonWebSignature;
import org.jose4j.jwt.JwtClaims;
import org.jose4j.jwt.consumer.JwtConsumer;
import org.jose4j.jwt.consumer.JwtConsumerBuilder;
import org.jose4j.jwx.HeaderParameterNames;
import org.jose4j.lang.JoseException;

import java.security.PrivateKey;
import java.security.PublicKey;

public class JtwManager {
    private final PrivateKey privateKey;
    private final PublicKey publicKey;

    public JtwManager(PrivateKey privateKey, PublicKey publicKey) {
        this.privateKey = privateKey;
        this.publicKey = publicKey;
    }

    public String build(String subject) {
        JwtClaims claims = new JwtClaims();
        claims.setSubject(subject);

        JsonWebSignature jws = new JsonWebSignature();
        jws.setPayload(claims.toJson());
        jws.setKey(privateKey);
        jws.setHeader(HeaderParameterNames.TYPE, "JWT");
        jws.setAlgorithmHeaderValue(AlgorithmIdentifiers.RSA_USING_SHA256);

        try {
            return jws.getCompactSerialization();
        } catch (JoseException e) {
            throw new RuntimeException(e);
        }
    }

    public String parse(String jwt) {
        JwtConsumer jwtConsumer = new JwtConsumerBuilder()
            .setRequireSubject()
            .setVerificationKey(publicKey)
            .build();

        try {
            JwtClaims jwtClaims = jwtConsumer.processToClaims(jwt);
            return jwtClaims.getSubject();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }
}
