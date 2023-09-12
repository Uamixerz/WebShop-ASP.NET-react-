import classNames from "classnames";
import { FormikErrors, FormikTouched } from "formik";
import { FC } from "react";

import { IProductCreate } from "./types";
import { Editor } from "@tinymce/tinymce-react";

interface IProductInputFormik {
    values: IProductCreate;
    errors: FormikErrors<IProductCreate>;
    touched: FormikTouched<IProductCreate>;
    handleChange: (e: React.ChangeEvent<any>) => void;
    formik: any;
}


const ProductInputString: FC<IProductInputFormik> = ({ values, errors, handleChange, touched, formik }) => {
    return (
        <>
            <div className="mb-3">
                <label htmlFor="name">Назва товару:</label>
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
                <label htmlFor="description">Опис товару:</label>
                <Editor
                    id="description"
                    value={values.description}
                    onEditorChange={(content) => {
                        formik.setFieldValue('description', content);
                    }}
                    init={{
                        height: 300,
                        menubar: false,
                        plugins: ["advlist autolink lists link image charmap print preview anchor","code", "searchreplace visualblocks code fullscreen", "insertdatetime media table paste code help wordcount"],
                        toolbar: "undo redo | formatselect | " +
                            "bold italic backcolor | alignleft aligncenter " +
                            "alignright alignjustify | bullist numlist outdent indent | " +
                            "removeformat | help",
                    }}
                />
                {errors.description && touched.description && <div className="invalid-feedback">
                    {errors.description}
                </div>}
            </div>
            <div className="mb-3">
                <label htmlFor="price">Ціна товару:</label>
                <input
                    type='number'
                    id="price"
                    name="price"
                    className={classNames("form-control", { "is-invalid": errors.price && touched.price })}
                    value={values.price}
                    onChange={handleChange}
                    required
                />
                {errors.price && touched.price && <div className="invalid-feedback">
                    {errors.price}
                </div>}
            </div>
            <div className="mb-3">
                <label htmlFor="oldPrice">Стара ціна товару (якщо є):</label>
                <input
                    type='number'
                    id="oldPrice"
                    name="oldPrice"
                    className={classNames("form-control", { "is-invalid": errors.oldPrice && touched.oldPrice })}
                    value={values.oldPrice}
                    onChange={handleChange}
                    required
                />
                {errors.oldPrice && touched.oldPrice && <div className="invalid-feedback">
                    {errors.oldPrice}
                </div>}
            </div>

        </>
    )
}
export default ProductInputString;