import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { appRoutes } from './routes';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthService } from './_services/auth.service';
import { RegisterComponent } from './register/register.component';
import { NavigationComponent } from './navigation/navigation.component';
import { MainComponent } from './main/main.component';
import { ProductComponent } from './product/product.component';
import { StatisticsComponent } from './statistics/statistics.component';
import { TransactionsOverviewComponent } from './transactions/transactions-overview/transactions-overview.component';
import { TransactionsNewComponent } from './transactions/transactions-new/transactions-new.component';
import { AuthGuard } from './_guards/AuthGuard';
import { ProductOverviewComponent } from './product/product-overview/product-overview.component';
import { ProductCatalogsComponent } from './product/product-catalogs/product-catalogs.component';
import { ProductAddComponent } from './product/product-add/product-add.component';


@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegisterComponent,
    NavigationComponent,
    MainComponent,
    ProductComponent,
    StatisticsComponent,
    TransactionsOverviewComponent,
    TransactionsNewComponent,
    ProductOverviewComponent,
    ProductCatalogsComponent,
    ProductAddComponent,
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(appRoutes)
  ],
  providers: [
    AuthGuard,
    AuthService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
