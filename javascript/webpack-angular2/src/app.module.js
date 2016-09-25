'use strict';

var NgModule = require('@angular/core').NgModule;
var BrowserModule = require('@angular/platform-browser').BrowserModule;
var FormsModule = require('@angular/forms').FormsModule;
var Routes = require('@angular/router').Routes; 
var RouterModule = require('@angular/router').RouterModule;

var HeroesComponent = require('./heroes.component');
var HeroDetailComponent = require('./hero-detail.component');
var HeroService = require('./hero.service');
var AppComponent = require('./app.component');
var DashboardComponent = require('./dashboard.component');

const routes = [
	{ path: 'heroes', component: HeroesComponent },
	{ path: 'dashboard', component: DashboardComponent },
	{ path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

module.exports = NgModule({
	imports: [
		BrowserModule,
		FormsModule,
		RouterModule.forRoot(routes, { useHash: true }),
	],
	declarations: [
		AppComponent,
		HeroesComponent,
		HeroDetailComponent,
		DashboardComponent,
	],
	providers: [HeroService],
	bootstrap: [AppComponent],
}).Class({
	constructor: function () {}
});
