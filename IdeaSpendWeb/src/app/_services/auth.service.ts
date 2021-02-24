import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Router } from '@angular/router';
import { BaseService } from './base.service';

@Injectable()

// The service provides create as register and get of the create by login method personal information
// NOTE: After create service put this class in app.module.ts inside provides
export class AuthService extends BaseService {

  //region Public Members

  jwtHelper = new JwtHelperService();
  decodedToken;

  //endregion

  constructor(private router: Router, private http: HttpClient) {
    super();
  }

  // login received response from api request
  login(model: any): Observable<void>{

    // Do something with response from server
    return this.http.post(`${this.backend}/api/auth/login`, model)
      .pipe(map((response: any) => {

        const user = response;

        if (user){
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }

      } ));
  }

  register(model: any): Observable<object> {
    return this.http.post(`${this.backend}/api/auth/register`, model);
  }

  // Check if token is still active - true for active
  loggedIn(): boolean {
    const token = localStorage.getItem('token');
    return !this.jwtHelper.isTokenExpired(token);
  }

  logout(): void {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}
