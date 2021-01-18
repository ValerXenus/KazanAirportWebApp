import React from 'react';
import { NavLink } from 'react-router-dom';
import logo from './../../logo.svg';
import styles from './NavigationMenu.module.css';

const NavigationMenu = () => {
    return (
        <div className={styles.menuStyle}>
            <img src={logo} style={{height: 40}} alt="logo" />
            <nav>
                <NavLink to="/schedule" >Табло рейсов</NavLink>
                <NavLink to="/" >Услуги</NavLink>
                <NavLink to="/" >Онлайн-регистрация</NavLink>
                <NavLink to="/" >Купить билет</NavLink>
                <NavLink to="/" >Как добраться</NavLink>
                <NavLink to="/" >Схема терминалов</NavLink>
                <NavLink to="/" >О нас</NavLink>
            </nav>
        </div>
    );
}

export default NavigationMenu;