import React from 'react';
import styles from './HowToGet.module.css';

const HowToGet = () => {
    return (
        <div className={styles.howToGetContainer}>
            <h3>Как добраться</h3>
            <div>
                Аэропорт «Казань» находится в 26 км от центра Казани.<br/>
                Добраться до аэропорта Вы можете на общественном транспорте, личном автомобиле, такси или электричке.<br/>
                Если Вы приехали на личном автомобиле, то Вы можете оставить свое авто на парковке.
            </div>
            <iframe style={{paddingTop: 20}} 
                src="https://yandex.ru/map-widget/v1/?um=constructor%3A2644d979829631cf03b268f595f791d63c67d236983a32ed18fed2366ea6727e&amp;source=constructor" 
                title="Адрес аэропорта"
                width="1200" 
                height="600" 
                frameBorder="0">             
            </iframe>
        </div>
    );
}

export default HowToGet;