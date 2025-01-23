import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7077/api'; // Asigură-te că această rută este corectă

  constructor(private http: HttpClient) {}

  searchUsers(username: string): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/search?username=${username}`);
  }

  sendFriendRequest(senderId: number, receiverId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/friendrequest/send?senderId=${senderId}&receiverId=${receiverId}`, {});
  }
}