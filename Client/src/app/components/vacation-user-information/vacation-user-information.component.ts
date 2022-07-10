import { Component, OnInit, OnDestroy } from '@angular/core';
import { RequestForLeaveService } from 'src/app/services/request-for-leave.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Subscription } from 'rxjs';
import { UserVacationInfoDto } from 'src/app/models/dto/userVacationInfoDto';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { GetAllUserRequestsForLeaveDto } from 'src/app/models/dto/getAllUserRequestsForLeaveDto';
import { PossibleAbsenceToChooseDto } from 'src/app/models/dto/possibleAbsenceToChooseDto';
import { RequestStatusEnum } from 'src/app/models/enums/requestStatus.enum';
import { ParamMap, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-vacation-user-information',
  templateUrl: './vacation-user-information.component.html',
  styleUrls: ['./vacation-user-information.component.css']
})
export class VacationUserInformationComponent implements OnInit, OnDestroy {

  idUserRoute: number;
  userId: number;
  thisYear: number;
  startJobYear: number;
  years = new Array();
  userVacationInfoResponse: BackendResponse<UserVacationInfoDto>;
  userRequestsForLeaveResponse: BackendResponse<GetAllUserRequestsForLeaveDto[]>;

  forConsiderationStatus: number;
  acceptedBySupervisorStatus: number;
  rejectedBySupervisorStatus: number;
  removedByUserStatus: number;

  private subscription: Subscription;
  routeSub: Subscription;

  constructor(private requestService: RequestForLeaveService,
    private authService: AuthenticationService,
    private route: ActivatedRoute) {
      this.thisYear = (new Date()).getFullYear();

      this.routeSub = this.route.params.subscribe(params => {
        this.idUserRoute = params['id'];
      });

      this.userId = this.idUserRoute != undefined ? this.idUserRoute : this.authService.user.id;
  }

  ngOnInit() {
    this.getUserVacationDaysInfo();
    this.getAllUserRequestsForLeave((new Date()).getFullYear());
    this.createYearsArray();

    this.forConsiderationStatus = RequestStatusEnum.forConsideration;
    this.acceptedBySupervisorStatus = RequestStatusEnum.acceptedBySupervisor;
    this.rejectedBySupervisorStatus = RequestStatusEnum.rejectedBySupervisor;
    this.removedByUserStatus = RequestStatusEnum.removedByUser;
  }

  ngOnDestroy() {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }

    if (this.routeSub != undefined) {
      this.routeSub.unsubscribe();
    }
  }

  loadRequests(year) {
    this.thisYear = year;
    this.getAllUserRequestsForLeave(year);
  }

  createYearsArray() {
      var lastFiveYears = this.thisYear - 4;
      for(let i = this.thisYear; i >= lastFiveYears; i--) { 
        this.years.push(i);
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

  removeRequest(requestId: number) {
    this.requestService.removeRequestForLeaveByUser(requestId);
    this.subscription = this.requestService.removeRequestForLeaveByUserResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        if (x.succeeded) {
          window.location.reload();
        }

        alert(x.message);
      }
    });
  }
}