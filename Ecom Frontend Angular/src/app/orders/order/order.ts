import { Component, OnInit } from '@angular/core';
import { OrderService } from '../order-service';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from '../../shared/Models/Order';

@Component({
  selector: 'app-order',
  standalone: false,
  templateUrl: './order.html',
  styleUrl: './order.scss',
})
export class Order implements OnInit {
  constructor(private _orderService:OrderService){}
  orders:IOrder[] = []
  ngOnInit(): void {
    this._orderService.getOrdersForUser().subscribe({
      next:(value)=>{
         this.orders = value;
      }
    })
  }
 
 getStatusClass(status: string) {
  switch (status.toLowerCase()) {
    case 'pending':
      return 'bg-warning text-dark';
    case 'paymentreceived':
    case 'paid':
      return 'bg-primary';
    case 'shipped':
      return 'bg-info text-dark';
    case 'delivered':
      return 'bg-success';
    case 'canceled':
      return 'bg-danger';
    default:
      return 'bg-secondary';
  }
}

}
