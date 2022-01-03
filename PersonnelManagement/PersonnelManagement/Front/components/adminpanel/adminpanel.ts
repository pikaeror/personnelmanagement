import Vue from 'vue';
import { Component, Prop, Watch  } from 'vue-property-decorator';
import Element, { Dialog } from 'element-ui';
import $ from "jquery";
import Rank from "../../classes/rank";
import Sourceoffinancing from "../../classes/sourceoffinancing";
import Positiontype from "../../classes/positiontype";
import Positioncategory from "../../classes/positioncategory";
import Mrd from '../../classes/mrd';
import Altrankconditiongroup from '../../classes/altrankconditiongroup';
import Altrankcondition from '../../classes/altrankcondition';
import Altrank from '../../classes/altrank';
import Structureregion from '../../classes/structureregion';
import Structuretype from '../../classes/structuretype';
import StructureTree from '../../classes/structuretree';
import Subject from '../../classes/subject';
import { Button, Select, Input, Collapse, CollapseItem, Switch, Notification, TabPane, Tabs, Checkbox, Option } from 'element-ui';
import Illcode from '../../classes/illcode';
import Subjectgender from '../../classes/subjectgender';
import Subjectcategory from '../../classes/subjectcategory';
import PositionManagement from '../../classes/positionmanagement';
import Rights from '../../classes/rights';
import Role from '../../classes/role';
import User from '../../classes/user';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Select.name, Select);
Vue.component(Option.name, Option);
Vue.component(Collapse.name, Collapse);
Vue.component(CollapseItem.name, CollapseItem);
Vue.component(Switch.name, Switch);
Vue.component(TabPane.name, TabPane);
Vue.component(Tabs.name, Tabs);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Dialog.name, Dialog);
Vue.use(Element);

//class User {
//    id: number;
//    name: string;
//    password: string;
//    salt: string;
//    admin: string;
//    structureeditor: string;
//    masterpersonneleditor: string;
//    personneleditor: string;
//    structure: string;
//    structurename: string;
//    structureread: string;
//    personnelread: string;
//    firstname: string;
//    surname: string;
//    patronymic: string;
//    positiontype: number;
//}




const fetchUserDelay: number = 3000;
const fetchRanksDelay: number = 6000;
const fetchPositiontypeDelay: number = 18000;
const fetchPositioncategoryDelay: number = 9000;
const fetchMrdDelay: number = 9000;
const fetchAltrankconditiongroupDelay = 9000;
const fetchAltrankconditionDelay = 9000;
const fetchSofsDelay: number = 6000;
const fetchSubjectsDelay: number = 9000;
const fetchStructureregionDelay = 9000;
const fetchStructuretypeDelay = 9000;
const fetchIllcodeDelay = 9000;
const updateDelayAfterEdit: number = 24000;
const delayMinimum: number = -20000;

@Component({
    components: {
        
    }
})
export default class AdminpanelComponent extends Vue {
    users: User[];
    structuresAll: any;
    newUser: User;
    newUserName: string;
    newUserAdmin: string;
    newUserStructure : string;
    newUserStructureEditor: string;
    newUserMasterPersonnelEditor: string;
    newUserPersonnelEditor: string;
    newUserStructureRead: string;
    newUserPersonnelRead: string;
    newUserFirstname: string;
    newUserSurname: string;
    newUserPatronymic: string;
    newUserPositiontype: number;
    userStopUpdateTimer: number;
    newUserEditid: number; // Если мы собираемся редактировать пользователя, будем делать это в меню "Добавить пользователя", чтобы избежать лишних пролагов. 

    rightsPositiontype: number;
    rightsRights: Rights;

    newRankName: string;
    newRankPositioncategory: number;
    new_rank_maximum_period: number;
    ranks: Rank[];
    rankStopUpdateTimer: number;

    newSofName: string;
    sofs: Sourceoffinancing[];
    sofStopUpdateTimer: number;

    newSubjectId: number; // Для редактирования
    newSubjectName: string;
    newSubjectName1: string;
    newSubjectName1Filtered: string;
    newSubjectName2: string;
    newSubjectName3: string;
    newSubjectName4: string;
    newSubjectName5: string;
    newSubjectName6: string;
    newSubjectGender: number;
    newSubjectGenderFiltered: number;
    newSubjectCategory: number;
    newSubjectCategoryFiltered: number;
    newSubjectDropword: boolean;
    subjects: Subject[];
    subjectStopUpdateTimer: number;

    newPositiontypeId: number;
    newPositiontypeName: string;
    newPositiontypeName1: string;
    newPositiontypeName2: string;
    newPositiontypeName3: string;
    newPositiontypeNameshort: string;
    newPositiontypePriority: number;
    positiontypes: Positiontype[];
    positiontypeStopUpdateTimer: number;

    newPositioncategoryName: string;
    newPositioncategoryCivil: boolean;
    newPositioncategoryVariable: boolean;
    positioncategories: Positioncategory[];
    positioncategoriesnocivil: Positioncategory[];
    positioncategoryStopUpdateTimer: number;

    newMrd: string;
    newMrdShort: string;
    mrds: Mrd[];
    mrdStopUpdateTimer: number;

    newAltrankconditiongroup: string;
    newAltrankconditionaltrankconditiongroup: number;
    altrankconditiongroups: Altrankconditiongroup[];
    altrankconditiongroupStopUpdateTimer: number;

    newAltrankcondition: string;
    altrankconditions: Altrankcondition[];
    altrankconditionStopUpdateTimer: number;

    newStructureregionName: string;
    structureregions: Structureregion[];
    structureregionStopUpdateTimer: number;

    newStructuretypeName: string;
    structuretypes: Structuretype[];
    structuretypeStopUpdateTimer: number;

    newIllcodeName: string;
    illcodes: Structuretype[];
    illcodeStopUpdateTimer: number;

    activeAdminSection: string;
    addUserResult: string;
    
    testSwitch: string;
    activeName: string;
    activeNameRank: string;
    activeNameUser: string;
    activeNameFinancing: string;
    activeNameSubject: string;
    activeNamePositiontype: string;
    activeNamePositioncategory: string;
    activeNameMrd: string;
    activeNameAltrankconditiongroup: string;
    activeNameAltrankcondition: string;
    activeNameStructureregion: string;
    activeNameStructuretype: string;
    activeNameIllcode: string;

    head: boolean;
    headid: number;
    headingStructureTree: StructureTree;
    headingselectionprocess: boolean;

    modalPositioneditMenuVisible: boolean;
    positiontypeid: number;
    positiontypeObject: Positiontype;
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
    subject16: number;
    subject17: number;
    subject18: number;
    subject19: number;
    subject20: number;
    filteredSubjects: Subject[];

    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: false })
    visiblevar: boolean;

    data() {
        return {
            activeAdminSection: 'section-users',
            newUser: new User(),
            newUserName: "",
            newUserAdmin: "0",
            addUserResult: "",
            newUserStructure: "",
            newUserStructureEditor: "0",
            newUserMasterPersonnelEditor: "0",
            newUserPersonnelEditor: "1",
            newUserStructureRead: "0",
            newUserPersonnelRead: "0",
            newUserFirstname: "",
            newUserSurname: "",
            newUserPatronymic: "",
            newUserPositiontype: 1,
            newUserEditid: 0, // Если мы собираемся редактировать пользователя, будем делать это в меню "Добавить пользователя", чтобы избежать лишних пролагов. 
            users: [],
            userStopUpdateTimer: 0,
            structuresAll: [],

            rightsPositiontype: null,
            rightsRights: null,

            newRankName: "",
            newRankPositioncategory: 1,
            new_rank_maximum_period: 1,
            rankStopUpdateTimer: 0,
            ranks: [],

            newSofName: "",
            sofs: [],
            sofStopUpdateTimer: 0,

            newSubjectId: 0,
            newSubjectName: "",
            newSubjectName1: "",
            newSubjectName1Filtered: "",
            newSubjectName2: "",
            newSubjectName3: "",
            newSubjectName4: "",
            newSubjectName5: "",
            newSubjectName6: "",
            newSubjectGender: 0,
            newSubjectGenderFiltered: 0,
            newSubjectCategory: 0,
            newSubjectCategoryFiltered: 2,
            newSubjectDropword: false,
            subjects: [],
            subjectStopUpdateTimer: 0,

            newPositiontypeName: "",
            newPositiontypeName1: "",
            newPositiontypeName2: "",
            newPositiontypeName3: "",
            newPositiontypeNameshort: "", 
            newPositiontypePriority: 50,
            positiontypes: [],
            positiontypeStopUpdateTimer: 0,

            newPositioncategoryName: "",
            newPositioncategoryCivil: false,
            newPositioncategoryVariable: false,
            positioncategories: [],
            positioncategoriesnocivil: [],
            positioncategoryStopUpdateTimer: 0,

            newMrd: "",
            newMrdShort: "",
            mrds: [],
            mrdStopUpdateTimer: 0,

            newAltrankconditiongroup: "",
            altrankconditiongroups: [],
            altrankconditiongroupStopUpdateTimer: 0,

            newAltrankcondition: "",
            newAltrankconditionaltrankconditiongroup: 1,
            altrankconditions: [],
            altrankconditionStopUpdateTimer: 0,

            newStructureregionName: "",
            structureregions: [],
            structureregionStopUpdateTimer: 0,

            newStructuretypeName: "",
            structuretypes: [],
            structuretypeStopUpdateTimer: 0,

            newIllcodeName: "",
            illcodes: [],
            illcodeStopUpdateTimer: 0,

            testSwitch: "1",
            activeName: "first",
            activeNameRank: "rfirst",
            activeNameUser: "ufirst",
            activeNameFinancing: "ffirst",
            activeNameSubject: "sufirst",
            activeNamePositiontype: "pfirst",
            activeNamePositioncategory: "pcfirst",
            activeNameMrd: "mfirst",
            activeNameAltrankconditiongroup: "arcgfirst",
            activeNameAltrankcondition: "arcfirst",
            activeNameStructureregion: "srfirst",
            activeNameStructuretype: "stfirst",
            activeNameIllcode: "icfirst",

            head: false,
            headid: 0,
            headingStructureTree: null,
            headingselectionprocess: false,

            modalPositioneditMenuVisible: false,
            positiontypeid: null,
            positiontypeObject: null,
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
            subject16: null,
            subject17: null,
            subject18: null,
            subject19: null,
            subject20: null,
            filteredSubjects: [],
        }
    }

    mounted() {
        //alert(this.newUser + " " + this.newUser.rights.menu);
        //this.structuresAll = await ((<any>Vue).getStructureAll()); 
        (<any>Vue).getStructureAll().then((data) => {
            this.structuresAll = data;
            
            
            //alert((<any>Vue).getKeyByValue(this.structuresAll, val));

        });
        this.fetchUsers();
        this.fetchRanks();
        this.fetchSofs();
        this.fetchSubjects();
        this.fetchPositiontypes();
        this.fetchPositioncategories();
        this.fetchMrds();
        this.fetchAltrankconditiongroups();
        this.fetchAltrankconditions();
        this.fetchStructureregions();
        this.fetchStructuretypes();
        setInterval(this.fetchUsers, fetchUserDelay); //$(element).is(':visible')
        setInterval(this.fetchRanks, fetchRanksDelay);
        setInterval(this.fetchSofs, fetchSofsDelay);
        setInterval(this.fetchSubjects, fetchSubjectsDelay);
        setInterval(this.fetchPositiontypes, fetchPositiontypeDelay);
        setInterval(this.fetchPositioncategories, fetchPositioncategoryDelay);
        setInterval(this.fetchMrds, fetchMrdDelay);
        setInterval(this.fetchAltrankconditiongroups, fetchAltrankconditiongroupDelay);
        setInterval(this.fetchAltrankconditions, fetchAltrankconditionDelay);
        setInterval(this.fetchStructureregions, fetchStructureregionDelay);
        setInterval(this.fetchStructuretypes, fetchStructuretypeDelay);
        setInterval(this.fetchIllcodes, fetchIllcodeDelay);

        this.filteredSubjects = this.subjects.filter(option => (option.category == 1 || option.category == 7));
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value && this.headingselectionprocess) {
            if (this.$store.state.modeselectedheading > 0) {
                // Раньше мы редактировали/добавляли пользователя в отдельных полях, теперь все редактирование происходит в объекте newUser
                this.headid = this.$store.state.modeselectedheading;
                if (this.newUser != null) {
                    //this.headid.toString()
                    //this.newUser.structure = this.headid.toString();
                    this.newUser.structure = this.headid;
                }
                this.$store.commit("setModeselectedheading", 0);
            }
            this.prepareTrees();
            this.headingselectionprocess = false;
            return;
        }
    }



    get positiontypesList(): Positiontype[] {
        return this.$store.state.positiontypes;
    }

    get roles(): Role[] {
        return this.$store.state.roles;
    }

    getPositiontype(positiontype: number): string {
        if (positiontype == null || positiontype == 0) {
            return "";
        }
        let ptype: Positiontype = this.positiontypesList.find(p => p.id == positiontype);
        if (ptype != null) {
            return ptype.name;
        } else {
            return "";
        }

    }

    get subjectgenders(): Subjectgender[] {
        return this.$store.state.subjectgenders;
    }

    getSubjectgender(subjectgender: number): string {
        if (subjectgender == null || subjectgender == 0) {
            return "";
        }
        let stype: Subjectgender = this.subjectgenders.find(p => p.id == subjectgender);
        if (stype != null) {
            return stype.name;
        } else {
            return "";
        }
    }

    get subjectcategories(): Subjectcategory[] {
        return this.$store.state.subjectcategories;
    }

    getSubjectcategory(subjectcategory: number): string {
        if (subjectcategory == null || subjectcategory == 0) {
            return "";
        }
        let stype: Subjectcategory = this.subjectcategories.find(p => p.id == subjectcategory);
        if (stype != null) {
            return stype.name;
        } else {
            return "";
        }
    }

    fetchUsers(force?: boolean) {
        //$('adminpanel-container').
        if (($('#adminpanel-container').is(':visible') && this.activeNameUser === 'ufirst'
            && this.userStopUpdateTimer <= 0) || force == true) {
        fetch('api/Users', { credentials: 'include' })
            .then(response => response.json() as Promise<User[]>)
            .then(data => {
                data.forEach(u => {
                    //if (u.admin == "1") { u.admin = "1" };
                    ////if (u.admin == null) { u.admin = "0" };
                    //if (u.structureeditor == "1") { u.structureeditor = "1" };
                    //if (u.structureread == "1") { u.structureread = "1" };
                    ////if (u.structure_editor == null) { u.structure_editor = "0" };
                    //if (u.masterpersonneleditor == "1") { u.masterpersonneleditor = "1" };
                    ////if (u.master_personnel_editor == null) { u.master_personnel_editor = "0" };
                    //if (u.personneleditor == "1") { u.personneleditor = "1" };
                    //if (u.personnelread == "1") { u.personnelread = "1" };
                    ////if (u.personnel_editor == null) { u.personnel_editor = "0" };
                    ////if (u.structure == null) { u.structure = "1" };
                    u.structurename = this.structuresAll[u.structure];

                    //alert(u.firstname);
                });
                this.users = data;
            })
        }
        if (this.userStopUpdateTimer > delayMinimum) {
            this.userStopUpdateTimer -= fetchUserDelay;
        }
        
    }


    stopUpdate() {

    }

    get ranksOrdered() {
        return this.ranks.sort((a, b) => (a.order > b.order) ? 1 : ((b.order > a.order) ? -1 : 0)); 
    }


    startUserUpdate() {
        this.userStopUpdateTimer = updateDelayAfterEdit;
    }

    startPositiontypeUpdate() {
        this.positiontypeStopUpdateTimer = updateDelayAfterEdit;
    }

    startStructuretypeUpdate() {
        this.structuretypeStopUpdateTimer = updateDelayAfterEdit;
    }

    startIllcodeUpdate() {
        this.illcodeStopUpdateTimer = updateDelayAfterEdit;
    }

    startStructureregionUpdate() {
        this.structureregionStopUpdateTimer = updateDelayAfterEdit;
    }

    startRankUpdate() {
        this.rankStopUpdateTimer = updateDelayAfterEdit;
    }

    startMrdUpdate() {
        this.mrdStopUpdateTimer = updateDelayAfterEdit;
    }

    startAltrankconditiongroupUpdate() {
        this.altrankconditiongroupStopUpdateTimer = updateDelayAfterEdit;
    }

    startAltrankconditionUpdate() {
        this.altrankconditiongroupStopUpdateTimer = updateDelayAfterEdit;
    }

    saveUserUpdate(event: any) {
        let userID = $(event.currentTarget).prop("id");
        let user: User = this.users.find(e => e.id == userID);
        //alert(user.id + " " + user.structurename);
        let structureIndex = (<any>Vue).getKeyByValue(this.structuresAll, user.structurename);
        if (!isNaN(Number.parseInt(user.structurename))) {
            structureIndex = user.structurename;
        }
        if (user.positiontype == null) {
            user.positiontype = 0;
        }

        this.userStopUpdateTimer = 0;
        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(<User>{
                id: user.id, name: user.name, admin: user.admin, structure: structureIndex, masterpersonneleditor: user.masterpersonneleditor,
                personneleditor: user.personneleditor, structureeditor: user.structureeditor, structureread: user.structureread, personnelread: user.personnelread,
                firstname: user.firstname, surname: user.surname, patronymic: user.patronymic, positiontype: user.positiontype,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.fetchUsers(true);
                (<any>Vue).forceStructureUpdate = true;
                (<any>Vue).notify(response);
            });
        
    }

    /**
     * Обнуляем пароль пользователя. Чтобы при следующем входе вводил новый пароль. 
     * @param event
     */
    nullPassUserUpdate(event: any) {
        if (event) event.preventDefault();
        let userID = $(event.currentTarget).prop("id");
        let user: User = this.users.find(e => e.id == userID);
        user.salt = (<any>Vue).keys["STATUS_NULLIFYPASS"];
        this.userStopUpdateTimer = 0;
        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(user),
            //body: JSON.stringify(<User>{ salt: user.salt, id: user.id }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.fetchUsers(true);
                (<any>Vue).notify(response);
            });
    }

    deleteUser(event: any) {
        if (event) event.preventDefault();
        let userID = $(event.currentTarget).prop("id");
        let user: User = this.users.find(e => e.id == userID);
        user.salt = (<any>Vue).keys["STATUS_DELETE"];
        this.userStopUpdateTimer = 0;
        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(<User>{ salt: user.salt, id: user.id }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.fetchUsers(true);
                (<any>Vue).notify(response);
            });
    }

    addUser(event: any) {
        if (event) event.preventDefault();
        let structureKey: string = (<any>Vue).getKeyByValue(this.structuresAll, this.newUserStructure); // structure: structureKey,
        //alert('add');
        if (this.newUserPositiontype == null) {
            this.newUserPositiontype = 0;
        }
        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(this.newUser),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.addUserResult = response;
                this.fetchUsers(true);
                (<any>Vue).notify(response);
                //Vue.$forceUpdate();
            });

        //fetch('/api/Users', {
        //    method: 'post',
        //    body: JSON.stringify(<User>{
        //        name: this.newUserName, admin: this.newUserAdmin, structure: this.headid.toString(), masterpersonneleditor: this.newUserMasterPersonnelEditor,
        //        personneleditor: this.newUserPersonnelEditor, structureeditor: this.newUserStructureEditor, personnelread: this.newUserPersonnelRead, structureread: this.newUserStructureRead,
        //        firstname: this.newUserFirstname, surname: this.newUserSurname, patronymic: this.newUserPatronymic, positiontype: this.newUserPositiontype,
        //    }),
        //    credentials: 'include',
        //    headers: new Headers({
        //        'Accept': 'application/json',
        //        'Content-Type': 'application/json'
        //    })
        //})
        //    .then(response => { return response.json(); })
        //    .then((response) => {
        //        this.addUserResult = response;
        //        this.fetchUsers(true);
        //        (<any>Vue).notify(response);
        //        //Vue.$forceUpdate();
        //    });
        
    }

    isAdmin(user: User) {
        if (user.admin == 1) {
        //if (user.admin == "1") {
            return true;
        } else {
            return false;
        };
    }

    fetchRanks() {
        //$('adminpanel-container').
        if ($('#adminpanel-container').is(':visible') && this.activeNameRank === 'rfirst'
            && this.rankStopUpdateTimer <= 0) {
            fetch('api/Ranks', { credentials: 'include' })
                .then(response => response.json() as Promise<Rank[]>)
                .then(data => {
                    //data.forEach(r => {
                        // Future logic
                    //});
                    this.ranks = data;
                })
        }
        if (this.rankStopUpdateTimer > delayMinimum) {
            this.rankStopUpdateTimer -= fetchRanksDelay;
        }
        
    }

    fetchSofs() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameFinancing === 'ffirst'
            && this.sofStopUpdateTimer <= 0) {
            fetch('api/SourceOfFinancing', { credentials: 'include' })
                .then(response => response.json() as Promise<Sourceoffinancing[]>)
                .then(data => {
                    //data.forEach(r => {
                        // Future logic
                    //});
                    this.sofs = data;
                })
        }
        if (this.sofStopUpdateTimer > delayMinimum) {
            this.sofStopUpdateTimer -= fetchSofsDelay;
        }
    }

    fetchSubjects() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameSubject === 'sufirst'
            && this.subjectStopUpdateTimer <= 0) {
            fetch('api/Subject', { credentials: 'include' })
                .then(response => response.json() as Promise<Subject[]>)
                .then(data => {
                    data.forEach(s => {
                        if (s.dropword != null && s.dropword > 0) {
                            s.dropwordBool = true;
                        } else {
                            s.dropwordBool = false;
                        }
                    })
                    //this.subjects = data;

                    // Сортируем по названию
                    this.subjects = data.sort((s1, s2) => {
                        if (s1.name1 > s2.name1) { return 1 };
                        if (s1.name1 < s2.name1) { return -1 };
                        return 0;
                    });
                })
        }
        if (this.subjectStopUpdateTimer > delayMinimum) {
            this.subjectStopUpdateTimer -= fetchSubjectsDelay;
        }
    }


    fetchPositiontypes() {
        if ($('#adminpanel-container').is(':visible') && this.activeNamePositiontype === 'pfirst'
            && this.positiontypeStopUpdateTimer <= 0) {
            // && this.activeName === 'fourth'
            this.fetchPositiontypesForced();
        }
        if (this.positiontypeStopUpdateTimer > delayMinimum) {
            this.positiontypeStopUpdateTimer -= fetchPositiontypeDelay;
        }
    }

    fetchPositiontypesForced() {
        fetch('api/Positiontype', { credentials: 'include' })
            .then(response => response.json() as Promise<Positiontype[]>)
            .then(data => {
                //data.forEach(r => {
                // Future logic
                //});
                // Сортируем по наименованию
                this.positiontypes = data.sort((s1, s2) => {
                    if (s1.name > s2.name) { return 1 };
                    if (s1.name < s2.name) { return -1 };
                    return 0;
                });
            })
    }

    fetchPositioncategories() {
        if ($('#adminpanel-container').is(':visible') && this.activeNamePositioncategory === 'pcfirst'
            && this.positioncategoryStopUpdateTimer <= 0) {
            //alert(this.activeNamePositioncategory);
            fetch('api/Positioncategory', { credentials: 'include' })
                .then(response => response.json() as Promise<Positioncategory[]>)
                .then(data => {
                    data.forEach(r => {
                        // Future logic
                        if (r.civil > 0) {
                            r.civilbool = true;
                        } else {
                            r.civilbool = false;

                        }
                        if (r.variable > 0) {
                            r.variablebool = true;
                        } else {
                            r.variablebool = false;
                        }
                    });
                    this.positioncategories = data;
                    //this.positioncategoriesnocivil = data; //this.positioncategories.filter(d => d.civil == 0);
                    this.positioncategoriesnocivil = this.positioncategories.filter(d => d.civil == 0);

                })
        }
        if (this.positioncategoryStopUpdateTimer > delayMinimum) {
            this.positioncategoryStopUpdateTimer -= fetchPositioncategoryDelay;
        }
    }

    fetchMrds() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameMrd === 'mfirst'
            && this.mrdStopUpdateTimer <= 0) {
            fetch('api/Mrd', { credentials: 'include' })
                .then(response => response.json() as Promise<Mrd[]>)
                .then(data => {
                    //data.forEach(r => {
                    //});
                    this.mrds = data.sort((s1, s2) => {
                        if (s1.name > s2.name) { return 1 };
                        if (s1.name < s2.name) { return -1 };
                        return 0;
                    });

                })
        }
        if (this.mrdStopUpdateTimer > delayMinimum) {
            this.mrdStopUpdateTimer -= fetchMrdDelay;
        }
    }

    fetchAltrankconditiongroups() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameAltrankconditiongroup === 'arcgfirst'
            && this.altrankconditiongroupStopUpdateTimer <= 0) {
            fetch('api/Altrankconditiongroup', { credentials: 'include' })
                .then(response => response.json() as Promise<Altrankconditiongroup[]>)
                .then(data => {
                    //data.forEach(r => {
                    //});
                    this.altrankconditiongroups = data;

                })
        }
        if (this.altrankconditiongroupStopUpdateTimer > delayMinimum) {
            this.altrankconditiongroupStopUpdateTimer -= fetchAltrankconditiongroupDelay;
        }
    }

    fetchAltrankconditions() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameAltrankcondition === 'arcfirst'
            && this.altrankconditionStopUpdateTimer <= 0) {
            fetch('api/Altrankcondition', { credentials: 'include' })
                .then(response => response.json() as Promise<Altrankcondition[]>)
                .then(data => {
                    //data.forEach(r => {
                    //});
                    this.altrankconditions = data;

                })
        }
        if (this.altrankconditionStopUpdateTimer > delayMinimum) {
            this.altrankconditionStopUpdateTimer -= fetchAltrankconditionDelay;
        }
    }

    fetchStructureregions() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameStructureregion === 'srfirst'
            && this.structureregionStopUpdateTimer <= 0) {
            fetch('api/Structureregion', { credentials: 'include' })
                .then(response => response.json() as Promise<Structureregion[]>)
                .then(data => {
                    this.structureregions = data;
                })
        }
        if (this.structureregionStopUpdateTimer > delayMinimum) {
            this.structureregionStopUpdateTimer -= fetchStructureregionDelay;
        }
    }

    fetchStructuretypes() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameStructuretype === 'stfirst'
            && this.structuretypeStopUpdateTimer <= 0) {
            fetch('api/Structuretype', { credentials: 'include' })
                .then(response => response.json() as Promise<Structuretype[]>)
                .then(data => {
                    this.structuretypes = data.sort((s1, s2) => {
                        if (s1.name > s2.name) { return 1 };
                        if (s1.name < s2.name) { return -1 };
                        return 0;
                    });
                })
        }
        if (this.structuretypeStopUpdateTimer > delayMinimum) {
            this.structuretypeStopUpdateTimer -= fetchStructuretypeDelay;
        }
    }

    fetchIllcodes() {
        if ($('#adminpanel-container').is(':visible') && this.activeNameIllcode === 'icfirst'
            && this.illcodeStopUpdateTimer <= 0) {
            fetch('api/Illcode', { credentials: 'include' })
                .then(response => response.json() as Promise<Illcode[]>)
                .then(data => {
                    this.illcodes = data.sort((s1, s2) => {
                        if (s1.name > s2.name) { return 1 };
                        if (s1.name < s2.name) { return -1 };
                        return 0;
                    });
                })
        }
        if (this.illcodeStopUpdateTimer > delayMinimum) {
            this.illcodeStopUpdateTimer -= fetchIllcodeDelay;
        }
    }

    addRank(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Ranks', {
            method: 'post',
            body: JSON.stringify(<Rank>{
                name: this.newRankName,
                maxPeriod: this.new_rank_maximum_period,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                //this.addUserResult = response;
                //this.fetchUsers();
                (<any>Vue).notify(response);
                //Vue.$forceUpdate();
            });
        //alert('Добавление звания ' + this.newRankName);
    }

    addSof(event: any) {
        if (event) event.preventDefault();
        fetch('/api/SourceOfFinancing', {
            method: 'post',
            body: JSON.stringify(<Sourceoffinancing>{
                name: this.newSofName,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });
    }

    addSubject(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Subject', {
            method: 'post',
            body: JSON.stringify(<Subject>{
                id: this.newSubjectId,
                name: this.newSubjectName,
                name1: this.newSubjectName1,
                name2: this.newSubjectName2,
                name3: this.newSubjectName3,
                name4: this.newSubjectName4,
                name5: this.newSubjectName5,
                name6: this.newSubjectName6,
                gender: this.prepareNumToExport(this.newSubjectGender),
                category: this.prepareNumToExport(this.newSubjectCategory),
                dropword: this.boolToNumb(this.newSubjectDropword),
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.subjectStopUpdateTimer = 0; // Обнуляем, чтобы можно было произвести запрос subjects.
                this.fetchSubjects();
                (<any>Vue).notify(response);
            });
        this.updateSubjectCancel();
    }

    onSubjectName1Change() {
        this.newSubjectName = this.newSubjectName1;
        this.newSubjectName2 = this.newSubjectName1;
        this.newSubjectName3 = this.newSubjectName1;
        this.newSubjectName4 = this.newSubjectName1;
        this.newSubjectName5 = this.newSubjectName1;
        this.newSubjectName6 = this.newSubjectName1;
    }

    updateSubject(subject: Subject) {
        this.activeNameSubject = "susecond";
        this.newSubjectId = subject.id;
        this.newSubjectName = subject.name;
        this.newSubjectName1 = subject.name1;
        this.newSubjectName2 = subject.name2;
        this.newSubjectName3 = subject.name3;
        this.newSubjectName4 = subject.name4;
        this.newSubjectName5 = subject.name5;
        this.newSubjectName6 = subject.name6;
        this.newSubjectGender = subject.gender;
        this.newSubjectCategory = subject.category;
        this.newSubjectDropword = this.numberToBool(subject.dropword);

    }

    addeditSubjectName(): string {
        if (this.newSubjectId > 0) {
            return "Редактировать наименование";
        } else {
            return "Добавить наименование";
        }
    }

    updateSubjectCancel() {
        this.activeNameSubject = "sufirst";
        this.newSubjectId = 0;
        this.newSubjectName = "";
        this.newSubjectName1 = "";
        this.newSubjectName2 = "";
        this.newSubjectName3 = "";
        this.newSubjectName4 = "";
        this.newSubjectName5 = "";
        this.newSubjectName6 = "";
        this.newSubjectGender = 0;
        this.newSubjectCategory = 0;
        this.newSubjectDropword = false;
    }

    addPositiontype(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Positiontype', {
            method: 'post',
            body: JSON.stringify(<Positiontype>{
                name: this.newPositiontypeName,
                name2: this.newPositiontypeName2,
                name3: this.newPositiontypeName3,
                nameshort: this.newPositiontypeNameshort,
                priority: this.newPositiontypePriority,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updatePositiontypes");
                this.fetchPositiontypesForced();
                //this.$forceUpdate();
                
            });
    }

    addPositioncategory(event: any) {
        if (event) event.preventDefault();
        let positionvariableNum: number = 0;
        if (this.newPositioncategoryVariable) {
            positionvariableNum = 1;
        }
        let newPositioncategoryCivilNum: number = 0;
        if (this.newPositioncategoryCivil) {
            newPositioncategoryCivilNum = 1;
        }

        fetch('/api/Positioncategory', {
            method: 'post',
            body: JSON.stringify(<Positioncategory>{
                name: this.newPositioncategoryName,
                civil: newPositioncategoryCivilNum,
                variable: positionvariableNum,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                //this.addUserResult = response;
                //this.fetchUsers();
                (<any>Vue).notify(response);
                //Vue.$forceUpdate();
            });
        //alert('Добавление звания ' + this.newRankName);
    }

    addMrd(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Mrd', {
            method: 'post',
            body: JSON.stringify(<Mrd>{
                name: this.newMrd,
                short: this.newMrdShort,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });

    }

    addAltrankconditiongroup(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Altrankconditiongroup', {
            method: 'post',
            body: JSON.stringify(<Altrankconditiongroup>{
                name: this.newAltrankconditiongroup,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });

    }

    addAltrankcondition(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Altrankcondition', {
            method: 'post',
            body: JSON.stringify(<Altrankcondition>{
                name: this.newAltrankcondition,
                group: this.newAltrankconditionaltrankconditiongroup,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });
    }

    addStructureregion(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Structureregion', {
            method: 'post',
            body: JSON.stringify(<Structureregion>{
                name: this.newStructureregionName,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });
    }

    addStructuretype(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Structuretype', {
            method: 'post',
            body: JSON.stringify(<Structuretype>{
                name: this.newStructuretypeName,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });
    }

    addIllcode(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Illcode', {
            method: 'post',
            body: JSON.stringify(<Illcode>{
                name: this.newIllcodeName,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            });
    }


    getPositioncategoryByID(id: number) {
        return this.positioncategories.find(pc => pc.id == id).name;
    }

    logout() {
        (<any>Vue).logout();
    }

    handleClick(tab, event) {

    }

    handleClickRank(tab, event) {

    }

    handleClickIllcode(tab, event) {

    }

    handleClickSof(tab, event) {

    }

    handleClickSubject(tab, event) {
        if (this.activeNameSubject == "susecond") {
            this.newSubjectCategory = this.newSubjectCategoryFiltered;
        }
    }

    handleClickUser(tab, event) {

    }

    handleClickPositioncategory(tab, event) {

    }

    handleClickPositiontype(tab, event) {

    }

    handleClickMrd(tab, event) {

    }

    handleClickAltrankconditiongroup(tab, event) {

    }

    handleClickAltrankcondition(tab, event) {

    }

    handleClickStructuretype(tab, event) {

    }

    handleClickStructureregion(tab, event) {

    }

    updatePositiontype(positiontype: Positiontype) {
        fetch('/api/Positiontype', {
            method: 'post',
            body: JSON.stringify(<Positiontype>{
                id: positiontype.id,
                name: positiontype.name,
                name2: positiontype.name2,
                name3: positiontype.name3,
                nameshort: positiontype.nameshort,
                priority: positiontype.priority,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updatePositiontypes");
                this.fetchPositiontypesForced();
            });
        
    }

    editPositiontype(positiontype: Positiontype) {
        this.modalPositioneditMenuVisible = true;
        this.positiontypeid = positiontype.id;
        this.positiontypeObject = positiontype;
        //this.filteredSubjects = this.subjects;

        // Потом сюда можно записывать подгруз, если нужно будет
        if (positiontype.subject1 == 0 || positiontype.subject1 == null) {
            this.subject1 = null;
        } else {
            this.subject1 = positiontype.subject1;
        }

        if (positiontype.subject2 == 0 || positiontype.subject2 == null) {
            this.subject2 = null;
        } else {
            this.subject2 = positiontype.subject2;
        }

        if (positiontype.subject3 == 0 || positiontype.subject3 == null) {
            this.subject3 = null;
        } else {
            this.subject3 = positiontype.subject3;
        }

        if (positiontype.subject4 == 0 || positiontype.subject4 == null) {
            this.subject4 = null;
        } else {
            this.subject4 = positiontype.subject4;
        }

        if (positiontype.subject5 == 0 || positiontype.subject5 == null) {
            this.subject5 = null;
        } else {
            this.subject5 = positiontype.subject5;
        }

        if (positiontype.subject6 == 0 || positiontype.subject6 == null) {
            this.subject6 = null;
        } else {
            this.subject6 = positiontype.subject6;
        }

        if (positiontype.subject7 == 0 || positiontype.subject7 == null) {
            this.subject7 = null;
        } else {
            this.subject7 = positiontype.subject7;
        }

        if (positiontype.subject8 == 0 || positiontype.subject8 == null) {
            this.subject8 = null;
        } else {
            this.subject8 = positiontype.subject8;
        }

        if (positiontype.subject9 == 0 || positiontype.subject9 == null) {
            this.subject9 = null;
        } else {
            this.subject9 = positiontype.subject9;
        }

        if (positiontype.subject10 == 0 || positiontype.subject10 == null) {
            this.subject10 = null;
        } else {
            this.subject10 = positiontype.subject10;
        }

        if (positiontype.subject11 == 0 || positiontype.subject11 == null) {
            this.subject11 = null;
        } else {
            this.subject11 = positiontype.subject11;
        }

        if (positiontype.subject12 == 0 || positiontype.subject12 == null) {
            this.subject12 = null;
        } else {
            this.subject12 = positiontype.subject12;
        }

        if (positiontype.subject13 == 0 || positiontype.subject13 == null) {
            this.subject13 = null;
        } else {
            this.subject13 = positiontype.subject13;
        }

        if (positiontype.subject14 == 0 || positiontype.subject14 == null) {
            this.subject14 = null;
        } else {
            this.subject14 = positiontype.subject14;
        }

        if (positiontype.subject15 == 0 || positiontype.subject15 == null) {
            this.subject15 = null;
        } else {
            this.subject15 = positiontype.subject15;
        }

        if (positiontype.subject16 == 0 || positiontype.subject16 == null) {
            this.subject16 = null;
        } else {
            this.subject16 = positiontype.subject16;
        }

        if (positiontype.subject17 == 0 || positiontype.subject17 == null) {
            this.subject17 = null;
        } else {
            this.subject17 = positiontype.subject17;
        }

        if (positiontype.subject18 == 0 || positiontype.subject18 == null) {
            this.subject18 = null;
        } else {
            this.subject18 = positiontype.subject18;
        }

        if (positiontype.subject19 == 0 || positiontype.subject19 == null) {
            this.subject19 = null;
        } else {
            this.subject19 = positiontype.subject19;
        }

        if (positiontype.subject20 == 0 || positiontype.subject20 == null) {
            this.subject20 = null;
        } else {
            this.subject20 = positiontype.subject20;
        }

        this.filterMethod('');

        //positiontypeid: null,
        //    subject1: null,
        //    subject2: null,
        //    subject3: null,
        //    subject4: null,
        //    subject5: null,
        //    subject6: null,
        //    subject7: null,
        //    subject8: null,
        //    subject9: null,
        //    subject10: null,
        //    subject11: null,
        //    subject12: null,
        //    subject13: null,
        //    subject14: null,
        //    subject15: null,
        //    subject16: null,
        //    subject17: null,
        //    subject18: null,
        //    subject19: null,
        //    subject20: null,
    }

    updatePositiontypeDocument() {
        fetch('/api/Positions', {
            method: 'post',
            body: JSON.stringify(<PositionManagement>{
                positiontype: this.positiontypeid,
                type: "updatepositionsbypositiontype",
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
                subject16: this.prepareNumToExport(this.subject16),
                subject17: this.prepareNumToExport(this.subject17),
                subject18: this.prepareNumToExport(this.subject18),
                subject19: this.prepareNumToExport(this.subject19),
                subject20: this.prepareNumToExport(this.subject20),
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
            

        this.modalPositioneditMenuVisible = false;
    }

    

    updateStructuretype(structuretype: Structuretype) {
        fetch('/api/Structuretype', {
            method: 'post',
            body: JSON.stringify(<Structuretype>{
                id: structuretype.id,
                name: structuretype.name,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateStructuretypes");
            });

    }

    updateRank(rank: Rank) {
        fetch('/api/Ranks', {
            method: 'post',
            body: JSON.stringify(<Rank>{
                id: rank.id,
                name: rank.name,
                order: 0,
                positioncategory: rank.positioncategory,
                maxPeriod: rank.maxPeriod,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateRanks");
            });
    }

    upRank(rank: Rank) {
        fetch('/api/Ranks', {
            method: 'post',
            body: JSON.stringify(<Rank>{
                id: rank.id,
                name: rank.name,
                order: rank.order - 1,
                positioncategory: rank.positioncategory,
                maxPeriod: rank.maxPeriod,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateRanks");
            });
    }

    downRank(rank: Rank) {
        fetch('/api/Ranks', {
            method: 'post',
            body: JSON.stringify(<Rank>{
                id: rank.id,
                name: rank.name,
                order: rank.order + 1,
                positioncategory: rank.positioncategory,
                maxPeriod: rank.maxPeriod,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateRanks");
            });
    }

    updateMrd(mrd: Mrd) {
        fetch('/api/Mrd', {
            method: 'post',
            body: JSON.stringify(<Mrd>{
                id: mrd.id,
                name: mrd.name,
                short: mrd.short,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateMrds");
            });
    }

    updateStructureregion(structureregion: Structureregion) {
        fetch('/api/Structureregion', {
            method: 'post',
            body: JSON.stringify(<Structureregion>{
                id: structureregion.id,
                name: structureregion.name,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateStructureregions");
            });

    }

    updateAltrankconditiongroup(altrankconditiongroup: Altrankconditiongroup) {
        fetch('/api/Altrankconditiongroup', {
            method: 'post',
            body: JSON.stringify(<Altrankconditiongroup>{
                id: altrankconditiongroup.id,
                name: altrankconditiongroup.name,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateAltrankconditiongroups");
            });

    }

    updateAltrankcondition(altrankcondition: Altrankcondition) {
        fetch('/api/Altrankcondition', {
            method: 'post',
            body: JSON.stringify(<Altrankcondition>{
                id: altrankcondition.id,
                name: altrankcondition.name,
                group: altrankcondition.group,
            }),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
                this.$store.commit("updateAltrankconditions");
            });
    }

    /**
     * Включаем режим выбора подразделения при редактировании/добавлении пользователя 
     **/
    addHeading() {
        this.headingselectionprocess = true;
        this.$store.commit("setModeselectheading", true);
        //this.$emit('update:visible', false);
    }

    /**
     * Убираем выбранное подразделение при редактировании/добавлении пользователя. 
     **/
    removeHeading() {
        this.headid = 0;
        this.headingStructureTree = null;
        if (this.newUser != null) {
            this.newUser.structure = 0; 
            //this.newUser.structure = ""; 
            // this.headid.toString()
        }
    }


    prepareTrees() {
        let getString: string = "";
        let structureid: number = this.newUser.structure;
        //let structureid: number = Number.parseInt(this.newUser.structure);
        if (this.newUser != null && structureid > 0) {
        //if (this.headid > 0) {
            fetch('api/DetailedStructure/Trees' + structureid, { credentials: 'include' })
            //fetch('api/DetailedStructure/Trees' + this.headid, { credentials: 'include' })
                .then(response => response.json() as Promise<StructureTree[]>)
                .then(data => {
                    if (data.length > 0) {
                        this.headingStructureTree = data[0];
                    }

                });
        }
    }

    userEditMode(): boolean {
        return (this.newUser != null && this.newUser.id > 0);
        //return this.newUserEditid > 0;
    }

    userAddLabel(): string {
        if (this.userEditMode()) {
            return "Редактировать пользователя";
        } else {
            return "Добавить пользователя";
        }
    }

    userEditStart(user: User) {
        this.newUser = user;

        //this.newUserEditid = user.id;
        //this.newUserName = user.name;
        //this.newUserAdmin = user.admin;
        //this.headid = Number.parseInt(user.structure);
        //this.newUserMasterPersonnelEditor = user.masterpersonneleditor;
        //this.newUserPersonnelEditor = user.personneleditor;
        //this.newUserStructureEditor = user.structureeditor;
        //this.newUserPersonnelRead = user.personnelread;
        //this.newUserStructureRead = user.structureread;
        //this.newUserFirstname = user.firstname;
        //this.newUserSurname = user.surname;
        //this.newUserPatronymic = user.patronymic;
        //this.newUserPositiontype = user.positiontype;

        this.prepareTrees();
        this.activeNameUser = "usecond";
    }

    userEditComplete() {
        //alert('add');
        if (this.newUserPositiontype == null) {
            this.newUserPositiontype = 0;
        }

        fetch('/api/Users', {
            method: 'post',
            body: JSON.stringify(this.newUser),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                this.addUserResult = response;
                this.fetchUsers(true);
                (<any>Vue).notify(response);
                //Vue.$forceUpdate();
            });

        this.activeNameUser = "ufirst";
        this.newUser = new User();

        //fetch('/api/Users', {
        //    method: 'post',
        //    body: JSON.stringify(<User>{
        //        name: this.newUserName, admin: this.newUserAdmin, structure: this.headid.toString(), masterpersonneleditor: this.newUserMasterPersonnelEditor,
        //        personneleditor: this.newUserPersonnelEditor, structureeditor: this.newUserStructureEditor, personnelread: this.newUserPersonnelRead, structureread: this.newUserStructureRead,
        //        firstname: this.newUserFirstname, surname: this.newUserSurname, patronymic: this.newUserPatronymic, positiontype: this.newUserPositiontype,
        //        id: this.newUserEditid, // Если айдишник есть
        //    }),
        //    credentials: 'include',
        //    headers: new Headers({
        //        'Accept': 'application/json',
        //        'Content-Type': 'application/json'
        //    })
        //})
        //    .then(response => { return response.json(); })
        //    .then((response) => {
        //        this.addUserResult = response;
        //        this.fetchUsers(true);
        //        (<any>Vue).notify(response);
        //        //Vue.$forceUpdate();
        //    });

        //this.activeNameUser = "ufirst";
        //this.newUserEditid = 0;
        ////this.fetchUsers();
    }

    /**
     * Отменяем редактирование пользователя (если нажимали кнопочку редактировать). 
     **/
    userEditCancel() {

        this.activeNameUser = "ufirst";
        this.newUser = new User();
        this.removeHeading();
        //this.newUserEditid = 0;
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

    hasRightString(hasright: boolean): string {
        if (hasright) {
            return "+";
        }
        return "-";
    }

    boolToNumb(bool: boolean): number {
        if (bool != null && bool) {
            return 1;
        } else {
            return 0;
        }
    }

    numberToBool(numb: number): boolean {
        if (numb != null && numb > 0) {
            return true;
        } else {
            return false;
        }
    }

    filterMethod(query) {
        if (query == null) {
            query = "";
        }
        //this.filteredSubjects = this.subjects.filter(option => {
        //    return option.name1.toLowerCase().startsWith(query.toLowerCase());
        //})
        
        this.filteredSubjects = this.subjects.filter(option => {
            return (option.category == 1 || option.category == 7) && option.name1.toLowerCase().startsWith(query.toLowerCase());
        }).slice(0, 20);
        
        if (this.subject1 != null && this.subject1 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject1));
        }
        if (this.subject2 != null && this.subject2 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject2));
        }
        if (this.subject3 != null && this.subject3 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject3));
        }
        if (this.subject4 != null && this.subject4 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject4));
        }
        if (this.subject5 != null && this.subject5 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject5));
        }
        if (this.subject6 != null && this.subject6 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject6));
        }
        if (this.subject7 != null && this.subject7 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject7));
        }
        if (this.subject8 != null && this.subject8 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject8));
        }
        if (this.subject9 != null && this.subject9 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject9));
        }
        if (this.subject10 != null && this.subject10 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject10));
        }
        if (this.subject11 != null && this.subject11 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject11));
        }
        if (this.subject12 != null && this.subject12 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject12));
        }
        if (this.subject13 != null && this.subject13 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject13));
        }
        if (this.subject14 != null && this.subject14 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject14));
        }
        if (this.subject15 != null && this.subject15 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject15));
        }
        if (this.subject16 != null && this.subject16 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject16));
        }
        if (this.subject17 != null && this.subject17 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject17));
        }
        if (this.subject18 != null && this.subject18 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject18));
        }
        if (this.subject19 != null && this.subject19 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject19));
        }
        if (this.subject20 != null && this.subject20 > 0) {
            this.filteredSubjects.push(this.subjects.find(s => s.id == this.subject20));
        }

        this.filteredSubjects = [...new Set(this.filteredSubjects)];
    }

    fatherSubjectcategoryid(subjectcategoryid: number): number {
        let subjectcategory: Subjectcategory = this.subjectcategories.find(s => s.id == subjectcategoryid);
        return subjectcategory.category;
    }

    selectPositionRights() {
        fetch('api/Rights/Position/' + this.rightsPositiontype, { credentials: 'include' })
            .then(response => response.json() as Promise<Rights>)
            .then(data => {
                data.position = this.rightsPositiontype;
                this.rightsRights = data;
            });
    }

    updatePositiontypeRights() {
        fetch('/api/Rights', {
            method: 'post',
            body: JSON.stringify(this.rightsRights),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                //this.fetchUsers(true);
                (<any>Vue).forceStructureUpdate = true;
                (<any>Vue).notify(response);
            });
    }

    /**
     * Для случаев, когда нужно обновить поля в объекте, вложенном в другой объект. Например, поле user.rights.admin. Автоматическое обновление не работает/работает плохо 
     **/
    forceUpdate() {
        this.$forceUpdate();
    }

    /**
     * Когда мы подгружаем positiontype в меню добавления/редактирования пользователя, пробуем симпортировать все настройки с него в newUser
     **/
    selectUserPositiontype() {
        fetch('api/Rights/Position/' + this.newUser.positiontype, { credentials: 'include' })
            .then(response => response.json() as Promise<Rights>)
            .then(data => {
                //data.position = this.rightsPositiontype;
                //this.rightsRights = data;
                this.newUser.rights = data;
            });

        this.forceUpdate();
    }

    updateRightsMenu(rights: Rights, menu: number) {
        rights.menu = menu;
        this.forceUpdate();
    }

    grantEldAllAccess(rights: Rights) {
        rights.peopleedit = 1;
        rights.peopleread = 1;
        rights.peoplereadall = 1;
        rights.peopleeditmain = 1;
        rights.peoplereadmain = 1;
        rights.peopleeditpassport = 1;
        rights.peoplereadpassport = 1;
        rights.peopleeditphoto = 1;
        rights.peoplereadphoto = 1;
        rights.peopleeditcertificate = 1;
        rights.peoplereadcertificate = 1;
        rights.peopleeditrelative = 1;
        rights.peoplereadrelative = 1;
        rights.peopleediteducation = 1;
        rights.peoplereadeducation = 1;
        rights.peopleediteducationucp = 1;
        rights.peoplereadeducationucp = 1;
        rights.peopleeditjob = 1;
        rights.peoplereadjob = 1;
        rights.peopleeditjobprivelege = 1;
        rights.peoplereadjobprivelege = 1;
        rights.peopleeditjobpension = 1;
        rights.peoplereadjobpension = 1;
        rights.peopleeditill = 1;
        rights.peoplereadill = 1;
        rights.peopleeditdispanserization = 1;
        rights.peoplereaddispanserization = 1;
        rights.peopleeditvvk = 1;
        rights.peoplereadvvk = 1;
        rights.peopleeditpfl = 1;
        rights.peoplereadpfl = 1;
        rights.peopleeditrank = 1;
        rights.peoplereadrank = 1;
        rights.peopleeditcontract = 1;
        rights.peoplereadcontract = 1;
        rights.peopleeditcontractvacation = 1;
        rights.peoplereadcontractvacation = 1;
        rights.peopleeditcontractstate = 1;
        rights.peoplereadcontractstate = 1;
        rights.peopleeditvacation = 1;
        rights.peoplereadvacation = 1;
        rights.peopleeditreward = 1;
        rights.peoplereadreward = 1;
        rights.peopleeditattestation = 1;
        rights.peoplereadattestation = 1;
        rights.peopleeditlanguage = 1;
        rights.peoplereadlanguage = 1;
        rights.peopleeditscience = 1;
        rights.peoplereadscience = 1;
        rights.peopleeditelection = 1;
        rights.peoplereadelection = 1;
        rights.peopleeditworktrip = 1;
        rights.peoplereadworktrip = 1;
        rights.peopleeditpenalty = 1;
        rights.peoplereadpenalty = 1;
        rights.peopleeditphysical = 1;
        rights.peoplereadphysical = 1;
        rights.peopleeditdriver = 1;
        rights.peoplereaddriver = 1;
        rights.peopleeditpermission = 1;
        rights.peoplereadpermission = 1;
        rights.peopleeditprivelege = 1;
        rights.peoplereadprivelege = 1;
        rights.peopleeditwound = 1;
        rights.peoplereadwound = 1;
        this.forceUpdate();
    }
}