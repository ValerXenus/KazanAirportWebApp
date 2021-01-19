import React from 'react';
import storesImage from './../../../../images/services/services_stores.jpg';

const Stores = () => {
    return (
        <div>
            <h3>Магазины</h3>
            <img src={storesImage} width={950} alt="Магазины"/>
            В нашем аэропорту Вас ждут киоски, лавки, небольшие магазины и магазины беспошлинной торговли "дьюти-фри".<br/>
            В них вы сможете купить практически все, что может потребоваться в путешествии: одежду, обувь, лекарства, 
            продукты, предметы роскоши и подарки.<br/>
            В открытой и стерильной зонах терминалов аэропорта есть и продуктовые магазины, и элитные бутики, и ювелирные 
            и сувенирные ларьки, и небольшие универсальные магазины.<br/>
        </div>
    );
}

export default Stores;