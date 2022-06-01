import Person from '../../classes/person';
import Vacationtype from '../../classes/vacationtype'

export default class Vacation_Response {
    constructor() {
        this.person = new Person();
        this.education = new Vacationtype();
        this.count = 0;
        this.date_start = new Date();
    }
    person: Person;
    education: Vacationtype;
    count: number;
    date_start: Date;
}