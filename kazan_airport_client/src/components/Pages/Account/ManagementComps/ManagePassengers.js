import axios from 'axios';
import React, { Component } from 'react';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { passengersMethods } from '../../../HelperComponents/ApiUrls';
import { AddPassengerModal } from './Modals/AddPassengerModal';

export class ManagePassengers extends Component {
    constructor(props) {
        super(props);
        this.state = {
            passengersList: [],
            usersList: [],
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
        axios.post(passengersMethods.GET_PASSENGERS_LIST)
        .then(response => {
            this.setState({passengersList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post(passengersMethods.GET_PASSENGER_BY_ID, null, {
            params: {passengerId: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editRecord: {
                    id: id,
                    lastName: response.data.lastName,
                    firstName: response.data.firstName,
                    middleName: response.data.middleName,
                    passportNumber: response.data.passportNumber,
                    userLogin: response.data.login
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    removeRecord = (id) => {
        if (window.confirm("Вы действитель хотите удалить запись?")) {
            axios.post(passengersMethods.REMOVE_PASSENGER, null, {
                params: {passengerId: id}
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
        const { passengersList: passengersList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Фамилия</th>
                            <th>Имя</th>
                            <th>Отчество</th>
                            <th>Паспорт</th>
                            <th>Логин</th>
                            <th>Email</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {passengersList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.id}</td>
                            <td>{x.lastName}</td>
                            <td>{x.firstName}</td>
                            <td>{x.middleName}</td>
                            <td>{x.passportNumber}</td>
                            <td>{x.login}</td>
                            <td>{x.email}</td>
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
                    <Button variant="primary" onClick={() => this.setState({addModalShow: true})}>
                        Новый пассажир
                    </Button>
                    <AddPassengerModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddPassengerModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editRecord}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}