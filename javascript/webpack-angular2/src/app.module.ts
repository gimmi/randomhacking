import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { HeroesComponent } from './heroes.component';
import { HeroDetailComponent } from './hero-detail.component';
import { HeroService } from './hero.service';
import { AppComponent } from './app.component';
import { DashboardComponent } from './dashboard.component'

const routes: Routes = [
	{ path: 'heroes', component: HeroesComponent },
	{ path: 'dashboard', component: DashboardComponent },
	{ path: '', redirectTo: '/dashboard', pathMatch: 'full' },
];

@NgModule({
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
})
export class AppModule { }
