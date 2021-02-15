import {Component, OnInit} from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {

  // The color text of the dropdown button
  activeColor: string;
  logoImage = '../assets/logo/Logo-mini.png';

  constructor(private router: Router) {}

  ngOnInit(): void {
    this.setColor();
  }

  // Setting color text for dropdown button of the navigation depends on current url
  //region Get/Set Color

  setColor(): void {
    if (this.router.url.includes('/transactions')) { this.activeColor = '#fff'; }
    else                                           { this.activeColor = '#6C707E'; }
  }

  getColor(): string {
    return this.activeColor;
  }

  //endregion

}
