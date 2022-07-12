import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { GetPresenceByIdUserDto } from 'src/app/models/dto/getPresenceByIdUserDto';
import { PresenceService } from 'src/app/services/presence.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GetPresenceByIdUserListDto } from 'src/app/models/dto/getPresenceByIdUserListDto';
import { AbsenceTypes } from 'src/app/models/enums/absenceType';

@Component({
  selector: 'app-user-presence',
  templateUrl: './user-presence.component.html',
  styleUrls: ['./user-presence.component.css']
})
export class UserPresenceComponent implements OnInit, OnDestroy {

  holiday: number = AbsenceTypes.holiday;
  unauthorizedAbsence: number = AbsenceTypes.unauthorizedAbsence;
  weekend: number = AbsenceTypes.weekend;

  private subscription: Subscription;
  presenceResponse: BackendResponse<GetPresenceByIdUserListDto>;
  userId: number;
  month: number;
  year: number;

  maxDate: string = `${this.year}-${this.month}`;

  constructor(private presenceService: PresenceService,
    private authService: AuthenticationService) { }

  ngOnInit() {
    this.month = new Date().getMonth() + 1;
    this.year = new Date().getFullYear();
    this.userId = this.authService.user.id;

    this.getPresences(this.userId, this.month, this.year);
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  getPresences(userId: number, month: number, year: number) {
    this.presenceService.getAllUserPresenceByIdUser(userId, month, year);
    this.subscription = this.presenceService.getAllUserResponse$.subscribe(x => {
      this.presenceResponse = x;
    });
  }

  selectMonth(date) {
    var dates = date.split('-');
    var month = dates[1];
    var year = dates[0];

    this.month = month;
    this.year = year;

    this.getPresences(this.userId, month, year);
  }
}
