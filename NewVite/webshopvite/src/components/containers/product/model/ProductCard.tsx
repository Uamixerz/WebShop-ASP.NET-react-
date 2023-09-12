import React from 'react';
import { Link } from 'react-router-dom';
import { APP_ENV } from '../../../../env';
import { IProductGetItem } from '../list/types';
import classNames from 'classnames';


interface ProductCardProps {
    product: IProductGetItem;
}

const ProductCard: React.FC<ProductCardProps> = ({ product }) => {
    return (
        <div key={product.id} className="col mb-5">
            <div className="card h-100">
                <div className="badge bg-dark text-white position-absolute" style={{ top: "0.5rem", right: "0.5rem" }}>Sale</div>

                <div id ={`carouselExampleIndicators${product.id}`} className="carousel carousel-dark slide">
                    <div className="carousel-indicators">
                        {product && product.images.map((image, index) =>
                            <button key={image.name} type="button" className={classNames({ active: index === 0 })} data-bs-target={`#carouselExampleIndicators${product.id}`} data-bs-slide-to={`${index}`} aria-label={`Slide ${index}`}></button>
                        )}
                    </div>

                    <div className="carousel-inner">
                        {product && product.images.map((image, index) =>

                            <div key={image.id} className={classNames({
                                active: index === 0,
                                "carousel-item": true,
                            })}>
                                <img src={APP_ENV.BASE_URL + "Uploads/600_" + image.name} className="d-block w-100" alt="..." />
                            </div>


                        )}

                    </div>
                    <button className="carousel-control-prev" type="button" data-bs-target={`#carouselExampleIndicators${product.id}`} data-bs-slide="prev">
                        <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                        <span className="visually-hidden">Previous</span>
                    </button>
                    <button className="carousel-control-next" type="button" data-bs-target={`#carouselExampleIndicators${product.id}`} data-bs-slide="next">
                        <span className="carousel-control-next-icon" aria-hidden="true"></span>
                        <span className="visually-hidden">Next</span>
                    </button>
                </div>
                <div className="card-body p-4">
                    <div className="text-center">
                        <h5 className="fw-bolder">{product.name}</h5>

                        <div className="d-flex justify-content-center small text-warning mb-2">
                            <div className="bi-star-fill"></div>
                            <div className="bi-star-fill"></div>
                            <div className="bi-star-fill"></div>
                            <div className="bi-star-fill"></div>
                            <div className="bi-star-fill"></div>
                        </div>
                        {product.oldPrice && <span className="text-muted text-decoration-line-through">{product.oldPrice}.00 грн</span>}
                        
                        <p>{product.price}.00 грн</p>
                    </div>
                </div>

                <div className="card-footer p-4 pt-0 border-top-0 bg-transparent">
                    <div className="text-center">
                        <Link className="btn btn-outline-dark mt-auto" to={`/products/buy/${product.id}`} >Купити</Link>
                    </div>
                </div>
            </div>
        </div>
    );
}

export default ProductCard;