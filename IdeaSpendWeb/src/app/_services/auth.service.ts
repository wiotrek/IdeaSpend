import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { map } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { JwtHelperService } from '@auth0/angular-jwt';

@Injectable()

// The service provides create as register and get of the create by login method personal information
// NOTE: After create service put this class in app.module.ts inside provides
export class AuthService {

  //region Public Members

  baseUrl = 'http://localhost:5000/api/auth/';
  jwtHelper = new JwtHelperService();
  decodedToken;

  //endregion

  constructor(private http: HttpClient) { }

  // login received response from api request
  login(model: any): Observable<void>{
    return this.http.post(this.baseUrl + 'login', model)
      .pipe(map((response: any) => {

        const user = response;

        if (user){
          localStorage.setItem('token', user.token);
          this.decodedToken = this.jwtHelper.decodeToken(user.token);
        }

      } ));
  }
}
