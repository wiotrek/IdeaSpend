import {Component, OnInit} from '@angular/core';
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
    this.transactionService.getLast5Transactions(this.authService.decodedToken.nameid)
      .subscribe((transactions: Transaction[]) => {
        this.transactions = transactions;
    })
  }

}
