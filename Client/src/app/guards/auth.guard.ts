import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

constructor(private service: AuthenticationService, private router: Router) {}

  canActivate() {
    if (this.service.isUserAuthenticated()) {
      return true;
    }
    else {
      this.service.logOut();
      return false;
    }
  }
  
}
