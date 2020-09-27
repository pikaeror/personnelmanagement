export default class Pmrequest {
    id: number;
    type: number;
    positiontypes: string;
    structures: string;
    structuretypes: string;
    positioncategories: string;
    sofs: string;
    mrds: string;
    ranks: string;
    ranksexpanded: number;
    replacedbycivil: number;
    replacedbycivildate: Date;
    replacedbycivildateavailable: number;
    replacedbycivildateexpired: number;
    replacedbycivilnot: number;
    replacedbycivilpositiontypes: string;
    replacedbycivilpositioncategories: string;
    signed: number;
    notsigned: number;
    willbenotsigned: number;
    willbesigned: number;
    decertificate: number;
    decertificateexpired: number;    
    date: Date;
    civilclasslow: number;
    civilclasshigh: number;

    structurerank: string; 
    structureregion: string;
    structurecity: string;
    structurestreet: string;

    displaytreeseparately: number;
    displaytree: number;
    displaystructureparent: number;
    displaypositionchildren: number;
    displaymrds: number;
    displayreplacedbycivilinfo: number;
    civil: number;
    notopchs: number;
    allopchs: number;
    structurecountmode: number;
    structurecountallinclusive: number;
    structuresub: number; // Включать подчиненные подразделения тех, кто прошел фильтрацию
    structuresublevel: number; // Уровень вложенности для пункта выше
    structureselfcount: number; // Учитывать исключительно собственную численность

}