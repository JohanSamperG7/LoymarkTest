import { Component, OnInit } from '@angular/core';
import { ActivityService } from '../../services/activity.service';
import { CommonModule } from '@angular/common';

export interface Activity {
  id: number;
  createdDate: string;
  userName: string;
  activityType: string;
}

@Component({
  selector: 'app-activity',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './activity.component.html',
  styleUrl: './activity.component.css'
})
export default class ActivityComponent implements OnInit {
  activities: Activity[] = [];

  constructor (private activityService: ActivityService){ }

  ngOnInit(): void {
    this.getActivities();
  }

  getActivities(){
    this.activityService.getActivitites().subscribe(res => {
      this.activities = res;
    });
  }
}
