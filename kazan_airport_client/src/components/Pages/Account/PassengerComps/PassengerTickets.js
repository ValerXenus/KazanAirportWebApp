import React, { Component } from 'react';
import axios from 'axios';
import { Table } from 'react-bootstrap';
import { ticketsMethods } from '../../../HelperComponents/ApiUrls';
import Cookies from 'js-cookie';

export class PassengerTickets extends Component {
    constructor(props) {
        super(props);
        this.state = {
            ticketsList: []
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
        let authCookie = Cookies.get("authData");
        if (!authCookie) {
            return;
        }

        let currentSession = JSON.parse(authCookie);

        axios.post(ticketsMethods.GET_TICKET_FOR_PASSENGER, null, {
            params: {passengerId: currentSession.passengerId}
        })
        .then(response => {
            debugger;
            this.setState({ticketsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    render() {
        const { ticketsList } = this.state;

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Рейс</th>
                            <th>Номер билета</th>
                            <th>Город</th>
                            <th>Отправление</th>
                            <th>Прибытие</th>
                            <th>Авиакомпания</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {ticketsList.map(x => 
                        <tr key = {x.passengerId}>
                            <td>{x.flightNumber}</td>
                            <td>{x.ticketNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.departureScheduled}</td>
                            <td>{x.arrivalScheduled}</td>
                            <td>{x.airlineName}</td>
                        </tr>)}
                    </tbody>
                </Table>           
            </div>
        );
    }
}