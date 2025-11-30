import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { BehaviorSubject, tap } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class IdentityService {
  constructor(private _http: HttpClient) { }

  baseURL = environment.baseURL;

  roleSource = new BehaviorSubject<string | null>(null);
  role$ = this.roleSource.asObservable();

  authState = new BehaviorSubject<boolean>(false);
  isAuthenticated$ = this.authState.asObservable(); 

  register(body: any) {
    return this._http.post(this.baseURL + "Account/register", body)
  }
  login(body: any) {
    return this._http.post(this.baseURL + "Account/login", body)
  }

  active(body: any) {
    return this._http.post(this.baseURL + "Account/active", body)
  }

  forgetPassword(body: any) {
    return this._http.post(this.baseURL + "Account/forget-password", body);
  }

  resetPassword(body: any) {
    return this._http.post(this.baseURL + "Account/reset-password", body);
  }

  isAuth() {
    return this._http.get(this.baseURL + "Account/isAuth");
  }

  getRole() {
    return this._http.get<role>(this.baseURL + "Account/role");
  }

  loadRole() {
    this.getRole().subscribe({
      next: (value: any) => {
        // handle APIs that return either { role: 'admin' } or just 'admin'
        const role = value && (value.role ?? value);
        console.log('loadRole response:', value, '-> emitting role:', role);
        this.roleSource.next(role ?? null);
      },
      error: (err) => {
        console.error('loadRole error:', err);
        this.roleSource.next(null);
      }
    });
  }

  logout() {
    this.loadRole()
    return this._http.post(this.baseURL + "Account/logout", null)
  }


}
