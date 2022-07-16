import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SystemMessagesService } from 'src/app/services/system-messages.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { CountUnReadSystemMessages } from 'src/app/models/dto/countUnReadSystemMessages';
import * as signalR from '@aspnet/signalr';
import { BackendSettings } from 'src/app/models/consts/backendSettings';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent implements OnInit {

  public userRole: string;
  public userName: string;
  public userPhotoSource: string;

  private messagesSystemSubscription: Subscription;
  messagesSystemCountResponse: BackendResponse<CountUnReadSystemMessages>;

  private connection: signalR.HubConnection;

  constructor(private authService: AuthenticationService,
    private systemMessagesService: SystemMessagesService,
    private backendSettings: BackendSettings) {
    this.userRole = this.authService.user.role;
    this.userName = this.authService.user.userName;

    this.userPhotoSource = this.getUserPhotoSource(this.authService.user.photoName);
  }

  ngOnInit(): void {
    this.startConnection();
    this.getSystemMessagesCount();

    this.connection.on("NewSystemMessage", () => {
      this.getSystemMessagesCount();
    });
  }
  
  logOut() {
    this.authService.logOut();
    this.messagesSystemSubscription.unsubscribe();
  }

  public startConnection = () => {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.backendSettings.baseAddress + "systemMessages", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .build();

    this.connection.start()
      .then(() => console.log("Connection started"))
      .catch(err => console.log("Error while starting connection: " + err))
  }

  getUserPhotoSource(photoName: string): string {
    return "../../../assets/Images/People/" + photoName;
  }

  getSystemMessagesCount() {
    this.systemMessagesService.getCountMessagesSystem();
    this.messagesSystemSubscription = this.systemMessagesService.getCountMessagesSystemResponse$.subscribe(x => {
      this.messagesSystemCountResponse = x;
    });
  }
}
