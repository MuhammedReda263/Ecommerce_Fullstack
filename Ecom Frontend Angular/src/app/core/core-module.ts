import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NavBar } from './nav-bar/nav-bar';



@NgModule({
  declarations: [
    NavBar
  ],
  imports: [
    CommonModule
  ],
  exports:[
    NavBar
  ]
})
export class CoreModule { }
