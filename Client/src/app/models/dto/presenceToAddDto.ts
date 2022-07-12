import { Time } from "@angular/common";

export class PresenceToAddDto {
  startTime?: Time;
  endTime?: Time;
  idUser?: number;
  isPresent?: boolean;
  absenceReason?: number;
}
