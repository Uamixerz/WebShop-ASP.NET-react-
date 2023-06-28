import { useEffect, useState } from "react";
import http from "../../../../http";
import { ICategoryItem } from "./types";
import { APP_ENV } from "../../../../env";
import { Link } from "react-router-dom";
import ModalDelete from "../../../common/ModalDelete";

const categoryListPage = () => {

    const [category, setCategory] = useState<ICategoryItem[]>();

    const onDelete = async (id:number) => {
        try {
            await http.delete(`api/categories/delete/${id}`);
            setCategory(category?.filter(x=>x.id!==id));
        } catch {
            console.log("Delete bad request");
        }
    }
    
    useEffect(() => {
        const result = http.get<ICategoryItem[]>(`api/Categories/list`).then(resp => {
            // console.log("axios result", resp);
            setCategory(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    });

    const dataView = (category?.map(cat =>
            <tr>
                <th scope="row">{cat.id}</th>
                <td>{cat.name}</td>
                <td>{cat.parentId}</td>
                <td><img src={`${APP_ENV.BASE_URL}uploads/100_` + cat.image} className="float-start imageCategories" alt="..." /></td>
                <td><Link to={`/categories/edit/${cat.id}`} className={"btn btn-info"}>Редагувати</Link></td>
                <td><ModalDelete id={cat.id} text={cat.name} deleteFunc={onDelete} /></td>
            </tr>
    ));

    return (
        <>
            <table className="table table-striped">
                <thead>
                    <tr>
                        <th scope="col">ID</th>
                        <th scope="col">Назва</th>
                        <th scope="col">Батьківська категорія</th>
                        <th scope="col">Картинка</th>
                        <th scope="col">Редагувати</th>
                        <th scope="col">Видалити</th>
                    </tr>
                </thead>
                <tbody>
                    {dataView}
                </tbody>

            </table>
        </>
    )
}
export default categoryListPage