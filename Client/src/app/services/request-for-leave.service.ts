import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { RequestForLeaveToAddDto } from '../models/dto/requestForLeaveToAddDto';
import { BackendResponse } from '../models/response/backendResponse';
import { GetAllUserRequestsForLeaveDto } from '../models/dto/getAllUserRequestsForLeaveDto';
import { UserVacationInfoDto } from '../models/dto/userVacationInfoDto';

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
  getAllUserRequestForLeaveResponse$ = new BehaviorSubject<BackendResponse<GetAllUserRequestsForLeaveDto[]>>({});
  getInformationAboutUserVacationDaysResponse$ = new BehaviorSubject<BackendResponse<UserVacationInfoDto>>({});

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

  private getAllUserRequestsForLeaveResponse(idUser: number, year: number) : Observable<BackendResponse<GetAllUserRequestsForLeaveDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getUserRequestsForLeaveEndpoint;
    return this.http.post<BackendResponse<GetAllUserRequestsForLeaveDto[]>>(url, {IdUser: idUser, Year: year});
  }

  private getInformationAboutUserVacationDaysResponse(idUser: number) : Observable<BackendResponse<UserVacationInfoDto>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getInformationAboutUserVacationDaysEndpoint;
    return this.http.post<BackendResponse<UserVacationInfoDto>>(url, {IdUser: idUser});
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

  getAllUserRequestsForLeave(idUser: number, year: number) {
    this.getAllUserRequestsForLeaveResponse(idUser, year).subscribe(x => {
      this.getAllUserRequestForLeaveResponse$.next(x);
    });
  }

  getInformationAboutUserVacationDays(idUser: number) {
    this.getInformationAboutUserVacationDaysResponse(idUser).subscribe(x => {
      this.getInformationAboutUserVacationDaysResponse$.next(x);
    });
  }
}
