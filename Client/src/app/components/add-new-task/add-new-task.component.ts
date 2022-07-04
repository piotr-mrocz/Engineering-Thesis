import { Component, OnInit, OnDestroy } from '@angular/core';
import { PriorityDto } from 'src/app/models/dto/priorityDto';
import { Priority } from 'src/app/models/enums/priority.enum';
import { NewTaskDto } from 'src/app/models/dto/newTaskDto';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TasksService } from 'src/app/services/tasks.service';
import { Subscription } from 'rxjs';
import { BackendResponse } from 'src/app/models/response/backendResponse';

@Component({
  selector: 'app-add-new-task',
  templateUrl: './add-new-task.component.html',
  styleUrls: ['./add-new-task.component.css']
})
export class AddNewTaskComponent implements OnInit, OnDestroy {

  priorityDtoArray = new BackendResponse<PriorityDto[]>();
  userId: number;
  private subscription: Subscription;

  constructor(private authService: AuthenticationService,
    private taskService: TasksService) {
    this.userId = this.authService.user.id;
  }

  ngOnInit() {
    this.createListOfPriority();
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  createListOfPriority() {
    this.taskService.getAllPriority();
    this.subscription = this.taskService.priorityResponse$.subscribe(x => {
      this.priorityDtoArray = x;
    });
  }

  addNewTask(data) {
    var newTask = new NewTaskDto();
    newTask.idUser = this.userId;
    newTask.title = data.title;
    newTask.description = data.description;
    newTask.deadline = data.deadline;
    newTask.priority = data.priority;

    var validation = this.validateForm(newTask);

    if (!validation) {
      alert("Nie uzupełniono wszystkich wymaganych pól!");
      return;
    }

    this.addNewUserAfterValidation(newTask);
  }

  validateForm(newTask: NewTaskDto) : boolean {
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
        alert(x.message);

        if (x.succeeded) {
          window.location.reload();
        }
    });
  }

  resetForm(idForm: string) {
    var form = <HTMLFormElement>document.getElementById(idForm);
    form.reset();
  }
}
