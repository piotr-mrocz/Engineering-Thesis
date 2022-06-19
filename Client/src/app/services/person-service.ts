import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { BackendSettings } from '../models/consts/backendSettings';
import { UserDetailsDto } from '../models/dto/userDetailsDto';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BackendResponse } from '../models/response/backendResponse';

@Injectable({
  providedIn: 'root'
})
export class PersonService {

  private token;

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private authService: AuthenticationService,
    private endpoints: EndpointsUrl) {
    this.token = this.authService.getToken();
   }

   usersResponse$ = new BehaviorSubject<BackendResponse<UserDetailsDto[]>>({});

   private getAllUsersResponse() : Observable<BackendResponse<UserDetailsDto[]>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getAllUsersEndpoint;
      return this.http.post<BackendResponse<UserDetailsDto[]>>(url, {});
   }

   private getUsersByIdDepartmentResponse(idDepartment: number) : Observable<BackendResponse<UserDetailsDto[]>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getAllUsersByIdDepartmentEndpoint;
      return this.http.post<BackendResponse<UserDetailsDto[]>>(url, {IdDepartment: idDepartment});
   }

  getAllUsers() {
    this.getAllUsersResponse().subscribe(x => {
      this.usersResponse$.next(x);
    });
  }

  getUsersByIdDepartment(idDepartment: number) {
    this.getUsersByIdDepartmentResponse(idDepartment).subscribe(x => {
      this.usersResponse$.next(x);
    });
  }
}
 