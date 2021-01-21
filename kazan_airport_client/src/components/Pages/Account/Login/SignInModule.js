import axios from 'axios';
import md5 from 'md5';
import React, { useState } from 'react';
import { NavLink } from 'react-router-dom';
import InputValidations from '../../../HelperComponents/Logic/InputValidations';
import Cookies from 'js-cookie';
import styles from './Login.module.css';

const SignInModule = () => {

    // Временное хранилище
    const [state, setState] = useState({
        login: "",
        password: ""
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
        errorText += InputValidations.validateRequiredField(state.password, "Пароль");

        if (errorText.length !== 0) {
            alert(`Некоторые поля заполнены не верно. Подробности:\n${errorText}`)
            return false;
        }

        return true;
    }

    // Обработчик кнопки "Войти"
    const handleSignInButton = () => {
        if (!validateInputs())
            return;

        let encryptedPassword = md5(state.password);

        axios.post("https://localhost:44377/api/UserAccount/LoginUser", {
            login: state.login,
            passWord: encryptedPassword
        })
        .then((response) => completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}.`);
        });
    }

    // Выполнение запроса успешно завершено
    const completedSuccessfully = (response) => {
        if (response.data === null){
            alert("Неправильный логин или пароль");
            return;
        }

        let userData = response.data;
        if (userData === undefined) {
            alert("Неправильный логин или пароль");
            return;
        }

        Cookies.set("authData", JSON.stringify({
            userId: userData.id,
            role: userData.userTypeId,
            isAuth: true
        }));

        alert("Вход выполнен успешно.\n");
        window.location = "/";
    }

    return(
        <div>
            <table className={styles.loginFieldsTable}>
                <tbody>
                    <tr>
                        <td colSpan="2"><h4>Авторизация</h4></td>
                    </tr>
                    <tr>
                        <td><div>Логин</div></td>
                        <td><div><input name="login" value={state.login} onChange={handleTextInputChange} type="text"/></div></td>
                    </tr>
                    <tr>
                        <td><div>Пароль</div></td>
                        <td><div><input name="password" value={state.password} onChange={handleTextInputChange} type="password"/></div></td>
                    </tr>
                    <tr>
                        <td colSpan="2"><button onClick={handleSignInButton}>Войти</button></td>
                    </tr>
                    <tr>
                        <td colSpan="2"><NavLink to="/login/register">Создать новый аккаунт</NavLink></td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default SignInModule;