import StructureTree from '../../classes/structuretree';

export default class Award_Request {
    constructor() {
        this.current_structure = [];
        this.awards = [];
        this.awards_type = [];
        this.keep = true;
        this.min_awards_number = 0;
        this.others = false;
    };
    current_structure: StructureTree[];
    awards: string[];
    awards_type: string[];
    keep: boolean;
    min_awards_number: number;
    others: boolean;
}