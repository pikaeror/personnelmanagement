import Rights from './rights';

export default class User {
    id: number;
    name: string;
    positionString: string; // звание
    structureString: string;
    structureTreeString: string;
    password: string;
    salt: string;
    admin: number;
    structure: number;
    structureeditor: number;
    masterpersonneleditor: number;
    personneleditor: number;
    decree: number;
    positioncompact: number;
    date: Date;
    sidebardisplay: number;
    currentstructuretree: string;
    structureread: number;
    personnelread: number;
    mode: number;
    firstname: string;
    surname: string;
    patronymic: string;
    positiontype: number;
    fullmode: number;

    structurename: string;
    rights: Rights = new Rights();

    //public constructor(test?: number) {
    //    this.admin = test;
    //}
}