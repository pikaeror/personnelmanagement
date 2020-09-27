export default class PositionManagement {
    type: string;
    department: number;
    name: string;
    rankCap: number;
    sof: number;
    id: number;
    positiontype: number;
    positioncategory: number;
    mrd: string;
    quantity: number;
    notice: string;
    datecustom: number;
    dateactive: Date;
    replacedbycivil: number;
    replacedbycivilpositioncategory: any;
    replacedbycivilpositiontype: number;
    altrankconditiongroup: number; // 0 if no alt ranks.
    altranks: string; // If altrankconditiongroup is not null, contains next information "conditionid=rank;conditionid2=rank2;..". For example, "3=7;4=8"
    decertificate: number;
    decertificatedate: Date;
    civilranklow: number;
    civilrankhigh: number;
    replacedbycivildatelimit: number;
    replacedbycivildate: Date;
    positionsCode: string; // For multiple positions converted to JSON.
    curator: number;
    curatorlist: string;
    head: number;
    headid: number;
    opchs: number;
    nodecree: number;

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
}