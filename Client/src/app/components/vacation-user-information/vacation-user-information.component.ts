import { Component, OnInit, OnDestroy } from '@angular/core';
import { RequestForLeaveService } from 'src/app/services/request-for-leave.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Subscription } from 'rxjs';
import { UserVacationInfoDto } from 'src/app/models/dto/userVacationInfoDto';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { GetAllUserRequestsForLeaveDto } from 'src/app/models/dto/getAllUserRequestsForLeaveDto';
import { PossibleAbsenceToChooseDto } from 'src/app/models/dto/possibleAbsenceToChooseDto';

@Component({
  selector: 'app-vacation-user-information',
  templateUrl: './vacation-user-information.component.html',
  styleUrls: ['./vacation-user-information.component.css']
})
export class VacationUserInformationComponent implements OnInit, OnDestroy {

  userId: number;
  userVacationInfoResponse: BackendResponse<UserVacationInfoDto>;
  userRequestsForLeaveResponse: BackendResponse<GetAllUserRequestsForLeaveDto[]>;
  private subscription: Subscription;

  constructor(private requestService: RequestForLeaveService,
    private authService: AuthenticationService) {
      this.userId = this.authService.user.id;
   }

  ngOnInit() {
    this.getUserVacationDaysInfo();
    this.getAllUserRequestsForLeave((new Date()).getFullYear());
  }

  ngOnDestroy() {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  getUserVacationDaysInfo() {
    this.requestService.getInformationAboutUserVacationDays(this.userId);
    this.subscription = this.requestService.getInformationAboutUserVacationDaysResponse$.subscribe(x => {
      this.userVacationInfoResponse = x;
    });    
  }

  getAllUserRequestsForLeave(year: number) {
    this.requestService.getAllUserRequestsForLeave(this.userId, year);
    this.subscription = this.requestService.getAllUserRequestForLeaveResponse$.subscribe(x => {
      this.userRequestsForLeaveResponse = x;
    });
  }
}
