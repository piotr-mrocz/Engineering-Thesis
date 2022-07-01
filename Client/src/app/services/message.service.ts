import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BackendResponse } from '../models/response/backendResponse';
import { UserConversationDto } from '../models/dto/userConversationDto';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { MessageDto } from '../models/dto/messageDto';

@Injectable({
  providedIn: 'root'
})
export class MessageService {

  private token: string;
  private idUser: number;

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private authService: AuthenticationService,
    private endpoints: EndpointsUrl) {
    this.token = this.authService.getToken();
    this.idUser = this.authService.user.id;
  }

   messagesResponse$ = new BehaviorSubject<BackendResponse<UserConversationDto[]>>({});
   addMessageResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getConverationResponse(idAddressee: number) : Observable<BackendResponse<UserConversationDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllUserConversationEndpoint;
    return this.http.post<BackendResponse<UserConversationDto[]>>(url, { IdSender: this.idUser, IdAddressee: idAddressee });
  }

  private addNewMessageResponse(messageToAdd: MessageDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.addNewMessageEndpoint;
    return this.http.post<BaseBackendResponse>(url, messageToAdd);
  }

 getUserConversation(idAddressee: number) {
    this.getConverationResponse(idAddressee).subscribe(x => {
      this.messagesResponse$.next(x);
    });
  }

  addNewMessage(messageToAdd: MessageDto) {
    this.addNewMessageResponse(messageToAdd).subscribe(x => {
      this.addMessageResponse$.next(x);
    });
  }
}
