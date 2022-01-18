import Person from '../../classes/person';

export default class Language_respons {
    constructor() {
        this.person = new Person();
        this.languages = "";
        this.skill = "";
    }
    person: Person;
    languages: string;
    skill: string;
}