import { Component, OnInit } from '@angular/core';
import { Product } from 'src/app/_model/product';
import { AuthService } from 'src/app/_services/auth.service';
import {ProductService} from '../../_services/product.service';

@Component({
  selector: 'app-transactions-new',
  templateUrl: './transactions-new.component.html',
  styleUrls: ['./transactions-new.component.css']
})
export class TransactionsNewComponent implements OnInit {
  products: Product[];

  constructor(private productService: ProductService,
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

}
