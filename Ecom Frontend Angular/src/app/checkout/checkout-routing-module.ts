import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Checkout } from './checkout/checkout';
import { Success } from './success/success';
import { AuthGuard } from '../core/guards/auth.guard';
import { Card } from './card/card';

const routes: Routes = [
  {path:'', component: Checkout, canActivate: [AuthGuard]},
  {path:'card',component:Card,canActivate:[AuthGuard]},
  {path:'success/:orderId', component:Success,canActivate: [AuthGuard]}
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CheckoutRoutingModule { }
