/* tslint:disable */
import { Routes } from '@angular/router';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { MainComponent } from './main/main.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { TransactionsNewComponent } from './transactions/transactions-new/transactions-new.component';
import { TransactionsOverviewComponent } from './transactions/transactions-overview/transactions-overview.component';
import { AuthGuard } from './_guards/AuthGuard';
import {ProductOverviewComponent} from './product/product-overview/product-overview.component';
import {ProductCatalogsComponent} from './product/product-catalogs/product-catalogs.component';
import {ProductAddComponent} from './product/product-add/product-add.component';


export const appRoutes: Routes = [

  // canActivate: [AuthGuard] its using whenever user must be auth

  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },

  { path: '', component: MainComponent },

  { path: 'products/overview', component: ProductOverviewComponent },
  { path: 'products/categories', component: ProductCatalogsComponent },
  { path: 'products/add', component: ProductAddComponent },

  { path: 'statistics', component: StatisticsComponent },

  { path: 'transactions/new', component: TransactionsNewComponent },
  { path: 'transactions/overview', component: TransactionsOverviewComponent },

  { path: '**', redirectTo: 'index', pathMatch: 'full' }
];
