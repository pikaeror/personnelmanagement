import Person from '../../classes/person';
import Certificate from '../../classes/certificate';

export default class Certificate_Respons {
    constructor() {
        this.person = new Person();
        this.certificate = new Certificate();
    }
    person: Person;
    certificate: Certificate;
}