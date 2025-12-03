import { Component } from '@angular/core';
import { loadStripe } from '@stripe/stripe-js';
import { BasketService } from '../../basket/basketService';
import { OrderService } from '../../orders/order-service';
import { Payment } from '../payment/payment';
import { CheckoutService } from '../checkout-service';
import { ToastrService } from 'ngx-toastr';
import { Router } from '@angular/router';


@Component({
  selector: 'app-card',
  standalone: false,
  templateUrl: './card.html',
  styleUrl: './card.scss',
})
export class Card {

  constructor(private _basketService:BasketService,private _checkoutService : CheckoutService,private _toaster:ToastrService,private _router : Router){}

  stripe: any;
  cardElement: any;

  async ngAfterViewInit() {
    this.stripe = await loadStripe('pk_test_51RVElJCjyjrIVN0hwsYjs99yOChhpUYM1RPkzKXIb7iNVf6M5jeYx3zz7tf8L05q04ERzh5kizdAvfdOBHyS0YYL00wKdUXjD6');

    const elements = this.stripe.elements();
    this.cardElement = elements.create('card');
    this.cardElement.mount('#card-element');
  }


  async submitOrder() {

    const basket = this._basketService.getCurrentBasketValue();
    console.log("Client Secret:", basket.clientSecret);

    const result = await this.stripe.confirmCardPayment(basket.clientSecret, {
      payment_method: {
        card: this.cardElement
      }
    });

    if (result.error) {
      console.log("Payment error: ", result.error.message);
      this._toaster.error("Somthing Went Error", "Error");
    } else {
      if (result.paymentIntent.status === 'succeeded') {
        console.log("Payment succeeded!");
       this._checkoutService.createOrder().subscribe({
       next: (value) => {
        this._basketService.deleteBasketFE();
        this._toaster.success("Order Created Succefully", "Sucess");
        this._router.navigate(['/checkout/success',value.id])

      }
    })
      }
    }
  }

}
