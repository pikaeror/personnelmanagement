import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import download from 'downloadjs';
import Rank from '../../classes/rank';
import Sourceoffinancing from '../../classes/sourceoffinancing';
import Positiontype from '../../classes/positiontype';
import Positioncategory from '../../classes/positioncategory';
import Mrd from '../../classes/mrd';
import Positionmrd from "../../classes/positionmrd";
import Altrank from '../../classes/altrank';
import Altrankcondition from '../../classes/altrankcondition';
import Altrankconditiongroup from '../../classes/altrankconditiongroup';
import StructureTree from '../../classes/structuretree';
import Position from '../../classes/position';
import Subject from '../../classes/subject';
import Subjectcategory from '../../classes/subjectcategory';
import Subjectgender from '../../classes/subjectgender';
import { Input, Button, Checkbox, Select, Option } from 'element-ui';
Vue.component(Button.name, Button);
Vue.component(Input.name, Input);
Vue.component(Checkbox.name, Checkbox);
Vue.component(Select.name, Select);
Vue.component(Option.name, Option);
Vue.use(Element);

//public string Type { get; set; }
//public string Name { get; set; }
//public int ? Parent { get; set; }

class PositionManagement {
    type: string;
    department: number;
    name: string;
    rankCap: number;
    sof: number;
    id: number;
    positiontype: number;
    positioncategory: number;
    mrd: string;
    quantity: number;
    notice: string;
    datecustom: number;
    dateactive: Date;
    replacedbycivil: number;
    replacedbycivilpositioncategory: any;
    replacedbycivilpositiontype: number;
    altrankconditiongroup: number; // 0 if no alt ranks.
    altranks: string; // If altrankconditiongroup is not null, contains next information "conditionid=rank;conditionid2=rank2;..". For example, "3=7;4=8"
    decertificate: number;
    decertificatedate: Date;
    civilranklow: number;
    civilrankhigh: number;
    replacedbycivildatelimit: number;
    replacedbycivildate: Date;
    positionsCode: string; // For multiple positions converted to JSON.
    curator: number;
    curatorlist: string;
    head: number;
    headid: number;
    opchs: number;
    nodecree: number;

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
}


class ReplacedByCivilDatePosition {
    civildecree: boolean;
    civildecreenumber: string;
    civildecreedate: string;
    replaced: boolean;
    date: string;
}

class PositionPart {
    id: number;
    custom: boolean;
    customdate: string;
    civil: boolean;
    civildatelimit: boolean;
    civildate: string;
    decertificate: boolean;
    decertificatedate: string;
    decree: boolean;
    decreenumber: string;
    decreedate: string;
    
}

@Component({

})
export default class PositionmanagingpanelComponent extends Vue {
    filteredSofs: Sourceoffinancing[];
    filteredRanks: Rank[];
    filteredPositiontypes: Positiontype[];
    status: string;
    name: string;
    placeholdername: string;
    rankcap: any;
    ranks: Rank[];
    sof: any;
    sofs: Sourceoffinancing[];
    positiontype: any;
    positiontypeTitle: string;
    positioncategory: any;
    positioncategoryDisabled: boolean;
    positioncategories: Positioncategory[];
    positioncategoriesFiltred: Positioncategory[];
    positionReplaced: boolean;
    positioncategoriesCivil: Positioncategory[];
    mrd: any;
    quantity: number;
    notice: string;

    datecustom: boolean;
    dateactive: string;
    replacedbycivil: boolean;
    replacedbycivilquantity: number;
    //replacedbycivilpositioncategory: any;
    replacedbycivilpositioncategory: any;
    replacedbycivilpositiontype: any;
    replacedbycivilpositiontypeTitle: string;

    altrank: any;
    altrankconditionsFiltered: Altrankcondition[];
    altranks: Altrank[];
    decertificate: boolean;
    decertificatedate: string;
    civilranklow: number;
    civilrankhigh: number;
    replacedbycivildatelimit: boolean;
    replacedbycivildate: string;
    replacedbycivilclass: number;
    civilclass: number;

    /**
     * Solo only
     */
    curator: boolean;
    curatorlist: number[];
    head: boolean;
    headid: number;

    curationStructureTrees: StructureTree[];
    curationselectionprocess: boolean;
    headingStructureTree: StructureTree;
    headingselectionprocess: boolean;

    /**
     * Mass
     */
    positionsCode: string; // For multiple positions converted to JSON.
    positionParts: PositionPart[];
    displaytable: boolean;
    positioncivil: boolean; // true if civil, false if default;
    replacedbycivildatescount: number;
    replacedbycivildates: ReplacedByCivilDatePosition[];

    modalNewPositiontypeVisible: boolean;
    newPositiontypeName: string;
    newPositiontypeNameshort: string;
    newPositiontypePriority: number;

    opchs: boolean;
    nodecree: boolean;

    loadingPosition: boolean;

    focusid: number;

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

    // Statuses
    // addnewposition
    // removeposition
    // renameposition
    // removepositiondecree
    // renamepositiondecree

    @Prop({ default: 'Dr' })
    type: string;

    @Prop({ default: null })
    department: string;

    @Prop({ default: null })
    id: string;

    @Prop({ default: false })
    visible: boolean;

    @Prop({ default: false })
    visiblevar: boolean;

    data() {
        return {
            filteredSofs: [],
            filteredRanks: [],
            filteredPositiontypes: [],
            status: "renameposition",
            //type: "",
            //department: "",
            //id: "",
            name: "",
            placeholder: "",
            //visible: false,
            rankcap: null,
            ranks: [],
            sof: null,
            sofs: [],
            positiontype: null,
            positiontypeTitle: "",
            positioncategory: null,
            positioncategoryDisabled: false,
            positioncategories: [],
            positioncategoriesFiltred: [],
            positionReplaced: false,
            positioncategoriesCivil: [],
            mrd: [],
            quantity: 1,
            notice: "",
            datecustom: false,
            dateactive: this.toDateInputValue(new Date()),
            replacedbycivil: false,
            replacedbycivilquantity: 0,
            //replacedbycivilpositioncategory: [],
            replacedbycivilpositioncategory: null,
            replacedbycivilpositiontype: null,
            replacedbycivilpositiontypeTitle: "",

            altrank: null,
            altranks: [],
            decertificate: false,
            decertificatedate: this.toDateInputValue(new Date()),
            civilranklow: 6,
            civilrankhigh: 5,
            replacedbycivildatelimit: false,
            replacedbycivildate: this.toDateInputValue(new Date()),
            replacedbycivilclass: 0,
            civilclass: 0,

            positionsCode: "", // For multiple positions converted to JSON.
            positionParts: [],
            displayTable: false,
            positioncivil: true,
            replacedbycivildatescount: 0,
            replacedbycivildates: [],

            modalNewPositiontypeVisible: false,
            newPositiontypeName: "",
            newPositiontypeNameshort: "",
            newPositiontypePriority: 50,

            curator: false,
            curatorlist: [],
            curationStructureTrees: [],
            curatorselectionprocess: false,
            head: false,
            headid: 0,
            headingStructureTree: null,
            headingselectionprocess: false,
            opchs: true,
            nodecree: false,

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

            loadingPosition: false,
            focusid: 0,
        }
    }

    get ispositionpanel() {
        switch (this.type) {
            case "addnewposition": return true;
            case "removeposition": return true;
            case "renameposition": return true;
            case "removepositiondecree": return true;
            case "renamepositiondecree": return true;
            case "addnewdepartment": return false;
            case "removedepartment": return false;
            case "renamedepartment": return false;
            default: return false;
        }
    }

    get intromessage() {
        switch (this.type) {
            case "addnewposition": return "Ввести должность";
            case "removeposition": return "Вы уверены, что хотите сократить должность?";
            case "renameposition": return "Изменить должность";
            case "removepositiondecree": return "Вы уверены, что хотите отменить введение должности в проекте приказа?";
            case "renamepositiondecree": return "Редактировать должность";
            case "addnewdepartment": return "Создать подотдел";
            case "removedepartment": return "Вы уверены, что хотите упразднить подотдел?";
            case "renamedepartment": return "Изменить подотдел";
            default: return "Произошла непредвиденная оказия";
        }
        //return "Произошла какая-то дристня";
    }

    get getplaceholdername() {
        switch (this.type) {
            case "addnewposition": return "Название должности";
            case "removeposition": return "Название должности";
            case "renameposition": return "Название должности";
            case "removepositiondecree": return "Название должности";
            case "renamepositiondecree": return "Название должности";
            case "addnewdepartment": return "Название подотдела";
            case "removedepartment": return "Название подотдела";
            case "renamedepartment": return "Название подотдела";
            default: return "Произошла непредвиденная оказия";
        }
        //return "Произошла какая-то дристня";
    }

    get displayinput() {
        switch (this.type) {
            case "addnewposition": return false;
            case "removeposition": return true;
            case "renameposition": return false;
            case "removepositiondecree": return true;
            case "renamepositiondecree": return false;
            case "addnewdepartment": return false;
            case "removedepartment": return true;
            case "renamedepartment": return false;
            default: return true;
        }
    }

    get displayOnDelete() {
        switch (this.type) {
            case "addnewposition": return false;
            case "removeposition": return true;
            case "renameposition": return false;
            case "removepositiondecree": return true;
            case "renamepositiondecree": return false;
            case "addnewdepartment": return false;
            case "removedepartment": return true;
            case "renamedepartment": return false;
            default: return false;
        }
    }

    get displayOnNewPosition() {
        switch (this.type) {
            case "addnewposition": return true;
            case "removeposition": return false;
            case "renameposition": return true;
            case "removepositiondecree": return false;
            case "renamepositiondecree": return true;
            case "addnewdepartment": return false;
            case "removedepartment": return false;
            case "renamedepartment": return false;
            default: return false;
        }
    }

    get displayOnCount() {
        switch (this.type) {
            case "addnewposition": return true;
            case "removeposition": return true;
            case "renameposition": return true;
            case "removepositiondecree": return true;
            case "renamepositiondecree": return true;
            case "addnewdepartment": return false;
            case "removedepartment": return false;
            case "renamedepartment": return false;
            default: return false;
        }
    }

    get displayProlonged() {
        switch (this.type) {
            case "addnewposition": return true;
            case "removeposition": return true;
            case "renameposition": return true;
            case "removepositiondecree": return false;
            case "renamepositiondecree": return true;
            case "addnewdepartment": return false;
            case "removedepartment": return false;
            case "renamedepartment": return false;
            default: return false;
        }
    }

    get positiontypes(): Positiontype[] {
        return this.$store.state.positiontypes;
    }

    get altrankconditiongroups(): Altrankconditiongroup[] {
        return this.$store.state.altrankconditiongroups;
    }

    get altrankconditions(): Altrankcondition[] {
        return this.$store.state.altrankconditions;
    }

    get mrds(): Mrd[] {
        return this.$store.state.mrds;
    }

    get subjects(): Subject[] {
        return this.$store.state.subjects;
    }

    subjectsFilteredByGender(subjectgenderid: number): Subject[] {
        if (subjectgenderid == null || subjectgenderid == 0) {
            return this.subjects;
        }
    }

    mounted() {
        this.ranks = this.$store.state.ranks;
        this.sofs = this.$store.state.sofs;
        //alert(JSON.stringify(this.positiontypes));
        this.positioncategories = this.$store.state.positioncategories;
        this.onRankChange();
        this.onPositionCategoryChange();

        if (this.type == "renamepositiondecree" || this.type == "renameposition") {
            this.loadPosition();
        }

        this.onNumChange();
        this.onRankChange();

        this.filteredSubjects = this.subjects.filter(option => (option.category == 1 || option.category == 7));
        //(<any>this.$refs.inputpositionmanagementname).focus(); WHY NOT WORK
    }


    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {

        if (value && this.curationselectionprocess) {
            if (this.$store.state.modeselectedcuration > 0) {
                if (this.curatorlist.find(c => c == this.$store.state.modeselectedcuration) == null) {
                    this.curatorlist.push(this.$store.state.modeselectedcuration);
                }
                this.$store.commit("setModeselectedcuration", 0);
            }
            this.prepareTrees();
            this.curationselectionprocess = false;
            return;
        }
        if (value && this.headingselectionprocess) {
            if (this.$store.state.modeselectedheading > 0) {
                this.headid = this.$store.state.modeselectedheading;
                this.$store.commit("setModeselectedheading", 0);
            }
            this.prepareTrees();
            this.headingselectionprocess = false;
            return;
        }
        if (this.curationselectionprocess || this.headingselectionprocess) {
            return;
        }
        this.name = "";
        this.rankcap = null;
        this.sof = null;
        this.positiontype = null;
        this.positioncategory = null;
        this.mrd = new Array();
        this.quantity = 1;
        this.replacedbycivil = false;
        this.replacedbycivilquantity = 0;
        //this.replacedbycivilpositioncategory = new Array();
        this.replacedbycivilpositioncategory = null;
        this.replacedbycivilpositiontype = null;
        this.notice = "";
        this.datecustom = false;
        this.dateactive = this.toDateInputValue(new Date());
        this.altrank = null;
        this.altranks = new Array();
        this.decertificate = false;
        this.decertificatedate = this.toDateInputValue(new Date());
        this.replacedbycivildatelimit = false;
        this.replacedbycivildate = this.toDateInputValue(new Date());
        this.replacedbycivilclass = 0;
        this.civilclass = 0;
        this.onRankChange();
        this.onPositionCategoryChange();
        if (this.civilclass == 0) { 
            this.civilrankhigh = 0;
            this.civilranklow = 0;
        }
        this.replacedbycivildatescount = 0;
        this.replacedbycivildates = new Array();
        this.positionParts = new Array();
        this.opchs = true;

        if (value && (this.type == "renamepositiondecree" || this.type == "renameposition")) {
            this.loadPosition();
        }

        this.curator = false;
        this.curatorlist = new Array();
        this.head = false;
        this.headid = 0;
        this.curationStructureTrees = new Array();
        this.headingStructureTree = null;

        this.onNumChange();
        this.onRankChange();
        this.nodecree = false;

        this.loadingPosition = false;

        /**
         * Документная часть
         */
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
        this.subject16 = null;
        this.subject17 = null;
        this.subject18 = null;
        this.subject19 = null;
        this.subject20 = null;
    }

    onRankChange() {
        
        //alert(this.rankcap);
        //alert(JSON.stringify(this.positioncategories));
        this.positioncivil = true;
        if (this.rankcap == null || this.rankcap == "") {
            this.positioncategoriesFiltred = this.positioncategories.filter(pc => pc.civil == 1);
            this.positioncategory = this.positioncategoriesFiltred[0].id;
            this.positioncategoryDisabled = false;
        } else {
            this.positioncivil = false;
            this.positioncategoriesFiltred = this.positioncategories.filter(pc => (pc.id == this.ranks.find(r => r.id == this.rankcap).positioncategory) || pc.variable > 0);
            this.positioncategory = this.positioncategoriesFiltred.find(pc => pc.id == this.ranks.find(r => r.id == this.rankcap).positioncategory).id;
            //this.positioncategoryDisabled = true; 

            let positionCat: Positioncategory = this.positioncategoriesFiltred.find(pc => pc.id == this.positioncategory);
            //this.replacedbycivilpositioncategory = new Array();
            this.replacedbycivilpositioncategory = null;
            this.replacedbycivilclass = 0;
            this.civilclass = 0;
            this.civilrankhigh = 5;
            this.civilranklow = 6;
            this.replacedbycivildatelimit = false;
            if (positionCat.officer > 0) {
                this.positioncategoriesCivil = this.positioncategories.filter(pc => pc.civil == 1 && pc.replaceofficer > 0);
            } else {
                this.positioncategoriesCivil = this.positioncategories.filter(pc => pc.civil == 1 && pc.replacenonofficer > 0);
            }
        }
        
    }

 

    onReplacedChange() {
        if (this.replacedbycivil == false) {
            this.positioncategoriesCivil = this.positioncategories.filter(pc => pc.civil == 1);
            //this.replacedbycivilpositioncategory = new Array();
            this.replacedbycivilpositioncategory = null;
            this.positionReplaced = false;
            this.replacedbycivilclass = 0; 
            this.replacedbycivilquantity = 0;
            this.onCivilNumChange();
        } else {
            //this.replacedbycivilpositioncategory = new Array();
            this.replacedbycivilpositioncategory = null;
            this.positionReplaced = true;
            this.replacedbycivilquantity = 1;
            this.onCivilNumChange();
            this.onRankChange();
        }
    }

    onAltrankChange() {
        if (this.altrank != null) {
            this.altranks = new Array();
            this.altrankconditionsFiltered = this.altrankconditions.filter(a => a.group == this.altrank);
            this.altrankconditionsFiltered.forEach(a => {
                let altrank: Altrank = new Altrank();
                altrank.altrankcondition = a.id;
                altrank.altrankconditionname = a.name;
                altrank.rank = 1;
                this.altranks.push(altrank);
            })
           // alert(JSON.stringify(this.altrankconditions));
        }
        
    }

    /**
     * Отправляем позицию на добавление/изменение 
     **/
    okbutton() {

        var rankID: number = 0;
        if (this.rankcap > 0 ) {
            rankID = this.rankcap;
        }
        if (this.positiontype == null) {
            this.positiontype = 0;
        }
        if (this.positioncategory == null || this.positioncategory == "") {
            this.positioncategory = 1;
        }
        let datecustomNum: number = 0;
        if (this.datecustom) {
            datecustomNum = 1;
        }

        let decertificateNum: number = 0;
        if (this.decertificate) {
            decertificateNum = 1;
        }
        
        let mrdString: string = "";
        if (this.mrd != null) {
            this.mrd.forEach(x => {
                mrdString += x + ",";
            })
        }
        mrdString = mrdString.substring(0, mrdString.length - 1);
        //alert(this.dateactive);
        if (this.replacedbycivilpositioncategory == null) {
            //this.replacedbycivilpositioncategory = new Array();
            this.replacedbycivilpositioncategory = null;
        }

        
        /*let replacedbycivilpositioncategoryString: string = "";
        replacedbycivilpositioncategoryString = this.replacedbycivilpositioncategory.join('');
        if (replacedbycivilpositioncategoryString.length == 0) {
            replacedbycivilpositioncategoryString = "0";
        }*/
        let replacedbycivilpositioncategoryString: string = "0";
        if (this.replacedbycivilpositioncategory != null) {
            replacedbycivilpositioncategoryString = this.replacedbycivilpositioncategory.toString();
        }
        

        if (this.replacedbycivilpositiontype == null) {
            this.replacedbycivilpositiontype = 0;
        }
        let altranksstring: string = "";
        if (this.altrank == null) {
            this.altrank = 0;
        } else {
            this.altranks.forEach(a => {
                altranksstring += a.altrankcondition + "=" + a.rank + ";";
            })
            if (this.altranks.length > 0) {
                altranksstring = altranksstring.substring(0, altranksstring.length - 1);
            }
        }

        let replacedbycivildateNum: number = 0;
        let replacedbycivildateDate: Date = new Date();
        if (this.replacedbycivildatelimit) {
            replacedbycivildateNum = 1;
            replacedbycivildateDate = new Date(this.replacedbycivildate);
        }

        if (this.replacedbycivilclass == 0 && this.civilclass == 0) {
            this.civilranklow = 0;
            this.civilrankhigh = 0;
        }

        
        let positionsCode: string = this.stringifyPositionParts(this.positionParts);
        //alert(positionsCode);
        //if (true == true) {
        //    return;
        //}

        let replacedByCivilNum: number = 0;
        if (this.replacedbycivil) {
            replacedByCivilNum = 1;
        }

        let curatorNum: number = 0;
        let curatorStr: string = "";
        if (this.curator && this.curatorlist.length > 0) {
            curatorNum = 1;
            this.curatorlist.forEach(c => {
                curatorStr += c + ",";
            })
            curatorStr = curatorStr.slice(0, -1);
        }

        let headNum: number = 0;
        if (this.head && this.headid > 0) {
            headNum = 1;
        }

        let opchsNum: number = 0;
        if (this.opchs) {
            opchsNum = 1;
        }

        let nodecreeNum: number = 0;
        //if (this.nodecree || this.type == "renamestructurenodecree") {
        if (this.nodecree) {
            nodecreeNum = 1;
        }

        this.loadingPosition = true;
        fetch('/api/Positions', {
            method: 'post',
            body: JSON.stringify(<PositionManagement>{
                type: this.type, id: Number.parseInt(this.id), name: this.name, department: Number.parseInt(this.department), rankCap: rankID, sof: this.sof, quantity: this.quantity, positiontype: this.positiontype,
                datecustom: datecustomNum, dateactive: new Date(this.dateactive), positioncategory: this.positioncategory, mrd: mrdString, replacedbycivilpositioncategory: Number.parseInt(replacedbycivilpositioncategoryString),
                replacedbycivilpositiontype: this.replacedbycivilpositiontype, replacedbycivil: replacedByCivilNum, notice: this.notice, altrankconditiongroup: this.altrank, altranks: altranksstring,
                decertificate: decertificateNum, decertificatedate: new Date(this.decertificatedate), civilrankhigh: this.civilrankhigh, civilranklow: this.civilranklow, replacedbycivildate: replacedbycivildateDate, replacedbycivildatelimit: replacedbycivildateNum,
                positionsCode: positionsCode, curator: curatorNum, curatorlist: curatorStr, head: headNum, headid: this.headid, opchs: opchsNum, nodecree: nodecreeNum,
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
            /*.then(x => {
                if (this.preparedocument) {
                    fetch('/api/PositionsPrint', {
                        method: 'post',
                        body: JSON.stringify(<PositionManagement>{
                            type: this.type, id: Number.parseInt(this.id), name: this.name, department: Number.parseInt(this.department)
                        }),
                        credentials: 'include',
                        headers: new Headers({
                            'Accept': 'application/json',
                            'Content-Type': 'application/json'
                        })
                    }).then(x => x.blob())
                        .then(x => download(x, this.name))
                }
            })*/
            .then(x => {
                this.loadingPosition = false;
                this.$store.commit("setForcePositionUpdate", true);
                this.$emit('update:visiblevar', false);
            } );

    }

    cancelbutton() {
        this.$emit('update:visiblevar', false);
    }

    isReplacedByCivil() {
        if (this.replacedbycivil == true) {
            return true;
        } else {
            return false;
        }
    }

    toDateInputValue(date: Date): string {
        var local = new Date(date);
        local.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

    containsClass() {
        if (this.replacedbycivilclass > 0) {
            return true;
        } else {
            return false;
        }
    }

    containsCivilClass() {
        if (this.civilclass > 0) {
            return true;
        } else {
            return false;
        }
    }

    onReplacedByCivilPositionCategoryChange() {
        let anyClass: boolean = false;
        let pc: Positioncategory = this.positioncategories.find(pc => pc.id == this.replacedbycivilpositioncategory);
        if (pc.classcap > 0) {
            anyClass = true;
            this.replacedbycivilclass = pc.classcap;
            //return;
        }
        if (!anyClass) {
            this.replacedbycivilclass = 0;
        }
    }

    onPositionCategoryChange() {
        let anyClass: boolean = false;
        let pc: Positioncategory = this.positioncategories.find(pc => pc.id == this.positioncategory);
        if (pc.classcap > 0) {
            anyClass = true;
            this.civilclass = pc.classcap;
            //return;
        }
        if (!anyClass) {
            this.civilclass = 0;
        }
    }

    onTypeChange() {
        this.replacedbycivilpositiontype = this.positiontype;
        this.positiontypeTitle = this.positiontypes.find(pt => pt.id == this.positiontype).name;

        /**
         * Подгружаем части-наименования для документов, прикрепленные к данному наименованию должности. 
         * Например, если к главному специалисту подгружено "главный" "специалист", то оно появится в наименовании для документов.
         */
        if (this.positiontype != null && this.positiontype != 0) {
            let positiontypeObject: Positiontype = this.positiontypes.find(p => p.id == this.positiontype);
            if (positiontypeObject != null) {
                if (positiontypeObject.subject1 == null || positiontypeObject.subject1 == 0) {
                    this.subject1 = null;
                } else {
                    this.subject1 = positiontypeObject.subject1;
                }

                if (positiontypeObject.subject2 == null || positiontypeObject.subject2 == 0) {
                    this.subject2 = null;
                } else {
                    this.subject2 = positiontypeObject.subject2;
                }
                if (positiontypeObject.subject3 == null || positiontypeObject.subject3 == 0) {
                    this.subject3 = null;
                } else {
                    this.subject3 = positiontypeObject.subject3;
                }
                if (positiontypeObject.subject4 == null || positiontypeObject.subject4 == 0) {
                    this.subject4 = null;
                } else {
                    this.subject4 = positiontypeObject.subject4;
                }
                if (positiontypeObject.subject5 == null || positiontypeObject.subject5 == 0) {
                    this.subject5 = null;
                } else {
                    this.subject5 = positiontypeObject.subject5;
                }
                if (positiontypeObject.subject6 == null || positiontypeObject.subject6 == 0) {
                    this.subject6 = null;
                } else {
                    this.subject6 = positiontypeObject.subject6;
                }
                if (positiontypeObject.subject7 == null || positiontypeObject.subject7 == 0) {
                    this.subject7 = null;
                } else {
                    this.subject7 = positiontypeObject.subject7;
                }
                if (positiontypeObject.subject8 == null || positiontypeObject.subject8 == 0) {
                    this.subject8 = null;
                } else {
                    this.subject8 = positiontypeObject.subject8;
                }
                if (positiontypeObject.subject9 == null || positiontypeObject.subject9 == 0) {
                    this.subject9 = null;
                } else {
                    this.subject9 = positiontypeObject.subject9;
                }
                if (positiontypeObject.subject10 == null || positiontypeObject.subject19 == 0) {
                    this.subject10 = null;
                } else {
                    this.subject10 = positiontypeObject.subject10;
                }
                if (positiontypeObject.subject11 == null || positiontypeObject.subject11 == 0) {
                    this.subject11 = null;
                } else {
                    this.subject11 = positiontypeObject.subject11;
                }
                if (positiontypeObject.subject12 == null || positiontypeObject.subject12 == 0) {
                    this.subject12 = null;
                } else {
                    this.subject12 = positiontypeObject.subject12;
                }
                if (positiontypeObject.subject13 == null || positiontypeObject.subject13 == 0) {
                    this.subject13 = null;
                } else {
                    this.subject13 = positiontypeObject.subject13;
                }
                if (positiontypeObject.subject14 == null || positiontypeObject.subject14 == 0) {
                    this.subject14 = null;
                } else {
                    this.subject14 = positiontypeObject.subject14;
                }
                if (positiontypeObject.subject15 == null || positiontypeObject.subject15 == 0) {
                    this.subject15 = null;
                } else {
                    this.subject15 = positiontypeObject.subject15;
                }
                if (positiontypeObject.subject16 == null || positiontypeObject.subject16 == 0) {
                    this.subject16 = null;
                } else {
                    this.subject16 = positiontypeObject.subject16;
                }
                if (positiontypeObject.subject17 == null || positiontypeObject.subject17 == 0) {
                    this.subject17 = null;
                } else {
                    this.subject17 = positiontypeObject.subject17;
                }
                if (positiontypeObject.subject18 == null || positiontypeObject.subject18 == 0) {
                    this.subject18 = null;
                } else {
                    this.subject18 = positiontypeObject.subject18;
                }
                if (positiontypeObject.subject19 == null || positiontypeObject.subject19 == 0) {
                    this.subject19 = null;
                } else {
                    this.subject19 = positiontypeObject.subject19;
                }
                if (positiontypeObject.subject20 == null || positiontypeObject.subject20 == 0) {
                    this.subject20 = null;
                } else {
                    this.subject20 = positiontypeObject.subject20;
                }
            }
        }
    }

    onReplacedByCivilTypeChange() {
        this.replacedbycivilpositiontypeTitle = this.positiontypes.find(pt => pt.id == this.replacedbycivilpositiontype).name;
    }

    onNumChange() {
        //alert(this.dateactive);

        if (this.quantity < 0) {
            this.quantity = 0;
        }
        if (this.quantity > 1000) {
            this.quantity = 1000;
        }
        if (this.replacedbycivilquantity > this.quantity) {
            this.replacedbycivilquantity = this.quantity;
            this.onCivilNumChange();
        }
        this.updatePositionParts();
        //alert(this.quantity);
    }

    onCivilNumChange() {
        if (this.replacedbycivilquantity < 0) {
            this.replacedbycivilquantity = 0;
        }
        if (this.replacedbycivilquantity > this.quantity) {
            this.replacedbycivilquantity = this.quantity;
        }
        this.updateReplacedByCivilDatePositions();
        
    }

    onCivilDateUpdate() {

    }

    updateReplacedByCivilDatePositions() {
        if (this.replacedbycivildates == null) {
            this.replacedbycivildates = new Array();
        }

        let previousLength: number = this.replacedbycivildates.length;
        if (this.replacedbycivilquantity > previousLength) {
            let quantityDifference: number = this.replacedbycivilquantity - previousLength;
            for (let i = 0; i < quantityDifference; i++) {
                let replaced: ReplacedByCivilDatePosition = new ReplacedByCivilDatePosition();
                replaced.date = this.toDateInputValue(new Date());
                replaced.replaced = false;
                replaced.civildecree = false;
                replaced.civildecreedate = this.toDateInputValue(new Date());
                replaced.civildecreenumber = "";

                this.replacedbycivildates.push(replaced);
            }
        } else if (this.replacedbycivilquantity < previousLength) {
            let quantityDifference: number = previousLength - this.replacedbycivilquantity;
            for (let i = 0; i < quantityDifference; i++) {
                this.replacedbycivildates.pop();
            }
        }
        this.updatePositionParts();
    }

    updatePositionParts() {
        if (this.positionParts == null) {
            this.positionParts = new Array();
        }
        let previousLength: number = this.positionParts.length;

        let quantityFull: number = Math.trunc(this.quantity);
        let quantityPart: number = this.quantity - quantityFull;

        if (quantityFull < 1) {
            quantityFull = 1;
        } else if (quantityPart > 0.01) {
            quantityFull += 1; // Докидываем доброе значение.
        }
        if (quantityFull > previousLength) {
            let quantityDifference: number = quantityFull - previousLength;
            for (let i = 0; i < quantityDifference; i++) {
                let posPart: PositionPart = new PositionPart();
                posPart.id = this.positionParts.length + 1;
                posPart.civil = false;
                posPart.custom = false;
                posPart.decertificate = false;
                posPart.civildatelimit = false;
                posPart.civildate = null;
                posPart.customdate = null;
                posPart.decertificatedate = null;

                this.positionParts.push(posPart);
            }
        } else if (quantityFull < previousLength) {
            let quantityDifference: number = previousLength - quantityFull;
            for (let i = 0; i < quantityDifference; i++) {
                this.positionParts.pop();
            }
        }

        let replacedByCivilNumLeft: number = this.replacedbycivilquantity;
        //let replacedByCivilNumLeftDateLimit: number = this.replacedbycivildates.length;
        let index: number = 0;
        this.positionParts.forEach(p => {
            if (replacedByCivilNumLeft > 0) {
                p.civil = true;
                if (this.replacedbycivildates[index].replaced) {
                    p.civildatelimit = true;
                    p.civildate = this.replacedbycivildates[index].date;
                } else {
                    p.civildatelimit = false;
                }

                if (this.replacedbycivildates[index].civildecree) {
                    p.decree = true;
                    p.decreedate = this.replacedbycivildates[index].civildecreedate;
                    p.decreenumber = this.replacedbycivildates[index].civildecreenumber;
                } else {
                    p.decree = false;
                }

                index++;
                replacedByCivilNumLeft--;
            } else {
                p.civildatelimit = false;
                p.civil = false;
                p.decree = false;
            } 
            
        })

    }

    stringifyPositionParts(parts: PositionPart[]): string {
        let stringParts: string = ""; 
        parts.forEach(p => {
            stringParts += "id=";
            stringParts += p.id + "&"; 
            stringParts += "civil=";
            if (p.civil) {
                stringParts += "true&";
            } else {
                stringParts += "false&";
            }

            stringParts += "civildatelimit=";
            if (p.civildatelimit) {
                stringParts += "true&";
            } else {
                stringParts += "false&";
            }
            stringParts += "civildate=";
            if (p.civildate == null) {
                stringParts += "&";
            } else {
                stringParts += p.civildate + "&";
            }



            stringParts += "custom=";
            if (p.custom) {
                stringParts += "true&";
            } else {
                stringParts += "false&";
            }
            stringParts += "customdate=";
            if (p.customdate == null) {
                stringParts += "&";
            } else {
                stringParts += p.customdate + "&";
            }

            stringParts += "decertificate=";
            if (p.decertificate) {
                stringParts += "true&";
            } else {
                stringParts += "false&";
            }
            stringParts += "decertificatedate=";
            if (p.decertificatedate == null) {
                stringParts += "&";
            } else {
                stringParts += p.decertificatedate + "&";
            }

            stringParts += "decree=";
            if (p.decree) {
                stringParts += "true&";
            } else {
                stringParts += "false&";
            }
            stringParts += "decreenumber=";
            if (p.decreenumber == null) {
                stringParts += "&";
            } else {
                stringParts += p.decreenumber + "&";
            }
            stringParts += "decreedate=";
            if (p.decreedate == null) {
                stringParts += ";";
            } else {
                stringParts += p.decreedate + ";";
            }

        });
        if (stringParts.length > 0) {
            stringParts = stringParts.slice(0, -1);
        }
        return stringParts;
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

    addPositiontype(event: any) {
        if (event) event.preventDefault();
        fetch('/api/Positiontype', {
            method: 'post',
            body: JSON.stringify(<Positiontype>{
                name: this.newPositiontypeName,
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
                setTimeout(() => { this.modalNewPositiontypeVisible = false; }, 2000); 
            });
    }

    shortifyTitle(title: string): string {
        return (title.length > 90) ? title.substr(0, 90 - 1) + '...' : title;
    }

    loadPosition() {
        fetch('api/Positions/Solo' + this.id, { credentials: 'include' })
            .then(response => response.json() as Promise<Position>)
            .then(data => {
                this.name = data.name;
                this.quantity = 1;
                if (data.part > 0) {
                    this.quantity = data.partval;
                }
                this.rankcap = data.cap;
                this.onRankChange();
                this.onNumChange();
                //alert(data.cap);
                this.sof = data.sourceoffinancing;
                this.filterMethod("");
                this.filterRanks("");
                this.filterSofs("");
                this.positiontype = data.positiontype;
                this.positioncategory = data.positioncategory;
                if (data.opchs > 0) {
                    this.opchs = true;
                } else {
                    this.opchs = false;
                }
                this.onPositionCategoryChange();
                fetch('api/Positions/Mrd' + this.id, { credentials: 'include' })
                    .then(response => response.json() as Promise<string>)
                    .then(mrddata => {
                        let mrds: number[] = mrddata.split(',').map(Number);
                        this.mrd = mrds;
                    });

                if (true == true) {
                    //"conditiongroup:condition=rank,condition=rank" - "1:1=2,2=4"
                    fetch('api/Positions/Stringaltrankload' + this.id, { credentials: 'include' })
                        .then(response => response.json() as Promise<string>)
                        .then(data => {
                            if (data == null || data.length < 2 ) {
                                return;
                            }
                            let splitted: string[] = data.split(':');
                            this.altrank = Number.parseInt(splitted[0]);
                            this.altranks = new Array();
                            this.onAltrankChange();
                            splitted[1].split(',').forEach(ss => {
                                let altrank: number = Number.parseInt(ss.split('=')[0]);
                                let rank: number = Number.parseInt(ss.split('=')[1]);
                                this.altranks.forEach(ar => {
                                    if (altrank == ar.altrankcondition) {
                                        ar.rank = rank;
                                    }
                                })
                            })


                        });
                }
                
                
                if (data.replacedbycivil > 0) {
                    this.replacedbycivil = true;
                }
                this.replacedbycivilpositiontype = data.replacedbycivilpositiontype;
                if (data.replacedbycivil > 0) {
                    this.replacedbycivilquantity = 1;
                    this.onReplacedChange();
                    this.replacedbycivilpositioncategory = data.replacedbycivilpositioncategory;
                    this.onReplacedByCivilPositionCategoryChange();
                    
                    
                }
                this.notice = data.notice;
                //this.datecustom = data.da;
                //this.dateactive = data.;
                //this.altrank = null;
                //this.altranks = new Array();
                //this.decertificate = false;
                //this.decertificatedate = this.toDateInputValue(new Date());
                if (data.replacedbycivildatelimit > 0) {
                    this.replacedbycivildatelimit = true;
                    this.replacedbycivildate = this.toDateInputValue(data.replacedbycivildate);
                }

                this.civilrankhigh = data.civilrankhigh;
                this.civilranklow = data.civilranklow;

                let treepreparerequired: boolean = false;
                if (data.head > 0 && data.headid > 0) {
                    this.head = true;
                    this.headid = data.headid;
                    treepreparerequired = true;
                }

                if (data.curator > 0 && data.curatorlist != null && data.curatorlist.length > 0) {
                    this.curator = true;
                    this.curatorlist = data.curatorlist.split(',').map(Number);
                    treepreparerequired = true;
                }

                

                if (treepreparerequired) {
                    this.prepareTrees();
                }

                
                /**
                 * Документная часть 
                 */
                if (data.subject1 == null || data.subject1 == 0) {
                    this.subject1 = null;
                } else {
                    this.subject1 = data.subject1;
                }
                if (data.subject2 == null || data.subject2 == 0) {
                    this.subject2 = null;
                } else {
                    this.subject2 = data.subject2;
                }
                if (data.subject3 == null || data.subject3 == 0) {
                    this.subject3 = null;
                } else {
                    this.subject3 = data.subject3;
                }
                if (data.subject4 == null || data.subject4 == 0) {
                    this.subject4 = null;
                } else {
                    this.subject4 = data.subject4;
                }
                if (data.subject5 == null || data.subject5 == 0) {
                    this.subject5 = null;
                } else {
                    this.subject5 = data.subject5;
                }
                if (data.subject6 == null || data.subject6 == 0) {
                    this.subject6 = null;
                } else {
                    this.subject6 = data.subject6;
                }
                if (data.subject7 == null || data.subject7 == 0) {
                    this.subject7 = null;
                } else {
                    this.subject7 = data.subject7;
                }
                if (data.subject8 == null || data.subject8 == 0) {
                    this.subject8 = null;
                } else {
                    this.subject8 = data.subject8;
                }
                if (data.subject9 == null || data.subject9 == 0) {
                    this.subject9 = null;
                } else {
                    this.subject9 = data.subject9;
                }
                if (data.subject10 == null || data.subject10 == 0) {
                    this.subject10 = null;
                } else {
                    this.subject10 = data.subject10;
                }
                if (data.subject11 == null || data.subject11 == 0) {
                    this.subject11 = null;
                } else {
                    this.subject11 = data.subject11;
                }
                if (data.subject12 == null || data.subject12 == 0) {
                    this.subject12 = null;
                } else {
                    this.subject12 = data.subject12;
                }
                if (data.subject13 == null || data.subject13 == 0) {
                    this.subject13 = null;
                } else {
                    this.subject13 = data.subject13;
                }
                if (data.subject14 == null || data.subject14 == 0) {
                    this.subject14 = null;
                } else {
                    this.subject14 = data.subject14;
                }
                if (data.subject15 == null || data.subject15 == 0) {
                    this.subject15 = null;
                } else {
                    this.subject15 = data.subject15;
                }
                if (data.subject16 == null || data.subject16 == 0) {
                    this.subject16 = null;
                } else {
                    this.subject16 = data.subject16;
                }
                if (data.subject17 == null || data.subject17 == 0) {
                    this.subject17 = null;
                } else {
                    this.subject17 = data.subject17;
                }
                if (data.subject18 == null || data.subject18 == 0) {
                    this.subject18 = null;
                } else {
                    this.subject18 = data.subject18;
                }
                if (data.subject19 == null || data.subject19 == 0) {
                    this.subject19 = null;
                } else {
                    this.subject19 = data.subject19;
                }
                if (data.subject20 == null || data.subject20 == 0) {
                    this.subject20 = null;
                } else {
                    this.subject20 = data.subject20;
                }

            });
        //alert(this.id);
    }

    addCuration() {
        this.curationselectionprocess = true;
        this.$store.commit("setModeselectcuration", true);
        //this.$emit('update:visible', false);
    }

    removeCuration(id: number) {
        this.curatorlist = this.curatorlist.filter(c => c != id);
        this.curationStructureTrees = this.curationStructureTrees.filter(c => c.id != id);
    }

    downCuration(index: number) {
        if (this.curationStructureTrees.length == 1) {
            return;
        }
        let tempST: StructureTree = null;
        let tempN: number = 0;
        if (index + 1 == this.curationStructureTrees.length) {
            tempST = this.curationStructureTrees[0];
            this.curationStructureTrees[0] = this.curationStructureTrees[index];
            this.curationStructureTrees[index] = tempST;

            tempN = this.curatorlist[0];
            this.curatorlist[0] = this.curatorlist[index];
            this.curatorlist[index] = tempN;
        } else {
            tempST = this.curationStructureTrees[index + 1];
            this.curationStructureTrees[index + 1] = this.curationStructureTrees[index];
            this.curationStructureTrees[index] = tempST;

            tempN = this.curatorlist[index + 1];
            this.curatorlist[index + 1] = this.curatorlist[index];
            this.curatorlist[index] = tempN;
        }
        this.curator = false;
        this.curator = true;
    }

    upCuration(index: number) {
        if (this.curationStructureTrees.length == 1) {
            return;
        }
        let tempST: StructureTree = null;
        let tempN: number = 0;
        if (index == 0) {
            tempST = this.curationStructureTrees[this.curationStructureTrees.length - 1];
            this.curationStructureTrees[this.curationStructureTrees.length - 1] = this.curationStructureTrees[index];
            this.curationStructureTrees[index] = tempST;

            tempN = this.curatorlist[this.curatorlist.length - 1];
            this.curatorlist[this.curatorlist.length - 1] = this.curatorlist[index];
            this.curatorlist[index] = tempN;
        } else {
            tempST = this.curationStructureTrees[index - 1];
            this.curationStructureTrees[index - 1] = this.curationStructureTrees[index];
            this.curationStructureTrees[index] = tempST;

            tempN = this.curatorlist[index - 1];
            this.curatorlist[index - 1] = this.curatorlist[index];
            this.curatorlist[index] = tempN;
        }
        this.curator = false;
        this.curator = true;
    }

    addHeading() {
        this.headingselectionprocess = true;
        this.$store.commit("setModeselectheading", true);
        //this.$emit('update:visible', false);
    }

    removeHeading() {
        this.headid = 0;
        this.headingStructureTree = null;
    }

    prepareTrees() {
        let getString: string = "";
        this.curatorlist.forEach(c => {
            getString += c + "&";
        })
        if (getString.length > 0) {
            getString = getString.slice(0, -1);
            fetch('api/DetailedStructure/Trees' + getString, { credentials: 'include' })
                .then(response => response.json() as Promise<StructureTree[]>)
                .then(data => {
                    this.curationStructureTrees = data;
                });
        }
        if (this.headid > 0) {
            fetch('api/DetailedStructure/Trees' + this.headid, { credentials: 'include' })
                .then(response => response.json() as Promise<StructureTree[]>)
                .then(data => {
                    if (data.length > 0) {
                        this.headingStructureTree = data[0];
                    }
                    
                });
        }
    }

    inputFilter() {
        alert('1');
    }

    onClose() {
        alert('close');
    }

    filterMethod(query) {
        if (query == null) {
            query = "";
        }
        this.filteredPositiontypes = this.positiontypes.filter(option => {
            return option.name.toLowerCase().startsWith(query.toLowerCase());
        })
    }

    filterRanks(query) {
        if (query == null) {
            query = "";
        }
        this.filteredRanks = this.ranks.filter(option => {
            return option.name.toLowerCase().startsWith(query.toLowerCase());
        })
    }

    filterSofs(query) {
        if (query == null) {
            query = "";
        }
        this.filteredSofs = this.sofs.filter(option => {
            return option.name.toLowerCase().startsWith(query.toLowerCase());
        })
    }

    focusrankcap() {
        //var test = this.rankcap;
        //this.rankcap = null;
        //this.rankcap = test;
        //this.rankcap = "123";
    }

    changecustomdate(part: PositionPart) {
        if (this.focusid != part.id) {
            return;
        }
        //alert('changecustomdate');
        //part.customdate
        let indexSelected: number = this.positionParts.findIndex(p => p.id == part.id);
        this.positionParts.slice(indexSelected).forEach(p => p.customdate = part.customdate);
        //alert(indexSelected);
    }

    changecustomcheckbox(part: PositionPart) {
        let indexSelected: number = this.positionParts.findIndex(p => p.id == part.id);
        this.positionParts.slice(indexSelected).forEach(p => p.custom = part.custom);
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

    onSubjectChange() {
        alert('subject input');
    }

    filterMethodSubject(query) {
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

        this.filteredSubjects = [...new Set(this.filteredSubjects)];
    }
}