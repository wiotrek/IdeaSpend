import { Component, OnInit } from '@angular/core';
import { Catalog } from 'src/app/_model/catalog';
import { Product } from 'src/app/_model/product';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css']
})
export class ProductAddComponent implements OnInit {

  // list of category for product
  productCategory: Catalog[];

  // model of product
  product: Product = new Product();

  // list of added product to save
  productToAdd: Product[] = [];

  UnitsValue: Array<string> = [ 'szt', 'kg', 'l', 'm' ];

  constructor(private authService: AuthService, private catalogService: CatalogService) {}

  ngOnInit(): void {
    this.loadCatalogs();
  }

  loadCatalogs() {
    if (this.authService.loggedIn()){

      this.catalogService.getUserCatalogs(this.authService.decodedToken.nameid)
      .subscribe((catalog: Catalog[]) => {this.productCategory = catalog;})
    }
  }

  /* 
  * Product is adding to local products list before user submit created list
  */
  addProduct(): void {
    if (this.authService.loggedIn()){

      this.product.catalog = (<HTMLSelectElement>document.getElementById('addProductCategory')).value;
      this.productToAdd.push(this.product);
      this.product = new Product();
    }
  }

  // drop element product of array products
  kickProduct(id: number): void {
    this.productToAdd.splice(id, 1);
  }

}
