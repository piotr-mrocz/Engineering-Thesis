import { Component, OnInit, Input, OnDestroy } from '@angular/core';
import { UserDetailsDto } from 'src/app/models/dto/userDetailsDto';
import { PositionsAndDepartmentsAndRoleDto } from 'src/app/models/dto/positionsAndDepartmentsAndRolesDto';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { Subscription } from 'rxjs';
import { PersonService } from 'src/app/services/person-service';
import { ApplicationSettings } from 'src/app/models/consts/applicationSettings';
import { UpdateUserDto } from 'src/app/models/dto/updateUserDto';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit, OnDestroy {

  @Input() person: UserDetailsDto;
  responseSelect: BackendResponse<PositionsAndDepartmentsAndRoleDto>;
  private subscription: Subscription;
  public imageUrl = "";
  public fileImage: any;
  photoName: any;

  constructor(private personService: PersonService,
    private appSettings: ApplicationSettings) { }

  ngOnInit() {
    this.personService.getAllValuesForSelects();

    this.subscription = this.personService.selectsResponse$.subscribe(x => {
      this.responseSelect = x;
    });

    this.imageUrl = this.appSettings.userPhotoBaseAddress + this.person.photoName;
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  onFileChange(event: any) {
    this.fileImage = event.target.files[0];
    this.photoName = event.target.files[0].name;
    var reader = new FileReader();
    reader.onload = (event: any) => {
      this.imageUrl = event.target.result as string;
    }

    reader.readAsDataURL(this.fileImage);
  }

  editUser(editUserForm) {
    var updateUserModel = new UpdateUserDto();
    updateUserModel.idUser = this.person.id;
    updateUserModel.firstName = editUserForm.firstName;
    updateUserModel.lastName = editUserForm.lastName;
    updateUserModel.idDepartment = this.responseSelect.data.departments.find(x => x.departmentName == editUserForm.idDepartment.toString()).id;
    updateUserModel.idPosition = this.responseSelect.data.positions.find(x => x.name == editUserForm.idPosition.toString()).id;
    updateUserModel.idRole = editUserForm.idRole;
    updateUserModel.phone = editUserForm.phone;
    updateUserModel.email = editUserForm.email;
    updateUserModel.photoName = editUserForm.photoName == undefined ? this.person.photoName : editUserForm.photoName;

    var validation = this.validateForm(updateUserModel);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.updateUserAfterValidation(updateUserModel);
  }

  validateForm(updateUserModel: UpdateUserDto) : boolean {
    if (updateUserModel.firstName && updateUserModel.lastName && updateUserModel.photoName && 
      updateUserModel.idDepartment > 0  && updateUserModel.idPosition > 0 && updateUserModel.idRole > 0) {
          return true;
        }
    else {
      return false;
    }
  }

  private updateUserAfterValidation(updateUserModel: UpdateUserDto) {
    this.personService.updateUser(updateUserModel);

    this.personService.updateUserDataResponse$.subscribe(x => {
        alert(x.message);

        if (x.succeeded) {
          window.location.reload();
        }
    });
  }

  resetForm(idForm: string) {
    var form = <HTMLFormElement>document.getElementById(idForm);
    form.reset();
  }
}
