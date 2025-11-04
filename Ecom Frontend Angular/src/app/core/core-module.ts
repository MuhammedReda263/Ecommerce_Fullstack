import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBar } from './nav-bar/nav-bar';
import { RouterLink, RouterModule } from '@angular/router';



@NgModule({
  declarations: [
    NavBar
  ],
  imports: [
    CommonModule,
    RouterModule
  ],
  exports:[
    NavBar
  ]
})
export class CoreModule { }
