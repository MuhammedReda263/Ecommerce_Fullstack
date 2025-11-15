import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop-service';
import { IProducts } from '../../shared/Models/Product';
import { ActivatedRoute } from '@angular/router';
import { NgxImageZoomModule } from 'ngx-image-zoom';
import { ToastrService } from 'ngx-toastr';
import { BasketService } from '../../basket/basketService';

@Component({
  selector: 'app-product-details',
  standalone: false,
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss'
})
export class ProductDetails implements OnInit {
  [x: string]: any;
  constructor(private _shopService: ShopService, 
    private _route: ActivatedRoute,
    private _toster : ToastrService,
    private _basketService : BasketService
  ) { }
  product: IProducts
  imageSrc: String
  quantity: number =1
  ngOnInit(): void {
    this._shopService.getProductById(parseInt(this._route.snapshot.paramMap.get('id'))).subscribe({
      next: ((value: IProducts) => {
        this.product = value;
        this.imageSrc = value.photos[0].imageName;
      })
    })
  }
  ReplaceImg(imageName: string) {
    this.imageSrc = imageName;
  }

 incrementQuantity(){
  if (this.quantity<10){
    this.quantity++
    this._toster.success("Quantity increased","Success")
  }else{
    this._toster.info("Maximum quantity is 10","Info")
  }
 }
 decrementQuantity(){
  if (this.quantity>1){
    this.quantity--
    this._toster.success("Quantity decreased","Success")
  }else{
    this._toster.error("You can't decrement less than 1 item","Error")
  }
 }

 addToBasket (){
  this._basketService.addItemToBasket(this.product,this.quantity);
  this._toster.success("Product has added to nasket successfully","Success")
 }
 
 calculateDiscount (newPrice:number,oldPrice:number) : number {
return parseFloat(Math.round(((oldPrice - newPrice) / oldPrice) * 100).toFixed(1)) ;
 }

}