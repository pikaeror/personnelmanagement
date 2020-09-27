import Vue from 'vue';
import { Component, Prop } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, DropdownItem, DropdownMenu } from 'element-ui';
import Decreeoperationsrequest from '../../classes/decreeoperationsrequest';
import Decreeoperation from '../../classes/decreeoperation';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Dropdown.name, Dropdown);
Vue.component(DropdownItem.name, DropdownItem);
Vue.component(DropdownMenu.name, DropdownMenu);
Vue.use(Element);


class Department {
    id: number;
    name: string
    department: number
}

const subordinateStr: string = "subordinate";
const fetchDepartmentDelay: number = 6000;
const fetchDepartmentForceDelay: number = 250;

Vue.component('Departmentmanagingpanel', require('../departmentmanagingpanel/departmentmanagingpanel.vue.html'));

@Component({
    components: {

    }
})
export default class DepartmentslistComponent extends Vue {

    @Prop({ default: 0 })
    visible: number;

    departments: Department[];
    decreeoperations: Decreeoperation[];
    userStructure: string;
    masterpersonneleditorAccess: string;
    structureeditorAccess: string;
    personneleditorAccess: string;
    hasAccessToEdit: boolean;

    addNewDepartmentAvailable: boolean;
    addNewDepartment: string;

    removeDepartmentAvailable: boolean;
    removeDepartment: string;

    renameDepartmentAvailable: boolean;
    renameDepartment: string;

    modalDepartmentManagingPanelVisible: boolean;
    departmentManagingType: string;
    departmentManagingParent: string;
    departmentManagingStructure: string;
    title: string;

    data() {
        return {
            title: "Список отделов",
            departments: [],
            userStructure: "0",
            masterpersonneleditorAccess: "0",
            structureeditorAccess: "0",
            personneleditorAccess: "0",
            hasAccessToEdit: false,

            addNewDepartmentAvailable: false,
            addNewDepartment: "addnewdepartment",

            removeDepartmentAvailable: false,
            removeDepartment: "removedepartment",

            renameDepartmentAvailable: false,
            renameDepartment: "renamedepartment",

            modalDepartmentManagingPanelVisible: false,
            departmentManagingType: "",
            departmentManagingParent: "",
            departmentManagingStructure: "",


        }
    }

    mounted() {
        setInterval(this.forceFetch, fetchDepartmentForceDelay); 
        setInterval(this.fetchDepartments, fetchDepartmentDelay); 
    }

    async getUserData() {
        await (<any>Vue).getAccessStatus().then(s => {
            this.userStructure = s[(<any>Vue).keys["IDENTITY_STRUCTURE_KEY"]];
            this.masterpersonneleditorAccess = s[(<any>Vue).keys["IDENTITY_MASTERPERSONNELEDITOR_KEY"]];
            this.structureeditorAccess = s[(<any>Vue).keys["IDENTITY_STRUCTUREEDITOR_KEY"]];
            this.personneleditorAccess = s[(<any>Vue).keys["IDENTITY_PERSONNELEDITOR_KEY"]];
            //alert(this.userStructure);
        }).then(fetch('api/Departments/', { credentials: 'include' })
                .then(response => response.json() as Promise<boolean>)
                .then(data => {
                    this.hasAccessToEdit = data;
                    if (this.$store.state.decree == null || this.$store.state.decree == 0) {
                        this.hasAccessToEdit = false;
                    }
                })
        )
    }

    get canEdit() {
        if (this.hasAccessToEdit) {
            return "true";
        } else {
            return "false";
        }
    }


    fetchDepartments() {
        if (this.visible) {
            this.getUserData().then(x =>
                fetch('api/Departments/' + this.$store.state.departmentsListId, { credentials: 'include' })
                    .then(response => response.json() as Promise<Department[]>)
                    .then(data => {
                        this.departments = data;
                        //this.title = this.$store.state.departmentsListTitle;
                    })
            ).then( x => (<any>Vue).getStructureAll()
                ).then(x => {
                    this.title = x[this.$store.state.departmentsListId];
                }
            ).then(x => {
                /**
                 * Get decree operations for departments
                 */
                let operationsRequest: Decreeoperationsrequest[] = new Array<Decreeoperationsrequest>();
                let currentDateStart: Date = new Date(this.$store.state.date);
                this.departments.forEach(s => {
                    let decreeoperationsrequest: Decreeoperationsrequest = new Decreeoperationsrequest();
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
                        this.decreeoperations = response;
                        //alert(JSON.stringify(response));
                    });
            })
        }

    }

    forceFetch() {
        if (this.$store.state.forceDepartmentUpdate) {
            this.$store.commit("setForceDepartmentUpdate", false);
            this.fetchDepartments();
        } 
    }

    close() {
        this.$store.commit("setDepartmentsListId", 0);
        //this.$store.commit("setPositionsListId", 0);
    }

    addDepartment() {
        this.modalDepartmentManagingPanelVisible = true;
        this.departmentManagingType = "addnewdepartment";
        this.departmentManagingParent = null;
        this.departmentManagingStructure = this.$store.state.departmentsListId;
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


    // 0 - no purpose, 1 - no purpose not signed, 
    // 2 - will create subject in future, 3 - will create subject in future not signed,
    // 4 - already created subject, 5 - already created subject not signed, 6 - will delete subject in future
    // 7 - will delete subject in future not signed,
    // 12 - deleted, 13 - deleted not signed, 14 - renamed not signed, 15 - will be renamed,
    // 16 - will be renamed not signed
    isSignedAndCreated(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 4).length > 0;
        } else {
            return false;
        }
    }

    isNotSignedAndCreated(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 5).length > 0;
        } else {
            return false;
        }
    }

    isSignedAndWillBeCreated(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 2).length > 0;
        } else {
            return false;
        }
    }

    isNotSignedAndWillBeCreated(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 3).length > 0;
        } else {
            return false;
        }
    }

    isDeletedSigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 12).length > 0;
        } else {
            return false;
        }
    }

    isDeletedUnsigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 13).length > 0;
        } else {
            return false;
        }
    }

    isWillBeDeletedSigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 6).length > 0;
        } else {
            return false;
        }
    }

    isWillBeDeletedUnsigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 7).length > 0;
        } else {
            return false;
        }   
    }

    isRenamedNotSigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 14).length > 0;
        } else {
            return false;
        }   
    }

    isWillBeRenamed(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 15).length > 0;
        } else {
            return false;
        }   
    }

    isWillBeRenamedNotSigned(department: Department): any {
        if (this.decreeoperations != null) {
            return this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 16).length > 0;
        } else {
            return false;
        }   
    }

    getDecreeName(department: Department): any {
        if (this.decreeoperations != null) {
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 4).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 4).metaDecreeName;
            }
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 2).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 2).metaDecreeName;
            }
        } 
        return "";
    }

    getDecreeStartDate(department: Department): any {
        if (this.decreeoperations != null) {
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 4).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 4).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 2).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 2).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 5).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 5).metaDateActive.toString().slice(0, 10);
            }
            if (this.decreeoperations.filter(d => d.subject == department.id && d.metaPurposeForSubject == 3).length > 0) {
                return this.decreeoperations.find(d => d.subject == department.id && d.metaPurposeForSubject == 3).metaDateActive.toString().slice(0, 10);
            }
        }
        
        return "";
    }


    handleCommand(command: string) {
        if (command.startsWith(this.addNewDepartment)) {
            this.modalDepartmentManagingPanelVisible = true;
            this.departmentManagingType = "addnewdepartment";
            this.departmentManagingParent = command.split('_')[1];
        } else if (command.startsWith(this.removeDepartment)) {
            this.modalDepartmentManagingPanelVisible = true;
            this.departmentManagingType = "removedepartment";
            this.departmentManagingParent = command.split('_')[1];
        } else if (command.startsWith(this.renameDepartment)) {
            this.modalDepartmentManagingPanelVisible = true;
            this.departmentManagingType = "renamedepartment";
            this.departmentManagingParent = command.split('_')[1];
        }
    }

    openDepartment(event: any, id: number, name: string) {
        if (event) event.preventDefault();
        this.$store.commit("setPositionsListId", id);
        this.$store.commit("setPositionsListTitle", name);
        this.$store.commit("setForcePositionUpdate", true);
        //alert('Послушай, скажи мне друг. Ты всюду был, ты знаешь всё на свете. Не то что я, гуляка человек.' );
    }

}
