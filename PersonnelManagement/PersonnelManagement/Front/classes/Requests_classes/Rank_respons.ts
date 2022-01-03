import Person from '../../classes/person';
import Rank from '../../classes/rank';

export default class Rank_respons {
    constructor() {
        this.person = new Person();
        this.rank = new Rank();
        this.date_end = new Date();
    }
    person: Person;
    rank: Rank;
    date_end: Date;
}