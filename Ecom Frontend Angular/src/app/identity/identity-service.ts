import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  constructor(private _http : HttpClient) { }
  baseURL = environment.baseURL;
  register (body:any){
    return this._http.post(this.baseURL + "Account/register",body)
  }

  active (body:any){
    return this._http.post(this.baseURL + "Account/active",body)
  }
}
