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
                accountUrl = "/admin";
                break;
            case 1:
                accountUrl = "/operator";
                break;
            case 2:
                accountUrl = "/passenger";
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
                    <img src={logo} className={styles.logoStyle} alt="logo" />
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