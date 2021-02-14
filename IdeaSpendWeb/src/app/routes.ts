import { Routes } from '@angular/router';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {MainComponent} from './main/main.component';


export const appRoutes: Routes = [
  { path: 'register', component: RegisterComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', component: MainComponent },
  { path: '**', redirectTo: 'index', pathMatch: 'full' }
];
