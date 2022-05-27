import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot } from '@angular/router';
import { AuthenticationService } from '../services/authentication.service';

@Injectable({
  providedIn: 'root'
})
export class RoleGuard implements CanActivate {

  constructor(private service: AuthenticationService, private router: Router) {
  }

  canActivate(route: ActivatedRouteSnapshot) {
     const userRole = this.service.user.role;
     const isAuthorized = route.data.roles.includes(userRole);

     if (!isAuthorized) {
       window.alert("Nie masz dostępu do zasobu!"); // or redirect somewhere
     }

     return isAuthorized; // do usunięcia
  }
  
}
