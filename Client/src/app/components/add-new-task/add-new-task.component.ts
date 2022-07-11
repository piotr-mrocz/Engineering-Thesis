import { Component, OnInit, OnDestroy } from '@angular/core';
import { PriorityDto } from 'src/app/models/dto/priorityDto';
import { Priority } from 'src/app/models/enums/priority.enum';
import { NewTaskDto } from 'src/app/models/dto/newTaskDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TasksService } from 'src/app/services/tasks.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { PersonService } from 'src/app/services/person-service';
import { UserDto } from 'src/app/models/dto/userDto';

@Component({
  selector: 'app-add-new-task',
  templateUrl: './add-new-task.component.html',
  styleUrls: ['./add-new-task.component.css']
})
export class AddNewTaskComponent implements OnInit, OnDestroy {

  priorityDtoArray = new BackendResponse<PriorityDto[]>();
  allUsersInDepartmentResponse = new BackendResponse<UserDto[]>();
  userId: number;
  private subscription: Subscription;
  isTaskAddBySupervisor: boolean = false;

  constructor(private authService: AuthenticationService,
    private taskService: TasksService,
    private userService: PersonService) {
    this.userId = this.authService.user.id;
  }

  ngOnInit() {
    this.createListOfPriority();
    this.isTaskAddBySupervisor = this.taskService.isTaskAddBySupervisor;

    if (this.isTaskAddBySupervisor) {
      this.getAllUsersInDepartmentByIdSupervisor();
    }
  }

  ngOnDestroy() {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  createListOfPriority() {
    this.taskService.getAllPriority();
    this.subscription = this.taskService.priorityResponse$.subscribe(x => {
      this.priorityDtoArray = x;
    });
  }

  getAllUsersInDepartmentByIdSupervisor() {
    this.userService.getUsersInDepartmentByIdSupervisor(this.userId);

    this.userService.usersDepartmentResponse$.subscribe(x => {
      this.allUsersInDepartmentResponse = x;
    });
  }

  addNewTask(data) {
    var newTask = new NewTaskDto();
    newTask.idUser = this.isTaskAddBySupervisor ? data.idUser : this.userId;
    newTask.title = data.title;
    newTask.description = data.description;
    newTask.deadline = data.deadline.length > 0 ? data.deadline : null;
    newTask.priority = data.priority;
    newTask.whoAdd = this.userId;

    var validation = this.validateForm(newTask);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addNewUserAfterValidation(newTask);
  }

  validateForm(newTask: NewTaskDto): boolean {
    if (this.isTaskAddBySupervisor && (newTask.idUser == 0 || newTask.idUser == null)) {
      return false;
    }

    if (newTask.title && newTask.priority && newTask.idUser) {
      return true;
    }
    else {
      return false;
    }
  }

  private addNewUserAfterValidation(newTask: NewTaskDto) {
    this.taskService.addNewTask(newTask);

    this.taskService.addNewTaskResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        alert(x.message);

        if (x.succeeded) {
          window.location.reload();
        }
      }
    });
  }

  resetForm(idForm: string) {
    var form = <HTMLFormElement>document.getElementById(idForm);
    form.reset();
  }
}
