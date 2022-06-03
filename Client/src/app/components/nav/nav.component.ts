import { Component, OnDestroy } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  public userRole: string;
  public userName: string;
  public userPhotoSource: string;

  constructor(private authService: AuthenticationService) {
    this.userRole = this.authService.user.role;
    this.userName = this.authService.user.userName;

    this.userPhotoSource = this.getUserPhotoSource(this.authService.user.photoName);
  }

  logOut() {
    this.authService.logOut();
  }

  getUserPhotoSource(photoName: string): string {
    return "../../../assets/Images/People/" + photoName;
  }
}
