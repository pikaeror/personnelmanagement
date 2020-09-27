
import './css/styles.css';
import 'bootstrap';
//import Vue from 'vue';
import Element from 'element-ui';
import { Notification } from 'element-ui';
declare var require: any;
var Vue = require("vue").default;
import Vuex from 'vuex';
import Rank from './classes/rank';
import Sourceoffinancing from './classes/sourceoffinancing';
import Positiontype from './classes/positiontype';
import Positioncategory from './classes/positioncategory';
import Mrd from './classes/mrd';
import Altrank from './classes/altrank';
import Altrankcondition from './classes/altrankcondition';
import Altrankconditiongroup from './classes/altrankconditiongroup';
import Structureregion from './classes/structureregion';
import Structuretype from './classes/structuretype';
import Usersettings from './classes/usersettings';
import DlgDraggable from "vue-element-dialog-draggable";
import Relativetype from './classes/relativetype';
import Attestationtype from './classes/attestationtype';
import Vacationmilitary from './classes/vacationmilitary';
import Vacationtype from './classes/vacationtype';
import Languagetype from './classes/languagetype';
import Languageskill from './classes/languageskill';
import Jobtype from './classes/jobtype';
import Servicetype from './classes/servicetype';
import Servicefeature from './classes/servicefeature';
import Servicecoef from './classes/servicecoef';
import Penalty from './classes/penalty';
import Country from './classes/country';
import Science from './classes/science';
import Illcode from './classes/illcode';
import Illregime from './classes/illregime';
import Rewardtype from './classes/rewardtype';
import Reward from './classes/reward';
import Educationlevel from './classes/educationlevel';
import Educationtype from './classes/educationtype';
import Educationdocument from './classes/educationdocument';
import Normativ from './classes/normativ';
import Drivertype from './classes/drivertype';
import Drivercategory from './classes/drivercategory';
import Permissiontype from './classes/permissiontype';
import Prooftype from './classes/prooftype';
import Holiday from './classes/holiday';
import Persondecreeblocktype from './classes/persondecreeblocktype';
import Persondecreeblocksubtype from './classes/persondecreeblocksubtype';
import Region from './classes/region';
import Area from './classes/area';
import Fire from './classes/fire';
import Appointtype from './classes/appointtype';
import Transfertype from './classes/transfertype';
import Subject from './classes/subject';
import Subjectgender from './classes/subjectgender';
import Subjectcategory from './classes/subjectcategory';
import Interrupttype from './classes/interrupttype';
import Changedocumentstype from './classes/changedocumentstype';
import Setpersondatatype from './classes/setpersondatatype';
import Rewardmoney from './classes/rewardmoney';
import Persondecreelevel from './classes/persondecreelevel';
import Ordernumbertype from './classes/ordernumbertype';
import Streettype from './classes/streettype';
import Citytype from './classes/citytype';
import Areaother from './classes/areaother';
import Externalorderwhotype from './classes/externalorderwhotype';
import Persondecreetype from './classes/persondecreetype';
import Educationadditionaltype from './classes/educationadditionaltype';
import Citysubstate from './classes/citysubstate';
import Educationstage from './classes/educationstage';
import Educationpositiontype from './classes/educationpositiontype';
import Role from './classes/role';
import User from './classes/user';

import VueRouter from 'vue-router';
Vue.use(VueRouter);
Vue.use(Vuex);
Vue.use(DlgDraggable);


/**
 * Keys
 */
Vue.keys = new Object();
Vue.keys["COOKIES_SESSION"] = "sessionid";
Vue.keys["IDENTITY_LOGIN_KEY"] = "ln";
Vue.keys["IDENTITY_LOGINED_KEY"] = "logined";
Vue.keys["IDENTITY_STRUCTURE_KEY"] = "structure";
Vue.keys["IDENTITY_LOGINED_TRUE"] = "logtr";
Vue.keys["IDENTITY_LOGINED_FALSE"] = "logfa";
Vue.keys["STATUS_DELETE"] = "DELETE";
Vue.keys["STATUS_NULLIFYPASS"] = "NULLIFYPASS";
Vue.keys["IDENTITY_MASTERPERSONNELEDITOR_KEY"] = "mpe";
Vue.keys["IDENTITY_STRUCTUREEDITOR_KEY"] = "se";
Vue.keys["IDENTITY_STRUCTUREREAD_KEY"] = "sr";
Vue.keys["IDENTITY_PERSONNELEDITOR_KEY"] = "pe";
Vue.keys["IDENTITY_PERSONNELREAD_KEY"] = "pr";
Vue.keys["IDENTITY_DECREE"] = "decree";
Vue.keys["IDENTITY_DECREE_NAME"] = "decreename";
Vue.keys["IDENTITY_POSITIONCOMPACT"] = "positioncompact";
Vue.keys["IDENTITY_DATE_KEY"] = "date";
Vue.keys["IDENTITY_MODE_KEY"] = "mode";
Vue.keys["IDENTITY_ADMIN_KEY"] = "admin";
Vue.keys["IDENTITY_SIDEBAR_DISPLAY_KEY"] = "sidebardisplay";
Vue.keys["IDENTITY_CURRENTSTRUCTURETREE_KEY"] = "cst"
Vue.keys["IDENTITY_FULLMODE_KEY"] = "fullmode";

Vue.forceStructureUpdate = false;


Vue.sidebar = true;



/**
 * Global methods
 */
Vue.redirectToLoginPage = function () {
    window.location.pathname = "/login";
}

Vue.redirectBack = function (url?: string) {
    window.location.pathname = "/";
}

/**
 * Get key - value data about user rights from the back-end.
 */
Vue.getAccessStatus = async function (): Promise<string[]> {
    let access: string[];
    await fetch('api/Identity', { credentials: 'include' })
        .then(response => {
            return response.json() as Promise<Array<string>>;
        })
        .then(result => {
            access = result;
            //alert(JSON.stringify(result));
        })
    return access;
}

/**
 * Обновленный метод getAccessStatus - возвращает сразу пользователя
 */
Vue.getUserStatus = async function (): Promise<User> {
    let user: User;
    await fetch('api/Identity/User', { credentials: 'include' })
        .then(response => {
            return response.json() as Promise<User>;
        })
        .then(result => {
            user = result;
            //alert(JSON.stringify(result));
        })
    return user;
}

Vue.logout = function () {
    fetch('api/Logout', { credentials: 'include' }).then(
        r => { (<any>Vue).redirectToLoginPage(); }
    )
}

/**
 * Checks if user is logined, do nothing. If not logined — redirect to login page.
 */
Vue.requireLogin = function (accessStatus: string[]) {
    
    if (accessStatus[(<any>Vue).keys["IDENTITY_LOGINED_KEY"]] != (<any>Vue).keys["IDENTITY_LOGINED_TRUE"]) {
        (<any>Vue).redirectToLoginPage();
    }

}

/**
 * Checks if user is logined, do nothing. If not logined — redirect to login page.
 */
Vue.requireLoginNew = function () {
    //alert(this.$store);
    if (this.$store != null && this.$store.state.login != (<any>Vue).keys["IDENTITY_LOGINED_TRUE"]) {
        (<any>Vue).redirectToLoginPage();
    }

}

/**
 * Displays success/fail/info notification.
 */
Vue.notify = function (message: string, offset?:number) {
    let messageArray: string[] = message.split(":");
    if (offset == null) {
        offset = 80;
    }
    if (messageArray.length > 1) {
        if (messageArray[0] == "S") {
            (<any>Vue).notifySuccess(messageArray[1], offset);
        } else if (messageArray[0] == "E") {
            (<any>Vue).notifyError(messageArray[1], offset);
        }
    } else {
        (<any>Vue).notifySuccess(message, offset);
    }
}

Vue.notifySuccess = function (message: string, offset?: number) {
    Notification.success({ title: "Действие выполнено", message: message, offset: offset });
}

Vue.notifyError = function (message: string, offset?: number) {
    Notification.error({ title: "Ошибка", message: message, offset: offset });
}

/**
 * Returns complete list of structures.
 */ 
Vue.getStructureAll = async function (): Promise<string[]> {
    let access: string[];
    await fetch('api/Structure', { credentials: 'include' })
        .then(response => {
            return response.json() as Promise<Array<string>>;
        })
        .then(result => {
            access = result;
        })
    return access;
}

Vue.toDateInputValue = function (date: Date): string {
    var local = new Date(date);
    local.setMinutes(date.getMinutes() - date.getTimezoneOffset());
    return local.toJSON().slice(0, 10);
}


Vue.getKeyByValue = function (data: any, value: string): string {
    for (var key in data) {
        if (data[key] == value) {
            return key;
        }
    }
    return null;
}

const routes = [
    { path: '/login', component: require('./components/login/login.vue.html') },
    { path: '/', component: require('./components/home/home.vue.html') },
];


// this.$store.state.departmentsListId; - Getter
// this.$store.state.parentStructures
// this.$store.commit("setDepartmentsListId", id); - Setter

const store = new Vuex.Store({
    state: {
        departmentsListId: 0,
        departmentsListTitle: "Список отделов",
        forceDepartmentUpdate: false,
        positionsListId: 0,
        decreeoperationelementVisible: 0,
        decreeoperationtemplatecreatorVisible: false,
        persondecree: null,
        eldVisible: 0,
        eldId: 0,
        eldPosition: 0,
        eldStructure: 0,
        eldAction: 0,
        eldSelectedperson: 0,
        candidatesVisible: 0,
        candidatesId: 0,
        candidatesPosition: 0,
        candidatesStructure: 0,
        candidatesAction: 0,
        candidatesSelectedperson: 0,
        modepanelVisible: 1,
        positionsListTitle: "Должности",
        forcePositionUpdate: false,
        login: "",
        currentstructuretree: "",
        masterpersonneleditorAccess: "0",
        userStructure: "0",
        structureeditorAccess: "0",
        structurereadAccess: "0",
        personneleditorAccess: "0",
        personnelreadAccess: "0",
        mode: "0",
        fullmode: "0",
        admin: "0",
        parentStructures: [0],
        date: Vue.toDateInputValue(new Date()),
        ranks: Rank[""],
        sofs: Sourceoffinancing[""],
        positiontypes: Positiontype[""],
        positioncategories: Positioncategory[""],
        mrds: Mrd[""],
        altrankconditiongroups: Altrankconditiongroup[""],
        altrankconditions: Altrankcondition[""],
        decree: null,
        decreeName: "",
        positioncompact: "0",
        sidebarDisplay: "0",
        structureregions: Structureregion[""],
        structuretypes: Structuretype[""],
        grandparent: "",
        featured: 0, // If selected featured structure then it stores its id, if no then 0 
        sidebarParentOpen: 0,
        user: User,
        relativetypes: Relativetype[""],
        attestationtypes: Attestationtype[""],
        vacationmilitaries: Vacationmilitary[""],
        vacationtypes: Vacationtype[""],
        languagetypes: Languagetype[""],
        languageskills: Languageskill[""],
        jobtypes: Jobtype[""],
        servicetypes: Servicetype[""],
        servicefeatures: Servicefeature[""],
        servicecoefs: Servicecoef[""],
        penalties: Penalty[""],
        countries: Country[""],
        sciences: [{ id: 1, name: "Научный труд" }, { id: 2, name: "Звание" }, { id: 3, name: "Изобретение" }],
        illregimes: Illregime[""],
        illcodes: Illcode[""],
        rewardtypes: Rewardtype[""],
        rewards: Reward[""],
        educationlevels: Educationlevel[""],
        educationtypes: Educationtype[""],
        educationdocuments: Educationdocument[""],
        normativs: Normativ[""],
        drivertypes: Drivertype[""],
        drivercategories: Drivercategory[""],
        permissiontypes: Permissiontype[""],
        prooftypes: Prooftype[""],
        holidays: Holiday[""],
        persondecreeblocktypes: Persondecreeblocktype[""],
        persondecreeblocksubtypes: Persondecreeblocksubtype[""],
        regions: Region[""],
        areas: Area[""],
        fires: Fire[""],
        appointtypes: Appointtype[""],
        transfertypes: Transfertype[""],
        subjects: Subject[""],
        subjectgenders: Subjectgender[""],
        subjectcategories: Subjectcategory[""],
        interrupttypes: Interrupttype[""],
        changedocumentstypes: Changedocumentstype[""],
        setpersondatatypes: Setpersondatatype[""],
        rewardmoneys: Rewardmoney[""],
        persondecreelevels: Persondecreelevel[""],
        ordernumbertypes: Ordernumbertype[""],
        structuresalldocument: String[""],
        streettypes: Streettype[""],
        citytypes: Citytype[""],
        areaothers: Areaother[""],
        externalorderwhotypes: Externalorderwhotype[""],
        persondecreetypes: Persondecreetype[""],
        educationadditionaltypes: Educationadditionaltype[""],
        citysubstates: Citysubstate[""],
        educationstages: Educationstage[""],
        educationpositiontypes: Educationpositiontype[""],
        roles: Role[""],

        /**
         * Modes
         */
        modeselectcuration: false,
        modeselectedcuration: 0,
        modeselectheading: false,
        modeselectedheading: 0,
        modeselectstructure: false,
        modeselectedstructure: 0,
        modecopy: false,
        modeappointperson: false, // Режим назначения на должность для элд
        modeappointedperson: 0,
        modeappointpersondecree: false, // Режим назначения на должность для приказа
        modeappointedpersondecree: 0,
        modeappointpersonstructuredecree: false, // Режим прикомандирования к подразделению/начальнику подразделения для приказа
        modeappointedpersonstructuredecree: 0,
        modecopystring: "", // String with copy data. "s=1" means copy structure with id 1 and all substructrures with the same or null type and all positions inclusive.

    },
    mutations: {
        setdecreeoperationelementVisible(state, n) {
            state.decreeoperationelementVisible = n;
        },

        setdecreeoperationtemplatecreatorVisible(state, n) {
            state.decreeoperationtemplatecreatorVisible = n;
        },

        setpersondecree(state, n) {
            state.persondecree = n;
        },

        setEldId(state, n) {
            state.eldId = n;

        },
        setEldVisible(state, n) {
            state.eldVisible = n;

        },
        setEldPosition(state, n) {
            state.eldPosition = n;

        },
        setEldStructure(state, n) {
            state.eldStructure = n;

        },
        setEldAction(state, n) {
            state.eldAction = n;

        },
        setEldSelectedperson(state, n) {
            state.eldSelectedperson = n;

        },
        setCandidatesId(state, n) {
            state.candidatesId = n;

        },
        setCandidatesVisible(state, n) {
            state.candidatesVisible = n;

        },
        setCandidatesPosition(state, n) {
            state.candidatesPosition = n;

        },
        setCandidatesStructure(state, n) {
            state.candidatesStructure = n;

        },
        setCandidatesAction(state, n) {
            state.candidatesAction = n;

        },
        setCandidatesSelectedperson(state, n) {
            state.candidatesSelectedperson = n;

        },
        setModepanelVisible(state, n) {
            state.modepanelVisible = n;

        },
        setDepartmentsListId(state, n)
        {
            state.departmentsListId = n;
            state.positionsListId = 0;

        },
        setDepartmentsListTitle(state, n) {
            state.departmentsListTitle = n;
        },
        setForceDepartmentUpdate(state, n) {
            state.forceDepartmentUpdate = n;
            if (n) {
                state.forcePositionUpdate = n;
            }
        },
        setSidebarParentOpen(state, n) {
            state.sidebarParentOpen = n;
        },
        setUser(state, n) {
            state.user = n;
        },
        setPositionsListId(state, n) {
            state.positionsListId = n;
        },
        setPositionsListTitle(state, n) {
            state.positionsListTitle = n;
        },
        setGrandparent(state, n) {
            state.grandparent = n;
        },
        setForcePositionUpdate(state, n) {
            state.forcePositionUpdate = n;
        },
        setLogin(state, n) {
            state.login = n;
        },
        setCurrentStructureTree(state, n) {
            state.currentstructuretree = n;
        },
        setUserStructure(state, n) {
            state.userStructure = n;
        },
        setMasterpersonneleditorAccess(state, n) {
            state.masterpersonneleditorAccess = n;
        },
        setStructureeditorAccess(state, n) {
            state.structureeditorAccess = n;
        },
        setStructurereadAccess(state, n) {
            state.structurereadAccess = n;
        },
        setPersonneleditorAccess(state, n) {
            state.personneleditorAccess = n;
        },
        setPersonnelreadAccess(state, n) {
            state.personnelreadAccess = n;
        },
        setMode(state, n) {
            state.mode = n;
        },
        setFullmode(state, n) {
            state.fullmode = n;
        },
        setAdmin(state, n) {
            state.admin = n;
        },
        setParentStructures(state, n) {
            state.parentStructures = n;
        },
        addParentStructure(state, n) {
            state.parentStructures.push(n);
        },
        removeParentStructure(state, n) {
            var index = state.parentStructures.indexOf(n);
            state.parentStructures.splice(index, 1);
        },
        setDateFromDB(state, n) {
            state.date = n;
        },

        setDateByDate(state, n) {
            //state.date = this.toDateInputValue(n);

            fetch('/api/Identity', {
                method: 'post',
                body: JSON.stringify(<Usersettings>{
                    dateValue: new Date(this.toDateInputValue(n)), updateDate: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            });
        },
        setDateByInput(state, n) {
            //state.date = n;

            fetch('/api/Identity', {
                method: 'post',
                body: JSON.stringify(<Usersettings>{
                    dateValue: new Date(n), updateDate: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            });
        },
        updateRanks(state) {
            fetch('api/Ranks', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Rank[]>;
                })
                .then(result => {
                    state.ranks = result;
                })
        },
        updateSofs(state) {
            fetch('api/SourceOfFinancing', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Sourceoffinancing[]>;
                })
                .then(result => {
                    state.sofs = result;
                })
        },
        updatePositiontypes(state) {
            fetch('api/Positiontype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Positiontype[]>;
                })
                .then(result => {
                    state.positiontypes = result.sort((a, b) => {
                        if (a.name < b.name) return -1;
                        if (a.name > b.name) return 1;
                        return 0;
                    });
                })
        },
        updatePositioncategories(state) {
            fetch('api/Positioncategory', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Positioncategory[]>;
                })
                .then(result => {
                    result.forEach(r => {
                        if (r.civil > 0) {
                            r.civilbool = true;
                        } else {
                            r.civilbool = false;
                        }
                    })
                    state.positioncategories = result;
                })
        },
        updateMrds(state) {
            fetch('api/Mrd', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Mrd[]>;
                })
                .then(result => {
                    state.mrds = result;
                })
        },
        updateAltrankconditiongroups(state) {
            fetch('api/Altrankconditiongroup', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Altrankconditiongroup[]>;
                })
                .then(result => {
                    state.altrankconditiongroups = result;
                })
        },
        updateAltrankconditions(state) {
            fetch('api/Altrankcondition', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Altrankcondition[]>;
                })
                .then(result => {
                    state.altrankconditions = result;
                })
        },
        updateStructureregions(state) {
            fetch('api/Structureregion', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Structureregion[]>;
                })
                .then(result => {
                    state.structureregions = result;
                })
        },
        updateStructuretypes(state) {
            fetch('api/Structuretype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Structuretype[]>;
                })
                .then(result => {
                    state.structuretypes = result;
                })
        },
        updateRelativetypes(state) {
            fetch('api/Relativetype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Relativetype[]>;
                })
                .then(result => {
                    state.relativetypes = result;
                })
        },
        updateAttestationtypes(state) {
            fetch('api/Attestationtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Attestationtype[]>;
                })
                .then(result => {
                    state.attestationtypes = result;
                })
        },
        updateVacationmilitaries(state) {
            fetch('api/Vacationmilitary', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Vacationmilitary[]>;
                })
                .then(result => {
                    state.vacationmilitaries = result;
                })
        },
        updateVacationtypes(state) {
            fetch('api/Vacationtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Vacationtype[]>;
                })
                .then(result => {
                    state.vacationtypes = result;
                })
        },
        updateLanguagetypes(state) {
            fetch('api/Languagetype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Languagetype[]>;
                })
                .then(result => {
                    state.languagetypes = result;
                })
        },
        updateLanguageskills(state) {
            fetch('api/Languageskill', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Languageskill[]>;
                })
                .then(result => {
                    state.languageskills = result;
                })
        },
        updateJobtypes(state) {
            fetch('api/Jobtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Jobtype[]>;
                })
                .then(result => {
                    state.jobtypes = result;
                })
        },
        updateServicetypes(state) {
            fetch('api/Servicetype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Servicetype[]>;
                })
                .then(result => {
                    state.servicetypes = result;
                })
        },
        updateServicefeatures(state) {
            fetch('api/Servicefeature', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Servicefeature[]>;
                })
                .then(result => {
                    state.servicefeatures = result;
                })
        },
        updateServicecoefs(state) {
            fetch('api/Servicecoef', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Servicecoef[]>;
                })
                .then(result => {
                    state.servicecoefs = result;
                })
        },
        updatePenalties(state) {
            fetch('api/Penalty', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Penalty[]>;
                })
                .then(result => {
                    state.penalties = result;
                })
        },
        updateCountries(state) {
            fetch('api/Country', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Country[]>;
                })
                .then(result => {
                    state.countries = result;
                })
        },
        updateIllregimes(state) {
            fetch('api/Illregime', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Illregime[]>;
                })
                .then(result => {
                    state.illregimes = result;
                })
        },
        updateIllcodes(state) {
            fetch('api/Illcode', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Illcode[]>;
                })
                .then(result => {
                    state.illcodes = result;
                })
        },
        updateRewardtypes(state) {
            fetch('api/Rewardtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Rewardtype[]>;
                })
                .then(result => {
                    state.rewardtypes = result;
                })
        },
        updateRewards(state) {
            fetch('api/Reward', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Reward[]>;
                })
                .then(result => {
                    state.rewards = result;
                })
        },
        updateEducationlevels(state) {
            fetch('api/Educationlevel', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationlevel[]>;
                })
                .then(result => {
                    state.educationlevels = result;
                })
        },
        updateEducationtypes(state) {
            fetch('api/Educationtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationtype[]>;
                })
                .then(result => {
                    state.educationtypes = result;
                })
        },
        updateEducationdocuments(state) {
            fetch('api/Educationdocument', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationdocument[]>;
                })
                .then(result => {
                    state.educationdocuments = result;
                })
        },
        updateNormativs(state) {
            fetch('api/Normativ', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Normativ[]>;
                })
                .then(result => {
                    state.normativs = result;
                })
        },
        updateDrivertypes(state) {
            fetch('api/Drivertype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Drivertype[]>;
                })
                .then(result => {
                    state.drivertypes = result;
                })
        },
        updateDrivercategories(state) {
            fetch('api/Drivercategory', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Drivercategory[]>;
                })
                .then(result => {
                    state.drivercategories = result;
                })
        },
        updatePermissiontypes(state) {
            fetch('api/Permissiontype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Permissiontype[]>;
                })
                .then(result => {
                    state.permissiontypes = result;
                })
        },
        updateProoftypes(state) {
            fetch('api/Prooftype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Prooftype[]>;
                })
                .then(result => {
                    state.prooftypes = result;
                })
        },

        updateHolidays(state) {
            fetch('api/Holiday', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Holiday[]>;
                })
                .then(result => {
                    state.holidays = result;
                })
        },

        updatePersondecreeblocktypes(state) {
            fetch('api/Persondecreeblocktype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Persondecreeblocktype[]>;
                })
                .then(result => {
                    state.persondecreeblocktypes = result;
                })
        },

        updatePersondecreeblocksubtypes(state) {
            fetch('api/Persondecreeblocksubtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Persondecreeblocksubtype[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.persondecreeblocksubtypes = result;
                })
        },

        updateRegions(state) {
            fetch('api/Region', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Region[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.regions = result;
                })
        },

        updateAreas(state) {
            fetch('api/Area', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Area[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.areas = result;
                })
        },

        updateFires(state) {
            fetch('api/Fire', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Fire[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.fires = result;
                })
        },

        updateAppointtypes(state) {
            fetch('api/Appointtype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Appointtype[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.appointtypes = result;
                })
        },

        updateTransfertypes(state) {
            fetch('api/Transfertype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Transfertype[]>;
                })
                .then(result => {
                    state.transfertypes = result;
                })
        },

        updateSubjects(state) {
            fetch('api/Subject', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Subject[]>;
                })
                .then(result => {
                    state.subjects = result;
                })
        },

        updateSubjectgenders(state) {
            fetch('api/Subjectgender', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Subjectgender[]>;
                })
                .then(result => {
                    state.subjectgenders = result;
                })
        },

        updateSubjectcategories(state) {
            fetch('api/Subjectcategory', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Subjectcategory[]>;
                })
                .then(result => {
                    state.subjectcategories = result;
                })
        },

        updateInterrupttypes(state) {
            fetch('api/Interrupttype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Interrupttype[]>;
                })
                .then(result => {
                    state.interrupttypes = result;
                })
        },

        updateChangedocumentstypes(state) {
            fetch('api/Changedocumentstype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Changedocumentstype[]>;
                })
                .then(result => {
                    state.changedocumentstypes = result;
                })
        },

        updateSetpersondatatypes(state) {
            fetch('api/Setpersondatatype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Setpersondatatype[]>;
                })
                .then(result => {
                    state.setpersondatatypes = result;
                })
        },

        updateRewardmoneys(state) {
            fetch('api/Rewardmoney', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Rewardmoney[]>;
                })
                .then(result => {
                    state.rewardmoneys = result;
                })
        },

        updatePersondecreelevels(state) {
            fetch('api/Persondecreelevel', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Persondecreelevel[]>;
                })
                .then(result => {
                    state.persondecreelevels = result;
                })
        },

        updateOrdernumbertypes(state) {
            fetch('api/Ordernumbertype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Ordernumbertype[]>;
                })
                .then(result => {
                    state.ordernumbertypes = result;
                })
        },

        updateStructuresalldocument(state) {
            fetch('api/DetailedStructure/Alldocument', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<string[]>;
                })
                .then(result => {
                    state.structuresalldocument = result;
                })
        },

        updateCitytypes(state) {
            fetch('api/Citytype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Citytype[]>;
                })
                .then(result => {
                    state.citytypes = result;
                })
        },

        updateStreettypes(state) {
            fetch('api/Streettype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Streettype[]>;
                })
                .then(result => {
                    state.streettypes = result;
                })
        },

        updateAreaothers(state) {
            fetch('api/Areaother', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Areaother[]>;
                })
                .then(result => {
                    state.areaothers = result;
                })
        },

        updateExternalorderwhotypes(state) {
            fetch('api/Externalorderwhotype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Externalorderwhotype[]>;
                })
                .then(result => {
                    state.externalorderwhotypes = result;
                })
        },

        updatePersondecreetypes(state) {
            fetch('api/Persondecreetype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Persondecreetype[]>;
                })
                .then(result => {
                    state.persondecreetypes = result;
                })
        },

        updateEducationadditionaltypes(state) {
            fetch('api/Educationadditionaltype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationadditionaltype[]>;
                })
                .then(result => {
                    state.educationadditionaltypes = result;
                })
        },

        updateCitysubstates(state) {
            fetch('api/Citysubstate', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Citysubstate[]>;
                })
                .then(result => {
                    state.citysubstates = result;
                })
        },

        updateEducationstages(state) {
            fetch('api/Educationstage', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationstage[]>;
                })
                .then(result => {
                    state.educationstages = result;
                })
        },

        updateEducationpositiontypes(state) {
            fetch('api/Educationpositiontype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Educationpositiontype[]>;
                })
                .then(result => {
                    state.educationpositiontypes = result;
                })
        },

        updateRoles(state) {
            fetch('api/Role', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Role[]>;
                })
                .then(result => {
                    state.roles = result;
                })
        },

        //areaothers: Areaother[""],
        //externalorderwhotypes: Externalorderwhotype[""],

        setDecree(state, n) {
            state.decree = n;
        },
        setDecreeName(state, n) {
            state.decreeName = n;
        },
        setPositionCompact(state, n) {
           // alert(n);
            state.positioncompact = n;

        },
        updatePositionCompact(state, n) {
            fetch('/api/Identity', {
                method: 'post',
                body: JSON.stringify(<Usersettings>{
                    positioncompactValue: n, updatePositioncompact: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            });
        },
        updateUserAppearance(state, n) {
            
            fetch('/api/Identity', {
                method: 'post',
                body: JSON.stringify(<Usersettings>{
                    positioncompactValue: n.positioncompact,
                    updatePositioncompact: 1,
                    sidebarDisplayValue: n.sidebardisplay,
                    updateSidebarDisplay: 1,
                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            }).then(x => {
                //state.sidebarDisplay = n.sidebardisplay;
                //state.positioncompact = n.positioncompact;
            });
        },
        setSidebarDisplay(state, n) {
            state.sidebarDisplay = n;
        },
        setFeatured(state, n) {
            state.featured = n;
        },
        setModeselectcuration(state, n) {
            state.modeselectcuration = n;
        },
        setModeselectedcuration(state, n) {
            state.modeselectedcuration = n;
        },
        setModeselectheading(state, n) {
            state.modeselectheading = n;
        },
        setModeselectedheading(state, n) {
            state.modeselectedheading = n;
        },
        setModeselectstructure(state, n) {
            state.modeselectstructure = n;
        },
        setModeselectedstructure(state, n) {
            state.modeselectedstructure = n;
        },
        setModecopy(state, n) {
            state.modecopy = n;
        },
        setModeappointperson(state, n) {
            state.modeappointperson = n;
        },
        setModeappointedperson(state, n) {
            state.modeappointedperson = n;
        },
        setModeappointpersondecree(state, n) {
            state.modeappointpersondecree = n;
        },
        setModeappointedpersondecree(state, n) {
            state.modeappointedpersondecree = n;
        },
        setModeappointpersonstructuredecree(state, n) {
            state.modeappointpersonstructuredecree = n;
        },
        setModeappointedpersonstructuredecree(state, n) {
            state.modeappointedpersonstructuredecree = n;
        },

        setModecopystring(state, n) {
            state.modecopystring = n; // String with copy data. "s=1" means copy structure with id 1 and all substructrures with the same or null type and all positions inclusive.
        },
    }
})

new Vue({
    el: '#app-root',
    //mixins: globals,
    router: new VueRouter({ mode: 'history', routes: routes }),
    store,
    data: {
        currentRoute: window.location.pathname,
        
    },
    render: function (h) {
        //alert(window.location.pathname);
        //this.currentRoute
        if (window.location.pathname != '/login') {
            return h(require('./components/app/app.vue.html'))
        } else {
            return h(require('./components/notemplate/notemplate.vue.html'))
        }
    }

});