import Vue from 'vue';

import { Component, Prop, Watch} from 'vue-property-decorator';
import Element, { Switch } from 'element-ui';
import { Button, Select, Input, Dialog, Dropdown, DropdownItem, DropdownMenu, Checkbox, CheckboxGroup, Autocomplete } from 'element-ui';
import Decreemanagement from '../../classes/decreemanagement'
import Decreeoperation from '../../classes/decreeoperation'
import Persondecree from '../../classes/persondecree'
import { FeaturedStructure, excerptStructures } from '../../classes/persondecreeoperation'
import Persondecreeoperation from '../../classes/persondecreeoperation'
import Persondecreeblock from '../../classes/persondecreeblock'
import Persondecreeblocktype from '../../classes/persondecreeblocktype'
import Persondecreeblocksub from '../../classes/persondecreeblocksub'
import Persondecreeblocksubtype from '../../classes/persondecreeblocksubtype'
import Region from '../../classes/region'
import Area from '../../classes/area'
import Structure from '../../classes/structure'
import Rewardmoney from '../../classes/rewardmoney'
import download from 'downloadjs';

import Educationstage from '../../classes/educationstage';
import Educationadditionaltype from '../../classes/educationadditionaltype';
import Educationpositiontype from '../../classes/educationpositiontype';
import Educationperiod from '../../classes/educationperiod';
import Educationtypeblock from '../../classes/educationtypeblock';
import Structureregion from '../../classes/structureregion';
import Structuretype from '../../classes/structuretype';
import StructureTree from '../../classes/structuretree';
import Rank from '../../classes/rank';
import Person from '../../classes/person';
import Personrank from '../../classes/personrank';
import Personphoto from '../../classes/personphoto';
import Position from '../../classes/position';
import Personcontract from '../../classes/personcontract';
import Relativetype from '../../classes/relativetype';
import Personrelative from '../../classes/personrelative';
import Personattestation from '../../classes/personattestation';
import Personvacation from '../../classes/personvacation';
import Attestationtype from '../../classes/attestationtype';
import Vacationtype from '../../classes/vacationtype';
import Vacationmilitary from '../../classes/vacationmilitary';
import Languagetype from '../../classes/languagetype';
import Languageskill from '../../classes/languageskill';
import Jobtype from '../../classes/jobtype';
import Servicetype from '../../classes/servicetype';
import Servicefeature from '../../classes/servicefeature';
import Servicecoef from '../../classes/servicecoef';
import Personlanguage from '../../classes/personlanguage';
import Personjob from '../../classes/personjob';
import Penalty from '../../classes/penalty';
import Country from '../../classes/country';
import Personpenalty from '../../classes/personpenalty';
import Personworktrip from '../../classes/personworktrip';
import Personelection from '../../classes/personelection';
import Personscience from '../../classes/personscience';
import Science from '../../classes/science';
import Illregime from '../../classes/illregime';
import Illcode from '../../classes/illcode';
import Rewardtype from '../../classes/rewardtype';
import Reward from '../../classes/reward';
import Personreward from '../../classes/personreward';
import Educationmaternity from '../../classes/educationmaternity';
import Personill from '../../classes/personill';
import Personeducation from '../../classes/personeducation';
import Educationlevel from '../../classes/educationlevel';
import Educationtype from '../../classes/educationtype';
import Certificate from '../../classes/certificate';
import Personphysical from '../../classes/personphysical';
import Physicalfield from '../../classes/physicalfield';
import Normativ from '../../classes/normativ';
import Persondriver from '../../classes/persondriver';
import Personpermission from '../../classes/personpermission';
import Personprivelege from '../../classes/personprivelege';
import Drivertype from '../../classes/drivertype';
import Drivercategory from '../../classes/drivercategory';
import Permissiontype from '../../classes/permissiontype';
import Persondispanserization from '../../classes/persondispanserization';
import Personvvk from '../../classes/personvvk';
import Personjobprivelege from '../../classes/personjobprivelege';
import Persontransfer from '../../classes/persontransfer';
import Prooftype from '../../classes/prooftype';
import Educationdocument from '../../classes/educationdocument';
import Holiday from '../../classes/holiday';
import Jobperiod from '../../classes/jobperiod';
import User from '../../classes/user';
import moment from 'moment';
import Fire from '../../classes/fire';
import Appointtype from '../../classes/appointtype';
import Transfertype from '../../classes/transfertype';
import Interrupttype from '../../classes/interrupttype';
import Changedocumentstype from '../../classes/changedocumentstype';
import Setpersondatatype from '../../classes/setpersondatatype';
import Persondecreeblockintro from '../../classes/persondecreeblockintro';
import Persondecreelevel from '../../classes/persondecreelevel';
import Subject from '../../classes/subject';
import Academicvacation from '../../classes/academicvacation';
import Subjectcategory from '../../classes/subjectcategory'
import Subjectgender from '../../classes/subjectgender'
import Countycities from '../../classes/countrycities'
import Countrycities from '../../classes/countrycities';
import Ordernumbertype from '../../classes/ordernumbertype';
import Link from '../../classes/link';
import Dismissalclauses from '../../classes/dismissalclauses';
import Cabinetdata from '../../classes/cabinetdata';
import ExcertComposition from '../../classes/Excerts/excertComposition';
import Mailexplorer from '../../classes/Mail/mailexplorer';
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
    fiosearch: string;
    person: Person;
    personssearch: Person[];
    lastUploadedPhoto: any;
    photos: Personphoto[];
    photosPreview: Personphoto[]; // Если мы что-то ищем в поиске, то заодно будем загружать предварительные фотографии
    photoToCreate: Personphoto;

    lastSearchFio: string;

    specialityName: string;
    facultyName: string;
    courseName: string;
    newCourseName: string;
    structureName: string;
    structureNewName: string;

    moveorder: boolean;
    usersearch: string;
    userSelected: User;
    usersSearch: User[];

    structuresReward: Structure[];
    structuresRewardAllowedToSelect: Structure[];
    structuresElders: Structure[];
    persondecreeblocksubsMass: number[];

    //rewardmoneys: Rewardmoney[];
    customwidth: boolean;
    excertmode: boolean;
    excertstructlist: FeaturedStructure[];

    checkedDecreeResualt: string;

    value5: boolean;

    
    onDecreeDatesignedChange(value: string, oldValue: string) {
        this.decreeDateactive = this.decreeDatesigned;
    }

    data() {
        return {
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
            personFromStructure: [],
            candidateSearch: [],
            decreesSignedList: [],
            decreeSignedOperations: [],
            modalDecreeMenuSignedVisible: false,

            moveorder: false,
            usersearch: "",
            userSelected: null,
            usersSearch: [],

            structuresReward: [],
            structuresRewardAllowedToSelect: [],
            structuresElders: [],

            personvacationHolidays: null,
            //rewardmoneys: [],

            customwidth: false,
            excertmode: false,
            excertstructlist: [],

            checkedDecreeResualt: "",

            value5: this.$store.state.user.onlyreadflagtoeditor == 1 ? true : false,
        }
    }

    mounted() {
        setInterval(this.checkSidebarAndAccessAndDecreeName, 250);
        setInterval(this.renewDecrees, 1000);
        this.fetchFeaturedStructures();
        this.fetchStructureRewards();
        this.fetchStructureRewardsAllowed();
        this.fetchStructureElders();

        //this.$store.commit("setDepartmentsListId", 7);

        //let rewardmoney1: Rewardmoney = new Rewardmoney();
        //rewardmoney1.id = 1;
        //rewardmoney1.name = "должностного оклада";
        //rewardmoney1.rewardmoneytype = "должностного оклада";
        //let rewardmoney2: Rewardmoney = new Rewardmoney();
        //rewardmoney2.id = 2;
        //rewardmoney2.name = "оклада денежного содержания";
        //rewardmoney2.rewardmoneytype = "рублей";
        //let rewardmoney3: Rewardmoney = new Rewardmoney();
        //rewardmoney3.id = 3;
        //rewardmoney3.name = "расчетного денежного оклада";
        //rewardmoney3.rewardmoneytype = "рублей";
        //this.rewardmoneys.push(rewardmoney1);
        //this.rewardmoneys.push(rewardmoney2);
        //this.rewardmoneys.push(rewardmoney3);
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

    get relativetypes(): Relativetype[] {
        return this.$store.state.relativetypes;
    }

    get attestations(): Attestationtype[] {
        return this.$store.state.attestationtypes;
    }

    get vacations(): Vacationtype[] {
        return this.$store.state.vacationtypes;
    }

    get vacationmilitaries(): Vacationmilitary[] {
        return this.$store.state.vacationmilitaries;
    }

    get languagetypes(): Languagetype[] {
        return this.$store.state.languagetypes;
    }

    get languageskills(): Languageskill[] {
        return this.$store.state.languageskills;
    }

    get jobtypes(): Jobtype[] {
        return this.$store.state.jobtypes;
    }

    get servicetypes(): Servicetype[] {
        return this.$store.state.servicetypes;
    }

    get servicefeatures(): Servicefeature[] {
        return this.$store.state.servicefeatures;
    }

    get servicecoefs(): Servicecoef[] {
        return this.$store.state.servicecoefs;
    }

    get penalties(): Penalty[] {
        return this.$store.state.penalties;
    }

    get dismissalclauses(): Dismissalclauses[] {
        return this.$store.state.dismissalclauses;
    }

    get countries(): Country[] {
        return this.$store.state.countries;
    }

    get sciences(): Science[] {
        return this.$store.state.sciences;
    }

    get illregimes(): Illregime[] {
        return this.$store.state.illregimes;
    }

    get illcodes(): Illcode[] {
        return this.$store.state.illcodes;
    }

    get rewardtypes(): Rewardtype[] {
        return this.$store.state.rewardtypes;
    }

    get rewards(): Reward[] {
        return this.$store.state.rewards;
    }

    get educationlevels(): Educationlevel[] {
        return this.$store.state.educationlevels;
    }

    get educationtypes(): Educationtype[] {
        return this.$store.state.educationtypes;
    }
    get educationperiods(): Educationperiod[] {
        return this.$store.state.educationperiods;
    }

    get educationadditionaltypes(): Educationadditionaltype[] {
        return this.$store.state.educationadditionaltypes;
    }

    get educationstages(): Educationstage[] {
        return this.$store.state.educationstages;
    }

    get educationpositiontypes(): Educationpositiontype[] {
        return this.$store.state.educationpositiontypes;
    }

    get educationdocuments(): Educationdocument[] {
        return this.$store.state.educationdocuments;
    }

    get normativs(): Normativ[] {
        return this.$store.state.normativs;
    }

    get drivertypes(): Drivertype[] {
        return this.$store.state.drivertypes;
    }

    get drivercategories(): Drivercategory[] {
        return this.$store.state.drivercategories;
    }

    get permissiontypes(): Permissiontype[] {
        return this.$store.state.permissiontypes;
    }

    get personeducation(): Personeducation[]{
        return this.$store.state.personeducation;
    }

    get prooftypes(): Prooftype[] {
        return this.$store.state.prooftypes;
    }

    get holidays(): Holiday[] {
        return this.$store.state.holidays;
    }

    get persondecreeblocktypes(): Persondecreeblocktype[] {
        return this.$store.state.persondecreeblocktypes;
    }

    get persondecreeblocksubtypes(): Persondecreeblocksubtype[] {
        return this.$store.state.persondecreeblocksubtypes;
    }

    get regions(): Region[] {
        return this.$store.state.regions;
    }

    get areas(): Area[] {
        return this.$store.state.areas;
    }

    get fires(): Fire[] {
        return this.$store.state.fires;
    }

    get appointtypes(): Appointtype[] {
        return this.$store.state.appointtypes;
    }

    get transfertypes(): Transfertype[] {
        return this.$store.state.transfertypes;
    }

    get interrupttypes(): Interrupttype[] {
        return this.$store.state.interrupttypes;
    }

    get changedocumentstypes(): Changedocumentstype[] {
        return this.$store.state.changedocumentstypes;
    }

    get setpersondatatypes(): Setpersondatatype[] {
        return this.$store.state.setpersondatatypes;
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
        this.value5 = this.$store.state.user.onlyreadflagtoeditor == 1 ? true : false
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

    selectSignedDecree(event: any, id: number) {
        this.modalDecreeMenuSignedVisible = true;
        this.fetchDecreeOperationsSigned(id);
        this.fetchDecreeSigned(id);
        this.checkedDecreeResualt = "";
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
            //this.$store.commit("setDecree", 1);
            fetch('/api/Decrees', {
                method: 'post',
                body: JSON.stringify(<Decreemanagement>{
                    decreemanagementstatus: 6,
                    dateactivestart: this.decreeFilterDateactiveStart,
                    dateactiveend: this.decreeFilterDateactiveEnd,
                    datesignedstart: this.decreeFilterDatesignedStart,
                    datesignedend: this.decreeFilterDatesignedEnd,
                    number: this.decreeFilterNumber,
                    name: this.decreeFilterName,
                    nickname: this.decreeFilterNickname,
                    
                    
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
            .then(response => {
                return response.json() as Promise<Decreemanagement[]>;
            })
                .then(result => {
                    this.decreesSignedList = result;
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
            .then(x => download(x, this.decreeName + ".docx"))
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
            });
        /*fetch('api/Persondecreeoperationexcert/structureslist', { credentials: 'include' })
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

    fetchDecreeOperationsSigned(decree: number) {
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
            case "candidates":
                this.toggleCandidates();
                
                break;
            case "eld":
                this.toggleEld();
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

    onPersondecreeDatesignedChange(value: string, oldValue: string) {
        this.decreeDateactive = this.decreeDatesigned;
    }

    searchPersons(fio: string) {
        fetch('api/Person/Search' + fio, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person[]>;
            })
            .then(result => {
                this.personssearch = result;
                this.person = null;
                fetch('api/Person/Photospreview' + fio, { credentials: 'include' })
                    .then(response => {
                        return response.json() as Promise<Personphoto[]>;
                    })
                    .then(personphotos => {
                        if (personphotos != null) {

                            personphotos.forEach(p => p.photo64 = p.photo64header + "," + p.photo64);

                            this.photosPreview = personphotos;
                            this.lastSearchFio = fio;

                        }
                    })
            })
    }

    /**
     * Поиск людей специально внутри проекта приказов
     * @param fio
     * @param block
     */
    searchPersonsBlock(block: Persondecreeblock) {
        block.searchiteration = block.searchiteration + 1;
        let searchiteration: number = block.searchiteration;

        let fio: string = block.fiosearch;
        fetch('api/Person/Search' + fio, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person[]>;
            })
            .then(result => {
                if (searchiteration < block.searchiteration) {
                    return;
                }
                block.personssearch = result;
                //this.person = null;
                fetch('api/Person/Photospreview' + fio, { credentials: 'include' })
                    .then(response => {
                        return response.json() as Promise<Personphoto[]>;
                    })
                    .then(personphotos => {
                        if (personphotos != null) {
                            
                            personphotos.forEach(p => p.photo64 = p.photo64header + "," + p.photo64);

                            block.photosPreview = personphotos;
                            //this.lastSearchFio = fio;
                        }
                    })
            })
    }

    searchCandidatsBlock(block: Persondecreeblock) {
        block.searchiterationCandidate = block.searchiterationCandidate + 1;
        let searchiterationCandidate: number = block.searchiterationCandidate;

        let fio: string = block.fiocandidatesearch;
        fetch('api/Cabinet/Search' + fio, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Cabinetdata[]>;
            })
            .then(result => {
                if (searchiterationCandidate < block.searchiterationCandidate) {
                    return;
                }
                block.candidatessearch = result;
            })
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

        if (person.personranks != null) {
            person.personranks.forEach(p => {
                p.decreedateString = this.toDateInputValue(p.decreedate);
            })
        }

        if (person.personcontracts != null) {
            person.personcontracts.forEach(p => {
                p.datestartString = this.toDateInputValue(p.datestart);
                p.dateendString = this.toDateInputValue(p.dateend);
                p.payBool = this.numberToBool(p.pay);
            })
        }

        if (person.personrelatives != null) {
            person.personrelatives.forEach(p => {
                p.birthdayString = this.toDateInputValue(p.birthday);
            })
        }

        if (person.personattestations != null) {
            person.personattestations.forEach(p => {
                p.dateString = this.toDateInputValue(p.date);
            })
        }

        if (person.personvacations != null) {
            person.personvacations.forEach(p => {
                p.dateString = this.toDateInputValue(p.date);
                p.compensationBool = this.numberToBool(p.compensation);
                p.compensationdateString = this.toDateInputValue(p.compensationdate);
                p.cancelBool = this.numberToBool(p.cancel);
                p.allowstartString = this.toDateInputValue(p.allowstart);
                p.allowendString = this.toDateInputValue(p.allowend);
                p.canceldateString = this.toDateInputValue(p.canceldate);
                p.cancelcontinueBool = this.numberToBool(p.cancelcontinue);
                p.canceldateendString = this.toDateInputValue(p.canceldateend);
            })
        }

        if (person.personeducations != null) {
            person.personeducations.forEach(p => {
                p.startString = this.toDateInputValue(p.start);
                p.endString = this.toDateInputValue(p.end);
                p.cadetBool = this.numberToBool(p.cadet);
                p.interruptedBool = this.numberToBool(p.interrupted);
            })
        }

        if (person.personjobs != null) {
            person.personjobs.forEach(p => {
                //alert(p.orderdate == null);
                p.startString = this.toDateInputValue(p.start);
                p.endString = this.toDateInputValue(p.end);
                p.orderdateString = this.toDateInputValue(p.orderdate);
                p.actualBool = this.numberToBool(p.actual);
                p.mchsBool = this.numberToBool(p.mchs);
                //alert(p.startString);
            })
        }

        if (person.personpenalties != null) {
            person.personpenalties.forEach(p => {
                p.orderdateString = this.toDateInputValue(p.orderdate);
            })
        }

        if (person.personworktrips != null) {
            person.personworktrips.forEach(p => {
                p.tripdateString = this.toDateInputValue(p.tripdate);
            })
        }

        if (person.personelections != null) {
            person.personelections.forEach(p => {
                p.electiondateString = this.toDateInputValue(p.electiondate);
                p.electiondateendString = this.toDateInputValue(p.electiondateend);
            })
        }

        if (person.personsciences != null) {
            person.personsciences.forEach(p => {
                p.sciencedateString = this.toDateInputValue(p.sciencedate);
            })
        }

        if (person.personrewards != null) {
            person.personrewards.forEach(p => {
                p.rewarddateString = this.toDateInputValue(p.rewarddate);
            })
        }

        if (person.personills != null) {
            person.personills.forEach(p => {
                p.datestartString = this.toDateInputValue(p.datestart);
                p.dateendString = this.toDateInputValue(p.dateend);
                p.illtypeBool = this.numberToBool(p.illtype);
            })
        }

        if (person.personphysicals != null) {
            person.personphysicals.forEach(p => {
                p.physicaldateString = this.toDateInputValue(p.physicaldate);
            })
        }

        if (person.persondrivers != null) {
            person.persondrivers.forEach(p => {
                p.datestartString = this.toDateInputValue(p.datestart);
                p.dateendString = this.toDateInputValue(p.dateend);
            })
        }

        if (person.personpermissions != null) {
            person.personpermissions.forEach(p => {
                p.datestartString = this.toDateInputValue(p.datestart);
                p.dateendString = this.toDateInputValue(p.dateend);
            })
        }

        if (person.persondispanserizations != null) {
            person.persondispanserizations.forEach(p => {
                p.dateString = this.toDateInputValue(p.date);
            })
        }

        if (person.personvvks != null) {
            person.personvvks.forEach(p => {
                p.dateString = this.toDateInputValue(p.date);
            })
        }

        if (person.personjobpriveleges != null) {
            person.personjobpriveleges.forEach(p => {
                p.startString = this.toDateInputValue(p.start);
                p.endString = this.toDateInputValue(p.end);
                p.proofdateString = this.toDateInputValue(p.proofdate);
                p.documentdateString = this.toDateInputValue(p.documentdate);
            })
        }
    }

    prepareToExport(person: Person): void {

        person.birthdate = this.prepareDateToExport(person.birthdateString);
        person.passportdatestart = new Date(person.passportdatestartString);
        person.passportdateend = new Date(person.passportdateendString);

        if (person.personranks != null) {
            person.personranks.forEach(p => {
                p.decreedate = new Date(p.decreedateString);
            })
        }

        if (person.personcontracts != null) {
            person.personcontracts.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
                p.pay = this.boolToNumb(p.payBool);
            })
        }

        if (person.personrelatives != null) {
            person.personrelatives.forEach(p => {
                p.birthday = new Date(p.birthdayString);
            })
        }

        if (person.personattestations != null) {
            person.personattestations.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personvacations != null) {
            person.personvacations.forEach(p => {
                p.date = new Date(p.dateString);
                p.compensation = this.boolToNumb(p.compensationBool);
                p.compensationdate = new Date(p.compensationdateString);
                p.cancel = this.boolToNumb(p.cancelBool);
                p.allowstart = new Date(p.allowstartString);
                p.allowend = new Date(p.allowendString);
                p.canceldate = new Date(p.canceldateString);
                p.canceldateend = new Date(p.canceldateendString);
                p.cancelcontinue = this.boolToNumb(p.cancelcontinueBool);
            })
        }

        if (person.personeducations != null) {
            person.personeducations.forEach(p => {

                p.start = new Date(p.startString);
                p.end = new Date(p.endString);
                p.cadet = this.boolToNumb(p.cadetBool);
                p.interrupted = this.boolToNumb(p.interruptedBool);
            })
        }

        if (person.personjobs != null) {
            person.personjobs.forEach(p => {
                p.start = new Date(p.startString);
                p.end = new Date(p.endString);
                p.orderdate = new Date(p.orderdateString);
                p.actual = this.boolToNumb(p.actualBool);
                p.mchs = this.boolToNumb(p.mchsBool);
            })
        }

        if (person.personpenalties != null) {
            person.personpenalties.forEach(p => {
                p.orderdate = new Date(p.orderdateString);
            })
        }

        if (person.personworktrips != null) {
            person.personworktrips.forEach(p => {
                p.tripdate = new Date(p.tripdateString);
            })
        }

        if (person.personelections != null) {
            person.personelections.forEach(p => {
                p.electiondate = new Date(p.electiondateString);
                p.electiondateend = new Date(p.electiondateendString);
            })
        }

        if (person.personsciences != null) {
            person.personsciences.forEach(p => {
                p.sciencedate = new Date(p.sciencedateString);
            })
        }

        if (person.personrewards != null) {
            person.personrewards.forEach(p => {
                p.rewarddate = new Date(p.rewarddateString);
            })
        }

        if (person.personills != null) {
            person.personills.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
                p.illtype = this.boolToNumb(p.illtypeBool);
            })
        }

        if (person.personphysicals != null) {
            person.personphysicals.forEach(p => {
                p.physicaldate = new Date(p.physicaldateString);
            })
        }

        if (person.persondrivers != null) {
            person.persondrivers.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
            })
        }

        if (person.personpermissions != null) {
            person.personpermissions.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
            })
        }

        if (person.persondispanserizations != null) {
            person.persondispanserizations.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personvvks != null) {
            person.personvvks.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personjobpriveleges != null) {
            person.personjobpriveleges.forEach(p => {
                p.start = new Date(p.startString);
                p.end = new Date(p.endString);
                p.proofdate = new Date(p.proofdateString);
                p.documentdate = new Date(p.documentdateString);

            })
        }
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
     * Добавляем сотрудника по айди в блок в режиме множественного добавления, когда несколько человек привязаны к одной операции.
     * @param id
     * @param block
     */
    selectPersonBlockNonAutoMultiselect(id: number, block: Persondecreeblock) {
        fetch('api/Person/Single' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person>;
            })
            .then(person => {
                if (person != null) {
                    this.prepareToImport(person);
                    block.optionarraypersonArray.push(person.id);
                    block.optionarraypersonObjects.push(person);
                    //block.person = person;
                    //block.optionarraypersonObjects.length
                    block.personssearch = [];
                    block.fiosearch = "";
                    block.nonperson = ""; // Если был человек не из МЧС, убираем.

                    // Если присвоить
                    if (block.persondecreeblocktype == 14) {
                        block.persondecreeblocksub = null; // Обнуляем список доступных званий для выбора
                    }

                    block.personssearchadditional = false; // Там где добавляем сразу несколько людей, помечать, что уже хотя бы один человек добавлен,
                                                           // чтобы скрыть строку поиска и показать кнопку "добавить".

                    //this.addPersonblockelement(block); - отрубаем автоматическое дополнение. Но тогда для добавления отдельно должна быть кнопка.
                }
            })
    }

    multipersonAddAdditional(block: Persondecreeblock) {
        block.personssearchadditional = true;
    }

    multicountryAddAdditional(block: Persondecreeblock) {
        block.countrycitiesList.push(new Countycities());
    }

    getEducationtype(educationtype: number): string {
        if (educationtype == null || educationtype == 0) {
            return "";
        }
        let etype: Educationtype = this.educationtypes.find(t => t.id == educationtype);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }
    } 

    getEducationperiod(educationperiod: number): Educationperiod {
        if (educationperiod == null || educationperiod == 0) {
            return null;
        }
        let eperiod: Educationperiod = this.educationperiods.find(t => t.id == educationperiod);
        if (eperiod != null) {
            return eperiod;
        } else {
            return null;
        }
    } 

    getEducationlevel(educationlevel: number): string {
        if (educationlevel == null || educationlevel == 0) {
            return "";
        }
        let object: Educationlevel = this.educationlevels.find(e => e.id == educationlevel);
        if (object != null) {
            return object.levelname;
        } else {
            return "";
        }
    } 

    getFullEducationlevel(educationlevel: number): string {
        if (educationlevel == null || educationlevel == 0) {
            return "";
        }
        let object: Educationlevel = this.educationlevels.find(e => e.id == educationlevel);
        if (object != null) {
            return object.levelcomment;
        } else {
            return "";
        }
    } 

    getEducationstage(educationstage: number): string{
        if (educationstage == null || educationstage == 0) {
            return "";
        }
        let object: Educationstage = this.educationstages.find(e => e.id == educationstage);
        if (object != null) {
            return object.name;
        } else {
            return "";
        }
    }
    
    getEducationpositiontype(educationpositiontype: number): string {
        if (educationpositiontype == null || educationpositiontype == 0) {
            return "";
        }
        let object: Educationpositiontype = this.educationpositiontypes.find(e => e.id == educationpositiontype);
        if (object != null) {
            return object.name;
        } else {
            return "";
        }
    }

    getEducationadditionaltype(educationadditionaltype: number): string {
        if (educationadditionaltype == null || educationadditionaltype == 0) {
            return "";
        }
        let etype: Educationtype = this.educationadditionaltypes.find(t => t.id == educationadditionaltype);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }

    }

    getEducationdocument(educationdocument: number): string {
        if (educationdocument == null || educationdocument == 0) {
            return "";
        }
        let etype: Educationdocument = this.educationdocuments.find(t => t.id == educationdocument);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }
    }

    getPersoneducation(personId: number): Personeducation{
        if (personId == null || personId == 0) {
            return null;
        }
        let personeducation: Personeducation = this.personeducation.find(e => e.id == personId);
        if (personeducation != null) {
            return personeducation;
        } else {
            return null;
        }
    }

    getPersoneducationUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    removeEducationperiod(educationtypeblock: Educationtypeblock, educationperiod: Educationperiod) {
        educationtypeblock.educationperiods = educationtypeblock.educationperiods.filter(e => e != educationperiod);
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

    

    closeSearch() {
        this.personssearch = [];
    }

    closeSearchBlock(block: Persondecreeblock) {
        block.personssearch = [];
    }

    rerenderSearch() {
        if (this.personssearch != null && this.personssearch.length > 0) {
            this.searchPersons(this.lastSearchFio);
        }
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

    getPersondecreeblockname(block: Persondecreeblock): string {
        let type: Persondecreeblocktype = this.persondecreeblocktypes.find(p => p.id == block.persondecreeblocktype);
        if (type != null) {
            return type.name;
        } else {
            return "";
        }
    }

    getPersondecreeblocksubname(block: Persondecreeblocksub): string {
        let type: Persondecreeblocksubtype = this.persondecreeblocksubtypes.find(p => p.id == block.persondecreeblocksubtype);
        if (type != null) {
            return type.name;
        } else {
            return "";
        }
    }

    getPersondecreeblocksubtype(persondecreeblocksubtypeid: number): string {
        if (persondecreeblocksubtypeid == null || persondecreeblocksubtypeid == 0) {
            return "";
        }
        let persondecreeblocksubtype: Persondecreeblocksubtype = this.persondecreeblocksubtypes.find(t => t.id == persondecreeblocksubtypeid);
        if (persondecreeblocksubtype != null) {
            return persondecreeblocksubtype.name;
        } else {
            return "";
        }
    }

    getRewardtype(rewardtype: number): string {
        if (rewardtype == null || rewardtype == 0) {
            return "";
        }
        let rtype: Rewardtype = this.rewardtypes.find(t => t.id == rewardtype);
        if (rtype != null) {
            return rtype.name;
        } else {
            return "";
        }
    }

    getReward(reward: number): string {
        if (reward == null || reward == 0) {
            return "";
        }
        let rtype: Reward = this.rewards.find(t => t.id == reward);
        if (rtype != null) {
            return rtype.name;
        } else {
            return "";
        }
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

    searchUsers(search: string) {

        fetch('api/Users/Search' + search, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<User[]>;
            })
            .then(result => {
                result.forEach(r => {
                    if (r.positionString == null) {
                        r.positionString = "";
                    }
                })

                this.usersSearch = result;
                this.userSelected = null;
            })
    }

    hasUserSearchResults(): boolean {
        if (this.usersSearch != null && this.usersSearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    persondecreeblocksubChange(persondecreeblock: Persondecreeblock) {
        persondecreeblock.optionstring1 = "";
        persondecreeblock.optionstring2 = "";
        persondecreeblock.optionstring3 = "";
        persondecreeblock.optionstring4 = "";
        persondecreeblock.optionstring5 = "";
        persondecreeblock.optionstring6 = "";
        persondecreeblock.optionstring7 = "";
        persondecreeblock.optionstring8 = "";
        persondecreeblock.optionnumber1 = null;
        persondecreeblock.optionnumber2 = null;
        persondecreeblock.optionnumber3 = null;
        persondecreeblock.optionnumber4 = null;
        persondecreeblock.optionnumber5 = null;
        persondecreeblock.optionnumber6 = null;
        persondecreeblock.optionnumber7 = null;
        persondecreeblock.optionnumber8 = null;
        persondecreeblock.optionnumber9 = null;
        persondecreeblock.optionnumber10 = null;
        persondecreeblock.optionnumber11 = null;
        persondecreeblock.optiondate1 = null;
        persondecreeblock.optiondate2 = null;
        persondecreeblock.optiondate3 = null;
        persondecreeblock.optiondate4 = null;
        persondecreeblock.optiondate5 = null;
        persondecreeblock.optiondate6 = null;
        persondecreeblock.optiondate7 = null;
        persondecreeblock.optiondate8 = null;
        //this.toDateInputValue(Date.now);
        persondecreeblock.optiondate1String = this.getdate();
        persondecreeblock.optiondate2String = "";
        persondecreeblock.optiondate3String = "";
        persondecreeblock.optiondate4String = "";
        persondecreeblock.optiondate5String = "";
        persondecreeblock.optiondate6String = "";
        persondecreeblock.optiondate7String = "";
        persondecreeblock.optiondate8String = "";
        //persondecreeblock.subvaluenumber1 = null;
        persondecreeblock.subvaluenumber2 = null;
        persondecreeblock.subvaluestring1 = "";
        persondecreeblock.subvaluestring2 = "";

        if (persondecreeblock.person != null && persondecreeblock.person.surname2.length > 0 && persondecreeblock.person.name2.length > 0
            && persondecreeblock.person.fathername2.length > 0) {
            if (persondecreeblock.person.actualRank != null) {
                persondecreeblock.optionstring3 = "рапорт " + persondecreeblock.person.surname2 + " " + persondecreeblock.person.name2[0].toUpperCase() + "." + persondecreeblock.person.fathername2[0].toUpperCase() + ".";
            } else {
                persondecreeblock.optionstring3 = "заявление " + persondecreeblock.person.surname2 + " " + persondecreeblock.person.name2[0].toUpperCase() + "." + persondecreeblock.person.fathername2[0].toUpperCase() + ".";
            }
            
        }
    }

    firerewardchange(persondecreeblock: Persondecreeblock) {
        persondecreeblock.optionstring4 = "";
        persondecreeblock.optionstring5 = "";
        persondecreeblock.optionstring6 = "";
        persondecreeblock.optionstring7 = "";
        persondecreeblock.optionstring8 = "";
        persondecreeblock.optionnumber7 = null;
        persondecreeblock.optionnumber8 = null;
        persondecreeblock.optionnumber10 = null;
        persondecreeblock.optionnumber11 = null;
        persondecreeblock.optiondate5 = null;
        persondecreeblock.optiondate6 = null;
        persondecreeblock.optiondate7 = null;
        persondecreeblock.optiondate8 = null;
    }

    rewardChestsign(reward: Reward): boolean {
        if (reward.rewardtype != 1) {
            return false;
        }
        if (reward.name.startsWith("Нагрудный знак")) {
            return true;
        } else {
            return false;
        }
    }

    rewardMedal(reward: Reward): boolean {
        if (reward.rewardtype != 1) {
            return false;
        }
        if (reward.name.startsWith("Медаль")) {
            return true;
        } else {
            return false;
        }
    }

    beautifyString(text: string): string {
        if (text == null || text.length == 0) {
            return "";
        }
        if (text.length == 1) {
            return text.charAt(0).toLowerCase();
        }
        text = text.trim();
        text = text.charAt(0).toLowerCase() + text.substring(1);

        return text;
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

    fetchStructureElders() {
        fetch('api/DetailedStructure/Elders', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                //alert(JSON.stringify(data));
                this.structuresElders = data;
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

    getRewardmoneytype(rewardmoneyid: number, count: string = "1"): string {
        if (rewardmoneyid == null || rewardmoneyid == 0) {
            return "";
        }
        // В зависимости от того в единственном или множественном числе
        let countNum: number = Number.parseFloat(count);
        let rewardmoney: Rewardmoney = this.rewardmoneys.find(t => t.id == rewardmoneyid);
        if (rewardmoney != null && countNum <= 1) {
            return rewardmoney.rewardmoneytype;
        } else if (rewardmoney != null) {
            return rewardmoney.rewardmoneytypeplural;
        } else {
            return "";
        }
    }

    RewardNotOne(rewardstring: string): boolean {
        if (rewardstring == null) {
            return true;
        }
        if (rewardstring.trim() === "1") {
            return false;
        } else {
            return true;
        }
    }

    getPenalty(penaltyid: number): string {
        if (penaltyid == null || penaltyid == 0) {
            return "";
        }
        let penalty: Penalty = this.penalties.find(t => t.id == penaltyid);
        if (penalty != null) {
            return penalty.name;
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
     * Медаль «160 год пажарнай службе Беларусі» меняет на «160 год пажарнай службе Беларусі» и т.п.
     * @param rewardString
     */
    cutRewardString(rewardString: string): string {
        if (rewardString == null || rewardString.length == 0) {
            return "";
        }
        if (!rewardString.includes("«")) {
            return rewardString;
        }
        let result: string = "«" + rewardString.substr(rewardString.indexOf("«") + 1);
        return result;
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

    getAppointtype(appointtype: number): string {
        if (appointtype == null || appointtype == 0) {
            return "";
        }
        let atype: Appointtype = this.appointtypes.find(at => at.id == appointtype);
        if (atype != null) {
            return atype.name;
        } else {
            return "";
        }
    }

    getFireObject(fire: number): Fire {
        if (fire == null || fire == 0) {
            return null;
        }
        let fireobject: Fire = this.fires.find(f => f.id == fire);
        return fireobject;
    }

    getTransfertypeObject(transfertype: number): Transfertype {
        if (transfertype == null || transfertype == 0) {
            return null;
        }
        let transfertypeobject: Transfertype = this.transfertypes.find(t => t.id == transfertype);
        return transfertypeobject;
    }

    getInterrupttypeObject(interrupttype: number): Interrupttype {
        if (interrupttype == null || interrupttype == 0) {
            return null;
        }
        let interrupttypeobject: Interrupttype = this.interrupttypes.find(i => i.id == interrupttype);
        return interrupttypeobject;
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

    filterRankGain(rank: Rank, person: Person): boolean {
        let allow: boolean = false;
        if (person == null || person.actualRank == null) {
            return true;
        } else {
            if (rank.id <= person.actualRank.id) {
                return false;
            }
            
            // Рядовой и младший начальствующий состав
            if (person.actualRank.positioncategory == 1 || person.actualRank.positioncategory == 2) {
                // Если выше лейтенанта, не отображать
                if (rank.id > 9) {
                    return false;
                }
            // Остальные
            } else {
                // Допускает только на одно звание выше. Но при этом не допускает полковника и генералов, так как их присваивает только президент.
                if (rank.id > person.actualRank.id + 1 || rank.id >= 14 ) {
                    return false;
                }
            }
            return true;
        }
        //return false;
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

    getAllPersondecreeblocksubs(persondecreeblocksubs: number): boolean{
        return true;
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

    jobperiodvacationinitializeAll(persondecreeblock: Persondecreeblock) {
        if (persondecreeblock == null || persondecreeblock.person == null || persondecreeblock.person.jobperiods == null) {
            return;
        }

        let lastJobperiod: Jobperiod = null;

        persondecreeblock.person.jobperiods.forEach(p => {
            let least: number = 2000;
            if (p.vacationdaystransferleft > p.vacationdaysgivenclear) {
                least = p.vacationdaysgivenclear;
            } else {
                least = p.vacationdaystransferleft;
            }
            p.vacationselecteddays = least;
            lastJobperiod = p;
            p.vacationselectedshow = true;
            p.vacationmax = least;
        })

        if (lastJobperiod != null) {
            lastJobperiod.vacationselected = 1;
            lastJobperiod.vacationselectedshow = false;
        }
    }

    jobperiodvacationselect(jobperiod: Jobperiod) {
        jobperiod.vacationselecteddays = jobperiod.vacationdaystransferleft;
    }

    getVacationtype(vacationtype: number): Vacationtype {
        if (vacationtype == null || vacationtype == 0) {
            return null;
        }
        let vtype: Vacationtype = this.vacations.find(p => p.id == vacationtype);
        if (vtype != null) {
            return vtype;
        } else {
            return null;
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

    jobperiodvacationchange(event: Event, jobperiod: Jobperiod, persondecreeblock: Persondecreeblock) {
        let input = event.target as any;

        if (persondecreeblock == null || persondecreeblock.person == null || persondecreeblock.person.jobperiods == null) {
            return;
        }
        let count: number = 0;
        persondecreeblock.person.jobperiods.forEach(p => {

            if (p.vacationselected > 0 && p.vacationselecteddays != null) {
                count = count + Number.parseInt(p.vacationselecteddays.toString());
            }

        })
        if (jobperiod.vacationselecteddays != null && jobperiod.vacationselecteddays > jobperiod.vacationmax) {
            jobperiod.vacationselecteddays = jobperiod.vacationmax;

            input.value = jobperiod.vacationselecteddays;
        } else if (jobperiod.vacationselecteddays != null && jobperiod.vacationselecteddays < 0) {
            jobperiod.vacationselecteddays = 0;
            input.value = 0;
        }

        //persondecreeblock.optionnumber1 = 5;
    }

    getRefVacationinput(index: number): string {
        return "refvacationinput" + index;
    }

    /**
     * Так как мы не можем передать jobperiod'ы в persondecreeoperation, мы превращаем их в строковый эквивалент
     * 
     * @param persondecreeblock
     */
    stringifyJobperiods(persondecreeblock: Persondecreeblock) {
        if (persondecreeblock == null || persondecreeblock.person == null || persondecreeblock.person.jobperiods == null) {
            return;
        }
        let str: string = "";
        persondecreeblock.person.jobperiods.forEach(p => {
            if (p.vacationselected > 0 && p.vacationselecteddays != null && p.vacationselecteddays > 0) {
                //alert(p.start);
                //str += new Date(p.start).getFullYear().toString(); // вначале записываем год.
                str += this.printDate(p.start); // вначале записываем  начало периода.
                str += "+"; // разделитель между периодами.
                str += this.printDate(p.end); //  потом окончание периода.
                str += "%"; // разделитель внутри одного jobperiod
                str += p.vacationselecteddays;
                str += ";";
            }
        })
        if (str.length > 0) {
            str = str.slice(0, -1);
        }
        persondecreeblock.optionstring5 = str;
    }

    /**
     * Здесь генерируем текст на вроде "часть основного отпуска за 2020 год" или "часть основного отпуска за 2019 и 2020 года"
     * или дополнительный день отдыха за ранее отработанное время
     * @param persondecreeoperation
     */
    provideblocksubtext(persondecreeblocksub: Persondecreeblocksub,decreeoperation: Persondecreeblock): string {
        let str: string = "";
        if (persondecreeblocksub.persondecreeblocksubtype > 0) {
            // отпуск
            if (persondecreeblocksub.persondecreeblocksubtype == 11) {
                let vacationtype: Vacationtype = this.getVacationtype(persondecreeblocksub.subvaluenumber1);
                str += vacationtype.name2;
                if (vacationtype.main > 0 && vacationtype.military) {
                    if (persondecreeblocksub.subvaluestring1.length > 0 && persondecreeblocksub.subvaluestring2.length > 0) {
                        str += " за " + this.getPeriodYearFromString(persondecreeblocksub.subvaluestring1) + " и " + this.getPeriodYearFromString(persondecreeblocksub.subvaluestring2) + " года:";
                    } else if (persondecreeblocksub.subvaluestring1.length > 0) {
                        str += " за " + this.getPeriodYearFromString(persondecreeblocksub.subvaluestring1) + " год:";
                    } else {
                        str += ":";
                    }
                }
            // дополнительный день отдыха
            } else if (persondecreeblocksub.persondecreeblocksubtype == 12) {
                str += "дополнительный день отдыха за ранее отработанное время:";
            } else if (persondecreeblocksub.persondecreeblocksubtype == 15){
                str += "дополнительныe дни отдыха за ранее отработанное время:"; 
            }
        }
        return str;
    }


     /**
     * Здесь генерируем текст на вроде "часть основного отпуска за 2020 год" или "часть основного отпуска за 2019 и 2020 года"
     * или дополнительный день отдыха за ранее отработанное время
     * @param persondecreeoperation
     */
    provideSetSubBlockText(persondecreeblocksub: Persondecreeblocksub, decreeoperation: Persondecreeblock) : string {
        let str: string = "";
        if(persondecreeblocksub.persondecreeblocksubtype == 1){
            str +="в соответствии со статьей 43 Закона Республики Беларусь от 14 июня 2003 г. «О государственной службе в Республике Беларусь» стаж государственной службы";
            return str;
        }
        if(persondecreeblocksub.persondecreeblocksubtype == 2){
            str += "с " + this.printDateDocument(decreeoperation.optiondate1) + " (на период действия дисциплинарного взыскания) ежемесячную премию в размере " + decreeoperation.optionnumber2 + " % оклада денежного содержания.";
            return str;
        }
        if(persondecreeblocksub.persondecreeblocksubtype == 3){
            str += "стаж работы "
            return str;
        }
        if(persondecreeblocksub.persondecreeblocksubtype == 4){
            str += "выслуга лет"
            return str;
        }
        if(persondecreeblocksub.persondecreeblocksubtype == 5){
            str += "в соответствии с приказом МЧС Республики Беларусь от 31.01.2012 № 240 дсп «Об утверждении инструкции о порядке и условиях выплаты денежного довольствия лицам рядового и начальствующего состава органов и подразделений по чрезвычайным ситуациям Республики Беларусь» за высокие показатели в учебе по итогам "
            + decreeoperation.optionstring3 + " учебного года повышение должностного оклада с ";
            let count: number;
            return str;
        }
    }
    
    provideEnrollAStudent(persondecreeblocksub: Persondecreeblocksub): string{
        let str = "";
        if(persondecreeblocksub.subvaluestring1 == "Курсант"){
            str += "курсантами " + persondecreeblocksub.subvaluestring2 + "а специальностей " + this.getFullEducationlevel(persondecreeblocksub.subvaluenumber4) + " " + this.getEducationstage(persondecreeblocksub.subvaluenumber1) + " (" + this.getEducationtype(persondecreeblocksub.subvaluenumber2) + " форма обучения) государственного учреждения образования «Университет гражданской защиты Министерства по чрезвычайным ситуациям Республики Беларусь» с " + this.printDateDocument(persondecreeblocksub.subvaluedate2) + " абитуриентов, успешно выдержавших вступительные испытания, профессиональный отбор, прошедших по конкурсу и заключивших контракт о службе, присвоив первое специальное звание « " + this.getRank(persondecreeblocksub.subvaluenumber3) + " »:"
            return str;
        }
        if(persondecreeblocksub.subvaluedate1 != null){
            str += "в соответствии с Правилами приема лиц для получения " + this.getFullEducationlevel(persondecreeblocksub.subvaluenumber1) + " " + this.getEducationstage(persondecreeblocksub.subvaluenumber2) + " , утвержденными Указом Президента Республики Беларусь от 07.02.2006 № 80, Порядком приема в государственное учреждение образования «Университет гражданской защиты Министерства по чрезвычайным ситуациям Республики Беларусь» для получения " + this.getFullEducationlevel(persondecreeblocksub.subvaluenumber1) + " " + this.getEducationstage(persondecreeblocksub.subvaluenumber2) + " в 2020 году, утвержденным 09.09.2019, и на основании решения приемной комиссии университета (протокол заседания от " + this.printDateDocument(persondecreeblocksub.subvaluedate1) + " №" + persondecreeblocksub.subvaluenumber3 + "):"
            return str;
        }    
        return str;
    }

    provideTranslateSubBlockText(persondecreeblocksub: Persondecreeblocksub) : string{
        let str: string = "";
        str += "в соответствии с пунктом 44 Правил проведения аттестации студентов, курсантов, слушателей при освоении содержания образовательных программ высшего образования, утвержденных постановлением Министерства образования Республики Беларусь от 20.05.2012 № 53 по результатам " + persondecreeblocksub.subvaluestring1 + " :"
        return str;
    }

    provideDeductSubBlockText(persondecreeblocksub: Persondecreeblocksub) : string {
        let str: string = "в соответствии с подпунктом ";
        if(persondecreeblocksub){
            if(this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph > 170 && this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph < 180){
                str += this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph + "." + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph + " Положении о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь (" + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].titleofarticles + ") ";
                if(persondecreeblocksub.subvaluenumber1 == 1){
                    str += " и уволить в запас (с постановкой на воинский учёт) из органов подразделений по чрезвычайным ситуациям по подпункту ";
                    if(this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph > 170 && this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph < 180){
                        str += this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + "." + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + " Положении о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь (" + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].titleofarticles + ") ";
                        return str;
                    }else if(this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph > 0 && this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph < 7){
                        str += this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + "." + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + " статьи 79 Кодекса Республики Беларусь об образовании (" + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].titleofarticles + ") ";
                        return str;
                    }else
                    return str;
                }else
                return str;
            } else if(this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph > 0 && this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph < 7){
                str += this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph + "." + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].paragraph + " статьи 79 Кодекса Республики Беларусь об образовании (" + this.dismissalclauses[persondecreeblocksub.persondecreeblocksubtype].titleofarticles + ") ";
                if(persondecreeblocksub.subvaluenumber1 == 1){
                    str += " и уволить в запас (с постановкой на воинский учёт) из органов подразделений по чрезвычайным ситуациям по подпункту ";
                    if(this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph > 170 && this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph < 180){
                        str += this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + "." + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + " Положении о прохождении службы в органах и подразделениях по чрезвычайным ситуациям Республики Беларусь (" + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].titleofarticles + ") ";
                        return str;
                    }else if(this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph > 0 && this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph < 7){
                        str += this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + "." + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].subparagraph + " пункта " + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].paragraph + " статьи 79 Кодекса Республики Беларусь об образовании (" + this.dismissalclauses[persondecreeblocksub.subvaluenumber2].titleofarticles + ") ";
                        return str;
                    }else
                    return str;
                }else
                return str;
            }else
            return str;
        }
    }

    provideUpSubBlockText(persondecreeblocksub: Persondecreeblocksub): string{
        let str = "";
        str += "в соответствии с пунктом "+ persondecreeblocksub.subvaluestring1 +" приказа Министерства по чрезвычайным ситуациям Республики Беларусь «Об оплате труда лиц рядового и начальствующего состава органов и подразделений по чрезвычайным ситуациям Республики Беларусь» от 09.07.2013 № 180 дсп должностной оклад курсантам, признанным в соответствии с законодательством детьми-сиротами, детьми, оставшимися без попечения родителей, а также лицами из числа детей-сирот и детей, оставшихся без попечения родителей, "
        return str;
    }

    provideRestoreSubBlockText(persondecreeblocksub: Persondecreeblocksub): string{
        let str = "";
        str += "в соответствии с пунктом 2 статьи 80 Кодекса Республики Беларусь об образовании на "+ persondecreeblocksub.subvaluestring2 +" по специальности « "+ persondecreeblocksub.subvaluestring1 +" » (на условиях оплаты):"
        return str;
    }
    

    isSocialVacation(persondecreeoperation: Persondecreeoperation): boolean {
        let vacationtype: Vacationtype = this.getVacationtype(persondecreeoperation.subvaluenumber1);
        if (vacationtype == null) {
            return false;
        }
        return vacationtype.social;
    }

    /**
     * Информация в скобочках "(с учетом 1 дня на проезд)", если есть дни на проезд и/или если части отпуска за разные года 
     * @param persondecreeoperation
     */
    vacationanyadditionalinfo(persondecreeoperation: Persondecreeoperation): string {
        let str: string = "";
        let anycondition: boolean = false;
        // взято по 2 части с 2 разных лет
        if (persondecreeoperation.subvaluestring1.length > 0 && persondecreeoperation.subvaluestring2.length > 0) {
            if (!anycondition) {
                str += " (";
            }
            anycondition = true;

            str += persondecreeoperation.optionnumber4 + " календарных дней за " + this.getPeriodYearFromString(persondecreeoperation.subvaluestring1) + " год, ";
            str += persondecreeoperation.optionnumber5 + " календарных дней за " + this.getPeriodYearFromString(persondecreeoperation.subvaluestring2) + " год";


        }

        // есть дни на проезд
        if (persondecreeoperation.optionnumber2 > 0) {
            if (!anycondition) {
                str += " (";
            } else {
                str += ", ";
            }

            str += "с учетом " + persondecreeoperation.optionnumber2 + " " + this.getDayPluralString(persondecreeoperation.optionnumber2) + " на проезд";

            anycondition = true;
        }

        if (anycondition) {
            str += ")";
        }

        return str;
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

    vacationcivilperiodsadditionalinfo(persondecreeoperation: Persondecreeoperation): string {
        let str: string = "";
        let anycondition: boolean = false;
        // взято по 2 части с 2 разных лет
        if (persondecreeoperation.subvaluestring1.length > 0 && persondecreeoperation.subvaluestring2.length > 0) {
            if (!anycondition) {
                str += " (";
            }
            anycondition = true;

            str += persondecreeoperation.optionnumber4 + " календарных дней за период работы с " + this.printDateDocumentFromString(this.getPeriodStartFromString(persondecreeoperation.subvaluestring1))
                + " по " + this.printDateDocumentFromString(this.getPeriodEndFromString(persondecreeoperation.subvaluestring1)) + " и ";
            str += persondecreeoperation.optionnumber5 + " календарных дней за период работы с " + this.printDateDocumentFromString(this.getPeriodStartFromString(persondecreeoperation.subvaluestring2))
                + " по " + this.printDateDocumentFromString(this.getPeriodEndFromString(persondecreeoperation.subvaluestring2));


        } else if (persondecreeoperation.subvaluestring1.length > 0) {
            str += " за период работы с " + this.printDateDocumentFromString(this.getPeriodStartFromString(persondecreeoperation.subvaluestring1))
                + " по " + this.printDateDocumentFromString(this.getPeriodEndFromString(persondecreeoperation.subvaluestring1)) ;
        }

        if (anycondition) {
            str += ")";
        }

        return str;
    }

    vacationcivilholidaysadditionalinfo(persondecreeoperation: Persondecreeoperation): string {
        if (persondecreeoperation.optionnumber8 == null || persondecreeoperation.optionnumber8 == 0) {
            return "";
        }
        let str: string = "(";
        str += "с учетом " + persondecreeoperation.optionnumber8 + " неоплачиваемого праздничного дня";
        str += ")";
        return str;
    }

    vacationtraveltext(persondecreeoperation: Persondecreeoperation): string {
        let str: string = "";
        //if (persondecreeoperation.optionarray1Array.length > 0 || persondecreeoperation.optionstring2.length > 0) {
        //    str += " с выездом в ";
        //    // Если есть страна
        //    if (persondecreeoperation.optionarray1Array.length > 0) {
        //        //let country: Country = this.getCountry(persondecreeoperation.optionnumber3); - старый код, когда отображались единично страны
        //        //str += country.name4;
        //        let muiltiple: boolean = false;

        //        persondecreeoperation.optionarray1Array.forEach(a => {
        //            if (muiltiple) {
        //                str += ", "
        //            }
        //            let country: Country = this.getCountry(a);
        //            str += country.name4;
        //            muiltiple = true;
        //        })
        //    // если страны нет, но есть город, то его в скобки не берем.
        //    } else {
        //        str += persondecreeoperation.optionstring2;
        //    }

        //    // Если есть и страна и город пребывания. Здесь город берем в скобки
        //    if (persondecreeoperation.optionarray1Array.length > 0 && persondecreeoperation.optionstring2.length > 0) {
        //        str += " (" + persondecreeoperation.optionstring2 + ")";
        //    }
        //}

        if (persondecreeoperation.countrycitiesList != null && persondecreeoperation.countrycitiesList.length > 0) {
            let anyCountry: boolean = false;
            let anyCity: boolean = false;
            persondecreeoperation.countrycitiesList.forEach(c => {
                let anyCountryHere: boolean = false;
                let anyCityHere: boolean = false;

                // Уже были страны или города до этого. То есть продолжение списка.
                if (anyCountry || anyCity) {
                    str += ", ";
                }

                // записана ли страна
                if (c.country != null && c.country > 0) {
                    anyCountryHere = true;
                    if (!anyCountry && !anyCity) {
                        
                        str += " с выездом в ";
                    }
                    if (!anyCountry) {
                        anyCountry = true;
                    }

                    let country: Country = this.getCountry(c.country);
                    str += country.name4;
                    
                }
                c.cities.forEach(city => {
                    if (city.length > 0) {
                        
                        if (!anyCountry && !anyCity) {
                            str += " с выездом в ";
                        }
                        if (!anyCity) {
                            anyCity = true;
                        }
                        if (!anyCityHere && anyCountryHere) {
                            str += " (";
                        }
                        if (anyCityHere) {
                            str += ", ";
                        }

                        anyCityHere = true;
                    }
                    str += city;
                })

                if (anyCountryHere && anyCityHere) {
                    str += ")";
                }
            })
        }
        return str;
    }

    countTripfinaldate(persondecreeblock: Persondecreeblock) {
        if (persondecreeblock.optiondate1String == null) {

            return null;
        }
        let dateend: Date = new Date(persondecreeblock.optiondate1String);
        if (persondecreeblock.optionnumber2 == null) {
            persondecreeblock.optionnumber2 = 0;
        }
        let durationlocal: number = persondecreeblock.optionnumber2;
        durationlocal = Number.parseInt(durationlocal.toString());


        let addition: number = durationlocal - 1;
        if (addition < 0) {
            addition = 0;
        }
        dateend.setDate(dateend.getDate() + addition);

        persondecreeblock.optiondate3String = this.toDateInputValue(dateend);
        return this.toDateInputValue(dateend);
    }

    addCity(contrycities: Countycities) {
        contrycities.cities.push(contrycities.citytoadd);
        contrycities.citytoadd = "";
    }

    deleteCity(city: string, countrycities: Countycities) {
        countrycities.cities = countrycities.cities.filter(c => c != city);
    }

    deleteCountrycities(countrycities: Countrycities, persondecreeBlock: Persondecreeblock) {
        persondecreeBlock.countrycitiesList = persondecreeBlock.countrycitiesList.filter(c => c != countrycities);
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

    getVacationtypeObject(vacationtype: number): Vacationtype {
        if (vacationtype == null || vacationtype == 0) {
            return null;
        }
        let element: Vacationtype = this.vacations.find(t => t.id == vacationtype);
        if (element != null) {
            return element;
        } else {
            return null;
        }
    }

    isMaternity(vacationid: number): boolean {
        if (vacationid == null || vacationid == 0) {
            return false;
        }
        let vacationtype: Vacationtype = this.getVacationtypeObject(vacationid);
        if (vacationtype == null) {
            return false;
        }
        if (vacationtype.maternity == 1) {
            return true;
        }
    }

    vacationDateendChange(persondecreeblock: Persondecreeblock) {
        // persondecreeBlock.optiondate1String - начало
        // persondecreeBlock.optiondate3String
        if (persondecreeblock.optiondate3String == null || persondecreeblock.optiondate3String.length == 0 || persondecreeblock.optiondate1String == null || persondecreeblock.optiondate1String.length == 0) {
            return;
        }
        let diff = moment(persondecreeblock.optiondate3String).diff(persondecreeblock.optiondate1String, 'days');
        persondecreeblock.optionnumber1 = diff;
    }

    numpersonalchange(event: Event, numpersonal: string) {
        //alert(numpersonal);
        let input = event.target as any;

        if (numpersonal == null) {
            return;
        }
        numpersonal = numpersonal.toUpperCase();
        if (numpersonal.length == 1) {
            numpersonal += "-";
        }
        if (numpersonal.length == 2) {
            numpersonal = numpersonal.substring(0, 1) + "-";
        }
        if (numpersonal.length > 2) {
            numpersonal = numpersonal.substring(0, 1) + "-" + numpersonal.substring(2);
        }
        if (numpersonal.length > 0 && !this.isLetterCyrillic(numpersonal[0])) {
            numpersonal = numpersonal.substring(0, 0);
        }
        if (numpersonal.length > 2 && !this.isNum(numpersonal[2])) {
            numpersonal = numpersonal.substring(0, 2);
        }
        if (numpersonal.length > 3 && !this.isNum(numpersonal[3])) {
            numpersonal = numpersonal.substring(0, 3);
        }
        if (numpersonal.length > 4 && !this.isNum(numpersonal[4])) {
            numpersonal = numpersonal.substring(0, 4);
        }
        if (numpersonal.length > 5 && !this.isNum(numpersonal[5])) {
            numpersonal = numpersonal.substring(0, 5);
        }
        if (numpersonal.length > 6 && !this.isNum(numpersonal[6])) {
            numpersonal = numpersonal.substring(0, 6);
        }
        input.value = numpersonal;
        //alert(input.value);
        //persondecreeblock.optionnumber1 = 5;
    }
       
    getCourse(semestr: number): number{
        if(semestr > 0 && semestr < 3){
            return 1;
        }else if(semestr < 5){
            return 2;
        }else if(semestr < 7){
            return 3;
        }else if(semestr < 9){
            return 4;
        }
        return 0;
    }

    checkDecreeOperations() {
        console.log(JSON.stringify(<number>(this.decreeId)));
        fetch('/api/DecreeOperations/Checked', {
            method: 'post',
            body: JSON.stringify(<number>(this.decreeId)),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => { return response.json(); })
        .then((response) => {
            (<any>Vue).notify(response);
            this.checkedDecreeResualt = response.split(':')[1];
        });
    }

    special_acces() {
        var actual_user: User = this.$store.state.user;
        if (actual_user.id == 1 || actual_user.id == 49) {
            return true;
        }
        return false;
    }

    unsigned_decree() {
        console.log(this.decreeId);
        fetch('/api/Decrees/unsigned', {
            method: 'post',
            body: JSON.stringify(<number>(this.decreeId)),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.modalDecreeMenuSignedVisible = false;
                this.decreesSignedList = [];
                this.modalDecreesSignedMenuVisible = false;
            });
    }

    changeusereditorflag() {
        console.log(this.value5);
        var user: User = this.$store.state.user;
        user.onlyreadflagtoeditor = this.value5 === true ? 1 : 0
        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(user),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).forceStructureUpdate = true;
                (<any>Vue).notify(response);
            }).then(r => {
                fetch('/api/Users/update', {
                    method: 'post',
                    body: JSON.stringify(user),
                    credentials: 'include',
                    headers: new Headers({
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    })
                }).then(response => { return response.json() as Promise<User> })
                    .then(data => {
                        this.value5 = data.onlyreadflagtoeditor == 1 ? true : false;
                        this.$store.state.setUser(data);
                    })
            });
    }
}