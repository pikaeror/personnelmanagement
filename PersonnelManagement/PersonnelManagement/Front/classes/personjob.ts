import Personjobprivelege from './personjobprivelege';

export default class Personjob {
    id: number;
    person: number;
    jobtype: number;
    start: Date;
    end: Date;
    jobplace: string;
    jobposition: string;
    jobpositionplace: string;
    servicetype: number;
    servicetypestr: string;
    servicefeature: number;
    serviceorder: string;
    servicecoef: number;
    serviceplace: string;
    ordernumber: string;
    ordernumbertype: string;
    orderdate: Date;
    orderwho: string;
    orderwhoid: number;
    orderid: number;
    actual: number;
    manual: number;
    mchs: number;
    vacationdays: number;
    position: number;
    positiontoselect: number;
    positionnametree: string;
    fireordernumber: string;
    fireordernumbertype: string;
    fireorderdate: Date;
    fireorderwho: string;
    fireorderwhoid: number;
    fireorderid: number;
    statecivil: number;
    statecivilstart: Date;
    statecivilend: Date;
    startcustom: Date;
    privelege: number;

    startString: string;
    endString: string;
    orderdateString: string;
    actualBool: boolean;
    manualBool: boolean;
    mchsBool: boolean;
    statecivilBool: boolean;
    statecivilstartString: string;
    statecivilendString: string;
    startcustomString: string;
    privelegeBool: boolean;

    personjobpriveleges: Personjobprivelege[];
}