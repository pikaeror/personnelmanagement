import { DateRange } from 'element-ui/types/date-picker';
import StructureTree from '../../classes/structuretree';

export default class Punishment_Request {
    constructor() {
        this.current_structure = [];
        this.types = [];
        this.who = [];
        this.period = { minDate: new Date(), maxDate: new Date() };
    };
    current_structure: StructureTree[];
    types: string[];
    who: string[];
    period: DateRange;
}