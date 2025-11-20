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
  login (body:any){
    return this._http.post(this.baseURL + "Account/login",body)
  }

  active (body:any){
    return this._http.post(this.baseURL + "Account/active",body)
  }

  forgetPassword(body:any){
    return this._http.post(this.baseURL + "Account/forget-password", body);
  }

  resetPassword(body:any){
    return this._http.post(this.baseURL + "Account/reset-password", body);
  }
}
