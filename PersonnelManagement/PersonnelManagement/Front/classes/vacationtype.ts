export default class Vacationtype {
    id: number;
    name: string;
    name2: string;
    social: boolean;
    cadet: boolean;
    civil: boolean;
    military: boolean;
    transferworkyear: boolean;
    main: number;
    maternity: number; // декретный отпуск
    durationmax: number; // максимальная длительность отпуска. 0 если не установлен
    order: number;
}