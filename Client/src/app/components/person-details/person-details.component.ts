import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Person } from 'src/app/models/person';
import { ActivatedRoute, ParamMap } from '@angular/router';
import { switchMap, tap } from 'rxjs/operators';
import { Location } from '@angular/common';
import { PersonService } from '../../services/person-service';

@Component({
  selector: 'app-person-details',
  templateUrl: './person-details.component.html',
  styleUrls: ['./person-details.component.css']
})
export class PersonDetailsComponent implements OnInit {

  personDetails: Observable<Person>;

  constructor(private http: PersonService, private route: ActivatedRoute, private location: Location) { }

  ngOnInit() {
    this.personDetails = this.route.paramMap.pipe(
      switchMap((params: ParamMap) => this.http.getPerson(params.get('id')))
    );
  }
  
  comeBack() {
    this.location.back();
  }
 
}
