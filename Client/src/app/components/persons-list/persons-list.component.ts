import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { PersonService } from '../../services/person-service';
import { UserDetailsDto } from 'src/app/models/userDetailsDto';
import { DepartmentService } from 'src/app/services/department.service';

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit {

  persons: Observable<UserDetailsDto[]>;
  
  constructor(private personService: PersonService, private departmentService: DepartmentService) { }

  ngOnInit() { 
    //this.persons = this.personService.getAllPersons();
    var test = this.departmentService.getAllDepartments();
  }
}
