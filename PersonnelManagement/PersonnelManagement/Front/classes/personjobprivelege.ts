import Personjobprivelegeperiod from './personjobprivelegeperiod';

export default class Personjobprivelege {
    id: number;
    person: number;
    personjob: number;
    start: Date;
    end: Date;
    coef: number;
    prooftype: number;
    proofdate: Date;
    proofnumber: string;
    prooftext: string;
    documentorder: string;
    documentdate: Date;
    documentnumber: string;
    ordernumbertype: string;
    orderwho: string;
    orderwhoid: number;
    orderid: number;
    ordertype: number;

    startString: string;
    endString: string;
    proofdateString: string;
    documentdateString: string;

    personjobprivelegeperiods: Personjobprivelegeperiod[];
    daysbeforecoef: number;
    monthsbeforecoef: number;
    yearsbeforecoef: number;
    daysaftercoef: number;
    monthsaftercoef: number;
    yearsaftercoef: number;

}