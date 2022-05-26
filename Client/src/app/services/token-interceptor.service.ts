import { Injectable, Injector } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class TokenInterceptorService implements HttpInterceptor{

  constructor(private inject: Injector) { }
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    let authService = this.inject.get(AuthenticationService);
    console.log("first");
    let jwtToken = req.clone({
        setHeaders: {
          Authorization: 'bearer ' + authService.getToken()
        }
    });
console.log("jsdi");
    return next.handle(jwtToken);
  }
}
