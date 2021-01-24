var oojson = oojson || {};

oojson.OoJsonBuilder = function () {
	this.nativeJson = JSON;
	this.factories = [];
};

oojson.OoJsonBuilder.prototype.addFactory = function (selectorFn, factoryFn) {
	this.factories.push(new oojson.Factory(selectorFn, factoryFn));
};

oojson.OoJsonBuilder.prototype.build = function () {
	return new oojson.OoJson(this.nativeJson, this.factories);
};

oojson.OoJsonBuilder.prototype.replaceNativeJson = function () {
	if (!nativeJson) {
		throw 'Global JSON object not found';
	}
	if (nativeJson instanceof oojson.OoJson) {
		throw 'Global JSON object is already an OoJson object';
	}
	JSON = this.build();
};