import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/js/bootstrap';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-icons/font/bootstrap-icons.css';
import CategoryCreatePage from '../category/create/CategoryCreatePage';
import CategoryListPage from '../category/list/categoryListPage';
import { Link } from 'react-router-dom';

const AdminPage = () => {

    
    const divRef = React.useRef<HTMLDivElement>(null);
    const [divWidth, setDivWidth] = React.useState(0);

    useEffect(() => {
        const handleResize = () => {
            if (divRef.current) {
                setDivWidth(divRef.current.offsetWidth);
                console.log(divRef.current.offsetWidth);
            }
        };
        if (divRef.current) {
            setDivWidth(divRef.current.offsetWidth);
            console.log(divRef.current.offsetWidth);
        }
        // Додати прослуховувач події resize
        window.addEventListener('resize', handleResize);
    
        // Прибрати прослуховувач події після завершення компонента
        return () => {
          window.removeEventListener('resize', handleResize);
        };
      }, []);
    

    return (
        <>
            <div className="container-fluid h-100" tabIndex={-2}>
                <div className="row flex-nowrap">
                    <div className="h-100 col-auto col-md-3 col-xl-2 px-sm-2 px-0 position-fixed" ref={divRef}>
                        <div className="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
                            <a href="/" className="d-flex align-items-center pb-3 mb-md-0 me-md-auto text-white text-decoration-none">
                                <span className="fs-5 d-none d-sm-inline">Menu</span>
                            </a>
                            <ul className="nav nav-pills flex-column mb-sm-auto mb-0 align-items-center align-items-sm-start" id="menu">
                                <li className="nav-item">
                                    <Link to={"/admin"} className="nav-link align-middle px-0">
                                        <i className="fs-4 bi-house"></i> <span className="ms-1 d-none d-sm-inline">Home</span>
                                    </Link>
                                </li>

                                <li>
                                    <Link to={"/admin/orders/list"} className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-table"></i> <span className="ms-1 d-none d-sm-inline">Замовлення</span>
                                    </Link>
                                </li>
                                <li>
                                    <Link to={"/admin/users/list"} className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-people"></i> <span className="ms-1 d-none d-sm-inline">Користувачі</span>
                                    </Link>
                                </li>
                                <li>
                                    <a href="#submenu3" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-grid"></i> <span className="ms-1 d-none d-sm-inline">Категорії</span>
                                    </a>
                                    <ul className="collapse nav flex-column ms-1" id="submenu3" data-bs-parent="#menu">
                                        <li className="w-100">
                                            <Link  to={"/admin/categories/create"} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити категорію</span></Link>
                                        </li>
                                        <li>
                                            <Link  to={"/admin/categories/list"} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список категорій</span></Link>
                                        </li>
                                    </ul>
                                </li>

                                <li>
                                    <a href="#submenu4" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-grid"></i> <span className="ms-1 d-none d-sm-inline">Продукти</span>
                                    </a>
                                    <ul className="collapse nav flex-column ms-1" id="submenu4" data-bs-parent="#menu">
                                        <li className="w-100">
                                            <Link  to={"/admin/product/add"} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити продукт</span></Link>
                                        </li>
                                        <li>
                                            <Link to={"/admin/product/list"} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список продуктів</span></Link>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                            <hr />

                        </div>
                    </div>
                </div>
            </div>

        </>)
};

export default AdminPage;