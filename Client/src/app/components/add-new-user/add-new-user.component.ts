import { Component, OnInit, OnDestroy } from '@angular/core';
import { PersonService } from 'src/app/services/person-service';
import { PositionsAndDepartmentsAndRoleDto } from 'src/app/models/dto/positionsAndDepartmentsAndRolesDto';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { AddNewUserDto } from 'src/app/models/dto/addNewUserDto';
import { stringify } from 'querystring';

@Component({
  selector: 'app-add-new-user',
  templateUrl: './add-new-user.component.html',
  styleUrls: ['./add-new-user.component.css']
})
export class AddNewUserComponent implements OnInit, OnDestroy {
  selectFile: File = null;
  file: any;
  responseSelect: BackendResponse<PositionsAndDepartmentsAndRoleDto>;
  public imageUrl = "";
  public fileImage : any;

  private subscription: Subscription;
  photoName: any;

  constructor(private personService: PersonService) { }

  ngOnInit() {
    this.personService.getAllValuesForSelects();

    this.subscription = this.personService.selectsResponse$.subscribe(x => { 
        this.responseSelect = x;
    });
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  onFileChange(event : any) {
    this.fileImage = event.target.files[0];
    this.photoName = event.target.files[0].name;
    var reader = new FileReader();
    reader.onload = (event:any) => {
      this.imageUrl = event.target.result as string;   
    }
  
    reader.readAsDataURL(this.fileImage);
  }
  
  addNewUser(data) {
    var newUserModel = new AddNewUserDto();
    newUserModel.firstName = data.firstName;
    newUserModel.lastName = data.lastName;
    newUserModel.dateOfBirth = data.dateOfBirth;
    newUserModel.idDepartment = data.idDepartment;
    newUserModel.idRole = data.idRole;
    newUserModel.idPosition = data.idPosition;
    newUserModel.phone = data.phone;
    newUserModel.email = data.email;
    newUserModel.photoName = this.photoName;

    var validation = this.validateForm(newUserModel);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addNewUserAfterValidation(newUserModel);
  }

  validateForm(newUserModel: AddNewUserDto) : boolean {
    if (newUserModel.firstName && newUserModel.lastName && newUserModel.photoName && 
        newUserModel.idDepartment > 0  && newUserModel.idPosition > 0 && newUserModel.idRole > 0 &&
        newUserModel.dateOfBirth) {
          return true;
        }
    else {
      return false;
    }
  }

  private addNewUserAfterValidation(newUserModel: AddNewUserDto) {
    this.personService.addNewUser(newUserModel);
    this.personService.addUserResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        if (x.succeeded) {
          window.location.reload();
        }

        alert(x.message);
      }
    });
  }

  resetForm(idForm: string) {
    var form = <HTMLFormElement>document.getElementById(idForm);
    form.reset();
  }
}
