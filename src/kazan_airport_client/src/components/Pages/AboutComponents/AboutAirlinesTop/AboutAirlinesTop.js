import React, { Component } from 'react';
import { Table } from 'react-bootstrap';
import { airlinesMethods } from '../../../HelperComponents/ApiUrls';
import axios from "axios";

export class AboutAirlinesTop extends Component {
    constructor(props) {
        super(props);
        this.state = {
            airlinesList: []
        };
    }

    // Выполняется, когда все компоненты были отрендерены
    componentDidMount() {
        this.refreshList();
    }

    // Обновление списка
    refreshList = () => {
        axios.post(airlinesMethods.GET_AIRLINES_RATING)
        .then(response => {
            if (response.data === null)
                return;

            this.setState({airlinesList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    render() {
        const { airlinesList } = this.state;

        return(
            <div>
                <h3>Рейтинг авиакомпаний</h3>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>Авиакомпания</th>
                            <th>Среднее время задержки</th>                    
                        </tr>
                    </thead>
                    <tbody>
                        {airlinesList.map(x => 
                        <tr>
                            <td>{x.name}</td>
                            <td>{x.delayTime}</td>                            
                        </tr>)}
                    </tbody>
                </Table>
            </div>
        );
    }
}