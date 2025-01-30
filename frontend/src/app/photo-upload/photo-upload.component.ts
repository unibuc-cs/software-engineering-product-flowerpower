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
import {Dialog} from "primeng/dialog";
import {FileUpload} from "primeng/fileupload";
import {PrimeTemplate} from "primeng/api";

@Component({
  selector: 'app-photo-upload',
  standalone: true,
    imports: [
        Toolbar,
        Button,
        Dialog,
        FileUpload,
        PrimeTemplate
    ],
  templateUrl: './photo-upload.component.html',
  styleUrl: './photo-upload.component.css'
})
@Injectable({providedIn: 'root'})
export class PhotoUploadComponent {
    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private router: Router
    ) {
       
    }

    displayUploadDialog: boolean = false;

    showDialog() {
        this.displayUploadDialog = true;
    }

    onUpload(event: any) {
        const file = event.files[0];
        console.log('Uploaded file:', file);
        
    }

   
}
