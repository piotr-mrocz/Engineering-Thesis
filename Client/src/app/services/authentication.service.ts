import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings'
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginDto } from '../models/dto/loginDto';
import { AuthenticationResponse } from '../models/response/authenticationResponse';
import { BehaviorSubject } from 'rxjs';
import { ClaimsReponse } from '../models/response/claimsResponse';
import { Router } from '@angular/router';
import { EndpointsUrl } from '../models/consts/endpointsUrl';

import { JwtHelperService } from "@auth0/angular-jwt";

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiSettings = new BackendSettings();
  
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this._isLoggedIn$.asObservable();
  user!: ClaimsReponse;

  helper = new JwtHelperService();

  constructor(private http: HttpClient, private router: Router, private endpoints: EndpointsUrl) { 
    const token = localStorage.getItem('jwt');
    this._isLoggedIn$.next(!!token);
    this.user = this.getUser(token);
  }

  login(loginDto: LoginDto) {
    this.http.post(this.apiSettings.baseAddress + this.endpoints.loginEndpoint, loginDto)
    .subscribe(response => {
      const responseApi = (<AuthenticationResponse>response);
        if (responseApi.isAuthorize) {
           localStorage.setItem("jwt", responseApi.token);
           this._isLoggedIn$.next(true);

           this.user = this.getUser(responseApi.token);
           this.router.navigate(['/home']);
        }
        else {
            alert("Niepoprawny login lub hasło!");
        }
    });
  }

  isUserAuthenticated(): boolean {
    var token = localStorage.getItem("jwt");
    var isTokenExpired = this.isTokenExpired(token);
   return isTokenExpired && token != null;
  }

  logOut() {
    localStorage.removeItem("jwt");
    this._isLoggedIn$.next(false);
    this.router.navigate(['/login']);
  }

  getToken() {
    // if jwt is empty I will return null
    return localStorage.getItem("jwt") || '';
  }

  getUser(token: string) : ClaimsReponse {
    token = localStorage.getItem("jwt");

    if (token != null && token.length != 0) {
      var extractedToken = token.split('.')[1];
      var atobToken = atob(extractedToken);
      var finalData = JSON.parse(atobToken);

      var claims = new ClaimsReponse();
      claims.id = finalData[Object.keys(finalData)[0]];
      claims.userName = finalData[Object.keys(finalData)[1]];
      claims.role = finalData[Object.keys(finalData)[2]];
      claims.photoName = finalData[Object.keys(finalData)[3]];

      return claims;
    }
    
    return null;
  }

  isTokenExpired(token) : boolean {
    var isExpired = this.helper.isTokenExpired(token);
    return !isExpired;
  }
}
