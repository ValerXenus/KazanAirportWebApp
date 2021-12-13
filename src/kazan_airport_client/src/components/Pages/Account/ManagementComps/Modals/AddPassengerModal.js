import axios from 'axios';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { passengersMethods } from '../../../../HelperComponents/ApiUrls';

export class AddPassengerModal extends Component {
    constructor(props){
        super(props);

        this.state = {
            passengerInfo: {
                id: 0,
                lastName: "",
                firstName: "",
                middleName: "",
                passportNumber: "",
                userLogin: ""
            },
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.passengerInfo.id)){
            this.fillState(nextProps);
        }
    }

    // Событие нажатия кнопки "Сохранить"
    handleSubmit = (event) => {
        event.preventDefault();

        if (this.state.isEditMode) {
            this.updateInfo();
            return;
        }
        
        axios.post(passengersMethods.ADD_NEW_PASSENGER, {
            lastName: this.state.passengerInfo.lastName,
            firstName: this.state.passengerInfo.firstName,
            middleName: this.state.passengerInfo.middleName,
            passportNumber: this.state.passengerInfo.passportNumber,
        }, {
            params: { userLogin: this.state.passengerInfo.userLogin }
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateInfo = () => {
        axios.post(passengersMethods.UPDATE_PASSENGER, {
            id: this.state.passengerInfo.id,
            lastName: this.state.passengerInfo.lastName,
            firstName: this.state.passengerInfo.firstName,
            middleName: this.state.passengerInfo.middleName,
            passportNumber: this.state.passengerInfo.passportNumber,
            login: this.state.passengerInfo.userLogin
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
            passengerInfo: {
                id: 0,
                lastName: "",
                firstName: "",
                middleName: "",
                passportNumber: "",
                userLogin: ""
            }
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            passengerInfo: {                   
                ...prevState.passengerInfo,
                [event.target.name]: event.target.value
            }
        }))
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
            return;

        this.setState({
            passengerInfo: props.editInfo,
            isEditMode: true
        });
    }

    render() {

        return(
            <Modal
                {...this.props}
                size="lg"
                aria-labelledby="contained-modal-title-vcenter"
                centered>
                <Modal.Header closeButton>
                    <Modal.Title id="contained-modal-title-vcenter">
                        Данные пассажира
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="LastNameText">
                                        <Form.Label>Фамилия</Form.Label>
                                        <Form.Control type="text"
                                            name="lastName" required
                                            onChange={this.handleChanged}
                                            value={this.state.passengerInfo.lastName}/>
                                    </Form.Group>
                                    <Form.Group controlId="FirstNameText">
                                        <Form.Label>Имя</Form.Label>
                                        <Form.Control type="text"
                                            name="firstName" required
                                            onChange={this.handleChanged}
                                            value={this.state.passengerInfo.firstName}/>
                                    </Form.Group>
                                    <Form.Group controlId="MiddleNameText">
                                        <Form.Label>Отчество</Form.Label>
                                        <Form.Control type="text"
                                            name="middleName"
                                            onChange={this.handleChanged}
                                            value={this.state.passengerInfo.middleName}/>
                                    </Form.Group>
                                    <Form.Group controlId="PassportNumberText">
                                        <Form.Label>Номер паспорта</Form.Label>
                                        <Form.Control type="text"
                                            name="passportNumber" required
                                            onChange={this.handleChanged}
                                            value={this.state.passengerInfo.passportNumber}/>
                                    </Form.Group>
                                    <Form.Group controlId="UserSelectText">
                                        <Form.Label>Логин пользователя</Form.Label>
                                        <Form.Control type="text"
                                            name="userLogin"
                                            onChange={this.handleChanged}
                                            value={this.state.passengerInfo.userLogin}/>
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