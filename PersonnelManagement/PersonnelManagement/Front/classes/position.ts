export default class Position {
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
}