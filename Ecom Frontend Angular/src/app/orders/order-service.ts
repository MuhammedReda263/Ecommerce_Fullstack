import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { IOrder } from '../shared/Models/Order';

@Injectable({
  providedIn: 'root'
})
export class OrderService {
  constructor(private _http : HttpClient) {}
  baseURL = environment.baseURL
  getOrderForUserById (id:number) {
   return this._http.get<IOrder>(this.baseURL+"Orders/"+id)
  }
  getOrdersForUser () {
   return this._http.get<IOrder[]>(this.baseURL+"Orders");
  }
}
