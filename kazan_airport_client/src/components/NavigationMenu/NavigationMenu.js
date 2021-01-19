import React from 'react';
import { NavLink } from 'react-router-dom';
import logo from './../../logo_kzn.svg';
import styles from './NavigationMenu.module.css';

const NavigationMenu = () => {
    return (
        <div className={styles.menuStyle}>            
            <nav className={styles.menuContainer}>
                <NavLink to="/">
                    <img src={logo} style={{height: 60}} alt="logo" />
                </NavLink>
                <div style={{float: 'right', marginTop: 20}}>
                    <NavLink to="/schedule" >Табло рейсов</NavLink>
                    <NavLink to="/" >Услуги</NavLink>
                    <NavLink to="/" >Онлайн-регистрация</NavLink>
                    <NavLink to="/" >Купить билет</NavLink>
                    <NavLink to="/" >Как добраться</NavLink>
                    <NavLink to="/" >Схема терминалов</NavLink>
                    <NavLink to="/" >О нас</NavLink>
                    <NavLink to="/">Войти</NavLink>
                </div>
            </nav>
        </div>
    );
}

export default NavigationMenu;