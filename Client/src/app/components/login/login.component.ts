import { Component, OnInit } from '@angular/core';
import { Types } from 'src/app/models/enums/types.enum';
import { LoginDto } from 'src/app/models/dto/loginDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {

  showLoginErrorMessage: boolean = false;
  showPasswordErrorMessage: boolean = false;
  errorMessage: string;

  hiddenPassword: boolean = true;
  passwordType: string = Types[Types.password];

  constructor(private authService: AuthenticationService, private router: Router) {
    var isLoggin = this.authService.isUserAuthenticated();

      if (isLoggin) {
        this.router.navigate(['/home']);
      }
   }

  showPassword() {
    this.hiddenPassword = false;
    this.passwordType = Types[Types.text];
  }

  hidePassword() {
    this.hiddenPassword = true;
    this.passwordType = Types[Types.password];
  }

  logIn(login: string, password: string) {
    if (this.validateInputs(login, password)) {
      var dto = new LoginDto(login, password);
      this.authService.login(dto);
      
      var isLoggin = this.authService.isUserAuthenticated();

console.log(isLoggin);

      if (isLoggin) {
        this.router.navigate(['/home']);
      }
    }
  }

  validateInputs(login: string, password: string) {
    if (login.trim().length == 0 || password.trim().length == 0) {
      this.showLoginErrorMessage = true;
      this.showPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    if (login.trim().length == 0) {
      this.showLoginErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    if (password.trim().length == 0) {
      this.showPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    this.showLoginErrorMessage = false;
    this.showPasswordErrorMessage = false;
    this.errorMessage = "";

    return true;
  }

  resetLoginError(event: KeyboardEvent) {
    if (event.keyCode != 32) { // space
      this.showLoginErrorMessage = false;
    }
  }

  resetPasswordError(event: KeyboardEvent) {
    if (event.keyCode != 32) { // space
      this.showPasswordErrorMessage = false;
    }
  }
}
