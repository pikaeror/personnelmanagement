import StructureTree from '../../classes/structuretree';

export default class Education_Request {
    constructor() {
        this.current_structure = [];
        this.educationLevel = [];
        this.specializationList = [];
        this.cvalificationList = [];
    };
    current_structure: StructureTree[];
    specializationList: string[];
    cvalificationList: string[];
    educationLevel: string[];
}