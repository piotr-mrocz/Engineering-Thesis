import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Person } from '../models/person';
import { Observable } from 'rxjs';
import { tap, map, filter } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private url = 'http://localhost:5000/api/person';

  constructor(private http: HttpClient) { }


  getPersons(species: string): Observable<Person[]>{
    return this.http.get<Person[]>(this.url);
  }

  getPerson(id: string): Observable<Person> {
    return this.http.get<Person>(this.url + '/' + id);
  }
}
