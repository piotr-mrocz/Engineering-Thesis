import { UserPresentsPerDayDto } from "./userPresentsPerDayDto";

export interface UsersPresencesPerDayDto {
    usersNNPresencesList?: UserPresentsPerDayDto[],
    usersPresentPresencesList?: UserPresentsPerDayDto[],
    usersAbsentPresencesList?: UserPresentsPerDayDto[];
}
