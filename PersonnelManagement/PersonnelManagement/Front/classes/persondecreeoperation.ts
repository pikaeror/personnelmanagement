import Personreward from './personreward';
import Personpenalty from './personpenalty';
import Person from './person';
import Personattestation from './personattestation';
import Position from './position';
import Positiontype from './positiontype';
import Structure from './structure';
import Fire from './fire';
import Countrycities from './countrycities';
import Personjob from './personjob';
import Cabinetdata from './cabinetdata';

export default class Persondecreeoperation {
    id: number;
    persondecree: number;
    person: number;
    subjectid: number;
    subjecttype: number; // Тип операции. 1 - Награды.
    creator: number;
    persondecreeblock: number;
    persondecreeblocktype: number;
    persondecreeblocksub: number;
    persondecreeblocksubtype: number;
    intro: string;
    
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
    optionstring9: string;
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
    optionnumber3Bool: boolean;
    optionnumber4Bool: boolean;
    optionnumber5Bool: boolean;
    optionnumber6Bool: boolean;
    optionnumber7Bool: boolean;
    optionnumber8Bool: boolean;
    optionnumber9Bool: boolean;
    optionnumber10Bool: boolean;
    optionnumber11Bool: boolean;
    optionnumber12Bool: boolean;
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
    personFromStructure: Person[];
    candidateSearch: Cabinetdata[];
    checkboxdismiss: boolean;
    checkboxdirect: boolean;
    


    intronum: number = 0; // Записывается последовательно номер фабулы (1, 2 ...) для каждой первой операции в списке с одинаковой фабулой. 
    persondecreeblocksubtypenum: number = 0; // Аналогично верхнему.
    persondecreeoptionnumber1num: number = 0; // Аналогично верхнему

    status: number; // 1 - Добавить, 2 - удалить, 3 - обновить
    personjob: Personjob;
    personobject: Person;
    personreward: Personreward;
    personpenalty: Personpenalty;
    positionobject: Position;
    positiontypeobject: Positiontype;
    structureobject: Structure;
    fireobject: Fire;

    // Только на фронтэнде
    countrycitiesList: Countrycities[] = new Array(new Countrycities());
}