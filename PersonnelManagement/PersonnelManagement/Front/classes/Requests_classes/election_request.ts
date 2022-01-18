import { DateRange } from 'element-ui/types/date-picker';
import StructureTree from '../../classes/structuretree';

export default class Election_Request {
    constructor() {
        this.current_structure = [];
        this.locations = [];
        this.ranks = [];
        this.places = [];
        this.startperiod = { minDate: new Date(), maxDate: new Date() };
        this.endperiod = { minDate: new Date(), maxDate: new Date() };
    };
    current_structure: StructureTree[];
    locations: string[];
    ranks: string[];
    places: string[];
    startperiod: DateRange;
    endperiod: DateRange;
}