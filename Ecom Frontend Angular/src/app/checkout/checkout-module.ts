import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CheckoutRoutingModule } from './checkout-routing-module';
import { Checkout } from './checkout/checkout';
import { Stepper } from './stepper/stepper';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatStepperModule} from '@angular/material/stepper';
import {MatButtonModule} from '@angular/material/button';
import { Address } from './address/address';
import { Delivery } from './delivery/delivery';
import {MatRadioModule} from '@angular/material/radio';
import { Payment } from './payment/payment';
import { Success } from './success/success';


@NgModule({
  declarations: [
    Checkout,
    Stepper,
    Address,
    Delivery,
    Payment,
    Success
  ],
  imports: [
    CommonModule,
    CheckoutRoutingModule,
    MatButtonModule,
    MatStepperModule,
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule
    
  ],
  exports:[
    Stepper,
    Address,
    Delivery,
    Payment
  ]
})
export class CheckoutModule { }
