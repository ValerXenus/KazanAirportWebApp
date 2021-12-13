import axios from 'axios';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { planesMethods } from '../../../../HelperComponents/ApiUrls';

export class AddPlaneModal extends Component {
    constructor(props){
        super(props);

        this.state = {
            planeInfo: {
                id: 0,
                modelName: "",
                boardNumber: "",
                seatsNumber: "",
                passportNumber: "",
                airlineId: ""
            },
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.planeInfo.id)){
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
        
        axios.post(planesMethods.ADD_NEW_PLANE, {
            modelName: this.state.planeInfo.modelName,
            boardNumber: this.state.planeInfo.boardNumber,
            seatsNumber: this.state.planeInfo.seatsNumber,
            passportNumber: this.state.planeInfo.passportNumber,
            airlineId: this.state.planeInfo.airlineId
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateInfo = () => {
        axios.post(planesMethods.UPDATE_PLANE, {
            id: this.state.planeInfo.id,
            modelName: this.state.planeInfo.modelName,
            boardNumber: this.state.planeInfo.boardNumber,
            seatsNumber: this.state.planeInfo.seatsNumber,
            passportNumber: this.state.planeInfo.passportNumber,
            airlineId: this.state.planeInfo.airlineId
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
            planeInfo: {
                id: 0,
                modelName: "",
                boardNumber: "",
                seatsNumber: "",
                passportNumber: "",
                airlineId: ""
            }
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            planeInfo: {                   
                ...prevState.planeInfo,
                [event.target.name]: event.target.value
            }
        }))
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
            return;

        this.setState({
            planeInfo: props.editInfo,
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
                        Данные о самолете
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="PlaneModelNameText">
                                        <Form.Label>Модель самолета</Form.Label>
                                        <Form.Control type="text"
                                            name="modelName" required
                                            onChange={this.handleChanged}
                                            value={this.state.planeInfo.modelName}/>
                                    </Form.Group>
                                    <Form.Group controlId="BoardNumberText">
                                        <Form.Label>Бортовой номер</Form.Label>
                                        <Form.Control type="text"
                                            name="boardNumber" required
                                            onChange={this.handleChanged}
                                            value={this.state.planeInfo.boardNumber}/>
                                    </Form.Group>
                                    <Form.Group controlId="SeatsNumberText">
                                        <Form.Label>Количество мест</Form.Label>
                                        <Form.Control type="text"
                                            name="seatsNumber"
                                            onChange={this.handleChanged}
                                            value={this.state.planeInfo.seatsNumber}/>
                                    </Form.Group>
                                    <Form.Group controlId="AirlineIdText">
                                        <Form.Label>ID авиакомпании</Form.Label>
                                        <Form.Control type="text"
                                            name="airlineId" required
                                            onChange={this.handleChanged}
                                            value={this.state.planeInfo.airlineId}/>
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