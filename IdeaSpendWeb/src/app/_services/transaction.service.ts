import { Injectable } from '@angular/core';
import {Transaction} from '../_model/transaction';
import {Product} from '../_model/product';

@Injectable({
  providedIn: 'root'
})
export class TransactionService {
  currentAmountBoughtProduct: number = 1;

  constructor() { }

  // adding single product to transaction list
  addProductToLocalList(products: Product) :Transaction{
    let transaction = new Transaction();
    transaction.productNameFrom = products.productName + ' - ' + products.seller;
    transaction.quantity = this.currentAmountBoughtProduct;
    transaction.weights = 1;
    transaction.currency = 'PLN';
    transaction.transactionDate = new Date().toLocaleDateString();
    transaction.paid = transaction.quantity * products.price;

    return transaction;
  }
}
