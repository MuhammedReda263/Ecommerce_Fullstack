import { Component, Input, input } from '@angular/core';
import { BasketService } from '../../basket/basketService';
import { ICreateOrder } from '../../shared/Models/Order';
import { FormGroup } from '@angular/forms';
import { CheckoutService } from '../checkout-service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';

@Component({
  selector: 'app-payment',
  standalone: false,
  templateUrl: './payment.html',
  styleUrl: './payment.scss',
})
export class Payment {
  constructor(private _basketService: BasketService, 
    private _checkoutService: CheckoutService,
     private _toaster: ToastrService,
     private _router : Router
    ) { }
  order: ICreateOrder
  @Input() delivery: FormGroup
  @Input() shippingAddress: FormGroup

  createOrder() {
    var basket = this._basketService.getCurrentBasketValue();
    this._checkoutService.order = {
      deliveryMethodId: this.delivery.value.delivery,
      basketId: basket.id,
      shipAddress: this.shippingAddress.value
    }

  }
}
