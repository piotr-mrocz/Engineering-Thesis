import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';

export const delayMs = 2000;

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private inject: Injector, private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    console.log("sgsgus");
    let authService = this.inject.get(AuthenticationService);
 
    var token = authService.getToken();

    if (token) {
      console.log("Nieprawidłowy login lub hasło!");
      return null;
    }
    else {
        req = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });
    
    return next.handle(req);
    }
  }
}