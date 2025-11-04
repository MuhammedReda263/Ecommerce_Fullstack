import { Component, OnInit } from '@angular/core';
import { ShopService } from '../shop-service';
import { IProducts } from '../../shared/Models/Product';
import { ActivatedRoute } from '@angular/router';
import { NgxImageZoomModule } from 'ngx-image-zoom';

@Component({
  selector: 'app-product-details',
  standalone: false,
  templateUrl: './product-details.html',
  styleUrl: './product-details.scss'
})
export class ProductDetails implements OnInit {
  [x: string]: any;
  constructor(private _shopService: ShopService, private _route: ActivatedRoute) { }
  product: IProducts
  imageSrc: String
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
}