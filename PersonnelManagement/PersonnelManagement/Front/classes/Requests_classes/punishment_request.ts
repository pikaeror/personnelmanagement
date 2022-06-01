import { DateRange } from 'element-ui/types/date-picker';
import StructureTree from '../../classes/structuretree';
import Penalty from '../penalty';

export default class Punishment_Request {
    constructor() {
        this.current_structure = [];
        this.penaltytypes = [];
        this.types = [];
        this.who = [];
        //this.period = { minDate: new Date(), maxDate: new Date() };
    };
    current_structure: StructureTree[];
    penaltytypes: number[];
    types: number[];
    who: string[];
    //period: DateRange;
    isremowed: boolean;
}