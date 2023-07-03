import { IUploadImageResult } from "../types";

export interface IProductGetItem{
    name: string,
    price: number,
    description: string,
    categoryId: number,
    categoryName:string,
    images: IUploadImageResult[]
}