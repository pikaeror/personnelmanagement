import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip, Upload, Collapse, CollapseItem } from 'element-ui';
import _ from 'lodash';
import printJS from 'print-js';
import Rank from '../../classes/rank';
import Sourceoffinancing from '../../classes/sourceoffinancing';
import Positiontype from '../../classes/positiontype';
import Positioncategory from '../../classes/positioncategory';
import Mrd from '../../classes/mrd';
import Positionmrd from "../../classes/positionmrd";
import Decreeoperationsrequest from '../../classes/decreeoperationsrequest';
import Decreeoperation from '../../classes/decreeoperation';
import Positionhistory from '../../classes/positionhistory';
import Positiondecertificate from '../../classes/positiondecertificate';
import StructureTree from '../../classes/structuretree';
import StructureInfo from '../../classes/structureinfo';
import Person from '../../classes/person';
import Personrank from '../../classes/personrank';
import Personphoto from '../../classes/personphoto';
import Position from '../../classes/position';
import Personcontract from '../../classes/personcontract';
import Relativetype from '../../classes/personcontract';
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
import Cabinetdata from '../../classes/cabinetdata';
import Autobiographydata from '../../classes/autobiographydata';
import Profiledata from '../../classes/profiledata';
import Sheetdata from '../../classes/sheetdata';
import Declarationdata from '../../classes/declarationdata';
import Pseducation from '../../classes/pseducation';
import Pswork from '../../classes/pswork';
import Profilerelatives from '../../classes/profilerelatives';
import Sheetpolitics from '../../classes/sheetpolitics';
import Declarationrelative from '../../classes/declarationrelative';
import Declarationtabledata from '../../classes/declarationtabledata';

import download from 'downloadjs';
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
Vue.use(Element);

const externalAdressCandidates = "http://172.26.200.47/document.php?";

class Filelist {
    name: string;
    url: string;
}

@Component({
    components: {

    }
})
export default class CandidatesComponent extends Vue {

    @Prop({ default: 0 })
    visible: number;

    activeMode: string;

    newEmployeesid: number;
    newCreatorid: number;
    newReasonid: number;
    newDeclarationid: number;
    newUsersurname: string;
    newUsername: string;
    newUserpatronymic: string;
    newUserind: string;
    newUseraccesscode: string;
    reasonindchanged: boolean;

    displayCabinetes: boolean;
    
    cabinetes: Cabinetdata[];

    activecabinete: Cabinetdata;
    blockmode: boolean;
    blockreason: string;

    modalAutobiographyVisible: boolean;
    modalProfileVisible: boolean;
    modalSheetVisible: boolean;
    modalDeclarationVisible: boolean;

    oldDate: Date;

    searchtext: string;

    data() {
        return {
            activeMode: "2",
            
            newEmployeesid: 0,
            newCreatorid: 0,
            newReasonid: null,
            newDeclarationid: null,
            newUsersurname: "",
            newUsername: "",
            newUserpatronymic: "",
            newUserind: "",
            newUseraccesscode: "",
            reasonindchanged: false,

            displayCabinetes: false,
            cabinetes: [],

            activecabinete: null,
            blockmode: false,
            blockreason: "",

            modalAutobiographyVisible: false,
            modalProfileVisible: false,
            modalSheetVisible: false,
            modalDeclarationVisible: false,

            oldDate: new Date(2019, 7, 8),

            searchtext: "",
        }
    }

    mounted() {
        setInterval(this.changeInd, 100);
    }

    changeInd() {
        if (this.reasonindchanged) {
            this.newUserind = this.cyrToLat(this.newUserind);
            var letterNumber = /^[0-9a-zA-Z]+$/;
            if (this.newUserind.length > 0) {
                
                let lastletter = this.newUserind[this.newUserind.length - 1];
                if (!lastletter.match(letterNumber)) 
                {
                    this.newUserind = this.newUserind.substring(0, this.newUserind.length - 1);
                }
            }
            if (this.newUserind.length > 0 && !this.isNum(this.newUserind[0])) {
                this.newUserind = this.newUserind.substring(0, 0);
            }
            if (this.newUserind.length > 1 && !this.isNum(this.newUserind[1])) {
                this.newUserind = this.newUserind.substring(0, 1);
            }
            if (this.newUserind.length > 2 && !this.isNum(this.newUserind[2])) {
                this.newUserind = this.newUserind.substring(0, 2);
            }
            if (this.newUserind.length > 3 && !this.isNum(this.newUserind[3])) {
                this.newUserind = this.newUserind.substring(0, 3);
            }
            if (this.newUserind.length > 4 && !this.isNum(this.newUserind[4])) {
                this.newUserind = this.newUserind.substring(0, 4);
            }
            if (this.newUserind.length > 5 && !this.isNum(this.newUserind[5])) {
                this.newUserind = this.newUserind.substring(0, 5);
            }
            if (this.newUserind.length > 6 && !this.isNum(this.newUserind[6])) {
                this.newUserind = this.newUserind.substring(0, 6);
            }
            if (this.newUserind.length > 7 && !this.isLetter(this.newUserind[7])) {
                this.newUserind = this.newUserind.substring(0, 7);
            }
            if (this.newUserind.length > 8 && !this.isNum(this.newUserind[8])) {
                this.newUserind = this.newUserind.substring(0, 8);
            }
            if (this.newUserind.length > 9 && !this.isNum(this.newUserind[9])) {
                this.newUserind = this.newUserind.substring(0, 9);
            }
            if (this.newUserind.length > 10 && !this.isNum(this.newUserind[10])) {
                this.newUserind = this.newUserind.substring(0, 10);
            }
            if (this.newUserind.length > 11 && !this.isLetter(this.newUserind[11])) {
                this.newUserind = this.newUserind.substring(0, 11);
            }
            if (this.newUserind.length > 12 && !this.isLetter(this.newUserind[12])) {
                this.newUserind = this.newUserind.substring(0, 12);
            }
            if (this.newUserind.length > 13 && !this.isNum(this.newUserind[13])) {
                this.newUserind = this.newUserind.substring(0, 13);
            }
            if (this.newUserind.length > 14) {
                this.newUserind = this.newUserind.substring(0, 14);
            }
            this.newUserind = this.newUserind.toUpperCase();
            this.reasonindchanged = false;
            
        }
    }

    cyrToLat(str: string) {
        str = str.toLowerCase()
        .replace('ё', '~')
        .replace('й', 'Q')
        .replace('ц', 'W')
        .replace('у', 'E')
        .replace('к', 'R')
        .replace('е', 'T')
        .replace('н', 'Y')
        .replace('г', 'U')
        .replace('ш', 'I')
        .replace('щ', 'O')
        .replace('з', 'P')
        .replace('х', '[')
        .replace('ъ', ']')
        .replace('ф', 'A')
        .replace('ы', 'S')
        .replace('в', 'D')
        .replace('а', 'F')
        .replace('п', 'G')
        .replace('р', 'H')
        .replace('о', 'J')
        .replace('л', 'K')
        .replace('д', 'L')
        .replace('я', 'Z')
        .replace('ч', 'X')
        .replace('с', 'C')
        .replace('м', 'V')
        .replace('и', 'B')
        .replace('т', 'N')
        .replace('ь', 'M')
            ;


        return str;
    }

    isNum(str: string) {
        return str.length === 1 && str.match(/[0-9]/i);
    }

    isLetter(str: string) {
        return str.length === 1 && str.match(/[a-zA-Z]/i);
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value) {
            this.blockmode = false;
        }
    }
    
    createCabinet() {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                action: 0,
                reasonid: this.prepareNumToExport(this.newReasonid),
                declarationid: this.prepareNumToExport(this.newDeclarationid),
                usersurname: this.newUsersurname,
                username: this.newUsername,
                userpatronymic: this.newUserpatronymic,
                userind: this.newUserind,

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
                this.fetchCabinetes("");
                this.displayCabinetes = true;
                this.openCabineteByIdent(this.newUserind);
                //if (response != null && response.startsWith('E')) {
                //    this.openCabineteByIdent(this.newUserind);
                //}
            })
            .then(x => {
                
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
        return num;
    }

    toggleDisplayCabinetes() {
        this.displayCabinetes = !this.displayCabinetes;
        if (this.displayCabinetes) {
            this.fetchCabinetes('');
        }
    }

    searchCabinetes(search: string) {
        this.fetchCabinetes(search);
        this.displayCabinetes = true;
    }

    fetchCabinetes(fio: string) {
        if (fio == null || fio.length == 0 ) {
            fio = "nullsearch"; 
        }
        
        fetch('api/Cabinet/Search' + fio, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Cabinetdata[]>;
            })
            .then(result => {
                result.sort((a, b) => ('' + a.usersurname).localeCompare(b.usersurname));
                this.cabinetes = result;
                
            })
    }

    openCabinete(id: number) {
        fetch('api/Cabinet/Single' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Cabinetdata>;
            })
            .then(cabinet => {
                if (cabinet != null) {
                    //this.prepareToImport(person);
                    //alert(JSON.stringify(cabinet));
                    //alert(cabinet.userCompact.structureTree);
                    this.activecabinete = cabinet;
                    this.blockmode = false;
                    this.blockreason = "";
                    this.fetchCabinetes("");
                    this.displayCabinetes = false;
                    //this.getPhotos();
                    //this.photoToCreate = new Personphoto();

                }
            })
    }

    openCabineteByIdent(ident: string) {
        fetch('api/Cabinet/Ident' + ident, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Cabinetdata>;
            })
            .then(cabinet => {
                if (cabinet != null) {
                    this.activecabinete = cabinet;
                    this.blockmode = false;
                    this.blockreason = "";
                    this.fetchCabinetes("");
                } 
            })
    }

    predeleteCabinete() {
        this.blockmode = true;
        this.blockreason = "";
    }

    predeleteCabineteCancel() {
        this.blockmode = false;
        this.blockreason = "";
    }

    deleteCabinete(cabineteid: number) {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                id: cabineteid,
                action: 1,
                denyreason: this.blockreason,

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
                this.fetchCabinetes("");
                this.activecabinete = null;
            });
    }

    closeCabinete() {
        this.activecabinete = null;
        this.blockmode = false;
        this.blockreason = "";
    }

    identchange(event: Event) {
        //alert(event.);
        //event.target.
        
        //if (this.newUserind.length > 14) {
        //    this.newUserind = this.newUserind.substring(0, 14);
        //}
        this.reasonindchanged = true;
    }

    getAutobiography(cabinet: Cabinetdata): Autobiographydata {
        
        if (cabinet.autobiographydatalist == null) {
            return null;
        }
        if (cabinet.autobiographydatalist.length == 0) {
            return null;
        }
        return cabinet.autobiographydatalist[0]; 
    }

    getProfile(cabinet: Cabinetdata): Profiledata {

        if (cabinet.profiledatalist == null) {
            return null;
        }
        if (cabinet.profiledatalist.length == 0) {
            return null;
        }
        return cabinet.profiledatalist[0];
    }

    getProfileeducations(cabinet: Cabinetdata): Pseducation[] {
        if (cabinet.pseducationlist == null) {
            return null;
        }
        if (cabinet.pseducationlist.length == 0) {
            return null;
        }
        //cabinet.pseducationlist[0].facultyeducation
        return cabinet.pseducationlist;
        //todooooo
    }

    getProfileworks(cabinet: Cabinetdata): Pswork[] {
        if (cabinet.psworklist == null) {
            return null;
        }
        if (cabinet.psworklist.length == 0) {
            return null;
        }
        return cabinet.psworklist;
    }

    getProfilerelatives(cabinet: Cabinetdata): Profilerelatives[] {
        if (cabinet.profilerelativeslist == null) {
            return null;
        }
        if (cabinet.profilerelativeslist.length == 0) {
            return null;
        }
        return cabinet.profilerelativeslist;
    }

    getSheet(cabinet: Cabinetdata): Sheetdata {

        if (cabinet.sheetdatalist == null) {
            return null;
        }
        if (cabinet.sheetdatalist.length == 0) {
            return null;
        }
        return cabinet.sheetdatalist[0];
    }

    getSheetpolitics(cabinet: Cabinetdata): Sheetpolitics[] {

        if (cabinet.sheetpoliticslist == null) {
            return null;
        }
        if (cabinet.sheetpoliticslist.length == 0) {
            return null;
        }
        return cabinet.sheetpoliticslist;
    }

    getDeclaration(cabinet: Cabinetdata): Declarationdata {

        if (cabinet.declarationdatalist == null) {
            return null;
        }
        if (cabinet.declarationdatalist.length == 0) {
            return null;
        }
        return cabinet.declarationdatalist[0];
    }

    getDeclarationrelatives(cabinet: Cabinetdata): Declarationrelative[] {

        if (cabinet.declarationrelativelist == null) {
            return null;
        }
        if (cabinet.declarationrelativelist.length == 0) {
            return null;
        }
        return cabinet.declarationrelativelist;
    }

    getDeclarationtables(cabinet: Cabinetdata, section: number, point: number): Declarationtabledata[] {

        if (cabinet.declarationtabledatalist == null) {
            return null;
        }
        if (cabinet.declarationtabledatalist.length == 0) {
            return null;
        }
        if (section > 0 && point > 0) {
            return cabinet.declarationtabledatalist.filter(d => d.declarationsection == section && d.declarationpoint == point);
        } else if (section > 0) {
            return cabinet.declarationtabledatalist.filter(d => d.declarationsection == section);
        } else if (point > 0) {
            return cabinet.declarationtabledatalist.filter(d => d.declarationpoint == point);
        } else {
            return cabinet.declarationtabledatalist;
        }
        
    }

    toggleautobiography(cabinet: Cabinetdata) {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                id: cabinet.id,
                action: 2,
                status: this.getAutobiography(cabinet).autobiographylockunlock,

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
                this.openCabinete(cabinet.id);
            });
    }

    toggleprofile(cabinet: Cabinetdata) {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                id: cabinet.id,
                action: 3,
                status: this.getProfile(cabinet).profilelockunlock,

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
                this.openCabinete(cabinet.id);
            });
    }

    togglesheet(cabinet: Cabinetdata) {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                id: cabinet.id,
                action: 4,
                status: this.getSheet(cabinet).sheetlockunlock,

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
                this.openCabinete(cabinet.id);
            });
    }

    toggledeclaration(cabinet: Cabinetdata) {
        fetch('/api/Cabinet', {
            method: 'post',
            body: JSON.stringify(<Cabinetdata>{
                id: cabinet.id,
                action: 5,
                status: this.getDeclaration(cabinet).declarationlockunlock,

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
                this.openCabinete(cabinet.id);
            });
    }

    toDateInputValue(date: Date): string {

        var local = new Date(date);
        local.setMinutes(local.getMinutes() - local.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

    printDate(date: Date): string {
        if (date == null) {
            return "";
        }
        return this.beautifyDate(this.toDateInputValue(date));
    }

    printDateCabinet(cabinet: Cabinetdata): string {
        //alert(cabinet.creationdate);
        //if (cabinet.creationdate > this.oldDate) {
        if (!this.compareDates(cabinet.creationdate, this.oldDate)) {
            return "-";
        } else {
            return this.printDate(cabinet.creationdate);
        }
        
    }

    compareDates(day1: Date, day2: Date): boolean {
        let d1: Date = new Date(day1);
        let d2: Date = new Date(day2);
        //alert(d1 + "     " + d2);
        if (d1.getFullYear() > d2.getFullYear()) {
            return true;
        } else if (d1.getFullYear() < d2.getFullYear()) {
            return false;
        } else if (d1.getMonth() > d2.getMonth()) {
            return true;
        } else if (d1.getMonth() < d2.getMonth()) {
            return false;
        } else if (d1.getDate() > d2.getDate()) {
            return true;
        }
        return false;
    }

    dateNow(): string {
        return this.printDate(new Date());
    }

    previousYear(): string {
        let d = new Date();
        let pastYear = d.getFullYear() - 1;
        d.setFullYear(pastYear);

        return d.getFullYear().toString();
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

    displayAutobiographydata(cabinet: Cabinetdata): string {
        if (this.getAutobiography(cabinet).autobiographylockunlock == 0) {
            return this.toDateInputValue(new Date());
        } else {
            return this.toDateInputValue(this.getAutobiography(cabinet).autobiographysignature)
        }
    }

    //activecabinete - основная поступающая переменная
    profileFullname(cabinet: Cabinetdata): string {

        let profile: Profiledata = this.getProfile(cabinet);
        let fullName: string = profile.profilesurname + " " + profile.profilename + " " + profile.profilepatronymic;
        if (profile.profileothersurnames.length > 0) {
            fullName += "; Прежние фамилии: " + profile.profileothersurnames;
        }

        return fullName;
    }

    profilePhone(cabinet: Cabinetdata): string {
        let profile: Profiledata = this.getProfile(cabinet);
        if (profile == null){
            return "";
        }
        let phone: string = "";
        if (profile.profilehomephone != null && profile.profilehomephone.length > 0) {
            phone += "Домашний телефон: " + profile.profilehomephone + "; ";
        }
        if (profile.profileworkphone != null && profile.profileworkphone.length > 0) {
            phone += "Рабочий телефон: " + profile.profileworkphone + "; ";
        }
        if (profile.profilemobilephone != null && profile.profilemobilephone.length > 0) {
            phone += "Мобильный телефон: " + profile.profilemobilephone + "; "
        }
        return phone;
    }

    sheetPhone(cabinet: Cabinetdata): string {
        let sheet: Sheetdata = this.getSheet(cabinet);
        if (sheet == null) {
            return "";
        }
        let phone: string = "";
        if (sheet.sheethomephone != null && sheet.sheethomephone.length > 0) {
            phone += "Домашний телефон: " + sheet.sheethomephone + "; ";
        }
        if (sheet.sheetworkphone != null && sheet.sheetworkphone.length > 0) {
            phone += "Рабочий телефон: " + sheet.sheetworkphone + "; ";
        }
        if (sheet.sheetmobilephone != null && sheet.sheetmobilephone.length > 0) {
            phone += "Мобильный телефон: " + sheet.sheetmobilephone + "; "
        }
        return phone;
    }

    displayAutobiography(activecabinete: Cabinetdata) {
        let url: string = externalAdressCandidates;
        url += "type=autobiography";
        url += "&id=" + activecabinete.id;
        url += "&code=" + activecabinete.accesscode;
        Object.assign(document.createElement('a'), { target: '_blank', href: url }).click(); 
    }

    displayProfile(activecabinete: Cabinetdata) {
        let url: string = externalAdressCandidates;
        url += "type=profile";
        url += "&id=" + activecabinete.id;
        url += "&code=" + activecabinete.accesscode;
        Object.assign(document.createElement('a'), { target: '_blank', href: url }).click();
    }

    displaySheet(activecabinete: Cabinetdata) {
        let url: string = externalAdressCandidates;
        url += "type=sheet";
        url += "&id=" + activecabinete.id;
        url += "&code=" + activecabinete.accesscode;
        Object.assign(document.createElement('a'), { target: '_blank', href: url }).click();
    }

    displayDeclaration(activecabinete: Cabinetdata) {
        let url: string = externalAdressCandidates;
        url += "type=declaration";
        url += "&id=" + this.getDeclaration(activecabinete).cabinetid;
        url += "&code=" + activecabinete.accesscode;
        Object.assign(document.createElement('a'), { target: '_blank', href: url }).click();
    }

    printAutobiography() {
        this.modalAutobiographyVisible = true;
        // ./css/styles.css
        // printAutobiography
        setTimeout(x => { printJS({ printable: 'printAutobiography', type: 'html' }) }, 1000);
    }

    printProfile() {
        this.modalProfileVisible = true;
        // printProfile
        setTimeout(x => { printJS({ printable: 'printProfile', type: 'html' }) }, 1000);
    }

    printSheet() {
        this.modalSheetVisible = true;
        // printSheet
        setTimeout(x => { printJS({ printable: 'printSheet', type: 'html' }) }, 1000);
    }

    printDeclaration() {
        this.modalDeclarationVisible = true;
        setTimeout(x => { printJS({ printable: 'printDeclaration', type: 'html'}) }, 1000);
    }

    allowAccept(cabinet: Cabinetdata): boolean {
        if (cabinet.reasonid == 1) {
            return this.allowAcceptFirst(cabinet);
        } else if (cabinet.reasonid == 2) {
            return this.allowAcceptSecond(cabinet);
        } else if (cabinet.reasonid == 3) {
            return this.allowAcceptThird(cabinet);
        }
        return false;
    }

    allowAcceptFirst(activecabinete: Cabinetdata): boolean {
        return this.getAutobiography(activecabinete).autobiographylockunlock == 1 &&
            this.getProfile(activecabinete).profilelockunlock == 1 &&
            this.getDeclaration(activecabinete).declarationlockunlock == 1;
    }

    allowAcceptSecond(activecabinete: Cabinetdata): boolean {
        return this.getAutobiography(activecabinete).autobiographylockunlock == 1 &&
            this.getProfile(activecabinete).profilelockunlock == 1 &&
            this.getSheet(activecabinete).sheetlockunlock == 1 &&
            this.getDeclaration(activecabinete).declarationlockunlock == 1;
    }

    allowAcceptThird(activecabinete: Cabinetdata): boolean {
        return this.getAutobiography(activecabinete).autobiographylockunlock == 1 &&
            this.getProfile(activecabinete).profilelockunlock == 1 &&
            this.getSheet(activecabinete).sheetlockunlock == 1
    }

    loginData() {
        fetch('/api/Cabinet/LoginDate/' + this.activecabinete.id, {
            method: 'get',
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        }).then(x => x.blob())
            .then(x => download(x, this.activecabinete.usersurname + "_candidate"))
    }
}