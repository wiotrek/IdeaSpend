/* tslint:disable */
import { Component, OnInit } from '@angular/core';
import { Catalog } from 'src/app/_model/catalog';
import { AuthService } from 'src/app/_services/auth.service';
import { CatalogService } from 'src/app/_services/catalog.service';

@Component({
  selector: 'app-product-catalogs',
  templateUrl: './product-catalogs.component.html',
  styleUrls: ['./product-catalogs.component.css']
})
export class ProductCatalogsComponent implements OnInit {

  catalogs: Catalog[];

  constructor(private catalogService: CatalogService, 
              private authService: AuthService) { }

  ngOnInit(): void {
    this.loadCatalogs();
  }

  loadCatalogs() {
    if (this.authService.decodedToken){

      this.catalogService.getUserCatalogs(this.authService.decodedToken.nameid)
      .subscribe((catalog: Catalog[]) => {this.catalogs = catalog;})

    }
  }

}
