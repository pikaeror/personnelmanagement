import StructureTree from '../../classes/structuretree';

export default class Attestation_Request {
    constructor() {
        this.current_structure = [];
        this.lastdatestart = new Date();
        this.lastdateend = new Date();
        this.overdue = true;
    };
    current_structure: StructureTree[];
    lastdatestart: Date;
    lastdateend: Date;
    overdue: boolean;
}