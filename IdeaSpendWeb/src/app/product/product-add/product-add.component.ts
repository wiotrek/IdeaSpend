import { Component, OnInit } from '@angular/core';
import { Catalog } from 'src/app/_model/catalog';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';
import { ProductCatalogsComponent } from '../product-catalogs/product-catalogs.component';

@Component({
  selector: 'app-product-add',
  templateUrl: './product-add.component.html',
  styleUrls: ['./product-add.component.css']
})
export class ProductAddComponent implements OnInit {

  productCategory: Catalog[];

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

  UnitsValue: Array<string> = [
    "szt", "kg", "l", "m"
  ];
}
