import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Transaction } from 'src/app/_model/transaction';

@Component({
  selector: 'app-transactions-basket',
  templateUrl: './transactions-basket.component.html',
  styleUrls: ['./transactions-basket.component.css']
})
export class TransactionsBasketComponent implements OnInit {
  @Input()  transactionToBasket: Transaction[];

  totalPaid = 0;

  constructor() { }

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
  }

}
