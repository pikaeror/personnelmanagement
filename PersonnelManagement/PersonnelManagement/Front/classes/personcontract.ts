import Personadditionalagreement from './personadditionalagreement';

export default class Personcontract {
    id: number;
    person: number;
    pay: number;
    datestart: Date;
    dateend: Date;
    ordernumber: string;
    ordernumbertype: string;
    orderdate: Date;
    orderwho: string;
    orderwhoid: number;
    orderid: number;
    sourceoffinancing: number;
    payvalue: number;
    stateserviceyears: number;
    stateservicemonths: number;
    stateservicedays: number;
    vacationdays: number;
    personadditionalagreements: Personadditionalagreement[];

    datestartString: string;
    dateendString: string;
    payBool: boolean;
    orderdateString: string;
}