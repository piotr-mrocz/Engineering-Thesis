import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BackendSettings } from '../models/consts/backendSettings';
import { AuthenticationService } from './authentication.service';
import { EndpointsUrl } from '../models/consts/endpointsUrl';
import { BehaviorSubject, Observable } from 'rxjs';
import { BackendResponse } from '../models/response/backendResponse';
import { BaseBackendResponse } from '../models/response/BaseBackendResponse';
import { NewTaskDto } from '../models/dto/newTaskDto';
import { UpdateTaskDto } from '../models/dto/updateTaskDto';
import { Task } from '../models/dto/task';

@Injectable({
  providedIn: 'root'
})
export class TasksService {

  private token;

  constructor(private http: HttpClient, 
    private backendSettings: BackendSettings, 
    private authService: AuthenticationService,
    private endpoints: EndpointsUrl) {
    this.token = this.authService.getToken();
  }

  tasksResponse$ = new BehaviorSubject<BackendResponse<Task[]>>({});
  addNewTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  updateTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  deleteTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getAllUserTasksResponse(idUser: number, status: number) : Observable<BackendResponse<Task[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllUserTasks;
    return this.http.post<BackendResponse<Task[]>>(url, {IdUser: idUser, Status: status});
  }

  private addNewTaskResponse(newTaskDto: NewTaskDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.addNewTask;
    return this.http.post<BaseBackendResponse>(url, newTaskDto);
  }

  private updateTaskResponse(updateTaskDto: UpdateTaskDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.updateTask;
    return this.http.post<BaseBackendResponse>(url, updateTaskDto);
  }

  private deleteTaskResponse(idTask: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.deleteTask;
    return this.http.post<BaseBackendResponse>(url, idTask);
  }

  getAllUserTasks(idUser: number, status: number) {
    this.getAllUserTasksResponse(idUser, status).subscribe(x => {
      this.tasksResponse$.next(x);
    });
  }

  addNewTask(newTaskDto: NewTaskDto) {
    this.addNewTaskResponse(newTaskDto).subscribe(x => {
      this.addNewTaskResponse$.next(x);
    });
  }

  updateTask(updateTaskDto: UpdateTaskDto) {
    this.updateTaskResponse(updateTaskDto).subscribe(x => {
      this.updateTaskResponse$.next(x);
    });
  }

  deleteTask(idTask: number) {
    this.deleteTaskResponse(idTask).subscribe(x => {
      this.deleteTaskResponse$.next(x);
    });
  }
}