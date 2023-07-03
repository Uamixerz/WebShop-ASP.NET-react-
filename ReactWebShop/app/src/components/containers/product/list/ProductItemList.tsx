import { FC, useEffect } from "react";
import {IProductGetItem} from "./types";
import { APP_ENV } from "../../../../env";
interface IProductListItem {
    Product: IProductGetItem;
}

const ProductItemList: FC<IProductListItem> = ({ Product }) => {
    


    return (
        <>
            <div className="col mb-5">
                <div className="card h-100">

                    <img className="card-img-top" src={`${APP_ENV.BASE_URL}uploads/300_` + Product.images[0].name} alt="..."></img>

                    <div className="card-body p-4">
                        <div className="text-center">

                            <h5 className="fw-bolder">{Product.name}</h5>
                            <h6>{Product.description}</h6>
                            Ціна: {Product.price} грн
                           <h5>Категорія: {Product.categoryName}</h5>
                           <h5>[ID category: {Product.categoryId}]</h5>
                        </div>
                    </div>

                    <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                        <div className="text-center"><a className="btn btn-outline-dark mt-auto" href="#">Кнопка</a></div>
                    </div>
                </div>
            </div>

        </>
    )
}
export default ProductItemList;