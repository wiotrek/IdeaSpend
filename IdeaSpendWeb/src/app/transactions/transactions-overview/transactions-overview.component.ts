import { Component, OnInit } from '@angular/core';
import {AuthService} from '../../_services/auth.service';
import {TransactionService} from '../../_services/transaction.service';
import {Transaction} from '../../_model/transaction';

@Component({
  selector: 'app-transactions-overview',
  templateUrl: './transactions-overview.component.html',
  styleUrls: ['./transactions-overview.component.css']
})
export class TransactionsOverviewComponent implements OnInit {
  transactions: Transaction[];

  constructor(private authService: AuthService,
                      private transactionService: TransactionService) { }

  ngOnInit(): void {
    this.loadTransactions();
  }

  loadTransactions() {
    if (this.authService.loggedIn()){
      this.transactionService.getTransactions(this.authService.decodedToken.nameid)
        .subscribe((transactions: Transaction[]) => {this.transactions = transactions;})
    }
  }

  loadTransactionsBySeller(seller: HTMLInputElement){
    if (this.authService.loggedIn()){
      this.transactionService.getTransactionsBySeller(this.authService.decodedToken.nameid, seller.value)
        .subscribe((transactions: Transaction[]) => {this.transactions = transactions;})
    }
  }

}
