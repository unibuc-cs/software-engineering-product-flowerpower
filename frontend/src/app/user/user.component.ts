import { Component, OnInit } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { MatListModule } from '@angular/material/list';
import { MatIconModule } from '@angular/material/icon';
import { MatExpansionModule } from '@angular/material/expansion';
import {SearchUserComponent} from "../search-user/search-user.component";

@Component({
  selector: 'app-user',
  standalone: true,
  imports: [
    FormsModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatSelectModule,
    MatListModule,
    MatIconModule,
    MatExpansionModule,
    SearchUserComponent
  ],
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css']
})
export class UserComponent implements OnInit {
  friends: any[] = [];
  friendRequests: any[] = [];
  userId: number | null = null;
  groupForm: FormGroup;
  addMemberForm: FormGroup;
  userGroups: any[] = [];
  selectedGroup: any = null;

  constructor(private userService: UserService, private fb: FormBuilder) {
    this.groupForm = this.fb.group({
      groupName: ['', [Validators.required, Validators.minLength(3)]]
    });

    this.addMemberForm = this.fb.group({
      groupId: ['', [Validators.required]],
      memberId: ['', [Validators.required]]
    });
  }

  ngOnInit(): void {
    this.userId = Number(sessionStorage.getItem("userId"));
    if (this.userId) {
      this.getFriendsList();
      this.getFriendRequests();
      this.getUserGroups();
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

  createGroup(): void {
    if (this.groupForm.invalid || this.userId === null) {
      return;
    }

    const groupName = this.groupForm.get('groupName')?.value;
    this.userService.createGroup(groupName, this.userId).subscribe(
      (response: any) => {
        console.log('Group created successfully', response);
        this.groupForm.reset();
        this.getUserGroups(); // Refresh the groups list after creating a group
      },
      (error: any) => {
        console.error('Error creating group', error);
      }
    );
  }

  getUserGroups(): void {
    if (this.userId) {
      this.userService.getUserGroups(this.userId).subscribe(
        (data: any[]) => {
          this.userGroups = data;
          console.log(this.userGroups);
        },
        (error: any) => {
          console.error('Error fetching user groups', error);
        }
      );
    }
  }

  addMemberToGroup(): void {
    if (this.addMemberForm.invalid) {
      return;
    }

    const groupId = this.addMemberForm.get('groupId')?.value;
    const memberId = this.addMemberForm.get('memberId')?.value;

    this.userService.addMemberToGroup(groupId, memberId).subscribe(
      (response: any) => {
        console.log('Member added to group successfully', response);
        this.addMemberForm.reset();
        this.getGroupDetails(groupId); // Refresh the group details after adding a member
      },
      (error: any) => {
        console.error('Error adding member to group', error);
      }
    );
  }

  getGroupDetails(groupId: number): void {
    this.userService.getGroupDetails(groupId).subscribe(
      (data: any) => {
        this.selectedGroup = data;
        console.log(this.selectedGroup);
      },
      (error: any) => {
        console.error('Error fetching group details', error);
      }
    );
  }
}