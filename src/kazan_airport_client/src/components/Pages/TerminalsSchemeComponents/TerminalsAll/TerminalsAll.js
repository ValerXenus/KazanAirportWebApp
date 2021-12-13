import React from 'react';
import schemeAll from './../../../../images/terminals/terminals_all.jpg';

const TerminalsAll = () => {
    return (
        <div>
            <h3>Общая схема терминалов</h3>      
            <img src={schemeAll} width={950} alt="Общая схема терминалов"/>         
        </div>
    );
}

export default TerminalsAll;