import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-success',
  standalone: false,
  templateUrl: './success.html',
  styleUrl: './success.scss',
})
export class Success implements OnInit {
orderId:number=0
constructor(private _route : ActivatedRoute){}
  ngOnInit(): void {
    this._route.paramMap.subscribe(
      params=>{
        this.orderId=Number(params.get("orderId"))
      }
    )
  }

}
