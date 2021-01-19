import React from 'react';
import { NavLink } from 'react-router-dom';
import { Route } from 'react-router-dom';
import Cafes from './Cafes/Cafes';
import Parking from './Parking/Parking';
import styles from './Services.module.css';
import ServicesMain from './ServicesMain/ServicesMain';
import Stores from './Stores/Stores';
import Vip from './Vip/Vip';

const Services = () => {
    return (
        <div className={styles.servicesContainer}>
            {/* Submenu */}
            <div className={styles.servicesNavigation}>
                <div><NavLink to="/services/stores">Магазины</NavLink></div>
                <div><NavLink to="/services/cafes">Кафе и рестораны</NavLink></div>
                <div><NavLink to="/services/vip">VIP и бизнес-залы</NavLink></div>
                <div><NavLink to="/services/parking">Парковка</NavLink></div>
            </div>
            {/* Subchapter */}
            <div className={styles.servicesContent}>
                <Route path="/services" exact={true} render={() => <ServicesMain /> } />
                <Route path="/services/stores" exact={true} render={() => <Stores /> } />
                <Route path="/services/cafes" exact={true} render={() => <Cafes /> } />
                <Route path="/services/vip" exact={true} render={() => <Vip /> } />
                <Route path="/services/parking" exact={true} render={() => <Parking /> } />
            </div>
        </div>
    );
}

export default Services;