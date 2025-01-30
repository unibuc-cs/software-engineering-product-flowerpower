// frontend/src/app/search-user/search-user.component.ts
import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-search-user',
  standalone: true,
  imports: [FormsModule, CommonModule, MatButtonModule],
  templateUrl: './search-user.component.html',
  styleUrls: ['./search-user.component.css']
})
export class SearchUserComponent implements OnInit {
  users: any[] = [];
  searchQuery: string = '';
  senderId: number;
  isSearchClicked: boolean = false;

  constructor(private userService: UserService) {
    const userId = sessionStorage.getItem('userId');
    this.senderId = userId ? parseInt(userId, 10) : 0;
  }

  ngOnInit(): void {}

  searchUsers(): void {
    this.isSearchClicked = true;

    if (this.searchQuery) {
      this.userService.searchUsers(this.searchQuery, this.senderId).subscribe(
        (data: any) => {
          this.users = data;
        },
        (error: any) => {
          console.error('Error searching users', error);
          this.users = [];
        }
      );
    } else {
      this.users = [];
    }
  }

  sendFriendRequest(receiverId: number): void {
    this.userService.sendFriendRequest(this.senderId, receiverId).subscribe(
      (response: any) => {
        console.log('Friend request sent successfully', response);
        this.searchUsers(); // Refresh the search results after sending a friend request
      },
      (error: any) => {
        console.error('Error sending friend request', error);
        if (error.status === 400) {
          alert('Friend request already exists or users are already friends');
        }
      }
    );
  }

  canSendFriendRequest(user: any): boolean {
    return !user.friendRequestSent && !user.isFriend;
  }
}