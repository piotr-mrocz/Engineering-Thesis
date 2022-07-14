import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BackendResponse } from '../models/response/backendResponse';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { PresenceToUpdateDto } from '../models/dto/presenceToUpdateDto';
import { PresenceToAddDto } from '../models/dto/presenceToAddDto';
import { GetPresenceByIdUserListDto } from '../models/dto/getPresenceByIdUserListDto';
import { UsersPresencesPerDayDto } from '../models/dto/usersPresencesPerDayDto';
import { PossibleAbsenceToChooseDto } from '../models/dto/possibleAbsenceToChooseDto';
import { PresenceToAddRangeDto } from '../models/dto/presenceToAddRangeDto';

@Injectable({
  providedIn: 'root'
})
export class PresenceService {

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private endpoints: EndpointsUrl) {
  }

  getAllPossibleAbsenceTypeToChoosePresenceResponse$ = new BehaviorSubject<BackendResponse<PossibleAbsenceToChooseDto[]>>({});
  getAllUserResponse$ = new BehaviorSubject<BackendResponse<GetPresenceByIdUserListDto>>({});
  getUsersPresencesPerDayResponse$ = new BehaviorSubject<BackendResponse<UsersPresencesPerDayDto>>({});
  createPresenceResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  createRangePresenceResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  updatePresenceResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getAllUserPresenceByIdUserResponse(idUser: number, month: number, year: number) : Observable<BackendResponse<GetPresenceByIdUserListDto>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getPresencesUsersPerMonthEndpoint;
    return this.http.post<BackendResponse<GetPresenceByIdUserListDto>>(url, {IdUser: idUser, MonthNumber: month, Year: year});
  }

  private getAllPossibleAbsenceTypeToChoosePresenceResponse() : Observable<BackendResponse<PossibleAbsenceToChooseDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllPossibleAbsenceTypeToChoosePresenceEndpoint;
    return this.http.post<BackendResponse<PossibleAbsenceToChooseDto[]>>(url, {});
  }

  private getUsersPresencesPerDayResponse(day: number, month: number, year: number) : Observable<BackendResponse<UsersPresencesPerDayDto>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getUsersPresencePerDayEndpoint;
    return this.http.post<BackendResponse<UsersPresencesPerDayDto>>(url, {Day: day, Month: month, Year: year});
  }

  private updatePresenceResponse(updatePresence: PresenceToUpdateDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.updatePresenceEndpoint;
    return this.http.post<BaseBackendResponse>(url, { PresenceToUpdate: updatePresence });
  }

  private createPresenceResponse(presenceToAdd: PresenceToAddDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.createPresenceEndpoint;
    return this.http.post<BaseBackendResponse>(url, { PresenceInfo: presenceToAdd });
  }

  private createRangePresenceResponse(presenceToAdd: PresenceToAddRangeDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.createRangePresenceEndpoint;
    return this.http.post<BaseBackendResponse>(url, { PresenceInfo: presenceToAdd });
  }

  getAllUserPresenceByIdUser(idUser: number, month: number, year: number) {
    this.getAllUserPresenceByIdUserResponse(idUser, month, year).subscribe(x => {
      this.getAllUserResponse$.next(x);
    });
  }

  getAllPossibleAbsenceTypeToChoosePresence() {
    this.getAllPossibleAbsenceTypeToChoosePresenceResponse().subscribe(x => {
      this.getAllPossibleAbsenceTypeToChoosePresenceResponse$.next(x);
    });
  }

  getUsersPresencesPerDay(day: number, month: number, year: number) {
    this.getUsersPresencesPerDayResponse(day, month, year).subscribe(x => {
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
  createRangePresence(presenceToAdd: PresenceToAddRangeDto) {
    this.createRangePresenceResponse(presenceToAdd).subscribe(x => {
      this.createRangePresenceResponse$.next(x);
    });
  }
}
