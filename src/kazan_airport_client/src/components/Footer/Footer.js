import React from 'react';
import { NavLink } from 'react-router-dom';
import styles from './Footer.module.css';

const Footer = () => {
    return(
        <div className={styles.footerStyle}>
            <div className={styles.footerContainer}>
                <div className={styles.services}>
                    <h3>Сервисы</h3>
                    <div><NavLink className={styles.childItem} to="/schedule/departures">Онлайн-табло</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/online_registration">Онлайн-регистрация</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/buy_ticket">Купить билет</NavLink></div>
                </div>
                <div className={styles.otherServices}>
                    <h3>Услуги</h3>
                    <div><NavLink className={styles.childItem} to="/services/stores">Магазины</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/services/cafes">Кафе и рестораны</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/services/vip">VIP и бизнес-залы</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/services/parking">Парковка</NavLink></div>
                </div>
                <div className={styles.terminalSchemes}>
                    <h3>Схема терминалов</h3>
                    <div><NavLink className={styles.childItem} to="/terminals">Общая схема терминалов</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/terminals/terminal_1a">Схема терминала 1А</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/terminals/terminal_1">Схема терминала 1</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/terminals/terminal_2">Схема терминала 2</NavLink></div>
                </div>
                <div className={styles.aboutInfo}>
                    <h3>О нас</h3>
                    <div><NavLink className={styles.childItem} to="/about/news">Новости</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/howtoget">Как добраться</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/about/history">История</NavLink></div>
                    <div><NavLink className={styles.childItem} to="/about/contacts">Контактная информация</NavLink></div>
                </div>
            </div>
        </div>
    );
}

export default Footer;