import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { flightsMethods } from '../../../HelperComponents/ApiUrls';
import UtilityMethods from '../../../HelperComponents/Logic/UtilityMethods';

export class ManageFlights extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flightsList: [],
            editModalShow: false
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
            if (response.data === null)
                return;

            this.setState({flightsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post(flightsMethods.GET_FLIGHT_BY_ID, null, {
            params: {flightId: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editRecord: {
                    id: id,
                    flightNumber: response.data.FlightNumber,
                    departureScheduled: response.data.DepartureScheduled,
                    arrivalScheduled: response.data.ArrivalScheduled,
                    departureActual: response.data.DepartureActual,
                    arrivalActual: response.data.arrivalActual,
                    timeOnBoard: response.data.TimeOnBoard,
                    flightType: response.data.FlightType,
                    planeId: response.data.PlaneId,
                    cityId: response.data.CityId,
                    statusId: response.data.StatusId
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
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
                        <tr key = {x.Id}>
                            <td>{x.Id}</td>
                            <td>{x.AirlineName}</td>
                            <td>{x.FlightNumber}</td>
                            <td>{x.CityName}</td>
                            <td>{x.PlaneName}</td>
                            <td>{UtilityMethods.convertDateTime(x.ScheduledDateTime)}</td>
                            <td>{UtilityMethods.convertDateTime(x.ActualDateTime)}</td>
                            <td>{x.StatusName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => { this.showModalEdit(x.Id); }}>
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