export default class Personill {
    id: number;
    person: number;
    illtype: number;
    illcode: number;
    datestart: Date;
    dateend: Date;
    illregime: number;
    illwho: string;
    privelege: number;

    datestartString: string;
    dateendString: string;
    illtypeBool: boolean;
    privelegeBool: boolean;

    displayPrivelege: boolean;
}