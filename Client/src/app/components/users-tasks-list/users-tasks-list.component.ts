import { Component, OnInit, OnDestroy } from '@angular/core';
import { Priority } from 'src/app/models/enums/priority.enum';
import { TasksStatus } from 'src/app/models/enums/tasksStatus.enum';
import { TasksService } from 'src/app/services/tasks.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { Subscription } from 'rxjs';
import { TaskUserDto } from 'src/app/models/dto/taskUserDto';

@Component({
  selector: 'app-users-tasks-list',
  templateUrl: './users-tasks-list.component.html',
  styleUrls: ['./users-tasks-list.component.css']
})
export class UsersTasksListComponent implements OnInit, OnDestroy {

  userId: number;

  veryUrgentStatus: number = Priority.veryUrgent;
  urgentStatus: number = Priority.urgent;
  importantStatus: number = Priority.important;
  canWaitStatus: number = Priority.canWait;

  toDoStatus: number = TasksStatus.toDo;
  inProgressStatus: number = TasksStatus.inProgress;
  doneStatus: number = TasksStatus.done;

  tasksResponse: BackendResponse<TaskUserDto[]>;
  private subscription: Subscription;

  constructor(private tasksService: TasksService,
    private authService: AuthenticationService) {
    this.userId = this.authService.user.id;
  }

  ngOnInit() {
    this.tasksService.isTaskAddBySupervisor = true;
    this.getToDoTasks("tasksToDo");
  }

  ngOnDestroy() {
    if (this.subscription != undefined) {
      this.subscription.unsubscribe();
    }
  }

  private getAllTasks(idClickedButton: string, status: number) {
    this.changeClickedButtonBackgroundColor(idClickedButton);

    this.tasksService.getAllUserTasksForSupervisor(this.userId, status);
    this.subscription = this.tasksService.tasksForSupervisorResponse$.subscribe(x => {
      this.tasksResponse = x;
    });
  }

  getToDoTasks(idClickedButton: string) {
    var status = TasksStatus.toDo;
    this.getAllTasks(idClickedButton, status);
  }

  getInProgressTasks(idClickedButton: string) {
    var status = TasksStatus.inProgress;
    this.getAllTasks(idClickedButton, status);
  }

  getDoneTasks(idClickedButton: string) {
    var status = TasksStatus.done;
    this.getAllTasks(idClickedButton, status);
  }

  changeClickedButtonBackgroundColor(idClickedButton: string) {
    var buttons = (<HTMLScriptElement[]><any>document.getElementsByClassName("getTasksButton"));

    //reset styles all buttons
    for (let i = 0; i < buttons.length; i++) {
      var button = buttons[i];
      button.style.backgroundColor = "";
    }

    var clickedButton = (<HTMLScriptElement><any>document.getElementById(idClickedButton));

    //set background color clicked button
    clickedButton.style.backgroundColor = "#17a2b8";
  }

  deleteTask(taskId: number) {
    this.tasksService.deleteTask(taskId);
    this.tasksService.deleteTaskResponse$.subscribe(x => {
      if (x.succeeded != undefined) {
        alert(x.message);

        if (x.succeeded) {
          this.ngOnInit();
        }
      }
    });
  }
}
