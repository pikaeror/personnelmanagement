import StructureTree from '../../classes/structuretree';

export default class Rank_Request {
    constructor() {
        this.current_structure = [];
        this.first_rank_date = new Date();
        this.last_rank_date = new Date();
    }
    current_structure: StructureTree[];
    first_rank_date: Date;
    last_rank_date: Date;
}