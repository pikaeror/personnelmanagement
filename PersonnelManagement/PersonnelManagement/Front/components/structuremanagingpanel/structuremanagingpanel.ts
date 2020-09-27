import Vue from 'vue';
import { Component, Prop, Watch} from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Checkbox, InputNumber } from 'element-ui';
import Structureregion from '../../classes/structureregion';
import Structuretype from '../../classes/structuretype';
import StructureTree from '../../classes/structuretree';
import Subject from '../../classes/subject';
import Subjectgender from '../../classes/subjectgender';
Vue.component(Button.name, Button);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Input.name, Input);
Vue.component(InputNumber.name, InputNumber);
Vue.use(Element);

//public string Type { get; set; }
//public string Name { get; set; }
//public int ? Parent { get; set; }

class StructureManagement {
    id: number;
    type: string;
    name: string;
    name1: string;
    name2: string;
    name3: string;
    nameshortened: string;
    featured: boolean;
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

})
export default class StructuremanagingpanelComponent extends Vue {
    id: number;
    status: string;
    name: string;
    name1: string;
    name2: string;
    name3: string;
    nameshortened: string;
    featured: boolean;
    datecustom: boolean;
    dateactive: string;
    rank: any;
    structureregion: any;
    structureregions: Structureregion[];
    structuretype: any;
    structuretypes: Structuretype[];
    structuretypesiblings: boolean;
    city: string;
    street: string;
    nodecree: boolean;
    separatestructure: boolean;

    structurelist: number;
    structureTree: StructureTree;
    structureselectionprocess: boolean;
    // Statuses
    // addnewstructure 
    // removestructure
    // renamestructure
    // removestructuredecree
    // renamestructuredecree

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

    @Prop({ default: "" })
    parent: string;

    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: false })
    visiblevar: boolean;

    data() {
        return {
            id: 0,
            status: "renamestructure",
            name: "",
            name1: "",
            name2: "",
            name3: "",
            nameshortened: "",
            featured: false,
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

    get subjects(): Subject[] {
        return this.$store.state.subjects;
    }

    subjectsFilteredByGender(subjectgenderid: number): Subject[] {
        if (subjectgenderid == null || subjectgenderid == 0) {
            return this.subjects;
        }
    }

    get intromessage() {
        switch (this.type) {
            case "addnewstructure": return "Создать подразделение";
            case "removestructure": return "Вы уверены, что хотите упразднить подразделение?";
            case "renamestructure": return "Изменить подразделение";
            case "renamestructurenodecree": return "Изменить подразделение (без приказа)";
            case "removestructuredecree": return "Убрать из проекта приказа";
            case "renamestructuredecree": return "Редактировать подразделение";
            default: return "Произошла непредвиденная оказия";
        }
        //return "Произошла какая-то дристня";
    }

    get getplaceholdername() {
        switch (this.type) {
            case "addnewstructure": return "Название подразделения";
            case "removestructure": return "Название подразделения";
            case "renamestructure": return "Новое название подразделения";
            case "renamestructurenodecree": return "Новое название подразделения";
            case "removestructuredecree": return "Название подразделения";
            case "renamestructuredecree": return "Название подразделения";
            default: return "Произошла непредвиденная оказия";
        }
        //return "Произошла какая-то дристня";
    }

    get displayinput() {
        switch (this.type) {
            case "addnewstructure": return false;
            case "removestructure": return true;
            case "renamestructure": return false;
            case "renamestructurenodecree": return false;
            case "removestructuredecree": return true;
            case "renamestructuredecree": return false;
            default: return true;
        }
    }

    displayCheckbox() {
        switch (this.type) {
            case "addnewstructure": return true;
            case "removestructure": return false;
            case "renamestructure": return false;
            case "renamestructurenodecree": return false;
            case "removestructuredecree": return false;
            case "renamestructuredecree": return true;
            default: return false;
        }
    }

    displayDatecustom() {
        switch (this.type) {
            case "addnewstructure": return true;
            case "removestructure": return true;
            case "renamestructure": return true;
            case "renamestructurenodecree": return true;
            case "removestructuredecree": return false;
            case "renamestructuredecree": return true;
            default: return false;
        }
    }

    nodecreemode() {
        return this.type == "renamestructurenodecree";
    }

    mounted() {
        this.structureregions = this.$store.state.structureregions;
        this.structuretypes = this.$store.state.structuretypes;

        if (this.type == "renamestructuredecree" || this.type == "renamestructure" || this.type == "renamestructurenodecree") {
            this.loadStructure();
        } else {
            this.structurelist = Number.parseInt(this.parent);
            if (this.structurelist == null || Number.isNaN(this.structurelist)) {
                this.structurelist = 0;
            }
            if (this.structurelist == 0) {
                this.removeStructure();
            } else {
                this.prepareTrees();
            }
        }
        if (this.nodecreemode()) {
            this.nodecree = true;
        }

        this.filteredSubjects = this.subjects.filter(option => (option.category == 2 || option.category == 8));
        (<any>this.$refs.inputstructuremanagementname).focus();
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value && this.structureselectionprocess) {
            if (this.$store.state.modeselectedstructure > 0) {
                this.structurelist = this.$store.state.modeselectedstructure;
                this.$store.commit("setModeselectedstructure", 0);
            }
            this.prepareTrees();
            this.structureselectionprocess = false;
            return;
        }
        if (this.structureselectionprocess) {
            return;
        }

        this.id = 0;
        this.name = "";
        this.name1 = "";
        this.name2 = "";
        this.name3 = "";
        this.nameshortened = "";
        this.featured = false;
        this.rank = null;
        this.structureregion = null;
        this.structuretype = null;
        this.structuretypesiblings = false;
        this.city = "";
        this.street = "";
        this.nodecree = false;
        this.separatestructure = false;
        this.subject1 = null;
        this.subject2 = null;
        this.subject3 = null;
        this.subject4 = null;
        this.subject5 = null;
        this.subject6 = null;
        this.subject7 = null;
        this.subject8 = null;
        this.subject9 = null;
        this.subject10 = null;
        this.subject11 = null;
        this.subject12 = null;
        this.subject13 = null;
        this.subject14 = null;
        this.subject15 = null;
        this.subjectnumber = null;
        this.subjectnotice = "";
        this.subjectgender = null;

        this.datecustom = false;
        this.dateactive = this.toDateInputValue(new Date());

        if (value && (this.type == "renamestructuredecree" || this.type == "renamestructure" || this.type == "renamestructurenodecree")) {
            this.loadStructure();
        } else {
            this.structurelist = Number.parseInt(this.parent);
            if (this.structurelist == null || Number.isNaN(this.structurelist)) {
                this.structurelist = 0;
            }
            if (this.structurelist == 0) {
                this.removeStructure();
            } else {
                this.prepareTrees();
            }
            
        }
        if (this.nodecreemode()) {
            this.nodecree = true;
        }
        (<any>this.$refs.inputstructuremanagementname).focus();
    }

    okbutton() {
        let datecustomNum: number = 0;
        if (this.datecustom) {
            datecustomNum = 1;
        }

        if (this.rank == null || this.rank == "") {
            this.rank = 0;
        }
        if (this.structureregion == null || this.structureregion == "") {
            this.structureregion = 0;
        }
        if (this.structuretype == null || this.structuretype == "") {
            this.structuretype = 0;
        }

        this.id = Number.parseInt(this.parent); // Not to store id in parent;
        //this.structurelist - parent
        //alert(this.parent);

        let nodecreeNum: number = 0;
        if (this.nodecree || this.type == "renamestructurenodecree") {
            nodecreeNum = 1; 
        }

        let separatestructureNum: number = 0;
        if (this.separatestructure) {
            separatestructureNum = 1;
        }

        let structuretypesiblingsNum: number = 0;
        if (this.structuretypesiblings) {
            structuretypesiblingsNum = 1;
        }

        fetch('/api/DetailedStructure', {
            method: 'post',
            body: JSON.stringify(<StructureManagement>{
                id: this.id, type: this.type, parent: this.structurelist, name: this.name, featured: this.featured, nameshortened: this.nameshortened, datecustom: datecustomNum, dateactive: new Date(this.dateactive),
                name1: this.name1, name2: this.name2, name3: this.name3, separatestructure: separatestructureNum,
                rank: this.rank, structureregion: this.structureregion, structuretype: this.structuretype, city: this.city, street: this.street, nodecree: nodecreeNum, structuretypesiblings: structuretypesiblingsNum,
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

    cancelbutton() {
        this.$emit('update:visiblevar', false);
        //this.visible = false;
        //alert(this.dialogvisible);
    }

    toDateInputValue(date: Date): string {
        var local = new Date(date);
        local.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

    loadStructure() {
        fetch('api/DetailedStructure/Solo' + this.parent, { credentials: 'include' })
            .then(response => response.json() as Promise<StructureManagement>)
            .then(data => {
                this.name = data.name;
                this.name1 = data.name1;
                this.name2 = data.name2;
                this.name3 = data.name3;
                if (data.featured) {
                    this.featured = true;
                }
                this.nameshortened = data.nameshortened;
                if (data.datecustom > 0) {
                    this.datecustom = true;
                    this.dateactive = this.toDateInputValue(data.dateactive);
                }
                this.id = data.id;
                this.parent = data.id.toString();
                this.structurelist = data.parentstructure;
                //alert(data.parent);
                if (this.structurelist == null || Number.isNaN(this.structurelist)) {
                    this.structurelist = 0;
                }
                if (this.structurelist == 0) {
                    this.removeStructure();
                } else {
                    this.prepareTrees();
                }

                if (data.separatestructure > 0) {
                    this.separatestructure = true;
                } else {
                    this.separatestructure = false;
                }
                
                this.street = data.street;
                this.city = data.city;
                if (data.rank > 0) {
                    this.rank = data.rank;
                }
                if (data.structureregion > 0) {
                    this.structureregion = data.structureregion;
                }
                if (data.structuretype > 0) {
                    this.structuretype = data.structuretype;
                }

                /**
                 * Для прохождения службы 
                 */
                if (data.subject1 == 0) {
                    this.subject1 = null;
                } else {
                    this.subject1 = data.subject1;
                }

                if (data.subject2 == 0) {
                    this.subject2 = null;
                } else {
                    this.subject2 = data.subject2;
                }

                if (data.subject3 == 0) {
                    this.subject3 = null;
                } else {
                    this.subject3 = data.subject3;
                }

                if (data.subject4 == 0) {
                    this.subject4 = null;
                } else {
                    this.subject4 = data.subject4;
                }

                if (data.subject5 == 0) {
                    this.subject5 = null;
                } else {
                    this.subject5 = data.subject5;
                }

                if (data.subject6 == 0) {
                    this.subject6 = null;
                } else {
                    this.subject6 = data.subject6;
                }

                if (data.subject7 == 0) {
                    this.subject7 = null;
                } else {
                    this.subject7 = data.subject7;
                }

                if (data.subject8 == 0) {
                    this.subject8 = null;
                } else {
                    this.subject8 = data.subject8;
                }

                if (data.subject9 == 0) {
                    this.subject9 = null;
                } else {
                    this.subject9 = data.subject9;
                }

                if (data.subject10 == 0) {
                    this.subject10 = null;
                } else {
                    this.subject10 = data.subject10;
                }

                if (data.subject11 == 0) {
                    this.subject11 = null;
                } else {
                    this.subject11 = data.subject11;
                }

                if (data.subject12 == 0) {
                    this.subject12 = null;
                } else {
                    this.subject12 = data.subject12;
                }

                if (data.subject13 == 0) {
                    this.subject13 = null;
                } else {
                    this.subject13 = data.subject13;
                }

                if (data.subject14 == 0) {
                    this.subject14 = null;
                } else {
                    this.subject14 = data.subject14;
                }

                if (data.subject15 == 0) {
                    this.subject15 = null;
                } else {
                    this.subject15 = data.subject15;
                }

                if (data.subjectnumber == 0) {
                    this.subjectnumber = null;
                } else {
                    this.subjectnumber = data.subjectnumber;
                }

                if (data.subjectgender == 0) {
                    this.subjectgender = null;
                } else {
                    this.subjectgender = data.subjectgender;
                }

                this.subjectnotice = data.subjectnotice;
            });
    }

    prepareTrees() {
        fetch('api/DetailedStructure/Trees' + this.structurelist, { credentials: 'include' })
            .then(response => response.json() as Promise<StructureTree[]>)
            .then(data => {
                this.structureTree = data[0];

            });
    }

    addStructure() {
        this.structureselectionprocess = true;
        this.$store.commit("setModeselectstructure", true);
        //this.$emit('update:visible', false);
    }

    removeStructure() {
        this.structurelist = 0;
        this.structureTree = null;
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

    onSubjectChange(subjectid: number) {
        let subject: Subject = this.getSubject(subjectid);
        if (subject == null) {
            this.subjectgender = 0;
        } else {
            this.subjectgender = subject.gender;
        }
    }

    filterByGender(subject: Subject): boolean {
        if (this.subjectgender == null || this.subjectgender == 0) { // Если нет записей, которые устанавливали бы гендер
            return true;
        }
        if (subject.gender == null || subject.gender == 0) { // Пропускать, если у части не установлен гендер.
            return true;
        }
        if (subject.gender == this.subjectgender) {
            return true; // Если гендеры совпадают
        }
        return false;
    }

    filterMethod(query) {
        if (query == null) {
            query = "";
        }
        //this.filteredSubjects = this.subjects.filter(option => {
        //    return option.name1.toLowerCase().startsWith(query.toLowerCase());
        //})

        this.filteredSubjects = this.subjects.filter(option => {
            return (option.category == 2 || option.category == 8) && option.name1.toLowerCase().startsWith(query.toLowerCase());
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

        this.filteredSubjects = [...new Set(this.filteredSubjects)];
    }
}