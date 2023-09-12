import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import 'react-bootstrap';
import { Link } from "react-router-dom"
import "./DefaultHeader.css";
import { ICategoryItem } from "./types";
import { useEffect, useState } from "react";
import http from "../../../http";

import ModalDelete from "../../common/ModalDelete";
import { Container, Nav, NavDropdown, Navbar } from 'react-bootstrap';
import { useSelector } from 'react-redux';
import { IAuthUser } from '../auth/types';


const DefaultHeader = () => {

    // const navigator = useNavigate();
    const { user } = useSelector((store: any) => store.auth as IAuthUser);
    const isAdmin = user?.roles === "Admin";
    // Список категорій
    const [category, setCategory] = useState<ICategoryItem[]>();
    // id батьківської категорії
    const [search, setSearch] = useState("");

    // const dispatch = useDispatch();

    // const logout = (e: any) => {
    //     e.preventDefault();
    //     localStorage.removeItem("token");
    //     http.defaults.headers.common["Authorization"] = ``;
    //     dispatch({ type: AuthUserActionType.LOGOUT_USER });
    //     navigator('/');
    // };

    const onDelete = async (id: number) => {
        try {
            await http.delete(`api/categories/delete/${id}`);
            setCategory(category?.filter(x => x.id !== id));
        } catch {
            console.log("Delete bad request");
        }
    }
    // запит на апі
    useEffect(() => {
        // Якщо search пустий витягує всі категорії, інакше витяг категорій по батьківському id
        http.get<ICategoryItem[]>(`api/Categories/list${search}`).then(resp => {
            // console.log("axios result", resp);
            setCategory(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    }, []);


    // якщо search пустий вивід всіх категорій в яких немає батьківського ел., інакше вивід всіх що знаходяться в category
    const dataCategoriesView = ((search.length == 0) ? category?.filter(i => i.parentId == null) : category)?.sort((a, b) => a.priority - b.priority)?.map(cat =>
        <NavDropdown.Item key={cat.id} href="">
            <Link onClick={() => setSearch(cat.id.toString())} className="dropdown-item" to={""}>{cat.name}
            </Link>
            {isAdmin && <>&nbsp;&nbsp;<ModalDelete id={cat.id} text={cat.name} deleteFunc={onDelete} /></>}
            {isAdmin && <>&nbsp;&nbsp;<Link to={`/admin/categories/edit/${cat.id}`} className={"btn btn-info"}>Редагувати</Link></>}
        </NavDropdown.Item>
    );



    return (
        <>

            <nav className="navbar1 navbar-expand-lg navbar-light bg-light">
                <div className="container px-4 px-lg-5">

                    <Navbar expand="lg" className="bg-body-tertiary">
                        <Container>
                            <Navbar.Brand href="/">Магазин</Navbar.Brand>
                            <Navbar.Toggle aria-controls="basic-navbar-nav" />
                            <Navbar.Collapse id="basic-navbar-nav">
                                <Nav className="me-auto">
                                    <Nav.Link href="/">Головна</Nav.Link>
                                    <Nav.Link href="/">Про нас</Nav.Link>
                                    <NavDropdown title="Категорії" id="basic-nav-dropdown">
                                        <NavDropdown.Item href="#action/3.1">Всі категорії</NavDropdown.Item>
                                        <NavDropdown.Divider />
                                        
                                        {dataCategoriesView}
                                        
                                    </NavDropdown>
                                    {isAdmin && <Nav.Link href="/admin">Адмін панель</Nav.Link>}
                                </Nav>
                                <form className="d-flex">
                                    <button className="btn btn-outline-dark" type="submit">
                                        <i className="bi-cart-fill me-1"></i>
                                        Cart
                                        <span className="badge bg-dark text-white ms-1 rounded-pill">0</span>
                                    </button>
                                </form>
                            </Navbar.Collapse>

                        </Container>
                    </Navbar>
                </div>
            </nav>

        </>

    )

}
export default DefaultHeader;




