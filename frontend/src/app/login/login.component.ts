import {Component, Injectable} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Button} from "primeng/button";
import {MessageModule} from "primeng/message";

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
        MessageModule,
    ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
@Injectable({providedIn: 'root'})
export class LoginComponent {
    form: FormGroup;
    secretKey: string = ''
    errorMessage: string | null = null

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
        this.http.post<any>('api/login', this.form.value).subscribe( {
            next: res => {
                sessionStorage.setItem("userId", res.userId);
                sessionStorage.setItem("username", res.username);
                this.router.navigate(['']);
            },
            error: err => {
                this.errorMessage = "Invalid email or password";
            }
        });
    }       
    
    goToRegister(){
        this.router.navigate(['/register']);
    }
}
