export interface GetPresenceByIdUserDto {
    date?: string,
    dayNumber?: number,
    isFreeDay?: boolean,
    isPresent?: boolean,
    presentType?: number,
    absenceReason?: string,
    startTime?: string,
    endTime?: string,
    workHours?: string,
    extraWorkHours?: string
}
