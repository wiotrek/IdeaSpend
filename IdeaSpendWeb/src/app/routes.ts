import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainComponent } from './main/main.component';
import { ProductComponent } from './product/product.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { TransactionsComponent } from './transactions/transactions.component';


export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: '', component: MainComponent },
  { path: 'products', component: ProductComponent },
  { path: 'statistics', component: StatisticsComponent },
  { path: 'transactions', component: TransactionsComponent },
  { path: '**', redirectTo: 'index', pathMatch: 'full' }
];
