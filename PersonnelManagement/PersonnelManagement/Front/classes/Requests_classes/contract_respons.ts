import Person from '../../classes/person';
import Personcontract from '../../classes/personcontract';

export default class Contract_respons {
    constructor() {
        this.person = new Person();
        this.contract = new Personcontract();
    }
    person: Person;
    contract: Personcontract;
}