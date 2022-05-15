import { Component, OnInit } from '@angular/core';
import { Types } from 'src/app/models/enums/types.enum';
import { LoginDto } from 'src/app/models/dto/loginDto';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  hiddenPassword: boolean = true;
  passwordType: string = Types[Types.password];

model: LoginDto;

  constructor() { }

  showPassword() {
    this.hiddenPassword = false;
    this.passwordType = Types[Types.text];
  }

  hidePassword() {
    this.hiddenPassword = true;
    this.passwordType = Types[Types.password];
  }

  logIn() {
      console.log(this.model);
  }
}
