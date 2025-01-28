import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  friends: any[] = [];
  friendRequests: any[] = [];
  userId: number | null = null;

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.userId = Number(sessionStorage.getItem("userId"));
    if (this.userId) {
      this.getFriendsList();
      this.getFriendRequests();
    } else {
      console.error('User ID not found in sessionStorage');
    }
  }

  getFriendsList(): void {
    if (this.userId) {
      this.userService.getFriendsList(this.userId).subscribe(
        (data: any[]) => {
          this.friends = data;
          console.log(this.friends);
        },
        (error: any) => {
          console.error('Error fetching friends list', error);
        }
      );
    }
  }

  getFriendRequests(): void {
    if (this.userId) {
      this.userService.getAllFriendRequests(this.userId, false).subscribe(
        (data: any[]) => {
          this.friendRequests = data;
          console.log(this.friendRequests);
        },
        (error: any) => {
          console.error('Error fetching friend requests', error);
        }
      );
    }
  }

  acceptFriendRequest(requestId: number): void {
    this.userService.acceptFriendRequest(requestId).subscribe(
      (response: any) => {
        console.log('Friend request accepted', response);
        this.getFriendRequests(); // Refresh the list after accepting a request
        this.getFriendsList(); // Refresh the friends list after accepting a request
      },
      (error: any) => {
        console.error('Error accepting friend request', error);
      }
    );
  }
}