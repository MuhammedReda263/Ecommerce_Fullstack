import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, tap } from 'rxjs';
import { BasketClass, IBasket, IBasketItem } from '../shared/Models/Basket';
import { IProducts } from '../shared/Models/Product';

@Injectable({
  providedIn: 'root'
})
export class BasketService {

  constructor(private _http: HttpClient) { }
  baseUrl: string = "https://localhost:7029/api/";
  private basketSource = new BehaviorSubject<IBasket>(null); // save last value emitted of IBasket
  basket$ = this.basketSource.asObservable(); // observable to be used in components

  private basketSourceTotal = new BehaviorSubject<IBasketTotal>(null);
  basketTotal$ = this.basketSourceTotal.asObservable();

  calculateTotals() {
    const basket = this.getCurrentBasketValue();
    const shipping = 20; 
    const subTotal = basket.basketItems.reduce((a, b) => (b.price * b.quantity) + a, 0);
    const total = subTotal + shipping;
    this.basketSourceTotal.next({shipping, total, subTotal});
  }

  getBasket(id: string) {
    return this._http.get<IBasket>(this.baseUrl + "Basket/" + id).pipe(
      tap({
        next: (basket: IBasket) => {
          this.basketSource.next(basket);
          console.log(basket);
          this.calculateTotals() ;
          return basket; 
         
        },
        error: (err) => console.error('Error loading basket:', err)
      })
    )
  }

  setBasket(Basket: IBasket) {
    return this._http.post(this.baseUrl + "Basket", Basket).subscribe({
      next: (basket: IBasket) => {
        this.basketSource.next(basket);
        console.log(basket);
        this.calculateTotals() ;
      },
      error: (err) => console.error('Error creating basket:', err)
    })
  }

  getCurrentBasketValue() {
    return this.basketSource.value;
  }

  addItemToBasket(product: IProducts, quantity: number = 1) {
    const itemToAdd: IBasketItem = this.mapProductItemToBasketItem(product, quantity);
    let basket = this.getCurrentBasketValue() 
    if (basket == null){
      basket = this.CreateBasket();
    }
    basket.basketItems = this.addOrUpdateItem(basket.basketItems, itemToAdd, quantity);
    this.setBasket(basket);

  }
  addOrUpdateItem(BasketItems: IBasketItem[], itemToAdd: IBasketItem, quantity: number): IBasketItem[] {
    const index = BasketItems.findIndex(i => i.id === itemToAdd.id);
    if (index === -1) {
      itemToAdd.quantity = quantity;
      BasketItems.push(itemToAdd);
    } else {
      BasketItems[index].quantity += quantity;
    }
    return BasketItems;
  }
  private CreateBasket(): IBasket {
    const basket = new BasketClass();
    localStorage.setItem('basket_id', basket.id);
    return basket;

  }
  mapProductItemToBasketItem(product: IProducts, quantity: number): IBasketItem {
    return {
      id: product.id,
      name: product.name,
      image: product.photos[0]?.imageName || '',
      quantity: quantity,
      price: product.newPrice,
      category: product.categoryName
    }
  }

  incrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(x => x.id === item.id);
    basket.basketItems[foundItemIndex].quantity++;
    this.setBasket(basket);
  }

  decrementBasketItemQuantity(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    const foundItemIndex = basket.basketItems.findIndex(x => x.id === item.id);
    if (basket.basketItems[foundItemIndex].quantity > 1) {
      basket.basketItems[foundItemIndex].quantity--;
      this.setBasket(basket);
    } else {
      this.removeItemFromBasket(item);
    }

}
  removeItemFromBasket(item: IBasketItem) {
    const basket = this.getCurrentBasketValue();
    if (basket.basketItems.some(x => x.id === item.id)) {
      basket.basketItems = basket.basketItems.filter(i => i.id !== item.id);
      if (basket.basketItems.length > 0) {
        this.setBasket(basket);
      } else {
        this.deleteBasket(basket);
      }
    }
  }
  deleteBasket(basket: IBasket) {
    return this._http.delete(this.baseUrl + "Basket/" + basket.id).subscribe({
      next: () => {
        this.basketSource.next(null);
        localStorage.removeItem('basket_id');
      },
      error: (err) => console.error('Error deleting basket:', err)
    })
  }
}

export interface IBasketTotal {
  shipping:number,
  subTotal:number,
  total:number
}