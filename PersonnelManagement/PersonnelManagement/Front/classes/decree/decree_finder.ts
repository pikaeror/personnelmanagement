import { DateRange } from "element-ui/types/date-picker";

export default class DecreeFinder {
    constructor() {
        var current_date: Date = new Date();
        this.dates = {
            minDate: new Date(),
            maxDate: new Date()
        };
        this.dates.minDate.setFullYear(current_date.getFullYear() - 10);
        this.dates.maxDate.setFullYear(current_date.getFullYear() + 10);
        this.number = "";
        this.name = "";
        this.date_started = {
            minDate: new Date(),
            maxDate: new Date()
        };
        this.date_started.minDate.setFullYear(current_date.getFullYear() - 10);
        this.date_started.maxDate.setFullYear(current_date.getFullYear() + 10);
        this.nickname = "";

        this.rewrite = false
    }
    dates: DateRange;
    number: string;
    name: string;
    date_started: DateRange;
    nickname: string;

    rewrite: boolean;
}