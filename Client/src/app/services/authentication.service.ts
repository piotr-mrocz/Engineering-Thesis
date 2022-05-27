import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings'
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginDto } from '../models/dto/loginDto';
import { AuthenticationResponse } from '../models/response/authenticationResponse';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiSettings = new BackendSettings();
  
  private _isLoggedIn$ = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this._isLoggedIn$.asObservable();

  constructor(private http: HttpClient) { 
    const token = localStorage.getItem('jwt');
    this._isLoggedIn$.next(!!token);
  }

  login(loginDto: LoginDto) {
    this.http.post(this.apiSettings.baseAddress + 'api/Account/Login', loginDto)
    .subscribe(response => {
      const responseApi = (<AuthenticationResponse>response);
        if (responseApi.isAuthorize) {
           localStorage.setItem("jwt", responseApi.token);
           this._isLoggedIn$.next(true);
           // this.haveAccess();
        }
        else {
            alert("Niepoprawny login lub hasło!");
        }
    });

    return this.isLoggedIn$;
  }
  

  isUserAuthenticated(): boolean {
   return localStorage.getItem("jwt") != null;
  }

  logOut() {
    localStorage.removeItem("jwt");
  }

  getToken() {
    // if jwt is empty I will return null
    return localStorage.getItem("jwt") || '';
  }

  haveAccess() {
    var loginToken = this.getToken();

    if (loginToken == null || loginToken.length == 0) {
      var extractedToken = loginToken.split('.')[1];
      var atobToken = atob(extractedToken);
      var finalData = JSON.parse(atobToken);

      console.log(finalData);
    }
  }
}
