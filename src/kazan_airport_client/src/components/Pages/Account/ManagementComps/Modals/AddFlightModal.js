import axios from 'axios';
import React, {Component} from 'react';
import {Modal, Button, Row, Col, Form} from 'react-bootstrap';
import { flightsMethods } from '../../../../HelperComponents/ApiUrls';
import ruLocale from "date-fns/locale/ru";
import DateFnsUtils from "@date-io/date-fns";
import { MuiPickersUtilsProvider, KeyboardDateTimePicker } from '@material-ui/pickers';

export class AddFlightModal extends Component {
    constructor(props){
        super(props);

        this.state = {
            flightInfo: {
                id: 0,
                flightNumber: "",
                departureScheduled: "",
                arrivalScheduled: "",
                departureActual: "",
                arrivalActual: "",
                timeOnBoard: "",
                flightType: "0",
                planeId: "",
                cityId: "",
                statusId: "0"
            },
            isEditMode: false
        }
    }

    // Получение props. ToDo, заменить на обновленный
    UNSAFE_componentWillReceiveProps(nextProps) {
        if(nextProps.editInfo !== undefined && (nextProps.editInfo.id !== this.state.flightInfo.id)){
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
        
        axios.post(flightsMethods.ADD_NEW_FLIGHT, {
            FlightNumber: this.state.flightInfo.flightNumber,
            DepartureScheduled: this.state.flightInfo.departureScheduled,
            ArrivalScheduled: this.state.flightInfo.arrivalScheduled,
            DepartureActual: this.state.flightInfo.departureActual,
            ArrivalActual: this.state.flightInfo.arrivalActual,
            TimeOnBoard: this.state.flightInfo.timeOnBoard,
            FlightType: this.state.flightInfo.flightType,
            PlaneId: this.state.flightInfo.planeId,
            CityId: this.state.flightInfo.cityId,
            StatusId: this.state.flightInfo.statusId
        })
        .then((response) => this.completedSuccessfully(response))
        .catch((error) => {
            alert(`Ошибка при отправке данных: ${error}`);
        });
    }

    updateInfo = () => {
        axios.post(flightsMethods.UPDATE_FLIGHT, {
            Id: this.state.flightInfo.id,
            FlightNumber: this.state.flightInfo.flightNumber,
            DepartureScheduled: this.state.flightInfo.departureScheduled,
            ArrivalScheduled: this.state.flightInfo.arrivalScheduled,
            DepartureActual: this.state.flightInfo.departureActual,
            ArrivalActual: this.state.flightInfo.arrivalActual,
            TimeOnBoard: this.state.flightInfo.timeOnBoard,
            FlightType: this.state.flightInfo.flightType,
            PlaneId: this.state.flightInfo.planeId,
            CityId: this.state.flightInfo.cityId,
            StatusId: this.state.flightInfo.statusId
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
            flightInfo: {
                id: 0,
                flightNumber: "",
                departureScheduled: "",
                arrivalScheduled: "",
                departureActual: "",
                arrivalActual: "",
                timeOnBoard: "",
                flightType: "0",
                planeId: "",
                cityId: "",
                statusId: "0"
            }
        });
        this.props.onHide();
    }

    // Обработчик изменения текста в текстовых полях
    handleChanged = (event) => {
        this.setState(prevState => ({
            flightInfo: {                   
                ...prevState.flightInfo,
                [event.target.name]: event.target.value
            }
        }));
    }

    handleDepartureScheduledChanged = (newDate) => {
        this.setState(prevState => ({
            flightInfo: {
                ...prevState.flightInfo,
                departureScheduled: newDate
            }
        }));
    }

    handleArrivalScheduledChanged = (newDate) => {
        this.setState(prevState => ({
            flightInfo: {
                ...prevState.flightInfo,
                arrivalScheduled: newDate
            }
        }));
    }

    handleDepartureActualChanged = (newDate) => {
        this.setState(prevState => ({
            flightInfo: {
                ...prevState.flightInfo,
                departureActual: newDate
            }
        }));
    }

    handleArrivalActualChanged = (newDate) => {
        this.setState(prevState => ({
            flightInfo: {
                ...prevState.flightInfo,
                arrivalActual: newDate
            }
        }));
    }

    // Предзаполнение данных для редактирования
    fillState = (props) => {
        if (!props.editInfo)
            return;

        this.setState({
            flightInfo: props.editInfo,
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
                        Данные о рейсе
                    </Modal.Title>
                    <Modal.Body>
                    <div className="container">
                        <br/>
                        <Row>
                            <Col sm={6}>
                                <Form onSubmit={this.handleSubmit}>
                                    <Form.Group controlId="FlightNumberText">
                                        <Form.Label>Номер рейса</Form.Label>
                                        <Form.Control type="text"
                                            name="flightNumber" required
                                            onChange={this.handleChanged}
                                            value={this.state.flightInfo.flightNumber}/>
                                    </Form.Group>
                                    <Form.Group controlId="FlightNumberText">
                                        <Form.Label>Тип</Form.Label>
                                        <table>
                                            <tbody>
                                                <tr>
                                                    <td>
                                                        <input type="radio" name="flightType" 
                                                            value={0}
                                                            checked={this.state.flightInfo.flightType === "0" 
                                                                || this.state.flightInfo.flightType === 0} 
                                                            onChange={this.handleChanged}/>
                                                            &nbsp;Вылет
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <input type="radio" name="flightType" 
                                                            value={1}
                                                            checked={this.state.flightInfo.flightType === "1" 
                                                                || this.state.flightInfo.flightType === 1}
                                                            onChange={this.handleChanged}/>
                                                            &nbsp;Прилет
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>                            
                                    </Form.Group>
                                    <Form.Group controlId="DepartureScheduledText">
                                        <Form.Label>Отправление по расписанию</Form.Label>
                                        <MuiPickersUtilsProvider 
                                            utils={DateFnsUtils}
                                            locale={ruLocale}>
                                            <KeyboardDateTimePicker
                                                id="departureScheduled"
                                                ampm={false}
                                                format="dd-MM-yyyy HH:mm"
                                                value={this.state.flightInfo.departureScheduled}
                                                onChange={this.handleDepartureScheduledChanged}
                                            />
                                        </MuiPickersUtilsProvider>
                                    </Form.Group>
                                    <Form.Group controlId="ArrivalScheduledText">
                                        <Form.Label>Прибытие по расписанию</Form.Label>
                                        <MuiPickersUtilsProvider 
                                            utils={DateFnsUtils}
                                            locale={ruLocale}>
                                            <KeyboardDateTimePicker
                                                id="arrivalScheduled"
                                                ampm={false}
                                                format="dd-MM-yyyy HH:mm"
                                                value={this.state.flightInfo.arrivalScheduled}
                                                onChange={this.handleArrivalScheduledChanged}
                                            />
                                        </MuiPickersUtilsProvider>
                                    </Form.Group>
                                    <Form.Group controlId="DepartureActualText">
                                        <Form.Label>Отправление фактическое</Form.Label>
                                        <MuiPickersUtilsProvider 
                                            utils={DateFnsUtils}
                                            locale={ruLocale}>
                                            <KeyboardDateTimePicker
                                                id="departureActual"
                                                ampm={false}
                                                format="dd-MM-yyyy HH:mm"
                                                value={this.state.flightInfo.departureActual}
                                                onChange={this.handleDepartureActualChanged}
                                            />
                                        </MuiPickersUtilsProvider>
                                    </Form.Group>
                                    <Form.Group controlId="ArrivalActualText">
                                        <Form.Label>Прибытие фактическое</Form.Label>
                                        <MuiPickersUtilsProvider 
                                            utils={DateFnsUtils}
                                            locale={ruLocale}>
                                            <KeyboardDateTimePicker
                                                id="arrivalActual"
                                                ampm={false}
                                                format="dd-MM-yyyy HH:mm"
                                                value={this.state.flightInfo.arrivalActual}
                                                onChange={this.handleArrivalActualChanged}
                                            />
                                        </MuiPickersUtilsProvider>
                                    </Form.Group>
                                    <Form.Group controlId="TimeOnBoardText">
                                        <Form.Label>Время в пути</Form.Label>
                                        <Form.Control type="text"
                                            name="timeOnBoard" required
                                            onChange={this.handleChanged}
                                            value={this.state.flightInfo.timeOnBoard}/>
                                    </Form.Group>
                                    <Form.Group controlId="PlaneIdText">
                                        <Form.Label>ID самолета</Form.Label>
                                        <Form.Control type="text"
                                            name="planeId" required
                                            onChange={this.handleChanged}
                                            value={this.state.flightInfo.planeId}/>
                                    </Form.Group>
                                    <Form.Group controlId="CityIdText">
                                        <Form.Label>ID города</Form.Label>
                                        <Form.Control type="text"
                                            name="cityId" required
                                            onChange={this.handleChanged}
                                            value={this.state.flightInfo.cityId}/>
                                    </Form.Group>
                                    <Form.Group controlId="CityIdText">
                                        <Form.Label>Статус</Form.Label>
                                        <Form.Control as="select"
                                            name="statusId"
                                            value={this.state.flightInfo.statusId}
                                            onChange={this.handleChanged}>
                                            <option value={0} label="На стоянке"/>
                                            <option value={1} label="В пути"/>
                                            <option value={2} label="Приземлился"/>
                                            <option value={3} label="Отменен"/>
                                        </Form.Control>
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