import Person from '../../classes/person';
import Reward from '../../classes/reward';
import RewardType from '../../classes/rewardtype'
import Personreward from '../../classes/personreward'

export default class Award_respons {
    constructor() {
        this.person = new Person();
        this.award = new Reward();
        this.award_type = new RewardType();
        this.award_new = new Personreward();
    }
    person: Person;
    award: Reward;
    award_type: RewardType;
    award_new: Personreward;
}
