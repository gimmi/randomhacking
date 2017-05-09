package com.sample.spikejwt;

import org.jose4j.jws.AlgorithmIdentifiers;
import org.jose4j.jws.JsonWebSignature;
import org.jose4j.jwt.JwtClaims;
import org.jose4j.jwt.consumer.JwtConsumer;
import org.jose4j.jwt.consumer.JwtConsumerBuilder;
import org.jose4j.lang.JoseException;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import java.io.FileOutputStream;
import java.io.IOException;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.security.*;
import java.security.spec.InvalidKeySpecException;
import java.security.spec.PKCS8EncodedKeySpec;
import java.security.spec.X509EncodedKeySpec;

public class Main {
    private static final Logger logger = LoggerFactory.getLogger(Main.class);

    public static void main(String[] args) throws Exception {
        String path = "C:/Users/gimmi/Temp";

        KeyPair keyPair = generateNewKeyPair();
        saveKeyPair(path, keyPair);

        keyPair = loadKeyPair(path);

        String jwt = buildJwt(keyPair.getPrivate(), "Authenticated user");

        System.out.println("JWT");
        System.out.println(jwt);

        String userName = parseJwt(jwt, keyPair.getPublic());

        System.out.print("userName: ");
        System.out.println(userName);
    }

    private static KeyPair generateNewKeyPair() throws Exception {
        KeyPairGenerator keyGenerator = KeyPairGenerator.getInstance("RSA");
        keyGenerator.initialize(2048);
        return keyGenerator.generateKeyPair();
    }

    public static void saveKeyPair(String path, KeyPair keyPair) throws IOException {
        String publicKeyPath = Paths.get(path, "public.key").toString();
        X509EncodedKeySpec x509EncodedKeySpec = new X509EncodedKeySpec(keyPair.getPublic().getEncoded());
        try(FileOutputStream fos = new FileOutputStream(publicKeyPath)) {
            fos.write(x509EncodedKeySpec.getEncoded());
        }

        String privateKeyPath = Paths.get(path, "private.key").toString();
        PKCS8EncodedKeySpec pkcs8EncodedKeySpec = new PKCS8EncodedKeySpec(keyPair.getPrivate().getEncoded());
        try(FileOutputStream fos = new FileOutputStream(privateKeyPath)) {
            fos.write(pkcs8EncodedKeySpec.getEncoded());
        }
    }

    public static KeyPair loadKeyPair(String path) throws IOException, NoSuchAlgorithmException, InvalidKeySpecException {
        KeyFactory keyFactory = KeyFactory.getInstance("RSA");

        byte[] encodedPublicKey = Files.readAllBytes(Paths.get(path, "public.key"));
        X509EncodedKeySpec publicKeySpec = new X509EncodedKeySpec(encodedPublicKey);
        PublicKey publicKey = keyFactory.generatePublic(publicKeySpec);

        byte[] encodedPrivateKey = Files.readAllBytes(Paths.get(path, "private.key"));
        PKCS8EncodedKeySpec privateKeySpec = new PKCS8EncodedKeySpec(encodedPrivateKey);
        PrivateKey privateKey = keyFactory.generatePrivate(privateKeySpec);

        return new KeyPair(publicKey, privateKey);
    }
    private static String parseJwt(String jwt, Key key) {
        JwtConsumer jwtConsumer = new JwtConsumerBuilder()
            .setRequireSubject()
            .setVerificationKey(key)
            .build();

        try {
            JwtClaims jwtClaims = jwtConsumer.processToClaims(jwt);
            return jwtClaims.getSubject();
        } catch (Exception e) {
            throw new RuntimeException(e);
        }
    }

    private static String buildJwt(PrivateKey privateKey, String userName) throws JoseException {
        // Create the Claims, which will be the content of the JWT
        JwtClaims claims = new JwtClaims();
        claims.setSubject(userName); // the subject/principal is whom the token is about

        // A JWT is a JWS and/or a JWE with JSON claims as the payload.
        // In this example it is a JWS so we create a JsonWebSignature object.
        JsonWebSignature jws = new JsonWebSignature();

        // The payload of the JWS is JSON content of the JWT Claims
        jws.setPayload(claims.toJson());

        // The JWT is signed using the private key
        jws.setKey(privateKey);

        // Set the signature algorithm on the JWT/JWS that will integrity protect the claims
        jws.setAlgorithmHeaderValue(AlgorithmIdentifiers.RSA_USING_SHA256);

        // Sign the JWS and produce the compact serialization or the complete JWT/JWS
        // representation, which is a string consisting of three dot ('.') separated
        // base64url-encoded parts in the form Header.Payload.Signature
        // If you wanted to encrypt it, you can simply set this jwt as the payload
        // of a JsonWebEncryption object and set the cty (Content Type) header to "jwt".
        String jwt = jws.getCompactSerialization();

        // Now you can do something with the JWT. Like send it to some other party
        // over the clouds and through the interwebs.

        return jwt;
    }
}
