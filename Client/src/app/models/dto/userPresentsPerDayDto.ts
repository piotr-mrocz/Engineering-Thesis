export interface UserPresentsPerDayDto {
    userName?: string,
    isPresent?: boolean,
    presentType?: Int16Array,
    absenceReason?: string,
    startTime?: string,
    endTime?: string
}
