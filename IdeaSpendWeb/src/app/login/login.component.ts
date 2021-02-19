import { Component, OnInit } from '@angular/core';

import {AuthService} from '../_services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  logoImage = '../assets/logo/Logo.png';

  // User model arrived from api request
  userModel: any = {};

  constructor(public authService: AuthService, private router: Router) { }

  ngOnInit(): void {
  }

  //region Public Methods

  // login method invoke in login.component.html
  // TODO: change navigate to start page
  login(): void {
    this.authService.login(this.userModel)
      .subscribe(
        next =>  { console.log('Everything okay'); },
        error => { console.log('Nothing okay'); },
        () => { this.router.navigate(['/register'] );
        });
  }

  //endregion

}
