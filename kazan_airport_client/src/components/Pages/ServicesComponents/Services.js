import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';
import Cafes from './Cafes/Cafes';
import Parking from './Parking/Parking';
import ServicesMain from './ServicesMain/ServicesMain';
import Stores from './Stores/Stores';
import Vip from './Vip/Vip';

const Services = () => {
    let collection = { data: [
        { link: "/services/stores", name: "Магазины", component: <Stores />, key: "stores" }, 
        { link: "/services/cafes", name: "Кафе и рестораны", component: <Cafes />, key: "cafes"},
        { link: "/services/vip", name: "VIP и бизнес-залы", component: <Vip />, key: "vip"},
        { link: "/services/parking", name: "Парковка", component: <Parking />, key: "parking"}],
        main: { link: "/services", component: <ServicesMain /> }
    };

    return (
        <div>
            <SubchapterPage collection={collection}/>
        </div>
    );
}

export default Services;