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
  catalog: any = {};

  constructor(private catalogService: CatalogService,
              private authService: AuthService) { }

  ngOnInit(): void {
    this.loadCatalogs();
  }

  saveCatalog() {
    return this.catalogService.addUserCatalog(this.authService.decodedToken.nameid, this.catalog)
    .subscribe( 

      // With success save catalog..
      () => {
      // reload catalog list
      this.loadCatalogs();
      // and clear input field
      this.catalog.catalogName = '';
      },

      // TODO: add small textbox with error message (duplicate name or too short name)
      error => console.log('Nie dodano katalogu') );

  }

  // TODO: Move common methods to base component
  loadCatalogs() {
    if (this.authService.loggedIn()){

      this.catalogService.getUserCatalogs(this.authService.decodedToken.nameid)
      .subscribe((catalog: Catalog[]) => {this.catalogs = catalog;})

    }
  }

}
