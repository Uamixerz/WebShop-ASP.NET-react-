import React, { ChangeEvent, useEffect, useState } from 'react';
import axios from 'axios';
import { Link, Navigate, useNavigate } from 'react-router-dom';
import { ICategoryCreate, ICategoryCreateError } from './types';
import { useFormik } from 'formik';
import * as yup from "yup";
import classNames from 'classnames';
import { ICategoryItem } from '../../types';

const CategoryCreatePage = () => {
    // Список категорій
    const [category, setCategory] = useState<ICategoryItem[]>();
    // id батьківської категорії
    const [search, setSearch] = useState("");
    const [textSelectCategory, settextSelectCategory] = useState('Вибрати батьківську категорію (якщо є)');

    // запит на апі
    useEffect(() => {
        // Якщо search пустий витягує всі категорії, інакше витяг категорій по батьківському id
        const result = axios.get<ICategoryItem[]>(`http://localhost:5107/api/Categories/list${search}`).then(resp => {
            // console.log("axios result", resp);
            setCategory(resp.data);
        }
        )
            .catch(bad => {
                console.log("bad request", bad)
            }
            );

    });

    // Для вибору батьківської категорії
    const HandleClickSelect = (name:string, id:number) => {
        values.parentId = id;
        console.log(name);
        settextSelectCategory("Вибрано батьківську категорію: "+ name);
        setSearch("");
    }

    
    // якщо search пустий вивід всіх категорій в яких немає батьківського ел., інакше вивід всіх що знаходяться в category
    const dataView = ((search.length == 0) ? category?.filter(i => i.parentId == null) : category)?.sort((a, b) => a.priority - b.priority)?.map(cat =>
        <div className="d-flex justify-content-center vertical-align-middle mb-2 mt-2">
            <img src={cat.image} className="float-start imageCategories" alt="..." />

            <Link onClick={() => setSearch(cat.id.toString())} className="d-flex page-link vertical-align-middle h-100 w-100 d-inline" to={""}>
                {cat.name}
            </Link>
            <div className='ms-auto w-auto'>
                <button type="button" className="btn btn-outline-dark" onClick={() => HandleClickSelect(cat.name, cat.id)} data-bs-dismiss="modal" aria-label="Close">Вибрати</button>
            </div>
            <Link onClick={() => setSearch(cat.id.toString())} className="ms-auto d-flex page-link vertical-align-middle h-100 d-inline" to={""}>
                <div className='ms-auto'>
                    <i className="float-end bi bi-caret-right-fill mt-1" style={{ height: 25 }}></i>
                </div>
            </Link>
        </div>
    );


    const initValues: ICategoryCreate = {
        name: '',
        image: '',
        description: '',
        priority: 0,
        parentId: 0
    }
    const navigate = useNavigate();
    const {parentId} = initValues;

    const createSchema = yup.object({
        name: yup.string().required("Вкажіть назву"),
        description: yup.string().required("Вкажіть опис"),
        image: yup.string().required("Вкажіть url"),
        priority: yup.string().required("Вкажіть пріорітет")
    });

    const onSubmitFormikData = (values: ICategoryCreate) => {
        console.log("Formik send ", values);
        axios.post('http://localhost:5107/api/Categories/create', values)
            .then(resp => {
                console.log(values, resp);
                navigate("/");
            })
            .catch(bad => {
                console.log("Bad request", bad);
            })
    }

    const formik = useFormik({
        initialValues: initValues,
        validationSchema: createSchema,
        onSubmit: onSubmitFormikData
    });

    const { values, errors, touched, handleSubmit, handleChange } = formik;

    return (
        <>
        {/* Модал для вибору батьківської категорії */}
            <div className="modal fade" id="ModalSelect" aria-labelledby="ModalSelectLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="ModalSelectLabel">Вибір батьківської категорії</h1>
                            <button type="button" className="btn-close" onClick={() => setSearch("")} data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body">
                            <div className="d-flex justify-content-center vertical-align-middle mb-2 mt-2">
                                <img src={"https://e7.pngegg.com/pngimages/29/173/png-clipart-null-pointer-symbol-computer-icons-pi-miscellaneous-angle-thumbnail.png"} className="float-start imageCategories" alt="..." />
                                <Link className="d-flex page-link vertical-align-middle h-100 w-100 d-inline" to={""}>
                                    <p className='m-0'>- немає батьківського</p>

                                </Link>
                                <button onClick={()=>HandleClickSelect("",0)} className='justify-content-end float-end btn btn-outline-dark'>Вибрати</button>
                            </div>
                            {dataView}
                        </div>
                        <div className="modal-footer">

                        </div>
                    </div>
                </div>
            </div>

            <div>
                <h1 className='text-center'>Додавання категорії</h1>
                <form className="col-md-6 offset-md-3" onSubmit={handleSubmit} noValidate>
                    <div className="mb-3">
                        <label htmlFor="name">Назва категорії:</label>
                        <input
                            className={classNames("form-control", { "is-invalid": errors.name && touched.name })}
                            type="text"
                            id="name"
                            name="name"
                            value={values.name}
                            onChange={handleChange}
                            required
                        />
                        {errors.name && touched.name && <div className="invalid-feedback">
                            {errors.name}
                        </div>}

                    </div>
                    <div className="mb-3">
                        <label htmlFor="image">URL фотографії:</label>
                        <input
                            type="url"
                            id="image"
                            name="image"
                            className={classNames("form-control", { "is-invalid": errors.image && touched.image })}
                            value={values.image}
                            onChange={handleChange}
                            required
                        />
                        {errors.image && touched.image && <div className="invalid-feedback">
                            {errors.image}
                        </div>}
                    </div>
                    <div className="mb-3">
                        <label htmlFor="description">Опис категорії:</label>
                        <textarea
                            id="description"
                            name="description"
                            className={classNames("form-control", { "is-invalid": errors.description && touched.description })}
                            value={values.description}
                            onChange={handleChange}
                            required
                        />
                        {errors.description && touched.description && <div className="invalid-feedback">
                            {errors.description}
                        </div>}
                    </div>
                    <div className="mb-3">
                        <label htmlFor="priority">Пріорітет:</label>
                        <input
                            type='number'
                            id="priority"
                            name="priority"
                            className={classNames("form-control", { "is-invalid": errors.priority && touched.priority })}
                            value={values.priority}
                            onChange={handleChange}
                            required
                        />
                        {errors.priority && touched.priority && <div className="invalid-feedback">
                            {errors.priority}
                        </div>}
                    </div>
                    <div className='mb-3'>
                        <button type='button' data-bs-toggle="modal" data-bs-target="#ModalSelect" className='btn btn-outline-info'>{textSelectCategory}</button>
                    </div>

                    <button className="btn btn-primary" type="submit">Додати категорію</button>
                </form>
            </div>
        </>
    );
};

export default CategoryCreatePage;