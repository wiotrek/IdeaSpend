/* tslint:disable:no-trailing-whitespace */
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Catalog } from 'src/app/_model/catalog';
import { Product } from 'src/app/_model/product';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';
import { ProductService } from 'src/app/_services/product.service';

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

  UnitsValue: any = [ 'szt', 'kg', 'l', 'm' ];

  constructor(
    private authService: AuthService,
    private catalogService: CatalogService,
    private productService: ProductService,
    private router: Router
    ) {}

  ngOnInit(): void {
    this.loadCatalogs();
  }

  loadCatalogs(): void {
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
      this.product.catalogName = (document.getElementById('addProductCategory') as HTMLSelectElement).value;
      this.product.unit = (document.getElementById('addUnityName') as HTMLSelectElement).value;
      this.productToAdd.push(this.product);
      this.product = new Product();
    }
  }

  // drop element product of array products
  kickProduct(id: number): void {
    this.productToAdd.splice(id, 1);
  }

  saveProducts() {
    return this.productService.addUserProduct(this.authService.decodedToken.nameid, this.productToAdd)
    .subscribe(

      // With success save product
      () => {
      // clear products list, then goes to overview
        this.productToAdd = [];
        this.router.navigate(['/products/overview']);
      },

      // TODO: add small textbox with error message (duplicate name or too short name)
      error => console.log(error) );
  }

}
