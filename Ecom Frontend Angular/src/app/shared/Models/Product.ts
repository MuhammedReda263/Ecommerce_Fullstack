export interface IProducts {
  id: number
  name: string
  description: string
  newPrice: number
  oldPrice: number
  photos: IPhoto[]
  categoryName: string
  rating: number
}

export interface IPhoto {
  imageName: string
  productId: number
}