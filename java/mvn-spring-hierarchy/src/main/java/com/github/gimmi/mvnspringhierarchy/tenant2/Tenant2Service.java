package com.github.gimmi.mvnspringhierarchy.tenant2;

import com.github.gimmi.mvnspringhierarchy.Service;
import com.github.gimmi.mvnspringhierarchy.Tenant;

public class Tenant2Service implements Service {
   private final Tenant tenant;

   public Tenant2Service(Tenant tenant) {
      this.tenant = tenant;
   }

   @Override
   public String toString() {
      return tenant.getId();
   }
}
