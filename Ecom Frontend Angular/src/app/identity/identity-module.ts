import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { IdentityRoutingModule } from './identity-routing-module';
import { RegisterComponent } from './register/register';
import { ReactiveFormsModule } from '@angular/forms';
import { ActiveComponemt } from './active-componemt/active-componemt';
import { LoginComponent } from './login-component/login-component';
import { ForgetPasswordComponent } from './forget-password-component/forget-password-component';
import { ResetPasswordComponent } from './reset-password-component/reset-password-component';


@NgModule({
  declarations: [
    RegisterComponent,
    ActiveComponemt,
    LoginComponent,
    ForgetPasswordComponent,
    ResetPasswordComponent
  ],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    ReactiveFormsModule,
    
  ]
})
export class IdentityModule { }
