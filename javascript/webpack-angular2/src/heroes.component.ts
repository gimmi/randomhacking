import { Component, OnInit } from '@angular/core';
import { Hero } from './hero';
import { HeroService } from './hero.service';

@Component({
	selector: 'my-heroes',
	template: require('./heroes.component.html'),
})
export class HeroesComponent implements OnInit {
	heroes: Hero[];
	selectedHero: Hero;

	constructor(private heroService: HeroService) { }

	ngOnInit(): void {
		this.heroService.getHeroes().then(x => this.heroes = x);
	}

	onSelect(hero: Hero): void {
		this.selectedHero = hero;
	}
}
