import { DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { Person } from 'src/app/models/person';
import { Observable } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';
import { PersonService } from '../../services/person-service';

@Component({
  selector: 'app-persons-list',
  templateUrl: './persons-list.component.html',
  styleUrls: ['./persons-list.component.css']
})
export class PersonsListComponent implements OnInit {

  persons: Observable<Person[]>;
  
  constructor(private http: PersonService) { }

  ngOnInit() { 
    
  }
}
