import React from 'react';
import { NavLink } from 'react-router-dom';
import styles from './Login.module.css'

const SignInModule = () => {

    const handleSignInButton = () => {
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
                        <td><div><input id="signInLogin" type="text"/></div></td>
                    </tr>
                    <tr>
                        <td><div>Пароль</div></td>
                        <td><div><input id="signInPassword" type="password"/></div></td>
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