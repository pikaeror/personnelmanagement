export default class Decreemanagement {
    decreemanagementstatus: number; //  1 - create new decree, 2 - decline decree, 3 - accept decree, 4 - print decree. 5 - save decree info (date) changes. 6 - show filtered decrees
    id: number;
    name: string;
    signed: number;
    declined: number;
    dateactive: Date;
    datesigned: Date;
    user: number;
    nickname: string;
    number: string;
    historycal: number;

    dateactivestart: string; // Используется в фильтрации, с каких чисел приказы включаются
    dateactiveend: string; // по какое
    datesignedstart: string;
    datesignedend: string;

    toString() {
        return name;
    }
}