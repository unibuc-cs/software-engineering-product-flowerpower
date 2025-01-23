import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7077/api';

  constructor(private http: HttpClient) {}

  searchUsers(username: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/search?username=${username}`);
  }
}