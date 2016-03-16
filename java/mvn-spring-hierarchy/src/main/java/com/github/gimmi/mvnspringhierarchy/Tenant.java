package com.github.gimmi.mvnspringhierarchy;

public class Tenant {
   private final String id;
   private final String pkg;

   public Tenant(String id, String pkg) {
      this.id = id;
      this.pkg = pkg;
   }

   public String getId() {
      return id;
   }

   public String getPkg() {
      return pkg;
   }
}
