export default class Structureinfo {
    name: string;
    id: number;

    head: number;
    positionCountSigned: number;
    positionCountUnsigned: number;
    sofNameList: string; // Array of string "," splitted
    positionCountSofSigned: string; // Array of number "," splitted
    positionCountSofUnsigned: string; // Array of number "," splitted
    varCountSigned: number;
    varCountUnsigned: number;

    hasChildren: boolean;
    grandparent: string;

    positionAddFuture: number;
    positionRemoveFuture: number;
    positionFutureDetailed: string[];

    previous: boolean;

    /**
     * JS only. 
     */
    sofsReady: string[]; 
}