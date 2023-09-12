import { FormikErrors } from "formik";
import { FC, useEffect, useState } from "react";
import { ICharacteristicItem } from "./types";
import { ICategoryItem } from "../list/types";
import http from "../../../../http";



interface ICategoriesSelect {
    setFieldValue: (field: string, value: any, shouldValidate?: boolean | undefined) => Promise<void> | Promise<FormikErrors<ICharacteristicItem>>
}


const CategoriesSelect: FC<ICategoriesSelect> = ({setFieldValue}) => {
    
    useEffect(() => {
        http.get<ICategoryItem[]>(`api/Categories/list`).then(resp => {
            setCategoryOptions(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    }, []);

    const [category, setCategory] = useState<ICategoryItem[]>([]);
    const [categoryOptions, setCategoryOptions] = useState<ICategoryItem[]>([]);
    const handleCategoryChange = (event: any) => {

        console.log(event.target.value);
        const selectedCategoryId = parseInt(event.target.value, 10);
        const newCategoryItem = categoryOptions.find(cat => cat.id === selectedCategoryId);
        if (newCategoryItem)
            setCategory([...category, newCategoryItem]);
        else {
            console.log(`Запис з id ${event.target.value} не знайдено`); // Якщо запис не знайдено
        }
    };

    const deleteCategory = (id: number) => {

        const updatedCategory = category.filter(item => item.id !== id);
        setCategory(updatedCategory);

    };
    useEffect(() => {
        setFieldValue("categoriesId",  category.map(item => item.id));
    }, [category]);


    return (
        <>

            <label htmlFor="categories">Оберіть категорії:</label>
            <select
                id="categories"
                name="categories"

                onChange={handleCategoryChange}
                className="form-select"
                aria-label="Default select example"
            >
                <option key={-1} value={'Не вибрано'}>
                    Оберіть категорію
                </option>
                {categoryOptions
                    .filter(option => !category.some(cat => cat.id === option.id))
                    .map((option) => (
                        <option key={option.id} value={option.id}>
                            {option.name}
                        </option>
                    ))}
            </select>
            {category?.map(cat => {
                return (
                    <div className="d-flex align-items-center">
                        <input className="form-control" value={cat.name} disabled></input>
                        <button className="btn btn-danger" onClick={()=> deleteCategory(cat.id)} type="button" >X</button>
                    </div>
                )
            })}



        </>
    )
}
export default CategoriesSelect;