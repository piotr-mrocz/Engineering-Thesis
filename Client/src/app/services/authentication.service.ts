import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings'
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { LoginDto } from '../models/dto/loginDto';
import { AuthenticationResponse } from '../models/response/authenticationResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private http: HttpClient, private router: Router) { }

  private apiSettings = new BackendSettings();
  invalidLogin: boolean;

  login(loginDto: LoginDto) {
    this.http.post(this.apiSettings.baseAddress + 'api/Account/Login', loginDto)
    .subscribe(response => {
      const responseApi = (<AuthenticationResponse>response);
        if (responseApi.isAuthorize) {
           localStorage.setItem("jwt", responseApi.token);

            localStorage.setItem("role", responseApi.role);
            localStorage.setItem("userName", responseApi.userName);
      
            this.invalidLogin = false;
            this.router.navigate(["/home"]);

           // this.haveAccess();
        }
        else {
            alert("Niepoprawny login lub hasło!");
        }
    });
  }
  

  isUserAuthenticated(): boolean {
   return localStorage.getItem("jwt") != null;
  }

  logOut() {
    localStorage.removeItem("jwt");
    localStorage.removeItem("role");
    localStorage.removeItem("userName");
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
