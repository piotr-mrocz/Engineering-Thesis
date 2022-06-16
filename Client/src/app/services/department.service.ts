import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { HttpClient } from '@angular/common/http';
import { AuthenticationService } from './authentication.service';
import { BackendResponse } from '../models/response/backendResponse';

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

   getAllDepartments() {
     var url = this.backendSettings.baseAddress + this.urlEndpoints.getAllDepartmentsEndpoint;
     var response = this.http.post<BackendResponse>(url, {});

     return response;
   }

}
