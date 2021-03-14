import { Injectable } from '@angular/core';
import {Transaction} from '../_model/transaction';
import {Product} from '../_model/product';
import {AuthService} from './auth.service';
import {Observable} from 'rxjs';
import {BaseService} from './base.service';
import {HttpClient} from '@angular/common/http';
import {Catalog} from '../_model/catalog';
import {DatePipe} from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class TransactionService extends BaseService {

  currentAmountBoughtProduct: number = 1;
  transaction: Transaction;

  constructor(private authService: AuthService,
              private http: HttpClient,
              private datePipe: DatePipe) { super(); }

  addUserTransactions(userId: number, model: any): Observable<Array<Transaction>> {
    return this.http.post<Array<Transaction>>(`${this.backend}/api/transaction/add/${userId}`, model);
  }

  getTransactions(userId: number, date: string): Observable<Transaction[]> {
    // If date is selected then load by date
    if (date !== undefined)
      return this.http.get<Transaction[]>(`${this.backend}/api/transaction/get/${userId}/date=${date}`);
    // otherwise load transactions on date with registered transactions
    else
      return this.http.get<Transaction[]>(`${this.backend}/api/transaction/get/${userId}`);
  }

  getLast5Transactions(userId: number): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.backend}/api/transaction/get/last/${userId}`)
  }

  getTransactionsBySeller(userId: number, seller: string): Observable<Transaction[]> {
    return this.http.get<Transaction[]>(`${this.backend}/api/transaction/get/${userId}/${seller}`)
  }

  // adding single product to transaction list (the list before submit to save)
  addProductToLocalList(products: Product) :Transaction{
    this.transaction = new Transaction();
    this.transaction.productNameFrom = products.productName + ' - ' + products.seller;
    this.transaction.quantity = this.currentAmountBoughtProduct;
    this.transaction.weights = 1;
    this.transaction.currency = 'PLN';
    this.transaction.transactionDate = this.datePipe.transform(new Date(), 'yyyy-MM-dd');
    this.transaction.paid = this.transaction.quantity * products.price;

    return this.transaction;
  }

  // Return catalog name from dropdown list
  getSelectedCatalog(index: number, catalog: Catalog[]): string {
    let catalogName: string;

    if (index === -1)
      catalogName = 'Wybierz katalog';
    else
      catalogName = catalog[index].catalogName;

    return catalogName;
  }

}
