import { Injectable } from '@angular/core';
import {Transaction} from '../_model/transaction';
import {Product} from '../_model/product';
import {AuthService} from './auth.service';
import {Observable} from 'rxjs';
import {BaseService} from './base.service';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TransactionService extends BaseService {
  currentAmountBoughtProduct: number = 1;
  transaction: Transaction;

  constructor(private authService: AuthService, private http: HttpClient) { super(); }

  addUserTransactions(userId: number, model: any): Observable<Array<Transaction>> {
    return this.http.post<Array<Transaction>>(`${this.backend}/api/transaction/add/${userId}`, model);
  }

  getTransactions(userId: number): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.backend}/api/transaction/get/${userId}`)
  }

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
