import React, { Component } from 'react';
import axios from 'axios';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { planesMethods } from '../../../HelperComponents/ApiUrls';
import { AddPlaneModal } from './Modals/AddPlaneModal';

export class ManagePlanes extends Component {
    constructor(props) {
        super(props);
        this.state = {
            planesList: [],
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
        axios.post(planesMethods.GET_PLANES_LIST)
        .then(response => {
            this.setState({planesList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post(planesMethods.GET_PLANE_BY_ID, null, {
            params: {planeId: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editRecord: {
                    id: id,
                    modelName: response.data.Name,
                    boardNumber: response.data.Number,
                    seatsNumber: response.data.SeatsNumber,
                    airlineId: response.data.AirlineId
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    removeRecord = (id) => {
        if (window.confirm("Вы действительно хотите удалить запись?")) {
            axios.post(planesMethods.REMOVE_PLANE, null, {
                params: {planeId: id}
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
        const { planesList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Модель</th>
                            <th>Бортовой номер</th>
                            <th>Количество мест</th>
                            <th>Авиакомпания</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {planesList.map(x => 
                        <tr key = {x.Id}>
                            <td>{x.Id}</td>
                            <td>{x.Name}</td>
                            <td>{x.Number}</td>
                            <td>{x.SeatsNumber}</td>
                            <td>{x.AirlineName}</td>
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
                        Добавить самолет
                    </Button>
                    <AddPlaneModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddPlaneModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editRecord}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}