import Person from '../../classes/person';
import Personworktrip from '../../classes/personworktrip'

export default class Trip_respons {
    constructor() {
        this.person = new Person();
        this.trip = new Personworktrip();
        this.country = "";
    }
    person: Person;
    trip: Personworktrip;
    country: string;
}