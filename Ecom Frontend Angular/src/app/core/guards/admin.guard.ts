import { Injectable } from '@angular/core';
import { CanActivate, Router } from '@angular/router';
import { catchError, map, of } from 'rxjs';
import { IdentityService } from '../../identity/identity-service';

@Injectable({
  providedIn: 'root'
})
export class AdminGuard implements CanActivate {

  constructor(private auth: IdentityService, private router: Router) {}

  canActivate() {
    return this.auth.getRole().pipe(
      map(res => {
        console.log(res);

        if (res.role === 'admin') {
          return true;
        }

        this.router.navigate(['/pages/unauthorized']);
        return false;
      }),
      catchError(() => {
        this.router.navigate(['/account/login']);
        return of(false);
      })
    );
  }
}

