import React from 'react';
import { Departures } from './Departures';
import { Arrivals } from './Arrivals';
import styles from './Schedule.module.css';
import SubchapterPage from './../../HelperComponents/SubchapterPage/SubchapterPage';

const Schedule = () => {
    let collection = { data: [
        { link: "/schedule/departures", name: "Вылет", component: <Departures />, key: "departures" }, 
        { link: "/schedule/arrivals", name: "Прилет", component: <Arrivals />, key: "arrivals" }]
    };
    
    return (
        <div className={styles.scheduleStyle}>
            <SubchapterPage collection={collection}/>            
        </div>
    );
    
}

export default Schedule;