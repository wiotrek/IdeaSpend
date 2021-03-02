import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_model/product';
import { Transaction } from 'src/app/_model/transaction';
import { AuthService } from 'src/app/_services/auth.service';
import { TransactionService } from 'src/app/_services/transaction.service';
import {ProductService} from '../../_services/product.service';

@Component({
  selector: 'app-transactions-new',
  templateUrl: './transactions-new.component.html',
  styleUrls: ['./transactions-new.component.css']
})
export class TransactionsNewComponent implements OnInit {
  products: Product[] = [];
  transactions: Transaction[] = [];

  constructor(private productService: ProductService,
                      private transactionService: TransactionService,
                      private authService: AuthService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    if (this.authService.loggedIn()){
      this.productService.getUserProducts(this.authService.decodedToken.nameid)
        .subscribe((products: Product[]) => {this.products = products;})
    }
  }

  // The transaction list of chosen products
  onAddProductToLocalList(index: number) {
    let transaction = new Transaction();
    transaction = this.transactionService.addProductToLocalList(this.products[index])
    this.transactions.push(transaction);
  }
}
