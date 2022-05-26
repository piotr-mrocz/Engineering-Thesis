import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private service: AuthenticationService, private router: Router) {
  }

  canActivate() {
     this.service.haveAccess();
     return true;
  }
  
}
