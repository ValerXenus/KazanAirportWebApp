import React, { Component } from 'react';
import { Button, ButtonToolbar, Table, Form } from 'react-bootstrap';
import { savedFlightsMethods, passengersMethods, ticketsMethods } from '../../HelperComponents/ApiUrls';
import styles from './BuyTicket.module.css';
import axios from "axios";
import Cookies from 'js-cookie';
import UtilityMethods from '../../HelperComponents/Logic/UtilityMethods';

export class BuyTicket extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flightsList: [],
            selectedFlightId: -1,
            isAborted: false,
            passengerInfo: {
                id: 0,
                lastName: "",
                firstName: "",
                middleName: "",
                passportNumber: ""
            }
        };
    }

    // Выполняется, когда все компоненты были отрендерены
    componentDidMount() {
        this.refreshList();
    }

    // Обновление списка
    refreshList = () => {
        axios.post(savedFlightsMethods.GET_DEPARTURE_FLIGHTS, null, {
            params: {todayOnly: true}
        })
        .then(response => {
            if (response.data === null)
                return;

            this.setState({flightsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Обработчик кнопки "Выбрать рейс"
    selectFlight = (flight) => {
        this.setState({
            selectedFlightId: flight.id
        });
        this.refreshList();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            passengerInfo: {                   
                ...prevState.passengerInfo,
                [event.target.name]: event.target.value
            }
        }))
    }

    // Добавление нового пассажира и возврат id
    addNewPassenger = (userLogin = "") => {
        if (this.state.isAborted)
            return;

        axios.post(passengersMethods.ADD_NEW_PASSENGER, {
            lastName: this.state.passengerInfo.lastName,
            firstName: this.state.passengerInfo.firstName,
            middleName: this.state.passengerInfo.middleName,
            passportNumber: this.state.passengerInfo.passportNumber,
        }, {
            params: { userLogin: userLogin }
        })
        .then((response) => {
            console.log("AddNewPassenger");
            this.completedSuccessfully(response);
            this.getPassengerForTicket();
        })
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    /**
     * Покупка билета
     * @returns 
     */
    buyTicket = (passengerId) => {    
        if (this.state.isAborted)
            return;

        let id = passengerId ? passengerId : this.state.passengerInfo.id;

        axios.post(ticketsMethods.CREATE_TICKET, {
            passengerId: id,
            flightId: this.state.selectedFlightId
        })
        .then((response) => {
            this.completedSuccessfully(response);
            alert("Вы купили билет. \nПосмотреть информацию о билетах можно на странице в личном кабинете.");
            window.location = "/";
        })
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    // Получение id добавленного пассажира
    getPassengerForTicket = () => {
        if (this.state.isAborted)
            return;

        axios.post(passengersMethods.GET_PASSENGER_BY_PASSPORT, null, {
            params: { passportNumber: this.state.passengerInfo.passportNumber }
        })
        .then((response) => {
            if (!response.data){
                this.setState({ isAborted: true });
                return;
            }

            this.setState(prevState => ({
                passengerInfo: {                   
                    ...prevState.passengerInfo,
                    id: response.data.id
                }
            }));
            this.buyTicket(); 
        })
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    // Когда запрос выполнился без ошибок
    completedSuccessfully = (response) => {
        console.log(response);
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}\n${response}`);
            this.setState({ isAborted: true });
            return;
        }
    }

    // Обработчик кнопки "Купить билет"
    handleSubmit = () => {
        this.setState({ isAborted: false });

        if (this.state.selectedFlightId === -1) {
            alert("Выберите рейс, чтобы приступить к покупке билета");
            this.setState({ isAborted: true });
            return;            
        }

        let authCookie = Cookies.get("authData");
        if (authCookie) {
            let currentSession = JSON.parse(authCookie);

            // Если у пользователя нет пассажира, то добавляем его
            if (!currentSession.passengerId 
                || currentSession.passengerId === -1 
                || currentSession.passengerId === "-1"){
                this.addNewPassenger(currentSession.userLogin);
                return;
            } else {
                this.buyTicket(parseInt(currentSession.passengerId));
                return;
            }
        } else {
            this.addNewPassenger();
            return;
        }
    }

    getClassIfActive = (flightId) => {
        if (flightId === this.state.selectedFlightId)
            return styles.selectedFlight;

        return null;
    }

    render() {
        const { flightsList } = this.state;

        const isUserHasPassengerInfo = () => {
            let authCookie = Cookies.get("authData");
            if (!authCookie)
                return false;

            let currentSession = JSON.parse(authCookie);            
            if (currentSession.passengerId 
                && currentSession.passengerId !== "-1" 
                && currentSession.passengerId !== -1) {
                return true;
            }

            return false;
        }

        return(
            <div className={styles.pageContainer}>
                <h3 className={styles.centeredStyle}>Покупка билета</h3>
                <hr/>
                <h5 className={styles.centeredStyle}>Выберите рейс</h5>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Авиакомпания</th>
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Самолет</th>
                            <th>Вылет по расписанию</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x =>
                        <tr key = {x.id} className={this.getClassIfActive(x.id)}>
                            <td>{x.airlineName}</td>
                            <td>{x.flightNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.planeName}</td>
                            <td>{UtilityMethods.convertDateTime(x.scheduledDateTime)}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => { this.selectFlight(x); }}>
                                        Выбрать
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
                <hr/>
                <div>                    
                    <Form>
                        <div className={styles.passengerBlock} 
                            hidden={isUserHasPassengerInfo()}>
                            <h5 className={styles.centeredStyle}>Данные пассажира</h5>
                            <Form.Group controlId="LastNameText">
                                <Form.Label>Фамилия</Form.Label>
                                <Form.Control type="text"
                                    name="lastName" 
                                    required={!isUserHasPassengerInfo()}
                                    onChange={this.handleChanged}
                                    value={this.state.passengerInfo.lastName}/>
                            </Form.Group>
                            <Form.Group controlId="FirstNameText">
                                <Form.Label>Имя</Form.Label>
                                <Form.Control type="text"
                                    name="firstName" 
                                    required={!isUserHasPassengerInfo()}
                                    onChange={this.handleChanged}
                                    value={this.state.passengerInfo.firstName}/>
                            </Form.Group>
                            <Form.Group controlId="MiddleNameText">
                                <Form.Label>Отчество</Form.Label>
                                <Form.Control type="text"
                                    name="middleName"
                                    onChange={this.handleChanged}
                                    value={this.state.passengerInfo.middleName}/>
                            </Form.Group>
                            <Form.Group controlId="PassportNumbersText">
                                <Form.Label>Номер паспорта</Form.Label>
                                <Form.Control type="text"
                                    name="passportNumber" 
                                    required={!isUserHasPassengerInfo()}
                                    onChange={this.handleChanged}
                                    value={this.state.passengerInfo.passportNumber}/>
                            </Form.Group>
                        </div>      
                        <div className={styles.passengerBlock}>
                            <Form.Group>
                                <Button variant="primary"
                                    className="float-right"
                                    onClick={this.handleSubmit}>
                                    Купить билет
                                </Button>
                            </Form.Group>
                            <h5 className="float-left">Цена: 3000₽&nbsp;</h5>
                        </div>                        
                    </Form>  
                </div>
            </div>
        );
    }
}