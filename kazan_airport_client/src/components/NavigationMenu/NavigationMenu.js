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
                <NavLink to="/login" className={styles.menuItem}>Войти</NavLink>
            );

        let currentSession = JSON.parse(authCookie);
        if (!currentSession || 
            (currentSession !== undefined && !currentSession.isAuth)) {
            return(
                <NavLink to="/login" className={styles.menuItem}>Войти</NavLink>
            );
        }

        let accountUrl = "";
        let buttonName = "";

        switch(currentSession.role){
            case 0:
                accountUrl = "/account/admin";
                buttonName = "Личный кабинет";
                break;
            case 1:
                accountUrl = "/account/operator";
                buttonName = "Личный кабинет";
                break;
            case 2:
                accountUrl = "/account/passenger";
                buttonName = "Личный кабинет";
                break;
            default:
                accountUrl = "/login";
                buttonName = "Войти";
                break;
        }

        return(
            <NavLink to={accountUrl} className={styles.menuItem}>{buttonName}</NavLink>
        );
    }

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
                    {accountButton()}
                </div>
            </nav>
        </div>
    );
}

export default NavigationMenu;