import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BackendResponse } from '../models/response/backendResponse';
import { BehaviorSubject, Observable } from 'rxjs';
import { SystemMessageDto } from '../models/dto/systemMessageDto';
import { CountUnReadSystemMessages } from '../models/dto/countUnReadSystemMessages';

@Injectable({
  providedIn: 'root'
})
export class SystemMessagesService {

  private idUser: number;

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private authService: AuthenticationService,
    private endpoints: EndpointsUrl) {
    this.idUser = this.authService.user.id;
  }

  messagesSystemResponse$ = new BehaviorSubject<BackendResponse<SystemMessageDto[]>>({});
  getCountMessagesSystemResponse$ = new BehaviorSubject<BackendResponse<CountUnReadSystemMessages>>({});

  private getUserSystemMessagesResponse() : Observable<BackendResponse<SystemMessageDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllSystemMessageEndpoint;
    return this.http.post<BackendResponse<SystemMessageDto[]>>(url, { IdUser: this.idUser });
  }

  private getCountMessagesSystemResponse() : Observable<BackendResponse<CountUnReadSystemMessages>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getCountOnlyUnreadSystemMessagesEndpoint;
    return this.http.post<BackendResponse<CountUnReadSystemMessages>>(url, { IdUser: this.idUser });
  }

  getUserSystemMessages() {
    this.getUserSystemMessagesResponse().subscribe(x => {
      this.messagesSystemResponse$.next(x);
    });
  }

  getCountMessagesSystem() {
    this.getCountMessagesSystemResponse().subscribe(x => {
      this.getCountMessagesSystemResponse$.next(x);
    });
  }
}
