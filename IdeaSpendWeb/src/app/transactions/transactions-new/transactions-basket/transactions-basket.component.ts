import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Subscription } from 'rxjs';
import { Transaction } from 'src/app/_model/transaction';
import { AuthService } from 'src/app/_services/auth.service';
import { TransactionService } from 'src/app/_services/transaction.service';

@Component({
  selector: 'app-transactions-basket',
  templateUrl: './transactions-basket.component.html',
  styleUrls: ['./transactions-basket.component.css']
})
export class TransactionsBasketComponent implements OnInit {
  @Input()  transactionToBasket: Transaction[];
  @Output() transactionToBasketChange = new EventEmitter<Transaction[]>();
  @Output() mode = new EventEmitter();

  totalPaid = 0;

  // properties for pagination
  pageSize = 8;
  page = 1;

  constructor(
    private transactionService: TransactionService,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  onDeleteProductFromTransaction(index: number): void {
    // If product on specify transaction have more than 1 quantity
    if (this.transactionToBasket[index].quantity > 1) {
      // Decrease it by one
      this.transactionToBasket[index].quantity--;
      // Update paid with unit price for this product
      this.transactionToBasket[index].paid -= this.transactionToBasket[index].paid / (this.transactionToBasket[index].quantity+1);
      // And the same take off from total paid with unit price
      this.totalPaid -= this.transactionToBasket[index].paid  / (this.transactionToBasket[index].quantity);
    }
    // Otherwise
    else {
      // Update total paid and
      this.totalPaid -= this.transactionToBasket[index].paid;
      // delete transaction from local list
      this.transactionToBasket.splice(index, 1);
    }

    // if user delete last item then automatic basket will close
    this.checkTransactionInBasket(this.transactionToBasket);

  }

  public addTransactions(): Subscription {
    return this.transactionService.addUserTransactions(this.authService.decodedToken.nameid, this.transactionToBasket)
      .subscribe(
        // With success save transactions
        () => {
          // clear local data,
          console.log('czyszczenie');
          this.transactionToBasket = [];
          this.totalPaid = 0;
          this.checkTransactionInBasket(this.transactionToBasket);
        },

        // TODO: add small textbox with error message (duplicate name or too short name)
        error => console.log(error)
        );
  }

  checkTransactionInBasket(transaction: Transaction[]): void {
    this.transactionToBasketChange.emit(transaction);
    if (this.transactionToBasket.length === 0){
      this.mode.emit(false);
    }
  }

}
