import { useEffect, useState } from "react";
import http from "../../../../http";

import { Link } from "react-router-dom";
import ModalDelete from "../../../common/ModalDelete";
import { ICharacteristicItem } from "./types";
import React from "react";

const CharacteristicListPage = () => {

    const [category, setCategory] = useState<ICharacteristicItem[]>();

    const onDelete = async (id: number) => {
        try {
            await http.delete(`api/Characteristics/delete/${id}`);
            setCategory(category?.filter(x => x.id !== id));
        } catch {
            console.log("Delete bad request");
        }
    }

    useEffect(() => {
        http.get<ICharacteristicItem[]>(`api/Characteristics/list`).then(resp => {
            // console.log("axios result", resp);
            setCategory(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    }, []);

    const dataView = (category?.map(cat =>
        <tr key={cat.id}>
            <th scope="row">{cat.id}</th>
            <td>{cat.name}</td>
            <td>
                {cat.categories?.map((categ, index) =>
                    <React.Fragment key={categ.id}>
                        {index != 0 && <>, </>}
                        {categ.name}
                    </React.Fragment>
                )}
            </td>
            <td><Link to={`/admin/categories/edit/${cat.id}`} className={"btn btn-info"}>Редагувати</Link></td>
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
                        <th scope="col">Категорії</th>
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
export default CharacteristicListPage