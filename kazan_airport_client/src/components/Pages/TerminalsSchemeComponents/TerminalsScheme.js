import React from 'react';
import SubchapterPage from '../../HelperComponents/SubchapterPage/SubchapterPage';
import Terminal1 from './Terminal1/Terminal1';
import Terminal1A from './Terminal1A/Terminal1A';
import Terminal2 from './Terminal2/Terminal2';
import TerminalsAll from './TerminalsAll/TerminalsAll';

const TerminalsScheme = () => {
    let collection = { data: [
        { link: "/terminals/terminal_1", name: "Терминал 1", component: <Terminal1 /> },
        { link: "/terminals/terminal_1a", name: "Терминал 1A", component: <Terminal1A />},
        { link: "/terminals/terminal_2", name: "Терминал 2", component: <Terminal2 />}],
        main: { link: "/terminals", component: <TerminalsAll /> }
    };

    return (
        <div>
            <SubchapterPage collection={collection}/>
        </div>
    );
}

export default TerminalsScheme;