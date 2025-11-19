import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register';
import { ActiveComponemt } from './active-componemt/active-componemt';

const routes: Routes = [
  {path:'register', component: RegisterComponent},
  {path:'active', component: ActiveComponemt},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
