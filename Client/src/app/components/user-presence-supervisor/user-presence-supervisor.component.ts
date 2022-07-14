import { Component, OnInit } from '@angular/core';
import { PresenceService } from 'src/app/services/presence.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { UserPresentsPerDayDto } from 'src/app/models/dto/userPresentsPerDayDto';
import { UsersPresencesPerDayDto } from 'src/app/models/dto/usersPresencesPerDayDto';
import { AbsenceTypes } from 'src/app/models/enums/absenceType';
import { PossibleAbsenceToChooseDto } from 'src/app/models/dto/possibleAbsenceToChooseDto';
import { PresenceToAddRangeDto } from 'src/app/models/dto/presenceToAddRangeDto';
import { PresenceToAddDto } from 'src/app/models/dto/presenceToAddDto';
import { TimeSpan } from "timespan"

@Component({
  selector: 'app-user-presence-supervisor',
  templateUrl: './user-presence-supervisor.component.html',
  styleUrls: ['./user-presence-supervisor.component.css']
})
export class UserPresenceSupervisorComponent implements OnInit {

  private subscription: Subscription;
  today: Date;
  day: number;
  month: number;
  year: number;
  presenceResponse: BackendResponse<UsersPresencesPerDayDto>;
  absenceResponse: BackendResponse<PossibleAbsenceToChooseDto[]>;
  clickedButton: string;

  holiday: number = AbsenceTypes.holiday;
  l4: number = AbsenceTypes.doctorExcuse;
  delagation: number = AbsenceTypes.delegation;
  matherFreeDays: number = AbsenceTypes.maternityLeave;
  unauthorizedAbsence: number = AbsenceTypes.unauthorizedAbsence;
  weekend: number = AbsenceTypes.weekend;
  present: number = AbsenceTypes.present;

  createRangePresence: boolean = null;
  isPresent: boolean = false;
  absenceType: number;
  userId: number;

  constructor(private presenceService: PresenceService) {
   }

  ngOnInit() {
    this.today = new Date();
    this.day = this.today.getDate();
    this.month = this.today.getMonth() + 1;
    this.year = this.today.getFullYear();

    this.getNNUsers('allNNUsers');

    this.getPresences(this.day, this.month, this.year);
    this.getAllPossibleAbsenceTypeToChoose();
  }

  ngOnDestroy(): void {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  getAllPossibleAbsenceTypeToChoose() {
    this.presenceService.getAllPossibleAbsenceTypeToChoosePresence();
    this.subscription = this.presenceService.getAllPossibleAbsenceTypeToChoosePresenceResponse$.subscribe(x => {
      this.absenceResponse = x;
    });
  }

  getActualDate(dateTime: Date) {
    var date = new Date(dateTime.getTime());
    date.setHours(0, 0, 0, 0);
    return date;
  }

  getPresences(day: number, month: number, year: number) {
    this.presenceService.getUsersPresencesPerDay(day, month, year);
    this.subscription = this.presenceService.getUsersPresencesPerDayResponse$.subscribe(x => {
      this.presenceResponse = x;
    });
  }

  getNNUsers(idClickedButton: string) {
    this.changeClickedButtonBackgroundColor(idClickedButton);
    this.clickedButton = idClickedButton;
  }

  changeClickedButtonBackgroundColor(idClickedButton: string) {
    var buttons = (<HTMLScriptElement[]><any>document.getElementsByClassName("getUsersButton"));

    //reset styles all buttons
    for (let i = 0; i < buttons.length; i++) {
      var button = buttons[i];
      button.style.backgroundColor = "";
    }

    var clickedButton = (<HTMLScriptElement><any>document.getElementById(idClickedButton));

    //set background color clicked button
    if (idClickedButton == 'allNNUsers') {
      clickedButton.style.backgroundColor = "#dc3545";
    }
    else {
      clickedButton.style.backgroundColor = "#17a2b8";
    }
  }

  showForm(absenceType: number, userId: number) {
    
      if (absenceType == this.holiday || absenceType == this.l4 || absenceType == this.delagation || absenceType == this.matherFreeDays) {
        this.createRangePresence = true;
      }
      else {
        this.createRangePresence = false;
      }

      this.absenceType = absenceType;
      this.userId = userId;
      this.setPresent(absenceType);
  }

  setPresent(absenceType: number) {
    if (absenceType == this.present) {
      this.isPresent = true;
    }
    else {
      this.isPresent = false;
    }
  }

  createRangePresences(startDate: Date, endDate: Date) {
    var rangeModel = new PresenceToAddRangeDto();
    rangeModel.startDate = startDate;
    rangeModel.endDate = endDate;
    rangeModel.idUser = this.userId;
    rangeModel.isPresent = this.isPresent;
    rangeModel.absenceReason = this.absenceType;

    var validation = this.validateRangeForm(rangeModel);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addRangePresencesAfterValidation(rangeModel);
  }

  validateRangeForm(rangeModel: PresenceToAddRangeDto) : boolean {
    if (rangeModel.startDate && rangeModel.endDate && rangeModel.absenceReason > 0 && rangeModel.idUser > 0) {
      return true;
    }
    else {
      return false;
    }
  }

  addRangePresencesAfterValidation(rangeModel: PresenceToAddRangeDto) {
    this.presenceService.createRangePresence(rangeModel);
    this.subscription = this.presenceService.createRangePresenceResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        alert(x.message);

        if (x.succeeded) {
          window.location.reload();
        }
      }
    });
  }

  createPresence(startTime, endTime) {
    var newPresence = new PresenceToAddDto();
    newPresence.idUser = this.userId;
    newPresence.startTime = startTime;
    newPresence.endTime = endTime;
    newPresence.isPresent = this.isPresent;
    newPresence.absenceReason = this.absenceType;

    var validation = this.validateForm(newPresence);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addPresenceAfterValidation(newPresence);
  }

  validateForm(newPresence: PresenceToAddDto) : boolean {
    if (newPresence.startTime && newPresence.endTime && newPresence.absenceReason > 0 && newPresence.idUser > 0) {
      return true;
    }
    else {
      return false;
    }
  }

  addPresenceAfterValidation(newPresence: PresenceToAddDto) {
    this.presenceService.createPresence(newPresence);
    this.subscription = this.presenceService.createPresenceResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        alert(x.message);

        if (x.succeeded) {
          window.location.reload();
        }
      }
    });
  }
}
