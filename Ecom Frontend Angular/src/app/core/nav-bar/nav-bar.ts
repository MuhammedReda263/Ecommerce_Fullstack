import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basketService';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/Models/Basket';
import { IdentityService } from '../../identity/identity-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-bar',
  standalone: false,
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss',
})
export class NavBar implements OnInit {
  constructor(private _basketService: BasketService, private _auth: IdentityService, private _router: Router) { }
  count: Observable<IBasket>;
  role$: Observable<string | null>;
  isAuth$!: Observable<boolean>;
  

  ngOnInit(): void {
    this.isAuth$ = this._auth.isAuthenticated$;

    this.role$ = this._auth.role$;

    if (typeof window !== 'undefined') {

      const basketId = localStorage.getItem("basket_id");

      if (basketId) {
        this._basketService.getBasket(basketId).subscribe({
          next: (value) => {
            console.log("Basket loaded in NavBar:", value);
            this.count = this._basketService.basket$;
          }
        });
      }
    }
  }

  logOut() {
    this._auth.logout().subscribe({
      next: (value) => {
        this._auth.roleSource.next(null);
        this._auth.authState.next(false);
        this._router.navigate(['/account/login']);
      },
    });

  }
}
