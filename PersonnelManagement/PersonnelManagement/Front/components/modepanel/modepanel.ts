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

@Component({
    components: {
    }
})
export default class CandidatesComponent extends Vue {

    @Prop({ default: 0 })
    visible: number;

    data() {
        return {
        }
    }

    positionListId() {
        alert(this.$store.state.positionsListId);
    }

    org() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.orgMode();

        fetch('api/Identity/Fullmode1', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})
    }

    eld() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.peopleMode();
        this.toggleEld();

        fetch('api/Identity/Fullmode2', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})
    }

    decree() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.decreeMode();
        this.toggleDecreeOperationElement();

        fetch('api/Identity/Fullmode5', { credentials: 'include' })
        //.then(response => {
        //    return response.json() as Promise<string>;
        //})
        //.then(result => {

        //})
    }


    candidates() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.peopleMode();
        this.toggleCandidates();

        fetch('api/Identity/Fullmode3', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})
    }

    people() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.peopleMode();

        fetch('api/Identity/Fullmode4', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //})
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

    structureRead(): boolean {
            if (this.$store.state.admin == "1" || this.$store.state.structurereadAccess == "1") {
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
        //alert(this.$store.state.structureeditor);
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

    orgMode() {
        fetch('api/Identity/Org', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //    //alert(JSON.stringify(result));
            //})
    }

    peopleMode() {
        fetch('api/Identity/People', { credentials: 'include' })
            //.then(response => {
            //    return response.json() as Promise<string>;
            //})
            //.then(result => {

            //    //alert(JSON.stringify(result));
            //})
    }

    decreeMode() {
        fetch('api/Identity/Decree', { credentials: 'include' })
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

    toggleDecreeOperationElement() {
        let toggle: number = this.$store.state.decreeoperationelementVisible;
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
        this.$store.commit("setdecreeoperationelementVisible", toggle);
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

}