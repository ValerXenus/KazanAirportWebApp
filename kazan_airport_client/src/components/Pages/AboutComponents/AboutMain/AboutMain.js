import React from 'react';
import aboutImageMain from './../../../../images/about/about_main.jpg';

const AboutMain = () => {
    return (
        <div>
            <h3>О нас</h3>
            <img src={aboutImageMain} width={950} alt="О нас"/>
            Один из крупнейших аэропортов России, пятикратный обладатель звания "Лучший региональный 
            аэропорт России и СНГ", обладатель награды "Лучший аэропортовый персонал России и СНГ" 
            по версии Skytrax, а также обладателем 4 звезд в рейтинге Skytrax, трёхкратный обладатель 
            национальной премии «Воздушные ворота России».<br />
            В 2019 году в АО «Международный аэропорт «Казань» было обслужено 3 470 742 пассажира, 
            что на 10,5% больше, чем в 2018 году. <br />
        </div>
    );
}

export default AboutMain;