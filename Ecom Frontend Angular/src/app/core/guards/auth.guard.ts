import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { catchError, map, of } from 'rxjs';
import { IdentityService } from '../../identity/identity-service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private auth: IdentityService, private router: Router) {}

  canActivate() {
    return this.auth.isAuth().pipe(
      map(() => true),
      catchError(() => {
        this.router.navigate(['/account/login']);
        return of(false);
      })
    );
  }
}
