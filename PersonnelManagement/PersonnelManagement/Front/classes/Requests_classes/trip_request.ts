import { DateRange } from 'element-ui/types/date-picker';
import StructureTree from '../../classes/structuretree';

export default class Trip_Request {
    constructor() {
        this.current_structure = [];
        this.country = [];
        this.reason = [];
        this.period = { minDate: new Date(), maxDate: new Date() };
    };
    current_structure: StructureTree[];
    country: string[];
    reason: string[];
    period: DateRange;
}