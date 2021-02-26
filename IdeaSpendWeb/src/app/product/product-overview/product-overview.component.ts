/* tslint:disable */
import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';
import {Product} from '../../_model/product';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.css']
})
export class ProductOverviewComponent implements OnInit {

  products: Product[];

  constructor(private catalogService: CatalogService,
    private authService: AuthService) {
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    if (this.authService.loggedIn()){
      this.catalogService.getUserProducts(this.authService.decodedToken.nameid)
      .subscribe((products: Product[]) => {this.products = products;})
    }
  }

}
