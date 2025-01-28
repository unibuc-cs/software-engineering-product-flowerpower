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
  
  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/friend/all`);
  }
  getFriendsList(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/friend/${userId}/friends`);
  }

  getAllFriendRequests(userId: number, isAccepted?: boolean): Observable<any[]> {
    let url = `${this.apiUrl}/friendrequest/all?userId=${userId}`;
    if (isAccepted !== undefined) {
      url += `&isAccepted=${isAccepted}`;
    }
    return this.http.get<any[]>(url);
  }

  acceptFriendRequest(requestId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/friendrequest/accept?requestId=${requestId}`, {});
  }
}