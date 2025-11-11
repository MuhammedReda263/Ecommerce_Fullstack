import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { BasketRoutingModule } from './basket-routing-module';
import { BasketComponent } from './basket/basket';
import { RouterModule } from '@angular/router';


@NgModule({
  declarations: [
    BasketComponent
  ],
  imports: [
    CommonModule,
    BasketRoutingModule,
    RouterModule
    
  ]
})
export class BasketModule { }
