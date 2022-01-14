import StructureTree from '../../classes/structuretree';

export default class Certificate_Request {
    constructor() {
        this.current_structure = [];
        this.data_end = new Date();
        this.position_conformity = false;
    };
    current_structure: StructureTree[];
    data_end: Date;
    position_conformity: boolean;
}