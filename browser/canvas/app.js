App = function (canvas) {
	this.canvas = canvas;
	this.context = canvas.getContext("2d");
};
App.prototype = {
	drawImage: function (src, x, y) {
		var me = this;
		var img = new Image();
		img.onload = function() {
			me.context.drawImage(img, x, y);
		};
		img.src = src;
	}
};