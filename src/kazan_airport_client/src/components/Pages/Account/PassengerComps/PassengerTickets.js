import React, { Component } from 'react';
import axios from 'axios';
import { ticketsMethods } from '../../../HelperComponents/ApiUrls';
import Cookies from 'js-cookie';
import { TicketBody } from "./TicketBody.js"

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
                <table>
                    <thead>
                        <tr>
                            <th>
                                <h3>Список билетов</h3>
                            </th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                {ticketsList.map(x =>
                                    <TicketBody ticketItem={{
                                        flightNumber: x.flightNumber,
                                        ticketNumber: x.ticketNumber,
                                        cityName: x.cityName,
                                        departureScheduled: x.departureScheduled,
                                        airlineName: x.airlineName,
                                        seatNumber: x.seatNumber
                                    }} />
                                )}
                            </td>
                        </tr>                        
                    </tbody>
                </table>           
            </div>
        );
    }
}