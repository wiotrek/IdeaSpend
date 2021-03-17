/* tslint:disable */
import {Component, Input, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from '../_services/auth.service';
import {TransactionDateService} from '../_services/transaction-date.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  selectedMonth: string;
  months: Array<string> = [];
  // The color text of the dropdown button
  activeColor: string;
  logoImage = '../assets/logo/Logo-mini.png';

  constructor(private router: Router,
              public authService: AuthService,
              private transactionDateService: TransactionDateService) {}

  ngOnInit(): void {
    this.setMonth();
    this.setColor();
  }

  getSelectedMonth(selected: string): void {
      this.selectedMonth = selected;
      localStorage.setItem('month', selected);
  }

  public getMonths(): Array<string> {
    this.months = this.transactionDateService.setMonthRange();
    return this.months;
  }

  // Setting month on each page when user is
  public setMonth(): void {
    // If no one user select month then set month of last active transaction
    if (localStorage.getItem('month') === null)
      this.selectedMonth = 'Styczeń';
    // otherwise retrieve month from local storage
    else
      this.selectedMonth = localStorage.getItem('month');
  }

  //TODO: Good bye
  public years: Array<number> = [
    2021, 2020
  ];

  public currency: Array<string> = [
    "PLN", "USD", "EUR"
  ];

  // Setting color text for dropdown button of the navigation depends on current url
  //region Get/Set Color

  setColor(): void {

    if (this.router.url.includes('products')) {
      this.activeColor = '#fff';
    }

    if (this.router.url.includes('transactions')) {
      this.activeColor = '#fff';
    }

  }

  getTransactionColor(): string {
    if (this.router.url.includes('/transactions')) {
      return this.activeColor;
    }
  }

  getProductColor(): string {
    if (this.router.url.includes('/products')) {
      return this.activeColor;
    }
  }

  //endregion

}
