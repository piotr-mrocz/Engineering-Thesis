import { Component } from '@angular/core';
import { AuthenticationService } from 'src/app/services/authentication.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.css']
})
export class NavComponent {

  public userRole: string;

  constructor(private authService: AuthenticationService) {
    this.userRole = this.authService.user.role;
  }

  logOut() {
    this.authService.logOut();
  }
  
}
