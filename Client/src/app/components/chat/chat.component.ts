import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonService } from 'src/app/services/person-service';
import { Subscription } from 'rxjs';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { ApplicationSettings } from 'src/app/models/consts/applicationSettings';
import { MessageService } from 'src/app/services/message.service';
import * as signalR from '@aspnet/signalr';
import { BackendSettings } from 'src/app/models/consts/backendSettings';
import { UserConversationDto } from 'src/app/models/dto/userConversationDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { MessageDto } from 'src/app/models/dto/messageDto';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {

  userResponse: BackendResponse<UserDetailsDto[]>;
  messageResponse: BackendResponse<UserConversationDto[]>;
  basePhotoAddress: string;
  baseLogoAddress: string;
  showMessages: boolean = false;
  showTextArea: boolean = false;
  userId: number;
  message: string;

  idAddressee: number = null;
  private userSubscription: Subscription;
  private messageSubscription: Subscription;
  private connection: signalR.HubConnection;

  constructor(private personService: PersonService,
    private applicationSettings: ApplicationSettings,
    private messageService: MessageService,
    private backendSettings: BackendSettings,
    private authService: AuthenticationService) {
    this.basePhotoAddress = this.applicationSettings.userPhotoBaseAddress;
    this.baseLogoAddress = this.applicationSettings.logoBaseAddress;
    this.userId = this.authService.user.id;
  }

  ngOnInit() {
    this.getAllUsers();
    this.startConnection();
    this.message = "";

    if (this.idAddressee != null) {
      this.connection.on("NewMessage", () => {
        this.getUserConversation(this.idAddressee);
      });

      console.log(this.idAddressee); // because (I really don't know why) signalR doesn't work without this
    }

    var element = document.getElementById("messagesList");
    element.scrollTop = element.scrollHeight;
  }

  ngOnDestroy() {
    if (this.userSubscription != undefined) {
      this.userSubscription.unsubscribe();
    }
    
    if (this.messageSubscription != undefined) {
      this.messageSubscription.unsubscribe();
    }
    
    this.connection.stop();
  }

  getAllUsers() {
    this.personService.getAllUsers();

    this.userSubscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }

  getUserConversation(idAddressee: number) {
    this.idAddressee = idAddressee;

    if (idAddressee != null) {
      this.messageService.getUserConversation(this.idAddressee);

      this.messageSubscription = this.messageService.messagesResponse$.subscribe(x => {
        this.messageResponse = x;
      });

      this.showMessages = this.messageResponse.succeeded;
      this.showTextArea = true;
    }
  }

  sendMessage() {
    if (this.verificateMessage(this.message)) {
      var messageToAdd = new MessageDto();
      messageToAdd.content = this.message;
      messageToAdd.idSender = this.userId;
      messageToAdd.idAddressee = this.idAddressee;

      this.addNewMessageAfterValidation(messageToAdd);
    }
  }

  private verificateMessage(message: string): boolean {
    if (message && message.trim().length > 0) {
      return true;
    }

    alert("Nie można wysłać pustej wiadomości!");
    return false;
  }

  private addNewMessageAfterValidation(newMessage: MessageDto) {
    this.messageService.addNewMessage(newMessage);

    this.messageService.messagesResponse$.subscribe(x => {
        if (!x.succeeded) {
          alert(x.message);
        }
    });

    this.ngOnInit();
  }

  public startConnection = () => {
    this.connection = new signalR.HubConnectionBuilder()
      .withUrl(this.backendSettings.baseAddress + "conversation", {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      })
      .configureLogging(signalR.LogLevel.Information)
      .build();

    this.connection.start()
      .then(() => console.log("Connection started"))
      .catch(err => console.log("Error while starting connection: " + err))
  }
}
