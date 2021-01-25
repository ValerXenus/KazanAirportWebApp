import React, { Component } from 'react';
import { Button, ButtonToolbar, Table, Form } from 'react-bootstrap';
import { flightsMethods, passengersMethods, ticketsMethods } from '../../HelperComponents/ApiUrls';
import styles from './BuyTicket.module.css';
import axios from "axios";
import Cookies from 'js-cookie';

export class BuyTicket extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flightsList: [],
            selectedFlightId: 0,
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

    // Выполняется, когда некоторые данные изменились
    componentDidUpdate() {
        this.refreshList();
    }

    // Обновление списка
    refreshList = () => {
        axios.post(flightsMethods.GET_DEPARTURE_FLIGHTS)
        .then(response => {
            this.setState({flightsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Обработчик кнопки "Выбрать рейс"
    selectFlight = (flight) => {
        flight.isSelected = true;

        if (this.state.selectedFlightId !== 0){
            this.setState(prevState => ({
                flightsList: prevState.flightsList.map(x => (x.id === flight.id 
                    ? Object.assign(x, { isSelected: false })
                    : x))
            }));
        }

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

    // Метод покупки билета
    buyTicket = () => {        
        axios.post(ticketsMethods.CREATE_TICKET, {
            passengerId: this.state.passengerInfo.id,
            flightId: this.state.selectedFlightId
        })
        .then((response) => {
            console.log("BuyTicket");
            console.log(response);
            this.completedSuccessfully(response); 
            window.location = "/";
        })
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    // Получение id добавленного пассажира
    getPassengerForTicket = () => {
        axios.post(passengersMethods.GET_PASSENGER_BY_PASSPORT, null, {
            params: { passportNumber: this.state.passengerInfo.passportNumber }
        })
        .then((response) => {
            console.log("getPassengerForTicket");
            console.log(response);
            this.completedSuccessfully(response); 
            this.setState(prevState => ({
                passengerInfo: {                   
                    ...prevState.passengerInfo,
                    passengerId: response.data.id
                }
            }))            
            this.buyTicket(); 
        })
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    // Когда запрос выполнился без ошибок
    completedSuccessfully = (response) => {
        console.log(response);
        debugger;
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}`);
            return;
        }
    }

    // Обработчик кнопки "Купить билет"
    handleSubmit = () => {
        if (this.state.selectedFlightId === 0) {
            alert("Выберите рейс, чтобы приступить к покупке билета");
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
                this.setState(prevState => ({
                    passengerInfo: {                   
                        ...prevState.passengerInfo,
                        passengerId: currentSession.passengerId
                    }
                }))
            }
        } else {
            this.addNewPassenger();
            return;
        }

        this.buyTicket();
    }

    render() {
        const { flightsList } = this.state;

        const getClassIfActive = (flight) => {
            if (flight.isSelected === undefined)
                return null;

            if (flight.isSelected)
                return styles.selectedFlight;

            return null;
        }

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
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Отправление</th>
                            <th>Прибытие</th>
                            <th>Авиакомпания</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x => 
                        <tr key = {x.id} className={getClassIfActive(x)}>
                            <td>{x.flightNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.departureScheduled}</td>
                            <td>{x.arrivalScheduled}</td>
                            <td>{x.airlineName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => { this.selectFlight(x); }}>
                                        Выбрать рейс
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
                <hr/>
                <div>                    
                    <Form onSubmit={this.handleSubmit}>
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
                                    type="submit">
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