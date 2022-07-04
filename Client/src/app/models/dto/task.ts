export interface Task {
    id?: number,
    idUser?: number,
    title?: string
    description?: string,
    deadline?: Date,
    addedDate?: Date,
    ProgressDate?: Date,
    FinishDate?: Date,
    status?: number,
    priority?: string
}
