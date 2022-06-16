import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { BackendSettings } from '../models/consts/backendSettings';
import { UserDetailsDto } from '../models/userDetailsDto';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private token;

  constructor(private http: HttpClient, private backendSettings: BackendSettings, private authService: AuthenticationService) {
    this.token = this.authService.getToken();
   }

  getAllPersons(): Observable<UserDetailsDto[]>{
    var url = this.backendSettings.baseAddress + "api/User/GetAllUsers";
    var response = this.http.post<UserDetailsDto[]>(url, {});

    return response;
  }
}
 