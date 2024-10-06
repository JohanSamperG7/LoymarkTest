import { HttpClient, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { User, UserToRegister } from '../business/user/user.component';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:44393/api/User';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    return this.http.get<User[]>(this.apiUrl);
  }

  registerUser(user: UserToRegister){
    return this.http.post<User>(this.apiUrl, user);
  }

  updateUser(user: User){
    return this.http.patch<User>(this.apiUrl, user);
  }

  deleteUser(id: number): Observable<HttpResponse<any>>{
    return this.http.delete<HttpResponse<any>>(this.apiUrl+`/${id}`, { observe: 'response' });
  }
}
