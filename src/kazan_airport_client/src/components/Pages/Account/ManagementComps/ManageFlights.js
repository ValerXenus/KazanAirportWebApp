import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { flightsMethods } from '../../../HelperComponents/ApiUrls';
import UtilityMethods from '../../../HelperComponents/Logic/UtilityMethods';

export class ManageFlights extends Component {
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
        axios.post(flightsMethods.GET_DEPARTURE_FLIGHTS)
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
    saveFlight = (id) => {
        axios.post(flightsMethods.SAVE_FLIGHT, null, {
            params: {flightId: id, directionType: 1}
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

        alert("Рейс успешно сохранен");
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
                                        className="mr-2" variant="info"
                                        onClick={() => { this.saveFlight(x.id); }}>
                                        Сохранить
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