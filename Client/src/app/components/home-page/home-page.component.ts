import { Component, OnInit, OnDestroy } from '@angular/core';
import { ImportantInfoService } from 'src/app/services/important-info.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { GetImportantInfoDto } from 'src/app/models/dto/getImportantInfoDto';
import { AddImportantInfoDto } from 'src/app/models/dto/addImportantInfoDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { Roles } from 'src/app/models/enums/roles.enum';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit, OnDestroy {

  isAuthorized: boolean = true;
  today: string;

  private subscription: Subscription;
  infoResponse: BackendResponse<GetImportantInfoDto[]>;
  userId: number;
  userRole: string;

  constructor(private infoService: ImportantInfoService,
    private authService: AuthenticationService) {
      this.userId = this.authService.user.id;
      this.userRole = this.authService.user.role;
    }

  ngOnInit() {
    this.getImportantInfos();
    this.isUserAuthorized();
    this.today = new Date().toJSON().slice(0,10).replace(/-/g,'-');
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  isUserAuthorized(): boolean {
    var admin = Roles[Roles.admin];
    var manager = Roles[Roles.manager];
    var supervisor = Roles[Roles.supervisor];
    this.isAuthorized = (this.userRole.toLocaleLowerCase() == admin.toLocaleLowerCase() ||
                         this.userRole.toLocaleLowerCase() == manager.toLocaleLowerCase() ||
                         this.userRole.toLocaleLowerCase() == supervisor.toLocaleLowerCase());
    return this.isAuthorized;
  }

  getImportantInfos() {
    this.infoService.getInfos();
    this.subscription = this.infoService.getInfosResponse$.subscribe(x => {
      this.infoResponse = x;
    });
  }

  addNewImportantInfo(newInfo) {  
    var newUserModel = new AddImportantInfoDto();
    newUserModel.info = newInfo.info;
    newUserModel.startDate = newInfo.startDate;
    newUserModel.endDate = newInfo.endDate;
    newUserModel.idUser = this.userId;

    var validation = this.validateForm(newUserModel);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addNewUserAfterValidation(newUserModel);
  }

  validateForm(newInfo: AddImportantInfoDto) : boolean {
    if (newInfo) {
          return true;
        }
    else {
      return false;
    }
  }

  private addNewUserAfterValidation(newInfo: AddImportantInfoDto) {
    this.infoService.addNewInfo(newInfo);
    this.infoService.addNewInfoResponse$.subscribe(x => {
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
