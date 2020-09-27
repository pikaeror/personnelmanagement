import Vue from 'vue';
import { Component } from 'vue-property-decorator';

@Component({
    components: {
        SideBar: require('../sidebar/sidebar.vue.html'),
        TopMenu: require('../topmenu/topmenu.vue.html'),
    }
})
export default class AppComponent extends Vue {
    sidebarDisplay: boolean;
    firstLaunch: boolean;

    data(){
        return{
            sidebarDisplay: true,
            firstLaunch: true,
        }
    }

    mounted() {
        this.updateUserRights();
        this.updateRanksGlobal();
        this.updateSofsGlobal();
        this.updatePositiontypes();
        this.updatePositioncategories();
        this.updateMrds();
        this.updateAltrankconditiongroups();
        this.updateAltrankconditions();
        this.updateStructureregions();
        this.updateStructuretypes();
        this.updateRelativetypes();
        this.updateAttestationtypes();
        this.updateVacationmilitaries();
        this.updateVacationtypes();
        this.updateLanguagetypes();
        this.updateLanguageskills();
        this.updateJobtypes();
        this.updateServicetypes();
        this.updateServicefeatures();
        this.updateServicecoefs();
        this.updatePenalties();
        this.updateCountries();
        this.updateIllregimes();
        this.updateIllcodes();
        this.updateRewardtypes();
        this.updateRewards();
        this.updateEducationlevels();
        this.updateEducationtypes();
        this.updateEducationdocuments();
        this.updateNormativs();
        this.updateDrivertypes();
        this.updateDrivercategories();
        this.updatePermissiontypes();
        this.updateProoftypes();
        this.updateHolidays();
        this.updatePersondecreeblocktypes();
        this.updatePersondecreeblocksubtypes();
        this.updateRegions();
        this.updateAreas();
        this.updateFires();
        this.updateAppointtypes();
        this.updateTransfertypes();
        this.updateSubjects();
        this.updateSubjectgenders();
        this.updateSubjectcategories();
        this.updateInterrupttypes();
        this.updateChangedocumentstypes();
        this.updateSetpersondatatypes();
        this.updateRewardmoneys();
        this.updateOrdernumbertypes();
        this.updateStructuresalldocument();
        this.updateCitytypes();
        this.updateStreettypes();
        this.updateAreaothers();
        this.updateExternalorderwhotypes();
        this.updatePersondecreetypes();
        this.updateEducationadditionaltypes();
        this.updateCitysubstates();
        this.updateEducationstages();
        this.updateEducationpositiontypes();
        this.updateRoles();
        
        setInterval(this.checkSidebar, 400);
        setInterval(this.updateUserRights, 1500);
        //setInterval(this.updateRanksGlobal, 30000); 
        //setInterval(this.updateSofsGlobal, 30000);

        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 0,
        }

        this.$store.commit("updateUserAppearance", appearance);

        
    }


    checkSidebar() {
        if (this.$store.state.sidebarDisplay > 0) {
            this.sidebarDisplay = true;
        } else {
            this.sidebarDisplay = false;
        }
    }

    updateUserRights() {
        (<any>Vue).getAccessStatus().then(s => {
            this.$store.commit("setUserStructure", s[(<any>Vue).keys["IDENTITY_STRUCTURE_KEY"]]);
            this.$store.commit("setLogin", s[(<any>Vue).keys["IDENTITY_LOGIN_KEY"]]);
            this.$store.commit("setCurrentStructureTree", s[(<any>Vue).keys["IDENTITY_CURRENTSTRUCTURETREE_KEY"]]);
            this.$store.commit("setMasterpersonneleditorAccess", s[(<any>Vue).keys["IDENTITY_MASTERPERSONNELEDITOR_KEY"]]);
            this.$store.commit("setStructureeditorAccess", s[(<any>Vue).keys["IDENTITY_STRUCTUREEDITOR_KEY"]]);
            this.$store.commit("setStructurereadAccess", s[(<any>Vue).keys["IDENTITY_STRUCTUREREAD_KEY"]]);
            this.$store.commit("setPersonneleditorAccess", s[(<any>Vue).keys["IDENTITY_PERSONNELEDITOR_KEY"]]);
            this.$store.commit("setPersonnelreadAccess", s[(<any>Vue).keys["IDENTITY_PERSONNELREAD_KEY"]]);
            this.$store.commit("setMode", s[(<any>Vue).keys["IDENTITY_MODE_KEY"]]);
            this.$store.commit("setAdmin", s[(<any>Vue).keys["IDENTITY_ADMIN_KEY"]]); 
            this.$store.commit("setDecree", s[(<any>Vue).keys["IDENTITY_DECREE"]]);
            this.$store.commit("setDecreeName", s[(<any>Vue).keys["IDENTITY_DECREE_NAME"]]);
            this.$store.commit("setPositionCompact", s[(<any>Vue).keys["IDENTITY_POSITIONCOMPACT"]]); 
            this.$store.commit("setDateFromDB", s[(<any>Vue).keys["IDENTITY_DATE_KEY"]]);

            this.$store.commit("setFullmode", s[(<any>Vue).keys["IDENTITY_FULLMODE_KEY"]]);
            if (this.firstLaunch) {
                this.firstLaunch = false;

                if (this.$store.state.fullmode == "1") {
                    this.org();
                } else if (this.$store.state.fullmode == "2") {
                    this.eld();
                } else if (this.$store.state.fullmode == "3") {
                    this.candidates();
                } else if (this.$store.state.fullmode == "4") {
                    this.people();
                } else if (this.$store.state.fullmode == "5") {
                    this.decree();
                }
            }

            if (this.$store.state.fullmode != "1") {
                this.$store.commit("setSidebarDisplay", s[(<any>Vue).keys["IDENTITY_SIDEBAR_DISPLAY_KEY"]]);
            } else {
                this.$store.commit("setSidebarDisplay", "1");
            }
        });

        /**
         * Новый механизм передачи прав доступа 
         */
        (<any>Vue).getUserStatus().then(u => {
            this.$store.commit("setUser", u);
        });
    }

    org() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.orgMode();

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

    }

    people() {
        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("updateUserAppearance", appearance);
        this.$store.commit("setModepanelVisible", 0);
        this.peopleMode();

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

    decreeMode() {
        fetch('api/Identity/Decree', { credentials: 'include' })
    }

    peopleMode() {
        //fetch('api/Identity/People', { credentials: 'include' })
        //    .then(response => {
        //        return response.json() as Promise<string>;
        //    })
        //    .then(result => {

        //        //alert(JSON.stringify(result));
        //    })

        fetch('api/Identity/People', { credentials: 'include' })
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

    updateRanksGlobal() {
        this.$store.commit("updateRanks");
    }

    updateSofsGlobal() {
        this.$store.commit("updateSofs");
    }

    updatePositiontypes() {
        this.$store.commit("updatePositiontypes");
    }

    updatePositioncategories() {
        this.$store.commit("updatePositioncategories");
    }

    updateMrds() {
        this.$store.commit("updateMrds");
    }

    updateAltrankconditiongroups() {
        this.$store.commit("updateAltrankconditiongroups");
    }

    updateAltrankconditions() {
        this.$store.commit("updateAltrankconditions");
    }

    updateStructureregions() {
        this.$store.commit("updateStructureregions");
    }

    updateStructuretypes() {
        this.$store.commit("updateStructuretypes");
    }

    updateRelativetypes() {
        this.$store.commit("updateRelativetypes");
    }

    updateAttestationtypes() {
        this.$store.commit("updateAttestationtypes");
    }

    updateVacationmilitaries() {
        this.$store.commit("updateVacationmilitaries");
    }

    updateVacationtypes() {
        this.$store.commit("updateVacationtypes");
    }

    updateLanguagetypes() {
        this.$store.commit("updateLanguagetypes");
    }

    updateLanguageskills() {
        this.$store.commit("updateLanguageskills");
    }

    updateJobtypes() {
        this.$store.commit("updateJobtypes");
    }

    updateServicetypes() {
        this.$store.commit("updateServicetypes");
    }

    updateServicefeatures() {
        this.$store.commit("updateServicefeatures");
    }

    updateServicecoefs() {
        this.$store.commit("updateServicecoefs");
    }

    updatePenalties() {
        this.$store.commit("updatePenalties");
    }

    updateCountries() {
        this.$store.commit("updateCountries");
    }

    updateIllregimes() {
        this.$store.commit("updateIllregimes");
    }

    updateIllcodes() {
        this.$store.commit("updateIllcodes");
    }

    updateRewardtypes() {
        this.$store.commit("updateRewardtypes");
    }

    updateRewards() {
        this.$store.commit("updateRewards");
    }

    updateEducationlevels() {
        this.$store.commit("updateEducationlevels");
    }

    updateEducationtypes() {
        this.$store.commit("updateEducationtypes");
    }

    updateEducationdocuments() {
        this.$store.commit("updateEducationdocuments");
    }

    updateNormativs() {
        this.$store.commit("updateNormativs");
    }

    updateDrivertypes() {
        this.$store.commit("updateDrivertypes");
    }

    updateDrivercategories() {
        this.$store.commit("updateDrivercategories");
    }

    updatePermissiontypes() {
        this.$store.commit("updatePermissiontypes");
    }

    updateProoftypes() {
        this.$store.commit("updateProoftypes");
    }

    updateHolidays() {
        this.$store.commit("updateHolidays");
    }

    updatePersondecreeblocktypes() {
        this.$store.commit("updatePersondecreeblocktypes");
    }

    updatePersondecreeblocksubtypes() {
        this.$store.commit("updatePersondecreeblocksubtypes");
    }

    updateRegions() {
        this.$store.commit("updateRegions");
    }

    updateAreas() {
        this.$store.commit("updateAreas");
    }

    updateFires() {
        this.$store.commit("updateFires");
    }

    updateAppointtypes() {
        this.$store.commit("updateAppointtypes");
    }

    updateTransfertypes() {
        this.$store.commit("updateTransfertypes");
    }

    updateSubjects() {
        this.$store.commit("updateSubjects");
    }

    updateSubjectgenders() {
        this.$store.commit("updateSubjectgenders");
    }

    updateSubjectcategories() {
        this.$store.commit("updateSubjectcategories");
    }

    updateInterrupttypes() {
        this.$store.commit("updateInterrupttypes");
    }

    updateChangedocumentstypes() {
        this.$store.commit("updateChangedocumentstypes");
    }

    updateSetpersondatatypes() {
        this.$store.commit("updateSetpersondatatypes");
    }

    updateRewardmoneys() {
        this.$store.commit("updateRewardmoneys");
    }

    updatePersondecreelevels() {
        this.$store.commit("updatePersondecreelevels");
    }

    updateOrdernumbertypes() {
        this.$store.commit("updateOrdernumbertypes");
    }

    updateStructuresalldocument() {
        this.$store.commit("updateStructuresalldocument");
    }

    updateCitytypes() {
        this.$store.commit("updateCitytypes");
    }

    updateStreettypes() {
        this.$store.commit("updateStreettypes");
    }

    updateAreaothers() {
        this.$store.commit("updateAreaothers");
    }

    updateExternalorderwhotypes() {
        this.$store.commit("updateExternalorderwhotypes");
    }

    updatePersondecreetypes() {
        this.$store.commit("updatePersondecreetypes");
    }

    updateEducationadditionaltypes() {
        this.$store.commit("updateEducationadditionaltypes");
    }

    updateCitysubstates() {
        this.$store.commit("updateCitysubstates");
    }

    updateEducationstages() {
        this.$store.commit("updateEducationstages");
    }

    updateEducationpositiontypes() {
        this.$store.commit("updateEducationpositiontypes");
    }

    updateRoles() {
        this.$store.commit("updateRoles");
    }

    get sidebar() {
        return this.sidebarDisplay;
        //return (<any>Vue).sidebar;
    }
}
