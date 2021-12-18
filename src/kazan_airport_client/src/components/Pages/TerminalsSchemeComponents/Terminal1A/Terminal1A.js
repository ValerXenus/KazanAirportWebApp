import React from 'react';
import scheme1a1 from './../../../../images/terminals/terminal_1a_1.jpg';
import scheme1a2 from './../../../../images/terminals/terminal_1a_2.jpg';

const Terminal1A = () => {
    return (
        <div>
            <h3>Схемы терминала 1A</h3>
            <h4>1-й этаж</h4>            
            <img src={scheme1a1} width={950} alt="1-й этаж терминала 1A"/>
            <h4>2-й этаж</h4>
            <img src={scheme1a2} width={950} alt="2-й этаж терминала 1A"/>            
        </div>
    );
}

export default Terminal1A;