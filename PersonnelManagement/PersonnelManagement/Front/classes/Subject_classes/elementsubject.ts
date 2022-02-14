export default class elementsubject {
    constructor() {
        this.id = 0;
        this.subject1 = 0;
        this.subject2 = 0;
        this.subject3 = 0;
        this.subject4 = 0;
        this.subject5 = 0;
        this.subject6 = 0;
        this.subject7 = 0;
        this.subject8 = 0;
        this.subject9 = 0;
        this.subject10 = 0;
        this.subject11 = 0;
        this.subject12 = 0;
        this.subject13 = 0;
        this.subject14 = 0;
        this.subject15 = 0;
        this.subject16 = 0;
        this.subject17 = 0;
        this.subject18 = 0;
        this.subject19 = 0;
        this.subject20 = 0;
    }

    id: number;
    subject1: number;
    subject2: number;
    subject3: number;
    subject4: number;
    subject5: number;
    subject6: number;
    subject7: number;
    subject8: number;
    subject9: number;
    subject10: number;
    subject11: number;
    subject12: number;
    subject13: number;
    subject14: number;
    subject15: number;
    subject16: number;
    subject17: number;
    subject18: number;
    subject19: number;
    subject20: number;

    setSubjects(list: number[]): void {
        var len: number = list.length;
        if (len >= 1) { this.subject1 = list[0] }
        if (len >= 2) { this.subject2 = list[1] }
        if (len >= 3) { this.subject3 = list[2] }
        if (len >= 4) { this.subject4 = list[3] }
        if (len >= 5) { this.subject5 = list[4] }
        if (len >= 6) { this.subject6 = list[5] }
        if (len >= 7) { this.subject7 = list[6] }
        if (len >= 8) { this.subject8 = list[7] }
        if (len >= 9) { this.subject9 = list[8] }
        if (len >= 10) { this.subject10 = list[9] }
        if (len >= 11) { this.subject11 = list[10] }
        if (len >= 12) { this.subject12 = list[11] }
        if (len >= 13) { this.subject13 = list[12] }
        if (len >= 14) { this.subject14 = list[13] }
        if (len >= 15) { this.subject15 = list[14] }
        if (len >= 16) { this.subject16 = list[15] }
        if (len >= 17) { this.subject17 = list[16] }
        if (len >= 18) { this.subject18 = list[17] }
        if (len >= 19) { this.subject19 = list[18] }
        if (len == 20) { this.subject20 = list[19] }
    };

    getSubjectStructure(): number[] {
        var output: number[] = [
            this.subject1,
            this.subject2,
            this.subject3,
            this.subject4,
            this.subject5,
            this.subject6,
            this.subject7,
            this.subject8,
            this.subject9,
            this.subject10,
            this.subject11,
            this.subject12,
            this.subject13,
            this.subject14,
            this.subject15,
            this.subject16,
            this.subject17,
            this.subject18,
            this.subject19,
            this.subject20];
        output = output.filter(r => r > 0 );
        return output;
    };

    setElementByPromis(object) {
        this.id = object.id;
        this.subject1 = object.subject1;
        this.subject2 = object.subject2;
        this.subject3 = object.subject3;
        this.subject4 = object.subject4;
        this.subject5 = object.subject5;
        this.subject6 = object.subject6;
        this.subject7 = object.subject7;
        this.subject8 = object.subject8;
        this.subject9 = object.subject9;
        this.subject10 = object.subject10;
        this.subject11 = object.subject11;
        this.subject12 = object.subject12;
        this.subject13 = object.subject13;
        this.subject14 = object.subject14;
        this.subject15 = object.subject15;
        this.subject16 = object.subject16;
        this.subject17 = object.subject17;
        this.subject18 = object.subject18;
        this.subject19 = object.subject19;
        this.subject20 = object.subject20;
    };
}