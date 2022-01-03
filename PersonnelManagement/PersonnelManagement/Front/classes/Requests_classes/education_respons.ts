import Person from '../../classes/person';
import Personeducation from '../../classes/personeducation'

export default class education_respons {
    constructor() {
        this.person = new Person();
        this.education = new Personeducation();
    }
    person: Person;
    education: Personeducation;
}
