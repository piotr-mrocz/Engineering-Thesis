import { Component, OnInit, OnDestroy } from '@angular/core';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { Subscription } from 'rxjs';
import { RequestForLeaveService } from 'src/app/services/request-for-leave.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { GetAllRequestsForLeaveToAcceptDto } from 'src/app/models/dto/getAllRequestsForLeaveToAcceptDto';
import { Roles } from 'src/app/models/enums/roles.enum';

@Component({
  selector: 'app-vacation-supervisor',
  templateUrl: './vacation-supervisor.component.html',
  styleUrls: ['./vacation-supervisor.component.css']
})
export class VacationSupervisorComponent implements OnInit, OnDestroy {

  requestsForLeaveResponse: BackendResponse<GetAllRequestsForLeaveToAcceptDto[]>;

  private subscription: Subscription;
  userId: number;
  userRole: string;
  isAuthorized: boolean;

  constructor(private requestService: RequestForLeaveService,
    private authService: AuthenticationService) {
    this.userId = this.authService.user.id;
    this.userRole = this.authService.user.role;
    this.isUserAuthorized();
  }

  ngOnInit() {
    this.getAllRequestsForLeave(this.userId);
  }

  ngOnDestroy() {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  isUserAuthorized(): boolean {
    var manager: string = Roles[Roles.user];
    this.isAuthorized = this.userRole.toLocaleLowerCase() == manager.toLocaleLowerCase();
    return this.isAuthorized;
  }

  getAllRequestsForLeave(idUser: number) {
    if (this.isAuthorized) {
      this.requestService.getAllRequestsForLeaveToConfirmtByManager();
      this.subscription = this.requestService.getAllRequestToConfirmByManagerResponse$.subscribe(x => {
        this.requestsForLeaveResponse = x;
      });
    }
    else {
      this.requestService.getAllRequestToConfirm(idUser);
      this.subscription = this.requestService.getAllRequestToConfirmResponse$.subscribe(x => {
        this.requestsForLeaveResponse = x;
      });
    }

    this.subscription = this.requestService.getAllRequestToConfirmResponse$.subscribe(x => {
      this.requestsForLeaveResponse = x;
    });
  }

  acceptRequest(idRequest: number) {
    this.requestService.acceptRequestForLeave(idRequest);
    this.subscription = this.requestService.acceptRequestForLeaveResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        if (x.succeeded) {
          window.location.reload();
        }

        alert(x.message);
      }
    });
  }

  removeRequest(idRequest: number, reason: string) {
    if (reason) {
      this.requestService.rejectRequestForLeave(idRequest, reason);
      this.subscription = this.requestService.rejectRequestForLeaveResponse$.subscribe(x => {
        if (x.succeeded != undefined) {
          alert(x.message);

          if (x.succeeded) {
            window.location.reload();
          }
        }
      });
    }
    else {
      alert("Nie podano powodu odrzucenia wniosku!");
    }
  }
}
