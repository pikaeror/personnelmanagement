import Person from '../../classes/person';
import Vacationtype from '../../classes/vacationtype'

export default class Vacation_Response {
    constructor() {
        this.person = new Person();
        this.education = new Vacationtype();
    }
    person: Person;
    education: Vacationtype;
}