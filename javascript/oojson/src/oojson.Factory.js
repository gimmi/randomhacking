oojson = oojson || {};

oojson.Factory = function (selectorFn, factoryFn) {
	this._selectorFn = selectorFn;
	this._factoryFn = factoryFn;
};

oojson.Factory.prototype.canCreate = function (key, json) {
	return !!this._selectorFn(key, json);
};

oojson.Factory.prototype.create = function (key, json) {
	return this._factoryFn(key, json);
};
