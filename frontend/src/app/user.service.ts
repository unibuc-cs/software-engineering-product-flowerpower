import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:7077/api'; // Asigură-te că această rută este corectă

  constructor(private http: HttpClient) {}

  searchUsers(username: string, currentUserId: number): Observable<any> {
    return this.http.get<any[]>(`${this.apiUrl}/friend/search?username=${username}&currentUserId=${currentUserId}`);
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
    return this.http.get<any[]>(`${this.apiUrl}/friendrequest/all?userId=${userId}&isAccepted=${isAccepted}`);
  }

  createGroup(name: string, ownerId: number): Observable<any> {
    const groupDto = { name, ownerId };
    return this.http.post<any>(`${this.apiUrl}/group/create`, groupDto);
  }

  getUserGroups(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/group/user/${userId}/groups`);
  }

  acceptFriendRequest(requestId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/friendrequest/accept?requestId=${requestId}`, {});
  }

  addMemberToGroup(groupId: number, userId: number): Observable<any> {
    return this.http.post<any>(`${this.apiUrl}/group/${groupId}/add-member`, userId);
  }

  getGroupDetails(groupId: number): Observable<any> {
    return this.http.get<any>(`${this.apiUrl}/group/${groupId}`);
  }
}