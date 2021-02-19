import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainComponent } from './main/main.component';
import { ProductComponent } from './product/product.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { TransactionsNewComponent } from './transactions/transactions-new/transactions-new.component';
import { TransactionsOverviewComponent } from './transactions/transactions-overview/transactions-overview.component';
import { AuthGuard } from './_guards/AuthGuard';


export const appRoutes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: '', component: MainComponent, canActivate: [AuthGuard] },
  { path: 'products', component: ProductComponent, canActivate: [AuthGuard] },
  { path: 'statistics', component: StatisticsComponent, canActivate: [AuthGuard] },
  { path: 'transactions/new', component: TransactionsNewComponent, canActivate: [AuthGuard] },
  { path: 'transactions/overview', component: TransactionsOverviewComponent, canActivate: [AuthGuard] },
  { path: '**', redirectTo: 'index', pathMatch: 'full' }
];
