import { Component, OnInit } from '@angular/core';
import { BasketService } from '../basketService';
import { IBasket, IBasketItem } from '../../shared/Models/Basket';

@Component({
  selector: 'app-basket',
  standalone: false,
  templateUrl: './basket.html',
  styleUrl: './basket.scss',
})
export class BasketComponent implements OnInit {
constructor(private _basketService : BasketService) { }
basket : IBasket
  ngOnInit(): void {
   this._basketService.basket$.subscribe({
    next:(value) => {
      this.basket = value;
      console.log("Basket loaded in BasketComponent:", value);
    }
   })
  }

  removeBasketItem(item:IBasketItem){
    console.log(item);
    this._basketService.removeItemFromBasket(item);
  }
  
  incrementQuantity(item:IBasketItem){
    console.log(item);
    this._basketService.incrementBasketItemQuantity(item);
    
  }

  decrementQuantity(item:IBasketItem){
    this._basketService.decrementBasketItemQuantity(item);
  }

}
