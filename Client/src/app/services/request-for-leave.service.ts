import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { RequestForLeaveToAddDto } from '../models/dto/requestForLeaveToAddDto';

@Injectable({
  providedIn: 'root'
})
export class RequestForLeaveService {

  private token;

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private authService: AuthenticationService,
    private endpoints: EndpointsUrl) {
    this.token = this.authService.getToken();
  }

  createRequestForLeaveResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  acceptRequestForLeaveResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  rejectRequestForLeaveResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  removeRequestForLeaveByUserResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private createRequestForLeaveResponse(newRequestForLeave: RequestForLeaveToAddDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.createRequestForLeaveEndpoint;
    return this.http.post<BaseBackendResponse>(url, {RequestInfo: newRequestForLeave});
  }

  private acceptRequestForLeaveResponse(idRequest: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.acceptRequestForLeaveEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdRequest: idRequest});
  }

  private rejectRequestForLeaveResponse(idRequest: number, reason: string) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.rejectRequestForLeaveEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdRequest: idRequest, Reason: reason});
  }

  private removeRequestForLeaveByUserResponse(idRequest: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.removeRequestForLeaveEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdRequest: idRequest});
  }

  createRequestForLeave(newRequestForLeave: RequestForLeaveToAddDto) {
    this.createRequestForLeaveResponse(newRequestForLeave).subscribe(x => {
      this.createRequestForLeaveResponse$.next(x);
    });
  }

  acceptRequestForLeave(idRequest: number) {
    this.acceptRequestForLeaveResponse(idRequest).subscribe(x => {
      this.acceptRequestForLeaveResponse$.next(x);
    });
  }

  rejectRequestForLeave(idRequest: number, reason: string) {
    this.rejectRequestForLeaveResponse(idRequest, reason).subscribe(x => {
      this.rejectRequestForLeaveResponse$.next(x);
    });
  }

  removeRequestForLeaveByUser(idRequest: number) {
    this.removeRequestForLeaveByUserResponse(idRequest).subscribe(x => {
      this.removeRequestForLeaveByUserResponse$.next(x);
    });
  }
}
