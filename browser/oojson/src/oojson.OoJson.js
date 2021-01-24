var oojson = oojson || {};

oojson.OoJson = function (nativeJson, factories) {
	this.nativeJson = nativeJson;
	this.factories = factories;
};

oojson.OoJson.prototype.stringify = function () {
	return this.nativeJson.stringify.apply(this, arguments);
};

oojson.OoJson.prototype.parse = function (text, reviver) {
	var sf = this.factories;
	return this.nativeJson.parse(text, function (key, value) {
		for ( var i = 0; i < sf.length; i++) {
			if (sf[i].canCreate(key, value)) {
				value = sf[i].create(key, value);
				i = sf.length;
			}
		}
		return (typeof reviver === 'function' ? reviver(key, value) : value);
	});
};
