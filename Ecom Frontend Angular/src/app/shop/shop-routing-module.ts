import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ShopComponent } from './shop';
import { ProductDetails } from './product-details/product-details';

const routes: Routes = [
  {path: '', component: ShopComponent},
  {path: 'product-details/:id', component: ProductDetails},
]

@NgModule({
  declarations: [
    
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class ShopRoutingModule { }
