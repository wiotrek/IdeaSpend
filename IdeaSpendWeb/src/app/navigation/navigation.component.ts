/* tslint:disable */
import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';
import {AuthService} from '../_services/auth.service';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  // The color text of the dropdown button
  activeColor: string;
  logoImage = '../assets/logo/Logo-mini.png';

  constructor(private router: Router, public authService: AuthService) {}

  ngOnInit(): void {
    this.setColor();
  }

  // Setting color text for dropdown button of the navigation depends on current url
  //region Get/Set Color

  setColor(): void {

    switch ( this.router.url ) {

      case '/products/overview':
        this.activeColor = '#fff';
        break;

      case '/products/categories':
        this.activeColor = '#fff';
        break;

      case '/products/add':
        this.activeColor = '#fff';
        break;

      case '/transactions/new':
        this.activeColor = '#fff';
        break;

      case '/transactions/overview':
        this.activeColor = '#fff';
        break;

    }

  }

  getTransactionColor(): string {
    if (this.router.url.includes('/transactions')) {
      return this.activeColor;
    }
  }

  getProductColor(): string {
    if (this.router.url.includes('/products')) {
      return this.activeColor;
    }
  }

  //endregion

}
