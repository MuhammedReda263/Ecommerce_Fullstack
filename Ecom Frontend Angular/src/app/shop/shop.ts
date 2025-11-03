import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ShopService } from './shop-service';
import { IPagination } from '../shared/Models/Pagination';
import { IProducts } from '../shared/Models/Product';
import { ICategory } from '../shared/Models/Category';
import { ProductParams } from '../shared/Models/ProducrParams';
import { PageChangedEvent } from 'ngx-bootstrap/pagination';

@Component({
  selector: 'app-shop',
  standalone: false,
  templateUrl: './shop.html',
  styleUrl: './shop.scss',
})
export class ShopComponent implements OnInit {

  constructor(private _shopService: ShopService) { }
  products: IProducts[];
  categories: ICategory[];
  productParams = new ProductParams();
  totalCount : number;

  sortingArray = [
    { name: "Name", value: "Name" },
    { name: "Price: Low to High", value: "priceace" },
    { name: "Price: High to Low", value: "pricedce" },

  ]
  getProduct() {
    this._shopService.getProducts(this.productParams).subscribe({
      next: ((value: IPagination) => {
        this.products = value.data;
        this.totalCount = value.totalCount;
      })
    })
  }
  getCategory() {
    this._shopService.getCategories().subscribe({
      next: ((value: ICategory[]) => {
        this.categories = value;
        console.log(value)
      })
    })
  }
  ngOnInit(): void {
    this.getProduct();
    this.getCategory();
  }

  selectedId(categoryId: number) {
    this.productParams.categoryId = categoryId;
    this.getProduct();
  }
  selectedSortingValue(selectedSortingValue: Event) {
    this.productParams.SortingValue = (selectedSortingValue.target as HTMLSelectElement).value;
    this.getProduct();
  }

  onSearch(searchValue: string) {
    this.productParams.searchVal = searchValue;
    this.getProduct();
  }
  @ViewChild('search') searchInput!: ElementRef;
  @ViewChild('selected') selectedSorting!: ElementRef;
  Reset() {
    this.productParams.SortingValue = ''
    this.productParams.searchVal = ''
    this.productParams.categoryId = 0
    this.searchInput.nativeElement.value = '';
    this.selectedSorting.nativeElement.value = ""
    this.getProduct()
  }

  onChangePage(page: PageChangedEvent) {
    this.productParams.PageNumber = page.page;
    this.getProduct();
  }
}
