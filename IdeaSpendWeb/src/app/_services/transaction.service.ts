import { Injectable } from '@angular/core';
import {Transaction} from '../_model/transaction';
import {Product} from '../_model/product';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  currentAmountBoughtProduct: number = 1;
  transaction: Transaction;

  constructor() { }

  // adding single product to transaction list (the list before submit to save)
  addProductToLocalList(products: Product) :Transaction{
    this.transaction = new Transaction();
    this.transaction.productNameFrom = products.productName + ' - ' + products.seller;
    this.transaction.quantity = this.currentAmountBoughtProduct;
    this.transaction.weights = 1;
    this.transaction.currency = 'PLN';
    this.transaction.transactionDate = new Date().toLocaleDateString();
    this.transaction.paid = this.transaction.quantity * products.price;

    return this.transaction;
  }

}
