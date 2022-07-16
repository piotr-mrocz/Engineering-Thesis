import { Component, OnInit } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { SystemMessagesService } from 'src/app/services/system-messages.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { CountUnReadSystemMessages } from 'src/app/models/dto/countUnReadSystemMessages';

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

  constructor(private authService: AuthenticationService,
    private systemMessagesService: SystemMessagesService) {
    this.userRole = this.authService.user.role;
    this.userName = this.authService.user.userName;

    this.userPhotoSource = this.getUserPhotoSource(this.authService.user.photoName);
  }
  ngOnInit(): void {
    this.getSystemMessagesCount();
  }
  
  logOut() {
    this.authService.logOut();
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
