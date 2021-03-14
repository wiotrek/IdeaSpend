import {Component, OnInit} from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {TransactionService} from '../../_services/transaction.service';
import {Transaction} from '../../_model/transaction';
import {MonthMapper} from '../../_mappers/month-mapper';

@Component({
  selector: 'app-transactions-overview',
  templateUrl: './transactions-overview.component.html',
  styleUrls: ['./transactions-overview.component.css']
})
export class TransactionsOverviewComponent implements OnInit {
  transactions: Transaction[];
  page = 1;
  pageSize = 6;

  constructor(private authService: AuthService,
              private transactionService: TransactionService
    ) { }

  ngOnInit(): void {
    this.loadTransactions();
  }

  // Initialize transactions by date on load page or after every changes date from navigation
  loadTransactions(): void {
    let selectedDate: string
    let convertBack = new MonthMapper();

    // Build selected date to correct format
    if (localStorage.getItem('year') !== null && localStorage.getItem('month') !== null)
      selectedDate = localStorage.getItem('year') + '-' + convertBack.wordToNumber(localStorage.getItem('month')) + '-01'

    if (this.authService.loggedIn()){
      this.transactionService.getTransactions(this.authService.decodedToken.nameid, selectedDate)
        .subscribe((transactions: Transaction[]) => {
          this.transactions = transactions;
        });
    }
  }

  loadTransactionsBySeller(seller: HTMLInputElement): void{
    if (this.authService.loggedIn()){
      this.transactionService.getTransactionsBySeller(this.authService.decodedToken.nameid, seller.value)
        .subscribe((transactions: Transaction[]) => {
          this.transactions = transactions;
        });
    }
  }

}
