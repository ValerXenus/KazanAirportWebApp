import React, { Component } from 'react';
import { Table } from 'react-bootstrap';
import { flightsMethods } from '../../HelperComponents/ApiUrls';
import styles from './Schedule.module.css';
import mlIcon from '../../../images/icons/ml_icon.png';
import axios from "axios";
import UtilityMethods from '../../HelperComponents/Logic/UtilityMethods';

export class Arrivals extends Component {
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
        axios.post(flightsMethods.GET_ARRIVAL_FLIGHTS)
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

        const renderActualTime = (date, isPredicted) => {
            if (isPredicted)
                return (
                    <div>
                        {date}
                        <img src={mlIcon} className={styles.mlLogoStyle} alt={"Рассчитано при помощи модели машинного обучения"}/>
                    </div>
                );

            return (
                <div>{date}</div>
            );
        }

        return(
            <div>
                <h4>Прилет</h4>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Авиакомпания</th>
                            <th>Рейс</th>
                            <th>Город</th>
                            <th>Самолет</th>
                            <th>Прибытие по расписанию</th>
                            <th>Прибытие фактическое</th>
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
                            <td width={160}>{renderActualTime(UtilityMethods.convertDateTime(x.actualDateTime), x.isPredicted)}</td>
                            <td>{x.statusName}</td>
                        </tr>)}
                    </tbody>
                </Table>
            </div>
        );
    }
}