import StructureTree from '../../classes/structuretree';

export default class Rank_Request {
    constructor() {
        this.current_structure = [];
        this.last_rank_date = new Date();
    }
    current_structure: StructureTree[];
    last_rank_date: Date;
}