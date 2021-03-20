/* tslint:disable */
import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from '../_services/auth.service';
import {TransactionDateService} from '../_services/transaction-date.service';
import {MonthMapper} from '../_mappers/month-mapper';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  // Full represent data with format yyyy-MM-dd
  dateTransaction: string[] = [];

  selectedMonth: string;
  selectedYear: string;
  months: Array<string> = [];
  years: Array<string> = ['2020'];
  // The color text of the dropdown button
  activeColor: string;
  logoImage = '../assets/logo/Logo-mini.png';

  constructor(private router: Router,
              public authService: AuthService,
              private transactionDateService: TransactionDateService) {}

  ngOnInit(): void {
    this.loadDateRange();
    this.setMonth();
    this.setYear();
    this.setColor();
  }

  loadDateRange() {
    if (this.authService.loggedIn()){
      this.transactionDateService.getDateRange(this.authService.decodedToken.nameid)
        .subscribe((dates: string[]) => { this.dateTransaction = dates; }
        );
    }
  }

  getSelectedMonth(selected: string): void {
      this.selectedMonth = selected;
      localStorage.setItem('month', selected);
  }

  getSelectedYear(selected: string) {
    this.selectedYear = selected;
    localStorage.setItem('year', selected.toString());
  }

  // region Get / Set Month

  // Load available months on specify year to select by user
  getMonths(): Array<string> {
    this.months = this.transactionDateService.setMonthRange();
    return this.months;
  }

  // Setting selected month on each page when user is
  public setMonth(): void {
    let monthMapper: MonthMapper = new MonthMapper();
    // If no one user select month then set month of last active transaction
    if (localStorage.getItem('month') === null)
      this.selectedMonth = monthMapper.numberToWord(this.dateTransaction[1].substring(5, 2));
    // otherwise retrieve month from local storage
    else
      this.selectedMonth = localStorage.getItem('month');
  }
  // endregion

  // region Get / Set Year

  // Load available years to select by user
  getYears(): Array<string> {
    return this.transactionDateService.setYearRange(this.dateTransaction);
  }

  // Setting selected year on each page when user is
  setYear(): void {
    // If no one user select year then set year of last active transaction
    if (localStorage.getItem('year') === null)
      this.selectedYear = this.dateTransaction[1].substring(0, 4);
    // otherwise retrieve year from local storage
    else
      this.selectedYear = localStorage.getItem('year');
  }

  // endregion

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
