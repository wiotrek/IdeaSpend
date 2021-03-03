import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Catalog } from '../_model/catalog';
import { BaseService } from './base.service';

@Injectable()

export class CatalogService extends BaseService {

    constructor(private http: HttpClient) {
        super();
    }

    addUserCatalog(userId: number, model: any): Observable<Catalog> {
        return this.http.post<Catalog>(`${this.backend}/api/catalog/add/${userId}`, model);
    }

    getUserCatalogs(userId: number): Observable<Catalog[]> {
        return this.http.get<Catalog[]>(`${this.backend}/api/catalog/get/${userId}`);
    }

    deleteUserCatalog(catalogId: number): Observable<any>{
        return this.http.post(`${this.backend}/api/catalog/del`, catalogId);
    }

}
