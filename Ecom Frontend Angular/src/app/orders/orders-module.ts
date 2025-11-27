import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { OrdersRoutingModule } from './orders-routing-module';
import { Order } from './order/order';
import { OrderItem } from './order-item/order-item';


@NgModule({
  declarations: [
    Order,
    OrderItem
  ],
  imports: [
    CommonModule,
    OrdersRoutingModule
  ]
})
export class OrdersModule { }
