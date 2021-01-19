import React from 'react';
import imageParking from './../../../../images/services/services_parking.jpg';

const Parking = () => {
    return (
        <div>
            <h3>Парковка</h3>
            <img src={imageParking} width={950} alt="Парковка"/>
            Оплата услуг платной автопарковки производится в автоматических терминалах оплаты - паркоматах, 
            которые располагаются в Терминалах и зонах автопарковки.<br />
        </div>
    );
}

export default Parking;