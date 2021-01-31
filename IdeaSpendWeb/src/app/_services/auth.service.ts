import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {map} from 'rxjs/operators';
import {Observable} from 'rxjs';

@Injectable()

// The service provides create as register and get of the create by login method personal information
// NOTE: After create service put this class in app.module.ts inside provides
export class AuthService {

  baseUrl = 'http://localhost:5000/api/auth/';

  constructor(private http: HttpClient) { }

  login(model: any): Observable<void>{
    return this.http.post(this.baseUrl + 'login', model)
      // Do something with response from server
      .pipe(map((response: any) => {
        const user = response;
        if (user){
          localStorage.setItem('token', user.token);
        }
      } ));
  }
}
