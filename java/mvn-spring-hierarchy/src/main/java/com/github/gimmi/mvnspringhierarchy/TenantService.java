package com.github.gimmi.mvnspringhierarchy;

public class TenantService implements Service {
   private final Tenant tenant;

   public TenantService(Tenant tenant) {
      this.tenant = tenant;
   }

   @Override
   public String toString() {
      return tenant.getId();
   }
}
