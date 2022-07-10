export interface GetAllRequestsForLeaveToAcceptDto {
    idRequest?: number,
    displayUserName?: string,
    idApplicant?: number,
    startDate?: string,
    endDate?: string,
    addedDate?: string,
    totalDays?: number,
    absenceType?: string
}
