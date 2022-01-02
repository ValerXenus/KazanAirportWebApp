import axios from 'axios';
import md5 from 'md5';
import React, { useState } from 'react';
import { usersMethods } from '../../../HelperComponents/ApiUrls';
import InputValidations from '../../../HelperComponents/Logic/InputValidations';
import styles from './Login.module.css'

const SignUpModule = () => {
    // Временное хранилище
    const [state, setState] = useState({
        login: "",
        password: "",
        email: ""
    });

    // Обработчик заполнения текстовых полей
    const handleTextInputChange = event => {
        const { name, value } = event.target;
        setState(x => ({
            ...x,
            [name]: value
        }));
    };

    // Валидация введенных пользователем данных
    const validateInputs = () => {
        let errorText = "";

        errorText += InputValidations.validateRequiredField(state.login, "Логин");
        errorText += InputValidations.validateRequiredField(state.email, "Email");
        errorText += InputValidations.validatePassword(state.password);

        if (errorText.length !== 0) {
            alert(`Некоторые поля заполнены не верно. Подробности:\n${errorText}`)
            return false;
        }

        return true;
    }

    // Обработчик кнопки "Зарегистрироваться"
    const handleSignUpButton = () => {

        if (!validateInputs())
            return;

        let encryptedPassword = md5(state.password);

        axios.post(usersMethods.ADD_NEW_USER, {
            userLogin: state.login,
            userPassword: encryptedPassword,
            email: state.email,
            userTypeId: 0
        })
        .then((response) => completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });

    }

    // Выполнение запроса успешно завершено
    const completedSuccessfully = (response) => {
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}`);
            return;
        }

        alert("Регистрация успешно завершена.\nТеперь вы можете войти под своим логином и паролем.");
        window.location = "/login";
    }

    return(
        <div>
            <table className={styles.loginFieldsTable}>
                <tbody>
                    <tr>
                        <td colSpan="2"><h4>Регистрация нового пользователя</h4></td>
                    </tr>
                    <tr>
                        <td><div>Email</div></td>
                        <td><div><input name="email" onChange={handleTextInputChange} value={state.email} type="text"/></div></td>
                    </tr>
                    <tr>
                        <td><div>Логин</div></td>
                        <td><div><input name="login" onChange={handleTextInputChange} value={state.login} type="text"/></div></td>
                    </tr>
                    <tr>
                        <td><div>Пароль</div></td>
                        <td><div><input name="password" onChange={handleTextInputChange} value={state.password} type="password"/></div></td>
                    </tr>
                    <tr>
                        <td colSpan="2"><button onClick={handleSignUpButton}>Зарегистрироваться</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default SignUpModule;