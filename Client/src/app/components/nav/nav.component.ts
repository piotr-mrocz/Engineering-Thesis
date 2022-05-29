import { Component } from '@angular/core';
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
  }

  logOut() {
    this.authService.logOut();
  }
}
