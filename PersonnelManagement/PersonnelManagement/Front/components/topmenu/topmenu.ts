import Vue from 'vue';

import { Component, Prop, Watch} from 'vue-property-decorator';
import Element, { Switch } from 'element-ui';
import { Button, Select, Input, Dialog, Dropdown, DropdownItem, DropdownMenu, Checkbox, CheckboxGroup, Autocomplete } from 'element-ui';
import Decreemanagement from '../../classes/decreemanagement'
import Decreeoperation from '../../classes/decreeoperation'
import { FeaturedStructure, excerptStructures } from '../../classes/persondecreeoperation'
import Region from '../../classes/region'
import Area from '../../classes/area'
import Structure from '../../classes/structure'
import Rewardmoney from '../../classes/rewardmoney'
import download from 'downloadjs';

import Structureregion from '../../classes/structureregion';
import Structuretype from '../../classes/structuretype';
import StructureTree from '../../classes/structuretree';
import Rank from '../../classes/rank';
import Country from '../../classes/country';
import User from '../../classes/user';
import moment from 'moment';
import Changedocumentstype from '../../classes/changedocumentstype';
import Subject from '../../classes/subject';
import Countycities from '../../classes/countrycities'
import Countrycities from '../../classes/countrycities';
import Ordernumbertype from '../../classes/ordernumbertype';
import Link from '../../classes/link';
import Dismissalclauses from '../../classes/dismissalclauses';
import formatter from '../../classes/formating_functions/el_table_formatter';

import Decree from '../../classes/decree/decree';
import DecreeFinder from '../../classes/decree/decree_finder';
import decree_operation from '../../classes/decree/decree_operation';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Select.name, Select);
Vue.component(Dialog.name, Dialog);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(Switch.name, Switch);
Vue.component(Dropdown.name, Dropdown);
Vue.component(Autocomplete.name, Autocomplete);
Vue.use(Element);

const EDIT_LABEL = "Редактировать";
const SAVE_LABEL = "Сохранить";

/*class FeaturedStructure {
    name: string;
    id: string;
}

class excerptStructures {
    id: number;
    structures: FeaturedStructure[];
}*/

class StructureManagement {
    id: number;
    type: string;
    name: string;
    name1: string;
    name2: string;
    name3: string;
    nameshortened: string;
    featuredStr: boolean;
    parent: number;
    parentstructure: number;
    datecustom: number;
    dateactive: Date;
    rank: number;
    structureregion: number;
    structuretype: number;
    structuretypesiblings: number;
    city: string;
    street: string;
    nodecree: number;
    separatestructure: number;
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
    filteredSubjects: Subject[];
    subjectnumber: number;
    subjectnotice: string;
    subjectgender: number;
}

@Component({
    components: {
        AdminPanel: require('../adminpanel/adminpanel.vue.html'),
        Structuremanagingpanel: require('../structuremanagingpanel/structuremanagingpanel.vue.html'),
        Settingspanel: require('../settingspanel/settingspanel.vue.html'),
        Pmrequestpanel: require('../pmrequestpanel/pmrequestpanel.vue.html')
    }
})
export default class TopmenuComponent extends Vue {
    formatter = formatter;

    current_decree: Decree;
    all_decrees: Decree[];

    decree_find_parameters: DecreeFinder;

    user: User;

    decree_operations: decree_operation[];

    id: number;
    name: string;
    name1: string;
    name2: string;
    name3: string;
    nameshortened: string;
    featuredStr: boolean;
    parentstructure: number;
    datecustom: number;
    dateactive: Date;
    rank: number;
    structureregion: number;
    structuretype: number;
    structuretypesiblings: number;
    city: string;
    street: string;
    nodecree: number;
    separatestructure: number;
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
    filteredSubjects: Subject[];
    subjectnumber: number;
    subjectnotice: string;
    subjectgender: number;

    @Prop({ default: "" })
    type: string;

    @Prop({ default: 0 })
    parent: number;

    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: false })
    visiblevar: boolean;

    modalAdminVisible: boolean;
    sidebarDisplay: boolean;
    modalStructureManagingPanelTopMenuVisible: boolean;
    modalSettingsPanelVisible: boolean;
    modalPmrequestPanelVisible: boolean;
    modalAboutPanelVisible: boolean;
    featured: FeaturedStructure[];
    ttt: excerptStructures[];
    addStructureAvailable: boolean;

    num: number;
    structureeditorAccess: string;
    removeStructureAvailable: boolean;
    removeStructure: string;

    renameStructureAvailable: boolean;
    renameStructure: string;

    modalDecreesMenuVisible: boolean;

    modalDecreeMenuVisible: boolean;
    decreeId: number;
    decreeButtonName: string;
    decreeNumber: string;
    decreeNickname: string;
    decreeName: string;
    decreeDateactive: string;
    decreeDatesigned: string;

    decreesList: Decreemanagement[];
    decreeCreateName: string;
    decreeOperations: Decreeoperation[];

    focused: boolean;

    decreesActionsDisabled: boolean;
    modalDecreesSignedMenuVisible: boolean;
    decreeFilterNumber: string;
    decreeFilterNickname: string;
    decreeFilterName: string;
    decreeFilterDateactiveStart: string;
    decreeFilterDateactiveEnd: string;
    decreeFilterDatesignedStart: string;
    decreeFilterDatesignedEnd: string;

    decreesSignedList: Decreemanagement[];
    decreeSignedOperations: Decreeoperation[];
    modalDecreeMenuSignedVisible: boolean;

    numberNewStructure: number;
    numberStructure: number;

    specialityName: string;
    facultyName: string;
    courseName: string;
    newCourseName: string;
    structureName: string;
    structureNewName: string;

    structuresReward: Structure[];
    structuresRewardAllowedToSelect: Structure[];
    structuresElders: Structure[];
    persondecreeblocksubsMass: number[];

    personvacationHolidays: number;
    //rewardmoneys: Rewardmoney[];
    customwidth: boolean;
    excertmode: boolean;
    excertstructlist: FeaturedStructure[];

    
    onDecreeDatesignedChange(value: string, oldValue: string) {
        this.decreeDateactive = this.decreeDatesigned;
    }

    data() {
        return {
            current_decree: new Decree(),
            all_decrees: [],

            decree_find_parameters: new DecreeFinder(),

            user: new User(),

            decree_operations: [],

            id: 0,
            status: "renamestructure",
            name: "",
            name1: "",
            name2: "",
            name3: "",
            nameshortened: "",
            featuredStr: false,
            datecustom: false,
            dateactive: this.toDateInputValue(new Date()),
            rank: null,
            structureregion: null,
            structureregions: [],
            structuretype: null,
            structuretypes: [],
            structuretypesiblings: false,
            city: "",
            street: "",
            nodecree: false,
            separatestructure: false,

            structurelist: 0,
            structureTree: null,
            structureselectionprocess: false,

            subject1: null,
            subject2: null,
            subject3: null,
            subject4: null,
            subject5: null,
            subject6: null,
            subject7: null,
            subject8: null,
            subject9: null,
            subject10: null,
            subject11: null,
            subject12: null,
            subject13: null,
            subject14: null,
            subject15: null,
            filteredSubjects: [],
            subjectnumber: null,
            subjectnotice: "",
            subjectgender: null,

            modalAdminVisible: false,
            modalStructureManagingPanelTopMenuVisible: false,
            modalSettingsPanelVisible: false,
            modalPmrequestPanelVisible: false,
            modalAboutPanelVisible: false,
            sidebarDisplay: true,
            featured: [],
            ttt: [],
            structureeditorAccess: "0",

            removeStructureAvailable: false,
            removeStructure: "removestructure",

            renameStructureAvailable: false,
            renameStructure: "renamestructure",

            specialityName: "",
            facultyName: "",
            newCourseName: "",
            courseName: "",
            structureNewName: "",
            structureName: "",
            modalDecreesMenuVisible: false,

            modalDecreeMenuVisible: false,
            decreeButtonName: "Создать приказ",

            decreeId: 0,
            decreeNumber: "",
            decreeNickname: "",
            decreeName: "",
            decreeDateactive: this.toDateInputValue(new Date()),
            decreeDatesigned: this.toDateInputValue(new Date()),

            addStructureAvailable: false,
            decreesList: [],
            decreeCreateName: "",
            decreeOperations: [],
            focused: false,

            decreesActionsDisabled: false,

            num: 0,
            modalDecreesSignedMenuVisible: false,
            decreeFilterNumber: "",
            decreeFilterNickname: "",
            decreeFilterName: "",
            decreeFilterDateactiveStart: "",
            decreeFilterDateactiveEnd: "",
            decreeFilterDatesignedStart: "",
            decreeFilterDatesignedEnd: "",
            candidateSearch: [],
            decreesSignedList: [],
            decreeSignedOperations: [],
            modalDecreeMenuSignedVisible: false,

            modalPersondecreesMenuVisible: false,
            modalPersondecreeMenuVisible: false,

            structuresReward: [],
            structuresRewardAllowedToSelect: [],
            structuresElders: [],

            customwidth: false,
            excertmode: false,
            excertstructlist: [],
        }
    }

    mounted() {
        this.userObject();
        setInterval(this.checkSidebarAndAccessAndDecreeName, 250);
        setInterval(this.renewDecrees, 1000);
        this.fetchFeaturedStructures();
        this.fetchStructureRewards();
        this.fetchStructureRewardsAllowed();
    }

    userObject() {
        fetch('api/Identity/User', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<User>;
            })
            .then(result => {
                this.user = result;
                //alert(JSON.stringify(result));
            });
    }

    get modeselectstructure(): boolean {
        return this.$store.state.modeselectstructure;
    }

    get modeselectheading(): boolean {
        return this.$store.state.modeselectheading;
    }

    get ranks(): Rank[] {
        return this.$store.state.ranks;
    }

    get dismissalclauses(): Dismissalclauses[] {
        return this.$store.state.dismissalclauses;
    }

    get countries(): Country[] {
        return this.$store.state.countries;
    }

    get regions(): Region[] {
        return this.$store.state.regions;
    }

    get areas(): Area[] {
        return this.$store.state.areas;
    }

    get changedocumentstypes(): Changedocumentstype[] {
        return this.$store.state.changedocumentstypes;
    }

    get rewardmoneys(): Rewardmoney[] {
        return this.$store.state.rewardmoneys;
    }

    get subjects(): Subject[] {
        return this.$store.state.subjects;
    }

    get ordernumbertypes(): Ordernumbertype[] {
        return this.$store.state.ordernumbertypes;
    }

    get structuresalldocument(): string[] {
        return this.$store.state.structuresalldocument;
    }

    /**
     * Visible if button is pressed and mode is not enabled; 
     */
    get pmrequestManagingPanelVisible(): boolean {
        return this.modalPmrequestPanelVisible && !this.modeselectstructure;
    }

    isNum(str: string) {
        return str.length === 1 && str.match(/[0-9]/i);
    }

    isLetter(str: string) {
        return str.length === 1 && str.match(/[a-zA-Z]/i);
    }

    isLetterCyrillic(str: string) {
        return str.length === 1 && str.match(/[а-яА-Я]/i);
    }

    logout() {
        (<any>Vue).logout();
    }

    togglesidebar() {
        (<any>Vue).sidebar = !(<any>Vue).sidebar;
    }

    checkSidebarAndAccessAndDecreeName() {
        //alert((<any>Vue).sidebar);
        this.sidebarDisplay = (<any>Vue).sidebar;
        this.structureeditorAccess = this.$store.state.structureeditorAccess;
        //alert(this.structureeditorAccess + "   " + this.$store.state.structureeditorAccess);
        if (this.structureeditorAccess == "1" && this.$store.state.decree != null && this.$store.state.decree != 0) {
            this.removeStructureAvailable = true;
            this.renameStructureAvailable = true;
            this.addStructureAvailable = true;
        } else {
            //alert(":(");
            this.removeStructureAvailable = false;
            this.renameStructureAvailable = false;
            this.addStructureAvailable = false;
        }

        if (this.$store.state.decree == null || this.$store.state.decree == 0) {
            this.decreeButtonName = "Создать приказ";
        } else {
            this.decreeButtonName = "Параметры приказа";
        }
    }

    decrees() {
        this.modalDecreesMenuVisible = true;
        this.fetchDecreesActive();
        
    }

    disableDecrees() {
        this.decreesActionsDisabled = true;
        setTimeout(() => this.decreesActionsDisabled = false, 1500);
    }

    renewDecrees() {
        if (this.modalDecreesMenuVisible) {
            this.fetchDecreesActive();
        }
    }


    decreeCreate() {
        this.disableDecrees();
        fetch('/api/Decrees', {
            method: 'post',
            body: JSON.stringify(<Decreemanagement>{
                decreemanagementstatus: 1,
                nickname: this.decreeCreateName,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {  })
    }

    decreeSelect(event: any, id: number) {
        if (this.$store.state.decree == null || this.$store.state.decree != id) {
            //alert(id);
            //this.$store.commit("setDecree", id);
            fetch('api/Decrees/SelectActive' + id, { credentials: 'include' });
        // Open decree
        } else {
            this.selectCurrentDecree();
        }
    }

    selectSignedDecree(event: any, decree: Decree) {
        this.modalDecreeMenuSignedVisible = true;
        this.fetchDecreeOperationsSigned(decree);
        //this.fetchDecreeSigned(id);
    }

    selectCurrentDecree() {
        if (this.$store.state.decree != 0) {
            this.modalDecreeMenuVisible = true;
            this.fetchDecreeOperations();
            this.fetchDecree();
        }
        
    }


    // obsolete
    decree() {
        if (this.$store.state.decree == null || this.$store.state.decree == 0) {
            //this.$store.commit("setDecree", 1);
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
        } else {
            //this.$store.commit("setDecree", 0);
            this.modalDecreeMenuVisible = true;
            this.fetchDecreeOperations();
        }
    }

    filterSignedDecree() {
        console.log(JSON.stringify(<DecreeFinder>(this.decree_find_parameters)));
        fetch('/api/Decrees/Finder', {
            method: 'post',
            body: JSON.stringify(<DecreeFinder>(this.decree_find_parameters)),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => {
            return response.json() as Promise<Decree[]>;
        }).then(date => {
            this.all_decrees = date;
        });
    }

    decreeDeny() {
        if (!confirm("Вы уверены?")) {
            return;
        }
        if (this.$store.state.decree != null && this.$store.state.decree != 0) {
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 2,
                    id: this.$store.state.decree,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => {this.fetchDecreesActive()})
        }
        this.modalDecreeMenuVisible = false;
    }

    decreePrint() {
        fetch('/api/ChangesByDecreeOutputWord/Decree/ListOfChanges', {
            method: 'post',
            body: JSON.stringify(this.decreeOperations),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => x.blob())
            .then(x => download(x, this.decreeName))

/*        fetch('/api/Decrees', {
            method: 'post',
            body: JSON.stringify(<Decreemanagement>{
                decreemanagementstatus: 4, // 4 - print decree.
                id: this.$store.state.decree,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => x.blob())
            .then(x => download(x, this.decreeName))*/
        //this.modalDecreeMenuVisible = false;
    }

    decreehistoryPrint() {
        fetch('/api/ChangesByDecreeOutputWord/Decree/ListOfChanges/' + this.decreeId.toString(), {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => x.blob())
            .then(x => download(x, this.decreeName))
    }

    decreeAccept() {
        let csharpDateActive: Date = new Date(this.decreeDateactive);
        let csharpDateSigned: Date = new Date(this.decreeDatesigned); 

        if (this.$store.state.decree != null && this.$store.state.decree != 0) {
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 3,
                    id: this.$store.state.decree,
                    name: this.decreeName,
                    number: this.decreeNumber,
                    dateactive: csharpDateActive,
                    datesigned: csharpDateSigned,
                    signed: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
        }
        this.modalDecreeMenuVisible = false;
    }

    decreeUpdate() {
        
        let csharpDateActive: Date = new Date(this.decreeDateactive);
        let csharpDateSigned: Date = new Date(this.decreeDatesigned);

        if (this.$store.state.decree != null && this.$store.state.decree != 0) {
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 5, // 5 - save decree info (date) changes.
                    id: this.$store.state.decree,
                    name: this.decreeName,
                    nickname: this.decreeNickname,
                    number: this.decreeNumber,
                    dateactive: csharpDateActive,
                    datesigned: csharpDateSigned,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => {  this.fetchDecree(); })
        }
        //this.modalDecreeMenuVisible = false;
    }

    decreeUpdateSigned() {

        let csharpDateActive: Date = new Date(this.decreeDateactive);
        let csharpDateSigned: Date = new Date(this.decreeDatesigned);

        if (this.decreeId == 0)
            return;
        let changed_access: boolean = true;
        this.decreesSignedList.forEach(f => {
            if (f.id == this.decreeId && f.signed == 1) {
                changed_access = false;
                return;
            }
        })

        if (changed_access) {
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 5, // 5 - save decree info (date) changes.
                    id: this.decreeId,
                    name: this.decreeName,
                    nickname: this.decreeNickname,
                    number: this.decreeNumber,
                    dateactive: csharpDateActive,
                    datesigned: csharpDateSigned,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => { this.fetchDecreeSigned(this.decreeId); this.filterSignedDecree(); })
        }
    }



    get sidebardisp() {
        return this.sidebarDisplay;
        //return (<any>Vue).sidebar;
    }

    renameStructureID(id): any {
        return this.renameStructure + "_" + id;
    }

    removeStructureID(id): any {
        return this.removeStructure + "_" + id;
    }

    fetchFeaturedStructures() {
        let featuredStructures: Array<FeaturedStructure>;
        fetch('api/Structure/Featured', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Array<FeaturedStructure>>;
            })
            .then(result => {
                featuredStructures = result;
                this.featured = new Array();

                featuredStructures.forEach(f => {
                    let featuredStructure: FeaturedStructure = new FeaturedStructure();
                    featuredStructure.name = f.name;
                    featuredStructure.id = f.id;
                    this.featured.push(featuredStructure);
                })
            });/*
        fetch('api/Persondecreeoperationexcert/structureslist', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Array<FeaturedStructure>>;
            })
            .then(result => {
                let Structures = result;
                this.excertstructlist = new Array();

                Structures.forEach(f => {
                    let featuredStructure: FeaturedStructure = new FeaturedStructure();
                    featuredStructure.name = f.name;
                    featuredStructure.id = f.id;
                    this.excertstructlist.push(featuredStructure);
                })
            });*/
    }

    fetchDecree() {
        fetch('api/Decrees/' + this.$store.state.decree, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Decreemanagement>;
            })
            .then(result => {
                //this.decreesList = result;
                //alert(result.dateactive);
                //alert(this.toDateInputValue(result.dateactive));
                this.decreeDateactive = this.toDateInputValue(result.dateactive);
                this.decreeDatesigned = this.toDateInputValue(result.datesigned);
                this.decreeName = result.name;
                this.decreeNickname = result.nickname;
                this.decreeNumber = result.number;
            });
    }

    fetchDecreeSigned(id: number) {
        fetch('api/Decrees/' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Decreemanagement>;
            })
            .then(result => {
                //this.decreesList = result;
                //alert(result.dateactive);
                //alert(this.toDateInputValue(result.dateactive));
                this.decreeDateactive = this.toDateInputValue(result.dateactive);
                this.decreeDatesigned = this.toDateInputValue(result.datesigned);
                this.decreeName = result.name;
                this.decreeNickname = result.nickname;
                this.decreeNumber = result.number;
                this.decreeId = id;
            });
    }

    fetchDecreesActive() {
        fetch('api/Decrees/Active', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Decreemanagement[]>;
            })
            .then(result => {
                this.decreesList = result.reverse();
                //(<any>this.$refs.inputdecreecreate).focus();
            });
    }

    fetchDecreeOperations() {
        fetch('api/DecreeOperations/' + this.$store.state.decree, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Decreeoperation[]>;
            })
            .then(result => {
                //alert(JSON.stringify(result));
                this.decreeOperations = result;
            });
    }

    fetchDecreeOperationsSigned(decree: Decree) {
        console.log(JSON.stringify(<Decree>(decree)));
        fetch('/api/DecreeOperations/Finder', {
            method: 'post',
            body: JSON.stringify(<Decree>(decree)),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => {
            return response.json() as Promise<decree_operation[]>;
        }).then(date => {
            this.decree_operations = date;
        });
        fetch('api/DecreeOperations/' + decree, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Decreeoperation[]>;
            })
            .then(result => {
                //alert(JSON.stringify(result));
                this.decreeSignedOperations = result;
            });
    }

    handleCommand(command: string) {
         if (command.startsWith(this.removeStructure)) {
             this.modalStructureManagingPanelTopMenuVisible = true;
            //this.structureManagingType = "removestructure";
            //this.structureManagingParent = command.split('_')[1];
            //alert(command);
        } else if (command.startsWith(this.renameStructure)) {
             this.modalStructureManagingPanelTopMenuVisible = true;
            //this.structureManagingType = "renamestructure";
            //this.structureManagingParent = command.split('_')[1];
            //alert(command);
        }

    }

    openFeatured(event: any, id: number, name: string) {
        if (this.$store.state.modeselectcuration) {
            this.$store.commit("setModeselectedcuration", id);
            this.$store.commit("setModeselectcuration", false);
            return;
        }
        if (this.$store.state.modeselectheading) {
            this.$store.commit("setModeselectedheading", id);
            this.$store.commit("setModeselectheading", false);
            return;
        }
        if (this.$store.state.modeselectstructure) {
            this.$store.commit("setModeselectedstructure", id);
            this.$store.commit("setModeselectstructure", false);
            return;
        }

        // Если мы будем нажимать по структурам сверху из Электронного Личного Дела, автоматически переводить нас в структуру в лицах
        if (this.getFullmodeselected()) {
            if (this.$store.state.fullmode == "2") {
                fetch('api/Identity/Fullmode4', { credentials: 'include' })
                    .then(response => {
                        return response.json() as Promise<string>;
                    })
                    .then(result => {

                    })

                let appearance = {
                    positioncompact: this.$store.state.positioncompact,
                    sidebardisplay: 1,
                }
                this.$store.commit("updateUserAppearance", appearance);
                this.$store.commit("setModepanelVisible", 0);
                this.$store.commit("setEldVisible", 0);
                this.$store.commit("setCandidatesVisible", 0);

            }
        }

        this.$store.state.parentStructures = [id];
        (<any>Vue).forceStructureUpdate = true;
        //this.$store.commit("setForceDepartmentUpdate", true);
        this.$store.commit("setForcePositionUpdate", true);
        this.$store.commit("setPositionsListId", -id);
        this.$store.commit("setFeatured", id);
        this.$store.commit("setPositionsListTitle", name);
    }

    removeDecreeOperation(event: any, decreeId: number) {
        fetch('/api/DecreeOperations', {
            method: 'post',
            body: JSON.stringify(<Decreeoperation>{
                id: decreeId,
                metaStatus: 1, // 1 - delete decreeoperation including its subject
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => { this.fetchDecreeOperations(); });

        //(id);
    }

    getDecreeName() {
        if (this.$store.state.decreeName.length > 0) {
            return ', выбран "' + this.$store.state.decreeName + '"';
        } else {
            return "";
        }
    }

    getDecreeNameAlt() {
        if (this.$store.state.decreeName.length > 0) {
            return 'Проект приказа "' + this.$store.state.decreeName + '"';
        } else {
            return "";
        }
    }

    decreeSelected(id: number) {
        return (this.$store.state.decree != null && this.$store.state.decree == id);
    }

    isCustomDate(decreeoperation: Decreeoperation) {
        if (decreeoperation.datecustom > 0) {
            return true; 
        } else {
            return false;
        }
    }


    toDateInputValue(date: Date): string {
        if (date == null) {
            return "";
        }

        var local = new Date(date);
        if (local == null) {
            return "";
        }
        local.setMinutes(local.getMinutes() - local.getTimezoneOffset());
        if (local.toJSON() == null) {
            return "";
        }
        return local.toJSON().slice(0, 10);
    }

    /**
     * Превращает строку из чисел, разделенными "," в массив
     * @param str
     */
    toArrayNumberInputValue(str: string): number[] {
        let array: number[] = new Array();
        if (str == null || str.length == 0) {
            return array;
        }
        let arrayStr: string[] = str.split(',');

        arrayStr.forEach(a => {
            array.push(Number.parseInt(a));
        })
        return array;
    }

    displayIt(): boolean {
        return true;
    }

    decreeOperationVisualRank(rank: number): string {
        return "decreeoperation-ranklevel-" + rank;
    }

    beautifyDate(date: string): string {

        date = date.slice(0, 10);
        //return date;
        if (date.length == 0) {
            return "";
        }
        let parts: string[] = date.split("-");
        if (parts.length != 3) {
            return "";
        }
        return parts[2] + "." + parts[1] + "." + parts[0];
    }

    topmenudropdown(command: string) {
        switch (command) {
            case "decrees":
                this.decrees();
                break;
            case "modalAdminVisible":
                this.modalAdminVisible = true;
                break;
            case "modalSettingsPanelVisible":
                this.modalSettingsPanelVisible = true;
                break;
            case "logout":
                this.logout();
                break;
            case "modalAboutPanelVisible":
                this.modalAboutPanelVisible = true;
                break;
            case "decreesvisible":
                this.modalDecreesSignedMenuVisible = true;
                break;
            case "eld":
                this.toggleEld();
                break;
            case "rewrite":
                this.rewrite();
                break;
        }
    }

    getdate(): string {
        return this.$store.state.date;
    }

    year(date: Date): string {
        if (date == null) {
            let nowDate: Date = new Date();
            return nowDate.getFullYear().toString();
        } else {
            return new Date(date).getFullYear().toString();
        }
    }


    get login(): string {
        return this.$store.state.login;
    }

    restoreCurrentDate() {
        this.$store.commit("setDateByInput", (<any>Vue).toDateInputValue(new Date()));
    }

    reloadPage() {
        location.reload();
    }

    onPmrequestPanelClose() {
        if (!this.$store.state.modeselectstructure) {
            
            this.modalPmrequestPanelVisible = false;
        }
    }

    get displaySelectmodewarning(): boolean {
        return (this.$store.state.modeselectcuration || this.$store.state.modeselectheading || this.$store.state.modeselectstructure || this.$store.state.modeappointpersondecree || this.$store.state.modeappointpersonstructuredecree || this.$store.state.modeappointpersondecreeStructure);
    }

    get displayAppointpersonmodewarning(): boolean {
        return this.$store.state.modeappointperson;
        //return (this.$store.state.modeselectcuration || this.$store.state.modeselectheading || this.$store.state.modeselectstructure);
    }

    cancelmode() {
        if (this.$store.state.modeselectcuration) {
            this.$store.commit("setModeselectedcuration", 0);
            this.$store.commit("setModeselectcuration", false);
            return;
        }
        if (this.$store.state.modeselectheading) {
            this.$store.commit("setModeselectedheading", 0);
            this.$store.commit("setModeselectheading", false);
            return;
        }
        if (this.$store.state.modeselectstructure) {
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            return;
        }
    }

    cancelpersonappoint() {
        this.$store.commit("setModeappointedperson", 0);
        this.$store.commit("setModeappointperson", false);

        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }

        /*this.$store.commit("setEldVisible", 1);
        this.$store.commit("setPositionsListId", 0);*/

        this.$store.commit("updateUserAppearance", appearance);

        this.$store.commit("setdecreeoperationtemplatecreatorVisible", false);
        this.$store.commit("setdecreeoperationelementVisible", false);
    }

    modeName(): string {
        if (this.$store.state.mode == "0") {
            return "Орг-штатная работа,";
        } else {
            return "Прохождение службы,";
        }
    }

    personMode(): boolean {
        if (this.$store.state.mode == "1") {
            return true;
        } else {
            return false;
        }
    }

    structureMode(): boolean {
        if (this.$store.state.mode == "0") {
            return true;
        } else {
            return false;
        }
    }

    isAdmin(): boolean {
        if (this.$store.state.admin == "1") {
            return true;
        } else {
            return false;
        }
        //if (this.$store.state.)
    }

    structureEdit(): boolean {
        if (this.$store.state.admin == "1" || this.$store.state.structureeditorAccess == "1") {
            return true;
        } else {
            return false;
        }
        //if (this.$store.state.)
    }

    successmode() {
        if(this.num == 1){
            this.numberStructure = this.$store.state.modeappointedpersondecreeStructure;
            this.$store.commit("setModeappointedpersondecreeStructure", 0);
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }else 
        if(this.num == 2){
            this.numberNewStructure = this.$store.state.modeappointedpersondecreeStructure;
            this.structureNewName = this.getStructureName(this.numberNewStructure);
            this.newCourseName = this.getStructureById(Number.parseInt(this.getStructureById(this.numberNewStructure).parentstructure)).name2;
            this.facultyName = this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(this.numberNewStructure).parentstructure)).parentstructure)).name2;
            this.$store.commit("setModeappointedpersondecreeStructure", 0);
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }else
        if(this.num == 3){
            this.numberStructure = this.$store.state.modeappointedpersondecreeStructure;
            this.structureName = this.getStructureName(this.numberStructure);
            this.courseName = this.getStructureById(Number.parseInt(this.getStructureById(this.numberStructure).parentstructure)).name2;
            this.specialityName = this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(this.numberStructure).parentstructure)).parentstructure)).name2;
            this.facultyName = this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(this.numberStructure).parentstructure)).parentstructure)).parentstructure)).name2;
            this.$store.commit("setModeappointedpersondecreeStructure", 0);
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }
        else
        if(this.num == 4){
            this.numberStructure = this.$store.state.modeappointedpersondecreeStructure;
            this.$store.commit("setModeappointedpersondecreeStructure", 0);
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }
        else
        if(this.num == 5){
            this.numberNewStructure = this.$store.state.modeappointedpersondecreeStructure;
            this.structureNewName = this.getStructureName(this.numberNewStructure);
            this.$store.commit("setModeappointedpersondecreeStructure", 0);
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }
        else{
            this.$store.commit("setModeappointedpersondecreeStructure", 0);  
            this.$store.commit("setModeappointpersondecreeStructure", false);
            this.$store.commit("setModeselectedstructure", 0);  
            this.$store.commit("setModeselectstructure", false);
            this.num = 0;
            return;
        }
    }     
 
    structureModeAccess(): boolean {
        if (this.$store.state.admin == "1" || this.$store.state.structureeditorAccess == "1" || this.$store.state.personnelreadAccess == "1") {
            return true;
        } else {
            return false;
        }
        //if (this.$store.state.)
    }

    personnelRead(): boolean {
        if (this.$store.state.admin == "1" || this.$store.state.personneleditorAccess == "1" || this.$store.state.personnelreadAccess == "1") {
            return true;
        } else {
            return false;
        }
        //if (this.$store.state.)
    }

    personnelEdit(): boolean {
        if (this.$store.state.admin == "1" || this.$store.state.personneleditorAccess == "1") {
            return true;
        } else {
            return false;
        }
        //if (this.$store.state.)
    }

    changeMode() {
        fetch('api/Identity/Switch', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {
               
            //    //alert(JSON.stringify(result));
            //})
    }

    /**
     * Visible if button is pressed and mode is not enabled; 
     */
    get adminVisible(): boolean {
        return this.modalAdminVisible && !this.modeselectheading;
    }
    



    onAdminPanelClose() {
        if (!this.$store.state.modeselectheading) {
            this.modalAdminVisible = false;
        }


    }

    toggleEld() {
        let toggle: number = this.$store.state.eldVisible;
        if (toggle == 0) {
            toggle = 1;
        } else {
            toggle = 0;
        }

        let toggleReverse: number = 0;
        if (toggle == 0) {
            toggleReverse = 1;
        } else {
            toggleReverse = 0;
        }
        this.$store.commit("setEldVisible", toggle);
        let appearance = {
            positioncompact: 0,
            sidebardisplay: toggleReverse,
        }
        this.$store.commit("setPositionsListId", 0);

        this.$store.commit("updateUserAppearance", appearance);
    }

    toggleCandidates() {
        let toggle: number = this.$store.state.candidatesVisible;
        if (toggle == 0) {
            toggle = 1;
        } else {
            toggle = 0;
        }

        let toggleReverse: number = 0;
        if (toggle == 0) {
            toggleReverse = 1;
        } else {
            toggleReverse = 0;
        }
        this.$store.commit("setCandidatesVisible", toggle);
        let appearance = {
            positioncompact: 0,
            sidebardisplay: toggleReverse,
        }
        this.$store.commit("setPositionsListId", 0);

        this.$store.commit("updateUserAppearance", appearance);
    }

    modepanelVisible(): boolean {
        if (this.$store.state.modepanelVisible == null || this.$store.state.modepanelVisible == 0 || this.$store.state.modepanelVisible == "0") {
            return false;
        } else {
            return true;
        }
        //return this.$store.state.modepanelVisible;
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

    boolToNumb(bool: boolean): number {
        if (bool) {
            return 1;
        } else {
            return 0;
        }
    }

    numberToBool(numb: number): boolean {
        if (numb > 0) {
            return true;
        } else {
            return false;
        }
    }

    prepareDateToExport(date: string): Date {
        if (date == null || date.length == 0) {
            return new Date();
        }
        return new Date(date);
    }

    /**
     * Превращает строку input в date. Nullable допускает возможность отсутствия даты.
     * @param date
     */
    prepareDateToExportNullable(date: string): Date {
        if (date == null || date.length == 0) {
            return null;
        }
        return new Date(date);
    }

    /**
     * Превращаем массив чисел в строку, разделенную ",".
     * @param array
     */
    prepareArrayNumberToExportNullable(array: number[]): string {
        let str: string = "";
        if (array == null) {
            return "";
        }
        array.forEach(a => {
            if (str.length > 0) {
                str += ",";
            }
            str += a.toString();
        })
        return str;
    }

    forceUpdate() {
        this.$forceUpdate();
    }

    okbutton() {
        let datecustomNum: number = 0;

        this.rank = 0;
        this.structureregion = 0;

        //this.id = Number.parseInt(this.parent); // Not to store id in parent;
        //this.structurelist - parent
        //alert(this.parent);

        let nodecreeNum: number = 0;
        nodecreeNum = 1;

        fetch('/api/DetailedStructure', {
            method: 'post',
            body: JSON.stringify(<StructureManagement>{
                id: this.id, type: "renamestructure", parent: this.parent, name: this.name, featuredStr: this.featuredStr, nameshortened: this.nameshortened, datecustom: datecustomNum, dateactive: new Date(this.dateactive),
                name1: this.name1, name2: this.name2, name3: this.name3, separatestructure: 0,
                rank: this.rank, structureregion: this.structureregion, structuretype: 9, city: this.city, street: this.street, nodecree: nodecreeNum, structuretypesiblings: 0,
                subject1: this.prepareNumToExport(this.subject1),
                subject2: this.prepareNumToExport(this.subject2),
                subject3: this.prepareNumToExport(this.subject3),
                subject4: this.prepareNumToExport(this.subject4),
                subject5: this.prepareNumToExport(this.subject5),
                subject6: this.prepareNumToExport(this.subject6),
                subject7: this.prepareNumToExport(this.subject7),
                subject8: this.prepareNumToExport(this.subject8),
                subject9: this.prepareNumToExport(this.subject9),
                subject10: this.prepareNumToExport(this.subject10),
                subject11: this.prepareNumToExport(this.subject11),
                subject12: this.prepareNumToExport(this.subject12),
                subject13: this.prepareNumToExport(this.subject13),
                subject14: this.prepareNumToExport(this.subject14),
                subject15: this.prepareNumToExport(this.subject15),
                subjectnumber: this.prepareNumToExport(this.subjectnumber),
                subjectnotice: this.subjectnotice,
                subjectgender: this.prepareNumToExport(this.subjectgender),
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
        .then(response => { return response.json(); })
        .then((response) => {
            //this.fetchUsers();
            //(<any>Vue).notify(response);
            (<any>Vue).forceStructureUpdate = true;
            this.$emit('update:visiblevar', false);
        });
        
    }

    /**
     * Убираем null значения у num и меняем их на 0
     * @param num
     */
    prepareNumToExport(num: number): number {
        if (num == null) {
            return 0;
        }
        if (String(num) === '') {
            return 0;
        }
        if (typeof num === 'boolean') {
            let boolNum: boolean = !!num;
            if (boolNum) {
                num = 1;
            } else {
                num = 0;
            }
        }
        return num;
    }

    getRank(rank: number): string {
        if (rank == null || rank == 0) {
            return "";
        }
        let rtype: Rank = this.ranks.find(t => t.id == rank);
        if (rtype != null) {
            return rtype.name;
        } else {
            return "";
        }
    }

    fetchStructureRewards() {
        fetch('api/DetailedStructure/Rewards', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                this.structuresReward = data;
            });
    }

    fetchStructureRewardsAllowed() {
        fetch('api/DetailedStructure/Rewardsallowed', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                //alert(JSON.stringify(data));
                this.structuresRewardAllowedToSelect = data;
            });
    }
   
    getStructureName(structureid: number): string {
        if (structureid == null || structureid == 0) {
            return "";
        }
        let structure: Structure = this.$store.state.structures.find(t => t.id === structureid);
        if (structure != null) {
            return structure.name;
        } else {
            return "";
        }
    }

    getFacultiStructureName(structureid: number): string {
       let str = this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(Number.parseInt(this.getStructureById(structureid).parentstructure)).parentstructure)).parentstructure)).name1
       return str 
    }

    getStructureById(structureid: number): Structure {
        if (structureid == null || structureid == 0) {
            return;
        }
        let structure: Structure = this.$store.state.structures.find(t => t.id === structureid);
        if (structure != null) {
            return structure;
        } else {
            return;
        }
    }

    getStructureName2(structureid: number): string {
        if (structureid == null || structureid == 0) {
            return "";
        }
        let structure: Structure = this.structuresReward.find(t => t.id == structureid);
        if (structure != null) {
            return structure.name2;
        } else {
            return "";
        }
    }

    printDate(date: Date): string {
        if (date == null) {
            return "";
        }
        return this.beautifyDate(this.toDateInputValue(date));
    }

    getFullmodeselected(): boolean {
        if (this.$store.state.fullmode == null || this.$store.state.fullmode == "0") {
            return false;
        }
        return true;
    }    

    changeFullmode() {
        fetch('api/Identity/Fullmode0', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})

        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }
        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 1);
        this.$store.commit("setPositionsListId", 0);
        this.$store.commit("setEldVisible", 0);
        this.$store.commit("setCandidatesVisible", 0);
        this.$store.commit("setdecreeoperationelementVisible", 0);
        //this.orgMode();
    }

    /**
     * Ставит запятую с пробелом, если определенная строка не пуста, иначе просто пробел.
     * @param inputstring
     */
    commaspaceifnotnull(inputstring: string): string {
        if (inputstring == null || inputstring.length == 0) {
            return " ";
        } else {
            return ", ";
        }
    }

    /**
     * Меняет первую букву строки на маленькую. Для записей, начинающихся с большой буквы, но попавших в середину текста.
     * @param inputstring
     */
    smallletter(inputstring: string): string {
        if (inputstring == null || inputstring.length == 0) {
            return "";
        }
        if (inputstring.length > 1) {
            return inputstring.charAt(0).toLowerCase() + inputstring.slice(1);
        } else {
            return inputstring.toLowerCase();
        }
        
    }

    getChangedocumentstypeObject(changedocumentstype: number): Changedocumentstype {
        if (changedocumentstype == null || changedocumentstype == 0) {
            return null;
        }
        let changedocumentstypeobject: Changedocumentstype = this.changedocumentstypes.find(i => i.id == changedocumentstype);
        return changedocumentstypeobject;
    }

    printDateDocument(date: Date): string {
        if (date == null) {
            return "";
        }
        return moment(date).locale('ru').format('D MMMM YYYY [года]');
    }

    printDateDocumentFromString(date: string): string {
        if (date == null) {
            return "";
        }
        let dateSplit: string[] = date.split('.');
        if (dateSplit.length != 3) {
            return "";
        }
        let dateDate: Date = new Date(Number.parseInt(dateSplit[2]), Number.parseInt(dateSplit[1]) - 1, Number.parseInt(dateSplit[0]));
        return moment(dateDate).locale('ru').format('D MMMM YYYY [года]');
    }

    changeTest() {
        
    }

    fireForMajor(fireid: number): boolean {
        if (fireid == 1 || fireid == 3 || fireid == 9 || fireid == 13 || fireid == 17 || fireid == 18) {
            return true;
        } else {
            return false;
        }
    }

    getSubject(subject: number): Subject {
        if (subject == null || subject == 0) {
            return null;
        }
        let stype: Subject = this.subjects.find(p => p.id == subject);
        if (stype != null) {
            return stype;
        } else {
            return null;
        }
    }

    /**
     * Определяем, выводить сутки или суток
     * @param count
     */
    getDayFullString(count: number): string {
        if (count <= 1) {
            return "сутки";
        } else if (count.toString().endsWith('1')) {
            return "сутки";
        } else {
            return "суток";
        }
    }

    /**
     * Определяем, выводить календарный день, календарного дня или календарных дней
     * @param count
     */
    getDayCalendarString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "календарных дней";
        } else if (lastLetter == 1) {
            return "календарный день";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "календарного дня";
        } else {
            return "календарных дней";
        }
    }

    /**
     * Определяем, выводить дня или дней
     * @param count
     */
    getDayPluralString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "дней";
        } else if (lastLetter == 1) {
            return "дня";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "дней";
        } else {
            return "дней";
        }
    }

    /**
     * Определяем, выводить день, дня или дней
     * @param count
     */
    getDayString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "дней";
        } else if (lastLetter == 1) {
            return "день";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "дня";
        } else {
            return "дней";
        }
    }

    /**
     * Определяем, выводить месяц, месяца или месяцев
     * @param count
     */
    getMonthString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "месяцев";
        } else if (lastLetter == 1) {
            return "месяц";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "месяца";
        } else {
            return "месяцев";
        }
    }

    /**
     * Определяем, выводить год, года или лет
     * @param count
     */
    getYearString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "лет";
        } else if (lastLetter == 1) {
            return "год";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "года";
        } else {
            return "лет";
        }
    }

    getCountry(country: number): Country {
        if (country == null || country == 0) {
            return null;
        }
        let countryObject: Country = this.countries.find(p => p.id == country);
        if (countryObject != null) {
            return countryObject;
        } else {
            return null;
        }
    }

    getRefVacationinput(index: number): string {
        return "refvacationinput" + index;
    }

    getPeriodStartFromString(jobperiodstring: string): string {
        return jobperiodstring.split('+')[0];
    }

    getPeriodEndFromString(jobperiodstring: string): string {
        let split: string[] = jobperiodstring.split('+');
        if (split.length < 2) {
            return "";
        }
        return split[1];
    }

    getPeriodYearFromString(jobperiodstring: string): string {
        // Для старых приказов, когда мы писали только год, а не дату.
        if (!jobperiodstring.includes('.')) {
            return jobperiodstring;
        }
        return jobperiodstring.split('+')[0].split('.')[2];
    }

    addCity(contrycities: Countycities) {
        contrycities.cities.push(contrycities.citytoadd);
        contrycities.citytoadd = "";
    }

    deleteCity(city: string, countrycities: Countycities) {
        countrycities.cities = countrycities.cities.filter(c => c != city);
    }

    ordernumbertypeSearch(queryString, cb) {
        var links = this.OrdernumbertypesToArrayFilterable();
        var results = queryString ? links.filter(this.createOrdernumbertypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createOrdernumbertypeFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
    }

    OrdernumbertypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.ordernumbertypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    structuresalldocumentSearch(queryString, cb) {
        var links = this.StructuresalldocumentToArrayFilterable();
        var results = queryString ? links.filter(this.createStructuresalldocumentFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createStructuresalldocumentFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
    }

    StructuresalldocumentToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.structuresalldocument.forEach(o => {
            let link: Link = new Link();
            link.value = o;
            links.push(link);
        })
        return links;
    }

    rewrite() {
        fetch('api/ReWrite', {
            method: 'post',
            body: JSON.stringify(<User>(this.user)),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })

    }
}