'use strict';

var Component = require('@angular/core').Component;
var HeroService = require('./hero.service');

module.exports = Component({
	selector: 'my-heroes',
	template: require('./heroes.component.html'),
}).Class({
	constructor: [HeroService, function (heroService) {
		this.heroService = heroService;
	}],
	ngOnInit: function () {
		this.heroService.getHeroes().then(x => this.heroes = x);
	},
	onSelect: function (hero) {
		this.selectedHero = hero;
	}
})
