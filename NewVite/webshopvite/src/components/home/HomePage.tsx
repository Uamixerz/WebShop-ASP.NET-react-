
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.min';

import http from '../../http';
import { useEffect, useState } from 'react';


import ProductCard from '../containers/product/model/ProductCard';
import { IProductGetItem } from '../containers/product/list/types';

const HomePage = () => {

    // const navigator = useNavigate();
    // const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    // const isAdmin = user?.roles === "Admin";

    // const dispatch = useDispatch();

    const [products, setProducts] = useState<IProductGetItem[]>();

    // const logout = (e: any) => {
    //     e.preventDefault();
    //     localStorage.removeItem("token");
    //     http.defaults.headers.common["Authorization"] = ``;
    //     dispatch({ type: AuthUserActionType.LOGOUT_USER });
    //     navigator('/');
    // };


    useEffect(() => {

        http.get<IProductGetItem[]>(`api/Product/homePageList`).then(resp => {
            // console.log("axios result", resp);
            setProducts(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    }, []);



    const dataProductsView = products?.map(product =>
        <ProductCard key={product.id} product={product}></ProductCard>
    );


    return (
        <>
            <header className="bg-primary py-5">
                <div className="container px-4 px-lg-5 my-5">
                    <div className="text-center text-white">
                        <h1 className="display-4 fw-bolder">Український</h1>
                        <p className="lead fw-normal text-white-50 mb-0">магазин гуртових цін</p>
                    </div>
                </div>
            </header>
            <section className="py-5">
                <div className="container px-4 px-lg-5 mt-5">
                    <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                        {dataProductsView}
                    </div>
                </div>
            </section>
        </>

    )
}
export default HomePage;




