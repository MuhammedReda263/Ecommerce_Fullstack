import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Home } from './home/home';
import { ShopModule } from './shop/shop-module';
import { ShopComponent } from './shop/shop';

const routes: Routes = [
  {path: '', component: Home},
  {path: 'shop', component: ShopComponent},
  {path: '**', redirectTo: '', pathMatch: 'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
