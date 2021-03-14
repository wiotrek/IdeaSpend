import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class TransactionDateService {

  months: Array<string>;
  years: Array<number>;

  constructor() { }

  setMonthRange(): Array<string>{
    // if (this.years[0])
    //   this.months = ['Październik', 'Listopad', 'Grudzień'];
    // else
      this.months = ['Styczeń', 'Luty', 'Październik'];

    return this.months;
  }

  setYearRange() {
    this.years = [2020, 2021];
  }

}
