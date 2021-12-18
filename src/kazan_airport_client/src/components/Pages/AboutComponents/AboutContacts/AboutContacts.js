import React from 'react';
import aboutImageContacts from './../../../../images/about/about_contacts.jpg';

const AboutContacts = () => {
    return (
        <div>
            <h3>Контакты</h3>
            <img src={aboutImageContacts} width={950} alt="О нас"/>
            420017,Россия, Республика Татарстан, г.Казань, Аэропорт.<br/>
            АО «Международный аэропорт «Казань»<br/>
            E-mail:office@airportkzn.ru<br/>
            Официальный интернет-сайт: www.kazan.aero<br/>
        </div>
    );
}

export default AboutContacts;