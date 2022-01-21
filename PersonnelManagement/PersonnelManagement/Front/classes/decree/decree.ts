export default class Decree {
    constructor() {
        this.id = 0;
        this.name = "";
        this.signed = false;
        this.declined = false;
        this.dateactive = new Date();
        this.datesigned = new Date();
        this.user = 0;
        this.nickname = "";
        this.number = "";
        this.historycal = 0;
    }
    id: number;
    name: string;
    signed: boolean;
    declined: boolean;
    dateactive: Date;
    datesigned: Date;
    user: number;
    nickname: string;
    number: string;
    historycal: number;
}