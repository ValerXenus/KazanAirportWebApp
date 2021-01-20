import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';


const AccountGeneral = () => {
    let collection = { data: [
        { link: "/terminals/terminal_1", name: "Терминал 1", component: <Terminal1 />, key: "terminal1" }],
        main: { link: "/terminals", component: <TerminalsAll /> }
    };

    return (
        <div>
            <SubchapterPage collection={collection}/>
        </div>
    );
}

export default AccountGeneral;