import { Component, OnInit, OnDestroy, APP_BOOTSTRAP_LISTENER, ViewChild } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { PersonService } from '../../services/person-service';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { DepartmentService } from 'src/app/services/department.service';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { DepartmentDto } from 'src/app/models/dto/departmentDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { ApplicationSettings } from 'src/app/models/consts/applicationSettings';
import { Roles } from 'src/app/models/enums/roles.enum';
import { BaseBackendResponse } from 'src/app/models/response/BaseBackendResponse';

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit, OnDestroy {

  departmentResponse: BackendResponse<DepartmentDto[]>;
  userResponse: BackendResponse<UserDetailsDto[]>;

  userId: number;
  userRole: string;
  photoBaseAddress: string;
  isAuthorized: boolean;
  showEditVacationDaysDiv: boolean;

  private subscription: Subscription;

  constructor(private personService: PersonService,
    private departmentService: DepartmentService,
    private authService: AuthenticationService,
    private applicationSettings: ApplicationSettings) { }

  ngOnInit(): void {
    this.departmentService.getAllDepartments();

    this.subscription = this.departmentService.departmentsResponse$.subscribe(x => {
      this.departmentResponse = x;
    });

    this.getAllUsers("allUsers");
    this.userId = this.authService.user.id;
    this.userRole = this.authService.user.role;
    this.photoBaseAddress = this.applicationSettings.userPhotoBaseAddress;

    this.isUserAuthorized();
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  isUserAuthorized(): boolean {
    var admin = Roles[Roles.admin];
    this.isAuthorized = this.userRole.toLocaleLowerCase() == admin.toLocaleLowerCase();
    return this.isAuthorized;
  }

  getUsersByIdDepartment(idDepartment: number, idClickedButton: string) {
    this.changeClickedButtonBackgroundColor(idClickedButton);

    this.personService.getUsersByIdDepartment(idDepartment);

    this.subscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }

  getAllUsers(idClickedButton: string) {

    if (idClickedButton) {
      this.changeClickedButtonBackgroundColor(idClickedButton);
    }

    this.personService.getAllUsers();

    this.subscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }

  changeClickedButtonBackgroundColor(idClickedButton: string) {
    var buttons = (<HTMLScriptElement[]><any>document.getElementsByClassName("getUsersButton"));

    //reset styles all buttons
    for (let i = 0; i < buttons.length; i++) {
      var button = buttons[i];
      button.style.backgroundColor = "";
    }

    var clickedButton = (<HTMLScriptElement><any>document.getElementById(idClickedButton));

    //set background color clicked button
    clickedButton.style.backgroundColor = "#17a2b8";
  }

  releaseUser(personId) {
    var answer = confirm("Czy jesteś pewny?");

    if (answer) {
      this.personService.releaseUser(personId);
      this.subscription = this.personService.releaseUserResponse$.subscribe(x => {
        if (x.succeeded != undefined) {
          alert(x.message);

          if (x.succeeded) {
            window.location.reload();
          }
        }
      });
    }
  }

  resetUserPassword(idUser: number) {
    this.personService.resetUserPassword(idUser);
    this.subscription = this.personService.resetUserPasswordResponse$.subscribe(x => {
      alert(x.message);

      if (x.succeeded) {
        window.location.reload();
      }
    });
  }

  toggleEditVacationDays() {
    this.showEditVacationDaysDiv = !this.showEditVacationDaysDiv;
  }

  addVacationsDays(vacationDays: number, userId: number) {
    if (vacationDays > 0 && userId > 0) {
      this.personService.addVacationDaysToNewUser(vacationDays, userId);
      this.subscription = this.personService.addVacationDaysToNewUserResponse$.subscribe(x => {
        if (x.succeeded != undefined) {
          alert(x.message);

          if (x.succeeded) {
            window.location.reload();
          }
        }
      });
    }
    else {
      alert("Nie uzupełniono wszystkich danych!");
    }
  }
}
