import { Component, OnInit } from '@angular/core';

import {AuthService} from '../_services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  model: any = {};

  constructor(public authService: AuthService) { }

  ngOnInit(): void {
  }

  //region Public Methods

  // login method invoke in login.component.html
  login(): void {
    this.authService.login(this.model)
      .subscribe(
        next =>  { console.log('Everything okay'); },
        error => { console.log('Nothing okay'); }
        );
  }

  //endregion

}
