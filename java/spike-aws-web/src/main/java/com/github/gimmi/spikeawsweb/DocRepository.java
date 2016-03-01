package com.github.gimmi.spikeawsweb;

import com.amazonaws.services.s3.AmazonS3;
import com.amazonaws.services.s3.model.*;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.FileCopyUtils;

import java.io.*;
import java.nio.charset.StandardCharsets;
import java.util.UUID;

public class DocRepository {
   private static final Logger logger = LoggerFactory.getLogger(DocRepository.class);

   private final AmazonS3 s3;
   private final String bucketName;

   public DocRepository(AmazonS3 s3, String bucketName) {
      this.s3 = s3;
      this.bucketName = bucketName;
   }

   public String put(String doc) {
      String key = UUID.randomUUID().toString();
      InputStream stream = new ByteArrayInputStream(doc.getBytes(StandardCharsets.UTF_8));
      ObjectMetadata metadata = new ObjectMetadata();
      metadata.setContentType("text/plain; charset=UTF-8");
      PutObjectResult result = s3.putObject(new PutObjectRequest(bucketName, key, stream, metadata));
      logger.info("Stored https://{}.s3.amazonaws.com/{} (MD5: {})", bucketName, key, result.getContentMd5());
      return key;
   }

   public String get(String docId) {
      S3Object obj = s3.getObject(new GetObjectRequest(bucketName, docId));
      try (InputStream inputStream = obj.getObjectContent()) {
         return FileCopyUtils.copyToString(new BufferedReader(new InputStreamReader(inputStream, StandardCharsets.UTF_8)));
      } catch (IOException e) {
         throw new UncheckedIOException(e);
      }
   }
}
