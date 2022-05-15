import { PersonDetailsComponent } from './components/person-details/person-details.component';
import { PersonsListComponent } from './components/persons-list/persons-list.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { ProcessingOfPersonalDataComponent } from './components/processing-of-personal-data/processing-of-personal-data.component';
import { PrivacyPolicyComponent } from './components/privacy-policy/privacy-policy.component';
import { HomePageComponent } from './components/home-page/home-page.component';

import { NgModule } from "@angular/core";
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
    {path: '', redirectTo: '/login', pathMatch: 'full'},
    {path: 'login', component: LoginComponent},
    {path: 'home', component: HomePageComponent},
    {path: 'privacypolicy', component: PrivacyPolicyComponent},
    {path: 'processingofpersonaldata', component: ProcessingOfPersonalDataComponent},
    {path: 'personslist', component: PersonsListComponent},
    {path: 'persondetails/:id', component: PersonDetailsComponent},
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
