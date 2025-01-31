import {Component, Injectable, OnInit} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {ListboxModule} from "primeng/listbox";
import {ButtonModule} from "primeng/button";
import {DatePipe, NgForOf, NgIf} from "@angular/common";
import {Router} from "@angular/router";

interface Notification {
    username: string;
    uploadTime: string;
}
@Injectable({providedIn: 'root'})
@Component({
    selector: 'app-notifications',
    templateUrl: './notifications.component.html',
    standalone: true,
    imports: [
        ListboxModule,
        ButtonModule,
        NgIf,
        DatePipe,
        NgForOf
    ],
    styleUrls: ['./notifications.component.css']
})
export class NotificationsComponent implements OnInit {
    notifications: Notification[] = [];
    userId: string | null = sessionStorage.getItem("userId");

    constructor(
        private http: HttpClient,
        private router: Router
    ) {  }

    ngOnInit(): void {
        this.http.get<Notification[]>(`api/notifications/get-notifications/${this.userId}`)
            .subscribe({
                next: res => {
                    this.notifications = res;
                }
            });
    }
    
    navigateToDailyPhotos() {
        this.router.navigate(['/home/dailyPhoto']);
    }
    
}
