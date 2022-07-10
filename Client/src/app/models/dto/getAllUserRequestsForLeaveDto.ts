export interface GetAllUserRequestsForLeaveDto {
    idRequest?: number,
    createdDate?: string,
    startDate?: string,
    endDate?: string,
    totalDays?: number,
    absenceType?: string,
    status?: number,
    statusDescription?: string,
    reason?: string
}
