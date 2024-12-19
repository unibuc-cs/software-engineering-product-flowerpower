import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import {HttpClient, provideHttpClient} from "@angular/common/http";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
      RouterOutlet,
      
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
    
}
