import React from 'react';
import cafesImage from './../../../../images/services/services_cafes.jpg';

const Cafes = () => {
    return (
        <div>
            <h3>Кафе и рестораны</h3>
            <img src={cafesImage} width={950} alt="Кафе и рестораны"/>
            <h4>Американский Бар и Гриль</h4>
            «Американский Бар и Гриль» — это классическая обстановка американского бара, свободный дух молодой Америки, 
            уютно обставленные залы, выдержанный ковбойский стиль и домашняя американская кухня. Огромные сэндвичи, 
            великанские стейки, щедрые порции картофеля, горы салата.<br/>
            Терминал 1А, вылет международных рейсов Терминал 1А, вылет внутренних рейсов.<br/><br/>

            <h4>Патио Пицца</h4>
            Все началось в далеких 90-х, когда появилась скромная, но прогрессивная пиццерия «Патио Пицца», где впервые 
            в Москве пиццу стали готовить в настоящей дровяной печи на открытом огне. Постепенно мы стали больше, чем 
            просто пиццерией, и в 2004 году «Патио Пицца» превратилась в «IL Патио», сеть демократичных ресторанов 
            итальянской кухни с широким меню. Сегодня в меню «IL Патио» большое количество блюд из мяса, морепродуктов 
            и овощей. Внимательный сервис и уютная домашняя обстановка в «IL Патио» сочетаются с демократичными ценами. 
            Именно поэтому в «IL Патио» так приятно проводить время всей семьёй, с друзьями, коллегами и детьми.<br/>
            Терминал 1А, 2 этаж, левое крыло <br/><br/>

            <h4>Сoffee CAVA</h4>
            Кофейня Сoffee CAVA с радостью встречает ароматным кофе жителей и гостей Казани. Вернуться домой или начать 
            изучение города одинаково приятно с чашки одного из лучших кофе в городе.<br/>
            Терминал 1, 1 этаж
        </div>
    );
}

export default Cafes;