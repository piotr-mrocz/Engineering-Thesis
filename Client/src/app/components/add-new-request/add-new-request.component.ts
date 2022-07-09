import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { PossibleAbsenceToChooseDto } from 'src/app/models/dto/possibleAbsenceToChooseDto';
import { RequestForLeaveService } from 'src/app/services/request-for-leave.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { RequestForLeaveToAddDto } from 'src/app/models/dto/requestForLeaveToAddDto';
import { Roles } from 'src/app/models/enums/roles.enum';

@Component({
  selector: 'app-add-new-request',
  templateUrl: './add-new-request.component.html',
  styleUrls: ['./add-new-request.component.css']
})
export class AddNewRequestComponent implements OnInit, OnDestroy {

  private subscription: Subscription;
  userId: number;
  userRole: string;
  absenceTypesResponse: BackendResponse<PossibleAbsenceToChooseDto[]>;

  constructor(private requestService: RequestForLeaveService,
    private authService: AuthenticationService) {
    this.userId = this.authService.user.id;
    this.userRole = this.authService.user.role;
  }

  ngOnInit() {
    this.getAllPossibleAbsenceTypeToChoose();
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  addNewRequest(newRequestData) {
    var newRequest = new RequestForLeaveToAddDto();
    newRequest.idUser = this.userId;
    newRequest.startDate = newRequestData.startDate;
    newRequest.endDate = newRequestData.endDate;
    newRequest.absenceType = newRequestData.absenceType;
    newRequest.isManager = this.userRole.toLowerCase() == Roles.manager.toString();

    var validation = this.validateForm(newRequest);

    // if (!validation) {
    //   alert("Nie uzupełniono wszystkich wymaganych pól!");
    //   return;
    // }

    this.addNewRequestAfterValidation(newRequest);
  }

  validateForm(newRequestModel: RequestForLeaveToAddDto): boolean {
    if (newRequestModel.startDate && newRequestModel.endDate && newRequestModel.absenceType > 0) {
      return true;
    }
    else {
      return false;
    }
  }

  private addNewRequestAfterValidation(newRequestModel: RequestForLeaveToAddDto) {
    this.requestService.createRequestForLeave(newRequestModel);
    this.subscription = this.requestService.createRequestForLeaveResponse$.subscribe(x => {

      if (x.succeeded != undefined) {
        if (x.succeeded) {
          window.location.reload();
        }

        alert(x.message);
      }
    });
  }

  getAllPossibleAbsenceTypeToChoose() {
    this.requestService.getAllPossibleAbsenceTypeToChoose();
    this.subscription = this.requestService.getAllPossibleAbsenceTypeToChooseResponse$.subscribe(x => {
      this.absenceTypesResponse = x;
    });
  }

  resetForm(idForm: string) {
    var form = <HTMLFormElement>document.getElementById(idForm);
    form.reset();
  }
}
