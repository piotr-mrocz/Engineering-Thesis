import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HTTP_INTERCEPTORS, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { AuthenticationService } from './authentication.service';
import { Router } from '@angular/router';
import { catchError } from 'rxjs/operators';

export const delayMs = 2000;

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private inject: Injector, private router: Router) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authService = this.inject.get(AuthenticationService);
 
    var token = authService.getToken();

        req = req.clone({
        headers: req.headers.set('Authorization', `Bearer ${token}`)
      });

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';

        if (error.error instanceof ErrorEvent) {

          // client-side error
          errorMessage = `Error: ${error.error.message}`;

        } else {
          // server-side error
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}\nError: ${error.error.message} `;
        }

        alert(error.error.Message);

        return throwError(errorMessage);
      }));
  }
}