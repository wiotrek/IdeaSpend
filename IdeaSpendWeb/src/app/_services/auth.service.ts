import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
<<<<<<< HEAD
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';
import { environment } from 'src/environments/environment';
=======
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';
import {Router} from '@angular/router';
>>>>>>> 35f87c2f43ac74153ba46e694c21fe7bb26ebc41

@Injectable()

// The service provides create as register and get of the create by login method personal information
// NOTE: After create service put this class in app.module.ts inside provides
export class AuthService {

<<<<<<< HEAD
  private backend =  environment.apiUrl;
=======
  //region Public Members

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken;

  //endregion
>>>>>>> 35f87c2f43ac74153ba46e694c21fe7bb26ebc41

  constructor(private router: Router, private http: HttpClient) { }

  // login received response from api request
  login(model: any): Observable<void>{
<<<<<<< HEAD
    return this.http.post(`${this.backend}/api/auth/login`, model)
      // Do something with response from server
=======
    return this.http.post(this.baseUrl + 'login', model)
>>>>>>> 35f87c2f43ac74153ba46e694c21fe7bb26ebc41
      .pipe(map((response: any) => {

        const user = response;

        if (user){
<<<<<<< HEAD
          localStorage.setItem(`${this.backend}/api/auth/token`, user.token);
=======
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
>>>>>>> 35f87c2f43ac74153ba46e694c21fe7bb26ebc41
        }

      } ));
  }

  register(model: any): Observable<object> {
    return this.http.post(this.baseUrl + 'register', model);
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
