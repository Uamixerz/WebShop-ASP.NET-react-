import React, { useEffect, useState } from 'react';
import 'bootstrap/dist/js/bootstrap';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-icons/font/bootstrap-icons.css';
import CategoryCreatePage from '../category/create/CategoryCreatePage';
import CategoryListPage from '../category/list/categoryListPage';

const AdminPage = () => {

    const [menuSelect, setMenuSelect] = useState("home");
    const divRef = React.useRef<HTMLDivElement>(null);
    const [divWidth, setDivWidth] = React.useState(0);

    useEffect(() => {
        const handleResize = () => {
            if (divRef.current) {
                setDivWidth(divRef.current.offsetWidth);
                console.log(divRef.current.offsetWidth);
            }
        };
    
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
                                    <button onClick={() => setMenuSelect('home')} className="nav-link align-middle px-0">
                                        <i className="fs-4 bi-house"></i> <span className="ms-1 d-none d-sm-inline">Home</span>
                                    </button>
                                </li>

                                <li>
                                    <button onClick={() => setMenuSelect('orders')} className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-table"></i> <span className="ms-1 d-none d-sm-inline">Замовлення</span>
                                    </button>
                                </li>
                                <li>
                                    <button onClick={() => setMenuSelect('users')} className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-people"></i> <span className="ms-1 d-none d-sm-inline">Користувачі</span>
                                    </button>
                                </li>
                                <li>
                                    <a href="#submenu3" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-grid"></i> <span className="ms-1 d-none d-sm-inline">Категорії</span>
                                    </a>
                                    <ul className="collapse nav flex-column ms-1" id="submenu3" data-bs-parent="#menu">
                                        <li className="w-100">
                                            <button onClick={() => setMenuSelect('categoryCreatePage')} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити категорію</span></button>
                                        </li>
                                        <li>
                                            <button onClick={() => setMenuSelect('categoryListPage')} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список категорій</span></button>
                                        </li>
                                    </ul>
                                </li>

                                <li>
                                    <a href="#submenu4" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-grid"></i> <span className="ms-1 d-none d-sm-inline">Продукти</span>
                                    </a>
                                    <ul className="collapse nav flex-column ms-1" id="submenu4" data-bs-parent="#menu">
                                        <li className="w-100">
                                            <button onClick={() => setMenuSelect('productCreatePage')} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити продукт</span></button>
                                        </li>
                                        <li>
                                            <button onClick={() => setMenuSelect('productListPage')} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список продуктів</span></button>
                                        </li>
                                    </ul>
                                </li>
                            </ul>
                            <hr />

                        </div>
                    </div>
                    <div className="col py-3 text-center" style={{marginLeft: divWidth}}>
                        {menuSelect === 'home' && <><h1>ADMIN PANEL</h1></>}
                        {menuSelect === 'categoryCreatePage' && <CategoryCreatePage></CategoryCreatePage>}
                        {menuSelect === 'categoryListPage' && <CategoryListPage></CategoryListPage>}
                        {menuSelect === 'productCreatePage' && <></>}
                        {menuSelect === 'productListPage' && <></>}
                        {menuSelect === 'orders' && <></>}
                        {menuSelect === 'users' && <></>}
                    </div>
                </div>
            </div>

        </>)
};

export default AdminPage;