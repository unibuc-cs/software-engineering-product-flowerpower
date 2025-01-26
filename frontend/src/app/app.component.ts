import {Component, OnInit} from '@angular/core';
import {Router, RouterOutlet} from '@angular/router';
import { HttpClient } from "@angular/common/http";
import { FormsModule } from '@angular/forms';
import {Toast} from "primeng/toast";

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet,
    FormsModule,
    Toast
  ],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  constructor(       
      private router: Router
  
  ) {
  }
  ngOnInit(): void {
    
  }
}