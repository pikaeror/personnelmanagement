export default class Decreeoperation {
    id: number;
    decree: number;
    subject: number; // У подразделений subject имеет знак минуса
    created: number;
    deleted: number;
    changed: number;
    changedtype: number;
    changedtext: string;

    decreeActionDescr: string;
    decreeChangedTypeDescr: string;
    decreeConnectionTypeDescr: string;
    decreeSubjectNameDescr: string;
    

    metaDateActive: Date; // Date when operation starts working
   // metaDateInactive: Date; // If element will be deleted, date when it will become inactive
    metaDecreeName: string;
    metaDecreeNameUnofficial: string;
    metaDecreeNumber: string;
    metaPurposeForSubject: number;
            // 0 - no purpose, 1 - no purpose not signed,
            // 2 - will create subject in future, 3 - will create subject in future not signed,
            // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
            // 7 - will delete subject in future not signed,
            // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
            // 16 - will be renamed not signed
    metaStatus: number;// 0 - nothing, 1 - delete decreeoperation including its subject

    dateactive: Date;
    datecustom: number;
    /**
     * Hierarchial tree. For example, if decreeoperation subject is position, then it could be like this: structurename,departmentname,positionname
     */
    tree: string;

    fullTree: string;
    /**
     * Starts from 0. For example, position in tree "structurename,departmentname,subdepartmentname,positionname" has rank 3. 
     */
    treeRank: number;

    /**
     * Visual alignment of decreeoperation. Starts from zero (most left). 
     */
    visualRank: number;
}