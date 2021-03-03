import {AfterContentChecked, Component, DoCheck, EventEmitter, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import { Catalog } from 'src/app/_model/catalog';
import { Product } from 'src/app/_model/product';
import { Transaction } from 'src/app/_model/transaction';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';
import { TransactionService } from 'src/app/_services/transaction.service';
import {ProductService} from '../../_services/product.service';

@Component({
  selector: 'app-transactions-new',
  templateUrl: './transactions-new.component.html',
  styleUrls: ['./transactions-new.component.css']
})
export class TransactionsNewComponent implements OnInit {
  // region Properties

  totalPaid: number = 0;
  selectedCatalog: string = '';
  products: Product[] = [];
  filteredProducts: Product[] = [];
  transactionsToSave: Transaction[] = [];
  catalogs: Catalog[] = [];

  // endregion

  // region Constructor

  constructor(private productService: ProductService,
                      private transactionService: TransactionService,
                      private catalogService: CatalogService,
                      private authService: AuthService) { }

   // endregion

  ngOnInit(): void {
    this.loadProducts();
    this.loadCatalogs();
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
        .subscribe((products: Product[]) => {
          this.products = products.slice();
          this.filteredProducts = products.slice();
        })
    }
  }

  loadCatalogs(): void {
    if (this.authService.loggedIn()){
      this.catalogService.getUserCatalogs(this.authService.decodedToken.nameid)
        .subscribe((catalogs: Catalog[]) => {this.catalogs = catalogs;})
    }
  }

  // endregion

  // region Local Request Methods

  onAddProductToLocalList(index: number) {
    let transaction = new Transaction();
    // initialize new transaction
    transaction = this.transactionService.addProductToLocalList(this.filteredProducts[index])

    // Check if just adding transaction isn't duplicate of exist local transaction list
    for (let i = 0; i < this.transactionsToSave.length; i++)
      // If exist then
      if (this.transactionsToSave[i].productNameFrom === transaction.productNameFrom) {
        // increase his quantity by 1
        this.transactionsToSave[i].quantity++;
        // update paid for this product as quantity * price
        this.transactionsToSave[i].paid = (this.transactionsToSave[i].quantity * this.filteredProducts[index].price);
        // and of course update total paid for whole local transaction list
        this.totalPaid += this.filteredProducts[index].price;
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

 // Display filtered products ba category to list
  filterProductByCatalogName(index: number) {
    this.selectedCatalog = this.transactionService.getSelectedCatalog(index, this.catalogs);
    // List of filtered products
    this.filteredProducts = [];

    // collect products which are assign to chosen catalog
    if (this.selectedCatalog !== 'Wybierz katalog')
      for (let i = 0; i < this.products.length; i++) {
        if (this.products[i].catalogName === this.selectedCatalog)
          this.filteredProducts.push(this.products[i]);
      }
    else
      this.filteredProducts = this.products;

  }

  // endregion

}
