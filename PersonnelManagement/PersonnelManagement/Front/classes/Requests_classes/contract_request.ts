import StructureTree from '../../classes/structuretree';

export default class Contract_Request {
    constructor() {
        this.current_structure = [];
        this.last_contract_date = new Date();
    }
    current_structure: StructureTree[];
    last_contract_date: Date;
}