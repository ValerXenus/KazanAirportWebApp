import React from 'react';
import scheme21 from './../../../../images/terminals/terminal_2_1.jpg';
import scheme22 from './../../../../images/terminals/terminal_2_2.jpg';

const Terminal2 = () => {
    return (
        <div>
            <h3>Схемы терминала 2</h3>
            <h4>1-й этаж</h4>            
            <img src={scheme21} width={950} alt="1-й этаж терминала 2"/>
            <h4>2-й этаж</h4>
            <img src={scheme22} width={950} alt="2-й этаж терминала 2"/>            
        </div>
    );
}

export default Terminal2;