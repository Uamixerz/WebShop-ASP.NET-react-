import { useSelector } from "react-redux";
import AdminPage from "../AdminPage";

import { Outlet, useNavigate } from "react-router-dom";
import { IAuthUser } from "../../auth/types";
import { useEffect } from "react";
import React from "react";



const AdminLayout = () => {

    const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    const isAdmin = user?.roles === "Admin";
    const navigate = useNavigate();
    useEffect(() => {
        if (isAuth) {
            if (!isAdmin)
                navigate("/403");
        }
        else {
            navigate('/login');
        }
    }, []);
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
            <div className={""}>
                <div className="row" >
                    <div className="h-100 col-auto col-md-3 col-xl-2 px-sm-2 px-0 position-fixed" ref={divRef} style={{minWidth: 54}}>
                        <AdminPage></AdminPage>
                    </div>
                    <main className="col-sm-8 col-md-9 ms-sm-auto col-lg-10 px-md-4" style={{ marginLeft: divWidth, flexShrink: 1 }}>
                        <Outlet />
                    </main>
                </div>
            </div>
        </>
    )
}

export default AdminLayout;