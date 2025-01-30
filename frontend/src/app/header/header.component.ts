import {Component, Injectable} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Button} from "primeng/button";
import {Toolbar} from "primeng/toolbar";

@Component({
  selector: 'app-header',
  standalone: true,
    imports: [
        Toolbar,
        Button
    ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
@Injectable({providedIn: 'root'})
export class HeaderComponent {
    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private router: Router
    ) {
       
    }

   
}
