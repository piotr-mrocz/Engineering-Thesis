import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { BackendResponse } from '../models/response/backendResponse';
import { BehaviorSubject, Observable } from 'rxjs';
import { DepartmentDto } from '../models/dto/departmentDto';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private token;

  constructor(private backendSettings: BackendSettings, 
    private urlEndpoints: EndpointsUrl, 
    private http: HttpClient,
    private authService: AuthenticationService) {
    this.token = this.authService.getToken();
   }

   departmentsResponse$ = new BehaviorSubject<BackendResponse<DepartmentDto[]>>({});

   private getAllDepartmentsResponse() : Observable<BackendResponse<DepartmentDto[]>> {
    var url = this.backendSettings.baseAddress + this.urlEndpoints.getAllDepartmentsEndpoint;
    return this.http.post<BackendResponse<DepartmentDto[]>>(url, {});
   }

   getAllDepartments() {
      this.getAllDepartmentsResponse().subscribe(x => {
      this.departmentsResponse$.next(x);
     });
   }

}
