import { GetPresenceByIdUserDto } from "./getPresenceByIdUserDto";

export interface GetPresenceByIdUserListDto {
    totalWorkHour?: number,
    totalWorkExtraHour?: number,
    userPresencesList?: GetPresenceByIdUserDto[]
}