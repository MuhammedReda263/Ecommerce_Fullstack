import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, catchError, throwError } from 'rxjs';
import { Router } from '@angular/router';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {

  constructor(private router: Router) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    // Endpoints that should NOT trigger redirect on 401 (auth-check endpoints)
    const whitelistedEndpoints = [
      '/Account/isAuth',
      '/Account/role',
      '/Account/register',
      '/Account/login',
      '/Account/active',
      '/Account/forget-password',
      '/Account/reset-password'
    ];

    const isWhitelisted = whitelistedEndpoints.some(endpoint => req.url.includes(endpoint));

    return next.handle(req).pipe(
      catchError((error: HttpErrorResponse) => {
        // Only redirect to login on 401 if:
        // 1. Status is 401
        // 2. Request is NOT whitelisted (auth-check endpoints)
        // 3. Request is NOT a GET request to protected data (avoid redirect loops)
        if (error.status === 401 && !isWhitelisted) {
          this.router.navigate(['/account/login']);
        }

        return throwError(() => error);
      })
    );
  }

}
