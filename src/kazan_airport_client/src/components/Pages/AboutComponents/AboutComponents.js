import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';
import AboutHistory from './AboutHistory/AboutHistory';
import AboutMain from './AboutMain/AboutMain';
import { AboutAirlinesTop } from './AboutAirlinesTop/AboutAirlinesTop';
import AboutContacts from './AboutContacts/AboutContacts';

const AboutComponents = () => {
    let collection = { data: [
        { link: "/about/airlinestop", name: "Рейтинг авиакомпаний", component: <AboutAirlinesTop /> },
        { link: "/about/history", name: "История", component: <AboutHistory /> },
        { link: "/about/contacts", name: "Контакты", component: <AboutContacts /> }],
        main: { link: "/about", component: <AboutMain /> }
    };

    return (
        <div>
            <SubchapterPage collection={collection}/>
        </div>
    );
}

export default AboutComponents;