import axios from 'axios';
import React, { Component } from 'react';
import {Button, ButtonToolbar, Table} from 'react-bootstrap';
import { AddUserModal } from './Modals/AddUserModal';

export  class ManageUsers extends Component {
    constructor(props) {
        super(props);
        this.state = {
            usersList: [],
            addModalShow: false,
            editModalShow: false
        };
    }

    // Выполняется, когда все компоненты были отрендерены
    componentDidMount() {
        this.refreshList();
    }

    componentWillUnmount(){
        this.state = {};
    }

    // Выполняется, когда некоторые данные изменились
    componentDidUpdate() {
        this.refreshList();
    }

    // Обновление списка пользователей
    refreshList = () => {
        axios.post("https://localhost:44377/api/UserAccount/GetUsersList")
        .then(response => {
            this.setState({usersList: response.data})
        })
        .catch((error) => {
            alert(`Ошибка при получении данных: ${error}`);
        });
    }

    // Открытие модального окна для редактирования данных
    showModalEdit = (id) => {
        axios.post("https://localhost:44377/api/UserAccount/GetUserById", null, {
            params: {id: id}
        })
        .then(response => {
            this.setState({
                editModalShow: true,
                editUserInfo: {
                    id: id,
                    login: response.data.login,
                    email: response.data.email,
                    role: response.data.userTypeId
                }});
        })
        .catch((error) => {
            alert(`Произошла ошибка при получении данных: ${error}`);
        });
    }

    render() {
        const { usersList } = this.state;

        let modalClose = () => {
            this.setState({addModalShow: false, editModalShow: false});
        }

        return(
            <div>
                <Table className="mt-4 striped bordered hover" size="sm">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>Логин</th>
                            <th>Email</th>
                            <th>Тип пользователя</th>
                            <th/>
                        </tr>
                    </thead>
                    <tbody>
                        {usersList.map(x => 
                        <tr key = {x.id}>
                            <td>{x.id}</td>
                            <td>{x.login}</td>
                            <td>{x.email}</td>
                            <td>{x.typeName}</td>
                            <td>
                                <ButtonToolbar>
                                    <Button
                                        className="mr-2" variant="info"
                                        onClick={() => {
                                            this.showModalEdit(x.id);
                                        }}>
                                        Редактировать
                                    </Button>
                                    <Button
                                        className="mr-2 bg-danger" variant="info">
                                        Удалить
                                    </Button>
                                </ButtonToolbar>
                            </td>
                        </tr>)}
                    </tbody>
                </Table>
                <ButtonToolbar>
                    <Button variant="primary" onClick={() => this.setState({addModalShow: true})}>
                        Новый пользователь
                    </Button>
                    <AddUserModal
                        show={this.state.addModalShow}
                        onHide={modalClose} />
                    <AddUserModal
                        show={this.state.editModalShow}
                        editInfo={this.state.editUserInfo}
                        onHide={modalClose} />
                </ButtonToolbar>
            </div>
        );
    }
}