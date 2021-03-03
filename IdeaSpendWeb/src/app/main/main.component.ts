import { Component, OnInit } from '@angular/core';
import {AuthService} from '../_services/auth.service';
import {TransactionService} from '../_services/transaction.service';
import {Transaction} from '../_model/transaction';

@Component({
  selector: 'app-main',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent implements OnInit {

  transactions: Transaction[];

  // TODO: change font family for information inside square

  greenTriangleImage = '../assets/main/green-triangle.png';
  redTriangleImage = '../assets/main/red-triangle.png';

  constructor(private authService: AuthService,
              private transactionService: TransactionService) { }

  ngOnInit(): void {
    this.loadLastTransaction();
  }

  loadLastTransaction() {
    this.transactionService.getTransactions(this.authService.decodedToken.nameid)
      .subscribe((transactions: Transaction[]) => {

        transactions.sort((d, d1) => (d.transactionDate > d1.transactionDate ? -1 : 1));
        this.transactions = transactions .slice(0, 5);

    })
  }

  public months: Array<string> = [
    "styczen", "luty", "marzec", "kwiecien", "maj",
    "czerwiec", "lipiec", "sierpien", "wrzesien", "pazdziernik",
    "listopad", "grudzien"
  ];

  public years: Array<number> = [
    2020, 2019, 2018
  ];

  public currency: Array<string> = [
    "PLN", "USD", "EUR"
  ];

}
