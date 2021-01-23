import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { airlinesMethods } from '../../../HelperComponents/ApiUrls';
import { AddAirlineModal } from './Modals/AddAirlineModal';

export class ManageAirlines extends Component {
    constructor(props) {
        super(props);
        this.state = {
            airlinesList: [],
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
        axios.post(airlinesMethods.GET_AIRLINES_LIST)
        .then(response => {
            this.setState({airlinesList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post(airlinesMethods.GET_AIRLINE_BY_ID, null, {
            params: {airlineId: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editRecord: {
                    id: id,
                    airlineName: response.data.airlineName
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    removeRecord = (id) => {
        if (window.confirm("Вы действитель хотите удалить запись?")) {
            axios.post(airlinesMethods.REMOVE_AIRLINE, null, {
                params: {airlineId: id}
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
        const { airlinesList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Авиакомпания</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {airlinesList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.id}</td>
                            <td>{x.airlineName}</td>
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
                        Добавить авиакомпанию
                    </Button>
                    <AddAirlineModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddAirlineModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editRecord}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}