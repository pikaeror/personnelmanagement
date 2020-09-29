import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip } from 'element-ui';
import rand from 'random-seed';
import _ from 'lodash';
import Rank from '../../classes/rank';
import Sourceoffinancing from '../../classes/sourceoffinancing';
import Positiontype from '../../classes/positiontype';
import Positioncategory from '../../classes/positioncategory';
import Mrd from '../../classes/mrd';
import Positionmrd from "../../classes/positionmrd";
import Decreeoperationsrequest from '../../classes/decreeoperationsrequest';
import Decreeoperation from '../../classes/decreeoperation';
import Positionhistory from '../../classes/positionhistory';
import Positiondecertificate from '../../classes/positiondecertificate';
import StructureTree from '../../classes/structuretree';
import StructureInfo from '../../classes/structureinfo';
import Person from '../../classes/person';
import Personphoto from '../../classes/personphoto';
import Pmrequest from '../../classes/pmrequest';
import download from 'downloadjs';
import Order from '../../classes/OrderHistrory/FullHistory';

rand.create();
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Dropdown.name, Dropdown);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.component(Popover.name, Popover);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Tooltip.name, Tooltip);
Vue.component(Dialog.name, Dialog);
Vue.use(Element);

class PosDepsGroup {
    posdeps: PosDep[];
    padding: boolean; // Подбивка.
}

class SubDepartment {
    id: number;
    name: string;
    department: number;
}

class Position {
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

    structure: number;
    parenttype: number;
    part: number;
    partval: number;

    curator: number;
    curatorlist: string;
    head: number;
    headid: number;
}

class PosDep {
    id: number;
    name: string;
    nameshort: string;
    department: number;
    photo: Blob;
    order: number;
    isposition: boolean;
    color: string;
    lastnonsub: boolean;
    lastsub: boolean;
    rankcap: Rank;
    sof: Sourceoffinancing;
    positiontype: Positiontype;
    positioncategory: Positioncategory;
    popover: string;
    quantity: number;
    notice: string;
    replacedbycivil: number;
    replacedbycivilpositioncategory: number; // can be 687 means 6, 8, 7
    replacedbycivilpositiontype: number;
    replacedbycivilpositioncategoryobjects: Positioncategory[];
    replacedbycivilpositiontypeobject: Positiontype;
    replacedbycivilquantity: number; // For compact view summary count.
    mrd: string; // Ids of mrd listed as comma separated values "3,11,8"
    mrds: string[];

    sofmap: Map<string, number>;
    sofmapstring: string[];

    dateactivemap: Map<string, number>;
    dateactive: string[];
    dateinactivemap: Map<string, number>;
    dateinactive: string[];

    displayaltrank: boolean; // true if alt ranks are displayed
    altranksgroup: string; // name of alt rank group
    altranks: string[];
    altranksquantity: Map<number, number>; // number of stars - quantity.
    altranksquantitystring: string[];
    altranksstars: number; // number of stars.
    decertificate: boolean;
    decertificatedate: Date;
    civilranklow: number;
    civilrankhigh: number;
    replacedbycivildatelimit: boolean;
    replacedbycivildate: Date;

    cloned: boolean; // specially when we clone object to show deletion date, prevent that original of this object will be marked as deleted.
    padding: boolean; // if yes, posdep will request padding info (if date was 3000 year and all decrees were signed)

    structure: number;
    parenttype: number;
    part: number;
    partval: number;

    curator: number;
    curatorlist: string;
    head: number;
    headid: number;
    curationstructuretrees: StructureTree[];
    headingstructuretree: StructureTree;

    headingthisstructure: boolean;
}

const fetchPositionDelay: number = 4000;
const fetchPositionForceDelay: number = 250;

Vue.component('Positionmanagingpanel', require('../positionmanagingpanel/positionmanagingpanel.vue.html'));

@Component({
    components: {

    }
})
export default class PositionslistComponent extends Vue {

    @Prop({ default: 0 })
    visible: number;

    structureinfo: StructureInfo;
    previous: StructureInfo;
    positions: Position[];
    subdepartments: SubDepartment[];
    posdeps: PosDep[];
    posdepsNew: PosDep[];

    posdepgroups: PosDepsGroup[];
    posdepsBeforeNew: PosDep[];
    posdepsAfterNew: PosDep[];
    posdepsDecreesNew: PosDep[];
    posdepsPaddingNew: PosDep[];
    posdepsBefore: PosDep[];
    posdepsAfter: PosDep[];
    posdepsPadding: PosDep[];
    posdepsDecrees: PosDep[][];


    mrdsReady: Map<number, string[]>;
    altranksReady: Map<number, string[]>;
    altranksGroupsReady: Map<number, string>;
    decreeoperations: Decreeoperation[];
    decreeoperationsDep: Decreeoperation[];
    decreeoperationsPadding: Decreeoperation[];
    decreeoperationsPaddingDep: Decreeoperation[];
    userStructure: string;
    masterpersonneleditorAccess: string;
    structureeditorAccess: string;
    personneleditorAccess: string;
    hasAccessToEdit: boolean;
    lastNonSubAnyOld: boolean; // if we have ANY last non subs.
    lastNonSubAnyNew: boolean;

    removePositionAvailable: boolean;
    removePosition: string;
    removePositiondecree: string;

    decertificatePosition: string;

    removeDepartmentAvailable: boolean;
    removeDepartment: string;

    renamePositionAvailable: boolean;
    renamePosition: string;
    renamePositiondecree: string;

    renameDepartmentAvailable: boolean;
    renameDepartment: string;

    modalDecertificatePanelVisible: boolean;
    modalPositionManagingPanelVisible: boolean;
    positionManagingType: string;
    positionManagingId: string;
    positionManagingDepartment: string;

    addNewPositionAvailable: boolean;
    addNewPosition: string;

    addNewDepartmentAvailable: boolean;
    addNewDepartment: string;
    grandparent: string; 
    title: string;

    skipFetch: boolean;
    fetchStarted: boolean;
    tries: number;
    triesMax: number;


    displayedAltRanks: number[]; // list of positions id where alt ranks 
    order_list: Order[];
    flag_first_element: boolean;
    curationstructuretrees: StructureTree[];
    headingstructuretree: StructureTree;

    decertificateID: number;
    decertificateBool: boolean;
    decertificateDate: string;

    structureInfos: StructureInfo[];

    /**
     * Прохождение службы
     */
    persons: Person[];
    photosPreview: Personphoto[];

    data() {
        return {
            structureinfo: null,
            previous: null,
            grandparent: "",
            title: "Должности",
            positions: [],
            subdepartments: [],

            posdepgroups: [],
            posdeps: [],
            posdepsNew: [],
            posdepsBeforeNew: [],
            posdepsAfterNew: [],
            posdepsDecreesNew: [],
            posdepsPaddingNew: [],
            posdepsBefore: [],
            posdepsAfter: [],
            posdepsDecrees: [],
            posdepsPadding: [],

            mrdsReady: new Map<number, string[]>(),
            altranksReady: new Map<number, string[]>(),
            altranksGroupsReady: new Map<number, string[]>(),
            decreeoperations: [],
            decreeoperationsDep: [],
            decreeoperationsPadding: [],
            decreeoperationsPaddingDep: [],

            userStructure: "0",
            masterpersonneleditorAccess: "0",
            structureeditorAccess: "0",
            personneleditorAccess: "0",
            hasAccessToEdit: false,
            lastNonSubAnyOld: false,
            lastNonSubAnyNew: false,

            addNewPositionAvailable: false,
            addNewPosition: "addnewposition",

            addNewDepartmentAvailable: false,
            addNewDepartment: "addnewdepartment",

            removePositionAvailable: false,
            removePosition: "removeposition",
            removePositiondecree: "removepositiondecree",

            decertificatePosition: "decertificateposition",

            removeDepartmentAvailable: false,
            removeDepartment: "removedepartment",

            renamePositionAvailable: false,
            renamePosition: "renameposition",
            renamePositiondecree: "renamepositiondecree",

            renameDepartmentAvailable: false,
            renameDepartment: "renamedepartment",

            modalDecertificatePanelVisible: false,
            modalPositionManagingPanelVisible: false,
            positionManagingType: "",
            positionManagingParent: "",
            positionManagingStructure: "",

            positionManagingId: "",
            positionManagingDepartment: "",
            skipFetch: false,
            fetchStarted: false,
            tries: 0,
            triesMax: 3,

            displayedAltRanks: [], // list of positions id where alt ranks 
            order_list: [],
            flag_first_element: false,
            curationstructuretrees: [],
            headingstructuretree: null,

            decertificateID: 0,
            decertificateBool: false,
            decertificateDate: "",

            structureInfos: [],

            /**
             * Прохождение службы
             */
            persons: [],
            photosPreview: [],
        }
    }

    mounted() {
        setInterval(this.forceFetch, fetchPositionForceDelay);
        setInterval(this.fetchPositionsRegular, fetchPositionDelay);
        this.posdepgroups = new Array();
        this.posdeps = new Array();
        this.posdepsBeforeNew = new Array();
        this.posdepsAfterNew = new Array();
        this.posdepsDecreesNew = new Array();
        this.posdepsPadding = new Array();
        this.posdepsPaddingNew = new Array();

        let posdepsGroupBase: PosDepsGroup = new PosDepsGroup();
        posdepsGroupBase.padding = false;
        posdepsGroupBase.posdeps = this.posdeps;
        this.posdepgroups.push(posdepsGroupBase);

        
        /*let posdepsGroupPadding: PosDepsGroup = new PosDepsGroup();
        posdepsGroupPadding.padding = true;
        posdepsGroupPadding.posdeps = this.posdepsPadding;
        this.posdepgroups.push(posdepsGroupPadding);  CURRENTLY DISABLING PADDINGS */

    }

    async getUserData() {
        this.userStructure = this.$store.state.userStructure;
        this.masterpersonneleditorAccess = this.$store.state.masterpersonneleditorAccess;
        this.structureeditorAccess = this.$store.state.structureeditorAccess; 
        this.personneleditorAccess = this.$store.state.personneleditorAccess;  

        //await (<any>Vue).getAccessStatus().then(s => {
        //    this.userStructure = s[(<any>Vue).keys["IDENTITY_STRUCTURE_KEY"]];
        //    this.masterpersonneleditorAccess = s[(<any>Vue).keys["IDENTITY_MASTERPERSONNELEDITOR_KEY"]];
        //    this.structureeditorAccess = s[(<any>Vue).keys["IDENTITY_STRUCTUREEDITOR_KEY"]];
        //    this.personneleditorAccess = s[(<any>Vue).keys["IDENTITY_PERSONNELEDITOR_KEY"]];
        //    //alert(this.userStructure);
        //}).then(fetch('api/Positions/', { credentials: 'include' })
        //    .then(response => response.json() as Promise<boolean>)
        //    .then(data => {
        //        this.hasAccessToEdit = data;
        //        if (this.$store.state.decree == null || this.$store.state.decree == 0) {
        //            this.hasAccessToEdit = false;
        //        }
        //        //alert(this.hasAccessToEdit);
        //    })
        //)

        fetch('api/Positions/', { credentials: 'include' })
            .then(response => response.json() as Promise<boolean>)
            .then(data => {
                this.hasAccessToEdit = data;
                if (this.$store.state.decree == null || this.$store.state.decree == 0) {
                    this.hasAccessToEdit = false;
                }
                //alert(this.hasAccessToEdit);
            })
    }

    /**
     * Id of this position list. Structures has negative value. 
     */
    get structureid(): number {
        return this.$store.state.positionsListId;
    }

    /**
     * Get detailed information about structure;
     */
    fetchStructureInfo() {
        fetch('api/DetailedStructure/Info' + this.structureid, { credentials: 'include' })
            .then(response => response.json() as Promise<StructureInfo[]>)
            .then(data => {
                if (data.length < 1) {
                    return;
                }
                if (this.structureinfo != null) {
                    data[0].sofsReady = this.structureinfo.sofsReady;
                }
                
                //alert(JSON.stringify(this.structureinfo));
                data.forEach(d => {
                    if (d.sofNameList != null) {
                        let sofNames: string[] = d.sofNameList.split(';');
                        let sofCountSigned: string[] = d.positionCountSofSigned.split(';');
                        let sofCountUnsigned: string[] = d.positionCountSofUnsigned.split(';');
                        let sofReady: string[] = new Array();
                        let ind: number = 0;
                        sofNames.forEach(sn => {
                            if (sofCountUnsigned[ind] != null && Number.parseFloat(sofCountUnsigned[ind]) != 0) {
                                sofReady[ind] = sn + " " + sofCountSigned[ind] + "(" + sofCountUnsigned[ind] + ")";
                            } else {
                                sofReady[ind] = sn + " " + sofCountSigned[ind];
                            }
                            //sofReady[ind] = sn + " " + sofCountSigned[ind] + "(" + sofCountUnsigned[ind] + ")";

                            ind++;
                        })
                        d.sofsReady = sofReady;
                    } else {
                        d.sofsReady = new Array();
                    }
                })

                
                this.structureinfo = data[0];
                data.shift();
                if (data.length > 0 && data[data.length - 1].previous) {
                    this.previous = data.pop();
                } else {
                    
                    this.previous = null;
                }
                this.structureInfos = data;
            });
    }

    get canEdit() {
        if (this.hasAccessToEdit) {
            return "true";
        } else {
            return "false";
        }
    }

    get positiontypes(): Positiontype[] {
        return this.$store.state.positiontypes;
    }

    get mrds(): Mrd[] {
        return this.$store.state.mrds;
    }

    get modeselectcuration(): boolean {
        return this.$store.state.modeselectcuration;
    }

    get modeselectheading(): boolean {
        return this.$store.state.modeselectheading;
    }

    get transfer(): boolean {
        // Для дебага
        //if (true == true) {
        //    return true;
        //}
        if (this.$store.state.modeappointpersonstructuredecree) {
            return true;
        }

        return false;
    }


    /**
     * Visible if button is pressed and mode is not enabled; 
     */
    get positionManagingPanelVisible(): boolean {
        return this.modalPositionManagingPanelVisible && !this.modeselectcuration && !this.modeselectheading;
    }

    fetchPositionsRegular() {
        if (!this.skipFetch) {
            
            if (!this.fetchStarted) {
                this.fetchPositions();
                this.tries = 0;
            } else {
                this.tries++;
                if (this.tries >= this.triesMax) {
                    this.fetchStarted = false;
                }
            }
            
        } else {
            this.skipFetch = false;
        }
    }


    fetchPositions() {
        if (this.visible) {
            //this.fetchPositionGroup();
            this.fetchStarted = true;
            this.fetchPositionGroup(false).then(x => {
                this.posdepgroups[0].padding = false;
                this.posdepgroups[0].posdeps = x;
                this.fetchStarted = false;
            })/*.then(r => {
                if (true == !true) {
                    this.fetchPositionGroup(true).then(s => {
                       // this.posdepgroups[1].padding = true; // Починить ебаную подбивку
                       // this.posdepgroups[1].posdeps = s;

                    });
                }
                
            })*/
            
        }

    }

    fetchPositionGroup(padding: boolean) {
        //alert("fetch");
        let newMrds: Map<number, string[]> = new Map<number, string[]>();
        let newAltranks: Map<number, string[]> = new Map<number, string[]>();
        let newAltranksGroup: Map<number, string> = new Map<number, string>();
        let positionpadding: string = "";
        if (padding) {
            positionpadding = "Positionpadding";
        }

        /**
         * Get decree operations for sub departments
         */
        let operationsRequest: Decreeoperationsrequest[] = new Array<Decreeoperationsrequest>();
        let currentDateStart: Date = new Date(this.$store.state.date);
        if (padding) {
            currentDateStart.setFullYear(3000);
        }
        let result = this.getUserData().then(x =>


            /**
             *  Fetch positions
             */
            fetch('api/Positions/' + positionpadding + this.$store.state.positionsListId, { credentials: 'include' })
                .then(response => response.json() as Promise<Position[]>)
                .then(data => {
                    this.posdepsNew = new Array();
                    this.positions = data;
                    this.positions.forEach(p => this.posdepsNew.push(this.positionToPosDep(p, padding)));

                //}).then(x => fetch('api/Positions/Stringmrds' + this.$store.state.positionsListId, { credentials: 'include' })
                //    .then(response => response.json() as Promise<string>)
                //    .then(data => {
                //        let mrds: string[] = data.split(';');
                //        //alert(mrds.length + "   " + this.positions.length);
                //        let index: number = 0;
                //        this.positions.forEach(pd => {
                //            if (this.positions.length > index) {
                //                if (mrds != null && mrds[index] != null) { // Sometimes  mrds[index] UNDEFINED
                //                    newMrds.set(this.positions[index].id, mrds[index].split(','));
                //                }
                //                index++;
                //            }
                //        })
                //        this.mrdsReady = newMrds;
                //    }))
                }).then(x => fetch('api/Positions/Stringmrdids' + this.$store.state.positionsListId, { credentials: 'include' })
                    .then(response => response.json() as Promise<string>)
                    .then(data => {
                        let mrds: string[] = data.split(';');
                        //alert(mrds.length + "   " + this.positions.length);
                        let index: number = 0;
                        this.positions.forEach(pd => {
                            if (this.positions.length > index) {
                                if (mrds != null && mrds[index] != null) { // Sometimes  mrds[index] UNDEFINED
                                    let splittedMrds: string[] = mrds[index].split(',');
                                    //Number.parseInt(splittedMrds[0]);
                                    newMrds.set(Number.parseInt(splittedMrds[0]), splittedMrds.slice(1));
                                }
                                index++;
                            }
                        })
                        this.mrdsReady = newMrds;
                    }))
                .then(x => fetch('api/Positions/Stringaltranks' + this.$store.state.positionsListId, { credentials: 'include' })
                    .then(response => response.json() as Promise<string>)
                    .then(data => {
                        //alert(data);
                        let altranksByPosition: string[] = data.split(';');
                        let index: number = 0;
                        this.positions.forEach(pd => {
                            if (this.positions.length > index && altranksByPosition.length > index) {
                                let splitted: string[] = altranksByPosition[index].split(':');
                                if (splitted.length >= 2) {

                                    newAltranksGroup.set(this.positions[index].id, splitted[0]);
                                    newAltranks.set(this.positions[index].id, splitted[1].split(','));
                                }

                                index++;
                            }

                        })
                        this.altranksReady = newAltranks;
                        this.altranksGroupsReady = newAltranksGroup;
                    }))
                .then(x => {
                    /**
                      * Get decree operations for positions
                      */
                    let operationsRequest: Decreeoperationsrequest[] = new Array<Decreeoperationsrequest>();
                    this.positions.forEach(s => {
                        let decreeoperationsrequest: Decreeoperationsrequest = new Decreeoperationsrequest();
                        decreeoperationsrequest.subjectId = s.id; // У подразделений subject имеет знак минуса
                        decreeoperationsrequest.requestedDate = currentDateStart;
                        if (!padding) {
                            decreeoperationsrequest.padding = 0;
                        } else {
                            decreeoperationsrequest.padding = 1;
                        }

                        decreeoperationsrequest.detailed = 0;
                        operationsRequest.push(decreeoperationsrequest);
                    })

                    fetch('/api/DecreeOperationsRequests', {
                        method: 'post',
                        body: JSON.stringify(operationsRequest),
                        credentials: 'include',
                        headers: new Headers({
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        })
                    })
                        .then(response => response.json() as Promise<Decreeoperation[]>)
                        .then((response) => {
                            if (!padding) {
                                this.decreeoperations = response;
                            } else {
                                this.decreeoperationsPadding = response;
                            }
                        });

                })
                .then(x => {
                    /**
                     * If compact mode enabled.
                     */
                    if (this.$store.state.positioncompact > 0) {
                        this.posdepsNew = this.makeCompact(this.posdepsNew, padding);

                    /**
                     * If compact mode is not enabled, we generate shortified sof and alt ranks quantity.
                     */
                    } else {
                        if (this.posdepsNew != null) {
                            this.posdepsNew.forEach(p => {
                                p.sofmapstring = new Array();
                                if (p.sofmap != null) { // FIX ERROR HERE SOMETIMES IT IS NULL
                                    p.sofmap.forEach((v, k) => {
                                        let str = k.toUpperCase().split(' ').map(function (item) { return item[0] }).join('');
                                        //str = str + "-" + v.toString() + "\n";
                                        p.sofmapstring.push(str);
                                    })
                                }

                            })
                        }
                    }

                    this.lastNonSubAnyNew = false;

                    let index: number = 0;
                    if (this.posdepsNew != null) {
                        this.posdepsNew.forEach(p => {
                            //alert(p.structure);
                            if (((p.parenttype == 0 || p.parenttype == 1) && p.department == this.structureid)
                                || (p.parenttype == 2 && (p.structure == -this.structureid || p.headingthisstructure))) {
                                p.order = index;
                                index++;
                            }
                        });
                    }

                    let posDep = this.posdepsNew.find(p => p.order == index - 1);
                    if (posDep != null) {
                        posDep.lastnonsub = true;
                        this.lastNonSubAnyNew = true;
                    }

                    this.posdepsNew.sort(this.compare);


                    if (!padding) {

                    }
                    this.posdeps = this.posdepsNew;  // Пока что скрываем нахуй, потому что будем возвращать
                    // Разобраться, почему верх и низ иногда меняются местами.

                    this.lastNonSubAnyOld = this.lastNonSubAnyNew;
                    this.title = this.$store.state.positionsListTitle;
                    if (this.title != this.$store.state.grandparent) {
                        this.grandparent = this.$store.state.grandparent;
                    }
                })
                .then(x => {
                    /**
                     * Here was a code about subdepartments decree requests but it was removed 
                     */
                    /**
                     * Sort posdeps by categories
                     */
                    this.posdepsBeforeNew = new Array();
                    this.posdepsAfterNew = new Array();
                    this.posdepsDecreesNew = new Array();
                    // MARK1
                })
        ).then(x => {
            return this.posdepsNew;
            });
        /**
         *  Fetch persons - заодно кроме запроса должностей, их типа и т.д. мы одновременно спрашиваем всех сотрудников и кандидатов, 
         *  ибо там где есть доступ на структуру, там есть доступ и на кандидатов
         */
        let positionslist: PositionslistComponent = this;
        fetch('api/Person/' + this.$store.state.positionsListId, { credentials: 'include' })
            .then(response => response.json() as Promise<Person[]>)
            .then(data => {
                
                data.forEach(p => { positionslist.prepareToImport(p); });
                this.persons = data;
                fetch('api/Person/Positionsphotospreview' + this.$store.state.positionsListId, { credentials: 'include' })
                    .then(response => {
                        return response.json() as Promise<Personphoto[]>;
                    })
                    .then(personphotos => {
                        if (personphotos != null) {

                            personphotos.forEach(p => p.photo64 = p.photo64header + "," + p.photo64);
                            this.photosPreview = personphotos;
                            //this.lastSearchFio = fio;
                        }
                    })
            });


        return result;
    }

    makeCompact(posdeps: PosDep[], padding: boolean): PosDep[] {
        let positionsCompact: PosDep[] = new Array();

        // 0 - no purpose, 1 - no purpose not signed,
        // 2 - will create subject in future, 3 - will create subject in future not signed,
        // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
        // 7 - will delete subject in future not signed, 8 - already renamed subject, 9 - already renamed subject 
        // not signed, 10 - will rename subject, 11 - will rename subject not signed,
        // 12 - deleted, 13 - deleted not signed,
        let positionsCompactSigned: PosDep[] = new Array();
        let positionsCompactUnsigned: PosDep[] = new Array();

        posdeps.forEach(posdep => {
            ////////////////////////////////////////////////////
            if (this.isNotSignedAndCreated(posdep)) {
                //let p: PosDep = positionsCompactUnsigned.find(p => p.positiontype.id == posdep.positiontype.id);
                let p: PosDep = positionsCompactUnsigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    posdep.dateinactivemap = new Map<string, number>();
                    positionsCompactUnsigned.push(posdep);
                } else {
                    this.putToSofmap(p.sofmap, posdep, padding);
                    this.putToAltranksquantityMap(p.altranksquantity, posdep, padding);
                    let currentlyInactive: boolean = this.putToDateactiveMap(p.dateactivemap, posdep, padding);
                    if (!currentlyInactive) {
                        // p.quantity += 1;
                        p.quantity += posdep.partval;
                        p.replacedbycivilquantity += posdep.replacedbycivilquantity;
                    }
                }
                return;
            }

            if (this.isSignedAndWillBeCreated(posdep)) {
                let p: PosDep = positionsCompactSigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                //let p: PosDep = positionsCompactSigned.find(p => p.positiontype.id == posdep.positiontype.id);
                if (p == null) {
                    posdep.dateinactivemap = new Map<string, number>();
                    positionsCompactSigned.push(posdep);
                } else {
                    this.putToSofmap(p.sofmap, posdep, padding);
                    this.putToAltranksquantityMap(p.altranksquantity, posdep, padding);
                    let currentlyInactive: boolean = this.putToDateactiveMap(p.dateactivemap, posdep, padding);
                    if (!currentlyInactive) {
                        // p.quantity += 1;
                        p.quantity += posdep.partval;
                        p.replacedbycivilquantity += posdep.replacedbycivilquantity;
                    }
                }
                return;
            }

            if (this.isNotSignedAndWillBeCreated(posdep)) {
                let p: PosDep = positionsCompactUnsigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    posdep.dateinactivemap = new Map<string, number>();
                    positionsCompactUnsigned.push(posdep);
                } else {
                    this.putToSofmap(p.sofmap, posdep, padding);
                    this.putToAltranksquantityMap(p.altranksquantity, posdep, padding);
                    let currentlyInactive: boolean = this.putToDateactiveMap(p.dateactivemap, posdep, padding);
                    if (!currentlyInactive) {
                        // p.quantity += 1;
                        p.quantity += posdep.partval;
                        p.replacedbycivilquantity += posdep.replacedbycivilquantity;
                    }
                }
                return;
            }

            if (this.isDeletedUnsigned(posdep)) {
                let p: PosDep = positionsCompactUnsigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    let clone: PosDep = _.cloneDeep(posdep);
                    this.clearifyClone(clone);
                    positionsCompactUnsigned.push(clone);
                    posdep.cloned = true;
                } else {
                    this.putToDateinactiveMap(p.dateinactivemap, posdep); 
                }
            }

            if (this.isWillBeDeletedSigned(posdep)) {
                let p: PosDep = positionsCompactSigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    let clone: PosDep = _.cloneDeep(posdep);
                    this.clearifyClone(clone);
                    positionsCompactSigned.push(clone);
                    posdep.cloned = true;
                } else {
                    this.putToDateinactiveMap(p.dateinactivemap, posdep); 
                }
            }

            if (this.isWillBeDeletedUnsigned(posdep)) {
                let p: PosDep = positionsCompactUnsigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    let clone: PosDep = _.cloneDeep(posdep);
                    this.clearifyClone(clone);
                    positionsCompactUnsigned.push(clone);
                    posdep.cloned = true;
                } else {
                    this.putToDateinactiveMap(p.dateinactivemap, posdep);
                }
            }

            /**
             * Other cases.
             */
            if (this.isSignedAndCreated(posdep)) {
                let p: PosDep = positionsCompactSigned.find(p => p.positiontype.id == posdep.positiontype.id && p.positioncategory == posdep.positioncategory);
                if (p == null) {
                    posdep.dateinactivemap = new Map<string, number>(); // - We clear dateinactive map if it exists.
                    positionsCompactSigned.push(posdep);
                } else {
                    this.putToSofmap(p.sofmap, posdep, padding);
                    this.putToAltranksquantityMap(p.altranksquantity, posdep, padding);
                    let currentlyInactive: boolean = this.putToDateactiveMap(p.dateactivemap, posdep, padding);
                    if (!currentlyInactive) {
                        p.quantity += 1;
                        p.replacedbycivilquantity += posdep.replacedbycivilquantity;
                    }
                        
                }
                return;
            }
                
        })
        positionsCompact = positionsCompact.concat(positionsCompactSigned, positionsCompactUnsigned);

        positionsCompact.forEach(p => {
            p.sofmapstring = new Array();
            if (p.sofmap != null) {
                p.sofmap.forEach((v, k) => {
                    let str = k.toUpperCase().split(' ').map(function (item) { return item[0] }).join('');
                    str = str + "-" + v.toString() + "\n";
                    p.sofmapstring.push(str);
                })
            }

            p.dateactive = new Array();
            if (p.dateactivemap != null) {
                p.dateactivemap.forEach((v, k) => {
                    //let str = k.toUpperCase().split(' ').map(function (item) { return item[0] }).join('');
                    let str = v.toString() + " вводится с " + this.beautifyDate(k) + "\n";
                    p.dateactive.push(str);
                })
            }

            p.dateinactive = new Array();

            if (p.dateinactivemap != null) {
                p.dateinactivemap.forEach((v, k) => {
                    //let str = k.toUpperCase().split(' ').map(function (item) { return item[0] }).join('');
                    let str = v.toString() + " сокращается с " + this.beautifyDate(k) + "\n";
                    p.dateinactive.push(str);
                })
            }

            p.altranksquantitystring = new Array();
            p.altranksquantity.forEach((v, k) => {
                let str: string = "";
                for (let i: number = 0; i < k; i++) {
                    str += "*";
                }
                str += v;
                p.altranksquantitystring.push(str);
            })
            
            //alert(p.sofmapstring);
        })

        //this.posdepsNew.length = 0;
        //positionsCompact.forEach(p => {
        //    this.posdepsNew.push(p);
        //})
        return positionsCompact;
    }

    /**
     * If posdep is cloned to display deletion date, we remove unnecessary info
     * @param posdep
     */
    clearifyClone(posdep: PosDep) {
        posdep.quantity = 0;
        posdep.replacedbycivilquantity = 0;
        posdep.sofmap = new Map<string, number>();
        posdep.dateactivemap = new Map<string, number>();
        posdep.altranksquantity = new Map<number, number>();
    }

    /**
     * returns true if action successful => date actual for future.
     * @param sofmap
     * @param posdep
     */
    putToSofmap(sofmap: Map<string, number>, posdep: PosDep, padding: boolean): boolean {
        let dateElementInactive: string = this.getDecreeStartDate(posdep);
        let date: Date = new Date(dateElementInactive);
        let currentDate: Date = new Date(this.$store.state.date);

        if (currentDate >= date || padding) {
            if (sofmap.has(posdep.sof.name)) {
                let newValue: number = sofmap.get(posdep.sof.name) + posdep.partval;
                sofmap.set(posdep.sof.name, newValue);
            } else {
                sofmap.set(posdep.sof.name, posdep.partval);
            }
            return true;
        }
        return false;
    }

    /**
     * returns true if action successful => date actual for future.
     * @param altranksquantityMap
     * @param posdep
     */
    putToAltranksquantityMap(altranksquantityMap: Map<number, number>, posdep: PosDep, padding: boolean): boolean {
        let dateElementInactive: string = this.getDecreeStartDate(posdep);
        let date: Date = new Date(dateElementInactive);
        let currentDate: Date = new Date(this.$store.state.date);

        if (currentDate >= date || padding) {
            if (altranksquantityMap != null && posdep.altranksstars > 0) {
                if (altranksquantityMap.has(posdep.altranksstars)) {
                    let newValue: number = altranksquantityMap.get(posdep.altranksstars) + 1;
                    altranksquantityMap.set(posdep.altranksstars, newValue);
                } else {
                    altranksquantityMap.set(posdep.altranksstars, 1);
                }
            }
            return true;
        }
        return false;
    }

    //posDep.altranksquantity.set(posDep.altranks.length, 1);

    /**
     * returns true if action successful => date actual for future.
     * @param dateactivemap
     * @param posdep
     */
    putToDateactiveMap(dateactivemap: Map<string, number>, posdep: PosDep, padding: boolean): boolean {
        let dateElementActive: string = this.getDecreeStartDate(posdep);
        let date: Date = new Date(dateElementActive);
        let currentDate: Date = new Date(this.$store.state.date);
        if (currentDate < date && !padding) {
            if (dateElementActive != null && dateElementActive != "") {
                if (dateactivemap.has(dateElementActive)) {
                    let newValue: number = dateactivemap.get(dateElementActive) + 1;
                    dateactivemap.set(dateElementActive, newValue);
                } else {
                    dateactivemap.set(dateElementActive, 1);
                }
            }
            return true;
        }
        return false;
        
    }

    /**
     * returns true if has inactive date. False if doesn't
     * @param dateinactivemap
     * @param posdep
     */
    putToDateinactiveMap(dateinactivemap: Map<string, number>, posdep: PosDep): boolean {
        let dateElementInactive: string = this.getElementInactiveDate(posdep);

            if (dateElementInactive != null && dateElementInactive != "") {
                if (dateinactivemap.has(dateElementInactive)) {
                    let newValue: number = dateinactivemap.get(dateElementInactive) + 1;
                    dateinactivemap.set(dateElementInactive, newValue);
                } else {
                    dateinactivemap.set(dateElementInactive, 1);
                }
                return true;
            }
            return false;
    }


    forceFetch() {
        if (this.$store.state.forcePositionUpdate) {
            this.$store.commit("setForcePositionUpdate", false);
            this.fetchPositions();
            this.fetchStructureInfo(); // Only on force fetches currently;
        }
    }


    close() {
        this.$store.commit("setPositionsListId", 0);
    }

    rankToString(rank: Rank): string {
        if (rank == null) {
            return "";
        } else {
            return rank.name.split(' ').slice(0, -2).join(" ");
        }
    }

    sofToString(sof: Sourceoffinancing): string {
        if (sof == null) {
            return "";
        } else {
            return sof.name;
        }
    }

    positiontypeSame(positiontype: Positiontype): boolean {
        return (positiontype.name == positiontype.nameshort || positiontype.nameshort == null || positiontype.nameshort.length == 0);
    }

    positiontypeToString(positiontype: Positiontype): string {
        if (positiontype == null) {
            return "";
        } else {
            return positiontype.name;
        }
    }

    positiontypeToStringShort(positiontype: Positiontype): string {
        if (positiontype == null) {
            return "";
        } else {
            if (positiontype.nameshort == null || positiontype.nameshort.length == 0) {
                return positiontype.name;
            } else {
                return positiontype.nameshort;
            }
            
        }
    }

    positiontypeGetId(positiontype: Positiontype): number {
        if (positiontype == null) {
            return 0;
        } else {
            return positiontype.id;
        }
    }

    positioncategoryReplacedToString(pcs: any): string {
        if (pcs == null || pcs.length == 0 || !Array.isArray(pcs)) {
            return "";
        } else {
            //alert(JSON.stringify(positioncategories));
            let positioncategorystring: string = "";
            pcs.forEach(pc => {
                positioncategorystring += "«" + pc.name + "», ";
            })
            if (positioncategorystring.length > 0) {
                positioncategorystring = positioncategorystring.substring(0, positioncategorystring.length - 2);
            }
            return positioncategorystring;
        }
    }



    positioncategoryToString(pcs: Positioncategory): string {
        if (pcs == null) {
            return "";
        }
        if (pcs.officer == 0) {
            return pcs.name;
        } else {
            return "";
        }
        //if (pcs == null || pcs.length == 0 || !Array.isArray(pcs)) {
        //    return "";
        //} else {
        //    //alert(JSON.stringify(positioncategories));
        //    let positioncategorystring: string = "";
        //    pcs.forEach(pc => {
        //        positioncategorystring += "«" + pc.name + "», ";
        //    })
        //    if (positioncategorystring.length > 0) {
        //        positioncategorystring = positioncategorystring.substring(0, positioncategorystring.length - 2);
        //    }
        //    return positioncategorystring;
        //}
    }

    displayClassIfAvaiable(posdep: PosDep): string {
        if (posdep.civilranklow > 0) {
            return "Класс " + posdep.civilrankhigh + "-" + posdep.civilranklow;
        } else {
            return "";
        }
    }

    displayClassCivilIfAvaiable(posdep: PosDep): string {
        if (posdep.civilranklow > 0 && posdep.positioncategory != null && posdep.positioncategory.civil > 0) {
            return "Класс " + posdep.civilrankhigh + "-" + posdep.civilranklow;
        } else {
            return "";
        }
    }

    markAltrank(marked: boolean, positionid: number) {
        if (marked) {
            this.displayedAltRanks.push(positionid);
        } else {
            this.displayedAltRanks = this.displayedAltRanks.filter(item => item != positionid);
            
        }
        //alert(marked + "  " + positionid);
    }

    positionToPosDep(position: Position, padding: boolean): PosDep {
        let posDep: PosDep = new PosDep();
        //alert(position.rankCap);
        posDep.id = position.id;
        posDep.department = position.department;
        posDep.structure = position.structure;
        posDep.parenttype = position.parenttype;
        //posDep.name = position.name;
        posDep.isposition = true;
        posDep.lastnonsub = false;
        posDep.lastsub = false;
        
        posDep.part = position.part;
        if (posDep.part > 0) {
            posDep.partval = position.partval;
            posDep.quantity = position.partval;
        } else {
            posDep.partval = 1;
            posDep.quantity = 1;
        }

        posDep.dateactivemap = new Map<string, number>();
        posDep.notice = position.notice;
        let currentlyInactive: boolean = this.putToDateactiveMap(posDep.dateactivemap, posDep, padding);
        posDep.dateinactivemap = new Map<string, number>();
        this.putToDateinactiveMap(posDep.dateinactivemap, posDep);
        if (currentlyInactive) {
            posDep.quantity = 0; // Object will be added in future, so we don't count it.
        }

        if (position.positiontype > 0) {
            posDep.positiontype = this.positiontypes.find(e => e.id == position.positiontype);
        }

        if (position.positioncategory > 0) {

            posDep.positioncategory = (<Positioncategory[]>this.$store.state.positioncategories).find(e => e.id == position.positioncategory);
        }

        //replacedbycivilpositioncategoryobject: Positioncategory;
        //replacedbycivilpositiontypeobject: Positiontype;

        if (position.replacedbycivil > 0) {
            posDep.replacedbycivil = 1;
            posDep.replacedbycivilquantity = 1;
            if (currentlyInactive) {
                posDep.replacedbycivilquantity = 0; // Object will be added in future, so we don't count it.
            }
            posDep.replacedbycivilpositioncategory = position.replacedbycivilpositioncategory;
            posDep.replacedbycivilpositiontype = position.replacedbycivilpositiontype;
            if (posDep.replacedbycivilpositioncategory > 0) {
                let rbcpcString: string[] = posDep.replacedbycivilpositioncategory.toString().split('');
                let rbcpcArray: Positioncategory[] = new Array();
                rbcpcString.forEach(r => {
                    let index = Number.parseInt(r);
                    rbcpcArray.push((<Positioncategory[]>this.$store.state.positioncategories).find(e => e.id == index));
                })
                //posDep.replacedbycivilpositioncategoryobject = (<Positioncategory[]>this.$store.state.positioncategories).find(e => e.id == position.replacedbycivilpositioncategory);
                posDep.replacedbycivilpositioncategoryobjects = rbcpcArray;
                //alert(JSON.stringify(posDep.replacedbycivilpositioncategoryobjects));
            }
            if (posDep.replacedbycivilpositiontype > 0) {
                posDep.replacedbycivilpositiontypeobject = this.positiontypes.find(e => e.id == position.replacedbycivilpositiontype);
            }
            
        } else {
            posDep.replacedbycivil = 0;
            posDep.replacedbycivilquantity = 0;
            posDep.replacedbycivilpositioncategory = 0;
            posDep.replacedbycivilpositiontype = 0;
            posDep.replacedbycivilpositioncategoryobjects = null;
            posDep.replacedbycivilpositiontypeobject = null;
        }

        if (position.cap > 0) {
            
            posDep.rankcap = (<Rank[]>this.$store.state.ranks).find(e => e.id == position.cap);
        }
        if (position.sourceoffinancing > 0) {
            posDep.sof = (<Sourceoffinancing[]>this.$store.state.sofs).find(e => e.id == position.sourceoffinancing);
            posDep.sofmap = new Map<string, number>();
            if (currentlyInactive) {
                posDep.sofmap.set(posDep.sof.name, 0); // Object will be added in future, so we don't count it.
            } else {
                posDep.sofmap.set(posDep.sof.name, posDep.partval);
            }
            //posDep.sofmap.set(posDep.sof.name, 1);
        }

        if (this.mrdsReady.has(position.id)) {
            posDep.mrds = this.mrdsReady.get(position.id);
        }

        //posDep.altranksgroup = position.
        if (this.altranksGroupsReady.has(position.id)) {
            posDep.altranksgroup = this.altranksGroupsReady.get(position.id);
        }

        posDep.altranksstars = 0;
        posDep.altranksquantity = new Map<number, number>();
        if (this.altranksReady.has(position.id)) {
            posDep.altranks = this.altranksReady.get(position.id);
            
            posDep.altranksstars = posDep.altranks.length;
            if (posDep.altranksstars > 0) {
                //posDep.altranksquantity.set(posDep.altranks.length, 1);
                if (currentlyInactive) {
                    posDep.altranksquantity.set(posDep.altranks.length, 0); // Object will be added in future, so we don't count it.
                } else {
                    posDep.altranksquantity.set(posDep.altranks.length, 1);
                }
            }
            
        }
        //posDep.dateactivesimple = posDep.dateactive.toString();
        posDep.displayaltrank = false;
        if (this.displayedAltRanks.indexOf(posDep.id) > -1) {
            posDep.displayaltrank = true;
        }

        if (position.decertificate > 0) {
            posDep.decertificatedate = position.decertificatedate;
            posDep.decertificate = true;
        }
        posDep.civilrankhigh = position.civilrankhigh;

        posDep.replacedbycivildatelimit = false;
        posDep.civilranklow = position.civilranklow;
        if (position.replacedbycivildatelimit > 0) {
            posDep.replacedbycivildatelimit = true;
            posDep.replacedbycivildate = position.replacedbycivildate;
        }

        if (position.curator > 0) {
            posDep.curator = 1;
            posDep.curatorlist = position.curatorlist;
        }

        posDep.headingthisstructure = false;
        if (position.head > 0 && position.headid > 0) {
            posDep.head = 1;
            posDep.headid = position.headid;
            if (posDep.headid == this.structureid || posDep.headid == -this.structureid) {
                //alert('true');
                posDep.headingthisstructure = true;
            }
        }

        posDep.padding = padding;
        //altranksquantity: Map<string, number>; !!!!!!!!!
        posDep.cloned = false;
        posDep.popover = "popover" + posDep.id;
        return posDep;
    }

    departmentToPosDep(department: SubDepartment, padding: boolean): PosDep {
        let posDep: PosDep = new PosDep();
        posDep.id = department.id;
        posDep.department = department.department;
        posDep.name = department.name;
        posDep.isposition = false;
        posDep.lastnonsub = false;
        posDep.lastsub = false;
        posDep.padding = padding;
        posDep.cloned = false;
        posDep.popover = "popover" + posDep.id;
        return posDep;
    }

    sortPosDepsByDepartment(posdepsList: PosDep[], departmentid: number, lastindex: number, color: string): number {
        posdepsList.forEach(p => {
            if (p.isposition && p.department == departmentid) {
                lastindex++;
                p.order = lastindex;
                p.color = color;
            }
        });
        return lastindex;
    }

    getRandomColor(departmentid: number): string {

        var random = new rand(departmentid); 
        var letters = '0123456789ABCDEF';
        var color = '#';
        for (var i = 0; i < 6; i++) {
            color += letters[Math.floor(random.random() * 16)];
        }
        //alert(color);
        return color;
       
    }


    compare(a: PosDep, b: PosDep) {
        if (a.order < b.order) {
            return -1;
        }
        if (a.order > b.order) {
            return 1;
        }
        // a must be equal to b
        return 0;
    }

   
    addPositionID(id): any {
        return this.addNewPosition + "_" + id;
    }

    addPositionIDInSubdep(posdep: PosDep): any {
        //alert(posdep.id + "  " + posdep.department);
        if (posdep.isposition) {
            return this.addNewPosition + "_" + posdep.department;
        } else {
           // alert(this.addNewPosition + "_" + posdep.id);
            return this.addNewPosition + "_" + posdep.id;
        }
    }

    renamePositionID(id): any {
        return this.renamePosition + "_" + id;
    }

    renamedecreePositionID(id): any {
        return this.renamePositiondecree + "_" + id;
    }

    removePositionID(id): any {
        return this.removePosition + "_" + id;
    }

    removedecreePositionID(id): any {
        return this.removePositiondecree + "_" + id;
    }

    decertificatePositionID(id): any {
        return this.decertificatePosition + "_" + id;
    }

    addDepartmentID(id): any {
        return this.addNewDepartment + "_" + id;
    }

    renameDepartmentID(id): any {
        return this.renameDepartment + "_" + id;
    }

    removeDepartmentID(id): any {
        return this.removeDepartment + "_" + id;
    }

    hasPhoto(position: Position) {
        if (position.id == 22) {
            return true;
        }


        if (position.photo == null) {
            return false; 
        } else {
            return true;
        }
    }

    photoSrc(position: Position) {
        if (position.id == 22) {
            return "url(/img/horolskiy.JPG)";
        }

        if (this.hasPhoto(position)) {
            return "";
        } else {
            return "url(/img/awesome/vacant.png)";
        }
    }

    photoCompact(position: Position) {
        return "url(/img/icons/positioncompact.png)";
    }

    posdepColor(posdep: PosDep) {
        return posdep.color + " 0px 2px 4px";
    }

    displayAltranksInterface(posdep: PosDep) {
        if (posdep.altranksgroup != null && posdep.altranksgroup != "") {
            return true;
        } else {
            return false;
        }
    }

     // 0 - no purpose, 1 - no purpose not signed, 
     // 2 - will create subject in future, 3 - will create subject in future not signed,
     // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
     // 7 - will delete subject in future not signed,
     // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
     // 16 - will be renamed not signed
    isSignedAndCreated(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0;
            } else {
                return false;
            }
        }
        
        
    }

    isNotSignedAndCreated(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).length > 0;
            } else {
                return false;
            }
        }
        
    }

    isSignedAndWillBeCreated(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0;
            } else {
                return false;
            }
        }

        
    }

    isNotSignedAndWillBeCreated(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).length > 0;
            } else {
                return false;
            }
        }

        
    }

    isDeletedUnsigned(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null && !posdep.cloned) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null && !posdep.cloned) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).length > 0;
            } else {
                return false;
            }
        }

        
    }

    isWillBeDeletedSigned(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null && !posdep.cloned) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null && !posdep.cloned) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).length > 0;
            } else {
                return false;
            }
        }

        
    }

    isWillBeDeletedUnsigned(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null && !posdep.cloned) {
                return this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).length > 0;
            } else {
                return false;
            }
        } else {
            if (this.decreeoperationsPadding != null && !posdep.cloned) {
                return this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).length > 0;
            } else {
                return false;
            }
        }

        
    }


    isRenamedNotSigned(posdep: PosDep): any {
        if (!posdep.padding) {
            if (!posdep.isposition) {
                if (this.decreeoperationsDep != null && !posdep.cloned) {
                    return this.decreeoperationsDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 14).length > 0;
                } else {
                    return false;
                }
            }
        } else {
            if (!posdep.isposition) {
                if (this.decreeoperationsPaddingDep != null && !posdep.cloned) {
                    return this.decreeoperationsPaddingDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 14).length > 0;
                } else {
                    return false;
                }
            }
        }

        
    }

    isWillBeRenamed(posdep: PosDep): any {
        if (!posdep.padding) {
            if (!posdep.isposition) {
                if (this.decreeoperationsDep != null && !posdep.cloned) {
                    return this.decreeoperationsDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 15).length > 0;
                } else {
                    return false;
                }
            }
        } else {
            if (!posdep.isposition) {
                if (this.decreeoperationsPaddingDep != null && !posdep.cloned) {
                    return this.decreeoperationsPaddingDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 15).length > 0;
                } else {
                    return false;
                }
            }
        }

        
    }

    isWillBeRenamedNotSigned(posdep: PosDep): any {
        if (!posdep.padding) {
            if (!posdep.isposition) {
                if (this.decreeoperationsDep != null && !posdep.cloned) {
                    return this.decreeoperationsDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 16).length > 0;
                } else {
                    return false;
                }
            }
        } else {
            if (!posdep.isposition) {
                if (this.decreeoperationsPaddingDep != null && !posdep.cloned) {
                    return this.decreeoperationsPaddingDep.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 16).length > 0;
                } else {
                    return false;
                }
            }
        }

        
    }

    getDecreeName(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).metaDecreeName;
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).metaDecreeName;
                }
            }

            return "";
        } else {
            if (this.decreeoperationsPadding != null) {
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).metaDecreeName;
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).metaDecreeName;
                }
            }

            return "";
        }

        
    }


    
    getDecreeStartDate(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).metaDateActive.toString().slice(0, 10);
                }
            }
            return "";
        } else {
            if (this.decreeoperationsPadding != null) {
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 4).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 2).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 5).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 3).metaDateActive.toString().slice(0, 10);
                }
            }
            return "";
        }

        
    }

    getElementInactiveDate(posdep: PosDep): any {
        if (!posdep.padding) {
            if (this.decreeoperations != null) {
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 12).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 12).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperations.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).length > 0) {
                    return this.decreeoperations.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).metaDateActive.toString().slice(0, 10);
                }
            }
            return "";
        } else {
            if (this.decreeoperationsPadding != null) {
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 12).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 12).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 6).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 13).metaDateActive.toString().slice(0, 10);
                }
                if (this.decreeoperationsPadding.filter(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).length > 0) {
                    return this.decreeoperationsPadding.find(d => d.subject == posdep.id && d.metaPurposeForSubject == 7).metaDateActive.toString().slice(0, 10);
                }
            }
            return "";
        }
    }


    testDate(posdep: PosDep): any {
        alert(this.getElementInactiveDate(posdep));
    }

    hasElementInactiveDate(posdep: PosDep): boolean {
        let str: string = this.getElementInactiveDate(posdep);
        if (str != null && str.length > 0) {
            return true;
        } else {
            return false;
        }
    }


    getElementDecertificateDate(posdep: PosDep): any {
        return posdep.decertificatedate.toString().slice(0, 10);
    }

    getElementReplacedbycivilDate(posdep: PosDep): any {
        return posdep.replacedbycivildate.toString().slice(0, 10);
    }


    handleCommand(command: string) {
        if (command.startsWith(this.removePositiondecree)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "removepositiondecree";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.removePosition)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "removeposition";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.renamePositiondecree)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "renamepositiondecree";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.renamePosition)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "renameposition";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.addNewPosition)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "addnewposition";
            this.positionManagingDepartment = command.split('_')[1];
        } else if (command.startsWith(this.removeDepartment)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "removedepartment";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.renameDepartment)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "renamedepartment";
            this.positionManagingId = command.split('_')[1];
        } else if (command.startsWith(this.decertificatePosition)) {
            this.decertificateID = Number.parseInt(command.split('_')[1]);
            let posdep: PosDep = this.posdeps.find(p => p.id == this.decertificateID);
            if (posdep.decertificate) {
                this.decertificateDate = this.toDateInputValue(posdep.decertificatedate);
                this.decertificateBool = true;
            } else {
                this.decertificateDate = this.toDateInputValue(new Date());
                this.decertificateBool = false;
            }
            this.modalDecertificatePanelVisible = true;
        }
    }

    handleExtended(command: string) {
        //alert(command + " oh");
        if (command.startsWith(this.addNewPosition)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "addnewposition";
            //alert(this.$store.state.positionsListId);
            this.positionManagingDepartment = this.$store.state.positionsListId; // Before we add subdepartments.
        } else if (command.startsWith(this.addNewDepartment)) {
            this.modalPositionManagingPanelVisible = true;
            this.positionManagingType = "addnewdepartment";
            this.positionManagingDepartment = this.$store.state.positionsListId;
        }
    }

    decertificateokbutton() {
        let decertificateNum: number = 0;
        if (this.decertificateBool) {
            decertificateNum = 1;
        }

        fetch('/api/Positiondecertificate', {
            method: 'post',
            body: JSON.stringify(<Positiondecertificate>{
                decertificate: decertificateNum, decertificatedate: new Date(this.decertificateDate), id: this.decertificateID
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {

            })

        this.modalDecertificatePanelVisible = false;
    }

    decertificatecancelbutton() {
        this.modalDecertificatePanelVisible = false;
    }

    shortifyRank(rankname: string): string {
        return rankname.split(' ').splice(-2, 2).join(" ");
        //TODO
    }

    compactView(){
        if (this.$store.state.positioncompact > 0) {
            return true;
        }
        return false;
    }

    //toDateInputValue(date: Date): string {
    //    return date.toString().slice(0, 10);
    //}

    toDateInputValue(date: Date): string {

        var local = new Date(date);
        local.setMinutes(local.getMinutes() - local.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

    loadAdditionaldata(posdep: PosDep) {
        this.flag_first_element = true;
        fetch('api/FullHistory/Position/' + posdep.id, { credentials: 'include' })
            .then(response => response.json() as Promise<Order[]>)
            .then(data => {
                this.order_list = data;
            }).then(x => {
                if (posdep.curator > 0 && posdep.curatorlist.length > 0) {
                    fetch('api/Positions/Curations' + posdep.curatorlist + "&" + posdep.id, { credentials: 'include' })
                        .then(response => response.json() as Promise<StructureTree[]>)
                        .then(data => {
                            this.curationstructuretrees = data;
                            this.curationstructuretrees.forEach(st => {
                                let splitted: string[] = st.tree.split(' — ');
                                st.name = splitted[splitted.length - 1];
                                //let removestring: string = ' — ' + st.name;
                                st.tree = st.tree.substring(0, st.tree.length - st.name.length);
                            })
                            //alert(JSON.stringify(posdep.curationstructuretrees));
                        });
                } else {
                    this.curationstructuretrees = new Array();
                }

                if (posdep.head > 0 && posdep.headid > 0) {
                    fetch('api/Positions/Heading' + posdep.headid + "&" + posdep.id, { credentials: 'include' })
                        .then(response => response.json() as Promise<StructureTree>)
                        .then(data => {
                            this.headingstructuretree = data;
                            //alert(JSON.stringify(data));
                            if (this.headingstructuretree != null && this.headingstructuretree.tree != null && this.headingstructuretree.tree.length > 0) {
                                let splitted: string[] = this.headingstructuretree.tree.split(' — ');
                                this.headingstructuretree.name = splitted[splitted.length - 1];
                                this.headingstructuretree.tree = this.headingstructuretree.tree.substring(0, this.headingstructuretree.tree.length - this.headingstructuretree.name.length);
                            }
                            //alert(JSON.stringify(posdep.curationstructuretrees));
                        });
                } else {

                }
            })
    }

    isDecreeAddSigned(posdep: PosDep): boolean {
        return this.isNotSignedAndWillBeCreated(posdep) || this.isNotSignedAndCreated(posdep);
    }

    isDecreeDeleteSigned(posdep: PosDep): boolean {
        return this.isDeletedUnsigned(posdep) || this.isWillBeDeletedUnsigned(posdep);
    }

    futureAdd(posdep: PosDep): boolean {
        //getDecreeStartDate(posdep)
        //this.$store.state.date;
        let startDate: Date = new Date(this.getDecreeStartDate(posdep));
        let currentDate: Date = new Date(this.$store.state.date);
        return startDate < currentDate;
    }

    futureDelete(posdep: PosDep): boolean {
        //getElementInactiveDate(posdep)
        //this.$store.state.date;

        return false;
    }

    printDate(date: Date): string {
        if (date == null) {
            return "";
        }
        return this.beautifyDate(this.toDateInputValue(date));
    }


    beautifyDate(date: string): string {
        if (date.length == 0) {
            return "";
        }
        let parts: string[] = date.split("-");
        if (parts.length != 3) {
            return "";
        }
        return parts[2] + "." + parts[1] + "." + parts[0];
    }

    isUnsigned(posdep: PosDep): boolean {
        return this.isDeletedUnsigned(posdep) || this.isNotSignedAndWillBeCreated(posdep) || this.isNotSignedAndCreated(posdep) || this.isWillBeDeletedUnsigned(posdep);
    }

    isUnsignedCreate(posdep: PosDep): boolean {
        return this.isNotSignedAndWillBeCreated(posdep) || this.isNotSignedAndCreated(posdep);
    }

    onPositionManagingPanelClose() {
        if (!this.$store.state.modeselectcuration && !this.$store.state.modeselectheading) {
            this.modalPositionManagingPanelVisible = false;
        }
    }

    openStructureInfo(event: any, structureInfo: StructureInfo) {
        if (event) event.preventDefault();

        let showSubs: boolean = false;
        if (this.$store.state.positionsListId == -structureInfo.id) {
            showSubs = true;
        }

        this.$store.commit("setSidebarParentOpen", this.$store.state.positionsListId);
        this.$store.commit("setForcePositionUpdate", true);
        this.$store.commit("setPositionsListId", -structureInfo.id);

        this.$store.commit("setPositionsListTitle", structureInfo.name);
        //this.$store.commit("setGrandparent", structure.grandparent);

        //if (showSubs && structure.hasChildren) {
        //    this.showSubordinates(structure.id);
        //} else {
        //}
        //alert('Послушай, скажи мне друг. Ты всюду был, ты знаешь всё на свете. Не то что я, гуляка человек.' );
    }

    /**
     * Если режим прохождения службы, возвращает true, если орг-штатная работа, то false
     */
    personsMode(): boolean {
        if (this.$store.state.mode == "0") {
            return false;
        } else {
            return true;
        }
    }

    getPerson(posdep: PosDep) {
        let person: Person = this.persons.find(d => d.position == posdep.id);
        return person;
        // if
        //return null;
    }

    appoint(position: PosDep) {
        let toggle: number = 1;
        let toggleReverse: number = 0;

        this.$store.commit("setEldVisible", toggle);
        let appearance = {
            positioncompact: 0,
            sidebardisplay: toggleReverse,
        }
        this.$store.commit("setPositionsListId", 0);
        this.$store.commit("setEldPosition", position.id);
        this.$store.commit("setEldStructure", position.structure);

        this.$store.commit("updateUserAppearance", appearance);
    }

    takeoff(person: Person) {
        let id: string = person.id.toString();
        fetch('api/Person/Takeoff' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(response => {
            })

    }

    hasPhotopreview(personid: number): boolean {
        if (this.photosPreview == null || this.photosPreview.length == 0) {
            return false;
        }
        if (this.photosPreview.find(p => p.person == personid)) {
            return true;
        }
        return false;
    }

    getPhotopreview(personid: number): Personphoto {
        return this.photosPreview.find(p => p.person == personid);
    }

    /**
     * После загрузки с базы данных нам нужно Даты превратить в string эквивалент
     */
    prepareToImport(person: Person): void {
        //person.birthdateString = this.toDateInputValue(new Date());
        person.birthdateString = this.toDateInputValue(person.birthdate);
        person.passportdatestartString = this.toDateInputValue(person.passportdatestart);
        person.passportdateendString = this.toDateInputValue(person.passportdateend);
        person.age = this.getAge(person.birthdateString).toString();
    }

    prepareToExport(person: Person): void {
        person.birthdate = new Date(person.birthdateString);
        person.passportdatestart = new Date(person.passportdatestartString);
        person.passportdateend = new Date(person.passportdateendString);
    }

    getAge(dateString) {
        var today = new Date();
        var birthDate = new Date(dateString);
        var age = today.getFullYear() - birthDate.getFullYear();
        var m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) {
            age--;
        }
        return age;
    }

    openProfile(person: Person) {
        if (person == null) {
            return;
        }
        let toggle: number = 1;
        let toggleReverse: number = 0;

        fetch('api/Identity/Fullmode2', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})

        this.$store.commit("setEldVisible", toggle);
        let appearance = {
            positioncompact: 0,
            sidebardisplay: toggleReverse,
        }

        this.$store.commit("setPositionsListId", 0);
        this.$store.commit("setEldSelectedperson", person.id);

        this.$store.commit("updateUserAppearance", appearance);
    }

    excelAddRemove() {

        let today: Date = new Date();
        var dd = today.getDate();
        var mm = today.getMonth() + 1; //January is 0!
        var yyyy = today.getFullYear();

        let ddString = dd.toString();
        if (dd < 10) {
            ddString = '0' + dd;
        }

        let mmString = mm.toString();
        if (mm < 10) {
            mmString = '0' + mm;
        }

        fetch('/api/Pmrequest', {
            method: 'post',
            body: JSON.stringify(<Pmrequest>{
                type: 6,
                id: this.structureid
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(x => x.blob())
            .then(x => {
                download(x, "Запрос_" + ddString + mmString + yyyy);
            })
    }

    selectPosdep(position: PosDep) {
        // Если назначаем на должность через ЭЛД
        if (this.$store.state.modeappointperson) {
            this.$store.commit("setModeappointedperson", position.id);
            this.$store.commit("setModeappointperson", false);

            let appearance = {
                positioncompact: this.$store.state.positioncompact,
                sidebardisplay: 0,
            }

            this.$store.commit("setEldVisible", 1);
            this.$store.commit("setPositionsListId", 0);

            this.$store.commit("updateUserAppearance", appearance);
        } else

        // Если назначаем на должность через проекты приказов ЭЛД
        if (this.$store.state.modeappointpersondecree) {
            if (this.$store.state.mailmodeprevios) {
                this.$store.commit("setdecreeoperationtemplatecreatorVisible", this.$store.state.persondecree != null ? true : false);
                this.$store.commit("setdecreeoperationelementVisible", true);
                this.$store.commit("setmailmodeprevios", false);
                this.$store.commit("setchosenPosition", position);
            } else {
                this.$store.commit("setModeappointedpersondecree", position.id);
            }

            this.$store.commit("setModeappointpersondecree", false);

            let appearance = {
                positioncompact: this.$store.state.positioncompact,
                sidebardisplay: 0,
            }

            //this.$store.commit("setModeappointpersondecreeenablepersondecree", true); // При помощи этого посылаем информацию topmenu о том, что необходимо вернуть отображение меню проекта приказов.
            //this.$store.commit("setEldVisible", 1); - поменять на 
            this.$store.commit("setPositionsListId", 0);

            this.$store.commit("updateUserAppearance", appearance);
        } else if (this.getPerson(position) != null) {
            //@click="openProfile(getPerson(posdep))"
            this.openProfile(this.getPerson(position));
        }
    }

    appointPersonToStructure() {
        // Если назначаем на должность через проекты приказов ЭЛД
        if (this.$store.state.modeappointpersonstructuredecree) {
            //alert(this.structureid); - подразделение со знаком минус
            let positiveid: number = -this.structureid;
            this.$store.commit("setModeappointedpersonstructuredecree", positiveid);
            this.$store.commit("setModeappointpersonstructuredecree", false);

            let appearance = {
                positioncompact: this.$store.state.positioncompact,
                sidebardisplay: 0,
            }

            this.$store.commit("setPositionsListId", 0);

            this.$store.commit("updateUserAppearance", appearance);
        }
    }


}
