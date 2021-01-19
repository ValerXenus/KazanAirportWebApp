import React from 'react';
import { NavLink } from 'react-router-dom';
import logo from './../../logo_kzn.svg';
import styles from './NavigationMenu.module.css';

const NavigationMenu = () => {
    return (
        <div className={styles.menuStyle}>            
            <nav className={styles.menuContainer}>
                <NavLink to="/">
                    <img src={logo} style={{height: 110}} alt="logo" />
                </NavLink>
                <div className={styles.menuItems}>
                    <NavLink to="/schedule" className={styles.menuItem}>Табло рейсов</NavLink>
                    <NavLink to="/services" className={styles.menuItem}>Услуги</NavLink>
                    <NavLink to="/" className={styles.menuItem}>Онлайн-регистрация</NavLink>
                    <NavLink to="/" className={styles.menuItem}>Купить билет</NavLink>
                    <NavLink to="/howtoget" className={styles.menuItem}>Как добраться</NavLink>
                    <NavLink to="/terminals" className={styles.menuItem}>Схема терминалов</NavLink>
                    <NavLink to="/about" className={styles.menuItem}>О нас</NavLink>
                    <NavLink to="/" className={styles.menuItem}>Войти</NavLink>
                </div>
            </nav>
        </div>
    );
}

export default NavigationMenu;