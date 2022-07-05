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
import { PriorityDto } from '../models/dto/priorityDto';
import { TaskUserDto } from '../models/dto/taskUserDto';

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

  isTaskAddBySupervisor: boolean = false;

  priorityResponse$ = new BehaviorSubject<BackendResponse<PriorityDto[]>>({});
  tasksResponse$ = new BehaviorSubject<BackendResponse<Task[]>>({});
  tasksForSupervisorResponse$ = new BehaviorSubject<BackendResponse<TaskUserDto[]>>({});
  addNewTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  updateTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  updateStatusTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});
  deleteTaskResponse$ = new BehaviorSubject<BaseBackendResponse>({});

  private getAllUserTasksResponse(idUser: number, status: number) : Observable<BackendResponse<Task[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllUserTasksEndpoint;
    return this.http.post<BackendResponse<Task[]>>(url, {IdUser: idUser, Status: status});
  }

  private getAllUserTasksForSupervisorResponse(idSupervisor: number, status: number) : Observable<BackendResponse<TaskUserDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getUsersTasksForSupervisorEndpoint;
    return this.http.post<BackendResponse<TaskUserDto[]>>(url, {IdSupervisor: idSupervisor, Status: status});
  }

  private addNewTaskResponse(newTaskDto: NewTaskDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.addNewTaskEndpoint;
    return this.http.post<BaseBackendResponse>(url, newTaskDto);
  }

  private updateTaskResponse(updateTaskDto: UpdateTaskDto) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.updateTaskEndpoint;
    return this.http.post<BaseBackendResponse>(url, updateTaskDto);
  }

  private updateStatusTaskResponse(idTask: number, status: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.updateStatusTaskEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdTask: idTask,Status: status});
  }

  private deleteTaskResponse(idTask: number) : Observable<BaseBackendResponse> {
    var url = this.backendSettings.baseAddress + this.endpoints.deleteTaskEndpoint;
    return this.http.post<BaseBackendResponse>(url, {IdTask: idTask});
  }

  private getAllPriorityResponse() : Observable<BackendResponse<PriorityDto[]>> {
    var url = this.backendSettings.baseAddress + this.endpoints.getAllPriorityEndpoint;
    return this.http.post<BackendResponse<PriorityDto[]>>(url, {});
  }

  getAllUserTasks(idUser: number, status: number) {
    this.getAllUserTasksResponse(idUser, status).subscribe(x => {
      this.tasksResponse$.next(x);
    });
  }

  getAllUserTasksForSupervisor(idSupervisor: number, status: number) {
    this.getAllUserTasksForSupervisorResponse(idSupervisor, status).subscribe(x => {
      this.tasksForSupervisorResponse$.next(x);
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

  updateStatusTask(idTask: number, status: number) {
    this.updateStatusTaskResponse(idTask, status).subscribe(x => {
      this.updateStatusTaskResponse$.next(x);
    });
  }

  deleteTask(idTask: number) {
    this.deleteTaskResponse(idTask).subscribe(x => {
      this.deleteTaskResponse$.next(x);
    });
  }

  getAllPriority() {
    this.getAllPriorityResponse().subscribe(x => {
      this.priorityResponse$.next(x);
    });
  }
}
