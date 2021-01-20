import React from 'react';
import { NavLink } from 'react-router-dom';
import { Route } from 'react-router-dom';
import styles from './SubchapterPage.module.css';

const SubchapterPage = (props) => {

    let collection = props.collection;

    let navLinks = collection.data.map(x => {
        return (
            <div key={x.key}><NavLink to={x.link}>{x.name}</NavLink></div>
        );
    });

    let routes = collection.data.map(x => {
        return(
            <Route key={x.key} path={x.link} exact={true} render={() => {
                return (x.component);
            } } />
        );
    });

    return (
        <div className={styles.subchapterContainer}>
            {/* Submenu */}
            <div className={styles.subchapterNavigation}>
                {navLinks}
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