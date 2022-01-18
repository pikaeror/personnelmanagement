import Person from '../../classes/person';
import Personelection from '../../classes/personelection';

export default class Election_respons {
    constructor() {
        this.person = new Person();
        this.election = new Personelection();
    }
    person: Person;
    election: Personelection;
}