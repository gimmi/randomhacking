import { Component, OnInit } from '@angular/core';
import { Hero }  from './hero';
import HeroService from './hero.service';

@Component({
	selector: 'my-heroes',
	template: `
		<h2>My Heroes</h2>
		<ul>
			<li *ngFor="let hero of heroes" [class.selected]="hero === selectedHero" (click)="onSelect(hero)">{{hero.id}} {{hero.name}}</li>
		</ul>
		<my-hero-detail [hero]="selectedHero"></my-hero-detail>
	`,
})
export default class HeroesComponent implements OnInit {
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
