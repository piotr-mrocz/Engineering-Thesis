import { Component, OnInit, OnDestroy } from '@angular/core';
import { BackendResponse } from 'src/app/models/response/backendResponse';
import { Subscription } from 'rxjs';
import { TasksService } from 'src/app/services/tasks.service';
import { AuthenticationService } from 'src/app/services/authentication.service';
import { TasksStatus } from 'src/app/models/enums/tasksStatus.enum';
import { Task } from 'src/app/models/dto/task';
import { Priority } from 'src/app/models/enums/priority.enum';

@Component({
  selector: 'app-tasks',
  templateUrl: './tasks.component.html',
  styleUrls: ['./tasks.component.css']
})
export class TasksComponent implements OnInit, OnDestroy {

  tasksResponse: BackendResponse<Task[]>;
  private subscription: Subscription;
  
  userId: number;

  veryUrgentStatus: number = Priority.veryUrgent;
  urgentStatus: number = Priority.urgent;
  importantStatus: number = Priority.important;
  canWaitStatus: number = Priority.canWait;

  toDoStatus: number = TasksStatus.toDo;
  inProgressStatus: number = TasksStatus.inProgress;
  doneStatus: number = TasksStatus.done;

  constructor(private tasksService: TasksService,
    private authService: AuthenticationService) { 
      this.userId = this.authService.user.id;

    }
    
  ngOnInit() {
    this.getToDoTasks("tasksToDo");
  }

  ngOnDestroy() {
    this.subscription.unsubscribe();
  }

  private getAllTasks(idClickedButton: string, status: number) {
    this.changeClickedButtonBackgroundColor(idClickedButton);

    this.tasksService.getAllUserTasks(this.userId, status);
    this.subscription = this.tasksService.tasksResponse$.subscribe(x  => {
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

  startTask(taskId: number) {
    this.changeStatusTask(taskId, this.inProgressStatus);
  }

  finishTask(taskId: number) {
    this.changeStatusTask(taskId, this.doneStatus);
  }

  changeStatusTask(taskId: number, newStatus: number) {
    this.tasksService.updateStatusTask(taskId, newStatus);
    this.tasksService.updateStatusTaskResponse$.subscribe(x => {
      alert(x.message);

      if (x.succeeded) {
        this.ngOnInit();
      }
    });
  }

  deleteTask(taskId: number) {
    this.tasksService.deleteTask(taskId);
    this.tasksService.deleteTaskResponse$.subscribe(x => {
      alert(x.message);

      if (x.succeeded) {
        this.ngOnInit();
      }
    });
  }
}
