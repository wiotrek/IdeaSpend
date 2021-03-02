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
  totalPaid: number = 0;
  products: Product[] = [];
  transactionsToSave: Transaction[] = [];

  // region Constructor

  constructor(private productService: ProductService,
                      private transactionService: TransactionService,
                      private authService: AuthService) { }

   // endregion

  ngOnInit(): void {
    this.loadProducts();
  }

  // region API Request Methods

  public addTransactions() {
    return this.transactionService.addUserTransactions(this.authService.decodedToken.nameid, this.transactionsToSave)
      .subscribe(
        // With success save transactions
        () => {
          // clear local data,
          this.transactionsToSave = [];
          this.totalPaid = 0;
        },

        // TODO: add small textbox with error message (duplicate name or too short name)
        error => console.log(error) );
  }

  loadProducts(): void {
    if (this.authService.loggedIn()){
      this.productService.getUserProducts(this.authService.decodedToken.nameid)
        .subscribe((products: Product[]) => {this.products = products;})
    }
  }

  // endregion

  // region Local Request Methods

  // TODO: Too much for this method. Extract updating function to other
  // The transaction list of chosen products
  onAddProductToLocalList(index: number) {
    let transaction = new Transaction();
    // initialize new transaction
    transaction = this.transactionService.addProductToLocalList(this.products[index])

    // Check if just adding transaction isn't duplicate of exist local transaction list
    for (let i = 0; i < this.transactionsToSave.length; i++)
      // If exist then
      if (this.transactionsToSave[i].productNameFrom === transaction.productNameFrom) {
        // increase his quantity by 1
        this.transactionsToSave[i].quantity++;
        // update paid for this product as quantity * price
        this.transactionsToSave[i].paid = (this.transactionsToSave[i].quantity * this.products[index].price);
        // and of course update total paid for whole local transaction list
        this.totalPaid += this.products[index].price;
        return;
      }

      // Otherwise update total paid with price of really new transaction
    this.totalPaid += transaction.paid;
      // and put this transaction as new to local list
    this.transactionsToSave.push(transaction);
  }

  // Deleting by one specify transaction
  onDeleteProductFromTransaction(index: number) {
    // If product on specify transaction have more than 1 quantity
    if (this.transactionsToSave[index].quantity > 1) {
      // Decrease it by one
      this.transactionsToSave[index].quantity--;
      // Update paid with unit price for this product
      this.transactionsToSave[index].paid -= this.transactionsToSave[index].paid / (this.transactionsToSave[index].quantity+1);
      // And the same take off from total paid with unit price
      this.totalPaid -= this.transactionsToSave[index].paid  / (this.transactionsToSave[index].quantity);
    }
    // Otherwise
    else {
      // Update total paid and
      this.totalPaid -= this.transactionsToSave[index].paid;
      // delete transaction from local list
      this.transactionsToSave.splice(index, 1);
    }
  }

  // endregion

}
