﻿import Personreward from './personreward';
import Person from './person';
import Personphoto from './personphoto';
import Structure from './structure';
import Position from './position';
import Positiontype from './positiontype';
import Persondecreeblockintro from './persondecreeblockintro';
import Persondecreeblocksub from './persondecreeblocksub';
import Countrycities from './countrycities';

export default class Persondecreeblock {
    id: number;
    persondecree: number;
    persondecreeblocktype: number; // 1 - награда

    status: number; // 1 - Добавить.
    intro: string = ""; // Фабула
    persondecreeblocksub: number = null; // Когда выбираем саб на добавление 
    
    optionnumber1: number;
    optionnumber2: number;
    optionnumber3: number;
    optionnumber4: number;
    optionnumber5: number;
    optionnumber6: number;
    optionnumber7: number;
    optionnumber8: number;
    optionnumber9: number;
    optionnumber10: number;
    optionnumber11: number;
    optionstring1: string;
    optionstring2: string;
    optionstring3: string;
    optionstring4: string;
    optionstring5: string;
    optionstring6: string;
    optionstring7: string;
    optionstring8: string;
    optiondate1: Date;
    optiondate2: Date;
    optiondate3: Date;
    optiondate4: Date;
    optiondate5: Date;
    optiondate6: Date;
    optiondate7: Date;
    optiondate8: Date;
    optiondate1String: string;
    optiondate2String: string;
    optiondate3String: string;
    optiondate4String: string;
    optiondate5String: string;
    optiondate6String: string;
    optiondate7String: string;
    optiondate8String: string;
    optionnumber1Bool: boolean; // Там где вместо числа должно быть булевское значение. 0 - ложь. 1 - правда.
    optionnumber2Bool: boolean;
    priority: number;
    subvaluenumber1: number;
    subvaluenumber2: number;
    subvaluestring1: string;
    subvaluestring2: string;
    nonperson: string;
    optionarray1: string; // массив number, разделенный ','
    optionarrayperson: string; // массив айдишников person, разделенный ','
    optionarray1Array: number[]; // массив айдишников number
    optionarraypersonArray: number[]; // массив айдишников person
    optionarraypersonObjects: Person[]; // массив person, заключенных в optionarrayperson 

    index: number; // Номер блока.

    persondecreeblocksubs: Persondecreeblocksub[];
    persondecreeblockintros: Persondecreeblockintro[];

    samplePersonreward: Personreward; // шаблон для добавления 
    sampleStructure: Structure;
    samplePosition: Position;
    samplePositiontype: Positiontype;
    fiosearch: string = ""; // Для поиска
    person: Person; // Для поиска
    personssearch: Person[] = []; // Для поиска
    photosPreview: Personphoto[] = []; // Для поиска
    searchiteration: number = 0; // Для поиска
    personssearchadditional: boolean = true; // Для командирования. 
    countrycitiesList: Countrycities[] = new Array();
}