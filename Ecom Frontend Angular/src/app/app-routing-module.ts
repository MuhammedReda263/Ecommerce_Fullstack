import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';
import { ShopModule } from './shop/shop-module';
import { ShopComponent } from './shop/shop';
import { ProductDetails } from './shop/product-details/product-details';

const routes: Routes = [
  {path: '', component: Home},
  {path: 'shop', component: ShopComponent},
  {path: 'product-details/:id', component: ProductDetails},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
