import { Component, Input } from '@angular/core';
import { Hero }  from './hero';

@Component({
	selector: 'my-hero-detail',
	template: require('./hero-detail.component.html'),
})
export default class HeroDetailComponent {
	@Input()
	hero: Hero
}