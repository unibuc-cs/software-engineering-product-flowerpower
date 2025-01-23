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

  constructor(private userService: UserService) {}

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
}