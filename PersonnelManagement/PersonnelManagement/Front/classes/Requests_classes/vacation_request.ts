import StructureTree from '../../classes/structuretree';

export default class Vacation_Request {
    constructor() {
        this.current_structure = [];
        this.minimal_days = 0;
        this.old_year_days = false;
    }
    current_structure: StructureTree[];
    minimal_days: number;
    old_year_days: boolean;
}