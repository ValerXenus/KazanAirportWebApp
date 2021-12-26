import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { flightsMethods } from '../../../../HelperComponents/ApiUrls';
import UtilityMethods from '../../../../HelperComponents/Logic/UtilityMethods';
import styles from './ManageFlights.module.css';

export class ManageFlightsWeb extends Component {
    constructor(props) {
        super(props);
        this.state = {
            departureflightsList: [],
            arrivalFlightsList: [],
            arrivalSelected: false
        };
    }

    // Выполняется, когда все компоненты были отрендерены
    componentDidMount() {
        this.refreshList();
    }

    /**
     * Обновление списка авиарейсов
     */
    refreshList = () => {
        axios.post(flightsMethods.GET_ALL_FLIGHTS)
        .then(response => {
            if (response.data === null)
                return;

            this.setState({
                departureflightsList: response.data.Departures,
                arrivalFlightsList: response.data.Arrivals
            });
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

    /**
     * Проверка выполнения запроса
     * @param {*} response 
     * @returns 
     */
    completedSuccessfully = (response) => {
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}`);
            return;
        }

        alert("Рейс успешно сохранен");
    }

    handleFlightsButtonClick = (type) => {
        if (type === this.state.arrivalSelected)
            return;
        
        this.setState({ arrivalSelected: type });
    }

    render() {
        const { departureflightsList } = this.state;
        const { arrivalFlightsList } = this.state;
        const { arrivalSelected } = this.state;

        const departuresComponent = (
        <div>
            <h3 style={{textAlign: "center"}}>Вылетающие рейсы</h3>
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
                    {departureflightsList.map(x => 
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
                                    onClick={() => { this.saveFlight(x.Id); }}>
                                    Сохранить
                                </Button>
                            </ButtonToolbar>
                        </td>
                    </tr>)}
                </tbody>
            </Table>
        </div>);

        const arrivalsComponent = (
        <div>
            <h3 style={{textAlign: "center"}}>Прилетающие рейсы</h3>
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
                    {arrivalFlightsList.map(x => 
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
                                    onClick={() => { this.saveFlight(x.Id); }}>
                                    Сохранить
                                </Button>
                            </ButtonToolbar>
                        </td>
                    </tr>)}
                </tbody>
            </Table>
        </div>);

        const selectFlightsPanel = () => {
            if (arrivalSelected)
                return arrivalsComponent;
            
            return departuresComponent;
        }

        return(
        <table>
            <tbody>
                <tr>
                    <td>
                        <ul className={styles.buttonsContainer}>
                            <li className={styles.selectButton} onClick={() => { this.handleFlightsButtonClick(false); }}>Вылет</li>
                            <li className={styles.selectButton} onClick={() => { this.handleFlightsButtonClick(true); }}>Прилет</li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>
                        {selectFlightsPanel()}
                    </td>
                </tr>
            </tbody>
        </table>            
        );
    }
}