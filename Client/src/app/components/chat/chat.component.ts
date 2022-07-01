import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonService } from 'src/app/services/person-service';
import { Subscription } from 'rxjs';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { ApplicationSettings } from 'src/app/models/consts/applicationSettings';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css']
})
export class ChatComponent implements OnInit, OnDestroy {

  userResponse: BackendResponse<UserDetailsDto[]>;
  basePhotoAddress: string;

  private subscription: Subscription;

  constructor(private personService: PersonService,
    private applicationSettings: ApplicationSettings) {
      this.basePhotoAddress = applicationSettings.userPhotoBaseAddress;
  }

  ngOnInit() {
    this.getAllUsers();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  getAllUsers() {
    this.personService.getAllUsers();

    this.subscription = this.personService.usersResponse$.subscribe(x => {
      this.userResponse = x;
    });
  }
}
