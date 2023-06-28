import { Link, useNavigate } from "react-router-dom"
import "./DefaultHeader.css";
import axios from "axios";
import { ICategoryItem } from "./types";
import { useEffect, useState } from "react";
import http from "../../../http";
import { APP_ENV } from "../../../env";
import { useSelector } from "react-redux";
import { AuthUserActionType, IAuthUser } from "../auth/types";
import { useDispatch } from "react-redux";
import ModalDelete from "../../common/ModalDelete";

const DefaultHeader = () => {

    const navigator = useNavigate();
    const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    // Список категорій
    const [category, setCategory] = useState<ICategoryItem[]>();
    // id батьківської категорії
    const [search, setSearch] = useState("");

    const dispatch = useDispatch();

    const logout = (e: any) => {
        e.preventDefault();
        localStorage.removeItem("token");
        http.defaults.headers.common["Authorization"] = ``;
        dispatch({ type: AuthUserActionType.LOGOUT_USER });
        navigator('/');
    };

    const onDelete = async (id:number) => {
        try {
            await http.delete(`api/categories/delete/${id}`);
            setCategory(category?.filter(x=>x.id!==id));
        } catch {
            console.log("Delete bad request");
        }
    }
    // запит на апі
    useEffect(() => {
        // Якщо search пустий витягує всі категорії, інакше витяг категорій по батьківському id
        const result = http.get<ICategoryItem[]>(`api/Categories/list${search}`).then(resp => {
            // console.log("axios result", resp);
            setCategory(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    });


    const textCategory = "Категорії"
    // якщо search пустий вивід всіх категорій в яких немає батьківського ел., інакше вивід всіх що знаходяться в category
    const dataView = ((search.length == 0) ? category?.filter(i => i.parentId == null) : category)?.sort((a, b) => a.priority - b.priority)?.map(cat =>
        <div className="d-flex vertical-align-middle mb-2 mt-2">
            <img src={`${APP_ENV.BASE_URL}uploads/100_` + cat.image} className="float-start imageCategories" alt="..." />
            
            <Link onClick={() => setSearch(cat.id.toString())} className="page-link vertical-align-middle h-100 w-100 d-inline" to={""}>{cat.name}<i className="float-end bi bi-caret-right-fill mt-1" style={{ height: 25 }}></i></Link>
            &nbsp;&nbsp;
            <ModalDelete id={cat.id} text={cat.name} deleteFunc={onDelete}/>
            &nbsp;&nbsp;
                <Link to={`/categories/edit/${cat.id}`} className={"btn btn-info"}>Редагувати</Link>
        </div>
    );

    return (
        <>
            <div className="modal fade" id="exampleModal" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="exampleModalLabel">{textCategory}</h1>
                            <button type="button" className="btn-close" onClick={() => setSearch("")} data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            {dataView}
                        </div>
                        <div className="modal-footer">

                        </div>
                    </div>
                </div>
            </div>

            <nav className="navbar bg-body-tertiary fixed-top" >
                <div className="container-fluid">
                    <a className="navbar-brand" href="/">Твій Магазин</a>
                    <div className="d-flex justify-content-around justify-content-center align-items-center">
                        <img src={`${APP_ENV.BASE_URL}uploads/50_` + user?.image} className="float-start imageCategories" alt="..." />
                        <p className="m-0 pr-50" style={{paddingRight: '10px'}}>{user?.email}</p>
                        <button className="navbar-toggler" type="button" data-bs-toggle="offcanvas" data-bs-target="#offcanvasNavbar" aria-controls="offcanvasNavbar" aria-label="Toggle navigation">
                            <span className="navbar-toggler-icon"></span>
                        </button>
                    </div>
                    <div className="offcanvas offcanvas-end" id="offcanvasNavbar" aria-labelledby="offcanvasNavbarLabel">
                        <div className="offcanvas-header">
                            <h5 className="offcanvas-title" id="offcanvasNavbarLabel">Меню</h5>
                            <button type="button" className="btn-close" data-bs-dismiss="offcanvas" aria-label="Close"></button>
                        </div>
                        <div className="offcanvas-body">
                            <ul className="navbar-nav justify-content-end flex-grow-1 pe-3">
                                <li className="nav-item">
                                    <a className="nav-link active" aria-current="page" href="/">Home </a>

                                </li>
                                <li className="nav-item">
                                    <Link className="nav-link active" aria-current="page" to="/categories/create">
                                        Створити категорію
                                    </Link>
                                </li>

                                {!isAuth ?
                                    <>
                                        <li className="nav-item">
                                            <Link className="nav-link active" aria-current="page" to="/register">
                                                Регістрація
                                            </Link>
                                        </li>
                                        <li className="nav-item">
                                            <Link className="nav-link active" aria-current="page" to="/login">
                                                Вхід
                                            </Link>
                                        </li>
                                    </> :
                                    <li className="nav-item">
                                        <Link className="nav-link active" aria-current="page" to="/" onClick={logout}>
                                            Вихід
                                        </Link>
                                    </li>
                                }

                                <li className="nav-item">
                                    <button type="button" className="btn p-0" data-bs-toggle="modal" data-bs-target="#exampleModal">
                                        Категорії товарів
                                    </button>
                                </li>
                            </ul>
                            <form className="d-flex mt-3" role="search">
                                <input className="form-control me-2" type="search" placeholder="Search" aria-label="Search" />
                                <button className="btn btn-outline-success" type="submit">Search</button>
                            </form>
                        </div>
                    </div>
                </div>
            </nav>
        </>
    )
}
export default DefaultHeader;