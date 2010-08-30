<%@ page language="java" contentType="text/html; charset=ISO-8859-1" pageEncoding="ISO-8859-1"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<link rel="stylesheet" type="text/css" href="extjs/resources/css/ext-all.css" />
<script type="text/javascript" src="extjs/adapter/ext/ext-base-debug-w-comments.js"></script>
<script type="text/javascript" src="extjs/ext-all-debug-w-comments.js"></script>
<script type="text/javascript" src="djn/directprovider/src=test-api.js"></script>
<title></title>
</head>
<body>
<form action="/Login" method="post">
<input type="hidden" name="destinationUrl" value="<%=request.getParameter("destinationUrl")%>">
<label>Login</label><br />
<input type="text" name="federatedIdentity" value=""><br />
<input type="submit" name="submit"></form>
</body>
</html>