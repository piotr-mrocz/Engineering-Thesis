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

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit, OnDestroy  {

  departmentResponse: BackendResponse<DepartmentDto[]>;
  userResponse: BackendResponse<UserDetailsDto[]>;
  userId: number;
  userRole: string;
  photoBaseAddress: string;
  isAuthorized: boolean;
  showModalWindow: boolean = false;

  private subscription: Subscription;
  
  constructor(private personService: PersonService, 
    private departmentService: DepartmentService,
    private authService: AuthenticationService,
    private applicationSettings: ApplicationSettings) { }

  ngOnInit() : void { 
    this.departmentService.getAllDepartments();

    this.subscription = this.departmentService.departmentsResponse$.subscribe(x => {
      this.departmentResponse = x;
    });

    this.getAllUsers();
    this.userId = this.authService.user.id;
    this.userRole = this.authService.user.role;
    this.photoBaseAddress = this.applicationSettings.userPhotoBaseAddress;

    this.isUserAuthorized();
  }

  ngOnDestroy() : void {
    this.subscription.unsubscribe();
  }

  isUserAuthorized() : boolean {
    this.isAuthorized = this.userRole == Roles.admin.toString();
    return this.isAuthorized;
  }

  getUsersByIdDepartment(idDepartment: number) {
    this.personService.getUsersByIdDepartment(idDepartment);

    this.subscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }

  getAllUsers() {
      this.personService.getAllUsers();

      this.subscription = this.personService.usersResponse$.subscribe(x => {
          this.userResponse = x;
      });
  }

  openModal() {
     this.showModalWindow = true;
     console.log(this.showModalWindow);
  }

  addNewUser() {
    this.openModal();

    var button = document.getElementById("kurwa");
    button.click();
  }

}
