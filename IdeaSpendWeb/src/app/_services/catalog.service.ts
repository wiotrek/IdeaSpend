import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { Catalog } from "../_model/catalog";
import { BaseService } from "./base.service";

@Injectable()

export class CatalogService extends BaseService {

    constructor(private http: HttpClient) { 
        super(); 
    }

    getUserCatalogs(userId: number): Observable<Catalog[]> {
        return this.http.get<Catalog[]>(`${this.backend}/api/catalog/get/${userId}`);
      }

}