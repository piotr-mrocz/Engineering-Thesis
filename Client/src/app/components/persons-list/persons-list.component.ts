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
  addNewUserResponse: BaseBackendResponse;

  userId: number;
  userRole: string;
  photoBaseAddress: string;
  isAuthorized: boolean;

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
    this.subscription.unsubscribe();
  }

  isUserAuthorized(): boolean {
    this.isAuthorized = this.userRole == Roles.admin.toString();
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
}
