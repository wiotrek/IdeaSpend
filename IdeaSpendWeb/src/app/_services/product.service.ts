import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Product } from '../_model/product';
import { BaseService } from './base.service';

@Injectable()
export class ProductService extends BaseService {

  constructor(private http: HttpClient) {
    super();
  }

  getUserProducts(userId: number): Observable<Array<Product>> {
    return this.http.get<Array<Product>>(`${this.backend}/api/product/get/${userId}`);
  }

  addUserProduct(userId: number, model: any): Observable<Array<Product>> {
    return this.http.post<Array<Product>>(`${this.backend}/api/product/add/${userId}`, model);
  }

  deleteUserPrdocut(userId: number, productName: string): Observable<any>{
    return this.http.delete(`${this.backend}/api/product/del/${userId}/${productName}`);
  }

}
