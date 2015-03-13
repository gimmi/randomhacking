diff --git a/java/jarweb/src/main/java/com/github/gimmi/jarweb/MyApplication.java b/java/jarweb/src/main/java/com/github/gimmi/jarweb/MyApplication.java
index 0cb980b..2e0b685 100644
--- a/java/jarweb/src/main/java/com/github/gimmi/jarweb/MyApplication.java
+++ b/java/jarweb/src/main/java/com/github/gimmi/jarweb/MyApplication.java
@@ -1,19 +1,23 @@
 package com.github.gimmi.jarweb;
 
+import javax.json.stream.JsonGenerator;
 import org.glassfish.hk2.utilities.binding.AbstractBinder;
 import org.glassfish.jersey.process.internal.RequestScoped;
 import org.glassfish.jersey.server.ResourceConfig;
 
 public class MyApplication extends ResourceConfig {
 
     public MyApplication() {
         register(MyResource.class);
         
+        // register(org.glassfish.jersey.jsonp.JsonProcessingFeature.class);
+        property(JsonGenerator.PRETTY_PRINTING, true);
+        
         register(new AbstractBinder() {
             @Override
             protected void configure() {
                 bind(MyObject.class).to(MyObject.class).in(RequestScoped.class);
             }
         });
     }
 }
