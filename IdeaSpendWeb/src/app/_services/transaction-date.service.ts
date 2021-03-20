import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable} from 'rxjs';
import {BaseService} from './base.service';
import {MonthMapper} from '../_mappers/month-mapper';

@Injectable({
  providedIn: 'root'
})
export class TransactionDateService extends BaseService {

  months: Array<string> = [];
  years: Array<string> = [];

  constructor(private http: HttpClient) { super(); }

  getDateRange(userId: number): Observable<string[]> {
    return this.http.get<string[]>(`${this.backend}/api/transaction/get/date-range/${userId}`)
  }

  // Initialize months with at least one registered transaction for selected year
  setMonthRange(): Array<string>{
    let monthMapper: MonthMapper = new MonthMapper();
    for (let i = 1; i <= 12; i++){
        this.months[i-1] = monthMapper.numberToWord(i.toString());
    }

    return this.months;
  }

  // Initialize years with registered transactions
  setYearRange(dates: string[]) : string[]{
    if (dates.length > 0) {
      let firstYear: number = +dates[0].substring(0, 4);
      let lastYear: number = +dates[dates.length - 1].substring(0, 4);
      for (let i = firstYear; i <= lastYear; i++) {
        this.years[i - firstYear] = i.toString();
      }
    }

    return this.years;
  }

}
