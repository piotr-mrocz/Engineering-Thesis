import { Injectable } from '@angular/core';
import { BackendSettings } from '../models/consts/backendSettings'
import { HttpClient } from '@angular/common/http';
import { LoginDto } from '../models/dto/loginDto';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationService {

  constructor(private apiSetting: BackendSettings, private http: HttpClient) { }

  private apiSettings = new BackendSettings();
  
  login(loginDto: LoginDto) {
      return this.http.post(this.apiSettings.baseAddress + '/api/Account/Login', loginDto);
  };

  public loginUser = (loginDto: LoginDto) => {
      return this.http.post(this.apiSettings.baseAddress + '/api/Account/Login', loginDto);
  };
}
