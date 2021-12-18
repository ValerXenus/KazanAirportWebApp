import React from 'react';
import { NavLink } from 'react-router-dom';
import logo from './../../logo_kzn.svg';
import styles from './NavigationMenu.module.css';
import Cookies from 'js-cookie';

const NavigationMenu = () => {

    const accountButton = () => {
        var authCookie = Cookies.get("authData");
        if (!authCookie)
            return(
                <NavLink to="/login" className={`${styles.menuItem} ${styles.loginButton}`}>Войти</NavLink>
            );

        let currentSession = JSON.parse(authCookie);
        if (!currentSession || 
            (currentSession !== undefined && !currentSession.isAuth)) {
            return(
                <NavLink to="/login" className={`${styles.menuItem} ${styles.loginButton}`}>Войти</NavLink>
            );
        }

        let accountUrl = "";
        let buttonName = "Личный кабинет";

        switch(currentSession.role){
            case 0:
                accountUrl = "/passenger";
                break;
            case 1:
                accountUrl = "/operator";
                break;
            case 2:
                accountUrl = "/admin";
                break;     
            default:
                accountUrl = "/login";
                buttonName = "Войти";
                break;
        }

        return(
            <NavLink to={accountUrl} className={`${styles.menuItem} ${styles.loginButton}`}>{buttonName}</NavLink>
        );
    }

    return (
        <div className={styles.menuStyle}>            
            <nav className={styles.menuContainer}>
                <NavLink to="/">
                    <img src={logo} className={styles.logoStyle} alt="Аэропорт Казань" />
                </NavLink>
                <div className={styles.menuItems}>
                    <NavLink to="/schedule/departures" className={styles.menuItem}>Табло рейсов</NavLink>
                    <NavLink to="/services" className={styles.menuItem}>Услуги</NavLink>
                    <NavLink to="/online_registration" className={styles.menuItem}>Онлайн-регистрация</NavLink>
                    <NavLink to="/buy_ticket" className={styles.menuItem}>Купить билет</NavLink>
                    <NavLink to="/howtoget" className={styles.menuItem}>Как добраться</NavLink>
                    <NavLink to="/terminals" className={styles.menuItem}>Схема терминалов</NavLink>
                    <NavLink to="/about" className={styles.menuItem}>О нас</NavLink>
                    {accountButton()}
                </div>
            </nav>
        </div>
    );
}

export default NavigationMenu;