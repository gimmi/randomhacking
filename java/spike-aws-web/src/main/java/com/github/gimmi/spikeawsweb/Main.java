package com.github.gimmi.spikeawsweb;

import io.undertow.Handlers;
import io.undertow.Undertow;
import io.undertow.server.handlers.PathHandler;
import io.undertow.servlet.Servlets;
import io.undertow.servlet.api.DeploymentInfo;
import io.undertow.servlet.api.DeploymentManager;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.util.ObjectUtils;
import org.springframework.util.StringUtils;
import org.springframework.web.context.ContextLoaderListener;
import org.springframework.web.context.support.AnnotationConfigWebApplicationContext;
import org.springframework.web.context.support.HttpRequestHandlerServlet;

import static org.springframework.util.StringUtils.hasText;

public class Main {
   public static final int PORT = 5000; // Elastic Beanstalk default, see https://docs.aws.amazon.com/elasticbeanstalk/latest/dg/java-se-platform.html
   public static final String HOST = "0.0.0.0";

   public static void main(String[] args) throws Exception {
      Logger logger = LoggerFactory.getLogger(Main.class);
      Package pkg = Main.class.getPackage();

      logger.info("Starting {} v{} - {}", pkg.getSpecificationTitle(), pkg.getSpecificationVersion(), pkg.getImplementationVersion());

      String contextPath = "/";
      DeploymentInfo servletBuilder = Servlets.deployment()
         .setClassLoader(Main.class.getClassLoader())
         .setContextPath(contextPath)
         .setDeploymentName("spike-aws-web.war")
         .addListener(Servlets.listener(ContextLoaderListener.class))
         .addInitParameter("contextClass", AnnotationConfigWebApplicationContext.class.getName())
         .addInitParameter("contextConfigLocation", WebApiConfig.class.getName())
         .addServlets(
            Servlets.servlet("pushHttpRequestHandler", HttpRequestHandlerServlet.class).addMapping("/push"),
            Servlets.servlet("popHttpRequestHandler", HttpRequestHandlerServlet.class).addMapping("/pop")
         );

      DeploymentManager manager = Servlets.defaultContainer().addDeployment(servletBuilder);
      manager.deploy();
      PathHandler path = Handlers.path(Handlers.redirect(contextPath)).addPrefixPath(contextPath, manager.start());

      Undertow server = Undertow.builder()
         .addHttpListener(PORT, HOST) // TODO for ssl see http://stackoverflow.com/a/26603219
         .setHandler(path)
         .build();
      server.start();

      logger.info("Listening for HTTP requests on {}:{}", HOST, PORT);
   }
}
