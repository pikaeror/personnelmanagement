import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import Element from 'element-ui';
import { Tree, Dropdown, DropdownItem, DropdownMenu, Button, Dialog, Popover, Checkbox } from 'element-ui';
import Decreeoperationsrequest from '../../classes/decreeoperationsrequest'
import Decreeoperation from '../../classes/decreeoperation'
import Structureregion from '../../classes/structureregion'
import Structuretype from '../../classes/structuretype'
import StructureTree from '../../classes/structuretree';
import Staffmanagement from '../../classes/staffmanagement';
import download from 'downloadjs';
import Order from '../../classes/OrderHistrory/FullHistory';
import DecreeHistroryElementToAppending from '../../classes/OrderHistrory/DecreeHistroryElementToAppending';
Vue.component(Tree.name, Tree);
Vue.component(Dropdown.name, Dropdown);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.component(Dialog.name, Dialog);
Vue.component(Button.name, Button);
Vue.component(Popover.name, Popover);
Vue.component(Checkbox.name, Checkbox);
Vue.use(Element);


class Structure {
    id: number;
    realid: number; // To keep for decreeoperations requests
    name: string;
    parentstructure: string;
    hasChildren: boolean;
    childrenDisplayed: boolean;
    childrenNumber: number;
    level: number;
    order: number;
    levelchild: string; //sidebar-structure-0
    rank: number;
    structureregion: number;
    structureregionString: string;
    structuretype: number;
    structuretypeString: string;
    city: string;
    street: string;
    nameshortened: string;
    grandparent: string;
    priority: number;

    changeorigin: number;
    changestructurelast: number;
    changestructurerename: number;
    changestructureall: number;
    changestructurerank: number;
    changestructurelocation: number;
    changestructureparent: number;

}

const subordinateStr: string = "subordinate";
const fetchStructureDelay: number = 7000;


@Component({
    components: {
        Structuremanagingpanel: require('../structuremanagingpanel/structuremanagingpanel.vue.html')
    }
})
export default class SidebarComponent extends Vue {
    structures: Structure[];
    decreeoperations: Decreeoperation[];
    userStructure: string;
    firstFetch: boolean;
    level: number; // can be 1 or 2. 1 means highest level, has access to elder elements; 2 means see only children level.
    parentsWithDisplayedChildren: number[];
    masterpersonneleditorAccess: string;
    structureeditorAccess: string;
    personneleditorAccess: string;
    

    allStructuresShowAvailable: boolean;
    allStructuresShowed: boolean;
    showAllStructures: string;

    addNewStructureAvailable: boolean;
    addNewStructure: string;

    removeStructureAvailable: boolean;
    removeStructure: string;

    renameStructureAvailable: boolean;
    renameStructure: string;
    renameStructurenodecreeAvailable: boolean;
    renameStructurenodecree: string;

    removedecreeStructureAvailable: boolean;
    removedecreeStructure: string;

    renamedecreeStructureAvailable: boolean;
    renamedecreeStructure: string;

    copyStructureAvailable: boolean;
    copyStructure: string;

    pasteStructureAvailable: boolean;
    pasteStructure: string;

    printStaff: string;

    printStructurestaff: string;

    vizibleOrderHistory: boolean;
    printOrderHistory: string;
    decreeoperation: Decreeoperation[];
    orders_list: Order[];

    appendFlagDecree: string;
    vizibleAppendingDecree: boolean;
    newDecreeNumber: string;
    newDecreeDate: Date;
    newDecreeDateS: string;
    buttom_name: string;
    time_order_for_edit: Order;

    modalStructureManagingPanelVisible: boolean;
    structureManagingType: string;
    structureManagingParent: string;

    sidebarDisplay: boolean;

    detailedInfo: string;
    dropdownvisible: boolean; // To block updates
    reloaded: boolean;

    structurelist: number[];
    structureTrees: StructureTree[];
    structureselectionprocess: boolean;

    structureprintvisible: boolean;
    structureprintheading: boolean;
    structureprintid: number;

    fetchInProcess: boolean;
    fetchSkipCount: number;
    fetchSkipLimit: number;

    priorityAvailable: boolean;

    loadingStructureStaff: boolean;

    prioritychanged: boolean;

    post_counter_order_history: number;

    data() {
        return {
            structures: [],
            level: 1,
            parentsWithDisplayedChildren: [],
            userStructure: "0",
            firstFetch: true,
            masterpersonneleditorAccess: "0",
            structureeditorAccess: "0",
            personneleditorAccess: "0",

            allStructuresShowAvailable: false,
            allStructuresShowed: false,
            showAllStructures: "showallstructures",

            addNewStructureAvailable: false,
            addNewStructure: "addnewstructure",

            removeStructureAvailable: false,
            removeStructure: "removestructure",

            renameStructureAvailable: false,
            renameStructure: "renamestructure",
            renameStructurenodecreeAvailable: false,
            renameStructurenodecree: "renamestructurenodecree",

            removedecreeStructureAvailable: false,
            removedecreeStructure: "removedecreestructure",

            renamedecreeStructureAvailable: false,
            renamedecreeStructure: "renamedecreestructure",

            copyStructureAvailable: false,
            copyStructure: "copystructure",

            pasteStructureAvailable: false,
            pasteStructure: "pastestructure",

            printStaff: "printstaff",

            printStructurestaff: "printstructurestaff",

            vizibleOrderHistory: false,
            printOrderHistory: "printOrderHistory",
            decreeoperation: [],
            orders_list: [],

            appendFlagDecree: "appendDecree",
            vizibleAppendingDecree: false,
            newDecreeNumber: null,
            newDecreeDate: null,
            newDecreeDateS: "",
            buttom_name: "Добавить",
            time_order_for_edit: null,

            modalStructureManagingPanelVisible: false,
            structureManagingType: "",
            structureManagingParent: "",
            
            sidebarDisplay: true,

            detailedInfo: "",
            dropdownvisible: false,
            reloaded: false,

            structurelist: [],
            structureTrees: [],
            structureselectionprocess: false,

            structureprintvisible: false,
            structureprintheading: false,
            structureprintid: 0,

            fetchInProcess: false,
            fetchSkipCount: 0,
            fetchSkipLimit: 2,

            priorityAvalable: false,

            loadingStructureStaff: false,

            prioritychanged: false,
            post_counter_order_history: 0,
        }
    }

    mounted() {
        this.parentsWithDisplayedChildren = new Array();
        this.showAllStructuresCommand().then(x => {
            this.fetchStructures(true);
        });
        
        setInterval(this.fetchStructures, fetchStructureDelay); 
        setInterval(this.checkForcedStructureUpdate, 100); 
        
    }

    async getStructure() {
        await (<any>Vue).getAccessStatus().then(s => {
            this.userStructure = this.$store.state.userStructure;
            this.masterpersonneleditorAccess = this.$store.state.masterpersonneleditorAccess;
            this.structureeditorAccess = this.$store.state.structureeditorAccess;
            this.personneleditorAccess = this.$store.state.personneleditorAccess;

            if (!this.reloaded && this.$store.state.login != null && this.$store.state.login != "") {
                this.reloaded = true;
                //alert(this.$store.state.currentstructuretree);
                if (this.$store.state.currentstructuretree != null && this.$store.state.currentstructuretree.length > 0) {
                    let splitted: string[] = this.$store.state.currentstructuretree.split('f');
                    let firstPart: string = splitted[0];
                    let secondPart: string = splitted[1];
                    let structureid: number = Number.parseInt(splitted[2]);
                    let structurename: string = splitted[3];
                    
                    //this.$store.state.featured = Number.parseInt(secondPart);
                    let savedParentStructures: number[] = new Array();
                    firstPart.split('_').forEach(s => {
                        savedParentStructures.push(Number.parseInt(s));
                    })
                    this.$store.commit("setFeatured", Number.parseInt(secondPart));
                    this.$store.commit("setParentStructures", savedParentStructures);
                    if (structureid != 0) {
                        this.$store.commit("setPositionsListId", structureid);
                        this.$store.commit("setPositionsListTitle", structurename);
                    }
                }
                

                //this.$store.state.parentStructures
                //this.$store.state.featured;
            }
    })
    }

    get modeselectstructure(): boolean {
        return this.$store.state.modeselectstructure;
    }

    fetchStructures(fetchtops?: boolean, force?: boolean) {
        if (this.dropdownvisible) {
            return;
        }
        if (force == null) {
            force = false;
        }
        //alert(force);
        if (!force && this.prioritychanged) {
            this.prioritychanged = false;
            return;
        }
        this.prioritychanged = false;

        // Optimize later
        //if (this.fetchInProcess) {
        //    if (this.fetchSkipCount == this.fetchSkipLimit) {
        //        this.fetchSkipCount = 0;
        //        this.fetchInProcess = false;
        //    } else {
        //        this.fetchSkipCount += 1;
        //        return;
        //    }
        //}
        this.fetchInProcess = true; 
        //alert(this.parentsArrayToGet());
        this.getStructure().then(x =>
            fetch('api/DetailedStructure/' + this.parentsArrayToGet(), { credentials: 'include' })
                .then(response => response.json() as Promise<Structure[]>)
                .then(data => {
                    
                    if (this.masterpersonneleditorAccess == "1" || this.structureeditorAccess == "1") {
                        this.allStructuresShowAvailable = true;
                    } else {
                        this.allStructuresShowAvailable = false;
                    }

                    if (this.structureeditorAccess == "1" && this.$store.state.decree != null && this.$store.state.decree != 0) {
                        this.addNewStructureAvailable = true;
                        this.removeStructureAvailable = true;
                        this.priorityAvailable = true;
                        this.renameStructureAvailable = true;
                        this.removedecreeStructureAvailable = true;
                        this.renamedecreeStructureAvailable = true;
                        this.copyStructureAvailable = true;
                        this.pasteStructureAvailable = true;
                        this.renameStructurenodecreeAvailable = false;
                    } else {
                        this.addNewStructureAvailable = false;
                        this.removeStructureAvailable = false;
                        this.priorityAvailable = false;
                        this.renameStructureAvailable = false;
                        this.removedecreeStructureAvailable = false;
                        this.removedecreeStructureAvailable = false;
                        this.copyStructureAvailable = false;
                        this.pasteStructureAvailable = false;
                        this.renameStructurenodecreeAvailable = false;
                        if (this.structureeditorAccess == "1") {
                            this.renameStructurenodecreeAvailable = true; // без изменения приказа только
                        }
                    }

                    
                    data.forEach(p => {
                        
                        // Makes id of original
                        if (p.changeorigin == 0) {
                            p.realid = p.id;
                        } else {
                            p.realid = p.id;
                            p.id = p.changeorigin; // В основном айдишнике мы храним изначальное айди структуры. Чтобы все завязывалось на изначальном айди.
                        }


                        p.levelchild = "sidebar-structure-" + p.level;
                        if (p.childrenNumber > 0) {
                            p.hasChildren = true;
                        }
                        if (p.structureregion > 0) {
                            let structureregion: Structureregion = (<Structureregion[]>this.$store.state.structureregions).find(e => e.id == p.structureregion);
                            if (structureregion != null) {
                                p.structureregionString = structureregion.name;
                            } else {
                                p.structureregionString = "";
                            }

                        } else {
                            p.structureregionString = "";
                        }
                        //alert(JSON.stringify(this.$store.state.structuretypes));
                        if (p.structuretype > 0) {
                            let structuretype: Structuretype = (<Structuretype[]>this.$store.state.structuretypes).find(e => e.id == p.structuretype)
                            if (structuretype != null) {
                                p.structuretypeString = structuretype.name;
                            } else {
                                p.structuretypeString = "";
                            }
                        } else {
                            p.structuretypeString = "";
                        }
                    });
                    data = data.sort(this.compare);
                    //data.forEach(d => {
                    //    alert(d.name + " " + d.order);
                    //})
                    this.structures = data;
                    this.firstFetch = false;
                }).then(x => {
                    /**
                     * Get decree operations for structures
                     */
                    let operationsRequest: Decreeoperationsrequest[] = new Array<Decreeoperationsrequest>();
                    let currentDateStart: Date = new Date(this.$store.state.date);
                    this.structures.forEach(s => {
                        let decreeoperationsrequest: Decreeoperationsrequest = new Decreeoperationsrequest();
                        
                        if (s.id == s.changeorigin) {
                            decreeoperationsrequest.subjectidstructureupdate = -s.realid; // contains 
                        } else {
                            decreeoperationsrequest.subjectidstructureupdate = 0;
                        }

                        decreeoperationsrequest.subjectId = -s.id; // У подразделений subject имеет знак минуса
                        decreeoperationsrequest.requestedDate = currentDateStart;
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
                            // alert(response);
                            this.decreeoperations = response;
                            //alert(JSON.stringify(response));
                        });
                    
                    this.fetchInProcess = false;
                })

            
        )
        
    }

    parentsArrayToGet(): string {
        var parents: string = "";
        this.$store.state.parentStructures.forEach(x => {
            parents += x;
            parents += "_";
        }
        )
        if (parents.length > 1) {
            parents = parents.substring(0, parents.length - 1);
        }
        if (parents == "0") {
            parents = "";
        }
        parents += "f";
        parents += this.$store.state.featured;

        // - И зачем это надо?
        //parents += "f";
        //parents += this.$store.state.positionsListId;

        //parents += "f";
        //parents += this.$store.state.positionsListTitle;
        //alert(parents);
        return parents;
    }

    displaysChildren(structure: Structure): boolean {
        if (this.parentsWithDisplayedChildren.find(i => i == structure.id)) {
            return true;
        } else {
            return false;
        }
    }

    displayIt(): boolean {
        return true;
    }

    displayAsChild(structure: Structure): boolean {
        return true;
        //if (this.level == 1 && structure.parentstructure != null) {
        //    return true;
        //} else {
        //    return false;
        //}
    }

    checkForcedStructureUpdate() {
        if (Math.abs(this.$store.state.sidebarParentOpen) > 0) {
            this.showSubordinates(Math.abs(this.$store.state.sidebarParentOpen), true);
            this.$store.commit("setSidebarParentOpen", 0);
        }
        if ((<any>Vue).forceStructureUpdate) {
            this.fetchStructures(false, true);
            (<any>Vue).forceStructureUpdate = false;
        }
        this.clear_printing_history()
    }

    addNewSubstructureAvailable(structure: Structure): boolean {
        return true;
        //return (structure.parentstructure == null && this.addNewStructureAvailable);
    }

    subordinate(id): any {
        return subordinateStr + "_" + id;
    }

    addStructure(id): any {
        return this.addNewStructure + "_" + id;
    }

    renameStructureID(id): any {
        return this.renameStructure + "_" + id;
    }

    renameStructureIDnodecree(id): any {
        return this.renameStructurenodecree + "_" + id;
    }

    removeStructureID(id): any {
        return this.removeStructure + "_" + id;
    }

    renamedecreeStructureID(id): any {
        return this.renamedecreeStructure + "_" + id;
    }

    removedecreeStructureID(id): any {
        return this.removedecreeStructure + "_" + id;
    }

    copyStructureID(id): any {
        return this.copyStructure + "_" + id;
    }

    pasteStructureID(id): any {
        return this.pasteStructure + "_" + id;
    }

    printStaffID(id): any {
        return this.printStaff + "_" + id;
    }

    printStructurestaffID(id): any {
        return this.printStructurestaff + "_" + id;
    }

    printOrderHistoryID(id): any {
        return this.printOrderHistory + "_" + id;
    }

    isUserStructure(structure: Structure): any {
        return Number.parseInt(this.userStructure) == structure.id;
    }

    appendOldestDecree(id): any {
        return this.appendFlagDecree + "_" + id;
    }

    // 0 - no purpose, 1 - no purpose not signed,
    // 2 - will create subject in future, 3 - will create subject in future not signed,
    // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
    // 7 - will delete subject in future not signed, 8 - already renamed subject, 9 - already renamed subject 
    // not signed, 10 - will rename subject, 11 - will rename subject not signed,
    // 12 - deleted, 13 - deleted not signed,
    //14 - renamed not signed, 15 - will be renamed, 16 - will be renamed not signed
    // structureid - отрицательна, так как символизирует, что это ID подразделения
    isSignedAndCreated(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 4).length > 0;
        } else {
            return false;
        }
        
    }

    isNotSignedAndCreated(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 5).length > 0;
        } else {
            return false;
        }
    }

    isNotSignedAndCreatedForEdition(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.realid && d.metaPurposeForSubject == 5).length > 0;
        } else {
            return false;
        }
    }

    isSignedAndWillBeCreated(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 2).length > 0;
        } else {
            return false;
        }
    }

    signedAndWillBeCreatedTime(structure: Structure): any {
        if (this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 2).length > 0) {
            return this.decreeoperations.find(d => d.subject == -structure.id && d.metaPurposeForSubject == 2).metaDateActive.toString().slice(0, 10);
        }
    }

    isNotSignedAndWillBeCreated(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 3).length > 0;
        } else {
            return false;
        }
        
    }

    isNotSignedAndWillBeCreatedForEdition(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.realid && d.metaPurposeForSubject == 3).length > 0;
        } else {
            return false;
        }

    }

    notSignedAndWillBeCreatedTime(structure: Structure): any {
        if (this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 3).length > 0) {
            return this.decreeoperations.find(d => d.subject == -structure.id && d.metaPurposeForSubject == 3).metaDateActive.toString().slice(0, 10);
        }
    }

    isDeletedSigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 12).length > 0;
        } else {
            return false;
        }
        
    }

    isDeletedUnsigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 13).length > 0;
        } else {
            return false;
        }
        
    }

    isWillBeDeletedSigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 6).length > 0;
        } else {
            return false;
        }
    }

    willBeDeletedSignedTime(structure: Structure): any {
        if (this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 6).length > 0) {
            return this.decreeoperations.find(d => d.subject == -structure.id && d.metaPurposeForSubject == 6).metaDateActive.toString().slice(0, 10);
        }
    }

    isWillBeDeletedUnsigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 7).length > 0;
        } else {
            return false;
        }
    }

    willBeDeletedUnsignedTime(structure: Structure): any {
        if (this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 7).length > 0) {
            return this.decreeoperations.find(d => d.subject == -structure.id && d.metaPurposeForSubject == 7).metaDateActive.toString().slice(0, 10);
        }
    }

    isChangedUnsigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 14).length > 0;
        } else {
            return false;
        }

    }

    isWillBeChangedUnsigned(structure: Structure): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == -structure.id && d.metaPurposeForSubject == 16).length > 0;
        } else {
            return false;
        }

    }

    isUnsigned(structure: Structure): boolean {
        return (this.isNotSignedAndCreated(structure) || this.isNotSignedAndWillBeCreated(structure) || this.isWillBeDeletedUnsigned(structure) || this.isDeletedUnsigned(structure) || this.isChangedUnsigned(structure));
    }

    isNotCreatedYet(structure: Structure): boolean {
        //COMEHERE
        
        return (this.isNotSignedAndCreatedForEdition(structure) || this.isNotSignedAndWillBeCreatedForEdition(structure));
    }

    isChanged(structure: Structure): boolean {
        return (this.isChangedUnsigned(structure) || this.isWillBeChangedUnsigned(structure));
    }

    getDecreeName(structure: Structure): any {
        if (this.decreeoperations != null) {
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 4).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 4).metaDecreeName;
            }
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 2).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 2).metaDecreeName;
            }
        }
        
        return "";
    }

    getDecreeStartDate(structure: Structure): any {
        if (this.decreeoperations != null) {
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 4).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 4).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 2).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 2).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 5).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 5).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == structure.id && d.metaPurposeForSubject == 3).length > 0) {
                return this.decreeoperations.find(d => d.subject == structure.id && d.metaPurposeForSubject == 3).metaDateActive.toString().slice(0, 10);
            }
        }
        
        return "";
    }



    handleCommand(command: string) {
        if (command.startsWith(subordinateStr)) {
            let id: number = Number.parseInt(command.split('_')[1]);
            this.showSubordinates(id);
        } else if (command.startsWith(this.addNewStructure)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "addnewstructure";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.removedecreeStructure)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "removestructuredecree";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.removeStructure)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "removestructure";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.renameStructurenodecree)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "renamestructurenodecree";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.renamedecreeStructure)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "renamestructuredecree";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.renameStructure)) {
            this.modalStructureManagingPanelVisible = true;
            this.structureManagingType = "renamestructure";
            this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.copyStructure)) {
            this.$store.commit("setModecopy", true);
            this.$store.commit("setModecopystring", "s=" + command.split('_')[1]);
            //alert(command.split('_')[1]);
        } else if (command.startsWith(this.pasteStructure)) {
            fetch('api/DetailedStructure/Paste' + this.$store.state.modecopystring + "&" + command.split('_')[1], { credentials: 'include' })
                .then(data => {
                    //alert(this.$store.state.modecopystring + "&" + command.split('_')[1]);
                });
        } else if (command.startsWith(this.printStaff)) {
            let structure: Structure = this.structures.find(s => s.id == Number.parseInt(command.split('_')[1]));
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

            fetch('/api/PrintStaff', {
                method: 'post',
                body: JSON.stringify(<Staffmanagement>{
                    id: structure.id,
                    realid: structure.realid,
                    type: 0,

                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => x.blob())
                .then(x => {
                    let length: number = 40;
                    let structurename: string = structure.name.length > length ? structure.name.substring(0, length) : structure.name;
                    download(x, structurename + "_" + ddString + mmString + yyyy)
                }
                    )
            //.then(x => download(x, "Штат"))
            //structure.name + "_" + ddString + mmString + yyyy
        } else if (command.startsWith(this.printStructurestaff)) {
            this.structureprintvisible = true;
            this.structureprintid = Number.parseInt(command.split('_')[1]);
            //alert('printstructurestaff');   
        } else if (command.startsWith(this.printOrderHistory)) {
            this.vizibleOrderHistory = true;
            this.structureprintid = Number.parseInt(command.split('_')[1]);
            this.visualPrintOrderHistory();
        } else if (command.startsWith(this.appendFlagDecree)) {
            this.structureprintid = Number.parseInt(command.split('_')[1]);
            this.showdecrrewhithappending()
            //this.
        }
    }

    visualPrintOrderHistory() {
        this.vizibleAppendingDecree = false;
        this.newDecreeNumber = null;
        this.newDecreeDate = null;
        this.newDecreeDateS = "";
        fetch('api/FullHistory/Structure/' + this.structureprintid, { credentials: 'include' })
            .then(response => response.json() as Promise<Order[]>)
            .then(data => {
                this.orders_list = data;
            });
    }

    showdecrrewhithappending() {
        this.vizibleOrderHistory = true;
        this.vizibleAppendingDecree = true;
        this.visualPrintOrderHistory();
    }

    okPrintstructurestaff() {
        let structure: Structure = this.structures.find(s => s.id == this.structureprintid);
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

        let headval: number = 0;
        if (this.structureprintheading) {
            headval = 1;
        }

        this.loadingStructureStaff = true;
        fetch('/api/PrintStructurestaff', {
            method: 'post',
            body: JSON.stringify(<Staffmanagement>{
                id: structure.id,
                realid: structure.realid,
                type: 0,
                head: headval,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => x.blob())
            .then(x => {
                let length: number = 40;
                let structurename: string = structure.name.length > length ? structure.name.substring(0, length) :structure.name;
                download(x, structurename + "_" + ddString + mmString + yyyy)
                this.loadingStructureStaff = false;
            }) 
    }

    appendHistoryDecree(dates: string, number: string) {
        if (this.buttom_name == "Применить") {
            this.edit_item_from_orderlist(this.time_order_for_edit, new Date(Date.parse(dates)), number);
            this.vizibleAppendingDecree = false;
            this.vizibleOrderHistory = false;
            this.vizibleOrderHistory = true;
            this.buttom_name = "Добавить";
            number = null;
            return;

        }
        this.buttom_name = "Добавить";
        let date = new Date(Date.parse(dates));
        this.newDecreeDate = date;
        if (!this.vizibleAppendingDecree) {
            this.vizibleAppendingDecree = true;
            return;
        }
        let id: number = this.structureprintid;
        fetch('api/FullHistory/Structure/AddDecree', {
            method: 'post',
            body: JSON.stringify(<DecreeHistroryElementToAppending>{
                structure_id: id,
                number: number,
                date: date,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })})
            .then(response => response.json() as Promise<Order[]>)
            .then(data => {
            this.orders_list = [];
            this.orders_list = data;
        });
        this.vizibleAppendingDecree = false;
        this.vizibleOrderHistory = false;
        this.vizibleOrderHistory = true;
        date = null;
        number = null;
        return;
    }

    remove_item_from_orderlist(order: Order) {
        fetch('api/FullHistory/Structure/RemoveDecree/' + this.structureprintid, {
            method: 'post',
            body: JSON.stringify(order),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => response.json() as Promise<Order[]>)
            .then(data => {
                this.orders_list = [];
                this.orders_list = data;
            });
        this.vizibleAppendingDecree = false;
        this.vizibleOrderHistory = false;
        this.vizibleOrderHistory = true;
        return;
    }

    pre_edit_item_from_orderlist(order: Order) {
        this.vizibleAppendingDecree = true;
        this.newDecreeDateS = order.decree.dateactive.toString().split('T')[0];
        this.newDecreeNumber = order.decree.number;
        this.buttom_name = "Применить";
        this.time_order_for_edit = order;
    }

    edit_item_from_orderlist(order_old: Order, new_date: Date, new_number: string) {
        class outputeditor {
            order: Order;
            new_order: DecreeHistroryElementToAppending;
        };

        let id: number = this.structureprintid;
        let time: DecreeHistroryElementToAppending = <DecreeHistroryElementToAppending>{
            structure_id: id,
            number: new_number,
            date: new_date,
        };

        fetch('api/FullHistory/Structure/EditDecree', {
            method: 'post',
            body: JSON.stringify(<outputeditor>{
                order: order_old,
                new_order: time,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => response.json() as Promise<Order[]>)
            .then(data => {
                this.orders_list = [];
                this.orders_list = data;
            });
    }

    /**
     * Show or hide subordinate structures of structure
     * @param id
     */
    showSubordinates(id: number, onlyOpen?: boolean) {
        if (onlyOpen == null) {
            onlyOpen = false;
        }
        let topStructure: boolean = false;
        /**
         * Top structure
         */
        let structure: Structure = this.structures.find(s => s.id == id || s.id == -id);
        if (structure != null && structure.parentstructure == null) {
            topStructure = true;
        }

        if (this.$store.state.parentStructures.find(i => i == id)) {
            if (!onlyOpen) {
                this.$store.state.parentStructures = this.$store.state.parentStructures.filter(p => p != id);
                /**
                 * We push top structure with "-" mark to show it without parents.
                 */
                if (topStructure && id > 0) {

                    this.$store.state.parentStructures.push(-id);
                }
            }
            
        } else {
            this.$store.state.parentStructures.push(id);
        }
        (<any>Vue).forceStructureUpdate = true;
    }

    showSubordinatesTop(ids: number[]) {
        this.$store.state.parentStructures = [];
        ids.forEach(id => {
            let topStructure: boolean = true; // assume it is top.
            if (this.$store.state.parentStructures.find(i => i == id)) {
                this.$store.state.parentStructures = this.$store.state.parentStructures.filter(p => p != id);
                /**
                 * We push top structure with "-" mark to show it without parents.
                 */
                if (topStructure && id > 0) {
                    this.$store.state.parentStructures.push(-id);
                }
            } else {
                this.$store.state.parentStructures.push(id);
            }
        });
        
        (<any>Vue).forceStructureUpdate = true;
    }

    handleExtented(command: string) {
        if (command == this.showAllStructures) {
            this.showAllStructuresCommand();
        } else if (command == this.addNewStructure) {
            this.addNewStructureCommand();
        }
    }

    async showAllStructuresCommand() {
        this.$store.commit("setFeatured", 0);
        await fetch('api/DetailedStructure/Top', { credentials: 'include' })
            .then(response => response.json() as Promise<number[]>)
            .then(data => {
                
                let negative: number[] = new Array();
                data.forEach(d => {
                    //alert(d);
                    negative.push(-d);
                });
                this.showSubordinatesTop(negative);
            });
    }

    addNewStructureCommand() {
        this.modalStructureManagingPanelVisible = true;
        this.structureManagingType = "addnewstructure";
        this.structureManagingParent = null;
        //alert(command);
    }

    openStructure(event: any, structure: Structure) {
        if (event) event.preventDefault();

        let showSubs: boolean = false;
        if (structure.changeorigin == 0) {
            if (this.$store.state.positionsListId == -structure.id) {
                showSubs = true;
            }
        } else {
            if (this.$store.state.positionsListId == -structure.changeorigin) {
                showSubs = true;
            }
        }

        if (this.$store.state.modeselectcuration) {
            this.$store.commit("setModeselectedcuration", structure.id); 
            this.$store.commit("setModeselectcuration", false); 
            return;
        }
        if (this.$store.state.modeselectheading) {
            this.$store.commit("setModeselectedheading", structure.id);
            this.$store.commit("setModeselectheading", false);
            return;
        }
        if (this.$store.state.modeselectstructure) {
            this.$store.commit("setModeselectedstructure", structure.id);
            this.$store.commit("setModeselectstructure", false);
            return;
        }
        //alert(id);
        //this.$store.commit("setDepartmentsListId", id);
        //this.$store.commit("setDepartmentsListTitle", name);
        //this.$store.commit("setForceDepartmentUpdate", true);
        this.$store.commit("setForcePositionUpdate", true);
        if (structure.changeorigin == 0) {
            this.$store.commit("setPositionsListId", -structure.id);
        } else {
            this.$store.commit("setPositionsListId", -structure.changeorigin);
        }
        
        this.$store.commit("setPositionsListTitle", structure.name);
        this.$store.commit("setGrandparent", structure.grandparent);

        if (showSubs && structure.hasChildren) {
            this.showSubordinates(structure.id);
        } else {
        }
        //alert('Послушай, скажи мне друг. Ты всюду был, ты знаешь всё на свете. Не то что я, гуляка человек.' );
    }

    checkSidebar() {
        this.sidebarDisplay = (<any>Vue).sidebar;
    }

    get sidebar() {
        return this.sidebarDisplay;
        //return (<any>Vue).sidebar;
    }

    get departmentsListId() {
        return this.$store.state.departmentsListId;
    }

    set departmentsListId(value) {
        
        this.$store.commit("setDepartmentsListId", value);
    }

    compare(a: Structure, b: Structure) {
        if (a.order < b.order) {
            return -1;
        }
        if (a.order > b.order) {
            return 1;
        }
        // a must be equal to b
        return 0;
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

    onVisibleChange(change: boolean) {
        this.dropdownvisible = change;
    }

    isSelected(structureid: number): boolean {
        return (structureid == this.$store.state.positionsListId || structureid == -this.$store.state.positionsListId);
    }

    onStructureManagingPanelClose() {
        if (!this.$store.state.modeselectstructure) {

            this.modalStructureManagingPanelVisible = false;
        }
    }

    /**
     * Visible if button is pressed and mode is not enabled; 
     */
    get structureManagingPanelVisible(): boolean {
        return this.modalStructureManagingPanelVisible && !this.modeselectstructure;
    }

    priorityUp(structure: Structure) {
        fetch('api/DetailedStructure/Up' + structure.realid, { credentials: 'include' }).then().then(x => (<any>Vue).forceStructureUpdate = true);
    }

    priorityDown(structure: Structure) {
        fetch('api/DetailedStructure/Down' + structure.realid, { credentials: 'include' }).then().then(x => { (<any>Vue).forceStructureUpdate = true; });
    }

    priorityChange(structure: Structure) {
        fetch('api/DetailedStructure/Prioritychange' + structure.realid + ";" + structure.priority, { credentials: 'include' }).then().then(x => { (<any>Vue).forceStructureUpdate = true; });
    }

    priorityChangeValue() {
        this.prioritychanged = true;
    }

    priorityChangefocus() {
        alert('focues!');
    }

    toDateInputValue(date: Date): string {

        var local = new Date(date);
        local.setMinutes(local.getMinutes() - local.getTimezoneOffset());
        let output = ""
        let k = local.toJSON().slice(0, 10).split('-');
        let y = k.reverse();
        return y.join('.');
    }

    orderType(create_order: number, update_order: number, delete_order: number): string {
        if (create_order > 0) {
            return 'Введен';
        } else {
            if (update_order > 0) {
                return 'Изменен';
            } else {
                if (delete_order > 0) {
                    return 'Упразднен';
                }
            }
        }
    }

    clear_printing_history() {
        if(!this.vizibleOrderHistory) {
            this.decreeoperation = [];
            this.orders_list = [];
            //this.reloaded = true;
        }
    }
}