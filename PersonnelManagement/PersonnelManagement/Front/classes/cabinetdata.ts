import Autobiographydata from '../classes/autobiographydata';
import Declarationdata from './declarationdata';
import Declarationrelative from './declarationrelative';
import Declarationtabledata from './declarationtabledata';
import Profiledata from './profiledata';
import Profilerelatives from './profilerelatives';
import Pseducation from './pseducation';
import Pswork from './pswork';
import Sheetdata from './sheetdata';
import Sheetpolitics from './sheetpolitics';
import UserCompact from './usercompact';

export default class Cabinetdata {
    id: number;
    employeesid: number;
    creatorid: number;
    reasonid: number;
    usersurname: string;
    username: string;
    userpatronymic: string;
    userind: string;
    accesscode: string;
    status: number;
    denyreason: string;
    creationdate: Date;

    userCompact: UserCompact;

    action: number; // 0 - создать
    declarationid: number; // используется для создания личного кабинета, но не используется в одноименной таблице.
    autobiographydatalist: Autobiographydata[];
    declarationdatalist: Declarationdata[];
    declarationrelativelist: Declarationrelative[];
    declarationtabledatalist: Declarationtabledata[];
    profiledatalist: Profiledata[];
    profilerelativeslist: Profilerelatives[];
    pseducationlist: Pseducation[];
    psworklist: Pswork[];
    sheetdatalist: Sheetdata[];
    sheetpoliticslist: Sheetpolitics[];

    
}