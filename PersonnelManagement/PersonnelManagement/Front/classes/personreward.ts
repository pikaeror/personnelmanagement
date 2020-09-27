export default class Personreward {
    id: number;
    person: number;
    rewardtype: number;
    reward: number;
    reason: string;
    order: string;
    ordernumbertype: string;
    rewarddate: Date;
    optionstring1: string;
    optionnumber1: number;
    optionstring2: string;
    optionnumber2: number;
    optionnumber1Bool: boolean; // Там где вместо числа должно быть булевское значение. 0 - ложь. 1 - правда.
    optionnumber2Bool: boolean;
    orderwho: string;
    orderwhoid: number;
    orderid: number;
    area: number;
    areaother: number;
    externalorderwhotype: string;
    externalordertype: number;

    rewarddateString: string;

}