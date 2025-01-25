import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-friend-requests',
  standalone: true,
  imports: [FormsModule, CommonModule],
  templateUrl: './friend-requests.component.html',
  styleUrls: ['./friend-requests.component.css']
})
export class FriendRequestsComponent implements OnInit {
  friendRequests: any[] = [];

  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.getFriendRequests();
  }

  getFriendRequests(): void {
    this.userService.getAllFriendRequests().subscribe(
      (data: any[]) => {
       
        this.friendRequests = data.filter(request => !request.isAccepted);
        console.log(this.friendRequests);
      },
      (error: any) => {
        console.error('Error fetching friend requests', error);
      }
    );
  }

  acceptFriendRequest(requestId: number): void {
    this.userService.acceptFriendRequest(requestId).subscribe(
      (response: any) => {
        console.log('Friend request accepted', response);
        this.getFriendRequests(); // Refresh the list after accepting a request
      },
      (error: any) => {
        console.error('Error accepting friend request', error);
      }
    );
  }
}