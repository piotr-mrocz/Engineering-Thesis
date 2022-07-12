import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BackendResponse } from '../models/response/backendResponse';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { GetPresenceByIdUserDto } from '../models/dto/getPresenceByIdUserDto';
import { PresenceToUpdateDto } from '../models/dto/presenceToUpdateDto';
import { PresenceToAddDto } from '../models/dto/presenceToAddDto';
import { UserPresentsPerDayDto } from '../models/dto/userPresentsPerDayDto';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private endpoints: EndpointsUrl) {
  }

  getAllUserResponse$ = new BehaviorSubject<BackendResponse<GetPresenceByIdUserDto[]>>({});
  getUsersPresencesPerDayResponse$ = new BehaviorSubject<BackendResponse<UserPresentsPerDayDto[]>>({});
  createPresenceResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  updatePresenceResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getAllUserPresenceByIdUserResponse(idUser: number, month: number, year: number) : Observable<BackendResponse<GetPresenceByIdUserDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getPresencesUsersPerMonthEndpoint;
    return this.http.post<BackendResponse<GetPresenceByIdUserDto[]>>(url, {IdUser: idUser, MonthNumber: month, Year: year});
  }

  private getUsersPresencesPerDayResponse(date: Date) : Observable<BackendResponse<UserPresentsPerDayDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getUsersPresencePerDayEndpoint;
    return this.http.post<BackendResponse<UserPresentsPerDayDto[]>>(url, {Date: date});
  }

  private updatePresenceResponse(updatePresence: PresenceToUpdateDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.updatePresenceEndpoint;
    return this.http.post<BaseBackendResponse>(url, { PresenceToUpdate: updatePresence });
  }

  private createPresenceResponse(presenceToAdd: PresenceToAddDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.createPresenceEndpoint;
    return this.http.post<BaseBackendResponse>(url, { PresenceInfo: presenceToAdd });
  }

  getAllUserPresenceByIdUser(idUser: number, month: number, year: number) {
    this.getAllUserPresenceByIdUserResponse(idUser, month, year).subscribe(x => {
      this.getAllUserResponse$.next(x);
    });
  }

  getUsersPresencesPerDay(date: Date) {
    this.getUsersPresencesPerDayResponse(date).subscribe(x => {
      this.getUsersPresencesPerDayResponse$.next(x);
    });
  }

  updatePresence(updatePresence: PresenceToUpdateDto) {
    this.updatePresenceResponse(updatePresence).subscribe(x => {
      this.updatePresenceResponse$.next(x);
    });
  }

  createPresence(presenceToAdd: PresenceToAddDto) {
    this.createPresenceResponse(presenceToAdd).subscribe(x => {
      this.createPresenceResponse$.next(x);
    });
  }
}
