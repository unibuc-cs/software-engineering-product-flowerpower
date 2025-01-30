import {Component, Injectable} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Button} from "primeng/button";

@Component({
  selector: 'app-login',
  standalone: true,
    imports: [
        MatFormField,
        MatButton,
        MatInput,
        MatLabel,
        MatError,
        ReactiveFormsModule,
        NgIf,
        Button
    ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
@Injectable({providedIn: 'root'})
export class LoginComponent {
    form: FormGroup;
    secretKey: string = ''

    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private router: Router
    ) {
        this.form = this.fb.group({
            Email: ['', [Validators.required, Validators.email]],
            Password: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    onSubmit() {
        if (this.form.invalid) {
            return;
        }

        // Daca aveti alt port la backend mergeti la Properties/launchsettings.json la https si puneti 7077
        this.http.post<any>('api/login', this.form.value).subscribe(res => {
            sessionStorage.setItem("userId", res.userId);
            sessionStorage.setItem("username" , res.username);
            this.router.navigate(['']);
        });
    }
    
    goToRegister(){
        this.router.navigate(['/register']);
    }
}
