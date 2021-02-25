/* tslint:disable */
import { Component, OnInit } from '@angular/core';
import {Router} from '@angular/router';
import { Catalog } from '../_model/catalog';
import { AuthService } from '../_services/auth.service';
import { CatalogService } from '../_services/catalog.service';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css']
})
export class ProductComponent implements OnInit {

  opacity: string;
  catalogs: Catalog[];

  constructor(private router: Router, private authService: AuthService, private catalogService: CatalogService) { }

  ngOnInit(): void {
     this.setOpacity();
  }

  loadCatalogs() {
    if (this.authService.loggedIn()){

      this.catalogService.getUserCatalogs(this.authService.decodedToken.nameid)
      .subscribe((catalog: Catalog[]) => {this.catalogs = catalog;})

    }
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
