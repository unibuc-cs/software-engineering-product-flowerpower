import {Component, Injectable} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Button, ButtonModule} from "primeng/button";
import {Toolbar, ToolbarModule} from "primeng/toolbar";
import {PhotoUploadComponent} from "../photo-upload/photo-upload.component";

@Component({
  selector: 'app-header',
  standalone: true,
    imports: [
        PhotoUploadComponent,
        ButtonModule,
        ToolbarModule
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
    
    goToFriendsList(){
        this.router.navigate(["/home/user"]);
    }

    goToHome(){
        this.router.navigate(["/home/hero"]);
    }
    
    goToMyPhotos(){
        this.router.navigate(["/home/myPhoto"])
    }
    
    goToFriendsPhotos(){
        this.router.navigate(["/home/dailyPhoto"])
    }
    
    goToNotifications() {
        this.router.navigate(["/home/notifications"]);
    }
    
    disconnect(){
        sessionStorage.clear();
        this.router.navigate(["/login"]);
    }

   
}
