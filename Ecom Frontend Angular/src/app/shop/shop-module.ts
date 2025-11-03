import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop';
import { CardItem } from './card-item/card-item';
import { AppRoutingModule } from "../app-routing-module";
import { SharedModule } from '../shared/shared-module';




@NgModule({
  declarations: [
    ShopComponent,
    CardItem
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    SharedModule
],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
