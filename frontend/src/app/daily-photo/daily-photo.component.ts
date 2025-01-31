import {Component, Injectable, OnInit} from '@angular/core';
import {FormBuilder} from '@angular/forms';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute, Router} from "@angular/router";
import {ButtonModule} from "primeng/button";
import {ToolbarModule} from "primeng/toolbar";
import {PhotoUploadComponent} from "../photo-upload/photo-upload.component";
import {Photo, PhotoUploadService} from "../services/photo-upload.service";
import {ProgressSpinnerModule} from "primeng/progressspinner";
import {CardModule} from "primeng/card";
import {DatePipe, NgForOf, NgIf} from "@angular/common";

@Component({
  selector: 'app-header',
  standalone: true,
    imports: [
        PhotoUploadComponent,
        ButtonModule,
        ToolbarModule,
        ProgressSpinnerModule,
        CardModule,
        NgIf,
        NgForOf,
        DatePipe
    ],
  templateUrl: './daily-photo.component.html',
  styleUrl: './daily-photo.component.css'
})
@Injectable({providedIn: 'root'})
export class DailyPhotoComponent implements OnInit {
    constructor(
        private http: HttpClient,
        private fb: FormBuilder,
        private route: ActivatedRoute,
        private photoService:PhotoUploadService
    ) {
       
    }

    photos: Photo[] = [];
    loading = true;
    error: string | null = null;
    userId:any;
    routeType:any;

    ngOnInit() {
        this.userId = sessionStorage.getItem("userId");
        this.route.url.subscribe((segments) => {
            this.routeType = segments[0]?.path;
            if (this.routeType === 'dailyPhoto') {
                this.loadPhotos();
            } else if (this.routeType === 'myPhoto') {
                this.loadUploadedPhotos();
            }
        });
    }

    loadPhotos() {
        console.log("Poze normale")
        this.photoService.getUserPhotos(this.userId).subscribe({
            next: (data) => {
                this.photos = data;
                this.loading = false;
            },
            error: (err) => {
                this.error = 'Failed to load photos.';
                this.loading = false;
            }
        });
    }
    
    loadUploadedPhotos(){
        console.log("Poze cu upload")
        this.photoService.getUserUploadedPhotos(this.userId).subscribe({
            next: (data) => {
                this.photos = data;
                this.loading = false;
            },
            error: (err) => {
                this.error = 'Failed to load photos.';
                this.loading = false;
            }
        });
    }
   
}
