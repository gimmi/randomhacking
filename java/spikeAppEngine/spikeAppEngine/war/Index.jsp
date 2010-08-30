<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="extjs/resources/css/ext-all.css" />
<script type="text/javascript" src="extjs/adapter/ext/ext-base-debug-w-comments.js"></script>
<script type="text/javascript" src="extjs/ext-all-debug-w-comments.js"></script>
<script type="text/javascript" src="djn/directprovider/src=test-api.js"></script>
<script type="text/javascript">
	"use strict";

	Ext.BLANK_IMAGE_URL = 'extjs/resources/images/default/s.gif';
	Ext.USE_NATIVE_JSON = true;

	Ext.onReady(function() {
		Ext.QuickTips.init();
		Ext.Direct.addProvider(Ext.app.test.REMOTING_API);
		Ext.app.test.TestAction.doEcho('Hello world', function(result, e) {
			alert(result);
		});
	});
</script>
<title></title>
</head>
<body>
<a href="/Login?destinationUrl=<%= request.getAttribute("destinationUrl") %>"><%= request.getAttribute("userName") %></a>
</body>
</html>