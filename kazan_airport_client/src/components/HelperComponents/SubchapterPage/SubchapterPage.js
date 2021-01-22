import React from 'react';
import { NavLink } from 'react-router-dom';
import { Route } from 'react-router-dom';
import styles from './SubchapterPage.module.css';
import Cookies from 'js-cookie';

const SubchapterPage = (props) => {

    let collection = props.collection;

    let navLinks = collection.data.map(x => {
        return (
            <div key={x.key}>
                <NavLink className={styles.navlinkItem} key={x.key} to={x.link}>{x.name}</NavLink>
            </div>
        );
    });

    let routes = collection.data.map(x => {
        return(
            <Route key={x.key} path={x.link} exact={true} render={() => {
                return (x.component);
            } } />
        );
    });

    let handleLogOutClick = () => {
        Cookies.remove("authData");
        window.location = "/";
    }

    let logOutElement = () => {
        if (collection.needShowLogout === true) {
            return (
                <div>
                    <div className={styles.navlinkItem} onClick={handleLogOutClick}>Выход</div>
                </div>
            );
        }

        return null;
    }

    return (
        <div className={styles.subchapterContainer}>
            {/* Submenu */}
            <div className={styles.subchapterNavigation}>
                {navLinks}
                {logOutElement()}
            </div>
            {/* Subchapter */}
            <div className={styles.subchapterContent}>
                <Route path={collection.main.link} exact={true} render={() => {
                    return (collection.main.component);
                } } />
                {routes}
            </div>
        </div>
    );
}

export default SubchapterPage;