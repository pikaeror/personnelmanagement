export default class Pension {
    positionplace: string;
    orderstring: string;
    orderwho: string;
    orderdate: Date;
    ordernumber: string;
    ordernumbertype: string;
    start: Date;
    end: Date;
    coef: number;
    daysbeforecoef: number;
    monthsbeforecoef: number;
    yearsbeforecoef: number;
    daysaftercoef: number;
    monthsaftercoef: number;
    yearsaftercoef: number;
    education: boolean;
    educationConsider: boolean;
    educationFulltime: boolean;
    educationMilitary: boolean;
    job: boolean;
    jobMilitary: boolean;
}