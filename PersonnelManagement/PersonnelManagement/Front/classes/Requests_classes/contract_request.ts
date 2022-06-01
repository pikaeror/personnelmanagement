import StructureTree from '../../classes/structuretree';

export default class Contract_Request {
    constructor() {
        var currentdate = new Date()
        this.current_structure = [];
        this.last_contract_date = [new Date(currentdate), new Date(currentdate)];
        this.last_contract_date[1].setMonth(currentdate.getMonth() + 2);
    }
    current_structure: StructureTree[];
    last_contract_date: Date[];
}