import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop';
import { CardItem } from './card-item/card-item';
import { AppRoutingModule } from "../app-routing-module";
import { SharedModule } from '../shared/shared-module';
import { ProductDetails } from './product-details/product-details';
import { NgxImageZoomModule } from "ngx-image-zoom";




@NgModule({
  declarations: [
    ShopComponent,
    CardItem,
    ProductDetails
  ],
  imports: [
    CommonModule,
    AppRoutingModule,
    SharedModule,
    NgxImageZoomModule
],
  exports:[
    ShopComponent
  ]
})
export class ShopModule { }
