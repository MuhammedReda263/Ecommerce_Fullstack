import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OrderService } from '../order-service';
import { IOrder } from '../../shared/Models/Order';

@Component({
  selector: 'app-order-item',
  standalone: false,
  templateUrl: './order-item.html',
  styleUrl: './order-item.scss',
})
export class OrderItem implements OnInit {
constructor(private _ordersService: OrderService, private _route: ActivatedRoute) { }
  orderId: number = 0
  order:IOrder 
  ngOnInit(): void {
    this._route.queryParams.subscribe(
      params => {
        this.orderId = params["id"]
      }
    ),
    console.log(this.orderId)
    this._ordersService.getOrderForUserById(this.orderId).subscribe({
      next: (value)=> {
        this.order=value;
        console.log(value);
      },
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
    case 'cancelled':
      return 'bg-danger';

    default:
      return 'bg-secondary';
  }
}

}
