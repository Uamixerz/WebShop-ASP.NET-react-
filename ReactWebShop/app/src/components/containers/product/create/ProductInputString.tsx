import classNames from "classnames";
import { FormikErrors, FormikTouched, useFormikContext } from "formik";
import { FC } from "react";
import * as yup from "yup";
import { IProductCreate } from "./types";

interface IProductInputFormik {
    values: IProductCreate;
    errors: FormikErrors<IProductCreate>;
    touched: FormikTouched<IProductCreate>;
    handleChange: (e: React.ChangeEvent<any>) => void;
}


const ProductInputString: FC<IProductInputFormik> = ({ values, errors, handleChange, touched }) => {
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

        </>
    )
}
export default ProductInputString;