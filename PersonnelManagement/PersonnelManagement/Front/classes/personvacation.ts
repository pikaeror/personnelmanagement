export default class Personvacation {
    id: number;
    person: number;
    vacationmilitary: number;
    vacationtype: number;
    date: Date;
    duration: number;
    trip: number;
    compensation: number;
    compensationdate: Date;
    compensationnumber: string;
    compensationdays: number;
    cancel: number;
    canceldate: Date;
    allowstart: Date;
    allowend: Date;
    holidays: number;
    canceldateend: Date;
    cancelcontinue: number;

    dateString: string;
    compensationBool: boolean;
    compensationdateString: string;
    cancelBool: boolean;
    canceldateString: string;
    allowstartString: string;
    allowendString: string;
    canceldateendString: string;
    cancelcontinueBool: boolean;

    displayPrivelege: boolean;
}