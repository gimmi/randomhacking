package com.github.gimmi.mvnspringhierarchy.tenant1;

import com.github.gimmi.mvnspringhierarchy.Service;
import com.github.gimmi.mvnspringhierarchy.Tenant;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;

@Component
public class Tenant1Service implements Service {
   private final Tenant tenant;

   @Autowired
   public Tenant1Service(Tenant tenant) {
      this.tenant = tenant;
   }

   @Override
   public String toString() {
      return tenant.getId();
   }
}
