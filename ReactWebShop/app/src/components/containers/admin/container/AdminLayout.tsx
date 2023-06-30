import { useSelector } from "react-redux";
import AdminPage from "../AdminPage";

import {Outlet, useNavigate} from "react-router-dom";
import { IAuthUser } from "../../auth/types";
import { useEffect } from "react";



const AdminLayout = () => {

    const { isAuth, user } = useSelector((store: any) => store.auth as IAuthUser);
    const isAdmin = user?.roles === "Admin";
    const navigate = useNavigate();
    useEffect(() => {
        if(isAuth)
        {
            if(!isAdmin)
                navigate("/403");
        }
        else {
            navigate('/login');
        }
    },[]);


    return (
        <>
            <div className={""}>
                <div className="row">
                    <AdminPage/>
                    <main className="col-sm-8 col-md-9 ms-sm-auto col-lg-10 px-md-4">
                        <Outlet/>
                    </main>
                </div>
            </div>
        </>
    )
}

export default AdminLayout;