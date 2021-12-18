import axios from 'axios';
import React, { useState } from 'react';
import InputValidations from '../../HelperComponents/Logic/InputValidations';
import { ticketsMethods } from '../../HelperComponents/ApiUrls';
import styles from './OnlineRegistration.module.css';

const OnlineRegistration = () => {

    // Временное хранилище
    const [state, setState] = useState({
        passportNumber: "",
        ticketNumber: ""
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

        errorText += InputValidations.validateRequiredField(state.ticketNumber, "Номер билета");
        errorText += InputValidations.validateRequiredField(state.passportNumber, "Номер паспорта");        

        if (errorText.length !== 0) {
            alert(`Некоторые поля заполнены не верно. Подробности:\n${errorText}`)
            return false;
        }

        return true;
    }

    // Обработчик кнопки "Подтвердить"
    const handleCheckInButton = () => {
        if (!validateInputs())
            return;

        axios.post(ticketsMethods.ONLINE_REGISTER, null, {
            params: {
                passportNumber: state.passportNumber,
                ticketNumber: state.ticketNumber
            }
        })
        .then((response) => completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}.`);
        });
    }

    // Выполнение запроса успешно завершено
    const completedSuccessfully = (response) => {
        if (response.data === null){
            alert("Пассажир с таким номером паспорта не найден");
            return;
        }

        let userData = response.data;
        if (userData === undefined) {
            alert("Пассажир с таким номером паспорта не найден");
            return;
        }

        alert("Онлайн-регистрация пройдена успешно.\n");
        window.location = "/";
    }

    return(
        <div className={styles.pageContainer}>
            <table className={styles.pageFieldsTable}>
                <tbody>
                    <tr>
                        <td colSpan="2"><h4>Онлайн-регистрация</h4></td>
                    </tr>
                    <tr><td colSpan="2"><h6>Введите номер паспорта, чтобы подтвердить онлайн-регистрацию</h6></td></tr>
                    <tr>
                        <td><div>Номер билета</div></td>
                        <td><div><input name="ticketNumber" value={state.ticketNumber} onChange={handleTextInputChange} type="text"/></div></td>
                    </tr>
                    <tr>
                        <td><div>Номер паспорта</div></td>
                        <td><div><input name="passportNumber" value={state.passportNumber} onChange={handleTextInputChange} type="text"/></div></td>
                    </tr>
                    <tr>
                        <td colSpan="2"><button onClick={handleCheckInButton}>Подтвердить</button></td>
                    </tr>
                </tbody>
            </table>
        </div>
    );
}

export default OnlineRegistration;