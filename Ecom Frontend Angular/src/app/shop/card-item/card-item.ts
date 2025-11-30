import { Component, Input, input } from '@angular/core';
import { IProducts } from '../../shared/Models/Product';
import { BasketService } from '../../basket/basketService';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-card-item',
  standalone: false,
  templateUrl: './card-item.html',
  styleUrl: './card-item.scss',
})
export class CardItem {
@Input() product:IProducts
constructor(private _basketService:BasketService, private _toaster :ToastrService) {}
addToBasket(){
  this._basketService.addItemToBasket(this.product);
  this._toaster.success("Product added to card","Success")
}
}
