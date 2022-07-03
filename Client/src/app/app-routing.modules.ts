import { PersonsListComponent } from './components/persons-list/persons-list.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProcessingOfPersonalDataComponent } from './components/processing-of-personal-data/processing-of-personal-data.component';
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component';
import { HomePageComponent } from './components/home-page/home-page.component';

import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './guards/auth.guard';
import { RoleGuard } from './guards/role.guard';
import { ChatComponent } from './components/chat/chat.component';
import { TasksComponent } from './components/tasks/tasks.component';

const routes: Routes = [
    {path: '', redirectTo: '/login', pathMatch: 'full', canActivate: [AuthGuard]},
    {path: 'login', component: LoginComponent},
    {path: 'home', component: HomePageComponent, canActivate: [AuthGuard]},
    {path: 'privacypolicy', component: PrivacyPolicyComponent},
    {path: 'processingofpersonaldata', component: ProcessingOfPersonalDataComponent},
    {path: 'personslist', component: PersonsListComponent, canActivate: [AuthGuard] },
    {path: 'chat', component: ChatComponent, canActivate: [AuthGuard]},
    {path: 'tasks', component: TasksComponent, canActivate: [AuthGuard]},
    // {path: 'persondetails/:id', component: PersonDetailsComponent, canActivate: [AuthGuard, RoleGuard], data: {roles: ['Admin']}},
    {path: '**', component: PageNotFoundComponent}
];

@NgModule({
imports: [RouterModule.forRoot(routes,{
    scrollPositionRestoration: 'enabled',
    anchorScrolling: 'enabled'
  }
    )],
exports: [RouterModule]
})
export class AppRoutingModule {}
