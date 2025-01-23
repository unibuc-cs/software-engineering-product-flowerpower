import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-search-user',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
  users: any[] = [];
  searchQuery: string = '';
  senderId: number;

  constructor(private userService: UserService) {
    const userId = sessionStorage.getItem('userId');
    this.senderId = userId ? parseInt(userId, 10) : 0;
  }

  ngOnInit(): void {}

  searchUsers(): void {
    if (this.searchQuery) {
      this.userService.searchUsers(this.searchQuery).subscribe(
        (data: any) => {
          this.users = data;
        },
        (error: any) => {
          console.error('Error searching users', error);
        }
      );
    }
  }

  sendFriendRequest(receiverId: number): void {
    this.userService.sendFriendRequest(this.senderId, receiverId).subscribe(
      (response: any) => {
        console.log('Friend request sent successfully', response);
      },
      (error: any) => {
        console.error('Error sending friend request', error);
      }
    );
  }
}