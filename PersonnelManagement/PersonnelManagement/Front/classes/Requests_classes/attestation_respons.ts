import Person from '../../classes/person';
import Personattestation from '../../classes/personattestation'

export default class Attestation_respons {
    constructor() {
        this.person = new Person();
        this.attestation = new Personattestation();
    }
    person: Person;
    attestation: Personattestation;
}