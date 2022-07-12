import { Component, OnInit } from '@angular/core';
import { PresenceService } from 'src/app/services/presence.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { UserPresentsPerDayDto } from 'src/app/models/dto/userPresentsPerDayDto';

@Component({
  selector: 'app-user-presence-supervisor',
  templateUrl: './user-presence-supervisor.component.html',
  styleUrls: ['./user-presence-supervisor.component.css']
})
export class UserPresenceSupervisorComponent implements OnInit {

  private subscription: Subscription;
  today: Date;
  day: number;
  month: number;
  year: number;
  presenceResponse: BackendResponse<UserPresentsPerDayDto[]>;

  constructor(private presenceService: PresenceService) {
   }

  ngOnInit() {
    this.today = new Date();
    this.day = this.today.getDate();
    this.month = this.today.getMonth() + 1;
    this.year = this.today.getFullYear();

    this.getPresences(this.day, this.month, this.year);
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  getActualDate(dateTime: Date) {
    var date = new Date(dateTime.getTime());
    date.setHours(0, 0, 0, 0);
    return date;
  }

  getPresences(day: number, month: number, year: number) {
console.log(day + "." + month + "." + year)

    this.presenceService.getUsersPresencesPerDay(day, month, year);
    this.subscription = this.presenceService.getUsersPresencesPerDayResponse$.subscribe(x => {
      this.presenceResponse = x;
    });
  }
}
