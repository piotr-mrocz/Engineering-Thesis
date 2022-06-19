import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { PersonService } from '../../services/person-service';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { DepartmentService } from 'src/app/services/department.service';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { DepartmentDto } from 'src/app/models/dto/departmentDto';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit, OnDestroy  {

  departmentResponse: BackendResponse<DepartmentDto[]>;
  userResponse: BackendResponse<UserDetailsDto[]>;
  userId: number;
  
  private departmentSubscription: Subscription;
 // private userSubscription: Subscription;
  
  constructor(private personService: PersonService, 
    private departmentService: DepartmentService,
    private authService: AuthenticationService) { }

  ngOnInit() : void { 
    this.departmentService.getAllDepartments();

    this.departmentSubscription = this.departmentService.departmentsResponse$.subscribe(x => {
      this.departmentResponse = x;
    });

    this.getAllUsers();
    this.userId = this.authService.user.id;
  }

  ngOnDestroy() : void {
    this.departmentSubscription.unsubscribe();
    //this.userSubscription.unsubscribe();
  }

  getUsersByIdDepartment(idDepartment: number) {
    this.personService.getUsersByIdDepartment(idDepartment);

    this.departmentSubscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }

  getAllUsers() {
      this.personService.getAllUsers();

      this.departmentSubscription = this.personService.usersResponse$.subscribe(x => {
          this.userResponse = x;
      });
  }
}
