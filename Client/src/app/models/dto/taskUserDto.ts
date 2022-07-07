export interface TaskUserDto {
    id?: number,
    idUser?: number,
    whoAdded?: number,
    user?: string,
    photoName?: string,
    title?: string
    description?: string,
    deadline?: string,
    addedDate?: string,
    progressDate?: string,
    finishDate?: string,
    status?: number,
    priorityDescription?: string,
    priority?: number
}
