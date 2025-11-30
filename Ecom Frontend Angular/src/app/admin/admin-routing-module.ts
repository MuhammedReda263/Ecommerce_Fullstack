import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { Products } from './products/products';
import { AddProduct } from './add-product/add-product';
import { EditProduct } from './edit-product/edit-product';
import { AuthGuard } from '../core/guards/auth.guard';
import { AdminGuard } from '../core/guards/admin.guard';

const routes: Routes = [
  {path:'' , component:Products ,canActivate: [AdminGuard]},
  {path:'addProduct' , component:AddProduct,canActivate: [AdminGuard]},
  {path:'editProduct' , component:EditProduct,canActivate: [AdminGuard]},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class AdminRoutingModule { }
