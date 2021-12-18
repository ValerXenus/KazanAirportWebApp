import axios from 'axios';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { citiesMethods } from '../../../../HelperComponents/ApiUrls';
import InputValidations from '../../../../HelperComponents/Logic/InputValidations';

export class AddCityModal extends Component {
    constructor(props){
        super(props);

        this.state = {
            cityInfo: {
                id: 0,
                cityName: "",
                icaoCode: "",
                iataCode: ""
            },
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.cityInfo.id)){
            this.fillState(nextProps);
        }
    }
    
    // Событие нажатия кнопки "Сохранить"
    handleSubmit = (event) => {
        event.preventDefault();

        if (!this.validateInputs())
            return;

        if (this.state.isEditMode) {
            this.updateInfo();
            return;
        }
        
        axios.post(citiesMethods.ADD_NEW_CITY, {
            Name: this.state.cityInfo.cityName,
            IcaoCode: this.state.cityInfo.icaoCode,
            IataCode: this.state.cityInfo.iataCode
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateInfo = () => {
        axios.post(citiesMethods.UPDATE_CITY, {
            Id: this.state.cityInfo.id,
            Name: this.state.cityInfo.cityName,
            IcaoCode: this.state.cityInfo.icaoCode,
            IataCode: this.state.cityInfo.iataCode
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
            cityInfo: {
                id: 0,
                cityName: "",
                icaoCode: "",
                iataCode: ""
            }
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            cityInfo: {                   
                ...prevState.cityInfo,
                [event.target.name]: event.target.value
            }
        }))
    }

    // Проверка правильности заполнения полей
    validateInputs = () => {
        let outcome = "";

        if (!InputValidations.validateIcaoCode(this.state.cityInfo.icaoCode))
            outcome += "ICAO код должен состоять из 4 прописных букв\n";

        if (!InputValidations.validateIataCode(this.state.cityInfo.iataCode))
            outcome += "IATA код должен состоять из 3 прописных букв\n";

        if (outcome.length > 0) {
            alert(outcome);
            return false;
        }

        return true;
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
            return;

        this.setState({
            cityInfo: props.editInfo,
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
                        Данные о городе
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="CityNameText">
                                        <Form.Label>Название</Form.Label>
                                        <Form.Control type="text"
                                            name="cityName" required
                                            onChange={this.handleChanged}
                                            value={this.state.cityInfo.cityName}/>
                                    </Form.Group>
                                    <Form.Group controlId="IcaoCodeText">
                                        <Form.Label>ICAO</Form.Label>
                                        <Form.Control type="text"
                                            name="icaoCode" required
                                            onChange={this.handleChanged}
                                            value={this.state.cityInfo.icaoCode}/>
                                    </Form.Group>
                                    <Form.Group controlId="IataCodeText">
                                        <Form.Label>IATA</Form.Label>
                                        <Form.Control type="text"
                                            name="iataCode"
                                            onChange={this.handleChanged}
                                            value={this.state.cityInfo.iataCode}/>
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