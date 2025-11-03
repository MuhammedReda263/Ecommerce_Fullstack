import { Component, EventEmitter, Input, Output } from '@angular/core';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-pagination',
  standalone: false,
  templateUrl: './pagination.html',
  styleUrl: './pagination.scss',
})
export class Pagination {
@Input() totalCount:number
@Input() itemsPerPage:number=3

@Output() PageChanged = new EventEmitter();

onChangePage(event:PageChangedEvent){
  this.PageChanged.emit(event);
}

}
