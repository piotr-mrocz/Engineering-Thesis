import { Task } from "./task";

export interface TasksListDto {
    toDoTasks?: Array<Task>,
    inProgressTasks?: Array<Task>,
    doneTasks?: Array<Task>
}
