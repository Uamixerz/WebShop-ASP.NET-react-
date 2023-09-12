import classNames from "classnames";
import { FormikErrors, FormikTouched } from "formik";
import { FC } from "react";
import { ICharacteristicItem } from "./types";
import CategoriesSelect from "./CategoriesSelect";


interface ICharacteristicInputFormik {
    values: ICharacteristicItem;
    errors: FormikErrors<ICharacteristicItem>;
    touched: FormikTouched<ICharacteristicItem>;
    handleChange: (e: React.ChangeEvent<any>) => void;
    formik: any;
}


const CharacteristicInput: FC<ICharacteristicInputFormik> = ({ values, errors, handleChange, touched, formik }) => {
    const {setFieldValue } = formik;
    return (
        <>
            <div className="mb-3">
                <label htmlFor="name">Назва характеристики:</label>
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
                <CategoriesSelect setFieldValue={setFieldValue}></CategoriesSelect>
            </div>
            

        </>
    )
}
export default CharacteristicInput;