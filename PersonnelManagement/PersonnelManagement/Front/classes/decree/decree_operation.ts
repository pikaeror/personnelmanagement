export default class decree_operation {
    constructor() {
        this.id = 0;
        this.decree = 0;
        this.subject = 0; // У подразделений subject имеет знак минуса
        this.created = false;
        this.deleted = false;
        this.changed = false;
        this.changedtype = 0;
        
        this.dateactive = new Date();
        this.datecustom = false;
    }
    id: number;
    decree: number;
    subject: number; // У подразделений subject имеет знак минуса
    created: boolean;
    deleted: boolean;
    changed: boolean;
    changedtype: number;

    dateactive: Date;
    datecustom: boolean;
}