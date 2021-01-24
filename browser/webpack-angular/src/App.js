var angular = require('angular');
var ngRoute = require('angular-route');

module.exports = angular.module('app', [ ngRoute ])
	.controller('AppCtrl', require('./AppCtrl'))
	.config(function ($routeProvider) {
		$routeProvider
			.when('/home', { template: require('raw!./AppCtrl.html') })
			.otherwise('/home');
	});
