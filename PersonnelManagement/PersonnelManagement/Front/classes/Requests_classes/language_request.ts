import StructureTree from '../../classes/structuretree';

export default class Language_Request {
    constructor() {
        this.current_structure = [];
        this.languages = [];
        this.skills = [];
    };
    current_structure: StructureTree[];
    languages: string[];
    skills: string[];
}