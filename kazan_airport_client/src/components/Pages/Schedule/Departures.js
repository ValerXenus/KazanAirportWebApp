import React, { Component } from 'react';
import { Table } from 'react-bootstrap';
import { flightsMethods } from '../../HelperComponents/ApiUrls';
import axios from "axios";

export class Departures extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flightsList: []
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

    render() {
        const { flightsList } = this.state;

        const getNoteAboutFlight = (currentFlight) => {
            switch (currentFlight.statusId) {
                case 0:
                    if (Date.now() > Date.parse(currentFlight.departureScheduled))
                        return "Задержка взлета";
                        break;
                case 1:
                    if (Date.now() > Date.parse(currentFlight.arrivalScheduled))
                        return "Задерживается";
                        break;
            }

            return null;
        }

        return(
            <div>
                <h4>Вылет</h4>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Отправление</th>
                            <th>Прибытие</th>
                            <th>Авиакомпания</th>
                            <th>Статус</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.flightNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.departureScheduled}</td>
                            <td>{x.arrivalScheduled}</td>
                            <td>{x.airlineName}</td>
                            <td>{x.statusName}</td>
                            <td>{getNoteAboutFlight(x)}</td>
                        </tr>)}
                    </tbody>
                </Table>
            </div>
        );
    }
}