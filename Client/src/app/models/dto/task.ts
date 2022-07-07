export interface Task {
    id?: number,
    idUser?: number,
    whoAdded?: number,
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
