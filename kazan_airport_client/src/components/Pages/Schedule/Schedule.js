import React from 'react';
import styles from './Schedule.module.css';
import scheduleImg from './../../../images/about/test_dashboard.jpg';

const Schedule = () => {
    return(
        <div className={styles.scheduleStyle}>
            <img src={scheduleImg}/>
        </div>
    );
}

export default Schedule;