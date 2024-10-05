import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Activity } from '../business/activity/activity.component';

@Injectable({
  providedIn: 'root'
})
export class ActivityService {
  private apiUrl = 'https://localhost:44393/api/Activity';

  constructor(private http: HttpClient) { }

  getActivitites(): Observable<Activity[]> {
    return this.http.get<Activity[]>(this.apiUrl);
  }
}
