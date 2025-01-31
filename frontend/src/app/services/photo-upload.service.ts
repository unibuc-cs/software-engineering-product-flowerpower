import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


export interface Photo {
  id: number;
  username: string;
  blob: string;
  uploadTime: string;
}
@Injectable({
  providedIn: 'root'
})
export class PhotoUploadService {

  constructor(private http: HttpClient) {}
  baseLink:string = "api/photos";
  
  

  uploadPhoto(file: any , userId: any , groupId:any): Observable<any> {
    const formData = new FormData()
    formData.append('file', file);
    formData.append('userId', userId.toString());
    formData.append('groupId', groupId.toString());
    return this.http.post<any>(this.baseLink + "/upload", formData);
    
  }

  getUserPhotos(userId: number): Observable<Photo[]> {
    return this.http.get<Photo[]>(`${this.baseLink}/user/${userId}`);
  }

  getUserUploadedPhotos(userId: number): Observable<Photo[]> {
    return this.http.get<Photo[]>(`${this.baseLink}/uploaded/${userId}`);
  }
}