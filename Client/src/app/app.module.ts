import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProcessingOfPersonalDataComponent } from './components/processing-of-personal-data/processing-of-personal-data.component';
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component';
import { HomePageComponent } from './components/home-page/home-page.component';
import { FooterComponent } from './components/footer/footer.component';
import { NavComponent } from './components/nav/nav.component';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.modules';
import { PersonsListComponent } from './components/persons-list/persons-list.component';
import { PersonService } from './services/person-service';
import { AuthenticationService } from './services/authentication.service';
import { LoginComponent } from './components/login/login.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { TokenInterceptorService } from './services/token-interceptor.service';
import { AddNewUserComponent } from './components/add-new-user/add-new-user.component';
import { ChatComponent } from './components/chat/chat.component';
import { MessageService } from './services/message.service';
import { TasksComponent } from './components/tasks/tasks.component';
import { TasksService } from './services/tasks.service';
import { AddNewTaskComponent } from './components/add-new-task/add-new-task.component';
import { UsersTasksListComponent } from './components/users-tasks-list/users-tasks-list.component';
import { EditUserComponent } from './components/edit-user/edit-user.component';

@NgModule({
  declarations: [
    AppComponent,
    NavComponent,
    FooterComponent,
    HomePageComponent,
    PrivacyPolicyComponent,
    ProcessingOfPersonalDataComponent,
    PageNotFoundComponent,
    PersonsListComponent, 
    LoginComponent, 
    AddNewUserComponent, 
    ChatComponent, 
    TasksComponent, AddNewTaskComponent, UsersTasksListComponent, EditUserComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    FormsModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    ReactiveFormsModule
  ],
  providers: [
    PersonService, AuthenticationService, MessageService, TasksService,
    { provide: HTTP_INTERCEPTORS, useClass: TokenInterceptorService, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
