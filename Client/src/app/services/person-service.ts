import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject } from 'rxjs';
import { BackendSettings } from '../models/consts/backendSettings';
import { UserDetailsDto } from '../models/dto/userDetailsDto';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BackendResponse } from '../models/response/backendResponse';
import { PositionsAndDepartmentsAndRoleDto } from '../models/dto/positionsAndDepartmentsAndRolesDto';
import { AddNewUserDto } from '../models/dto/addNewUserDto';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { UserDto } from '../models/dto/userDto';

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
   usersDepartmentResponse$ = new BehaviorSubject<BackendResponse<UserDto[]>>({});
   selectsResponse$ = new BehaviorSubject<BackendResponse<PositionsAndDepartmentsAndRoleDto>>({});
   addUserResponse$ = new BehaviorSubject<BaseBackendResponse>({});
   releaseUserResponse$ = new BehaviorSubject<BaseBackendResponse>({});

   private getAllUsersResponse() : Observable<BackendResponse<UserDetailsDto[]>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getAllUsersEndpoint;
      return this.http.post<BackendResponse<UserDetailsDto[]>>(url, {});
   }

   private getUsersByIdDepartmentResponse(idDepartment: number) : Observable<BackendResponse<UserDetailsDto[]>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getAllUsersByIdDepartmentEndpoint;
      return this.http.post<BackendResponse<UserDetailsDto[]>>(url, {IdDepartment: idDepartment});
   }

   private getUsersInDepartmentByIdSupervisorResponse(idSupervisor: number) : Observable<BackendResponse<UserDto[]>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getAllUserInDepartmentByIdSupervisorEndpoint;
      return this.http.post<BackendResponse<UserDto[]>>(url, {IdSupervisor: idSupervisor});
   }

   private getAllValuesForSelectsResponse() : Observable<BackendResponse<PositionsAndDepartmentsAndRoleDto>> {
      var url = this.backendSettings.baseAddress + this.endpoints.getUsersPositionsAndDepartmentsAndRolesEndpoint;
      return this.http.post<BackendResponse<PositionsAndDepartmentsAndRoleDto>>(url, {});
   }

   private addNewUserResponse(data: AddNewUserDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.addNewUserEndpoint;
    return this.http.post<BaseBackendResponse>(url, {UserInfo: data});
   }

   private releaseUserResponse(idUser: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.releaseUserEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdUser: idUser});
   }

  getAllUsers() {
    this.getAllUsersResponse().subscribe(x => {
      this.usersResponse$.next(x);
    });
  }

  getUsersInDepartmentByIdSupervisor(idSupervisor: number) {
    this.getUsersInDepartmentByIdSupervisorResponse(idSupervisor).subscribe(x => {
      this.usersDepartmentResponse$.next(x);
    });
  }

  getUsersByIdDepartment(idDepartment: number) {
    this.getUsersByIdDepartmentResponse(idDepartment).subscribe(x => {
      this.usersResponse$.next(x);
    });
  }

  getAllValuesForSelects() {
    this.getAllValuesForSelectsResponse().subscribe(x => {
        this.selectsResponse$.next(x);
    });
  }

  addNewUser(data: AddNewUserDto) {
    this.addNewUserResponse(data).subscribe(x => {
      this.addUserResponse$.next(x);
    });
  }

  releaseUser(idUser: number) {
    this.releaseUserResponse(idUser).subscribe(x => {
      this.releaseUserResponse$.next(x);
    });
  }
}
 