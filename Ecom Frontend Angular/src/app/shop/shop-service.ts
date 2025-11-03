import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { IProducts } from '../shared/Models/Product';
import { IPagination } from '../shared/Models/Pagination';
import { ICategory } from '../shared/Models/Category';
import { ProductParams } from '../shared/Models/ProducrParams';

@Injectable({
  providedIn: 'root'
})
export class ShopService {
  constructor(private _http: HttpClient) { }

  Products: IProducts[];
  private baseUrl: string = "https://localhost:7029/api/";

  getProducts(productParams: ProductParams) {
    let params = new HttpParams()
    if (productParams.categoryId != null) {
      params = params.append('CategoryId', productParams.categoryId.toString())

    }
    if (productParams.SortingValue != null) {
      params = params.append('Sort', productParams.SortingValue)
    }
    if (productParams.searchVal != null) {
      params = params.append('Search', productParams.searchVal)
    }
    params = params.append('PageNumber', productParams.PageNumber)
    params = params.append('pageSize', productParams.pagesize)
    return this._http.get<IPagination>(this.baseUrl + "Products", { params })
  }

  getCategories() {
    return this._http.get<ICategory[]>(this.baseUrl + "Categories")
  }

}
