import { Component } from '@angular/core';
import {HeaderComponent} from "../header/header.component";
import {HomeHeroComponent} from "../home-hero/home-hero.component";
import {RouterOutlet} from "@angular/router";

@Component({
  selector: 'app-home',
  standalone: true,
    imports: [
        HeaderComponent,
        HomeHeroComponent,
        RouterOutlet
    ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

}
