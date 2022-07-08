import { Component, OnInit } from '@angular/core';
import { Types } from 'src/app/models/enums/types.enum';
import { ChangeUserPasswordDto } from 'src/app/models/dto/changeUserPasswordDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { PersonService } from 'src/app/services/person-service';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {

  showOldPasswordErrorMessage: boolean = false;
  showConfirmPasswordErrorMessage: boolean = false;
  showNewPasswordErrorMessage: boolean = false;
  errorMessage: string;

  hiddenOldPassword: boolean = true;
  hiddenNewPassword: boolean = true;
  hiddenConfirmPassword: boolean = true;

  typeOldPassword: string = Types[Types.password];
  typeNewPassword: string = Types[Types.password];
  typeConfirmPassword: string = Types[Types.password];
  userId: number;

  private subscription: Subscription;

  constructor(private authService: AuthenticationService,
    private personService: PersonService,
    private router: Router) {
    this.userId = this.authService.user.id;
   }

  ngOnInit() {
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  changePassword(oldPassword: string, newPassword: string, confirmPassword: string)
  {
    var changeUserPasswordModel = new ChangeUserPasswordDto();

    changeUserPasswordModel.idUser = this.userId;
    changeUserPasswordModel.oldPassword = oldPassword;
    changeUserPasswordModel.newPassword = newPassword;
    changeUserPasswordModel.confirmNewPassword = confirmPassword;

    if (this.validateModel(changeUserPasswordModel)) {
      this.changePasswordAfterValidation(changeUserPasswordModel);
    }
  }

  validateModel(changeUserPasswordModel: ChangeUserPasswordDto) : boolean {
    if (changeUserPasswordModel.oldPassword == changeUserPasswordModel.newPassword) {
      alert("Nowe hasło nie może być takie same jak stare!");
      return false;
    }

    if (changeUserPasswordModel.newPassword != changeUserPasswordModel.confirmNewPassword) {
      alert("Nie udało się potwierdzić hasła!");
      return false;
    }

    return true;
  }

  changePasswordAfterValidation(changeUserPasswordModel: ChangeUserPasswordDto) {
    this.personService.changeUserPassword(changeUserPasswordModel);
    this.subscription = this.personService.changeUserPasswordResponse$.subscribe(x => {
      alert(x.message);

      if(x.succeeded) {
        this.router.navigate(['/home']);
      }
    });
  }

  showOldPassword() {
    this.hiddenOldPassword = false;
    this.typeOldPassword = Types[Types.text];
  }

  hideOldPassword() {
    this.hiddenOldPassword = true;
    this.typeOldPassword = Types[Types.password];
  }

  showNewPassword() {
    this.hiddenNewPassword = false;
    this.typeNewPassword = Types[Types.text];
  }

  hideNewPassword() {
    this.hiddenNewPassword = true;
    this.typeNewPassword = Types[Types.password];
  }

  showConfirmPassword() {
    this.hiddenConfirmPassword = false;
    this.typeConfirmPassword = Types[Types.text];
  }

  hideConfirmPassword() {
    this.hiddenConfirmPassword = true;
    this.typeConfirmPassword = Types[Types.password];
  }

  validateInputs(oldPassword: string, newPassword: string, confirmNewPassword: string) {
    if (oldPassword.trim().length == 0 || newPassword.trim().length == 0 || confirmNewPassword.trim().length == 0) {
      this.showOldPasswordErrorMessage = true;
      this.showConfirmPasswordErrorMessage = true;
      this.showNewPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    if (oldPassword.trim().length == 0) {
      this.showOldPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    if (newPassword.trim().length == 0) {
      this.showNewPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    if (confirmNewPassword.trim().length == 0) {
      this.showConfirmPasswordErrorMessage = true;
      this.errorMessage = "Pole wymagane!";

      return false;
    }

    this.showOldPasswordErrorMessage = false;
    this.showConfirmPasswordErrorMessage = false;
    this.showNewPasswordErrorMessage = false;
    this.errorMessage = "";

    return true;
  }

  resetOldPasswordError(event: KeyboardEvent) {
    if (event.keyCode != 32) { // space
      this.showOldPasswordErrorMessage = false;
    }
  }

  resetNewPasswordError(event: KeyboardEvent) {
    if (event.keyCode != 32) { // space
      this.showNewPasswordErrorMessage = false;
    }
  }

  resetConfirmPasswordError(event: KeyboardEvent) {
    if (event.keyCode != 32) { // space
      this.showConfirmPasswordErrorMessage = false;
    }
  }
}
