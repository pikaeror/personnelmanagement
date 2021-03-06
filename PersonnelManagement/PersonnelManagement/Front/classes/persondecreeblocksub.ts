﻿import Personreward from './personreward';
import Person from './person';
import Personphoto from './personphoto';

export default class Persondecreeblocksub {
    id: number;
    persondecree: number;
    persondecreeblock: number;
    persondecreeblockintro: number;
    persondecreeblocksubtype: number;
    intro: number;
    introtext: string;
    index: number;
    priority: number;
    subvaluenumber1: number;
    subvaluenumber2: number;
    subvaluenumber3: number;
    subvaluenumber4: number;
    subvaluestring1: string;
    subvaluestring2: string;
    subvaluestring3: string;
    subvaluestring4: string;
    parentpersondecreeblocksub: number;
    
    subvaluedate1: Date;
    subvaluedate2: Date;
    
    status: number; // 1 - Добавить.


    samplePersonreward: Personreward; // шаблон для добавления 
    fiosearch: string = ""; // Для поиска
    person: Person; // Для поиска
    personssearch: Person[] = []; // Для поиска
    photosPreview: Personphoto[] = []; // Для поиска
    searchiteration: number = 0; // Для поиска
}