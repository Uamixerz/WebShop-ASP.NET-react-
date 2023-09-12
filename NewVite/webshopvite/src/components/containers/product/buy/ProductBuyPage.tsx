
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.min';
import { useParams } from 'react-router-dom';

import { IProductGetItem } from '../list/types';
import { useEffect, useState } from 'react';
import http from '../../../../http';
import { APP_ENV } from '../../../../env';
import ProductCard from '../model/ProductCard';
import { RelatedGetModel } from './types';
import classNames from 'classnames';



const HomePage = () => {
    const { id } = useParams<string>();
    const [count, setCount] = useState<number>(1);
    // const { user } = useSelector((store: any) => store.auth as IAuthUser);
    // const isAdmin = user?.roles === "Admin";

    const [product, setProduct] = useState<IProductGetItem>();
    const [relatedProduct, setRelatedProduct] = useState<IProductGetItem[]>();

    const handleCountChange = (e: any) => {
        const newValue = parseInt(e.target.value, 10); // Перетворення введеного значення на число
        if (!isNaN(newValue)) {
            setCount(newValue); // Встановлення нового значення count
        }
    };

    useEffect(() => {

        http.get<IProductGetItem>(`api/Product/getById?id=${id}`).then(resp => {
            // console.log("axios result", resp);
            setProduct(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    }, [id]);

    useEffect(() => {

        const values = { id: product?.id ?? 1, categoryId: product?.categoryId ?? 1 } as RelatedGetModel;
        http.post<IProductGetItem[]>(`api/Product/getRelatedProducts`, values).then(resp => {

            setRelatedProduct(resp.data);
            //console.log("axios result", relatedProduct);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );
        console.log("axios result", relatedProduct);
    }, [product]);



    return (
        <>
            <section className="py-5">
                <div className="container px-4 px-lg-5 my-5">
                    {product &&
                        <div className="row gx-4 gx-lg-5 align-items-center">
                            <div id="carouselExampleIndicators" className="carousel carousel-dark slide col-md-6">
                                <div className="carousel-indicators">
                                    {product && product.images.map((image, index) =>
                                        <button key={image.name} type="button" className={classNames({ active: index === 0 })} data-bs-target="#carouselExampleIndicators" data-bs-slide-to={`${index}`} aria-label={`Slide ${index}`}></button>
                                    )}
                                </div>

                                <div className="carousel-inner">
                                    {product && product.images.map((image, index) =>

                                        <div key={image.id} className={classNames({
                                            active: index === 0,
                                            "carousel-item": true,
                                        })}>
                                            <img src={APP_ENV.BASE_URL + "Uploads/600_" + image.name} className="card-img-top mb-5 mb-md-0" alt="..." />
                                        </div>


                                    )}

                                </div>
                                <button className="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
                                    <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                                    <span className="visually-hidden">Previous</span>
                                </button>
                                <button className="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
                                    <span className="carousel-control-next-icon" aria-hidden="true"></span>
                                    <span className="visually-hidden">Next</span>
                                </button>
                            </div>


                            <div className="col-md-6">
                                <div className="small mb-1"></div>
                                <h1 className="display-5 fw-bolder">{product.name}</h1>
                                <div className="fs-5 mb-5">
                                    {product.oldPrice && <span className="text-decoration-line-through">{product.oldPrice}.00 грн</span>}
                                    &nbsp;
                                    <span>{product?.price} грн</span>
                                </div>
                                <ul className="nav nav-tabs" style={{marginBottom: '1rem'}} id="myTab" role="tablist">
                                    <li className="nav-item" role="presentation">
                                        <button className="nav-link active" style={{ color: 'black' }} id="description-tab" data-bs-toggle="tab" data-bs-target="#description" type="button" role="tab" aria-controls="description" aria-selected="true">Опис</button>
                                    </li>
                                    <li className="nav-item" role="presentation">
                                        <button className="nav-link " style={{ color: 'black' }} id="characteristics-tab" data-bs-toggle="tab" data-bs-target="#characteristics" type="button" role="tab" aria-controls="characteristics" aria-selected="false">Характеристики</button>
                                    </li>
                                </ul>
                                <div className="tab-content" id="myTabContent">
                                    <p className="lead tab-pane fade show active" id="description" role="tabpanel" aria-labelledby="description-tab" dangerouslySetInnerHTML={{ __html: product?.description }}></p>
                                    <div className="tab-pane fade" id="characteristics" role="tabpanel" aria-labelledby="characteristics-tab">...</div>

                                </div>


                                <div className="d-flex">
                                    <input className="form-control text-center me-3" id="inputQuantity" type="number" value={count} onChange={(e) => handleCountChange(e)} style={{ maxWidth: "3rem" }} />
                                    <button className="btn btn-outline-dark flex-shrink-0" type="button">
                                        <i className="bi-cart-fill me-1"></i>
                                        Добавити в кошик
                                    </button>
                                    &nbsp;&nbsp;&nbsp;
                                    <button className="btn btn-outline-dark flex-shrink-0" type="button">
                                        <i className="bi-cart-fill me-1"></i>
                                        Купити в один клік
                                    </button>
                                </div>
                            </div>
                        </div>}
                </div>
            </section >
            <section className="py-5 bg-light">
                <div className="container px-4 px-lg-5 mt-5">
                    <h2 className="fw-bolder mb-4">Схожі товари</h2>
                    <div className="row gx-4 gx-lg-5 row-cols-2 row-cols-md-3 row-cols-xl-4 justify-content-center">
                        {relatedProduct?.map(product => <ProductCard key={product.id} product={product}></ProductCard>)}
                    </div>
                </div>
            </section>
        </>

    )
}
export default HomePage;




