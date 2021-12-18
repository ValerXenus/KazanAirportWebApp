import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { flightsMethods } from '../../../HelperComponents/ApiUrls';
import { AddFlightModal } from './Modals/AddFlightModal';

export class ManageFlights extends Component {
    constructor(props) {
        super(props);
        this.state = {
            flightsList: [],
            addModalShow: false,
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
        axios.post(flightsMethods.GET_FLIGHTS_LIST)
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

    removeRecord = (id) => {
        if (window.confirm("Вы действительно хотите удалить запись?")) {
            axios.post(flightsMethods.REMOVE_FLIGHT, null, {
                params: {flightId: id}
            })
            .then((response) => {
                if (response.data !== "Success") {
                    alert(`Ошибка:\n${response.data}`);
                }
            })
            .catch((error) => {
                alert(`Ошибка при отправке данных: ${error}`);
            });
        }        
    }

    render() {
        const { flightsList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        let getFlightType = (flightType) => {
            if (flightType === 0) 
                return "Вылет";
            
            return "Прилет";
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Тип</th>
                            <th>Номер рейса</th>
                            <th>Отправление по расписанию</th>
                            <th>Прибытие по расписанию</th>
                            <th>Отправление фактическое</th>
                            <th>Прибытие фактическое</th>
                            <th>Время в пути</th>                            
                            <th>Самолет</th>
                            <th>Авиакомпания</th>
                            <th>Город</th>
                            <th>Статус</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x => 
                        <tr key = {x.Id}>
                            <td>{x.Id}</td>
                            <td>{getFlightType(x.FlightType)}</td>
                            <td>{x.FlightNumber}</td>
                            <td>{x.DepartureScheduled}</td>
                            <td>{x.ArrivalScheduled}</td>
                            <td>{x.DepartureActual}</td>
                            <td>{x.ArrivalActual}</td>
                            <td>{x.TimeOnBoard}</td>
                            <td>{x.BoardNumber}</td>
                            <td>{x.AirlineName}</td>
                            <td>{x.CityName}</td>
                            <td>{x.StatusName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => { this.showModalEdit(x.Id); }}>
                                        Редактировать
                                    </Button>
                                    <Button
                                        className="mr-2 bg-danger" variant="info"
                                        onClick={() => { this.removeRecord(x.Id); }}>
                                        Удалить
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant="info" onClick={() => this.setState({addModalShow: true})}>
                        Добавить рейс
                    </Button>                    
                    <AddFlightModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddFlightModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editRecord}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}