import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';
import Cafes from './Cafes/Cafes';
import Parking from './Parking/Parking';
import styles from './Services.module.css';
import ServicesMain from './ServicesMain/ServicesMain';
import Stores from './Stores/Stores';
import Vip from './Vip/Vip';

const Services = () => {
    let collection = { data: [
        { link: "/services/stores", name: "Магазины", component: <Stores /> }, 
        { link: "/services/cafes", name: "Кафе и рестораны", component: <Cafes />},
        { link: "/services/vip", name: "VIP и бизнес-залы", component: <Vip />},
        { link: "/services/parking", name: "Парковка", component: <Parking />}],
        main: { link: "/services", component: <ServicesMain /> }
    };

    return (
        <div>
            <SubchapterPage collection={collection}/>
        </div>
    );
}

export default Services;