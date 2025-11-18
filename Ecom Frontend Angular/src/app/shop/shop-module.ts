import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ShopComponent } from './shop';
import { CardItem } from './card-item/card-item';
import { SharedModule } from '../shared/shared-module';
import { ProductDetails } from './product-details/product-details';
import { NgxImageZoomModule } from "ngx-image-zoom";
import { ShopRoutingModule } from './shop-routing-module';




@NgModule({
  declarations: [
    ShopComponent,
    CardItem,
    ProductDetails
  ],
  imports: [
    CommonModule,
    ShopRoutingModule,
    SharedModule,
    NgxImageZoomModule,
    
  ],
  exports: [

  ]
})
export class ShopModule { }
