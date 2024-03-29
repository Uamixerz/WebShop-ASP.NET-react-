import { useEffect, useState } from "react";
import http from "../../../../http";
import { IProductGetItem } from "./types";
import ProductCard from "../model/ProductCard";



const ProductListPage = () => {
    const [products, setProducts] = useState<IProductGetItem[]>();

    useEffect(() => {

        http.get<IProductGetItem[]>(`api/Product/list`).then(resp => {
            // console.log("axios result", resp);
            setProducts(resp.data);
        }).catch(bad => {
            console.log("bad request", bad)
        }
        );

    },[]);

    return (
        <>
            <h1 className={"text-center"}>Список продуктів</h1>

            <section className="py-5">
                <div className="container px-4 px-lg-5 mt-5">
                    <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                        {products?.map((item) => (<ProductCard product={item}></ProductCard>))}
                    </div>
                </div>
            </section>
        </>
    )
}
export default ProductListPage;