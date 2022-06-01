import StructureTree from '../../classes/structuretree';

export default class Vacation_Request {
    constructor() {
        this.current_structure = [];
        this.minimal_days = 0;
        this.old_year_days = false;
        var currentdate = new Date()
        this.date_range = [new Date(currentdate), new Date(currentdate)];
        this.date_range[1].setMonth(currentdate.getMonth() + 1);
        this.dates_count = false;
    }
    current_structure: StructureTree[];
    minimal_days: number;
    old_year_days: boolean;
    date_range: Date[];
    dates_count: boolean;
}