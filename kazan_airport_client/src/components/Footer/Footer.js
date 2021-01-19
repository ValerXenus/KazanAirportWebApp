import React from 'react';
import { NavLink } from 'react-router-dom';
import styles from './Footer.module.css';

const Footer = () => {
    return(
        <div className={styles.footerStyle}>
            <div className={styles.footerContainer}>
                <div className={styles.services}>
                    <h3>Сервисы</h3>
                    <div><NavLink to="/schedule">Онлайн-табло</NavLink></div>
                    <div><NavLink to="/">Онлайн-регистрация</NavLink></div>
                    <div><NavLink to="/">Купить билет</NavLink></div>
                </div>
                <div className={styles.otherServices}>
                    <h3>Услуги</h3>
                    <div><NavLink to="/services/stores">Магазины</NavLink></div>
                    <div><NavLink to="/services/cafes">Кафе и рестораны</NavLink></div>
                    <div><NavLink to="/services/vip">VIP и бизнес-залы</NavLink></div>
                    <div><NavLink to="/services/parking">Парковка</NavLink></div>
                </div>
                <div className={styles.terminalSchemes}>
                    <h3>Схема терминалов</h3>
                    <div><NavLink to="/terminals">Общая схема терминалов</NavLink></div>
                    <div><NavLink to="/terminals/terminal_1a">Схема терминала 1А</NavLink></div>
                    <div><NavLink to="/terminals/terminal_1">Схема терминала 1</NavLink></div>
                    <div><NavLink to="/terminals/terminal_2">Схема терминала 2</NavLink></div>
                </div>
                <div className={styles.aboutInfo}>
                    <h3>О нас</h3>
                    <div><NavLink to="/">Новости</NavLink></div>
                    <div><NavLink to="/howtoget">Как добраться</NavLink></div>
                    <div><NavLink to="/">История</NavLink></div>
                    <div><NavLink to="/">Контактная информация</NavLink></div>
                </div>
            </div>
        </div>
    );
}

export default Footer;