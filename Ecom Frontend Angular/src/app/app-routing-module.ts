import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';


const routes: Routes = [
  {path: '', component: Home},
  { path: 'shop', loadChildren: () => import('./shop/shop-module').then(m => m.ShopModule) },
  { path: 'account', loadChildren: () => import('./identity/identity-module').then(m => m.IdentityModule) },
  { path: 'basket', loadChildren: () => import('./basket/basket-module').then(m => m.BasketModule) },
  { path: 'checkout', loadChildren: () => import('./checkout/checkout-module').then(m => m.CheckoutModule) },
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
