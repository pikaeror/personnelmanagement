import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip, Upload, Autocomplete, Table, TableColumn } from 'element-ui';
import _ from 'lodash';
import '../../css/print.css';
import Decreemanagement from '../../classes/decreemanagement';
import Decreeoperation from '../../classes/decreeoperation';
import Persondecree from '../../classes/persondecree';
import Persondecreeoperation from '../../classes/persondecreeoperation';
import Persondecreeblock from '../../classes/persondecreeblock';
import Persondecreeblocktype from '../../classes/persondecreeblocktype';
import Persondecreeblocksub from '../../classes/persondecreeblocksub';
import Persondecreeblocksubtype from '../../classes/persondecreeblocksubtype';
import Region from '../../classes/region';
import Area from '../../classes/area';
import Structure from '../../classes/structure';
import Rewardmoney from '../../classes/rewardmoney';
import download from 'downloadjs';

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
import Subject from '../../classes/subject'
import Subjectcategory from '../../classes/subjectcategory'
import Subjectgender from '../../classes/subjectgender'
import Countycities from '../../classes/countrycities'
import Countrycities from '../../classes/countrycities';
import Ordernumbertype from '../../classes/ordernumbertype';
import Link from '../../classes/link';
import PosDep from '../positionslist/positionslist';
import Positiontype from '../../classes/positiontype';

Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Dropdown.name, Dropdown);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.component(Popover.name, Popover);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Tooltip.name, Tooltip);
Vue.component(Dialog.name, Dialog);
Vue.component(Upload.name, Upload);
Vue.component(Autocomplete.name, Autocomplete);
Vue.use(Element);

@Component({
    components: {

    }
})
export default class decreeoperationtemplatecreator extends Vue {

    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: null })
    input_decree: Persondecree;

    focused: boolean;

    decreesActionsDisabled: boolean;

    modalDecreesSignedMenuVisible: boolean;
    modalDecreeMenuSignedVisible: boolean;

    modalPersondecreesMenuVisible: boolean;
    modalPersondecreeMenuVisible: boolean;
    currentPersondecreeblock: Persondecreeblock;
    persondecreesList: Persondecree[];
    persondecreeCreateName: string;
    persondecreeOperations: Persondecreeoperation[];
    persondecreeId: number;
    persondecreeNumber: string;
    persondecreeNumbertype: string;
    persondecreeNickname: string;
    persondecreeName: string;
    persondecreeDatecreated: string;
    persondecreeDatesigned: string;
    persondecreeBlocks: Persondecreeblock[];
    persondecreesNewblock: number;
    persondecreeBlocksubs: Persondecreeblocksub[];
    persondecreesNewblocksub: number;
    persondecreesActionmenu: boolean;

    fiosearch: string;
    person: Person;
    personssearch: Person[];
    lastUploadedPhoto: any;
    photos: Personphoto[];
    photosPreview: Personphoto[]; // Если мы что-то ищем в поиске, то заодно будем загружать предварительные фотографии
    photoToCreate: Personphoto;

    lastSearchFio: string;

    personrewardRewardtype: number;
    personrewardReward: number;
    personrewardReason: string;
    personrewardOrder: string;
    personrewardDate: string;

    moveorder: boolean;
    usersearch: string;
    userSelected: User;
    usersSearch: User[];

    structuresReward: Structure[];
    structuresRewardAllowedToSelect: Structure[];
    structuresElders: Structure[];

    personvacationHolidays: number;
    //rewardmoneys: Rewardmoney[];


    persondecreeCreatorObject: User;

    update: boolean;

    data() {
        return {
            input_decree: this.input_decree,

            focused: false,

            decreesActionsDisabled: false,

            modalDecreesSignedMenuVisible: false,
            modalDecreeMenuSignedVisible: false,

            modalPersondecreesMenuVisible: false,
            modalPersondecreeMenuVisible: false,
            currentPersondecreeblock: null,
            persondecreesList: [],
            persondecreeCreateName: "",
            persondecreeOperations: [],
            persondecreeId: 0,
            persondecreeNumber: "",
            persondecreeNumbertype: "",
            persondecreeNickname: "",
            persondecreeName: "",
            persondecreeDatecreated: "",
            persondecreeDatesigned: "",
            persondecreeBlocks: [],
            persondecreesNewblock: null,
            persondecreeBlocksubs: [],
            persondecreesNewblocksub: null,
            persondecreesActionmenu: false,

            fiosearch: "",
            person: null,
            personssearch: [],
            photos: [],
            photosPreview: [],
            photoToCreate: new Personphoto(),

            lastSearchFio: "",

            personrewardRewardtype: null,
            personrewardReward: null,
            personrewardReason: "",
            personrewardOrder: "",
            personrewardDate: "",

            moveorder: false,
            usersearch: "",
            userSelected: null,
            usersSearch: [],

            structuresReward: [],
            structuresRewardAllowedToSelect: [],
            structuresElders: [],

            personvacationHolidays: null,
            //rewardmoneys: [],


            persondecreeCreatorObject: null,

            update: true,
        }
    }

    mounted() {
        if (this.$store.state.eldSelectedperson > 0) {
            //this.$store.commit("setEldSelectedperson", 0);
        }
        //this.input_decree.creatorObject.structureString
        this.persondecreeSelectUpdate(this.input_decree.id);
        //setInterval(this.persondecreeSelectUpdate, 10000);
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

    get chosenPosition(): boolean{
        return this.$store.state.chosenPosition != null;
    }

    /**
     * Visible if button is pressed and mode is not enabled; 
     */
    /*get pmrequestManagingPanelVisible(): boolean {
        return this.modalPmrequestPanelVisible && !this.modeselectstructure;
    }*/

    logout() {
        (<any>Vue).logout();
    }

    togglesidebar() {
        (<any>Vue).sidebar = !(<any>Vue).sidebar;
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value) {
            //this.update = true;
        }
    }

    @Watch('input_decree')
    onInputDecreeChange(value: Persondecree) {
        if (value) {
            this.fetchPersondecreeBlocks(value.id);
        }
    }

    updateMethod() {
        this.update = !this.update;
        this.update = !this.update;
        this.update = !this.update ? true : true;
    }

    fetchPersondecreeBlocks(id: number = -1) {
        if (this.input_decree == null)
            return;
        let value: number = id < 0 ? this.input_decree.id : id;
        //this.update = false;
        fetch('api/Persondecreeblock/' + value, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecreeblock[]>;
            })
            .then(result => {
                //alert(JSON.stringify(result));
                result.forEach(p => {
                    if (p.persondecreeblocksub == 0) {
                        p.persondecreeblocksub = null; // Чтобы ничего не отображало вместо 0
                    }
                    if (p.persondecreeblocktype == 1) { // Наградить
                        p.samplePersonreward = new Personreward();
                    }
                    if (p.optionnumber1 == 0) {
                        p.optionnumber1 = null;
                    }
                    if (p.optionnumber2 == 0) {
                        p.optionnumber2 = null;
                    }
                    if (p.optionnumber3 == 0) {
                        p.optionnumber3 = null;
                    }
                    if (p.optionnumber4 == 0) {
                        p.optionnumber4 = null;
                    }
                    if (p.optionnumber5 == 0) {
                        p.optionnumber5 = null;
                    }
                    if (p.optionnumber6 == 0) {
                        p.optionnumber6 = null;
                    }
                    if (p.optionnumber7 == 0) {
                        p.optionnumber7 = null;
                    }
                    if (p.optionnumber8 == 0) {
                        p.optionnumber8 = null;
                    }
                    if (p.optionnumber9 == 0) {
                        p.optionnumber9 = null;
                    }
                    if (p.optionnumber10 == 0) {
                        p.optionnumber10 = null;
                    }
                    if (p.optionnumber11 == 0) {
                        p.optionnumber11 = null;
                    }

                    if (p.subvaluenumber1 == 0) {
                        p.subvaluenumber1 = null;
                    }

                    if (p.subvaluenumber2 == 0) {
                        p.subvaluenumber2 = null;
                    }

                    p.optiondate1String = this.toDateInputValue(p.optiondate1);
                    p.optiondate2String = this.toDateInputValue(p.optiondate2);
                    p.optiondate3String = this.toDateInputValue(p.optiondate3);
                    p.optiondate4String = this.toDateInputValue(p.optiondate4);
                    p.optiondate5String = this.toDateInputValue(p.optiondate5);
                    p.optiondate6String = this.toDateInputValue(p.optiondate6);
                    p.optiondate7String = this.toDateInputValue(p.optiondate7);
                    p.optiondate8String = this.toDateInputValue(p.optiondate8);

                    p.optionarraypersonArray = this.toArrayNumberInputValue(p.optionarrayperson);
                    p.optionarray1Array = this.toArrayNumberInputValue(p.optionarray1);

                    p.personssearchadditional = true;
                    if (p.optionarraypersonArray.length > 0) {
                        p.personssearchadditional = false;
                    }

                    // Если пункт "предоставить" (для отпуска), то в 6ой строке может содержаться информация о странах, 
                    if (p.persondecreeblocktype == 15) {
                        p.countrycitiesList = new Array();

                        let baseCountrycities: Countrycities = new Countycities();
                        p.countrycitiesList.push(baseCountrycities);

                    }
                })
                if (result.length != 0 || result != null)
                    this.persondecreeBlocks = result;
                //this.updateMethod();
                //this.update = true;
            });
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

    getCreatePerson() {
        return this.input_decree.creatorObject;
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
        //this.input_decree.numbertype
    }

    getPersondecreeblockname(block: Persondecreeblock): string {
        let type: Persondecreeblocktype = this.persondecreeblocktypes.find(p => p.id == block.persondecreeblocktype);
        if (type != null) {
            return type.name;
        } else {
            return "";
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

    getdate(): string {
        return this.$store.state.date;
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

    getStructureName(structureid: number): string {
        if (structureid == null || structureid == 0) {
            return "";
        }
        let structure: Structure = this.structuresReward.find(t => t.id == structureid);
        if (structure != null) {
            return structure.name;
        } else {
            return "";
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

    addNonEld(block: Persondecreeblock) {
        block.nonperson = block.fiosearch;

        block.person = null;
        block.personssearch = [];
        block.fiosearch = "";

        this.addPersonblockelement(block);
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
                this.block_list_ubdate(block);
            })
    }

    multipersonAddAdditional(block: Persondecreeblock) {
        block.personssearchadditional = true;
    }

    multipersonRemove(person: Person, block: Persondecreeblock) {
        block.optionarraypersonArray = block.optionarraypersonArray.filter(p => p != person.id);
        block.optionarraypersonObjects = block.optionarraypersonObjects.filter(p => p.id != person.id);
    }

    multicountryAddAdditional(block: Persondecreeblock) {
        block.countrycitiesList.push(new Countycities());
    }

    addPersonblockelement(persondecreeblock: Persondecreeblock) {
        //this.addPersonreward(persondecreeblock);
        // Поощрить
        if (persondecreeblock.persondecreeblocktype == 1) {
            // Премировать деньгами в размере
            if (persondecreeblock.persondecreeblocksub == 7) {
                if (persondecreeblock.optionnumber2Bool) {
                    persondecreeblock.optionnumber2 = 1;
                } else {
                    persondecreeblock.optionnumber2 = 0;
                }
            }
        }
        // Установить
        if (persondecreeblock.persondecreeblocktype == 10) {
            if (persondecreeblock.persondecreeblocksub == 5) {
                persondecreeblock.optionnumber11 = 0;
            } else
                persondecreeblock.optionnumber11 = 1;
        }
        // Предоставить
        if (persondecreeblock.persondecreeblocktype == 15) {
            this.stringifyJobperiods(persondecreeblock);
        }
        let personid: number = 0;
        if (persondecreeblock.person != null) {
            personid = persondecreeblock.person.id;
        }

        // Предоставить
        if (persondecreeblock.persondecreeblocktype == 15) {
            persondecreeblock.optionstring6 = Countrycities.countrycitiesListToString(persondecreeblock.countrycitiesList);
        }

        // Зачислить
        if (persondecreeblock.persondecreeblocktype == 17) {
            persondecreeblock.optionnumber1 = null;
        }

        if (persondecreeblock.persondecreeblocktype == 18) {
            if (persondecreeblock.checkboxdirect)
                persondecreeblock.optionnumber8 = 1;
            else
                persondecreeblock.optionnumber8 = 0;

            if (persondecreeblock.checkboxdismiss)
                persondecreeblock.optionnumber7 = 1;
            else
                persondecreeblock.optionnumber7 = 0;

            persondecreeblock.optionnumber11 = 1;

        }
        // Увеличить
        if (persondecreeblock.persondecreeblocktype == 19) {
            persondecreeblock.persondecreeblocksubtype = 1;
            persondecreeblock.optionnumber11 = 1;
        }

        let transfer: Persondecreeoperation = <Persondecreeoperation>{
            person: personid,
            persondecree: this.input_decree.id,
            status: 1, // Создание
            personreward: persondecreeblock.samplePersonreward,
            //personreward: { },
            intro: persondecreeblock.intro,
            optionnumber1: this.prepareNumToExport(persondecreeblock.optionnumber1),
            optionnumber2: this.prepareNumToExport(persondecreeblock.optionnumber2),
            optionnumber3: this.prepareNumToExport(persondecreeblock.optionnumber3),
            optionnumber4: this.prepareNumToExport(persondecreeblock.optionnumber4),
            optionnumber5: this.prepareNumToExport(persondecreeblock.optionnumber5),
            optionnumber6: this.prepareNumToExport(persondecreeblock.optionnumber6),
            optionnumber7: this.prepareNumToExport(persondecreeblock.optionnumber7),
            optionnumber8: this.prepareNumToExport(persondecreeblock.optionnumber8),
            optionnumber9: this.prepareNumToExport(persondecreeblock.optionnumber9),
            optionnumber10: this.prepareNumToExport(persondecreeblock.optionnumber10),
            optionnumber11: this.prepareNumToExport(persondecreeblock.optionnumber11),
            optionstring1: persondecreeblock.optionstring1,
            optionstring2: persondecreeblock.optionstring2,
            optionstring3: persondecreeblock.optionstring3,
            optionstring4: persondecreeblock.optionstring4,
            optionstring5: persondecreeblock.optionstring5,
            optionstring6: persondecreeblock.optionstring6,
            optionstring7: persondecreeblock.optionstring7,
            optionstring8: persondecreeblock.optionstring8,
            optiondate1: this.prepareDateToExportNullable(persondecreeblock.optiondate1String),
            optiondate2: this.prepareDateToExportNullable(persondecreeblock.optiondate2String),
            optiondate3: this.prepareDateToExportNullable(persondecreeblock.optiondate3String),
            optiondate4: this.prepareDateToExportNullable(persondecreeblock.optiondate4String),
            optiondate5: this.prepareDateToExportNullable(persondecreeblock.optiondate5String),
            optiondate6: this.prepareDateToExportNullable(persondecreeblock.optiondate6String),
            optiondate7: this.prepareDateToExportNullable(persondecreeblock.optiondate7String),
            optiondate8: this.prepareDateToExportNullable(persondecreeblock.optiondate8String),
            subvaluenumber1: this.prepareNumToExport(persondecreeblock.subvaluenumber1),
            subvaluenumber2: this.prepareNumToExport(persondecreeblock.subvaluenumber2),
            subvaluestring1: persondecreeblock.subvaluestring1,
            subvaluestring2: persondecreeblock.subvaluestring2,
            nonperson: persondecreeblock.nonperson,
            optionarray1: this.prepareArrayNumberToExportNullable(persondecreeblock.optionarray1Array),
            optionarrayperson: this.prepareArrayNumberToExportNullable(persondecreeblock.optionarraypersonArray),

            //subjecttype: 1, // Награды - устарело
            persondecreeblock: persondecreeblock.id,
            persondecreeblocktype: persondecreeblock.persondecreeblocktype,
            persondecreeblocksubtype: persondecreeblock.persondecreeblocksub, // тип поощрения. У нас будет только вид, потому что единовременно может быть только один вид поощрения

            personFromStructure: persondecreeblock.allpersonsintoblock,
        };
        let t = JSON.stringify(transfer);

        fetch('/api/Persondecreeoperation', {
            method: 'post',
            body: JSON.stringify(transfer),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then(resualt => {
                this.rerenderSearch();
                if (persondecreeblock.person != null) {
                    //persondecreeblock.selectPerson(this.person.id, persondecreeblock);
                }

                this.persondecreeSelectUpdate(this.input_decree.id);
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

    persondecreeSelect(event: any, id: number) {
        this.modalPersondecreeMenuVisible = true;
        this.persondecreeSelectUpdate(id);
    }

    persondecreeSelectUpdate(id: number = this.input_decree.id) {
        this.fetchPersondecreeOperations(id);
        this.fetchPersondecreeBlocks(id);
        this.fetchPersondecree(id);
        //this.updateMethod();
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

    fetchPersondecreeOperations(decree: number) {
        //this.update = false;
        fetch('api/Persondecreeoperation/' + decree, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecreeoperation[]>;
            })
            .then(result => {

                /**
                 * Проводим сортировку всех частей по блоку, вводной фабуле, подблоку
                 */
                //alert(JSON.stringify(result));
                // list.sort((a, b) => (a.color > b.color) ? 1 : -1)
                // list.sort((a, b) => (a.color > b.color) ? 1 : (a.color === b.color) ? ((a.size > b.size) ? 1 : -1) : -1 )
                //result.sort((a, b) => (a.persondecreeblock > b.persondecreeblock) ? 1 : (a.persondecreeblock === b.persondecreeblock) ? ((a.intro < b.intro) ? 1 : -1) : -1);
                // : (a.persondecreeblock === b.persondecreeblock) ? ((a.intro > b.intro) ? 1 : -1) - вставляется перед первым : -1
                //result.sort((a, b) => (a.persondecreeblock > b.persondecreeblock) ? 1 : (a.persondecreeblock === b.persondecreeblock) ? ((a.intro < b.intro) ? 1 : (a.intro === b.intro) ? ((a.persondecreeblocksubtype > b.persondecreeblocksubtype) ? 1 : -1) : -1) : -1);
                result.sort((a, b) => (a.persondecreeblock > b.persondecreeblock) ? 1 : (a.persondecreeblock === b.persondecreeblock) ? ((a.intro < b.intro) ? 1 : (a.intro === b.intro) ? ((a.persondecreeblocksubtype > b.persondecreeblocksubtype) ? 1 : (a.persondecreeblocksubtype === b.persondecreeblocksubtype) ? ((a.optionnumber1 > b.optionnumber1) ? 1 : -1) : -1) : -1) : -1);

                let operationPrev: Persondecreeoperation = null;
                let intronumPrev: number = 0;
                let persondecreeblocksubtypePrev: number = 0;
                let persondecreeblockoptionnumber1Prev: number = 0;
                result.forEach(operation => {
                    // Если мы используем булевские типы вместо чисел
                    if (operation.optionnumber1 > 0) {
                        operation.optionnumber1Bool = true;
                    } else {
                        operation.optionnumber1Bool = false;
                    }
                    if (operation.optionnumber2 > 0) {
                        operation.optionnumber2Bool = true;
                    } else {
                        operation.optionnumber2Bool = false;
                    }
                    if (operation.optionnumber3 > 0) {
                        operation.optionnumber3Bool = true;
                    } else {
                        operation.optionnumber3Bool = false;
                    }
                    if (operation.optionnumber4 > 0) {
                        operation.optionnumber4Bool = true;
                    } else {
                        operation.optionnumber4Bool = false;
                    }
                    if (operation.optionnumber5 > 0) {
                        operation.optionnumber5Bool = true;
                    } else {
                        operation.optionnumber5Bool = false;
                    }

                    operation.optionarraypersonArray = this.toArrayNumberInputValue(operation.optionarrayperson);
                    operation.optionarray1Array = this.toArrayNumberInputValue(operation.optionarray1);

                    // Если пункт "предоставить" (для отпуска), то в 6ой строке может содержаться информация о странах, 
                    if (operation.persondecreeblocktype == 15 && operation.optionstring6.length > 0) {
                        operation.countrycitiesList = Countrycities.stringToCountrycitiesList(operation.optionstring6);
                    }

                    /**
                     * Здесь проводится сравнение, какие части текущей части и предыдущей совпадают. Например, чтобы если и текущему сотруднику и предыдущему выдается награда по идентичному пункту, 
                     * оно писало "присвоить очередное специальное звание на одну ступень выше" лишь один раз.
                     */
                    if (operationPrev != null) {
                        let newIntro: boolean = false;
                        let newSubtype: boolean = false;

                        // Вводные фабулы не совпадают или вводная фабула отсутствует
                        if (operation.intro.length == 0 || !(operationPrev.intro === operation.intro)) {
                            //if (!(operationPrev.intro === operation.intro)) {
                            intronumPrev += 1;
                            operation.intronum = intronumPrev;


                            if (operation.intro.length != 0 || operationPrev.intro.length != 0) {
                                persondecreeblocksubtypePrev = 1;
                                persondecreeblockoptionnumber1Prev = 1;
                                newIntro = true;
                            }

                        }
                        if (operation.persondecreeblocksubtype != operationPrev.persondecreeblocksubtype || newIntro) {
                            persondecreeblocksubtypePrev += 1;
                            operation.persondecreeblocksubtypenum = persondecreeblocksubtypePrev;

                        }
                        //if (operation.persondecreeoptionnumber1num != operationPrev.persondecreeoptionnumber1) {
                        if (operation.optionnumber1 != operationPrev.optionnumber1 || newIntro) {
                            persondecreeblockoptionnumber1Prev += 1;
                            operation.persondecreeoptionnumber1num = persondecreeblockoptionnumber1Prev;
                        } else {

                        }
                    } else {
                        intronumPrev = 1;
                        persondecreeblocksubtypePrev = 1;
                        persondecreeblockoptionnumber1Prev = 1;
                        operation.intronum = intronumPrev;
                        operation.persondecreeblocksubtypenum = persondecreeblocksubtypePrev;
                        operation.persondecreeoptionnumber1num = persondecreeblockoptionnumber1Prev;
                    }
                    //alert(operation.persondecreeoptionnumber1num + " "+ persondecreeblockoptionnumber1Prev);
                    operationPrev = operation;
                })

                this.persondecreeOperations = result;
                //this.updateMethod();
                //this.update = true;
            });
    }

    fetchPersondecree(id: number) {
        //this.update = false;
        fetch('api/Persondecree/' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecree>;
            })
            .then(result => {
                this.input_decree.datecreated = result.datecreated;
                if (result.datesigned != null) {
                    this.input_decree.datesigned = result.datesigned;
                } else {
                    this.input_decree.datesigned = new Date();
                }

                this.input_decree.name = result.name;
                this.input_decree.nickname = result.nickname;
                this.input_decree.number = result.number;
                this.input_decree.numbertype = result.numbertype;
                this.input_decree.id = id;
                //alert(result.creatorObject);
                this.input_decree.creatorObject = result.creatorObject;

                this.input_decree = result;
                //this.updateMethod();
                //this.update = true;
            });
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

    hasPhotopreviewBlock(personid: number, block: Persondecreeblock): boolean {
        if (block.photosPreview == null || block.photosPreview.length == 0) {
            return false;
        }
        if (block.photosPreview.find(p => p.person == personid)) {
            return true;
        }
        return false;
    }

    getPhotopreviewBlock(personid: number, block: Persondecreeblock): Personphoto {
        return block.photosPreview.find(p => p.person == personid);
    }

    hasSearchResults(): boolean {
        if (this.personssearch != null && this.personssearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    hasSearchResultsBlock(block: Persondecreeblock): boolean {
        if (block.personssearch != null && block.personssearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    selectPersonBlock(person: Person, block: Persondecreeblock) {
        if (person != null) {
            block.personssearch = [];
            block.fiosearch = "";
            block.nonperson = ""; // Если был человек не из МЧС, убираем.
            //this.prepareToImport(person);
            if (block.allpersonsintoblock == null || block.allpersonsintoblock == [])
                block.allpersonsintoblock = [person];
            else {
                if (block.allpersonsintoblock.find(r => r.id == person.id) == null)
                    block.allpersonsintoblock.push(person);
            }
            block.person = person;

            /*block.personssearch = [];
            block.fiosearch = "";
            block.nonperson = ""; // Если был человек не из МЧС, убираем.*/

            // Если присвоить
            if (block.persondecreeblocktype == 14) {
                block.persondecreeblocksub = null; // Обнуляем список доступных званий для выбора
            }
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

    selectPerson(id: number) {
        fetch('api/Person/Single' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person>;
            })
            .then(person => {
                if (person != null) {
                    this.prepareToImport(person);
                    this.person = person;
                    this.getPhotos();
                    this.photoToCreate = new Personphoto();

                    this.personssearch = [];
                    //alert(JSON.stringify(person));
                }
            })
    }

    removePersondecreeoperation(persondecreeoperation: Persondecreeoperation) {
        fetch('/api/Persondecreeoperation', {
            method: 'post',
            body: JSON.stringify(<Persondecreeoperation>{
                id: persondecreeoperation.id,
                status: 2, // Удалить операцию
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
            .then(x => {
                this.rerenderSearch();
                if (this.person != null) {
                    this.selectPerson(this.person.id);
                }
                this.persondecreeSelectUpdate(this.input_decree.id);

            });
    }

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

    getPhotos() {
        fetch('api/Person/Media' + this.person.id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Personphoto[]>;
            })
            .then(personphotos => {
                if (personphotos != null) {

                    personphotos.forEach(p => p.photo64 = p.photo64header + "," + p.photo64);
                    this.photos = personphotos;
                    //this.person = person;
                    //alert(JSON.stringify(person));
                }
            })
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

    selectUser(id: number) {
        if (this.input_decree == null) {
            return; // Нету проекта приказа
        }
        //this.disableDecrees();
        fetch('/api/Persondecree', {
            method: 'post',
            body: JSON.stringify(<Persondecree>{
                id: this.input_decree.id,
                persondecreeManagementStatus: 7, // Перенаправляем проект приказа на другого кадровика
                owner: id,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {
            this.modalPersondecreeMenuVisible = false;
            this.persondecreeId = 0;
            this.fetchPersondecreesActive();
            this.usersSearch = [];
            (<any>Vue).notify("S:Проект приказа направлен на кадровика");
        })
        this.$store.commit("setdecreeoperationtemplatecreatorVisible", false);
        this.visible = false;
    }

    closeUserSearch() {
        this.usersSearch = [];
    }

    fetchPersondecreesActive() {
        fetch('api/Persondecree/Active', { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Persondecree[]>;
            })
            .then(result => {
                //alert('mark');
                result.forEach(r => {
                    if (this.persondecreesList != null) {
                        let preloadedPersondecree: Persondecree = this.persondecreesList.find(p => p.id == r.id);
                        if (preloadedPersondecree != null) {
                            r.marked = preloadedPersondecree.marked;
                        } else {
                            r.marked = false;
                        }
                        //r.marked =p
                    } else {
                        r.marked = false;
                    }
                    this.persondecreesActionmenuCheck();
                });
                this.persondecreesList = result.reverse();
                //(<any>this.$refs.inputdecreecreate).focus();
            });
    }

    persondecreesActionmenuCheck() {
        if (this.persondecreesList == null) {
            this.persondecreesActionmenu = false;
        }
        let anyMarked: boolean = false;
        this.persondecreesList.forEach(p => {
            if (p.marked) {
                this.persondecreesActionmenu = true;
                anyMarked = true;
                return;
            }
        })
        this.persondecreesActionmenu = anyMarked;
        //this.persondecreesActionmenu = false;
    }

    hasUserSearchResults(): boolean {
        if (this.usersSearch != null && this.usersSearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    addPersondecreeblock() {
        fetch('/api/Persondecreeblock', {
            method: 'post',
            body: JSON.stringify(<Persondecreeblock>{
                persondecree: this.input_decree.id,
                status: 1, // Создание
                persondecreeblocktype: this.persondecreesNewblock,

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
            .then(x => {
                this.rerenderSearch();
                if (this.person != null) {
                    this.selectPerson(this.person.id);
                }
                this.persondecreeSelectUpdate(this.input_decree.id);
                this.fetchPersondecreeBlocks();
                /*this.updateMethod();*/
            });
        
    }

    deletePersondecreeblock(block: Persondecreeblock) {
        if (!confirm("Вы уверены?")) {
            return;
        }
        fetch('/api/Persondecreeblock', {
            method: 'post',
            body: JSON.stringify(<Persondecreeblock>{
                id: block.id,
                status: 2, // Удалить

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
            .then(x => {
                this.rerenderSearch();
                if (this.person != null) {
                    this.selectPerson(this.person.id);
                }
                this.persondecreeSelectUpdate(this.input_decree.id);
                /*this.fetchPersondecreeBlocks();
                this.updateMethod();*/
            });
        
    }

    persondecreeUpdate() {
        let csharpDateCreated: Date = new Date(this.persondecreeDatecreated);
        let csharpDateSigned: Date = new Date(this.persondecreeDatesigned);
        let decree = this.input_decree;
        decree.persondecreeManagementStatus = 5;
        fetch('/api/Persondecree', {
            method: 'post',
            body: JSON.stringify(decree),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => {
            this.fetchPersondecree(decree.id);
            (<any>Vue).notify("S:Данные проекта приказа зарезервированы");
        })
    }

    selectPersonBlockNonAuto(person: Person, block: Persondecreeblock) {
        /*fetch('api/Person/Single' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person>;
            })
            .then(person => {*/
                if (person != null) {
                    this.prepareToImport(person);
                    block.person = person;

                    block.personssearch = [];
                    block.fiosearch = "";
                    block.nonperson = ""; // Если был человек не из МЧС, убираем.

                    // Если присвоить
                    if (block.persondecreeblocktype == 14) {
                        block.persondecreeblocksub = null; // Обнуляем список доступных званий для выбора
                    }

                    // Если предоставить (отпуск)
                    if (block.persondecreeblocktype == 15) {
                        this.persondecreeblocksubChange(block);
                        this.jobperiodvacationinitializeAll(block);
                    }

                    //this.addPersonblockelement(block); - отрубаем автоматическое дополнение. Но тогда для добавления отдельно должна быть кнопка.
                    this.persondecreeSelectUpdate(this.input_decree.id);
                    this.block_list_ubdate(block);
                }
                //this.block_list_ubdate(block);
            //})
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

    block_list_ubdate(block: Persondecreeblock) {
        let new_blocks = [];
        let index = 0
        this.persondecreeBlocks.forEach(r => {
            if (r.id == block.id) {
                r = block;
                return;
                //new_blocks.push(block);
            } else {
                new_blocks.push(r);
            }
            index += 1;
        })
        this.persondecreeBlocks[index] = block;
        this.updateMethod();
        //this.persondecreeBlocks = new_blocks;
    }

    /**
     * Активация режима выбора должности
     * @param persondecreeblock
     */
    selectPosition(persondecreeblock: Persondecreeblock) {


        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("setdecreeoperationtemplatecreatorVisible", false);
        this.$store.commit("setdecreeoperationelementVisible", false);
        this.$store.commit("setmailmodeprevios", true);

        //this.visible = false;
        this.modalPersondecreeMenuVisible = false;
        this.modalPersondecreesMenuVisible = false;
        this.$store.commit("setModeappointpersondecree", true);
        this.currentPersondecreeblock = persondecreeblock;

        this.$store.commit("updateUserAppearance", appearance);
    }

    setPositionByBlock(persondecreeblock: Persondecreeblock) {
        fetch('api/Positions/Solo' + this.$store.state.chosenPosition.id, { credentials: 'include' })
            .then(response => response.json() as Promise<Position>)
            .then(data => {
                fetch('api/Positiontype/Current/' + data.positiontype, { credentials: 'include' })
                    .then(response => response.json() as Promise<Positiontype>)
                    .then(data => {
                        persondecreeblock.samplePositiontype = data;
                    })
                persondecreeblock.samplePosition = data;
            })
        //persondecreeblock.samplePosition
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
}