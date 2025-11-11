import { Component, OnInit } from '@angular/core';
import { BasketService } from '../../basket/basketService';
import { Observable } from 'rxjs';
import { IBasket } from '../../shared/Models/Basket';

@Component({
  selector: 'app-nav-bar',
  standalone: false,
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss',
})
export class NavBar implements OnInit {
  constructor(private _basketService : BasketService) { }
  count : Observable<IBasket> ;
  ngOnInit(): void {
    const basketId = localStorage.getItem("basket_id");
  this._basketService.getBasket(basketId).subscribe({
    next:(value) => {
      console.log("Basket loaded in NavBar:", value);
      this.count = this._basketService.basket$;
    }
  })
  }
 
}
