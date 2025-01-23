import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    FormsModule
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  // ...existing code...
}