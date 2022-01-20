
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
import Country from './classes/country';
import Structure from './classes/Structure';
import Region from './classes/region';
import Area from './classes/area';
import Subject from './classes/subject';
import Subjectgender from './classes/subjectgender';
import Subjectcategory from './classes/subjectcategory';
import Changedocumentstype from './classes/changedocumentstype';
import Streettype from './classes/streettype';
import Citytype from './classes/citytype';
import Areaother from './classes/areaother';
import Externalorderwhotype from './classes/externalorderwhotype';
import Citysubstate from './classes/citysubstate';
import User from './classes/user';

import VueRouter from 'vue-router';

import locale from 'element-ui/lib/locale/lang/ru-RU'
Vue.use(VueRouter);
Vue.use(Vuex);
Vue.use(DlgDraggable);
Vue.use(Element, { locale });

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
        mailmodeprevios: false,
        chosenPosition: null,

        eldVisible: 0,
        candidatesVisible: 0,
        candidatesId: 0,
        candidatesPosition: 0,
        candidatesStructure: 0,
        candidatesAction: 0,
        modepanelVisible: 1,
        positionsListTitle: "Должности",
        forcePositionUpdate: false,
        login: "",
        currentstructuretree: "",
        userStructure: "0",
        structureeditorAccess: "0",
        structurereadAccess: "0",
        personneleditorAccess: "0",
        personnelreadAccess: "0",
        masterpersonneleditorAccess: "0",
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
        countries: Country[""],
        regions: Region[""],
        areas: Area[""],
        subjects: Subject[""],
        subjectgenders: Subjectgender[""],
        subjectcategories: Subjectcategory[""],
        changedocumentstypes: Changedocumentstype[""],
        streettypes: Streettype[""],
        structures: Structure[""],
        citytypes: Citytype[""],
        areaothers: Areaother[""],
        externalorderwhotypes: Externalorderwhotype[""],
        citysubstates: Citysubstate[""],

        /**
         * Modes
         */
        modeselectstructureforCPU: false,
        modeselectcuration: false,
        modeselectedcuration: 0,
        modeselectheading: false,
        modeselectedheading: 0,
        modeselectstructure: false,
        modeselectedstructure: 0,
        modecopy: false,
        modecopystring: "", // String with copy data. "s=1" means copy structure with id 1 and all substructrures with the same or null type and all positions inclusive.

        oldcurrentmode: {"fullmode": "0",
                "sidebarDisplay" : "0",
                "modepanelVisible": 0,
                "candidatesVisible" : 0,
                "eldVisible" : 0,
                "positionsListId" : 0,
                "decreeoperationelementVisible" : 0,
                "decreeoperationtemplatecreatorVisible" : false },

        currentdecreemail: "",
        decreemail: false,

        excertmenu: false,
        excertdecreeid: null,
        lodingexcert: false,
    },
    mutations: {
        setdecreeoperationelementVisible(state, n) {
            state.decreeoperationelementVisible = n;
        },

        setdecreeoperationtemplatecreatorVisible(state, n) {
            state.decreeoperationtemplatecreatorVisible = n;
        },

        setmailmodeprevios(state, n) {
            state.mailmodeprevios = n;
        },

        setEldVisible(state, n) {
            state.eldVisible = n;

        },

        setchosenPosition(state, n) {
            state.chosenPosition = n;
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
        setPersonneleditorAccess(state, n) {
            state.personneleditorAccess = n;
        },
        setPersonnelreadAccess(state, n) {
            state.personnelreadAccess = n;
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
        setExcertMenu(state, n) {
            state.excertmenu = n;
        },
        setExcertDecreeId(state, n) {
            state.excertdecreeid = n;
        },
        setLodingExcert(state, n) {
            state.lodingexcert = n;
        },

        setdecreemailM(state, n) {
            state.currentdecreemail = n;
            if (n == "") {
                state.decreemail = false;
            } else {
                state.decreemail = true;
            }
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
        updateCountries(state) {
            fetch('api/Country', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Country[]>;
                })
                .then(result => {
                    state.countries = result;
                })
        },
        updateStructureresAll(state) {
            fetch('api/Structure/All', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Structure[]>;
                })
                .then(result => {
                    //alert(JSON.stringify(result));
                    state.structures = result;
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

        updateChangedocumentstypes(state) {
            fetch('api/Changedocumentstype', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Changedocumentstype[]>;
                })
                .then(result => {
                    state.changedocumentstypes = result;
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

        updateCitysubstates(state) {
            fetch('api/Citysubstate', { credentials: 'include' })
                .then(response => {
                    return response.json() as Promise<Citysubstate[]>;
                })
                .then(result => {
                    state.citysubstates = result;
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
                state.sidebarDisplay = n.sidebardisplay;
                state.positioncompact = n.positioncompact;
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
            if (n) {
                state.oldcurrentmode.fullmode = state.fullmode;
                state.oldcurrentmode.sidebarDisplay = state.sidebarDisplay;
                state.oldcurrentmode.modepanelVisible = state.modepanelVisible;
                state.oldcurrentmode.candidatesVisible = state.candidatesVisible;
                state.oldcurrentmode.eldVisible = state.eldVisible;
                state.oldcurrentmode.positionsListId = state.positionsListId;
                state.oldcurrentmode.decreeoperationelementVisible = state.decreeoperationelementVisible;
                state.oldcurrentmode.decreeoperationtemplatecreatorVisible = state.decreeoperationtemplatecreatorVisible;

                state.fullmode = "1";
                state.sidebarDisplay = "1";
                state.modepanelVisible = 0;
                state.eldVisible = 0;
                state.candidatesVisible = 0;
                state.positionsListId = 1;
                state.decreeoperationelementVisible = 0;
                state.decreeoperationtemplatecreatorVisible = false;
                fetch('/api/Identity', {
                    method: 'post',
                    body: JSON.stringify(<Usersettings>{
                        positioncompactValue: Number(state.positioncompact),
                        updatePositioncompact: 1,
                        sidebarDisplayValue: 1,
                        updateSidebarDisplay: 1,
                    }),
                    credentials: 'include',
                    headers: new Headers({
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    })
                }).then(x => {
                    state.sidebarDisplay = n.sidebardisplay;
                    state.positioncompact = n.positioncompact;
                });
                fetch('api/Identity/Fullmode1', { credentials: 'include' })
                //fetch('api/Identity/Org', { credentials: 'include' })
                //state.user.prototype.fullmode = 1;
            } else {
                state.fullmode = state.oldcurrentmode.fullmode;
                state.sidebarDisplay = state.oldcurrentmode.sidebarDisplay;
                state.modepanelVisible = state.oldcurrentmode.modepanelVisible;
                state.oldcurrentmode.eldVisible = state.eldVisible;
                state.candidatesVisible = state.oldcurrentmode.candidatesVisible;
                state.positionsListId = state.oldcurrentmode.positionsListId;
                state.decreeoperationelementVisible = state.oldcurrentmode.decreeoperationelementVisible;
                state.decreeoperationtemplatecreatorVisible = state.oldcurrentmode.decreeoperationtemplatecreatorVisible;
                fetch('/api/Identity', {
                    method: 'post',
                    body: JSON.stringify(<Usersettings>{
                        positioncompactValue: Number(state.positioncompact),
                        updatePositioncompact: 1,
                        sidebarDisplayValue: Number(state.oldcurrentmode.sidebarDisplay),
                        updateSidebarDisplay: 1,
                    }),
                    credentials: 'include',
                    headers: new Headers({
                        'Accept': 'application/json',
                        'Content-Type': 'application/json'
                    })
                }).then(x => {
                    state.sidebarDisplay = n.sidebardisplay;
                    state.positioncompact = n.positioncompact;
                });
                //this.updateUserAppearance(state, appearance);
                fetch('api/Identity/Fullmode' + state.oldcurrentmode.fullmode, { credentials: 'include' })
                //state.user.prototype.fullmode = Number(state.oldcurrentmode.fullmode);
            }
            state.modeselectstructure = n;
        },
        setModeselectedstructure(state, n) {
            state.modeselectedstructure = n;
        },
        setModecopy(state, n) {
            state.modecopy = n;
        },

        setModecopystring(state, n) {
            state.modecopystring = n; // String with copy data. "s=1" means copy structure with id 1 and all substructrures with the same or null type and all positions inclusive.
        },
    },
    /*CopyCurrentState() {
        var actual: { [name: string]: any } = {};
        actual.modepanelVisible = this.store.state.modepanelVisible;
        actual.candidatesVisible = this.store.state.candidatesVisible;
        actual.eldVisible = this.store.state.eldVisible;
        actual.positionsListId = this.store.state.positionsListId;
        actual.decreeoperationelementVisible = this.store.state.decreeoperationelementVisible;
        actual.decreeoperationtemplatecreatorVisible = this.store.state.decreeoperationtemplatecreatorVisible;
        return actual;
    }*/
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