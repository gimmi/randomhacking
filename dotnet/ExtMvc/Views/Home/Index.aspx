<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<!doctype html>
<html>
	<head>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
		<link rel="stylesheet" type="text/css" href="ext/resources/css/ext-all.css" />
		<script type="text/javascript" src="js/json2.js"></script>
		<script type="text/javascript" src="ext/adapter/ext/ext-base-debug-w-comments.js"></script>
		<script type="text/javascript" src="ext/ext-all-debug-w-comments.js"></script>
		<script type="text/javascript" src="js/Rpc.js"></script>
		<script type="text/javascript" src="js/Ext.ux.ProxyField.js"></script>
		<script type="text/javascript" src="js/ExtMvc.Ns.CategoryGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressGridPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressJsonReader.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryNormalSearchPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryNormalSearchFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressFormPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicListField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressListField.js"></script>
				<script type="text/javascript" src="js/ExtMvc.MainViewPort.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressColumn.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.Category.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographic.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.Customer.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.Employee.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetail.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Order.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Product.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Region.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Shipper.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Supplier.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Territory.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Address.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerDemographicEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.AddressEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryEditWindow.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CategoryEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.CustomerEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.Ns.EmployeeEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderDetailEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.OrderEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ProductEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.RegionEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.ShipperEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.SupplierEditPanel.js"></script>
				<script type="text/javascript" src="js/ExtMvc.TerritoryEditPanel.js"></script>
				
		<script type="text/javascript">
			"use strict";

			Ext.BLANK_IMAGE_URL = 'ext/resources/images/default/s.gif';
			Ext.USE_NATIVE_JSON = true;

			Ext.onReady(function () {
				Ext.QuickTips.init();
				Rpc.init();
				var mainViewport = new ExtMvc.MainViewport();
			});
		</script>
		<title></title>
	</head>
	<body>
	</body>
</html>