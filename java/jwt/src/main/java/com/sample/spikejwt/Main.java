package com.sample.spikejwt;

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

        saveKeyPair(path);

        PrivateKey privateKey = loadPrivateKey(path);
        PublicKey publicKey = loadPublicKey(path);

        JtwManager jtwManager = new JtwManager(privateKey, publicKey);

        String jwt = jtwManager.build("User Name Here");

        System.out.println("JWT");
        System.out.println(jwt);

        String userName = jtwManager.parse(jwt);

        System.out.print("userName: ");
        System.out.println(userName);
    }

    private static KeyPair generateNewKeyPair() throws Exception {
        KeyPairGenerator keyGenerator = KeyPairGenerator.getInstance("RSA");
        keyGenerator.initialize(2048);
        return keyGenerator.generateKeyPair();
    }

    public static void saveKeyPair(String path) throws Exception {
        KeyPair keyPair = generateNewKeyPair();

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

    public static PublicKey loadPublicKey(String path) throws IOException, NoSuchAlgorithmException, InvalidKeySpecException {
        KeyFactory keyFactory = KeyFactory.getInstance("RSA");

        byte[] encodedPublicKey = Files.readAllBytes(Paths.get(path, "public.key"));
        X509EncodedKeySpec publicKeySpec = new X509EncodedKeySpec(encodedPublicKey);
        return keyFactory.generatePublic(publicKeySpec);
    }

    public static PrivateKey loadPrivateKey(String path) throws IOException, NoSuchAlgorithmException, InvalidKeySpecException {
        KeyFactory keyFactory = KeyFactory.getInstance("RSA");

        byte[] encodedPrivateKey = Files.readAllBytes(Paths.get(path, "private.key"));
        PKCS8EncodedKeySpec privateKeySpec = new PKCS8EncodedKeySpec(encodedPrivateKey);
        return keyFactory.generatePrivate(privateKeySpec);
    }
}
