import React from 'react';
import scheme11 from './../../../../images/terminals/terminal_1_1.jpg';
import scheme12 from './../../../../images/terminals/terminal_1_2.jpg';

const Terminal1 = () => {
    return (
        <div>
            <h3>Схемы терминала 1</h3>
            <h4>1-й этаж</h4>            
            <img src={scheme11} width={950} alt="1-й этаж терминала 1"/>
            <h4>2-й этаж</h4>
            <img src={scheme12} width={950} alt="2-й этаж терминала 1"/>            
        </div>
    );
}

export default Terminal1;