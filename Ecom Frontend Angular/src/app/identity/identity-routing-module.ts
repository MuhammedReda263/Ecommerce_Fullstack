import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RegisterComponent } from './register/register';
import { ActiveComponemt } from './active-componemt/active-componemt';
import { LoginComponent } from './login-component/login-component';
import { ForgetPasswordComponent } from './forget-password-component/forget-password-component';
import { ResetPasswordComponent } from './reset-password-component/reset-password-component';

const routes: Routes = [
  {path:'register', component: RegisterComponent},
  {path:'login', component: LoginComponent},
  {path:'active', component: ActiveComponemt},
  {path:'foregetPassword', component: ForgetPasswordComponent},
  {path:'resetPassword', component: ResetPasswordComponent},

];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class IdentityRoutingModule { }
