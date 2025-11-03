import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { Pagination } from './pagination/pagination';



@NgModule({
  declarations: [
    Pagination
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot()
  ],
  exports:[
    PaginationModule,
    Pagination
  ]
})
export class SharedModule { }
