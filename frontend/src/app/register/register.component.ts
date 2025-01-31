import {Component, Injectable} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
    selector: 'app-register',
    imports: [
        MatFormField,
        MatButton,
        MatInput,
        MatLabel,
        MatError,
        ReactiveFormsModule,
        NgIf
    ],
    templateUrl: './register.component.html',
    standalone: true,
    styleUrl: './register.component.css'
})
@Injectable({providedIn: 'root'})
export class RegisterComponent {
    form: FormGroup;
    secretKey: string = ''

    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private router: Router
    ) {
        this.form = this.fb.group({
            Username: ['', [Validators.required, Validators.minLength(3)]],
            Email: ['', [Validators.required, Validators.email]],
            Password: ['', [Validators.required, Validators.minLength(6)]]
        });
    }

    onSubmit() {
        if (this.form.invalid) {
            return;
        }

        // Daca aveti alt port la backend mergeti la Properties/launchsettings.json la https si puneti 7077
        this.http.post('api/register', this.form.value).subscribe({
            next: res => {
                this.goToLogin();
            },
            error: err => {
                console.log(err);
            }
        });

    }


    goToLogin() {
        this.router.navigate(['/login']);
    }
}
