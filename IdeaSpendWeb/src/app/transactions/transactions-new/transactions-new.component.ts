import {Component, EventEmitter, OnInit, Output} from '@angular/core';
import { Subscription } from 'rxjs';
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
  basketMode = false;
  totalPaid = 0;
  selectedCatalog = '';
  products: Product[] = [];
  @Output() transactionsToSave: Transaction[] = [];
  catalogs: Catalog[] = [];

  // properties for pagination

  pageSize = 8;
  page = 1;

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

  loadProducts(): void {
    if (this.authService.loggedIn()){
      this.productService.getUserProducts(this.authService.decodedToken.nameid)
        .subscribe((products: Product[]) => {
          this.products = products;
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

  onAddProductToLocalList(index: number): void {
    let transaction = new Transaction();
    // initialize new transaction
    transaction = this.transactionService.addProductToLocalList(this.products[index]);

    // Check if just adding transaction isn't duplicate of exist local transaction list
    // tslint:disable-next-line: prefer-for-of
    for (let i = 0; i < this.transactionsToSave.length; i++){
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
    }

      // Otherwise update total paid with price of really new transaction
    this.totalPaid += transaction.paid;
      // and put this transaction as new to local list
    this.transactionsToSave.push(transaction);
  }

 // Display filtered products ba category to list
  filterProductByCatalogName(index: number): void {
    this.selectedCatalog = this.transactionService.getSelectedCatalog(index, this.catalogs);

    // If category is selected then
    if (this.selectedCatalog !== 'Wybierz katalog'){

      // fill list with products which have this category

      this.productService.getUserProducts(this.authService.decodedToken.nameid)
      .subscribe((products: Product[]) => {
        this.products = products.filter(c => c.catalogName === this.selectedCatalog);
      });

    }else{

      // otherwise load all products
      this.loadProducts();
    }

  }

  basketToggle(): void {
    this.basketMode = !this.basketMode;
  }

  cancelBasketMode(event: boolean): void {
    this.basketMode = event;
  }

  // endregion

}
