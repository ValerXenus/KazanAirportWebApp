import React, { Component } from 'react';
import { Table } from 'react-bootstrap';
import { flightsMethods } from '../../HelperComponents/ApiUrls';
import axios from "axios";
import UtilityMethods from "../../HelperComponents/Logic/UtilityMethods";

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
            if (response.data === null)
                return;
            
            this.setState({flightsList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    render() {
        const { flightsList } = this.state;
        
        return(
            <div>
                <h4>Вылет</h4>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Авиакомпания</th>
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Самолет</th>
                            <th>Отправление по расписанию</th>
                            <th>Отправление фактическое</th>
                            <th>Статус</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {flightsList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.airlineName}</td>
                            <td>{x.flightNumber}</td>
                            <td>{x.cityName}</td>
                            <td>{x.planeName}</td>
                            <td>{UtilityMethods.convertDateTime(x.scheduledDateTime)}</td>
                            <td>{UtilityMethods.convertDateTime(x.actualDateTime)}</td>                          
                            <td>{x.statusName}</td>
                        </tr>)}
                    </tbody>
                </Table>
            </div>
        );
    }
}