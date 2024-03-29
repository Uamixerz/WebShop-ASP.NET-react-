import 'bootstrap/dist/js/bootstrap';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-icons/font/bootstrap-icons.css';
import { Link } from 'react-router-dom';

const AdminPage = () => {
    return (
        <>
            <div className="container-fluid h-100" tabIndex={-2}>
                <div className="row flex-nowrap">
                    <div className="h-100 col-auto col-md-3 col-xl-2 px-sm-2 px-0 position-fixed" >
                        <div className="d-flex flex-column align-items-center align-items-sm-start px-3 pt-2 text-white min-vh-100">
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
                                            <Link  to={"/admin/products/create"} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити продукт</span></Link>
                                        </li>
                                        <li>
                                            <Link to={"/admin/products"} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список продуктів</span></Link>
                                        </li>
                                    </ul>
                                </li>
                                <li>
                                    <a href="#submenu5" data-bs-toggle="collapse" className="nav-link px-0 align-middle">
                                        <i className="fs-4 bi-grid"></i> <span className="ms-1 d-none d-sm-inline">Характеристики</span>
                                    </a>
                                    <ul className="collapse nav flex-column ms-1" id="submenu5" data-bs-parent="#menu">
                                        <li className="w-100">
                                            <Link  to={"/admin/characteristic/create"} className="nav-link px-0"> <span className="d-none d-sm-inline">Добавити характеристику</span></Link>
                                        </li>
                                        <li>
                                            <Link to={"/admin/characteristic"} className="nav-link px-0"> <span className="d-none d-sm-inline">Вивести список характеристик</span></Link>
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