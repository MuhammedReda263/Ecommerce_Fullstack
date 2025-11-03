import { Component, Input, input } from '@angular/core';
import { IProducts } from '../../shared/Models/Product';

@Component({
  selector: 'app-card-item',
  standalone: false,
  templateUrl: './card-item.html',
  styleUrl: './card-item.scss',
})
export class CardItem {
@Input() product:IProducts
}
