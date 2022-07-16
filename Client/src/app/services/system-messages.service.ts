import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BackendResponse } from '../models/response/backendResponse';
import { BehaviorSubject, Observable } from 'rxjs';
import { SystemMessageDto } from '../models/dto/systemMessageDto';

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

  private getUserSystemMessagesResponse() : Observable<BackendResponse<SystemMessageDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllSystemMessageEndpoint;
    return this.http.post<BackendResponse<SystemMessageDto[]>>(url, { IdUser: this.idUser });
  }

  getUserSystemMessages() {
    this.getUserSystemMessagesResponse().subscribe(x => {
      this.messagesSystemResponse$.next(x);
    });
  }
}
