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
    return this.http.get<any>(`api/search?username=${username}`);
  }

  sendFriendRequest(senderId: number, receiverId: number): Observable<any> {
    return this.http.post<any>(`api/friendrequest/send?senderId=${senderId}&receiverId=${receiverId}`, {});
  }
  
  getAllUsers(): Observable<any[]> {
    return this.http.get<any[]>(`api/friend/all`);
  }
  getFriendsList(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`api/friend/${userId}/friends`);
  }

  getAllFriendRequests(userId: number, isAccepted?: boolean): Observable<any[]> {
    let url = `api/friendrequest/all?userId=${userId}`;
    if (isAccepted !== undefined) {
      url += `&isAccepted=${isAccepted}`;
    }
    return this.http.get<any[]>(url);
  }

  createGroup(name: string, ownerId: number): Observable<any> {
    const groupDto = { name, ownerId };
    return this.http.post<any>(`api/group/create`, groupDto);
  }

  getUserGroups(userId: number): Observable<any[]> {
    return this.http.get<any[]>(`api/group/user/${userId}/groups`);
  }

  acceptFriendRequest(requestId: number): Observable<any> {
    return this.http.post<any>(`api/friendrequest/accept?requestId=${requestId}`, {});
  }

  addMemberToGroup(groupId: number, userId: number): Observable<any> {
    return this.http.post<any>(`api/group/${groupId}/add-member`, userId);
  }

  getGroupDetails(groupId: number): Observable<any> {
    return this.http.get<any>(`api/group/${groupId}`);
  }
}