/* tslint:disable */
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-product-catalogs',
  templateUrl: './product-catalogs.component.html',
  styleUrls: ['./product-catalogs.component.css']
})
export class ProductCatalogsComponent implements OnInit {

  catalogs: Array<string>;

  constructor() { }

  ngOnInit(): void {
    this.loadCatalogs();


  }

  loadCatalogs(): void {
    this.catalogs = new Array<string>(9);

    for (let i = 0; i < this.catalogs.length; i++) {
      this.catalogs[i] = `Katalog ${i+1}`;
    }
  }
}
