import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { savedFlightsMethods } from '../../../HelperComponents/ApiUrls';
import UtilityMethods from '../../../HelperComponents/Logic/UtilityMethods';

export class ManageSavedFlights extends Component {
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

    // Обновление списка
    refreshList = () => {
        axios.post(savedFlightsMethods.GET_DEPARTURE_FLIGHTS)
        .then(response => {
            if (response.data === null)
                return;

            this.setState({flightsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    /**
     * Сохранение выбранного авиарейса
     * @param {Идентификатор рейса} id 
     */
    removeFlight = (id) => {
        axios.post(savedFlightsMethods.REMOVE_FLIGHT, null, {
            params: {flightId: id}
        })
        .then(response => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    // Когда запрос выполнился без ошибок
    completedSuccessfully = (response) => {
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}`);
            return;
        }

        this.refreshList();
    }

    render() {
        const { flightsList } = this.state;

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Авиакомпания</th>
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Самолет</th> 
                            <th>Время по расписанию</th>
                            <th>Время фактическое</th>                                                                                   
                            <th>Статус</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.id}</td>
                            <td>{x.airlineName}</td>
                            <td>{x.flightNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.planeName}</td>
                            <td>{UtilityMethods.convertDateTime(x.scheduledDateTime)}</td>
                            <td>{UtilityMethods.convertDateTime(x.actualDateTime)}</td>
                            <td>{x.statusName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2 bg-danger" variant="info"
                                        onClick={() => { this.removeFlight(x.id); }}>
                                        Удалить
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
            </div>
        );
    }
}