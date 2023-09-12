import { IUploadImageResult } from "../types";

export interface IProductGetItem{
    id: number,
    name: string,
    price: number,
    description: string,
    oldPrice: number | undefined,
    categoryId: number,
    categoryName:string,
    images: IUploadImageResult[]
}