import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { citiesMethods } from '../../../HelperComponents/ApiUrls';
import { AddCityModal } from './Modals/AddCityModal';

export class ManageCities extends Component {
    constructor(props) {
        super(props);
        this.state = {
            citiesList: [],
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

    // Обновление списка пассажиров
    refreshList = () => {
        axios.post(citiesMethods.GET_CITIES_LIST)
        .then(response => {
            this.setState({citiesList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post(citiesMethods.GET_CITY_BY_ID, null, {
            params: {cityId: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editRecord: {
                    id: id,
                    cityName: response.data.cityName,
                    icaoCode: response.data.icaoCode,
                    iataCode: response.data.iataCode
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    removeRecord = (id) => {
        if (window.confirm("Вы действитель хотите удалить запись?")) {
            axios.post(citiesMethods.REMOVE_CITY, null, {
                params: {cityId: id}
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
        const { citiesList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Город</th>
                            <th>ICAO</th>
                            <th>IATA</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {citiesList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.id}</td>
                            <td>{x.cityName}</td>
                            <td>{x.icaoCode}</td>
                            <td>{x.iataCode}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => { this.showModalEdit(x.id); }}>
                                        Редактировать
                                    </Button>
                                    <Button
                                        className="mr-2 bg-danger" variant="info"
                                        onClick={() => { this.removeRecord(x.id); }}>
                                        Удалить
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant="info" onClick={() => this.setState({addModalShow: true})}>
                        Добавить город
                    </Button>
                    <AddCityModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddCityModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editRecord}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}