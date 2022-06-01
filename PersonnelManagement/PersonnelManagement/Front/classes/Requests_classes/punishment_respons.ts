import Person from '../../classes/person';
import Personpenalty from '../../classes/personpenalty'

export default class Punishment_respons {
    constructor() {
        this.person = new Person();
        this.penalty = new Personpenalty();
        this.penalty_string = "";
        this.penaltytype_string = "";
    }
    person: Person;
    penalty: Personpenalty;
    penalty_string: string;
    penaltytype_string: string;

}