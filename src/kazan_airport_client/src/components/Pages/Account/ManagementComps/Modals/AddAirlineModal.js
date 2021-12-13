import axios from 'axios';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { airlinesMethods } from '../../../../HelperComponents/ApiUrls';

export class AddAirlineModal extends Component {
    constructor(props){
        super(props);

        this.state = {
            airlineInfo: {
                id: 0,
                airlineName: ""
            },
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.airlineInfo.id)){
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
        
        axios.post(airlinesMethods.ADD_NEW_AIRLINE, {
            airlineName: this.state.airlineInfo.airlineName
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateInfo = () => {
        axios.post(airlinesMethods.UPDATE_AIRLINE, {
            id: this.state.airlineInfo.id,
            airlineName: this.state.airlineInfo.airlineName
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
            airlineInfo: {
                id: 0,
                airlineName: ""
            }
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            airlineInfo: {                   
                ...prevState.airlineInfo,
                [event.target.name]: event.target.value
            }
        }))
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
            return;

        this.setState({
            airlineInfo: props.editInfo,
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
                        Данные об авиакомпании
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="AirlineNameText">
                                        <Form.Label>Название</Form.Label>
                                        <Form.Control type="text"
                                            name="airlineName" required
                                            onChange={this.handleChanged}
                                            value={this.state.airlineInfo.airlineName}/>
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