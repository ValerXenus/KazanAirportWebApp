import axios from 'axios';
import md5 from 'md5';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { usersMethods } from '../../../../HelperComponents/ApiUrls';

export class AddUserModal extends Component {
    constructor(props){
        super(props);
        this.state = {
            id: 0,
            login: "",
            email: "",
            password: "",
            userTypeId: 0,
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.id)){
            this.fillState(nextProps);
        }        
    }

    // Событие нажатия кнопки "Сохранить"
    handleSubmit = (event) => {
        event.preventDefault();

        if (this.state.isEditMode) {
            this.updateUserInfo();
            return;
        }
        
        let encryptedPassword = md5(this.state.password);
        axios.post(usersMethods.ADD_NEW_USER, {
            UserLogin: this.state.login,
            UserPassword: encryptedPassword,
            Email: this.state.email,
            UserTypeId: this.state.userTypeId
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateUserInfo = () => {
        axios.post(usersMethods.UPDATE_USER, {
            Id: this.state.id,
            UserLogin: this.state.login,
            Email: this.state.email,
            UserTypeId: this.state.userTypeId
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    // Когда запрос выполнился без ошибок
    completedSuccessfully = (response) => {
        if (response.data !== "Success") {
            alert(`Ошибка:\n${response.data}`);
            return;
        }

        this.setState({
            id: 0,
            login: "",
            email: "",
            password: "",
            userTypeId: 0,
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleTextChanged = (event) => {
        this.setState({ [event.target.name] : event.target.value });
    }

    // Обработчик изменения выбранного типа пользователя
    handleUserTypeChange = (event) => {
        this.setState({ userTypeId: event.target.value });
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
        return;

        this.setState({
            id: props.editInfo.id,
            login: props.editInfo.login,
            email: props.editInfo.email,
            userTypeId: props.editInfo.role,
            isEditMode: true
        });
    }

    render() {
        let passwordBlock = () => {
            if (this.props.editInfo) {
                return null;
            }

            return (
                <Form.Group controlId="PasswordText">
                    <Form.Label>Пароль</Form.Label>
                    <Form.Control type="password"
                        name="password" required
                        onChange={this.handleTextChanged}
                        value={this.state.password}/>
                </Form.Group>
            );
        }

        return(
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Данные пользователя
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="LoginText">
                                        <Form.Label>Логин</Form.Label>
                                        <Form.Control type="text"
                                            name="login" required
                                            onChange={this.handleTextChanged}
                                            value={this.state.login}/>
                                    </Form.Group>
                                    {passwordBlock()}
                                    <Form.Group controlId="EmailText">
                                        <Form.Label>Email</Form.Label>
                                        <Form.Control type="text"
                                            name="email" required
                                            onChange={this.handleTextChanged}
                                            value={this.state.email}/>
                                    </Form.Group>
                                    <Form.Group controlId="UserTypeSelect">
                                        <Form.Label>Тип пользователя</Form.Label>
                                        <select value={this.state.userTypeId} 
                                            onChange={this.handleUserTypeChange}>
                                            <option value={0}>Пассажир</option>
                                            <option value={1}>Оператор</option>
                                            <option value={2}>Администратор</option>
                                        </select>
                                    </Form.Group>
                                    <Form.Group>
                                        <Button variant="primary"
                                            type="submit">
                                            Сохранить
                                        </Button>
                                    </Form.Group>
                                </Form>
                            </Col>
                        </Row>
                    </div>
                    </Modal.Body>
                </Modal.Header>
            </Modal>
        );
    }
}