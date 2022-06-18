import { Component, OnInit, OnDestroy } from '@angular/core';
import { Observable, Subscription } from 'rxjs';
import { PersonService } from '../../services/person-service';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { DepartmentService } from 'src/app/services/department.service';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { DepartmentDto } from 'src/app/models/dto/departmentDto';

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit, OnDestroy  {

  showUsers: boolean;

  persons: Observable<UserDetailsDto[]>;
  departmentResponse: BackendResponse<DepartmentDto[]>;
  
  private subscription: Subscription;
  
  constructor(private personService: PersonService, private departmentService: DepartmentService) { }

  ngOnInit() : void { 
    //this.persons = this.personService.getAllPersons();
    
    this.departmentService.getAllDepartments();

    this.subscription = this.departmentService.departmentsResponse$.subscribe(x => {
      this.departmentResponse = x;
    });
  }

  ngOnDestroy() : void {
    this.subscription.unsubscribe();
  }
}
