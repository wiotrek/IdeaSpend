/* tslint:disable */
import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  opacity: string;

  constructor(private router: Router) { }

  ngOnInit(): void {
     this.setOpacity();
  }

  // region Set/Get Opacity

  setOpacity(): void {
    if (this.router.url.includes('products')) {
      this.opacity = '100%';
    }
  }

  GetOpacityOverview(): string {
    if (this.router.url === '/products/overview') {
      return this.opacity;
    }
  }

  GetOpacityCategories(): string {
    if (this.router.url === '/products/categories') {
      return this.opacity;
    }
  }

  GetOpacityAdd(): string {
    if (this.router.url === '/products/add') {
      return this.opacity;
    }
  }

  // endregion

}
