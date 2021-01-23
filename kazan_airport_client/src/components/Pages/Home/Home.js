import React from 'react';
import styles from './Home.module.css';


const Home = () => {
    return(
        <div className={styles.homeContainer}>
            <div className={styles.homeStyle}/>
            <div className={styles.makeDark} />
            <div className={styles.mainContent}>
                <div className={styles.mainLabel}>
                    <div className={styles.airportWord}>аэропорт</div>
                    <div className={styles.kazanWord}>КАЗАНЬ</div>
                </div>
            </div>
        </div>
    );
}

export default Home;