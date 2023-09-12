
import { useFormik } from "formik";
import * as yup from "yup";
import { useNavigate } from "react-router-dom";
import http from "../../../../http";
import { ICharacteristicItem } from "./types";
import CharacteristicInput from "./CharacteristicInput";

const CharacteristicCreatePage = () => {



    const initValues: ICharacteristicItem = {
        name: '',
        categoriesId: []
    }

    const createSchema = yup.object({
        name: yup.string().required("Вкажіть назву"),
    });
    const navigate = useNavigate();
    const onSubmitFormikData = (values: ICharacteristicItem) => {
        console.log(values);

        http.post('/api/Characteristics/AddCharacteristic', values, {
            headers: {
                "Content-Type": "multipart/form-data",
            },
        }).then(resp => {
            console.log(values, resp);
            navigate("..");
        }).catch(bad => {
            console.log("bad request", bad)
        });
        navigate("..");
    }

    const formik = useFormik({
        initialValues: initValues,
        validationSchema: createSchema,
        onSubmit: onSubmitFormikData
    });
    const { values, errors, touched, handleSubmit, handleChange } = formik;


    return (
        <>
            <form className="col-md-6 offset-md-3" onSubmit={handleSubmit} noValidate>

                <h1 className={"text-center"}>Створення характеристики</h1>
                <CharacteristicInput values={values} errors={errors} touched={touched} handleChange={handleChange} formik={formik}></CharacteristicInput>
                <button className="btn btn-primary" type="submit">Додати характеристику</button>
            </form>

        </>
    )
}
export default CharacteristicCreatePage;