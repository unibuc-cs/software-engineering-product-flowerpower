import {Component, Injectable, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators} from '@angular/forms';
import {MatError, MatFormField, MatLabel} from "@angular/material/form-field";
import {MatButton} from "@angular/material/button";
import {MatInput} from "@angular/material/input";
import {NgIf} from "@angular/common";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {Button, ButtonModule} from "primeng/button";
import {Toolbar} from "primeng/toolbar";
import {Dialog, DialogModule} from "primeng/dialog";
import {FileUpload} from "primeng/fileupload";
import {MessageService, PrimeTemplate} from "primeng/api";
import {PhotoUploadService} from "../services/photo-upload.service";
import {UserService} from "../user.service";
import {DropdownModule} from "primeng/dropdown";

@Component({
    selector: 'app-photo-upload',
    standalone: true,
    imports: [
        NgIf,
        ButtonModule,
        DialogModule,
        DropdownModule,
        FormsModule
    ],
    templateUrl: './photo-upload.component.html',
    styleUrl: './photo-upload.component.css'
})
@Injectable({providedIn: 'root'})
export class PhotoUploadComponent implements OnInit {
    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private router: Router,
        private messageService: MessageService,
        private photoUploadService: PhotoUploadService,
        private userService: UserService,
    ) {
        this.userId = sessionStorage.getItem("userId");
    }

    displayUploadDialog: boolean = false;
    selectedFileName: string = '';
    selectedFile: any = null;
    userId: any;
    userGroups: any;
    selectedVisibility:any;

    ngOnInit() {
        this.userService.getUserGroups(this.userId).subscribe({
            next: (data: any[]) => {
                this.userGroups = data;
                this.userGroups.unshift({id:-1 , name:"All friends"});
                console.log(this.userGroups);
            },
            error: (error: any) => {
                console.error('Error fetching user groups', error);
            }


        });
    }

    showDialog() {
        this.displayUploadDialog = true;
    }

    onUpload(event: any): void {
        if (this.selectedFile != null) {
            this.photoUploadService.uploadPhoto(this.selectedFile, sessionStorage.getItem("userId"), this.selectedVisibility.id).subscribe({
                next: data => {
                    console.log('Uploaded file:', this.selectedFile);
                    this.messageService.add({
                        severity: "succes",
                        summary: "Upload succesful",
                        detail: "The photo:" + this.selectedFileName + " has been uploaded"
                    })
                    this.displayUploadDialog = false;
                },
                error: error => {
                    console.log(error)
                    this.messageService.add({severity: "error", summary: "Upload failed", detail: "Error un upload"})

                }
            })
        } else {
            console.error('No file uploaded or event is invalid.');
            this.messageService.add({
                severity: "error",
                summary: "Invalid file selection",
                detail: "Invalid file selection"
            })
        }
    }

    onFileSelected(event: Event): void {
        const input = event.target as HTMLInputElement;
        if (input?.files && input.files.length > 0) {
            this.selectedFileName = input.files[0].name;
            this.selectedFile = input.files[0];
            console.log('Selected file:', this.selectedFileName);
        } else {
            console.error('No file selected or input is invalid.');
            this.selectedFileName = '';
        }
    }


}
