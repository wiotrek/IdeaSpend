import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainComponent } from './main/main.component';
import { ProductComponent } from './product/product.component';
import { StatisticsComponent } from './statistics/statistics.component';
import {TransactionsNewComponent} from './transactions/transactions-new/transactions-new.component';
import {TransactionsOverviewComponent} from './transactions/transactions-overview/transactions-overview.component';


export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', component: MainComponent },
  { path: 'products', component: ProductComponent },
  { path: 'statistics', component: StatisticsComponent },
  { path: 'transactions/new', component: TransactionsNewComponent },
  { path: 'transactions/overview', component: TransactionsOverviewComponent },
  { path: '**', redirectTo: 'index', pathMatch: 'full' }
];
