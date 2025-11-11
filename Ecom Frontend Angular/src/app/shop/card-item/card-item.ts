import { Component, Input, input } from '@angular/core';
import { IProducts } from '../../shared/Models/Product';
import { BasketService } from '../../basket/basketService';

@Component({
  selector: 'app-card-item',
  standalone: false,
  templateUrl: './card-item.html',
  styleUrl: './card-item.scss',
})
export class CardItem {
@Input() product:IProducts
constructor(private _basketService:BasketService) {}
addToBasket(){
  this._basketService.addItemToBasket(this.product);
}
}
