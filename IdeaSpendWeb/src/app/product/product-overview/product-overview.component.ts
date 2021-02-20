/* tslint:disable */
import { Component, OnInit } from '@angular/core';
import {Product} from '../../_model/product';
import { newArray } from '@angular/compiler/src/util';

@Component({
  selector: 'app-product-overview',
  templateUrl: './product-overview.component.html',
  styleUrls: ['./product-overview.component.css']
})
export class ProductOverviewComponent implements OnInit {

  products: Product[];

  constructor() {
  }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts(): void {
    this.products = new Array<Product>(5);

    this.products[0] = { catalog: 'Żywność', price: 6.15, productName: 'Masło optima', seller: 'Netto', unit: 'szt' }
    this.products[1] = { catalog: 'Subskrypcje', price: 52.00, productName: 'Netflix HD', seller: 'Netflix', unit: 'szt' }
    this.products[2] = { catalog: 'Urządzenia', price: 135, productName: 'Xiaomi Mi Stick TV', seller: 'Ceneo', unit: 'szt' }
    this.products[3] = { catalog: 'Rozrywka', price: 129.90, productName: 'On the Road Truck Simulation', seller: 'Ceneo', unit: 'szt' }
    this.products[4] = { catalog: 'AGD', price: 478, productName: 'Xiaomi SmartMi Evaporative Hu...', seller: 'Allegro', unit: 'szt' }
  }

}
