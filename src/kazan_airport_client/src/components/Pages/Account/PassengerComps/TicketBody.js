import React, { Component } from 'react';
import Barcode from 'react-barcode';
import UtilityMethods from "../../../HelperComponents/Logic/UtilityMethods";
import whiteLogo from "../../../../images/icons/white_logo.svg";
import styles from './TicketStyles.module.css';

export class TicketBody extends Component {
    constructor(props) {
        super(props);
        this.state = {
            ticketItem: props.ticketItem
        };
    }

    render() {
        const { ticketItem } = this.state;

        return(
            <div className={styles.ticketContainer}>
                <table>
                    <tbody>
                        <tr className={styles.ticketHeader}>
                            <td colSpan={5} className={styles.tableHeader}>
                                <img src={whiteLogo} className={styles.logoStyle} alt="Аэропорт Казань" />
                            </td>                            
                        </tr>
                        <tr style={{height: 80}}>
                            <td/>
                            <td colSpan={4}>
                                <div className={styles.barcode}>
                                    <Barcode value={ticketItem.ticketNumber} />
                                </div>                               
                            </td>
                        </tr>                       
                        <tr>
                            <td style={{width: 30}}/>
                            <td style={{width: 110}}>
                                <div className={styles.infoCaption}>FROM/ОТ</div>
                                <div>КАЗАНЬ</div>
                            </td>
                            <td style={{width: 260}}>
                                <div className={styles.infoCaption}>AIRLINE/АВИАКОМПАНИЯ</div>
                                <div>{ticketItem.airlineName}</div>
                            </td>
                            <td style={{width: 140}}>
                                <div className={styles.infoCaption}>FLIGHT/РЕЙС</div>
                                <div>{ticketItem.flightNumber}</div>
                            </td>
                            <td style={{width: 295}}>
                                <div className={styles.infoCaption}>ДАТА И ВРЕМЯ/DATE AND TIME</div>
                                <div>{UtilityMethods.convertDateTime(ticketItem.departureScheduled)}</div>
                            </td>
                        </tr>
                        <tr style={{height: 120}}>
                            <td/>
                            <td>
                                <div className={styles.infoCaption}>TO/ДО</div>
                                <div/>
                                <div>{ticketItem.cityName}</div>
                            </td>
                            <td>
                                <div className={styles.infoCaption}>BOARDING TIME TILL</div>
                                <div className={styles.infoCaption}>ПОСАДКА ДО</div>
                                <div>{UtilityMethods.getBoardingTime(ticketItem.departureScheduled)}</div>
                            </td>
                            <td>
                                <div className={styles.infoCaption}>CLASS</div>
                                <div className={styles.infoCaption}>КЛАСС</div>
                                <h5>Y</h5>
                            </td>
                            <td>
                                <div className={styles.infoCaption}>SEAT NO</div>
                                <div className={styles.infoCaption}>МЕСТО</div>
                                <div>{ticketItem.seatNumber}</div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        );
    }
}