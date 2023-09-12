export interface IProductCreate {
    name: string,
    price: number,
    oldPrice: number | undefined,
    description: string,
    categoryId: number,
    imagesId: number[]
}