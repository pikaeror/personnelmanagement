import Decree from "./decree/decree";
import Positiontype from "./positiontype";

export default class Position {
    constructor() {
        this.id = 0;
        this.name = "";
        this.department = 0;
        this.photo = new Blob();
        this.cap = 0;
        this.sourceoffinancing = 0;
        this.positiontype = 0;
        this.positioncategory = 0;
        this.notice = "";
        this.replacedbycivil = 0;
        this.replacedbycivilpositioncategory = 0; // can be 687 means 6, 8, 7
        this.replacedbycivilpositiontype = 0;
        this.mrd = ""; // Ids of mrd listed as comma separated values "3,11,8"
        this.decertificate = 0;
        this.decertificatedate = new Date();
        this.civilranklow = 0;
        this.civilrankhigh = 0;
        this.replacedbycivildatelimit = 0;
        this.replacedbycivildate = new Date();
        this.civildecree = 0;
        this.civildecreenumber = "";
        this.civildecreedate = new Date();

        this.structure = 0;
        this.parenttype = 0;

        this.curator = 0;
        this.curatorlist = "";
        this.head = 0;
        this.headid = 0;
        this.opchs = 0;
        this.part = 0;
        this.partval = 0;

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
        this.name1 = "";
        this.name2 = "";
        this.name3 = "";
        this.name4 = "";
        this.name5 = "";
        this.name6 = "";

        this.positiontype_data = new Positiontype();
        this.last_decree = new Decree();
        
    }
    id: number;
    name: string;
    department: number;
    photo: Blob;
    cap: number;
    sourceoffinancing: number;
    positiontype: number;
    positioncategory: number;
    notice: string;
    replacedbycivil: number;
    replacedbycivilpositioncategory: number; // can be 687 means 6, 8, 7
    replacedbycivilpositiontype: number;
    mrd: string; // Ids of mrd listed as comma separated values "3,11,8"
    decertificate: number;
    decertificatedate: Date;
    civilranklow: number;
    civilrankhigh: number;
    replacedbycivildatelimit: number;
    replacedbycivildate: Date;
    civildecree: number;
    civildecreenumber: string;
    civildecreedate: Date;

    structure: number;
    parenttype: number;

    curator: number;
    curatorlist: string;
    head: number;
    headid: number;
    opchs: number;
    part: number;
    partval: number;

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
    name1: string;
    name2: string;
    name3: string;
    name4: string;
    name5: string;
    name6: string;

    positiontype_data: Positiontype;
    last_decree: Decree;
}