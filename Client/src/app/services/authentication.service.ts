import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings'
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../models/dto/loginDto';
import { AuthenticationResponse } from '../models/response/authenticationResponse';
import { ClaimsReponse } from '../models/response/claimsResponse';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {
  private apiSettings = new BackendSettings();
  
  user!: ClaimsReponse;
  public isLoggin: boolean;

  constructor(private http: HttpClient, private router: Router) { 
    const token = localStorage.getItem('jwt');
    this.user = this.getUser(token);
  }

  login(loginDto: LoginDto) {
     this.http.post(this.apiSettings.baseAddress + 'api/Account/Login', loginDto)
    .subscribe(response => {
      const responseApi = (<AuthenticationResponse>response);
        if (responseApi.isAuthorize) {
           localStorage.setItem("jwt", responseApi.token);

           this.user = this.getUser(responseApi.token);
           this.router.navigate(['/home']);
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

      return claims;
    }
    
    return null;
  }
}
