import Vue from 'vue';
import { Component, Prop, Watch } from 'vue-property-decorator';
import Element from 'element-ui';
import { Input, Button, Dropdown, Dialog, DropdownItem, DropdownMenu, Popover, Checkbox, Tooltip, Upload, Autocomplete } from 'element-ui';
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
import Personpfl from '../../classes/personpfl';
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
import Prooftype from '../../classes/prooftype';
import '../../css/print.css';
import Educationdocument from '../../classes/educationdocument';
import Holiday from '../../classes/holiday';
import Jobperiod from '../../classes/jobperiod';
import Region from '../../classes/region'
import Area from '../../classes/area'
import moment from 'moment';
import Structure from '../../classes/structure'
import Rewardmoney from '../../classes/rewardmoney'
import Subject from '../../classes/subject'
import Subjectcategory from '../../classes/subjectcategory'
import Subjectgender from '../../classes/subjectgender'
import Ordernumbertype from '../../classes/ordernumbertype';
import Link from '../../classes/link';
import Personadditionalagreement from '../../classes/personadditionalagreement';
import Streettype from '../../classes/streettype';
import Citytype from '../../classes/citytype';
import Areaother from '../../classes/areaother';
import Externalorderwhotype from '../../classes/externalorderwhotype';
import Persondecreetype from '../../classes/persondecreetype';
import Educationadditionaltype from '../../classes/educationadditionaltype';
import Citysubstate from '../../classes/citysubstate';
import Educationstage from '../../classes/educationstage';
import Educationpositiontype from '../../classes/educationpositiontype';
import Educationtypeblock from '../../classes/educationtypeblock';
import Educationperiod from '../../classes/educationperiod';
import Academicvacation from '../../classes/academicvacation';
import Educationmaternity from '../../classes/educationmaternity';
import Personeducationpart from '../../classes/personeducationpart';
import Personjobprivelegeperiod from '../../classes/personjobprivelegeperiod';
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

const EDIT_LABEL = "Редактировать";
const SAVE_LABEL = "Сохранить";

class Filelist {
    name: string;
    url: string;
}

@Component({
    components: {

    }
})
export default class EldComponent extends Vue {

    @Prop({ default: 0 })
    visible: number;

    fiosearch: string;
    createstring: string;
    creategender: string;
    createmenuvisible: boolean;
    menuid: number;
    person: Person;
    personssearch: Person[];
    filelist: Filelist[];
    lastUploadedPhoto: any;
    photos: Personphoto[];
    photosPreview: Personphoto[]; // Если мы что-то ищем в поиске, то заодно будем загружать предварительные фотографии
    photoToCreate: Personphoto;

    lastUploadedPfl: any;

    photoName: string;
    photoDescription: string;

    lastSearchFio: string;

    personrelativeMenuvisible: boolean;
    personrelativeMenuelement: Personrelative;
    personrelativeType: number;
    personrelativeFio: string;
    personrelativeFioother: string;
    personrelativeBirthdayString: string;
    personrelativeBirthplace: string;
    personrelativeJobplace: string;
    personrelativeJobposition: string;
    personrelativeLivecountry: string;
    personrelativeLivestate: string;
    personrelativeLivesubstate: string;
    personrelativeLivecitysubstate: number;
    personrelativeLivecitytype: string;
    personrelativeLivecity: string;
    personrelativeLivestreettype: string;
    personrelativeLivestreet: string;
    personrelativeLivehouse: string;
    personrelativeLivehousing: string;
    personrelativeLiveflat: string;
    personrelativeBirthcountry: string;
    personrelativeBirthstate: string;
    personrelativeBirthsubstate: string;
    personrelativeBirthcitysubstate: number;
    personrelativeBirthcitytype: string;
    personrelativeBirthcity: string;
    personrelativeBirthstreettype: string;
    personrelativeBirthstreet: string;
    personrelativeBirthadditional: string;
    personrelativeBirthadditionaldisplay: boolean;
    personrelativeNodata: boolean;
    personrelativeDeath: boolean;
    personrelativeDeathnodata: boolean;
    personrelativeDeathcountry: string;
    personrelativeDeathstate: string;
    personrelativeDeathsubstate: string;
    personrelativeDeathcitysubstate: number;
    personrelativeDeathcitytype: string;
    personrelativeDeathcity: string;
    personrelativeDeathstreettype: string;
    personrelativeDeathstreet: string;
    personrelativeDeathadditional: string;
    personrelativeDeathadditionaldisplay: boolean;


    personcontractMenu: number;
    personcontractStatecivilSelect: boolean;
    personcontractMenuvisible: boolean;
    personcontractMenuelement: Personcontract;
    personcontractDatestart: string;
    personcontractDateend: string;
    personcontractPay: boolean;
    personcontractOrdernumber: string;
    personcontractOrdernumbertype: string;
    personcontractOrderdate: string;
    personcontractOrderwho: string;
    personcontractOrderwhoid: number;
    personcontractOrderid: number;
    personcontractSourceoffinancing: number;
    personcontractPayvalue: number;
    personcontractStateserviceyears: number;
    personcontractStateservicemonths: number;
    personcontractStateservicedays: number;
    personcontractVacationdays: number;

    personrankMenuvisible: boolean;
    personrankMenuelement: Personrank;
    personrankType: number;
    personrankRankstring: string;
    personrankStructure: string;
    personrankStructureid: number;
    personrankNumber: string;
    personrankDate: string;
    personrankDecreenumbertype: string;
    personrankDecreeid: number;
    personrankDatestart: string;

    personattestationMenuvisible: boolean;
    personattestationMenuelement: Personattestation;
    personattestationType: number;
    personattestationDate: string;
    personattestationResult: string;
    personattestationRecomendation: string;

    personvacationMenuvisible: boolean;
    personvacationMenuelement: Personvacation;
    personvacationMilitary: number;
    personvacationType: number;
    personvacationDate: string;
    personvacationDateend: string; // Для ввода по беремености и родам
    personvacationDuration: number;
    personvacationTrip: number;
    personvacationCompensation: boolean;
    personvacationCompensationdate: string;
    personvacationCompensationnumber: string;
    personvacationCompensationdays: number;
    personvacationCancel: boolean;
    personvacationCanceldate: string;
    personvacationAllowstart: string;
    personvacationAllowend: string;
    personvacationHolidays: number;
    personvacationCanceldateend: string;
    personvacationCancelcontinue: boolean;

    personlanguageMenuvisible: boolean;
    personlanguageMenuelement: Personlanguage;
    personlanguageLanguagetype: number;
    personlanguageLanguageskill: number;


    personjobMenu: number;

    personjobMenuvisible: boolean;
    personjobMenuelement: Personjob;
    personjobJobtype: number;
    personjobStart: string;
    personjobEnd: string;
    personjobJobplace: string;
    personjobJobposition: string;
    personjobJobpositionplace: string;
    personjobServicetype: number;
    personjobServicetypestr: string;
    personjobServicefeature: number;
    personjobServiceorder: string;
    personjobServicecoef: number;
    personjobServiceplace: string;
    personjobOrdernumber: string;
    personjobOrdernumbertype: string;
    personjobOrderdate: string;
    personjobOrderwho: string;
    personjobActual: boolean;
    personjobManual: boolean; // Добавление вручную
    personjobMchs: boolean;
    personjobVacationdays: number;
    personjobPosition: number;
    personjobPositiontoselect: number;
    personjobStatecivil: boolean;
    personjobStatecivilstart: string;
    personjobStatecivilend: string;
    personjobStartcustom: string;
    personjobPrivelegebool: boolean;
    personjobPersonjobpriveleges: Personjobprivelege[]; 

    personpenaltyMenuvisible: boolean;
    personpenaltyMenuelement: Personpenalty;
    personpenaltyPenalty: number;
    personpenaltyViolation: string;
    personpenaltyOrderwho: string;
    personpenaltyOrdernumber: string;
    personpenaltyOrderdate: string;

    personworktripMenuvisible: boolean;
    personworktripMenuelement: Personworktrip;
    personworktripCountry: number;
    personworktripTripdate: string;
    personworktripReason: string;
    personworktripDays: number;
    personworktripPrivelege: boolean;

    personelectionMenuvisible: boolean;
    personelectionMenuelement: Personelection;
    personelectionLocation: string;
    personelectionElectionwho: string;
    personelectionElectiondate: string;
    personelectionElectionwhat: string;
    personelectionElectiondateend: string;

    personscienceMenuvisible: boolean;
    personscienceMenuelement: Personscience;
    personscienceSciencetype: number;
    personscienceSciencedescription: string;
    personscienceSciencedate: string;
    personscienceSciencediplom: string;

    personrewardMenuvisible: boolean;
    personrewardMenuelement: Personreward;
    personrewardRewardtype: number;
    personrewardReward: number;
    personrewardReason: string;
    personrewardOrder: string;
    personrewardOrdernumbertype: string;
    personrewardDate: string;
    personrewardOptionstring1: string;
    personrewardOptionnumber1: number;
    personrewardOptionstring2: string;
    personrewardOptionnumber2: number;
    personrewardOrderwho: string;
    personrewardOrderwhoid: number;
    personrewardOrderid: number;
    personrewardArea: number;
    personrewardAreaother: number;
    personrewardAreaotherdisplay: boolean;
    personrewardExternalorderwhotype: string;
    personrewardExternalordertype: number;

    personillMenuvisible: boolean;
    personillMenuelement: Personill;
    personillIlltype: boolean;
    personillIllcode: number;
    personillDatestart: string;
    personillDateend: string;
    personillIllregime: number;
    personillIllwho: string;
    personillPrivelege: boolean;

    personeducationMenuvisible: boolean;
    personeducationMenuelement: Personeducation;
    personeducationMain: number;
    personeducationEducationlevel: number;
    personeducationEducationstage: number;
    personeducationName: string;
    personeducationName2: string;
    personeducationLocation: string;
    personeducationCity: string;
    personeducationFaculty: string;
    personeducationEducationtype: number;
    personeducationDatestart: number;
    personeducationDateend: number;
    personeducationSpeciality: string;
    personeducationDocumentseries: string;
    personeducationDocumentnumber: string;
    personeducationCadet: boolean;
    personeducationQualification: string;
    personeducationStart: string;
    personeducationEnd: string;
    personeducationInterrupted: boolean;
    personeducationInterruptorderdate: string;
    personeducationInterruptorderwho: string;
    personeducationInterruptordernumber: string;
    personeducationInterruptordernumbertype: string;
    personeducationInterruptorderreason: string;
    personeducationEducationdocument: number;
    personeducationOrdernumber: string;
    personeducationOrdernumbertype: string;
    personeducationOrderdate: string;
    personeducationOrderwho: string;
    personeducationOrderwhoid: number;
    personeducationOrderid: number;
    personeducationNameasjobfull: string;
    personeducationNameasjobposition: string;
    personeducationNameasjobplace: string;
    personeducationEducationadditionaltype: number;
    personeducationUcp: number;
    personeducationAcademicvacation: boolean;
    personeducationMaternityvacation: boolean;
    personeducationEducationtypeblocks: Educationtypeblock[];
    personeducationAcademicvacations: Academicvacation[];
    personeducationEducationmaternities: Educationmaternity[];
    personeducationRating: number;
    personeducationState: string;
    personeducationCitytype: string;

    personphysicalMenuvisible: boolean;
    personphysicalMenuelement: Personphysical; 
    personphysicalPhysicalfields: Physicalfield[];
    personphysicalPhysicaldate: string;

    physicalfieldNormativ: number;
    physicalfieldResult: string;

    personotherMenu: number;

    persondriverMenuvisible: boolean;
    persondriverMenuelement: Persondriver;
    persondriverDrivertype: number;
    persondriverSeries: string;
    persondriverNumber: string;
    persondriverDatestart: string;
    persondriverDateend: string;
    persondriverDrivercategory: number;

    personpermissionMenuvisible: boolean;
    personpermissionMenuelement: Personpermission;
    personpermissionPermissiontype: number;
    personpermissionNumber: string;
    personpermissionDatestart: string;
    personpermissionDateend: string;

    personprivelegeMenuvisible: boolean;
    personprivelegeMenuelement: Personprivelege;
    personprivelegeName: string;

    personhealthMenu: number;

    persondispanserizationMenuvisible: boolean;
    persondispanserizationMenuelement: Persondispanserization;
    persondispanserizationGroup: number;
    persondispanserizationResult: string;
    persondispanserizationDate: string;

    personvvkMenuvisible: boolean;
    personvvkMenuelement: Personvvk;
    personvvkNumber: string;
    personvvkResult: string;
    personvvkDate: string;

    personjobprivelegeMenuvisible: boolean;
    personjobprivelegeMenuelement: Personjobprivelege;
    personjobprivelegeStart: string;
    personjobprivelegeEnd: string;
    personjobprivelegeCoef: number;
    personjobprivelegeProoftype: number;
    personjobprivelegeProofdate: string;
    personjobprivelegeProofnumber: string;
    personjobprivelegeProoftext: string;
    personjobprivelegeDocumentorder: string;
    personjobprivelegeDocumentdate: string;
    personjobprivelegeDocumentnumber: string;

    indchanged: boolean;
    passchanged: boolean;

    structuresReward: Structure[];
    structuresRewardAllowedToSelect: Structure[];
    structuresElders: Structure[];

    rewardmoneys: Rewardmoney[];

    pension_A: string;
    pension_B: string;

    peoplewhothoutjobplace: (Person[])[];

    data() {
        return {
            fiosearch: "",
            creategender: "Мужской",
            createstring: "",
            createmenuvisible: false,
            menuid: 1,
            person: null,
            personssearch: [],
            filelist: [],
            lastUploadedPhoto: null,
            photos: [],
            photosPreview: [],
            photoToCreate: new Personphoto(),

            lastUploadedPfl: null,

            photoName: "Фотография",
            photoDescription: "",

            
            personrelativeMenuvisible: false,
            personrelativeMenuelement: null,
            personrelativeType: null,
            personrelativeFio: "",
            personrelativeFioother: "",
            personrelativeBirthdayString: "",
            personrelativeBirthplace: "",
            personrelativeJobplace: "",
            personrelativeJobposition: "",
            personrelativeLivecountry: "",
            personrelativeLivestate: "",
            personrelativeLivesubstate: "",
            personrelativeLivecitysubstate: null,
            personrelativeLivecitytype: "",
            personrelativeLivecity: "",
            personrelativeLivestreettype: "",
            personrelativeLivestreet: "",
            personrelativeLivehouse: "",
            personrelativeLivehousing: "",
            personrelativeLiveflat: "",
            personrelativeBirthcountry: "",
            personrelativeBirthstate: "",
            personrelativeBirthsubstate: "",
            personrelativeBirthcitysubstate: null,
            personrelativeBirthcitytype: "",
            personrelativeBirthcity: "",
            personrelativeBirthadditional: "",
            personrelativeBirthadditionaldisplay: false,
            personrelativeNodata: false,
            personrelativeDeath: false,
            personrelativeDeathnodata: false,
            personrelativeDeathcountry: "",
            personrelativeDeathstate: "",
            personrelativeDeathcitysubstate: null,
            personrelativeDeathsubstate: "",
            personrelativeDeathcitytype: "",
            personrelativeDeathcity: "",
            personrelativeDeathadditional: "",
            personrelativeDeathadditionaldisplay: false,


            personcontractMenu: 1,
            personcontractStatecivilSelect: false,
            personcontractMenuvisible: false,
            personcontractMenuelement: null,
            personcontractDatestart: "",
            personcontractDateend: "",
            personcontractPay: false,
            personcontractOrdernumber: "",
            personcontractOrdernumbertype: "",
            personcontractOrderdate: "",
            personcontractOrderwho: "",
            personcontractOrderwhoid: null,
            personcontractOrderid: null,
            personcontractSourceoffinancing: null,
            personcontractPayvalue: null,
            personcontractStateserviceyears: null,
            personcontractStateservicemonths: null,
            personcontractStateservicedays: null,
            personcontractVacationdays: null,

            personrankMenuvisible: false,
            personrankMenuelement: null,
            personrankType: null,
            personrankRankstring: "",
            personrankStructure: "",
            personrankStructureid: null,
            personrankNumber: "",
            personrankDate: "",
            personrankDecreenumbertype: "",
            personrankDecreeid: null,
            personrankDatestart: "",

            personattestationMenuvisible: false,
            personattestationMenuelement: null,
            personattestationType: null,
            personattestationDate: "",
            personattestationResult: "",
            personattestationRecomendation: "",

            personvacationMenuvisible: false,
            personvacationMenuelement: Personvacation,
            personvacationMilitary: null,
            personvacationType: null,
            personvacationDate: "",
            personvacationDateend: "",
            personvacationDuration: 0,
            personvacationTrip: 0,
            personvacationCompensation: false,
            personvacationCompensationdate: "",
            personvacationCompensationnumber: "",
            personvacationCompensationdays: 0,
            personvacationCancel: false,
            personvacationCanceldate: "",
            personvacationAllowstart: "",
            personvacationAllowend: "",
            personvacationHolidays: null,
            personvacationCanceldateend: "",
            personvacationCancelcontinue: false,

            personlanguageMenuvisible: false,
            personlanguageMenuelement: null,
            personlanguageLanguagetype: null,
            personlanguageLanguageskill: null,

            personjobMenu: 1,

            personjobMenuvisible: false,
            personjobMenuelement: null,
            personjobJobtype: null,
            personjobStart: "",
            personjobEnd: "",
            personjobJobplace: "",
            personjobJobposition: "",
            personjobJobpositionplace: "",
            personjobServicetype: null,
            personjobServicetypestr: "",
            personjobServicefeature: null,
            personjobServiceorder: "",
            personjobServicecoef: null,
            personjobServiceplace: "",
            personjobOrdernumber: "",
            personjobOrdernumbertype: "",
            personjobOrderdate: null,
            personjobOrderwho: "",
            personjobActual: false,
            personjobManual: false,
            personjobMchs: false,
            personjobVacationdays: null,
            personjobPosition: 0,
            personjobPositiontoselect: 0,
            personjobStatecivil: false,
            personjobStatecivilstart: "",
            personjobStatecivilend: "",
            personjobStartcustom: "",
            personjobPrivelegebool: false,
            personjobPersonjobpriveleges: [],

            personpenaltyMenuvisible: false,
            personpenaltyMenuelement: null,
            personpenaltyPenalty: null,
            personpenaltyViolation: "",
            personpenaltyOrderwho: "",
            personpenaltyOrdernumber: "",
            personpenaltyOrderdate: "",

            personworktripMenuvisible: false,
            personworktripMenuelement: null,
            personworktripCountry: null,
            personworktripTripdate: "",
            personworktripReason: "",
            personworktripDays: null,
            personworktripPrivelege: false,

            personelectionMenuvisible: false,
            personelectionMenuelement: null,
            personelectionLocation: "",
            personelectionElectionwho: "",
            personelectionElectiondate: "",
            personelectionElectionwhat: "",
            personelectionElectiondateend: "",

            personscienceMenuvisible: false,
            personscienceMenuelement: null,
            personscienceSciencetype: null,
            personscienceSciencedescription: "",
            personscienceSciencedate: "",
            personscienceSciencediplom: "",

            personrewardMenuvisible: false,
            personrewardMenuelement: null,
            personrewardRewardtype: null,
            personrewardReward: null,
            personrewardReason: "",
            personrewardOrder: "",
            personrewardOrdernumbertype: "",
            personrewardDate: "",
            personrewardOptionstring1: "",
            personrewardOptionnumber1: null,
            personrewardOptionstring2: "",
            personrewardOptionnumber2: null,
            personrewardOrderwho: "",
            personrewardOrderwhoid: 0,
            personrewardOrderid: 0,
            personrewardArea: null,
            personrewardAreaother: null,
            personrewardAreaotherdisplay: false,
            personrewardExternalorderwhotype: "",
            personrewardExternalordertype: null,

            personillMenuvisible: false,
            personillMenuelement: null,
            personillIlltype: false,
            personillIllcode: null,
            personillDatestart: "",
            personillDateend: "",
            personillIllregime: null,
            personillIllwho: "",
            personillPrivelege: false,

            personeducationMenuvisible: false,
            personeducationMenuelement: null,
            personeducationMain: 1,
            personeducationEducationlevel: null,
            personeducationEducationstage: null,
            personeducationName: "",
            personeducationName2: "",
            personeducationLocation: "",
            personeducationCity: "",
            personeducationFaculty: "",
            personeducationEducationtype: null,
            personeducationDatestart: null,
            personeducationDateend: null,
            personeducationSpeciality: "",
            personeducationDocumentseries: "",
            personeducationDocumentnumber: "",
            personeducationCadet: false,
            personeducationQualification: "",
            personeducationStart: "",
            personeducationEnd: "",
            personeducationInterrupted: false,
            personeducationInterruptorderdate: null,
            personeducationInterruptorderwho: "",
            personeducationInterruptordernumber: "",
            personeducationInterruptordernumbertype: "",
            personeducationInterruptorderreason: "",
            personeducationEducationdocument: null,
            personeducationOrdernumber: "",
            personeducationOrdernumbertype: "",
            personeducationOrderdate: "",
            personeducationOrderwho: "",
            personeducationOrderwhoid: null,
            personeducationOrderid: null,
            personeducationNameasjobfull: "",
            personeducationNameasjobposition: "",
            personeducationNameasjobplace: "",
            personeducationEducationadditionaltype: null,
            personeducationUcp: "",
            personeducationAcademicvacation: false,
            personeducationMaternityvacation: false,
            personeducationEducationtypeblocks: [],
            personeducationAcademicvacations: [],
            personeducationEducationmaternities: [],
            personeducationRating: null,
            personeducationState: "",
            personeducationCitytype: "",

            personphysicalMenuvisible: false,
            personphysicalMenuelement: null,
            personphysicalPhysicalfields: [],
            personphysicalPhysicaldate: "",

            physicalfieldNormativ: null,
            physicalfieldResult: "",

            personotherMenu: 1,

            persondriverMenuvisible: false,
            persondriverMenuelement: null,
            persondriverDrivertype: null,
            persondriverSeries: "",
            persondriverNumber: "",
            persondriverDatestart: "",
            persondriverDateend: "",
            persondriverDrivercategory: null,

            personpermissionMenuvisible: false,
            personpermissionMenuelement: null,
            personpermissionPermissiontype: null,
            personpermissionNumber: "",
            personpermissionDatestart: "",
            personpermissionDateend: "",

            personprivelegeMenuvisible: false,
            personprivelegeMenuelement: null,
            personprivelegeName: "",

            personhealthMenu: 1,

            persondispanserizationMenuvisible: false,
            persondispanserizationMenuelement: null,
            persondispanserizationGroup: null,
            persondispanserizationResult: "",
            persondispanserizationDate: "",

            personvvkMenuvisible: false,
            personvvkMenuelement: null,
            personvvkNumber: "",
            personvvkResult: "",
            personvvkDate: "",

            personjobprivelegeMenuvisible: false,
            personjobprivelegeMenuelement: null,
            personjobprivelegeStart: "",
            personjobprivelegeEnd: "",
            personjobprivelegeCoef: 1,
            personjobprivelegeProoftype: null,
            personjobprivelegeProofdate: "",
            personjobprivelegeProofnumber: "",
            personjobprivelegeProoftext: "",
            personjobprivelegeDocumentorder: "",
            personjobprivelegeDocumentdate: "",
            personjobprivelegeDocumentnumber: "",

            indchanged: false,
            passchanged: false,

            structuresReward: [],
            structuresRewardAllowedToSelect: [],
            structuresElders: [],

            rewardmoneys: [],
            pension_A: "",
            pension_B: "",

            peoplewhothoutjobplace: [],
        }
    }

    mounted() {
        if (this.$store.state.eldSelectedperson > 0) {
            this.selectPerson(this.$store.state.eldSelectedperson);
            this.$store.commit("setEldSelectedperson", 0);
        }
        setInterval(this.changeInd, 500);
        setInterval(this.countHolidays, 2000);
        setInterval(this.appointPosition, 1000);
        setInterval(this.autoupdatePerson, 10000);
        setInterval(this.loadPeopleWhithoutJobPlace, 20000);

        //this.loadPeopleWhithoutJobPlace();
        this.fetchStructureRewards();
        this.fetchStructureRewardsAllowed();
        this.fetchStructureElders();

        let rewardmoney1: Rewardmoney = new Rewardmoney();
        rewardmoney1.id = 1;
        rewardmoney1.name = "должностного оклада";
        rewardmoney1.rewardmoneytype = "должностного оклада";
        let rewardmoney2: Rewardmoney = new Rewardmoney();
        rewardmoney2.id = 2;
        rewardmoney2.name = "оклада денежного содержания";
        rewardmoney2.rewardmoneytype = "рублей";
        let rewardmoney3: Rewardmoney = new Rewardmoney();
        rewardmoney3.id = 3;
        rewardmoney3.name = "расчетного денежного оклада";
        rewardmoney3.rewardmoneytype = "рублей";
        this.rewardmoneys.push(rewardmoney1);
        this.rewardmoneys.push(rewardmoney2);
        this.rewardmoneys.push(rewardmoney3);
    }

    changeInd() {
        if (this.person != null && this.indchanged) {
            this.person.passportid = this.cyrToLat(this.person.passportid);
            var letterNumber = /^[0-9a-zA-Z]+$/;
            if (this.person.passportid.length > 0) {

                let lastletter = this.person.passportid[this.person.passportid.length - 1];
                if (!lastletter.match(letterNumber)) {
                    this.person.passportid = this.person.passportid.substring(0, this.person.passportid.length - 1);
                }
            }//person.passportid
            if (this.person.passportid.length > 0 && !this.isNum(this.person.passportid[0])) {
                this.person.passportid = this.person.passportid.substring(0, 0);
            }
            if (this.person.passportid.length > 1 && !this.isNum(this.person.passportid[1])) {
                this.person.passportid = this.person.passportid.substring(0, 1);
            }
            if (this.person.passportid.length > 2 && !this.isNum(this.person.passportid[2])) {
                this.person.passportid = this.person.passportid.substring(0, 2);
            }
            if (this.person.passportid.length > 3 && !this.isNum(this.person.passportid[3])) {
                this.person.passportid = this.person.passportid.substring(0, 3);
            }
            if (this.person.passportid.length > 4 && !this.isNum(this.person.passportid[4])) {
                this.person.passportid = this.person.passportid.substring(0, 4);
            }
            if (this.person.passportid.length > 5 && !this.isNum(this.person.passportid[5])) {
                this.person.passportid = this.person.passportid.substring(0, 5);
            }
            if (this.person.passportid.length > 6 && !this.isNum(this.person.passportid[6])) {
                this.person.passportid = this.person.passportid.substring(0, 6);
            }
            if (this.person.passportid.length > 7 && !this.isLetter(this.person.passportid[7])) {
                this.person.passportid = this.person.passportid.substring(0, 7);
            }
            if (this.person.passportid.length > 8 && !this.isNum(this.person.passportid[8])) {
                this.person.passportid = this.person.passportid.substring(0, 8);
            }
            if (this.person.passportid.length > 9 && !this.isNum(this.person.passportid[9])) {
                this.person.passportid = this.person.passportid.substring(0, 9);
            }
            if (this.person.passportid.length > 10 && !this.isNum(this.person.passportid[10])) {
                this.person.passportid = this.person.passportid.substring(0, 10);
            }
            if (this.person.passportid.length > 11 && !this.isLetter(this.person.passportid[11])) {
                this.person.passportid = this.person.passportid.substring(0, 11);
            }
            if (this.person.passportid.length > 12 && !this.isLetter(this.person.passportid[12])) {
                this.person.passportid = this.person.passportid.substring(0, 12);
            }
            if (this.person.passportid.length > 13 && !this.isNum(this.person.passportid[13])) {
                this.person.passportid = this.person.passportid.substring(0, 13);
            }
            if (this.person.passportid.length > 14) {
                this.person.passportid = this.person.passportid.substring(0, 14);
            }
            this.person.passportid = this.person.passportid.toUpperCase();
            this.indchanged = false;

        }

        if (this.person != null && this.passchanged) {
            this.person.passportnum = this.cyrToLat(this.person.passportnum);
            var letterNumber = /^[0-9a-zA-Z]+$/;
            if (this.person.passportnum.length > 0) {

                let lastletter = this.person.passportnum[this.person.passportnum.length - 1];
                if (!lastletter.match(letterNumber)) {
                    this.person.passportnum = this.person.passportnum.substring(0, this.person.passportnum.length - 1);
                }
            }//person.passportid
            if (this.person.passportnum.length > 0 && !this.isLetter(this.person.passportnum[0])) {
                this.person.passportnum = this.person.passportnum.substring(0, 0);
            }
            if (this.person.passportnum.length > 1 && !this.isLetter(this.person.passportnum[1])) {
                this.person.passportnum = this.person.passportnum.substring(0, 1);
            }
            if (this.person.passportnum.length > 2 && !this.isNum(this.person.passportnum[2])) {
                this.person.passportnum = this.person.passportnum.substring(0, 2);
            }
            if (this.person.passportnum.length > 3 && !this.isNum(this.person.passportnum[3])) {
                this.person.passportnum = this.person.passportnum.substring(0, 3);
            }
            if (this.person.passportnum.length > 4 && !this.isNum(this.person.passportnum[4])) {
                this.person.passportnum = this.person.passportnum.substring(0, 4);
            }
            if (this.person.passportnum.length > 5 && !this.isNum(this.person.passportnum[5])) {
                this.person.passportnum = this.person.passportnum.substring(0, 5);
            }
            if (this.person.passportnum.length > 6 && !this.isNum(this.person.passportnum[6])) {
                this.person.passportnum = this.person.passportnum.substring(0, 6);
            }
            if (this.person.passportnum.length > 7 && !this.isNum(this.person.passportnum[7])) {
                this.person.passportnum = this.person.passportnum.substring(0, 7);
            }
            if (this.person.passportnum.length > 8 && !this.isNum(this.person.passportnum[8])) {
                this.person.passportnum = this.person.passportnum.substring(0, 8);
            }
            if (this.person.passportnum.length > 9) {
                this.person.passportnum = this.person.passportnum.substring(0, 9);
            }
            this.person.passportnum = this.person.passportnum.toUpperCase();
            this.passchanged = false;

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

    isLetterCyrillic(str: string) {
        return str.length === 1 && str.match(/[а-яА-Я]/i);
    }

    identchange(event: Event) {
        this.indchanged = true;
    }

    passchange(event: Event) {
        this.passchanged = true;
    }

    @Watch('visible')
    onVisibleChange(value: boolean, oldValue: boolean) {
        if (value) {
            
            if (this.$store.state.eldSelectedperson > 0) {
               
                fetch('api/Person/Specialphotospreview' + this.$store.state.eldSelectedperson, { credentials: 'include' })
                    .then(response => {
                        return response.json() as Promise<Personphoto[]>;
                    })
                    .then(personphotos => {
                        if (personphotos != null) {

                            personphotos.forEach(p => p.photo64 = p.photo64header + "," + p.photo64);
                            this.photosPreview = personphotos;
                           // alert(JSON.stringify(personphotos));
                            this.selectPerson(this.$store.state.eldSelectedperson);
                            this.$store.commit("setEldSelectedperson", 0);
                        }
                    })
                
            }
        }
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

    get regions(): Region[] {
        return this.$store.state.regions;
    }

    get areas(): Area[] {
        return this.$store.state.areas;
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

    get citytypes(): Citytype[] {
        return this.$store.state.citytypes;
    }

    get streettypes(): Streettype[] {
        return this.$store.state.streettypes;
    }

    get areaothers(): Areaother[] {
        return this.$store.state.areaothers;
    }

    get externalorderwhotypes(): Externalorderwhotype[] {
        return this.$store.state.externalorderwhotypes;
    }

    get persondecreetypes(): Persondecreetype[] {
        return this.$store.state.persondecreetypes;
    }

    get citysubstates(): Citysubstate[] {
        return this.$store.state.citysubstates;
    }

    get educationtypes(): Educationtype[] {
        return this.$store.state.educationtypes;
    }

    get educationadditionaltypes(): Educationadditionaltype[] {
        return this.$store.state.educationadditionaltypes;
    }

    get educationstages(): Educationstage[] {
        return this.$store.state.educationstages;
    }

    get educationpositiontypes(): Educationpositiontype[] {
        return this.$store.state.educationpositiontypes;
    }
    
    get educationlevels(): Educationlevel[] {
        return this.$store.state.educationlevels;
    }

    get educationdocuments(): Educationdocument[] {
        return this.$store.state.educationdocuments;
    }

    

    onBirthCountryChange() {
        this.person.livecountry = this.person.birthcountry;
        this.person.registercountry = this.person.birthcountry;
    }

    onBirthStateChange() {
        let birthregiontype: Region = this.regions.find(r => r.name == this.person.birthstate);
        if (birthregiontype != null) {
            this.person.registerstatenum = birthregiontype.id;
            this.onRegisterStateNumChange();
        }

    }

    onBirthSubstateChange() {
        let birthareatype: Area = this.areas.find(r => r.name == this.person.birthsubstate);
        if (birthareatype != null) {
            this.person.registersubstatenum = birthareatype.id;
            this.onRegisterSubstateNumChange();
        }

    }

    onBirthCitysubstateChange() {
        this.person.registercitysubstate = this.person.birthcitysubstate;
        // this.person.registersubstatenum = birthareatype.id;
        this.onRegisterCitysubstateChange();
    }

    onBirthCitytypeChange() {
        this.person.registercitytype = this.person.birthcitytype;
        this.onRegisterCitytypeChange();

    }

    onBirthCityChange() {
        this.person.registercity = this.person.birthcity;
        this.onRegisterCityChange();

    }

    onRegisterCountryChange() {
        this.person.livecountry = this.person.registercountry;
    }

    onRegisterStateChange() {
        this.person.livestate = this.person.registerstate;
    }

    onRegisterStateNumChange() {
        this.person.livestatenum = this.person.registerstatenum;
        this.person.livesubstatenum = null;
        this.person.registersubstatenum = null;
        this.person.registercity = "";
        this.person.livecity = ""
    }

    onLiveStateNumChange() {
        this.person.livesubstatenum = null;
        this.person.livecity = ""
    }

    onRegisterCitysubstateChange() {
        this.person.livecitysubstate = this.person.registercitysubstate;
    }

    onRegisterSubstateChange() {
        this.person.livesubstate = this.person.registersubstate;
    }

    onRegisterSubstateNumChange() {
        this.person.livesubstatenum = this.person.registersubstatenum;
    }

    onRegisterCitytypeChange() {
        this.person.livecitytype = this.person.registercitytype;
    }

    onRegisterCityChange() {
        this.person.livecity = this.person.registercity;
    }

    onRegisterStreettypeChange() {
        this.person.livestreettype = this.person.registerstreettype;
    }

    onRegisterStreetChange() {
        this.person.livestreet = this.person.registerstreet;
    }

    onRegisterHouseChange() {
        this.person.livehouse = this.person.registerhouse;
    }

    onRegisterHousingChange() {
        this.person.livehousing = this.person.registerhousing;
    }

    onRegisterFlatChange() {
        this.person.liveflat = this.person.registerflat;
    }

    onPersonrelativeBirthCountryChange() {
        //this.person.livecountry = this.person.birthcountry;
        //this.person.registercountry = this.person.birthcountry;
    }

    onPersonrelativeBirthStateChange() {
        //let birthregiontype: Region = this.regions.find(r => r.name == this.person.birthstate);
        //if (birthregiontype != null) {
        //    this.person.registerstatenum = birthregiontype.id;
        //    this.onRegisterStateNumChange();
        //}

    }

    onPersonrelativeBirthSubstateChange() {
        //let birthareatype: Area = this.areas.find(r => r.name == this.person.birthsubstate);
        //if (birthareatype != null) {
        //    this.person.registersubstatenum = birthareatype.id;
        //    this.onRegisterSubstateNumChange();
        //}

    }

    onPersonrelativeBirthCitytypeChange() {
        //this.person.registercitytype = this.person.birthcitytype;
        //this.onRegisterCitytypeChange();

    }

    onPersonrelativeBirthCityChange() {
        //this.person.registercity = this.person.birthcity;
        //this.onRegisterCityChange();

    }

    onEnterSearchPersons() {
        alert('hop');
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


    hasPhotopreview(personid: number): boolean {
        if (this.photosPreview == null || this.photosPreview.length == 0) {
            return false;
        }
        if (this.photosPreview.find(p => p.person == personid)) {
            return true;
        }
        return false;
    }
   
    getCourse(semestr: number): number{
        if(semestr > 0 && semestr < 3){
            return 1;
        }else if(semestr < 5){
            return 2;
        }else if(semestr < 7){
            return 3;
        }else if(semestr < 9){
            return 4;
        }
        return 0;
    }

    getPhotopreview(personid: number): Personphoto {
        return this.photosPreview.find(p => p.person == personid);
    }

    hasPhotomain(): boolean {
        if (this.person == null || this.photos == null || this.person.photo == 0 || this.photos.length == 0) {
            return false;
        }
        if (this.photos.find(p => p.id == this.person.photo)) {
            return true;
        }
        return false;
    }

    getPhotomain(): Personphoto {
        return this.photos.find(p => p.id == this.person.photo);
        //return this.photosPreview.find(p => p.person == personid).photo64 + "?" + this.randomInteger().toString();
    }

    randomInteger(): number {
        let rand = 2 - 0.5 + Math.random() * (99999 - 2 + 1);
        return Math.round(rand);
    }

    hasSearchResults(): boolean {
        if (this.personssearch != null && this.personssearch.length > 0) {
            return true;
        } else {
            return false;
        }
    }

    selectPerson(id: number, autoupdate: boolean = false) {
        fetch('api/Person/Single' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person>;
            })
            .then(person => {
                if (person != null) {
                    this.prepareToImport(person);

                    //let birthadditionaldisplay: boolean = false;
                    //if (autoupdate && this.person != null) {
                    //    birthadditionaldisplay = this.person.birthadditionaldisplay;
                    //}
                    //this.person = person;
                    //if (autoupdate && birthadditionaldisplay) {
                    //    this.person.birthadditionaldisplay = birthadditionaldisplay;
                    //}

                    this.person = person;
                    this.getPhotos();
                    this.photoToCreate = new Personphoto();

                    this.personssearch = [];
                    //alert(JSON.stringify(person));
                }
            })
    }

    closeSearch() {
        this.personssearch = [];
    }

    rerenderSearch() {
        if (this.personssearch != null && this.personssearch.length > 0) {
            this.searchPersons(this.lastSearchFio);
        }
    }

    saveChanges(person: Person, autoupdate: boolean = false) {
        this.prepareToExport(person);
        fetch('/api/Person', {
            method: 'post',
            body: JSON.stringify(person),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                if (!autoupdate) {
                    (<any>Vue).notify(response);
                }
                
            })
            .then(x => {
                // При автоматическом обновлении мы не подгружаем (не обновляем) информацию автоматически
                if (!autoupdate) {
                    this.rerenderSearch();
                    this.selectPerson(this.person.id, autoupdate);
                }
                
            });
    }

    createPerson() {
        let searchText: string = this.fiosearch;
        if (searchText == null || searchText.trim().length == 0) {
            searchText = "Фамилия Имя Отчество";
        }
        fetch('api/Person/Create' + searchText, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(id => {
                if (id > 0) {
                    fetch('api/Person/Single' + id, { credentials: 'include' })
                        .then(response => {
                            return response.json() as Promise<Person>;
                        })
                        .then(person => {
                            if (person != null) {
                                this.person = person;
                                this.personssearch = [];
                                //alert(JSON.stringify(person));
                            }
                        })
                }
            })
    }

    createMenuToggle() {
        this.createmenuvisible = !this.createmenuvisible;
        this.creategender = "Мужской";
    }

    createPersonNew() {
        let text: string = this.createstring;
        if (text == null || text.trim().length == 0) {
            return;
        }
        text = text + this.creategender;
        fetch('api/Person/Create' + text, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(id => {
                if (id > 0) {
                    fetch('api/Person/Single' + id, { credentials: 'include' })
                        .then(response => {
                            return response.json() as Promise<Person>;
                        })
                        .then(person => {
                            if (person != null) {
                                this.person = person;
                                this.personssearch = [];
                                this.createstring = "";
                                this.createmenuvisible = false;
                                //alert(JSON.stringify(person));
                            }
                        })
                }
            })
    }
    
    
    removePerson() {
        if (this.person == null) {
            return; // Нет открытых ЭЛД.
        }
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        fetch('api/Person/Remove' + this.person.id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(resultstatus => {
                if (resultstatus > 0) {
                    this.person = null;
                }
            })
    }

    setMenuid(id: number) {
        this.menuid = id;
    }

    handleRemove(file, fileList) {

    }

    handlePreview(file) {
    }

    handleExceed(files, fileList) {
    }

    beforeRemove(file, fileList) {
    }

    uploadPhoto(res, file) {
        this.lastUploadedPhoto = file;
        //alert('upload!');
        //URL.createObjectURL
    }

    uploadPfl(res, file) {
        this.lastUploadedPfl = file;
        //alert('upload!');
        //URL.createObjectURL
    }

    beforeFileUpload(file) {
        return true;
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

    savePhotos() {
        if (this.lastUploadedPhoto == null) {
            return;
        }
        let personphoto: Personphoto = new Personphoto();
        personphoto.name = this.photoName;
        personphoto.description = this.photoDescription;
        //personphoto.photo = this.lastUploadedPhoto.url;
        personphoto.date = new Date();
        personphoto.person = this.person.id;

        fetch(this.lastUploadedPhoto.url).then(r => { return r.blob() })
            .then((r) => {
                var reader = new FileReader();
                reader.readAsDataURL(r);
                var eldParent: any = this;
                reader.onloadend = function (parent: any = this) {
                    //personphoto.photo64 = reader.result; 
                    personphoto.photo64 = reader.result.toString(); 
                    fetch('/api/Personphoto', {
                        method: 'post',
                        body: JSON.stringify(personphoto),
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
                            eldParent.getPhotos();
                            eldParent.photoToCreate = new Personphoto();
                            eldParent.resetMediaInput();
                        });
                }
                //personphoto.photo = r;
               // alert(JSON.stringify(r.size));
            })
    }

    savePfl() {
        if (this.lastUploadedPfl == null) {
            return;
        }
        let personpfl: Personpfl = new Personpfl();
        personpfl.date = new Date();
        personpfl.person = this.person.id;

        fetch(this.lastUploadedPfl.url).then(r => { return r.blob() })
            .then((r) => {
                var reader = new FileReader();
                reader.readAsDataURL(r);
                var eldParent: any = this;
                reader.onloadend = function (parent: any = this) {
                    //personphoto.photo64 = reader.result; 
                    personpfl.person64 = reader.result.toString();
                    fetch('/api/Personpfl', {
                        method: 'post',
                        body: JSON.stringify(personpfl),
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
                            //eldParent.getPhotos();
                            //eldParent.photoToCreate = new Personphoto();
                            eldParent.resetMediaInput();
                        });
                }
                //personphoto.photo = r;
                // alert(JSON.stringify(r.size));
            })
    }

    deletePhoto(id: number) {
        fetch('api/Person/Deletephoto' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person>;
            })
            .then(person => {
                if (person != null) {
                }
                this.getPhotos();
            })
    }

    makePhotoMain(id: number) {
        fetch('api/Person/Mainphoto' + id, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(success => {
                if (success != null && success == 1) {
                    (<any>Vue).notify("S:Фотография обновлена");
                }
                this.getPhotos();
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            })
    }

    resetMediaInput() {
        this.photoName = "Фотография";
        this.photoDescription = "";
        this.filelist = new Array();
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
    }

    isValidDate(d): boolean {
        return d instanceof Date && !isNaN(d.getTime());
        
    }

    printDate(date: Date): string {
        if (date == null) {
            return "";
        }
        return this.beautifyDate(this.toDateInputValue(date));
    }

    /**
     * После загрузки с базы данных нам нужно Даты превратить в string эквивалент
     */
    prepareToImport(person: Person): void {
        //alert(person.birthcitytype);
        //person.birthdateString = this.toDateInputValue(new Date());
        person.birthdateString = this.toDateInputValue(person.birthdate);
        person.passportdatestartString = this.toDateInputValue(person.passportdatestart);
        person.passportdateendString = this.toDateInputValue(person.passportdateend);
        person.age = this.getAge(person.birthdateString).toString();
        if (person.registerstatenum == 0) {
            person.registerstatenum = null;
        }
        if (person.registersubstatenum == 0) {
            person.registersubstatenum = null;
        }
        if (person.livestatenum == 0) {
            person.livestatenum = null;
        }
        if (person.livesubstatenum == 0) {
            person.livesubstatenum = null;
        }

        if (person.birthcitysubstate == 0) {
            person.birthcitysubstate = null;
        }

        if (person.registercitysubstate == 0) {
            person.registercitysubstate = null;
        }

        if (person.livecitysubstate == 0) {
            person.livecitysubstate = null;
        }

        if (person.birthadditional.length > 0) {
            person.birthadditionaldisplay = true;
        } else {
            person.birthadditionaldisplay = false;
        }

        if (person.personranks != null) {
            person.personranks.forEach(p => {
                p.decreedateString = this.toDateInputValue(p.decreedate);
                p.datestartString = this.toDateInputValue(p.datestart);

                if (p.rank != 0 && p.rankstring.length == 0) {
                    p.rankstring = this.rankById(p.rank);
                }
                if (p.datestartString.length == 0 && p.decreedateString.length > 0 ) {
                    p.datestartString = p.decreedateString;
                }
            })
        }
        

        if (person.personcontracts != null) {
            person.personcontracts.forEach(p => {
                p.datestartString = this.toDateInputValue(p.datestart);
                p.dateendString = this.toDateInputValue(p.dateend);
                p.payBool = this.numberToBool(p.pay);
                p.orderdateString = this.toDateInputValue(p.orderdate);
                p.personadditionalagreements.forEach(a => {
                    a.datestartString = this.toDateInputValue(a.datestart);
                })
            })
        }

        if (person.personrelatives != null) {
            person.personrelatives.forEach(p => {
                p.birthdayString = this.toDateInputValue(p.birthday);
                p.nodataBool = this.numberToBool(p.nodata);
                p.deathBool = this.numberToBool(p.death);
                p.deathnodataBool = this.numberToBool(p.deathnodata);
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
                p.orderdateString = this.toDateInputValue(p.orderdate);
                p.interruptorderdateString = this.toDateInputValue(p.interruptorderdate);
                p.academicvacationBool = this.numberToBool(p.academicvacation);
                p.maternityvacationBool = this.numberToBool(p.maternityvacation);

                p.educationtypeblocks.forEach(etb => {
                    etb.educationperiods.forEach(ep => {
                        ep.serviceBool = this.numberToBool(ep.service);
                        ep.startString = this.toDateInputValue(ep.start);
                        ep.endString = this.toDateInputValue(ep.end);
                        ep.orderdateString = this.toDateInputValue(ep.orderdate);
                    })
                })
                p.academicvacations.forEach(av => {
                    av.startString = this.toDateInputValue(av.start);
                    av.endString = this.toDateInputValue(av.end);
                    av.orderdateString = this.toDateInputValue(av.orderdate);
                })
                p.educationmaternities.forEach(em => {
                    em.startString = this.toDateInputValue(em.start);
                    em.endString = this.toDateInputValue(em.end);
                    em.orderdateString = this.toDateInputValue(em.orderdate);

                })
            })
        }

        if (person.personjobs != null) {
            person.personjobs.forEach(p => {
                //alert(p.orderdate == null);
                p.startString = this.toDateInputValue(p.start);
                p.endString = this.toDateInputValue(p.end);
                p.orderdateString = this.toDateInputValue(p.orderdate);
                p.actualBool = this.numberToBool(p.actual);
                p.manualBool = this.numberToBool(p.manual);
                p.mchsBool = this.numberToBool(p.mchs);
                p.statecivilBool = this.numberToBool(p.statecivil);
                p.statecivilstartString = this.toDateInputValue(p.statecivilstart);
                p.statecivilendString = this.toDateInputValue(p.statecivilend);
                p.startcustomString = this.toDateInputValue(p.startcustom);
                p.privelegeBool = this.numberToBool(p.privelege);


                p.personjobpriveleges.forEach(jp => {
                    jp.documentdateString = this.toDateInputValue(jp.documentdate);
                    jp.personjobprivelegeperiods.forEach(jpp => {
                        jpp.startString = this.toDateInputValue(jpp.start);
                        jpp.endString = this.toDateInputValue(jpp.end);
                    })
                });
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
                p.privelegeBool = this.numberToBool(p.privelege);
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
                p.privelegeBool = this.numberToBool(p.privelege);
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

    prepareToExport(person: Person): void {
        person.birthdate = this.prepareDateToExport(person.birthdateString); 
        person.passportdatestart = this.prepareDateToExport(person.passportdatestartString);
        person.passportdateend = this.prepareDateToExport(person.passportdateendString);

        if (person.registerstatenum == null) {
            person.registerstatenum = 0;
        }
        if (person.registersubstatenum == null) {
            person.registersubstatenum = 0;
        }
        if (person.livestatenum == null) {
            person.livestatenum = 0;
        }
        if (person.livesubstatenum == null) {
            person.livesubstatenum = 0;
        }
        if (person.registercitysubstate == null) {
            person.registercitysubstate = 0;
        }
        if (person.birthcitysubstate == null) {
            person.birthcitysubstate = 0;
        }
        if (person.livecitysubstate == null) {
            person.livecitysubstate = 0;
        }


        if (person.personranks != null) {
            person.personranks.forEach(p => {
                p.decreedate = this.prepareDateToExport(p.decreedateString);
                p.datestart = this.prepareDateToExportNullable(p.datestartString);

                if (p.rankstring.length == 0) {
                    p.rank = 0;
                } else if (this.rankByName(p.rankstring) != null && this.rankByName(p.rankstring) != 0) {
                    p.rank = this.rankByName(p.rankstring);
                } else {
                    p.rank = 0;
                }
            })
        }

        if (person.personcontracts != null) {
            person.personcontracts.forEach(p => {
                p.datestart = this.prepareDateToExport(p.datestartString);
                p.dateend = this.prepareDateToExport(p.dateendString);
                p.pay = this.boolToNumb(p.payBool);
                p.orderdate = this.prepareDateToExportNullable(p.orderdateString);
                p.vacationdays = this.prepareNumToExport(p.vacationdays);
                p.personadditionalagreements.forEach(a => {
                    a.datestart = this.prepareDateToExportNullable(a.datestartString);
                    a.duration = this.prepareNumToExport(a.duration);
                })
                //alert(p.vacationdays);
            })
        }

        if (person.personrelatives != null) {
            person.personrelatives.forEach(p => {
                p.birthday = new Date(p.birthdayString);
                p.nodata = this.boolToNumb(p.nodataBool);
                p.death = this.boolToNumb(p.deathBool);
                p.deathnodata = this.boolToNumb(p.deathnodataBool);
            })
        }

        if (person.personattestations != null) {
            person.personattestations.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personvacations != null) {
            person.personvacations.forEach(p => {
                p.date = new Date(p.dateString);
                p.compensation = this.boolToNumb(p.compensationBool);
                p.compensationdate = new Date(p.compensationdateString);
                p.cancel = this.boolToNumb(p.cancelBool);
                p.allowstart = new Date(p.allowstartString);
                p.allowend = new Date(p.allowendString);
                p.canceldate = new Date(p.canceldateString);
                p.canceldateend = new Date(p.canceldateendString);
                p.cancelcontinue = this.boolToNumb(p.cancelcontinueBool);
            })
        }

        if (person.personeducations != null) {
            person.personeducations.forEach(p => {

                p.start = this.prepareDateToExportNullable(p.startString);
                p.end = this.prepareDateToExportNullable(p.endString);
                p.cadet = this.boolToNumb(p.cadetBool);
                p.interrupted = this.boolToNumb(p.interruptedBool);
                p.orderdate = this.prepareDateToExportNullable(p.orderdateString);
                p.interruptorderdate = this.prepareDateToExportNullable(p.interruptorderdateString);
                p.academicvacation = this.boolToNumb(p.academicvacationBool);
                p.maternityvacation = this.boolToNumb(p.maternityvacationBool);

                p.educationtypeblocks.forEach(etb => {
                    etb.educationperiods.forEach(ep => {
                        ep.start = this.prepareDateToExportNullable(ep.startString);
                        ep.end = this.prepareDateToExportNullable(ep.endString);
                        ep.service = this.boolToNumb(ep.serviceBool);
                        ep.orderdate = this.prepareDateToExportNullable(ep.orderdateString);
                    })
                })
                p.academicvacations.forEach(av => {
                    av.start = this.prepareDateToExportNullable(av.startString);
                    av.end = this.prepareDateToExportNullable(av.endString);
                    av.orderdate = this.prepareDateToExportNullable(av.orderdateString);
                })
                p.educationmaternities.forEach(em => {
                    em.start = this.prepareDateToExportNullable(em.startString);
                    em.end = this.prepareDateToExportNullable(em.endString);
                    em.orderdate = this.prepareDateToExportNullable(em.orderdateString);
                })
            })
        }

        if (person.personjobs != null) {
            person.personjobs.forEach(p => {
                p.start = this.prepareDateToExportNullable(p.startString);
                p.end = this.prepareDateToExportNullable(p.endString);
                p.orderdate = this.prepareDateToExportNullable(p.orderdateString);
                p.actual = this.boolToNumb(p.actualBool);
                p.manual = this.boolToNumb(p.manualBool);
                p.mchs = this.boolToNumb(p.mchsBool);
                p.statecivil = this.boolToNumb(p.statecivilBool);
                p.statecivilstart = this.prepareDateToExportNullable(p.statecivilstartString);
                p.statecivilend = this.prepareDateToExportNullable(p.statecivilendString);
                p.startcustom = this.prepareDateToExportNullable(p.startcustomString);
                p.privelege = this.boolToNumb(p.privelegeBool);

                p.personjobpriveleges.forEach(jp => {
                    jp.documentdate = this.prepareDateToExportNullable(jp.documentdateString);
                    jp.personjobprivelegeperiods.forEach(jpp => {
                        jpp.start = this.prepareDateToExportNullable(jpp.startString);
                        jpp.end = this.prepareDateToExportNullable(jpp.endString);
                    })
                });

               
            })
        }

        if (person.personpenalties != null) {
            person.personpenalties.forEach(p => {
                p.orderdate = new Date(p.orderdateString);
            })
        }

        if (person.personworktrips != null) {
            person.personworktrips.forEach(p => {
                p.tripdate = new Date(p.tripdateString);
                p.privelege = this.boolToNumb(p.privelegeBool);
            })
        }

        if (person.personelections != null) {
            person.personelections.forEach(p => {
                p.electiondate = new Date(p.electiondateString);
                p.electiondateend = new Date(p.electiondateendString);
            })
        }

        if (person.personsciences != null) {
            person.personsciences.forEach(p => {
                p.sciencedate = new Date(p.sciencedateString);
            })
        }

        if (person.personrewards != null) {
            person.personrewards.forEach(p => {
                p.rewarddate = new Date(p.rewarddateString);
            })
        }

        if (person.personills != null) {
            person.personills.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
                p.illtype = this.boolToNumb(p.illtypeBool);
                p.privelege = this.boolToNumb(p.privelegeBool);
            })
        }

        if (person.personphysicals != null) {
            person.personphysicals.forEach(p => {
                p.physicaldate = new Date(p.physicaldateString);
            })
        }

        if (person.persondrivers != null) {
            person.persondrivers.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
            })
        }

        if (person.personpermissions != null) {
            person.personpermissions.forEach(p => {
                p.datestart = new Date(p.datestartString);
                p.dateend = new Date(p.dateendString);
            })
        }

        if (person.persondispanserizations != null) {
            person.persondispanserizations.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personvvks != null) {
            person.personvvks.forEach(p => {
                p.date = new Date(p.dateString);
            })
        }

        if (person.personjobpriveleges != null) {
            person.personjobpriveleges.forEach(p => {
                p.start = new Date(p.startString);
                p.end = new Date(p.endString);
                p.proofdate = new Date(p.proofdateString);
                p.documentdate = new Date(p.documentdateString);

            })
        }
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

    isAppointButtonActive(person: Person): boolean {
        if ((this.$store.state.eldPosition > 0 || this.$store.state.eldStructure > 0)) {
            return true;
        }
        return false;
    }

    appointPerson(person: Person) {
        let ids: string = "";
        ids += person.id.toString();
        ids += "&";
        ids += this.$store.state.eldPosition;
        fetch('api/Person/Appoint' + ids, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<number>;
            })
            .then(response => {
                this.$store.commit("setEldPosition", 0);
                this.$store.commit("setEldStructure", 0);
            })

        // this.$store.state.parentStructures 
    }

    get positiontypes(): Positiontype[] {
        return this.$store.state.positiontypes;
    }

    addPersonrank() {
        //this.prepareToExport(person);
        fetch('/api/Personrank', {
            method: 'post',
            body: JSON.stringify(<Personrank>{
                person: this.person.id,
                rank: this.prepareNumToExport(this.personrankType),
                rankstring: this.personrankRankstring,
                decreedate: this.prepareDateToExport(this.personrankDate),
                decreenumber: this.personrankNumber,
                decreenumbertype: this.personrankDecreenumbertype,
                decreeid: this.prepareNumToExport(this.personrankDecreeid),
                structure: this.personrankStructure,
                structureid: this.prepareNumToExport(this.personrankStructureid),
                datestart: this.prepareDateToExportNullable(this.personrankDatestart),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    

    rankById(id: number): string {
        let rank: Rank = this.ranks.find(s => s.id == id);
        if (rank != null) {
            return rank.name;
        } else {
            return "";
        }
    }

    rankByName(name: string): number {
        let rank: Rank = this.ranks.find(s => s.name == name);
        if (rank != null) {
            return rank.id;
        } else {
            return 0;
        }
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

    updatePersonrank(person: Person, personrank: Personrank) {
        this.prepareToExport(person);
        fetch('/api/Personrank', {
            method: 'post',
            body: JSON.stringify(personrank),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonrank(person: Person, personrank: Personrank) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personrank.id = -personrank.id;
        fetch('/api/Personrank', {
            method: 'post',
            body: JSON.stringify(personrank),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersonrankUpdateName(): string {
        if (this.personrankMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonrankUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonrankButton(person: Person) {
        this.personrankMenuvisible = true;
        this.personrankMenuelement = null;
        this.personrankType = null;
        this.personrankRankstring = "";
        this.personrankDate = "";
        this.personrankNumber = "";
        this.personrankStructure = "";
        this.personrankStructureid = 0;
        this.personrankDatestart = "";
        this.personrankDecreenumbertype = "";
        this.personrankDecreeid = 0;
    }

    updatePersonrankButton(person: Person, personrank: Personrank) {

        this.personrankMenuelement = personrank;

        this.personrankType = this.personrankMenuelement.rank;
        this.personrankDate = this.personrankMenuelement.decreedateString;
        this.personrankNumber = this.personrankMenuelement.decreenumber;
        this.personrankRankstring = this.personrankMenuelement.rankstring;
        this.personrankStructure = this.personrankMenuelement.structure;
        this.personrankStructureid = this.personrankMenuelement.structureid;
        this.personrankDatestart = this.personrankMenuelement.datestartString;
        this.personrankDecreenumbertype = this.personrankMenuelement.decreenumbertype;
        this.personrankDecreeid = this.personrankMenuelement.decreeid;


        this.personrankMenuvisible = true;
    }

    completePersonrankButton(person: Person) {
        if (this.personrankMenuelement == null) {
            this.addPersonrank();
        } else {

            this.personrankMenuelement.rank = this.prepareNumToExport(this.personrankType);
            this.personrankMenuelement.rankstring = this.personrankRankstring;
            this.personrankMenuelement.decreedateString = this.personrankDate;
            this.personrankMenuelement.decreenumber = this.personrankNumber;
            this.personrankMenuelement.structure = this.personrankStructure;
            this.personrankMenuelement.datestartString = this.personrankDatestart;
            this.personrankMenuelement.decreenumbertype = this.personrankDecreenumbertype;
            this.personrankMenuelement.structureid = this.prepareNumToExport(this.personrankStructureid);
            this.personrankMenuelement.decreeid = this.prepareNumToExport(this.personrankDecreeid);

            this.updatePersonrank(person, this.personrankMenuelement);
        }

        this.personrankMenuvisible = false;
    }

    addPersonadditionalagreement(personcontract: Personcontract) {
        fetch('/api/Personadditionalagreement', {
            method: 'post',
            body: JSON.stringify(<Personadditionalagreement>{
                person: this.person.id,
                contract: personcontract.id,
                datestart: this.prepareDateToExport(personcontract.datestartString),
                duration: this.prepareNumToExport(28),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonadditionalagreement(person: Person, personadditionalagreement: Personadditionalagreement) {
        this.prepareToExport(person);
        fetch('/api/Personadditionalagreement', {
            method: 'post',
            body: JSON.stringify(personadditionalagreement),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonadditionalagreement(person: Person, personadditionalagreement: Personadditionalagreement) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personadditionalagreement.id = -personadditionalagreement.id;
        fetch('/api/Personadditionalagreement', {
            method: 'post',
            body: JSON.stringify(personadditionalagreement),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    addPersoncontract() {
        //this.prepareToExport(person);
        fetch('/api/Personcontract', {
            method: 'post',
            body: JSON.stringify(<Personcontract>{
                person: this.person.id,
                datestart: this.prepareDateToExport(this.personcontractDatestart),
                dateend: this.prepareDateToExport(this.personcontractDateend),
                pay: this.boolToNumb(this.personcontractPay),
                ordernumber: this.personcontractOrdernumber,
                ordernumbertype: this.personcontractOrdernumbertype,
                orderdate: this.prepareDateToExportNullable(this.personcontractOrderdate),
                orderwho: this.personcontractOrderwho,
                orderwhoid: this.prepareNumToExport(this.personcontractOrderwhoid),
                orderid: this.prepareNumToExport(this.personcontractOrderid),
                sourceoffinancing: this.prepareNumToExport(this.personcontractSourceoffinancing),
                payvalue: this.prepareNumToExport(this.personcontractPayvalue),
                stateserviceyears: this.prepareNumToExport(this.personcontractStateserviceyears),
                stateservicemonths: this.prepareNumToExport(this.personcontractStateservicemonths),
                stateservicedays: this.prepareNumToExport(this.personcontractStateservicedays),
                vacationdays: this.prepareNumToExport(this.personcontractVacationdays),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersoncontract(person: Person, personcontract: Personcontract) {
        this.prepareToExport(person);
        fetch('/api/Personcontract', {
            method: 'post',
            body: JSON.stringify(personcontract),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersoncontract(person: Person, personcontract: Personcontract) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personcontract.id = -personcontract.id;
        fetch('/api/Personcontract', {
            method: 'post',
            body: JSON.stringify(personcontract),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersoncontractUpdateName(): string {
        if (this.personcontractMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersoncontractUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersoncontractButton(person: Person) {
        this.personcontractMenuvisible = true;
        this.personcontractMenuelement = null;
    }

    updatePersoncontractButton(person: Person, personcontract: Personcontract) {

        this.personcontractMenuelement = personcontract;

        this.personcontractDatestart = this.personcontractMenuelement.datestartString;
        this.personcontractDateend = this.personcontractMenuelement.dateendString;
        this.personcontractPay = this.personcontractMenuelement.payBool;
        this.personcontractOrdernumber = this.personcontractMenuelement.ordernumber;
        this.personcontractOrdernumbertype = this.personcontractMenuelement.ordernumbertype;
        this.personcontractOrderdate = this.personcontractMenuelement.orderdateString;
        this.personcontractOrderwho = this.personcontractMenuelement.orderwho;
        this.personcontractOrderwhoid = this.personcontractMenuelement.orderwhoid;
        this.personcontractOrderid = this.personcontractMenuelement.orderid;
        this.personcontractSourceoffinancing = this.personcontractMenuelement.sourceoffinancing;
        this.personcontractPayvalue = this.personcontractMenuelement.payvalue;
        this.personcontractStateserviceyears = this.personcontractMenuelement.stateserviceyears;
        this.personcontractStateservicemonths = this.personcontractMenuelement.stateservicemonths;
        this.personcontractStateservicedays = this.personcontractMenuelement.stateservicedays;
        this.personcontractVacationdays = this.personcontractMenuelement.vacationdays;

        this.personcontractMenuvisible = true;
    }

    completePersoncontractButton(person: Person) {
        if (this.personcontractMenuelement == null) {
            this.addPersoncontract();
        } else {
            this.personcontractMenuelement.datestartString = this.personcontractDatestart;
            this.personcontractMenuelement.dateendString = this.personcontractDateend;
            this.personcontractMenuelement.payBool = this.personcontractPay;
            this.personcontractMenuelement.ordernumber = this.personcontractOrdernumber;
            this.personcontractMenuelement.ordernumbertype = this.personcontractOrdernumbertype;
            this.personcontractMenuelement.orderdateString = this.personcontractOrderdate;
            this.personcontractMenuelement.orderwho = this.personcontractOrderwho;
            this.personcontractMenuelement.orderwhoid = this.prepareNumToExport(this.personcontractOrderwhoid);
            this.personcontractMenuelement.orderid = this.prepareNumToExport(this.personcontractOrderid);
            this.personcontractMenuelement.sourceoffinancing = this.prepareNumToExport(this.personcontractSourceoffinancing);
            this.personcontractMenuelement.payvalue = this.prepareNumToExport(this.personcontractPayvalue);
            this.personcontractMenuelement.stateserviceyears = this.prepareNumToExport(this.personcontractStateserviceyears);
            this.personcontractMenuelement.stateservicemonths = this.prepareNumToExport(this.personcontractStateservicemonths);
            this.personcontractMenuelement.stateservicedays = this.prepareNumToExport(this.personcontractStateservicedays);
            this.personcontractMenuelement.vacationdays = this.prepareNumToExport(this.personcontractVacationdays);

            //this.prepareNumToExport

            this.updatePersoncontract(person, this.personcontractMenuelement);
        }

        this.personcontractMenuvisible = false;
    }

    completePersoncontractButtonDirect(person: Person, personcontract: Personcontract) {
        this.updatePersoncontract(person, personcontract);
    }

    getRelativetype(relativetype: number): string {
        if (relativetype == null || relativetype == 0) {
            return "";
        }
        let rtype: Relativetype = this.relativetypes.find(r => r.id == relativetype);
        if (rtype != null) {
            return rtype.name;
        } else {
            return "";
        }
        
    }

    addPersonrelative() {
        //this.prepareToExport(person);
        fetch('/api/Personrelative', {
            method: 'post',
            body: JSON.stringify(<Personrelative>{
                person: this.person.id,
                relativetype: this.personrelativeType,
                fio: this.personrelativeFio,
                fioother: this.personrelativeFioother,
                birthday: this.prepareDateToExport(this.personrelativeBirthdayString),
                birthplace: this.personrelativeBirthplace,
                jobplace: this.personrelativeJobplace,
                jobposition: this.personrelativeJobposition,
                livecountry: this.personrelativeLivecountry,
                livestate: this.personrelativeLivestate,
                livesubstate: this.personrelativeLivesubstate,
                livecitysubstate: this.prepareNumToExport(this.personrelativeLivecitysubstate),
                livecitytype: this.personrelativeLivecitytype,
                livecity: this.personrelativeLivecity,
                livestreettype: this.personrelativeLivestreettype,
                livestreet: this.personrelativeLivestreet,
                livehouse: this.personrelativeLivehouse,
                livehousing: this.personrelativeLivehousing,
                liveflat: this.personrelativeLiveflat,
                birthcountry: this.personrelativeBirthcountry,
                birthstate: this.personrelativeBirthstate,
                birthsubstate: this.personrelativeBirthsubstate,
                birthcitysubstate: this.prepareNumToExport(this.personrelativeBirthcitysubstate),
                birthcitytype: this.personrelativeBirthcitytype,
                birthcity: this.personrelativeBirthcity,
                birthadditional: this.personrelativeBirthadditional,
                nodata: this.boolToNumb(this.personrelativeNodata),
                death: this.boolToNumb(this.personrelativeDeath),
                deathnodata: this.boolToNumb(this.personrelativeDeathnodata),
                deathcountry: this.personrelativeDeathcountry,
                deathstate: this.personrelativeDeathstate,
                deathsubstate: this.personrelativeDeathsubstate,
                deathcitysubstate: this.prepareNumToExport(this.personrelativeDeathcitysubstate),
                deathcitytype: this.personrelativeDeathcitytype,
                deathcity: this.personrelativeDeathcity,
                deathadditional: this.personrelativeDeathadditional,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    getPersonrelativeUpdateName(): string {
        if (this.personrelativeMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonrelativeUpdateButtonName():string {
        return SAVE_LABEL;
    }

    addPersonrelativeButton(person: Person) {
        this.personrelativeMenuvisible = true;
        this.personrelativeMenuelement = null;

        this.personrelativeType = null;
        this.personrelativeBirthdayString = "";
        this.personrelativeBirthplace = "";
        this.personrelativeFio = "";
        this.personrelativeFioother = "";
        this.personrelativeJobplace = "";
        this.personrelativeJobposition = "";
        this.personrelativeLivecity = "";
        this.personrelativeLivecountry = "Республика Беларусь";
        this.personrelativeLiveflat = "";
        this.personrelativeLivehouse = "";
        this.personrelativeLivehousing = "";
        this.personrelativeLivestate = "";
        this.personrelativeLivestreet = "";
        this.personrelativeLivesubstate = "";
        this.personrelativeLivecitysubstate = null;
        this.personrelativeBirthadditional = "";
        this.personrelativeBirthcountry = "Республика Беларусь";
        this.personrelativeBirthstreet = "";
        this.personrelativeBirthstreettype = "";
        this.personrelativeBirthcity = "";
        this.personrelativeBirthcitytype = "";
        this.personrelativeBirthstate = "";
        this.personrelativeBirthsubstate = "";
        this.personrelativeBirthcitysubstate = null;
        this.personrelativeNodata = false;
        this.personrelativeDeath = false;
        this.personrelativeDeathnodata = false;
        this.personrelativeDeathadditional = "";
        this.personrelativeDeathadditionaldisplay = false;
        this.personrelativeDeathcountry = "Республика Беларусь";
        this.personrelativeDeathcity = "";
        this.personrelativeDeathcitytype = "";
        this.personrelativeDeathstate = "";
        this.personrelativeDeathsubstate = "";
        this.personrelativeDeathcitysubstate = null;
        this.personrelativeDeathstreet = "";
        this.personrelativeDeathstreettype = "";
    }

    updatePersonrelativeButton(person: Person, personrelative: Personrelative) {

        this.personrelativeMenuelement = personrelative;

        this.personrelativeType = this.personrelativeMenuelement.relativetype;
        this.personrelativeFio = this.personrelativeMenuelement.fio;
        this.personrelativeFioother = this.personrelativeMenuelement.fioother;
        this.personrelativeBirthdayString = this.personrelativeMenuelement.birthdayString;
        this.personrelativeBirthplace = this.personrelativeMenuelement.birthplace;
        this.personrelativeJobplace = this.personrelativeMenuelement.jobplace;
        this.personrelativeJobposition = this.personrelativeMenuelement.jobposition;
        this.personrelativeLivecountry = this.personrelativeMenuelement.livecountry;
        this.personrelativeLivestate = this.personrelativeMenuelement.livestate;
        this.personrelativeLivesubstate = this.personrelativeMenuelement.livesubstate;
        this.personrelativeLivecitysubstate = this.personrelativeMenuelement.livecitysubstate;
        this.personrelativeLivecitytype = this.personrelativeMenuelement.livecitytype;
        this.personrelativeLivecity = this.personrelativeMenuelement.livecity;
        this.personrelativeLivestreettype = this.personrelativeMenuelement.livestreettype;
        this.personrelativeLivestreet = this.personrelativeMenuelement.livestreet;
        this.personrelativeLivehouse = this.personrelativeMenuelement.livehouse;
        this.personrelativeLivehousing = this.personrelativeMenuelement.livehousing;
        this.personrelativeLiveflat = this.personrelativeMenuelement.liveflat;
        this.personrelativeBirthcountry = this.personrelativeMenuelement.birthcountry;
        this.personrelativeBirthstate = this.personrelativeMenuelement.birthstate;
        this.personrelativeBirthsubstate = this.personrelativeMenuelement.birthsubstate;
        this.personrelativeBirthcitysubstate = this.personrelativeMenuelement.birthcitysubstate;
        this.personrelativeBirthcitytype = this.personrelativeMenuelement.birthcitytype;
        this.personrelativeBirthcity = this.personrelativeMenuelement.birthcity;
        this.personrelativeBirthadditional = this.personrelativeMenuelement.birthadditional;
        this.personrelativeNodata = this.personrelativeMenuelement.nodataBool;
        this.personrelativeDeath = this.personrelativeMenuelement.deathBool;
        this.personrelativeDeathnodata = this.personrelativeMenuelement.deathnodataBool;
        this.personrelativeDeathcountry = this.personrelativeMenuelement.deathcountry;
        this.personrelativeDeathstate = this.personrelativeMenuelement.deathstate;
        this.personrelativeDeathsubstate = this.personrelativeMenuelement.deathsubstate;
        this.personrelativeDeathcitysubstate = this.personrelativeMenuelement.deathcitysubstate;
        this.personrelativeDeathcitytype = this.personrelativeMenuelement.deathcitytype;
        this.personrelativeDeathcity = this.personrelativeMenuelement.deathcity;
        this.personrelativeDeathadditional = this.personrelativeMenuelement.deathadditional;

        this.personrelativeMenuvisible = true;
    }

    completePersonrelativeButton(person: Person) {
        if (this.personrelativeMenuelement == null) {
            this.addPersonrelative();
        } else {
            this.personrelativeMenuelement.relativetype = this.personrelativeType;
            this.personrelativeMenuelement.fio = this.personrelativeFio;
            this.personrelativeMenuelement.fioother = this.personrelativeFioother;
            this.personrelativeMenuelement.birthdayString = this.personrelativeBirthdayString;
            this.personrelativeMenuelement.birthplace = this.personrelativeBirthplace;
            this.personrelativeMenuelement.jobplace = this.personrelativeJobplace;
            this.personrelativeMenuelement.jobposition = this.personrelativeJobposition;
            this.personrelativeMenuelement.livecountry = this.personrelativeLivecountry;
            this.personrelativeMenuelement.livestate = this.personrelativeLivestate;
            this.personrelativeMenuelement.livesubstate = this.personrelativeLivesubstate;
            this.personrelativeMenuelement.livecitysubstate = this.prepareNumToExport(this.personrelativeLivecitysubstate);
            this.personrelativeMenuelement.livecitytype = this.personrelativeLivecitytype;
            this.personrelativeMenuelement.livecity = this.personrelativeLivecity;
            this.personrelativeMenuelement.livestreettype = this.personrelativeLivestreettype; 
            this.personrelativeMenuelement.livestreet = this.personrelativeLivestreet;
            this.personrelativeMenuelement.livehouse = this.personrelativeLivehouse;
            this.personrelativeMenuelement.livehousing = this.personrelativeLivehousing;
            this.personrelativeMenuelement.liveflat = this.personrelativeLiveflat;
            this.personrelativeMenuelement.birthcountry = this.personrelativeBirthcountry;
            this.personrelativeMenuelement.birthstate = this.personrelativeBirthstate;
            this.personrelativeMenuelement.birthsubstate = this.personrelativeBirthsubstate;
            this.personrelativeMenuelement.birthcitysubstate = this.prepareNumToExport(this.personrelativeBirthcitysubstate);
            this.personrelativeMenuelement.birthcitytype = this.personrelativeBirthcitytype;
            this.personrelativeMenuelement.birthcity = this.personrelativeBirthcity;
            this.personrelativeMenuelement.birthadditional = this.personrelativeBirthadditional;
            this.personrelativeMenuelement.nodataBool = this.personrelativeNodata;
            this.personrelativeMenuelement.deathBool = this.personrelativeDeath;
            this.personrelativeMenuelement.deathnodataBool = this.personrelativeDeathnodata;
            this.personrelativeMenuelement.deathcountry = this.personrelativeDeathcountry;
            this.personrelativeMenuelement.deathstate = this.personrelativeDeathstate;
            this.personrelativeMenuelement.deathsubstate = this.personrelativeDeathsubstate;
            this.personrelativeMenuelement.deathcitysubstate = this.prepareNumToExport(this.personrelativeDeathcitysubstate);
            this.personrelativeMenuelement.deathcitytype = this.personrelativeDeathcitytype;
            this.personrelativeMenuelement.deathcity = this.personrelativeDeathcity;
            this.personrelativeMenuelement.deathadditional = this.personrelativeDeathadditional;


            this.updatePersonrelative(person, this.personrelativeMenuelement);
        }

        this.personrelativeMenuvisible = false;
    }

    updatePersonrelative(person: Person, personrelative: Personrelative) {
        this.prepareToExport(person);
        fetch('/api/Personrelative', {
            method: 'post',
            body: JSON.stringify(personrelative),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonrelative(person: Person, personrelative: Personrelative) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personrelative.id = -personrelative.id;
        fetch('/api/Personrelative', {
            method: 'post',
            body: JSON.stringify(personrelative),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    addPersonattestation() {
        //this.prepareToExport(person);
        fetch('/api/Personattestation', {
            method: 'post',
            body: JSON.stringify(<Personattestation>{
                person: this.person.id,
                attestationtype: this.personattestationType,
                date: new Date(this.personattestationDate),
                recomendation: this.personattestationRecomendation,
                result: this.personattestationResult,
                //relativetype: this.personrelativeType,

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonattestation(person: Person, personattestation: Personattestation) {
        this.prepareToExport(person);
        fetch('/api/Personattestation', {
            method: 'post',
            body: JSON.stringify(personattestation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonattestation(person: Person, personattestation: Personattestation) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personattestation.id = -personattestation.id;
        fetch('/api/Personattestation', {
            method: 'post',
            body: JSON.stringify(personattestation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getAttestation(attestationid: number): string {
        if (attestationid == null || attestationid == 0) {
            return "";
        }
        let attestation: Attestationtype = this.attestations.find(t => t.id == attestationid);
        if (attestation != null) {
            return attestation.name;
        } else {
            return "";
        }

    }

    getPersonattestationUpdateName(): string {
        if (this.personattestationMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonattestationUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonattestationButton(person: Person) {
        this.personattestationMenuvisible = true;
        this.personattestationMenuelement = null;
    }

    updatePersonattestationButton(person: Person, personattestation: Personattestation) {

        this.personattestationMenuelement = personattestation;

        this.personattestationType = this.personattestationMenuelement.attestationtype;
        this.personattestationDate = this.personattestationMenuelement.dateString;
        this.personattestationRecomendation = this.personattestationMenuelement.recomendation;
        this.personattestationResult = this.personattestationMenuelement.result;


        this.personattestationMenuvisible = true;
    }

    completePersonattestationButton(person: Person) {
        if (this.personattestationMenuelement == null) {
            this.addPersonattestation();
        } else {

            this.personattestationMenuelement.attestationtype = this.prepareNumToExport(this.personattestationType);
            this.personattestationMenuelement.dateString = this.personattestationDate;
            this.personattestationMenuelement.recomendation = this.personattestationRecomendation;
            this.personattestationMenuelement.result = this.personattestationResult;


            this.updatePersonattestation(person, this.personattestationMenuelement);
        }

        this.personattestationMenuvisible = false;
    }

    addPersonvacation() {
        //this.prepareToExport(person);
        fetch('/api/Personvacation', {
            method: 'post',
            body: JSON.stringify(<Personvacation>{
                person: this.person.id,
                vacationtype: this.prepareNumToExport(this.personvacationType),
                vacationmilitary: this.prepareNumToExport(this.personvacationMilitary),
                cancel: this.boolToNumb(this.personvacationCancel),
                canceldate: this.prepareDateToExport(this.personvacationCanceldate), 
                compensation: this.boolToNumb(this.personvacationCompensation),
                compensationdate: this.prepareDateToExport(this.personvacationCompensationdate),
                compensationnumber: this.personvacationCompensationnumber,
                compensationdays: this.personvacationCompensationdays,
                date: this.prepareDateToExport(this.personvacationDate),
                duration: this.personvacationDuration,
                trip: this.personvacationTrip,
                allowstart: this.prepareDateToExport(this.personvacationAllowstart),
                allowend: this.prepareDateToExport(this.personvacationAllowend),
                holidays: this.prepareNumToExport(this.personvacationHolidays),
                canceldateend: this.prepareDateToExport(this.personvacationCanceldateend),
                cancelcontinue: this.boolToNumb(this.personvacationCancelcontinue),
                
                //relativetype: this.personrelativeType,

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonvacation(person: Person, personvacation: Personvacation) {
        this.prepareToExport(person);
        fetch('/api/Personvacation', {
            method: 'post',
            body: JSON.stringify(personvacation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonvacation(person: Person, personvacation: Personvacation) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personvacation.id = -personvacation.id;
        fetch('/api/Personvacation', {
            method: 'post',
            body: JSON.stringify(personvacation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }



    getVacationmilitary(vacationmilitary: number): string {
        if (vacationmilitary == null || vacationmilitary == 0) {
            return "";
        }
        let element: Vacationmilitary = this.vacationmilitaries.find(t => t.id == vacationmilitary);
        if (element != null) {
            return element.name;
        } else {
            return "";
        }
    }

    getVacationtype(vacationtype: number): string {
        if (vacationtype == null || vacationtype == 0) {
            return "";
        }
        let element: Vacationtype = this.vacations.find(t => t.id == vacationtype);
        if (element != null) {
            return element.name;
        } else {
            return "";
        }
    }

    getVacationtypeObject(vacationtype: number): Vacationtype {
        if (vacationtype == null || vacationtype == 0) {
            return null;
        }
        let element: Vacationtype = this.vacations.find(t => t.id == vacationtype);
        if (element != null) {
            return element;
        } else {
            return null;
        }
    }

    getPersonvacationUpdateName(): string {
        if (this.personvacationMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonvacationUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonvacationButton(person: Person) {
        this.personvacationMenuvisible = true;
        this.personvacationMenuelement = null;

        this.personvacationAllowstart = this.toDateInputValue(this.currentYearStart());
        this.personvacationAllowend = this.toDateInputValue(this.currentYearEnd());
        if (person != null && person.actualRank == null && person.jobperiodcurrent != null) {
            this.personvacationAllowstart = this.toDateInputValue(person.jobperiodcurrent.start);
            this.personvacationAllowend = this.toDateInputValue(person.jobperiodcurrent.end);
        }
        this.personvacationMilitary = 1; // потом будет подстраиваться под актуальную должность.
        this.personvacationType = null;
        this.personvacationCancel = false;
        this.personvacationCanceldate = "";
        this.personvacationCompensation = false;
        this.personvacationCompensationdate = "";
        this.personvacationCompensationnumber = "";
        this.personvacationCompensationdays = 0;
        this.personvacationDuration = 0;
        this.personvacationTrip = 0;
        this.personvacationHolidays = 0;// Далее здесь будет подсчет по праздникам.
        this.personvacationCancelcontinue = false;
        this.personvacationCanceldateend = "";
        this.personvacationDate = this.toDateInputValue(new Date());
        this.personvacationDateend = "";
    }

    updatePersonvacationButton(person: Person, personvacation: Personvacation) {

        this.personvacationMenuelement = personvacation;

        //this.personvacationType = this.personvacationMenuelement.relativetype;
        this.personvacationType = this.personvacationMenuelement.vacationtype;
        this.personvacationMilitary = this.personvacationMenuelement.vacationmilitary;
        this.personvacationCancel = this.personvacationMenuelement.cancelBool;
        this.personvacationCanceldate = this.personvacationMenuelement.canceldateString;
        this.personvacationCompensation = this.personvacationMenuelement.compensationBool;
        this.personvacationCompensationdate = this.personvacationMenuelement.compensationdateString;
        this.personvacationCompensationnumber = this.personvacationMenuelement.compensationnumber;
        this.personvacationCompensationdays = this.personvacationMenuelement.compensationdays;
        this.personvacationDate = this.personvacationMenuelement.dateString;
        this.personvacationDuration = this.personvacationMenuelement.duration;
        this.personvacationTrip = this.personvacationMenuelement.trip;
        this.personvacationAllowstart = this.personvacationMenuelement.allowstartString;
        this.personvacationAllowend = this.personvacationMenuelement.allowendString;
        this.personvacationHolidays = this.personvacationMenuelement.holidays;
        this.personvacationCancelcontinue = this.personvacationMenuelement.cancelcontinueBool;
        this.personvacationCanceldateend = this.personvacationMenuelement.canceldateendString;

        this.personvacationMenuvisible = true;
    }

    completePersonvacationButton(person: Person) {
        if (this.personvacationMenuelement == null) {
            this.addPersonvacation();
        } else {

            this.personvacationMenuelement.vacationtype = this.prepareNumToExport(this.personvacationType);
            this.personvacationMenuelement.vacationmilitary = this.prepareNumToExport(this.personvacationMilitary);
            this.personvacationMenuelement.cancelBool = this.personvacationCancel;
            this.personvacationMenuelement.canceldateString = this.personvacationCanceldate;
            this.personvacationMenuelement.compensationBool = this.personvacationCompensation;
            this.personvacationMenuelement.compensationdateString = this.personvacationCompensationdate;
            this.personvacationMenuelement.compensationnumber = this.personvacationCompensationnumber;
            this.personvacationMenuelement.compensationdays = this.prepareNumToExport(this.personvacationCompensationdays);
            this.personvacationMenuelement.dateString = this.personvacationDate;
            this.personvacationMenuelement.duration = this.prepareNumToExport(this.personvacationDuration);
            this.personvacationMenuelement.trip = this.prepareNumToExport(this.personvacationTrip);
            this.personvacationMenuelement.allowstartString = this.personvacationAllowstart;
            this.personvacationMenuelement.allowendString = this.personvacationAllowend;
            this.personvacationMenuelement.holidays = this.personvacationHolidays;
            this.personvacationMenuelement.cancelcontinueBool = this.personvacationCancelcontinue;
            this.personvacationMenuelement.canceldateendString = this.personvacationCanceldateend;

            this.updatePersonvacation(person, this.personvacationMenuelement);
        }

        this.personvacationMenuvisible = false;
    }

    //getVacationend(person: Personvacation): Date {
    //    let dateend: Date = new Date(person.date);
    //    dateend.setDate(dateend.getDate() + person.duration + person.trip + person.holidays);
    //    return dateend;
    //}

    getVacationend(person: Personvacation): Date {
        if (person.date == null) {
            return null;
        }
        let dateend: Date = new Date(person.date);
        let durationlocal: number = person.duration;
        durationlocal = Number.parseInt(durationlocal.toString());
        let triplocal: number = person.trip;
        triplocal = Number.parseInt(triplocal.toString());
        let holidaylocal: number = person.holidays;
        if (holidaylocal == null) {
            holidaylocal = 0;
        }

        // Означает, что военный, а у них нет бонуса к отпуску если праздничный день приходится на рабочий
        if (this.person != null && this.person.actualRank != null) {
            holidaylocal = 0;
        }

        holidaylocal = Number.parseInt(holidaylocal.toString())
        let addition: number = durationlocal + triplocal + holidaylocal - 1;
        if (addition < 0) {
            addition = 0;
        }
        dateend.setDate(dateend.getDate() + addition);
        //alert(dateend);
        //this.countHolidays();
        return dateend;
    }

    getVacationendString(person: Personvacation): string {
        //let dateend: Date = new Date(person.date);
        //dateend.setDate(dateend.getDate() + person.duration + person.trip + person.holidays);
        //return this.toDateInputValue(dateend);

        return this.toDateInputValue(this.getVacationend(person));
    }

    

    getVacationendOnCreation(): string {
        if (this.personvacationDate == null) {
            return null;
        }
        let dateend: Date = new Date(this.personvacationDate);
        let durationlocal: number = this.personvacationDuration;
        durationlocal = Number.parseInt(durationlocal.toString());
        let triplocal: number = this.personvacationTrip;
        triplocal = Number.parseInt(triplocal.toString());
        let holidaylocal: number = this.personvacationHolidays;
        if (holidaylocal == null) {
            holidaylocal = 0;
        }

        // Означает, что военный, а у них нет бонуса к отпуску если праздничный день приходится на рабочий
        if (this.person != null && this.person.actualRank != null) {
            holidaylocal = 0;
        }
        
        holidaylocal = Number.parseInt(holidaylocal.toString())
        let addition: number = durationlocal + triplocal + holidaylocal - 1;
        if (addition < 0) {
            addition = 0;
        }
        dateend.setDate(dateend.getDate() + addition);
        //alert(dateend);
        //this.countHolidays();
        return this.toDateInputValue(dateend);
    }

    countHolidays(): number {
        let holidays: number = 0;

        
        let actualHolidays: Holiday[] = new Array();
        if (this.holidays == null) {
            return 0;
        }
        this.holidays.forEach(h => {
            let fullYearH = new Date(h.date).getFullYear();
            let fullYearCur = this.currentYearStart().getFullYear();
            if (h.permanent) {
                let hdayMod: Holiday = new Holiday();
                hdayMod.date = new Date(h.date);
                hdayMod.date.setFullYear(this.currentYearStart().getFullYear()); 
                actualHolidays.push(hdayMod);
            } else if (h.date != null && fullYearH == fullYearCur) {
                actualHolidays.push(h);
            }
        })
        if (this.personvacationDate != null && this.personvacationDuration != null) {
            actualHolidays.forEach(h => {
                var diff = moment(h.date).diff(this.personvacationDate, 'days');
                if (diff >= 0 && diff <= this.personvacationDuration) {
                    holidays++;
                }
            })
        }
        
        //alert(actualHolidays[0].date);
        this.personvacationHolidays = holidays;
        return holidays;
    }

    vacationDateendChange() {
        if (this.personvacationDateend == null || this.personvacationDateend.length == 0 || this.personvacationDate == null || this.personvacationDate.length == 0) {
            return;
        }
        let diff = moment(this.personvacationDateend).diff(this.personvacationDate, 'days');
        this.personvacationDuration = diff;
    }

    addPersonlanguage() {
        //this.prepareToExport(person);
        fetch('/api/Personlanguage', {
            method: 'post',
            body: JSON.stringify(<Personlanguage>{
                person: this.person.id,
                languagetype: this.prepareNumToExport(this.personlanguageLanguagetype),
                languageskill: this.prepareNumToExport(this.personlanguageLanguageskill),

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonlanguage(person: Person, personlanguage: Personlanguage) {
        this.prepareToExport(person);
        fetch('/api/Personlanguage', {
            method: 'post',
            body: JSON.stringify(personlanguage),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonlanguage(person: Person, personlanguage: Personlanguage) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personlanguage.id = -personlanguage.id;
        fetch('/api/Personlanguage', {
            method: 'post',
            body: JSON.stringify(personlanguage),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getLanguagetype(languagetypeid: number): string {
        if (languagetypeid == null || languagetypeid == 0) {
            return "";
        }
        let languagetype: Languagetype = this.languagetypes.find(t => t.id == languagetypeid);
        if (languagetype != null) {
            return languagetype.name;
        } else {
            return "";
        }

    }

    getLanguageskill(languageskillid: number): string {
        if (languageskillid == null || languageskillid == 0) {
            return "";
        }
        let languageskill: Languagetype = this.languageskills.find(t => t.id == languageskillid);
        if (languageskill != null) {
            return languageskill.name;
        } else {
            return "";
        }

    }

    getPersonlanguageUpdateName(): string {
        if (this.personlanguageMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonlanguageUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonlanguageButton(person: Person) {
        this.personlanguageMenuvisible = true;
        this.personlanguageMenuelement = null;
    }

    updatePersonlanguageButton(person: Person, personlanguage: Personlanguage) {

        this.personlanguageMenuelement = personlanguage;

        this.personlanguageLanguageskill = this.personlanguageMenuelement.languageskill;
        this.personlanguageLanguagetype = this.personlanguageMenuelement.languagetype;


        this.personlanguageMenuvisible = true;
    }

    completePersonlanguageButton(person: Person) {
        if (this.personlanguageMenuelement == null) {
            this.addPersonlanguage();
        } else {

            this.personlanguageMenuelement.languageskill = this.prepareNumToExport(this.personlanguageLanguageskill);
            this.personlanguageMenuelement.languagetype = this.prepareNumToExport(this.personlanguageLanguagetype);

            this.updatePersonlanguage(person, this.personlanguageMenuelement);
        }

        this.personlanguageMenuvisible = false;
    }

    addPersonjob() {
        //this.prepareToExport(person);
        fetch('/api/Personjob', {
            method: 'post',
            body: JSON.stringify(<Personjob>{
                person: this.person.id,
                jobtype: this.prepareNumToExport(this.personjobJobtype),
                start: this.prepareDateToExportNullable(this.personjobStart),
                end: this.prepareDateToExportNullable(this.personjobEnd),
                jobplace: this.personjobJobplace,
                jobposition: this.personjobJobposition,
                jobpositionplace: this.personjobJobpositionplace,
                servicetype: this.prepareNumToExport(this.personjobServicetype),
                servicetypestr: this.personjobServicetypestr,
                servicefeature: this.prepareNumToExport(this.personjobServicefeature),
                servicecoef: this.prepareNumToExport(this.personjobServicecoef),
                serviceorder: this.personjobServiceorder,
                serviceplace: this.personjobServiceplace,
                ordernumber: this.personjobOrdernumber,
                ordernumbertype: this.personjobOrdernumbertype,
                orderdate: this.prepareDateToExportNullable(this.personjobOrderdate),
                actual: this.boolToNumb(this.personjobActual),
                manual: this.boolToNumb(this.personjobManual),
                mchs: this.boolToNumb(this.personjobMchs),
                vacationdays: this.prepareNumToExport(this.personjobVacationdays),
                position: this.prepareNumToExport(this.personjobPosition),
                statecivil: this.boolToNumb(this.personjobStatecivil),
                statecivilstart: this.prepareDateToExportNullable(this.personjobStatecivilstart),
                statecivilend: this.prepareDateToExportNullable(this.personjobStatecivilend),
                startcustom: this.prepareDateToExportNullable(this.personjobStartcustom),
                privelege: this.boolToNumb(this.personjobPrivelegebool),
                personjobpriveleges: this.personjobPersonjobpriveleges,

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonjob(person: Person, personjob: Personjob) {
        this.prepareToExport(person);
        fetch('/api/Personjob', {
            method: 'post',
            body: JSON.stringify(personjob),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }
 
    deletePersonjob(person: Person, personjob: Personjob) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personjob.id = -personjob.id;
        fetch('/api/Personjob', {
            method: 'post',
            body: JSON.stringify(personjob),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getJobtype(jobtype: number): string {
        if (jobtype == null || jobtype == 0) {
            return "";
        }
        let jtype: Jobtype = this.jobtypes.find(t => t.id == jobtype);
        if (jtype != null) {
            return jtype.name;
        } else {
            return "";
        }

    }

    getServicetype(servicetype: number): string {
        if (servicetype == null || servicetype == 0) {
            return "";
        }
        let stype: Servicetype = this.servicetypes.find(t => t.id == servicetype);
        if (stype != null) {
            return stype.name;
        } else {
            return "";
        }
    }

    getServicefeature(servicefeature: number): string {
        if (servicefeature == null || servicefeature == 0) {
            return "";
        }
        let element: Servicefeature = this.servicefeatures.find(t => t.id == servicefeature);
        if (element != null) {
            return element.name;
        } else {
            return "";
        }
    }

    getServicecoef(servicecoef: number): string {
        if (servicecoef == null || servicecoef == 0) {
            return "";
        }
        let element: Servicecoef = this.servicecoefs.find(t => t.id == servicecoef);
        if (element != null) {
            return element.name;
        } else {
            return "";
        }
    }

    getPersonjobUpdateName(): string {
        if (this.personjobMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonjobUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonjobButton(person: Person) {
        this.personjobMenuvisible = true;
        this.personjobMenuelement = null;

        this.personjobJobtype = null;
        this.personjobStart = this.toDateInputValue(new Date());
        this.personjobEnd = this.toDateInputValue(new Date());
        this.personjobJobplace = "";
        this.personjobJobposition = "";
        this.personjobJobpositionplace = "";
        this.personjobServicetype = null;
        this.personjobServicetypestr = "органах и подразделениях по чрезвычайным ситуациям Республики Беларусь";
        this.personjobServicefeature = null;
        this.personjobServicecoef = null;
        this.personjobServiceorder = "";
        this.personjobServiceplace = "";
        this.personjobOrdernumber = "";
        this.personjobOrdernumbertype = "";
        this.personjobOrderdate = "";
        this.personjobOrderwho = "";
        this.personjobActual = true;
        this.personjobManual = false;
        this.personjobMchs = true;
        this.personjobVacationdays = 0;
        this.personjobPosition = 0;
        this.personjobStatecivil = false;
        this.personjobStatecivilstart = null;
        this.personjobStatecivilend = null;
        this.personjobStartcustom = "";
        this.personjobPrivelegebool = false;
        this.personjobPersonjobpriveleges = new Array();
    }

    updatePersonjobButton(person: Person, personjob: Personjob) {

        this.personjobMenuelement = personjob;
        //alert(this.personjobMenuelement.actualBool);
        this.personjobJobtype = this.personjobMenuelement.jobtype;
        this.personjobStart = this.personjobMenuelement.startString;
        this.personjobEnd = this.personjobMenuelement.endString;
        this.personjobJobplace = this.personjobMenuelement.jobplace;
        this.personjobJobposition = this.personjobMenuelement.jobposition;
        this.personjobJobpositionplace = this.personjobMenuelement.jobpositionplace;
        this.personjobServicetype = this.personjobMenuelement.servicetype;
        this.personjobServicetypestr = this.personjobMenuelement.servicetypestr;
        this.personjobServicefeature = this.personjobMenuelement.servicefeature;
        this.personjobServicecoef = this.personjobMenuelement.servicecoef;
        this.personjobServiceorder = this.personjobMenuelement.serviceorder;
        this.personjobServiceplace = this.personjobMenuelement.serviceplace;
        this.personjobOrdernumber = this.personjobMenuelement.ordernumber;
        this.personjobOrdernumbertype = this.personjobMenuelement.ordernumbertype;
        this.personjobOrderdate = this.personjobMenuelement.orderdateString;
        this.personjobOrderwho = this.personjobMenuelement.orderwho;
        this.personjobActual = this.personjobMenuelement.actualBool;
        this.personjobManual = this.personjobMenuelement.manualBool;
        this.personjobMchs = this.personjobMenuelement.mchsBool;
        this.personjobVacationdays = this.personjobMenuelement.vacationdays;
        this.personjobPosition = this.personjobMenuelement.position;
        this.personjobStartcustom = this.personjobMenuelement.startcustomString;
        this.personjobPrivelegebool = this.personjobMenuelement.privelegeBool;
        this.personjobPersonjobpriveleges = this.personjobMenuelement.personjobpriveleges;
        this.personjobStatecivil = this.personjobMenuelement.statecivilBool;
        this.personjobStatecivilstart = this.personjobMenuelement.statecivilstartString;
        this.personjobStatecivilend = this.personjobMenuelement.statecivilendString;

        this.personjobMenuvisible = true;
    }

    completePersonjobButton(person: Person) {
        if (this.personjobMenuelement == null) {
            this.addPersonjob();
        } else {
            //alert(this.personjobMenuelement.position);
            this.personjobMenuelement.jobtype = this.prepareNumToExport(this.personjobJobtype);
            this.personjobMenuelement.startString = this.personjobStart;
            this.personjobMenuelement.endString = this.personjobEnd;
            this.personjobMenuelement.jobplace = this.personjobJobplace;
            this.personjobMenuelement.jobposition = this.personjobJobposition;
            this.personjobMenuelement.jobpositionplace = this.personjobJobpositionplace;
            this.personjobMenuelement.servicetype = this.prepareNumToExport(this.personjobServicetype);
            this.personjobMenuelement.servicetypestr = this.personjobServicetypestr;
            this.personjobMenuelement.servicefeature = this.prepareNumToExport(this.personjobServicefeature);
            this.personjobMenuelement.servicecoef = this.prepareNumToExport(this.personjobServicecoef);
            this.personjobMenuelement.serviceorder = this.personjobServiceorder;
            this.personjobMenuelement.serviceplace = this.personjobServiceplace;
            this.personjobMenuelement.ordernumber = this.personjobOrdernumber;
            this.personjobMenuelement.ordernumbertype = this.personjobOrdernumbertype;
            this.personjobMenuelement.orderdateString = this.personjobOrderdate;
            this.personjobMenuelement.orderwho = this.personjobOrderwho;
            this.personjobMenuelement.actualBool = this.personjobActual;
            this.personjobMenuelement.manualBool = this.personjobManual;
            this.personjobMenuelement.mchsBool = this.personjobMchs;
            this.personjobMenuelement.vacationdays = this.personjobVacationdays;
            this.personjobMenuelement.position = this.prepareNumToExport(this.personjobPosition);
            this.personjobMenuelement.startcustomString = this.personjobStartcustom
            this.personjobMenuelement.privelegeBool = this.personjobPrivelegebool;
            this.personjobMenuelement.personjobpriveleges = this.personjobPersonjobpriveleges;
            this.personjobMenuelement.statecivilBool = this.personjobStatecivil;
            this.personjobMenuelement.statecivilstartString = this.personjobStatecivilstart;
            this.personjobMenuelement.statecivilendString = this.personjobStatecivilend;

            this.updatePersonjob(person, this.personjobMenuelement);
        }

        this.personjobMenuvisible = false;
    }

    addPersonjobpersonjobprivelege() {
        let personjobprivelege: Personjobprivelege = new Personjobprivelege();
        personjobprivelege.personjobprivelegeperiods = new Array();
        let personjobprivelegeperiod: Personjobprivelegeperiod = new Personjobprivelegeperiod();
        personjobprivelegeperiod.startString = this.personjobStart;
        personjobprivelege.personjobprivelegeperiods.push(personjobprivelegeperiod);

        this.personjobPersonjobpriveleges.push(personjobprivelege);

    }

    removePersonjobpersonjobprivelege(personjobprivelege: Personjobprivelege) {
        this.personjobPersonjobpriveleges = this.personjobPersonjobpriveleges.filter(e => e != personjobprivelege);
    }

    addPersonjobprivelegeperiod(personjobprivelege: Personjobprivelege) {
        let personjobprivelegeperiod: Personjobprivelegeperiod = new Personjobprivelegeperiod();
        personjobprivelegeperiod.startString = this.personjobStart;
        personjobprivelege.personjobprivelegeperiods.push(personjobprivelegeperiod);
    }


    removePersonjobpersonjobprivelegeperiod(personjobprivelege: Personjobprivelege, personjobprivelegeperiod: Personjobprivelegeperiod) {
        personjobprivelege.personjobprivelegeperiods = personjobprivelege.personjobprivelegeperiods.filter(e => e != personjobprivelegeperiod);
    }

    onPersonjobJobtypeChange() {
        this.personjobServicetypestr = "";
        this.personjobServicetype = 0;
        this.personjobPrivelegebool = false;
        this.personjobServiceplace = "";
        this.personjobPersonjobpriveleges = new Array();
    }

    personjobPersonjobprivelegeCheck() {
        this.forceUpdate();
        if (this.personjobPersonjobpriveleges.length == 0) {
            this.addPersonjobpersonjobprivelege();
        }
    }

    addPersonjobprivelege() {
        //this.prepareToExport(person);
        fetch('/api/Personjobprivelege', {
            method: 'post',
            body: JSON.stringify(<Personjobprivelege>{
                person: this.person.id,
                start: this.prepareDateToExportNullable(this.personjobprivelegeStart),
                end: this.prepareDateToExportNullable(this.personjobprivelegeEnd),
                coef: this.prepareNumToExport(this.personjobprivelegeCoef),
                prooftype: this.prepareNumToExport(this.personjobprivelegeProoftype),
                proofdate: this.prepareDateToExportNullable(this.personjobprivelegeProofdate),
                proofnumber: this.personjobprivelegeProofnumber,
                prooftext: this.personjobprivelegeProoftext,
                documentdate: this.prepareDateToExportNullable(this.personjobprivelegeDocumentdate),
                documentnumber: this.personjobprivelegeDocumentnumber,
                documentorder: this.personjobprivelegeDocumentorder,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonjobprivelege(person: Person, personjobprivelege: Personjobprivelege) {
        this.prepareToExport(person);
        fetch('/api/Personjobprivelege', {
            method: 'post',
            body: JSON.stringify(personjobprivelege),
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
                this.selectPerson(person.id);
            });
    }

    deletePersonjobprivelege(person: Person, personjobprivelege: Personjobprivelege) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personjobprivelege.id = -personjobprivelege.id;
        fetch('/api/Personjobprivelege', {
            method: 'post',
            body: JSON.stringify(personjobprivelege),
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
                this.selectPerson(person.id);
            });
    }

    getProoftype(prooftype: number): string {
        if (prooftype == null || prooftype == 0) {
            return "";
        }
        let jtype: Prooftype = this.prooftypes.find(t => t.id == prooftype);
        if (jtype != null) {
            return jtype.name;
        } else {
            return "";
        }

    }

    getPersonjobprivelegeUpdateName(): string {
        if (this.personjobprivelegeMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonjobprivelegeUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonjobprivelegeButton(person: Person) {
        this.personjobprivelegeMenuvisible = true;
        this.personjobprivelegeMenuelement = null;


        this.personjobprivelegeStart = this.toDateInputValue(this.currentYearStart());
        this.personjobprivelegeEnd = this.toDateInputValue(this.currentYearEnd());

    }

    updatePersonjobprivelegeButton(person: Person, personjobprivelege: Personjobprivelege) {

        this.personjobprivelegeMenuelement = personjobprivelege;

        this.personjobprivelegeStart = this.personjobprivelegeMenuelement.startString;
        this.personjobprivelegeEnd = this.personjobprivelegeMenuelement.endString;
        this.personjobprivelegeCoef = this.personjobprivelegeMenuelement.coef;
        this.personjobprivelegeProoftype = this.personjobprivelegeMenuelement.prooftype;
        this.personjobprivelegeProofdate = this.personjobprivelegeMenuelement.proofdateString;
        this.personjobprivelegeProofnumber = this.personjobprivelegeMenuelement.proofnumber;
        this.personjobprivelegeProoftext = this.personjobprivelegeMenuelement.prooftext;
        this.personjobprivelegeDocumentorder = this.personjobprivelegeMenuelement.documentorder;
        this.personjobprivelegeDocumentdate = this.personjobprivelegeMenuelement.documentdateString;
        this.personjobprivelegeDocumentnumber = this.personjobprivelegeMenuelement.documentnumber;

        this.personjobprivelegeMenuvisible = true;
    }

    completePersonjobprivelegeButton(person: Person) {
        if (this.personjobprivelegeMenuelement == null) {
            this.addPersonjobprivelege();
        } else {
            this.personjobprivelegeMenuelement.startString = this.personjobprivelegeStart;
            this.personjobprivelegeMenuelement.endString = this.personjobprivelegeEnd;
            this.personjobprivelegeMenuelement.coef = this.prepareNumToExport(this.personjobprivelegeCoef);
            this.personjobprivelegeMenuelement.prooftype = this.prepareNumToExport(this.personjobprivelegeProoftype);
            this.personjobprivelegeMenuelement.proofdateString = this.personjobprivelegeProofdate;
            this.personjobprivelegeMenuelement.proofnumber = this.personjobprivelegeProofnumber;
            this.personjobprivelegeMenuelement.prooftext = this.personjobprivelegeProoftext;
            this.personjobprivelegeMenuelement.documentorder = this.personjobprivelegeDocumentorder;
            this.personjobprivelegeMenuelement.documentdateString = this.personjobprivelegeDocumentdate;
            this.personjobprivelegeMenuelement.documentnumber = this.personjobprivelegeDocumentnumber;

            this.updatePersonjobprivelege(person, this.personjobprivelegeMenuelement); 
        }

        this.personjobprivelegeMenuvisible = false;
    }

    addPersonpenalty() {
        //this.prepareToExport(person);
        fetch('/api/Personpenalty', {
            method: 'post',
            body: JSON.stringify(<Personpenalty>{
                person: this.person.id,
                penalty: this.prepareNumToExport(this.personpenaltyPenalty),
                violation: this.personpenaltyViolation,
                orderwho: this.personpenaltyOrderwho,
                ordernumber: this.personpenaltyOrdernumber,
                orderdate: this.prepareDateToExport(this.personpenaltyOrderdate),

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonpenalty(person: Person, personpenalty: Personpenalty) {
        this.prepareToExport(person);
        fetch('/api/Personpenalty', {
            method: 'post',
            body: JSON.stringify(personpenalty),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonpenalty(person: Person, personpenalty: Personpenalty) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personpenalty.id = -personpenalty.id;
        fetch('/api/Personpenalty', {
            method: 'post',
            body: JSON.stringify(personpenalty),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
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

    getArea(areaid: number): string {
        if (areaid == null || areaid == 0) {
            return "";
        }
        let area: Area = this.areas.find(t => t.id == areaid);
        if (area != null) {
            return area.name;
        } else {
            return "";
        }
    }

    getAreaNoOther(areaid: number): string {
        if (areaid == null || areaid == 0) {
            return "";
        }
        let area: Area = this.areas.find(t => t.id == areaid);
        if (area != null && area.other == 0) {
            return area.name;
        } else {
            return "";
        }
    }

    getAreaother(areaid: number): string {
        if (areaid == null || areaid == 0) {
            return "";
        }
        let areaother: Areaother = this.areaothers.find(t => t.id == areaid);
        if (areaother != null) {
            return areaother.name;
        } else {
            return "";
        }
    }

    getExternalorderwhotype(externalorderwhotypeid: number): string {
        if (externalorderwhotypeid == null || externalorderwhotypeid == 0) {
            return "";
        }
        let externalorderwhotype: Externalorderwhotype = this.externalorderwhotypes.find(t => t.id == externalorderwhotypeid);
        if (externalorderwhotype != null) {
            return externalorderwhotype.name;
        } else {
            return "";
        }
    }

    getPersondecreetype(persondecreetypeid: number): string {
        if (persondecreetypeid == null || persondecreetypeid == 0) {
            return "";
        }
        let persondecreetype: Persondecreetype = this.persondecreetypes.find(t => t.id == persondecreetypeid);
        if (persondecreetype != null) {
            return persondecreetype.name;
        } else {
            return "";
        }
    }

    getPersonpenaltyUpdateName(): string {
        if (this.personpenaltyMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonpenaltyUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonpenaltyButton(person: Person) {
        this.personpenaltyMenuvisible = true;
        this.personpenaltyMenuelement = null;
    }

    updatePersonpenaltyButton(person: Person, personpenalty: Personpenalty) {

        this.personpenaltyMenuelement = personpenalty;

        this.personpenaltyPenalty = this.personpenaltyMenuelement.penalty;
        this.personpenaltyViolation = this.personpenaltyMenuelement.violation;
        this.personpenaltyOrderwho = this.personpenaltyMenuelement.orderwho;
        this.personpenaltyOrdernumber = this.personpenaltyMenuelement.ordernumber;
        this.personpenaltyOrderdate = this.personpenaltyMenuelement.orderdateString;

        this.personpenaltyMenuvisible = true;
    }

    completePersonpenaltyButton(person: Person) {
        if (this.personpenaltyMenuelement == null) {
            this.addPersonpenalty();
        } else {


            this.personpenaltyMenuelement.penalty = this.prepareNumToExport(this.personpenaltyPenalty);
            this.personpenaltyMenuelement.violation = this.personpenaltyViolation;
            this.personpenaltyMenuelement.orderwho = this.personpenaltyOrderwho;
            this.personpenaltyMenuelement.ordernumber = this.personpenaltyOrdernumber;
            this.personpenaltyMenuelement.orderdateString = this.personpenaltyOrderdate;


            this.updatePersonpenalty(person, this.personpenaltyMenuelement);
        }

        this.personpenaltyMenuvisible = false;
    }

    addPersonworktrip() {
        //this.prepareToExport(person);
        fetch('/api/Personworktrip', {
            method: 'post',
            body: JSON.stringify(<Personworktrip>{
                person: this.person.id,
                country: this.prepareNumToExport(this.personworktripCountry),
                reason: this.personworktripReason,
                tripdate: this.prepareDateToExport(this.personworktripTripdate),
                days: this.prepareNumToExport(this.personworktripDays),
                privelege: this.boolToNumb(this.personworktripPrivelege),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonworktrip(person: Person, personworktrip: Personworktrip) {
        this.prepareToExport(person);
        fetch('/api/Personworktrip', {
            method: 'post',
            body: JSON.stringify(personworktrip),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonworktrip(person: Person, personworktrip: Personworktrip) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personworktrip.id = -personworktrip.id;
        fetch('/api/Personworktrip', {
            method: 'post',
            body: JSON.stringify(personworktrip),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getCountry(countryid: number): string {
        if (countryid == null || countryid == 0) {
            return "";
        }
        let country: Country = this.countries.find(t => t.id == countryid);
        if (country != null) {
            return country.name;
        } else {
            return "";
        }

    }

    getPersonworktripUpdateName(): string {
        if (this.personworktripMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonworktripUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonworktripButton(person: Person) {
        this.personworktripMenuvisible = true;
        this.personworktripMenuelement = null;
        this.personworktripReason = "";
        this.personworktripTripdate = null;
        this.personworktripCountry = null;
        this.personworktripDays = null;
        this.personworktripPrivelege = false;
    }

    updatePersonworktripButton(person: Person, personworktrip: Personworktrip) {

        this.personworktripMenuelement = personworktrip;


        this.personworktripCountry = this.personworktripMenuelement.country;
        this.personworktripReason = this.personworktripMenuelement.reason;
        this.personworktripTripdate = this.personworktripMenuelement.tripdateString;
        this.personworktripDays = this.personworktripMenuelement.days;
        this.personworktripPrivelege = this.personworktripMenuelement.privelegeBool;

        this.personworktripMenuvisible = true;
    }

    completePersonworktripButton(person: Person) {
        if (this.personworktripMenuelement == null) {
            this.addPersonworktrip();
        } else {

            this.personworktripMenuelement.country = this.prepareNumToExport(this.personworktripCountry);
            this.personworktripMenuelement.reason = this.personworktripReason;
            this.personworktripMenuelement.tripdateString = this.personworktripTripdate;
            this.personworktripMenuelement.days = this.prepareNumToExport(this.personworktripDays);
            this.personworktripMenuelement.privelegeBool = this.personworktripPrivelege;

            this.updatePersonworktrip(person, this.personworktripMenuelement);
        }

        this.personworktripMenuvisible = false;
    }

    togglePersonworktripPrivelege(personill: number) {

        fetch('/api/Personworktrip/Toggleprivelege' + personill, { credentials: 'include' })
            .then(response => response.json())
            .then(response => {
                this.saveChanges(this.person);
            });
    }

    addPersonelection() {
        //this.prepareToExport(person);
        fetch('/api/Personelection', {
            method: 'post',
            body: JSON.stringify(<Personelection>{
                person: this.person.id,
                location: this.personelectionLocation,
                electiondate: this.prepareDateToExport(this.personelectionElectiondate),
                electionwho: this.personelectionElectionwho,
                electionwhat: this.personelectionElectionwhat,
                electiondateend: this.prepareDateToExport(this.personelectionElectiondateend),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonelection(person: Person, personelection: Personelection) {
        this.prepareToExport(person);
        fetch('/api/Personelection', {
            method: 'post',
            body: JSON.stringify(personelection),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonelection(person: Person, personelection: Personelection) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personelection.id = -personelection.id;
        fetch('/api/Personelection', {
            method: 'post',
            body: JSON.stringify(personelection),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersonelectionUpdateName(): string {
        if (this.personelectionMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonelectionUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonelectionButton(person: Person) {
        this.personelectionMenuvisible = true;
        this.personelectionMenuelement = null;
    }

    updatePersonelectionButton(person: Person, personelection: Personelection) {

        this.personelectionMenuelement = personelection;

        this.personelectionLocation = this.personelectionMenuelement.location;
        this.personelectionElectiondate = this.personelectionMenuelement.electiondateString;
        this.personelectionElectionwho = this.personelectionMenuelement.electionwho;
        this.personelectionElectionwhat = this.personelectionMenuelement.electionwhat;
        this.personelectionElectiondateend = this.personelectionMenuelement.electiondateendString;

        this.personelectionMenuvisible = true;
    }

    completePersonelectionButton(person: Person) {
        if (this.personelectionMenuelement == null) {
            this.addPersonelection();
        } else {
            this.personelectionMenuelement.location = this.personelectionLocation;
            this.personelectionMenuelement.electiondateString = this.personelectionElectiondate;
            this.personelectionMenuelement.electionwho = this.personelectionElectionwho;
            this.personelectionMenuelement.electionwhat = this.personelectionElectionwhat;
            this.personelectionMenuelement.electiondateendString = this.personelectionElectiondateend;

            this.updatePersonelection(person, this.personelectionMenuelement);
        }

        this.personelectionMenuvisible = false;
    }

    addPersonscience() {
        //this.prepareToExport(person);
        fetch('/api/Personscience', {
            method: 'post',
            body: JSON.stringify(<Personscience>{
                person: this.person.id,
                sciencetype: this.prepareNumToExport(this.personscienceSciencetype),
                sciencedescription: this.personscienceSciencedescription,
                sciencedate: this.prepareDateToExport(this.personscienceSciencedate),
                sciencediplom: this.personscienceSciencediplom,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonscience(person: Person, personscience: Personscience) {
        this.prepareToExport(person);
        fetch('/api/Personscience', {
            method: 'post',
            body: JSON.stringify(personscience),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonscience(person: Person, personscience: Personscience) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personscience.id = -personscience.id;
        fetch('/api/Personscience', {
            method: 'post',
            body: JSON.stringify(personscience),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    addPersonreward() {
        //this.prepareToExport(person);
        fetch('/api/Personreward', {
            method: 'post',
            body: JSON.stringify(<Personreward>{
                person: this.person.id,
                rewardtype: this.prepareNumToExport(this.personrewardRewardtype),
                reward: this.prepareNumToExport(this.personrewardReward),
                reason: this.personrewardReason,
                order: this.personrewardOrder,
                ordernumbertype: this.personrewardOrdernumbertype,
                rewarddate: this.prepareDateToExport(this.personrewardDate),
                optionnumber1: this.prepareNumToExport(this.personrewardOptionnumber1),
                optionnumber2: this.prepareNumToExport(this.personrewardOptionnumber2),
                optionstring1: this.personrewardOptionstring1,
                optionstring2: this.personrewardOptionstring2,
                orderwho: this.personrewardOrderwho,
                orderwhoid: this.prepareNumToExport(this.personrewardOrderwhoid),
                orderid: this.prepareNumToExport(this.personrewardOrderid),
                area: this.prepareNumToExport(this.personrewardArea),
                areaother: this.prepareNumToExport(this.personrewardAreaother),
                externalorderwhotype: this.personrewardExternalorderwhotype,
                externalordertype: this.prepareNumToExport(this.personrewardExternalordertype),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonreward(person: Person, personreward: Personreward) {
        this.prepareToExport(person);
        fetch('/api/Personreward', {
            method: 'post',
            body: JSON.stringify(personreward),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonreward(person: Person, personreward: Personreward) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personreward.id = -personreward.id;
        fetch('/api/Personreward', {
            method: 'post',
            body: JSON.stringify(personreward),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getReward(rewardid: number): string {
        if (rewardid == null || rewardid == 0) {
            return "";
        }
        let reward: Reward = this.rewards.find(t => t.id == rewardid);
        if (reward != null) {
            return reward.name;
        } else {
            return "";
        }

    }

    getRewardtype(rewardtypeid: number): string {
        if (rewardtypeid == null || rewardtypeid == 0) {
            return "";
        }
        let rewardtype: Rewardtype = this.rewardtypes.find(t => t.id == rewardtypeid);
        if (rewardtype != null) {
            return rewardtype.name;
        } else {
            return "";
        }

    }

    getRewardtypeObject(rewardtypeid: number): Rewardtype {
        if (rewardtypeid == null || rewardtypeid == 0) {
            return null;
        }
        let rewardtype: Rewardtype = this.rewardtypes.find(t => t.id == rewardtypeid);
        if (rewardtype != null) {
            return rewardtype;
        }
    }

    getRegionObject(regionid: number): Region {
        if (regionid == null || regionid == 0) {
            return null;
        }
        let region: Region = this.regions.find(t => t.id == regionid);
        if (region != null) {
            return region;
        }
    }

    getPersonrewardUpdateName(): string {
        if (this.personrewardMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonrewardUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonrewardButton(person: Person) {
        this.personrewardMenuvisible = true;
        this.personrewardMenuelement = null;

        this.personrewardRewardtype = null;
        this.personrewardReward = null;
        this.personrewardReason = "";
        this.personrewardOrderwhoid = null;
        this.personrewardOrderid = null;
        this.personrewardOrder = "";
        this.personrewardOrdernumbertype = "";
        this.personrewardOrderwho = null;
        this.personrewardDate = "";
        this.personrewardOptionnumber1 = null;
        this.personrewardOptionnumber2 = null;
        this.personrewardOptionstring1 = "";
        this.personrewardOptionstring2 = "";
        this.personrewardArea = null;
        this.personrewardAreaother = null;
        this.personrewardAreaotherdisplay = false;
        this.personrewardExternalorderwhotype = "";
        this.personrewardExternalordertype = null;
    }

    updatePersonrewardButton(person: Person, personreward: Personreward) {

        this.personrewardMenuelement = personreward;

        this.personrewardRewardtype = this.personrewardMenuelement.rewardtype;
        this.personrewardReward = this.personrewardMenuelement.reward;
        this.personrewardReason = this.personrewardMenuelement.reason;
        this.personrewardOrder = this.personrewardMenuelement.order;
        this.personrewardOrdernumbertype = this.personrewardMenuelement.ordernumbertype;
        this.personrewardDate = this.personrewardMenuelement.rewarddateString;
        this.personrewardOptionnumber1 = this.personrewardMenuelement.optionnumber1;
        this.personrewardOptionnumber2 = this.personrewardMenuelement.optionnumber2;
        this.personrewardOptionstring1 = this.personrewardMenuelement.optionstring1;
        this.personrewardOptionstring2 = this.personrewardMenuelement.optionstring2;
        this.personrewardOrderwho = this.personrewardMenuelement.orderwho;
        this.personrewardOrderwhoid = this.personrewardMenuelement.orderwhoid;
        this.personrewardOrderid = this.personrewardMenuelement.orderid;
        this.personrewardArea = this.personrewardMenuelement.area;
        this.personrewardAreaother = this.personrewardMenuelement.areaother;
        this.personrewardExternalorderwhotype = this.personrewardMenuelement.externalorderwhotype;
        this.personrewardExternalordertype = this.personrewardMenuelement.externalordertype;

        this.personrewardMenuvisible = true;
    }

    completePersonrewardButton(person: Person) {
        if (this.personrewardMenuelement == null) {
            this.addPersonreward();
        } else {
            this.personrewardMenuelement.rewardtype = this.prepareNumToExport(this.personrewardRewardtype);
            this.personrewardMenuelement.reward = this.prepareNumToExport(this.personrewardReward);
            this.personrewardMenuelement.reason = this.personrewardReason;
            this.personrewardMenuelement.order = this.personrewardOrder;
            this.personrewardMenuelement.ordernumbertype = this.personrewardOrdernumbertype;
            this.personrewardMenuelement.rewarddateString = this.personrewardDate;
            this.personrewardMenuelement.optionnumber1 = this.prepareNumToExport(this.personrewardOptionnumber1);
            this.personrewardMenuelement.optionnumber2 = this.prepareNumToExport(this.personrewardOptionnumber2);
            this.personrewardMenuelement.optionstring1 = this.personrewardOptionstring1;
            this.personrewardMenuelement.optionstring2 = this.personrewardOptionstring2;
            this.personrewardMenuelement.orderwho = this.personrewardOrderwho;
            this.personrewardMenuelement.orderwhoid = this.prepareNumToExport(this.personrewardOrderwhoid);
            this.personrewardMenuelement.orderid = this.prepareNumToExport(this.personrewardOrderid);
            this.personrewardMenuelement.area = this.personrewardArea;
            this.personrewardMenuelement.areaother = this.personrewardAreaother;
            this.personrewardMenuelement.externalorderwhotype = this.personrewardExternalorderwhotype; 
            this.personrewardMenuelement.externalordertype = this.personrewardExternalordertype;

            this.updatePersonreward(person, this.personrewardMenuelement);
        }

        this.personrewardMenuvisible = false;
    }

    addPersonill() {
        //this.prepareToExport(person);
        fetch('/api/Personill', {
            method: 'post',
            body: JSON.stringify(<Personill>{
                person: this.person.id,
                illtype: this.boolToNumb(this.personillIlltype),
                illcode: this.prepareNumToExport(this.personillIllcode),
                datestart: this.prepareDateToExport(this.personillDatestart),
                dateend: this.prepareDateToExport(this.personillDateend),
                illregime: this.prepareNumToExport(this.personillIllregime),
                illwho: this.personillIllwho,
                privelege: this.boolToNumb(this.personillPrivelege),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonill(person: Person, personill: Personill) {
        this.prepareToExport(person);
        fetch('/api/Personill', {
            method: 'post',
            body: JSON.stringify(personill),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonill(person: Person, personill: Personill) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personill.id = -personill.id;
        fetch('/api/Personill', {
            method: 'post',
            body: JSON.stringify(personill),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getIllregime(illregime: number): string {
        if (illregime == null || illregime == 0) {
            return "";
        }
        let iregime: Illregime = this.illregimes.find(t => t.id == illregime);
        if (iregime != null) {
            return iregime.name;
        } else {
            return "";
        }
    }

    getPersonillUpdateName(): string {
        if (this.personillMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonillUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonillButton(person: Person) {
        this.personillMenuvisible = true;
        this.personillMenuelement = null;
        this.personillIlltype = false;
        this.personillIllcode = null;
        this.personillDatestart = "";
        this.personillDateend = "";
        this.personillIllregime = null;
        this.personillIllwho = "";
        this.personillPrivelege = false;
    }

    updatePersonillButton(person: Person, personill: Personill) {

        this.personillMenuelement = personill;

        this.personillIlltype = this.personillMenuelement.illtypeBool;
        this.personillIllcode = this.personillMenuelement.illcode;
        this.personillDatestart = this.personillMenuelement.datestartString;
        this.personillDateend = this.personillMenuelement.dateendString;
        this.personillIllregime = this.personillMenuelement.illregime;
        this.personillIllwho = this.personillMenuelement.illwho;
        this.personillPrivelege = this.personillMenuelement.privelegeBool;

        this.personillMenuvisible = true;
    }

    completePersonillButton(person: Person) {
        if (this.personillMenuelement == null) {
            this.addPersonill();
        } else {
            this.personillMenuelement.illtypeBool = this.personillIlltype;
            this.personillMenuelement.illcode = this.prepareNumToExport(this.personillIllcode);
            this.personillMenuelement.datestartString = this.personillDatestart;
            this.personillMenuelement.dateendString = this.personillDateend;
            this.personillMenuelement.illregime = this.prepareNumToExport(this.personillIllregime);
            this.personillMenuelement.illwho = this.personillIllwho;
            this.personillMenuelement.privelegeBool = this.personillPrivelege;
            this.updatePersonill(person, this.personillMenuelement);
            
        }
        this.personillMenuvisible = false;
    }

    //personillPrivelegeOn() {
    //    this.personillPrivelege(true);
    //}

    //personillPrivelegeOff() {
    //    this.personillPrivelege(false);
    //}

    //personillPrivelege(privelege: boolean) {

    //}

    togglePersonillPrivelege(personill: number) {

        fetch('/api/Personill/Toggleprivelege' + personill, { credentials: 'include' })
            .then(response => response.json())
            .then(response => {
                this.saveChanges(this.person);
            });
    }

    addPersoneducation() {
        this.personeducationEducationtypeblocks.forEach(etb => {
            etb.educationperiods.forEach(ep => {
                ep.start = this.prepareDateToExportNullable(ep.startString);
                ep.end = this.prepareDateToExportNullable(ep.endString);
                ep.service = this.boolToNumb(ep.serviceBool);
                ep.orderdate = this.prepareDateToExportNullable(ep.orderdateString);
            })
        })
        this.personeducationAcademicvacations.forEach(av => {
            av.start = this.prepareDateToExportNullable(av.startString);
            av.end = this.prepareDateToExportNullable(av.endString);
            av.orderdate = this.prepareDateToExportNullable(av.orderdateString);
        })
        this.personeducationEducationmaternities.forEach(em => {
            em.start = this.prepareDateToExportNullable(em.startString);
            em.end = this.prepareDateToExportNullable(em.endString);
            em.orderdate = this.prepareDateToExportNullable(em.orderdateString);
        })

        fetch('/api/Personeducation', {
            method: 'post',
            body: JSON.stringify(<Personeducation>{
                person: this.person.id,
                main: this.prepareNumToExport(this.personeducationMain),
                educationlevel: this.prepareNumToExport(this.personeducationEducationlevel),
                educationstage: this.prepareNumToExport(this.personeducationEducationstage),
                name: this.personeducationName,
                name2: this.personeducationName2,
                location: this.personeducationLocation,
                city: this.personeducationCity,
                faculty: this.personeducationFaculty,
                educationtype: this.prepareNumToExport(this.personeducationEducationtype),
                datestart: this.prepareNumToExport(this.personeducationDatestart),
                dateend: this.prepareNumToExport(this.personeducationDateend),
                speciality: this.personeducationSpeciality,
                documentseries: this.personeducationDocumentseries,
                documentnumber: this.personeducationDocumentnumber,
                cadet: this.boolToNumb(this.personeducationCadet),
                qualification: this.personeducationQualification,
                start: this.prepareDateToExportNullable(this.personeducationStart),
                end: this.prepareDateToExportNullable(this.personeducationEnd),
                interrupted: this.boolToNumb(this.personeducationInterrupted),
                interruptorderdate: this.prepareDateToExportNullable(this.personeducationInterruptorderdate),
                interruptordernumber: this.personeducationInterruptordernumber,
                interruptordernumbertype: this.personeducationInterruptordernumbertype,
                interruptorderwho: this.personeducationInterruptorderwho,
                interruptorderreason: this.personeducationInterruptorderreason,
                educationdocument: this.prepareNumToExport(this.personeducationEducationdocument),
                orderdate: this.prepareDateToExportNullable(this.personeducationOrderdate),
                ordernumber: this.personeducationOrdernumber,
                ordernumbertype: this.personeducationOrdernumbertype,
                orderwho: this.personeducationOrderwho,
                orderwhoid: this.prepareNumToExport(this.personeducationOrderwhoid),
                orderid: this.prepareNumToExport(this.personeducationOrderid),
                nameasjobfull: this.personeducationNameasjobfull,
                nameasjobplace: this.personeducationNameasjobplace,
                nameasjobposition: this.personeducationNameasjobposition,
                educationadditionaltype: this.prepareNumToExport(this.personeducationEducationadditionaltype),
                ucp: this.prepareNumToExport(this.personeducationUcp),
                academicvacation: this.boolToNumb(this.personeducationAcademicvacation),
                maternityvacation: this.boolToNumb(this.personeducationMaternityvacation),
                educationtypeblocks: this.personeducationEducationtypeblocks,
                academicvacations: this.personeducationAcademicvacations,
                educationmaternities: this.personeducationEducationmaternities,
                rating: this.prepareNumToExport(this.personeducationRating),
                state: this.personeducationState,
                citytype: this.personeducationCitytype,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersoneducation(person: Person, personeducation: Personeducation) {
        this.prepareToExport(person);
        //alert(JSON.stringify(personeducation.educationtypeblocks));
        fetch('/api/Personeducation', {
            method: 'post',
            body: JSON.stringify(personeducation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersoneducation(person: Person, personeducation: Personeducation) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personeducation.id = -personeducation.id;
        fetch('/api/Personeducation', {
            method: 'post',
            body: JSON.stringify(personeducation),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getEducationtype(educationtype: number): string {
        if (educationtype == null || educationtype == 0) {
            return "";
        }
        let etype: Educationtype = this.educationtypes.find(t => t.id == educationtype);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }
    } 

    getEducationlevel(educationlevel: number): string {
        if (educationlevel == null || educationlevel == 0) {
            return "";
        }
        let object: Educationlevel = this.educationlevels.find(e => e.id == educationlevel);
        if (object != null) {
            return object.levelname;
        } else {
            return "";
        }
    } 

    getEducationpositiontype(educationpositiontype: number): string {
        if (educationpositiontype == null || educationpositiontype == 0) {
            return "";
        }
        let object: Educationpositiontype = this.educationpositiontypes.find(e => e.id == educationpositiontype);
        if (object != null) {
            return object.name;
        } else {
            return "";
        }
    }

    getEducationadditionaltype(educationadditionaltype: number): string {
        if (educationadditionaltype == null || educationadditionaltype == 0) {
            return "";
        }
        let etype: Educationtype = this.educationadditionaltypes.find(t => t.id == educationadditionaltype);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }

    }

    getEducationdocument(educationdocument: number): string {
        if (educationdocument == null || educationdocument == 0) {
            return "";
        }
        let etype: Educationdocument = this.educationdocuments.find(t => t.id == educationdocument);
        if (etype != null) {
            return etype.name;
        } else {
            return "";
        }

    }

    getPersoneducationUpdateName(): string {
        if (this.personeducationMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersoneducationUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersoneducationButton(person: Person) {
        this.personeducationMenuvisible = true;
        this.personeducationMenuelement = null;
        this.personeducationEducationlevel = null;
        this.personeducationEducationstage = null;
        this.personeducationLocation = "Республика Беларусь";
        this.personeducationCity = "";
        this.personeducationCadet = false;
        this.personeducationStart = "";
        this.personeducationEnd = "";
        this.personeducationName = 'ГУО "Университет гражданской защиты МЧС Беларуси"';
        this.personeducationName2 = 'ГУО "Университет гражданской защиты МЧС Беларуси"';
        this.personeducationEducationdocument = null;
        this.personeducationInterrupted = false;
        this.personeducationInterruptorderdate = null;
        this.personeducationInterruptorderwho = 'ГУО "Университет гражданской защиты МЧС Беларуси"';
        this.personeducationInterruptordernumber = "";
        this.personeducationInterruptordernumbertype = "";
        this.personeducationInterruptorderreason = "";
        this.personeducationOrderdate = null;
        this.personeducationOrdernumber = "";
        this.personeducationOrdernumbertype = "";
        this.personeducationOrderwho = "";
        this.personeducationOrderwhoid = null;
        this.personeducationOrderid = null;
        this.personeducationNameasjobfull = "";
        this.personeducationNameasjobplace = "";
        this.personeducationNameasjobposition = "";
        this.personeducationEducationadditionaltype = null;
        this.personeducationAcademicvacation = false;
        this.personeducationMaternityvacation = false;
        this.personeducationEducationtypeblocks = new Array();
        this.personeducationAcademicvacations = new Array();
        this.personeducationEducationmaternities = new Array();
        this.addEducationtypeblock();
        this.personeducationRating = null;
        this.personeducationFaculty = "";
        this.personeducationSpeciality = "";
        this.personeducationQualification = "";
        this.personeducationDocumentnumber = "";
        this.personeducationDocumentseries = "";
        this.personeducationState = "г. Минск";
        this.personeducationCitytype = "";
    }

    updatePersoneducationButton(person: Person, personeducation: Personeducation) {

        this.personeducationMenuelement = personeducation;
        this.personeducationMain = this.personeducationMenuelement.main;
        this.personeducationEducationlevel = this.personeducationMenuelement.educationlevel;
        this.personeducationEducationstage = this.personeducationMenuelement.educationstage;
        this.personeducationName = this.personeducationMenuelement.name;
        this.personeducationName2 = this.personeducationMenuelement.name2;
        this.personeducationLocation = this.personeducationMenuelement.location;
        this.personeducationCity = this.personeducationMenuelement.city;
        this.personeducationFaculty = this.personeducationMenuelement.faculty;
        this.personeducationEducationtype = this.personeducationMenuelement.educationtype;
        this.personeducationDatestart = this.personeducationMenuelement.datestart;
        this.personeducationDateend = this.personeducationMenuelement.dateend;
        this.personeducationSpeciality = this.personeducationMenuelement.speciality;
        this.personeducationDocumentseries = this.personeducationMenuelement.documentseries;
        this.personeducationDocumentnumber = this.personeducationMenuelement.documentnumber;
        this.personeducationCadet = this.personeducationMenuelement.cadetBool;
        this.personeducationQualification = this.personeducationMenuelement.qualification;
        this.personeducationStart = this.personeducationMenuelement.startString;
        this.personeducationEnd = this.personeducationMenuelement.endString;
        this.personeducationInterrupted = this.personeducationMenuelement.interruptedBool;
        this.personeducationInterruptorderdate = this.personeducationMenuelement.interruptorderdateString;
        this.personeducationInterruptorderwho = this.personeducationMenuelement.interruptorderwho;
        this.personeducationInterruptordernumber = this.personeducationMenuelement.interruptordernumber;
        this.personeducationInterruptordernumbertype = this.personeducationMenuelement.interruptordernumbertype;
        this.personeducationInterruptorderreason = this.personeducationMenuelement.interruptorderreason;
        this.personeducationEducationdocument = this.personeducationMenuelement.educationdocument;
        this.personeducationOrderdate = this.personeducationMenuelement.orderdateString;
        this.personeducationOrdernumber = this.personeducationMenuelement.ordernumber;
        this.personeducationOrdernumbertype = this.personeducationMenuelement.ordernumbertype;
        this.personeducationOrderwho = this.personeducationMenuelement.orderwho;
        this.personeducationOrderwhoid = this.personeducationMenuelement.orderwhoid;
        this.personeducationOrderid = this.personeducationMenuelement.orderid;
        this.personeducationNameasjobfull = this.personeducationMenuelement.nameasjobfull;
        this.personeducationNameasjobplace = this.personeducationMenuelement.nameasjobplace;
        this.personeducationNameasjobposition = this.personeducationMenuelement.nameasjobposition;
        this.personeducationEducationadditionaltype = this.personeducationMenuelement.educationadditionaltype;
        this.personeducationUcp = this.personeducationMenuelement.ucp
        this.personeducationEducationtypeblocks = this.personeducationMenuelement.educationtypeblocks;
        this.personeducationAcademicvacations = this.personeducationMenuelement.academicvacations;
        this.personeducationEducationmaternities = this.personeducationMenuelement.educationmaternities;
        this.personeducationAcademicvacation = this.personeducationMenuelement.academicvacationBool;
        this.personeducationMaternityvacation = this.personeducationMenuelement.maternityvacationBool;
        this.personeducationRating = this.personeducationMenuelement.rating;
        this.personeducationState = this.personeducationMenuelement.state;
        this.personeducationCitytype = this.personeducationMenuelement.citytype;
        this.personeducationMenuvisible = true;
    }

    completePersoneducationButton(person: Person) {
        if (!this.validatePersoneducation()) {
            (<any>Vue).notify("E:Ошибка сохранения формы");
            return;
        }
        if (this.personeducationMenuelement == null) {
            this.addPersoneducation();
        } else {
            this.personeducationMenuelement.main = this.prepareNumToExport(this.personeducationMain);
            this.personeducationMenuelement.educationlevel = this.prepareNumToExport(this.personeducationEducationlevel);
            this.personeducationMenuelement.educationstage = this.prepareNumToExport(this.personeducationEducationstage);
            this.personeducationMenuelement.name = this.personeducationName;
            this.personeducationMenuelement.name2 = this.personeducationName2;
            this.personeducationMenuelement.location = this.personeducationLocation;
            this.personeducationMenuelement.city = this.personeducationCity;
            this.personeducationMenuelement.faculty = this.personeducationFaculty;
            this.personeducationMenuelement.educationtype = this.prepareNumToExport(this.personeducationEducationtype);
            this.personeducationMenuelement.datestart = this.prepareNumToExport(this.personeducationDatestart);
            this.personeducationMenuelement.dateend = this.prepareNumToExport(this.personeducationDateend);
            this.personeducationMenuelement.speciality = this.personeducationSpeciality;
            this.personeducationMenuelement.documentseries = this.personeducationDocumentseries;
            this.personeducationMenuelement.documentnumber = this.personeducationDocumentnumber;
            this.personeducationMenuelement.cadetBool = this.personeducationCadet;
            this.personeducationMenuelement.qualification = this.personeducationQualification;
            this.personeducationMenuelement.startString = this.personeducationStart;
            this.personeducationMenuelement.endString = this.personeducationEnd;
            this.personeducationMenuelement.interruptedBool = this.personeducationInterrupted;
            this.personeducationMenuelement.interruptorderdateString = this.personeducationInterruptorderdate;
            this.personeducationMenuelement.interruptordernumber = this.personeducationInterruptordernumber;
            this.personeducationMenuelement.interruptordernumbertype = this.personeducationInterruptordernumbertype;
            this.personeducationMenuelement.interruptorderwho = this.personeducationInterruptorderwho;
            this.personeducationMenuelement.interruptorderreason = this.personeducationInterruptorderreason;
            this.personeducationMenuelement.educationdocument = this.prepareNumToExport(this.personeducationEducationdocument);
            this.personeducationMenuelement.orderdateString = this.personeducationOrderdate;
            this.personeducationMenuelement.ordernumber = this.personeducationOrdernumber;
            this.personeducationMenuelement.ordernumbertype = this.personeducationOrdernumbertype;
            this.personeducationMenuelement.orderwho = this.personeducationOrderwho;
            this.personeducationMenuelement.orderwhoid = this.prepareNumToExport(this.personeducationOrderwhoid);
            this.personeducationMenuelement.orderid = this.prepareNumToExport(this.personeducationOrderid);
            this.personeducationMenuelement.nameasjobfull = this.personeducationNameasjobfull;
            this.personeducationMenuelement.nameasjobplace = this.personeducationNameasjobplace;
            this.personeducationMenuelement.nameasjobposition = this.personeducationNameasjobposition;
            this.personeducationMenuelement.educationadditionaltype = this.personeducationEducationadditionaltype;
            this.personeducationMenuelement.ucp = this.prepareNumToExport(this.personeducationUcp);
            this.personeducationMenuelement.educationtypeblocks = this.personeducationEducationtypeblocks;
            this.personeducationMenuelement.academicvacations = this.personeducationAcademicvacations;
            this.personeducationMenuelement.educationmaternities = this.personeducationEducationmaternities;
            this.personeducationMenuelement.academicvacationBool = this.personeducationAcademicvacation;
            this.personeducationMenuelement.maternityvacationBool = this.personeducationMaternityvacation;
            this.personeducationMenuelement.rating = this.prepareNumToExport(this.personeducationRating);
            this.personeducationMenuelement.state = this.personeducationState;
            this.personeducationMenuelement.citytype = this.personeducationCitytype;

            //this.personelectionMenuelement.electiondateendString = this.personelectionElectiondateend;
             this.updatePersoneducation(person, this.personeducationMenuelement);
        }

        this.personeducationMenuvisible = false;
    }

    

    addEducationtypeblock() {
        let educationtypeblock: Educationtypeblock = new Educationtypeblock();
        educationtypeblock.educationperiods = new Array();
        let educationperiod: Educationperiod = new Educationperiod();
        educationperiod.startString = this.personeducationStart;
        educationtypeblock.educationperiods.push(educationperiod);
        this.personeducationEducationtypeblocks.push(educationtypeblock);
    }

    removeEducationtypeblock(educationtypeblock: Educationtypeblock) {
        this.personeducationEducationtypeblocks = this.personeducationEducationtypeblocks.filter(e => e != educationtypeblock);
    }

    addEducationperiod(educationtypeblock: Educationtypeblock) {
        let educationperiod: Educationperiod = new Educationperiod();
        educationperiod.startString = this.personeducationStart;
        educationtypeblock.educationperiods.push(educationperiod);
    }

    removeEducationperiod(educationtypeblock: Educationtypeblock, educationperiod: Educationperiod) {
        educationtypeblock.educationperiods = educationtypeblock.educationperiods.filter(e => e != educationperiod);
    }

    addAcademicvacation() {
        let academicvacation: Academicvacation = new Academicvacation();
        academicvacation.orderwho = this.personeducationName2;
        this.personeducationAcademicvacations.push(academicvacation);
    }

    removeAcademicvacation(academicvacation: Academicvacation) {
        this.personeducationAcademicvacations = this.personeducationAcademicvacations.filter(e => e != academicvacation);
    }

    addEducationmaternity() {
        let educationmaternity: Educationmaternity = new Educationmaternity();
        educationmaternity.orderwho = this.personeducationName2;
        this.personeducationEducationmaternities.push(educationmaternity);
    }

    removeEducationmaternity(educationmaternity: Educationmaternity) {
        this.personeducationEducationmaternities = this.personeducationEducationmaternities.filter(e => e != educationmaternity);
    }

    personeducationAcademicvacationCheck() {
        this.forceUpdate();
        if (this.personeducationAcademicvacations.length == 0) {
            this.addAcademicvacation();
        }
    }

    personeducationEducationmaternityCheck() {
        this.forceUpdate();
        if (this.personeducationEducationmaternities.length == 0) {
            this.addEducationmaternity();
        }
    }

    validatePersoneducation(): boolean {
        if (!this.validateInterrupted()) {
            return false;
        }

        let returnfalse: boolean = false;
        this.personeducationAcademicvacations.forEach(av => {
            if (!this.validateAcademicvacation(av)) {
                returnfalse = true;
                return false;
            }
        })
        if (returnfalse) {
            return false;
        }

        this.personeducationEducationmaternities.forEach(em => {
            if (!this.validateEducationmaternity(em)) {
                returnfalse = true;
                return false;
            }
        })
        if (returnfalse) {
            return false;
        }

        return true;
    }

    validateInterrupted(): boolean {
        if (!this.validatePersoneducationInterruptorderwho()) {
            return false;
        }
        if (!this.validatePersoneducationInterruptorderdate()) {
            return false;
        }
        if (!this.validatePersoneducationInterruptordernumber()) {
            return false;
        }
        if (!this.validatePersoneducationInterruptorderreason()) {
            return false;
        }
        return true;
    }

    validatePersoneducationInterruptorderwho(): boolean {
        if (this.personeducationInterrupted && (this.personeducationInterruptorderwho == null || this.personeducationInterruptorderwho.length == 0)) {
            return false;
        }
        return true;
    }

    validatePersoneducationInterruptorderdate(): boolean {
        if (this.personeducationInterrupted && (this.personeducationInterruptorderdate == null || this.personeducationInterruptorderdate.length == 0)) {
            return false;
        }
        return true;
    }

    validatePersoneducationInterruptordernumber(): boolean {
        if (this.personeducationInterrupted && (this.personeducationInterruptordernumber == null || this.personeducationInterruptordernumber.length == 0)) {
            return false;
        }
        return true;
    }

    validatePersoneducationInterruptordernumbertype(): boolean {
        if (this.personeducationInterrupted && this.personeducationInterruptordernumbertype.length == 0) {
            return false;
        }
        return true;
    }

    validatePersoneducationInterruptorderreason(): boolean {
        if (this.personeducationInterrupted && this.personeducationInterruptorderreason.length == 0) {
            return false;
        }
        return true;
    }

    validateAcademicvacation(academicvacation: Academicvacation): boolean {
        if (!this.validateAcademicvacationStart(academicvacation)) {
            return false;
        }
        if (!this.validateAcademicvacationEnd(academicvacation)) {
            return false;
        }
        if (!this.validateAcademicvacationOrderdate(academicvacation)) {
            return false;
        }
        if (!this.validateAcademicvacationOrdernumber(academicvacation)) {
            return false;
        }
        if (!this.validateAcademicvacationOrderwho(academicvacation)) {
            return false;
        }
        return true;
    }

    validateAcademicvacationStart(academicvacation: Academicvacation): boolean {
        if (this.personeducationAcademicvacation && (academicvacation.startString == null || academicvacation.startString.length == 0)){
            return false;
        }
        return true;
    }

    validateAcademicvacationEnd(academicvacation: Academicvacation): boolean {
        if (this.personeducationAcademicvacation && (academicvacation.endString == null || academicvacation.endString.length == 0)) {
            return false;
        }
        return true;
    }

    validateAcademicvacationOrderwho(academicvacation: Academicvacation): boolean {
        if (this.personeducationAcademicvacation && (academicvacation.orderwho == null || academicvacation.orderwho.length == 0)) {
            return false;
        }
        return true;
    }

    validateAcademicvacationOrderdate(academicvacation: Academicvacation): boolean {
        if (this.personeducationAcademicvacation && (academicvacation.orderdateString == null || academicvacation.orderdateString.length == 0)) {
            return false;
        }
        return true;
    }

    validateAcademicvacationOrdernumber(academicvacation: Academicvacation): boolean {
        if (this.personeducationAcademicvacation && (academicvacation.ordernumber == null || academicvacation.ordernumber.length == 0)) {
            return false;
        }
        return true;
    }

    validateEducationmaternity(educationmaternity: Educationmaternity): boolean {
        if (!this.validateEducationmaternityStart(educationmaternity)) {
            return false;
        }
        if (!this.validateEducationmaternityEnd(educationmaternity)) {
            return false;
        }
        if (!this.validateEducationmaternityOrderdate(educationmaternity)) {
            return false;
        }
        if (!this.validateEducationmaternityOrdernumber(educationmaternity)) {
            return false;
        }
        if (!this.validateEducationmaternityOrderwho(educationmaternity)) {
            return false;
        }
        return true;
    }

    validateEducationmaternityStart(educationmaternity: Educationmaternity): boolean {
        if (this.personeducationMaternityvacation && (educationmaternity.startString == null || educationmaternity.startString.length == 0)) {
            return false;
        }
        return true;
    }

    validateEducationmaternityEnd(educationmaternity: Educationmaternity): boolean {
        if (this.personeducationMaternityvacation && (educationmaternity.endString == null || educationmaternity.endString.length == 0)) {
            return false;
        }
        return true;
    }

    validateEducationmaternityOrderwho(educationmaternity: Educationmaternity): boolean {
        if (this.personeducationMaternityvacation && (educationmaternity.orderwho == null || educationmaternity.orderwho.length == 0)) {
            return false;
        }
        return true;
    }

    validateEducationmaternityOrderdate(educationmaternity: Educationmaternity): boolean {
        if (this.personeducationMaternityvacation && (educationmaternity.orderdateString == null || educationmaternity.orderdateString.length == 0)) {
            return false;
        }
        return true;
    }

    validateEducationmaternityOrdernumber(educationmaternity: Educationmaternity): boolean {
        if (this.personeducationMaternityvacation && (educationmaternity.ordernumber == null || educationmaternity.ordernumber.length == 0)) {
            return false;
        }
        return true;
    }

    addPersonphysical() {
        //this.prepareToExport(person);
        fetch('/api/Personphysical', {
            method: 'post',
            body: JSON.stringify(<Personphysical>{
                person: this.person.id,
                physicaldate: this.prepareDateToExport(this.personphysicalPhysicaldate),
                physicalfields: this.personphysicalPhysicalfields,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonphysical(person: Person, personphysical: Personphysical) {
        this.prepareToExport(person);
        fetch('/api/Personphysical', {
            method: 'post',
            body: JSON.stringify(personphysical),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonphysical(person: Person, personphysical: Personphysical) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personphysical.id = -personphysical.id;
        fetch('/api/Personphysical', {
            method: 'post',
            body: JSON.stringify(personphysical),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersonphysicalUpdateName(): string {
        if (this.personphysicalMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonphysicalUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonphysicalButton(person: Person) {
        this.personphysicalMenuvisible = true;
        this.personphysicalMenuelement = null;
    }

    updatePersonphysicalButton(person: Person, personphysical: Personphysical) {

        this.personphysicalMenuelement = personphysical;


        this.personphysicalPhysicaldate = this.personphysicalMenuelement.physicaldateString;
        this.personphysicalPhysicalfields = this.personphysicalMenuelement.physicalfields;

        //person: this.person.id,
        //physicaldate: this.prepareDateToExport(this.personphysicalPhysicaldate),
        //    physicalfields: this.personphysicalPhysicalfields,
        //
        //

        this.personphysicalMenuvisible = true;
    }

    completePersonphysicalButton(person: Person) {
        if (this.personphysicalMenuelement == null) {
            this.addPersonphysical();
        } else {


            this.personphysicalMenuelement.physicaldateString = this.personphysicalPhysicaldate;
            this.personphysicalMenuelement.physicalfields = this.personphysicalPhysicalfields;

            this.updatePersonphysical(person, this.personphysicalMenuelement);
        }

        this.personphysicalMenuvisible = false;
    }

    addPersondriver() {
        //this.prepareToExport(person);
        fetch('/api/Persondriver', {
            method: 'post',
            body: JSON.stringify(<Persondriver>{
                person: this.person.id,
                drivertype: this.prepareNumToExport(this.persondriverDrivertype),
                series: this.persondriverSeries,
                number: this.persondriverNumber,
                datestart: this.prepareDateToExport(this.persondriverDatestart),
                dateend: this.prepareDateToExport(this.persondriverDateend),
                drivercategory: this.prepareNumToExport(this.persondriverDrivercategory),
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersondriver(person: Person, persondriver: Persondriver) {
        this.prepareToExport(person);
        fetch('/api/Persondriver', {
            method: 'post',
            body: JSON.stringify(persondriver),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersondriver(person: Person, persondriver: Persondriver) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        persondriver.id = -persondriver.id;
        fetch('/api/Persondriver', {
            method: 'post',
            body: JSON.stringify(persondriver),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getDrivercategory(drivercategoryid: number): string {
        if (drivercategoryid == null || drivercategoryid == 0) {
            return "";
        }
        let drivercategory: Drivercategory = this.drivercategories.find(t => t.id == drivercategoryid);
        if (drivercategory != null) {
            return drivercategory.name;
        } else {
            return "";
        }
    }

    getDrivertype(drivertypeid: number): string {
        if (drivertypeid == null || drivertypeid == 0) {
            return "";
        }
        let drivertype: Drivertype = this.drivertypes.find(t => t.id == drivertypeid);
        if (drivertype != null) {
            return drivertype.name;
        } else {
            return "";
        }
    }

    getPersondriverUpdateName(): string {
        if (this.persondriverMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersondriverUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersondriverButton(person: Person) {
        this.persondriverMenuvisible = true;
        this.persondriverMenuelement = null;
    }

    updatePersondriverButton(person: Person, persondriver: Persondriver) {

        this.persondriverMenuelement = persondriver;


        this.persondriverDrivertype = this.persondriverMenuelement.drivertype;
        this.persondriverSeries = this.persondriverMenuelement.series;
        this.persondriverNumber = this.persondriverMenuelement.number;
        this.persondriverDatestart = this.persondriverMenuelement.datestartString;
        this.persondriverDateend = this.persondriverMenuelement.dateendString;
        this.persondriverDrivercategory = this.persondriverMenuelement.drivercategory;

        this.persondriverMenuvisible = true;
    }

    completePersondriverButton(person: Person) {
        if (this.persondriverMenuelement == null) {
            this.addPersondriver();
        } else {

            this.persondriverMenuelement.drivertype = this.prepareNumToExport(this.persondriverDrivertype);
            this.persondriverMenuelement.series = this.persondriverSeries;
            this.persondriverMenuelement.number = this.persondriverNumber;
            this.persondriverMenuelement.datestartString = this.persondriverDatestart;
            this.persondriverMenuelement.dateendString = this.persondriverDateend;
            this.persondriverMenuelement.drivercategory = this.prepareNumToExport(this.persondriverDrivercategory);


            this.updatePersondriver(person, this.persondriverMenuelement);
        }

        this.persondriverMenuvisible = false;
    }

    addPersonpermission() {
        //this.prepareToExport(person);
        fetch('/api/Personpermission', {
            method: 'post',
            body: JSON.stringify(<Personpermission>{
                person: this.person.id,
                permissiontype: this.prepareNumToExport(this.personpermissionPermissiontype),
                datestart: this.prepareDateToExport(this.personpermissionDatestart),
                dateend: this.prepareDateToExport(this.personpermissionDateend),
                number: this.personpermissionNumber,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonpermission(person: Person, personpermission: Personpermission) {
        this.prepareToExport(person);
        fetch('/api/Personpermission', {
            method: 'post',
            body: JSON.stringify(personpermission),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonpermission(person: Person, personpermission: Personpermission) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personpermission.id = -personpermission.id;
        fetch('/api/Personpermission', {
            method: 'post',
            body: JSON.stringify(personpermission),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPermissiontype(permissiontypeid: number): string {
        if (permissiontypeid == null || permissiontypeid == 0) {
            return "";
        }
        let permissiontype: Permissiontype = this.permissiontypes.find(t => t.id == permissiontypeid);
        if (permissiontype != null) {
            return permissiontype.name;
        } else {
            return "";
        }
    }

    getPersonpermissionUpdateName(): string {
        if (this.personpermissionMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonpermissionUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonpermissionButton(person: Person) {
        this.personpermissionMenuvisible = true;
        this.personpermissionMenuelement = null;
    }

    updatePersonpermissionButton(person: Person, personpermission: Personpermission) {

        this.personpermissionMenuelement = personpermission;

        this.personpermissionPermissiontype = this.personpermissionMenuelement.permissiontype;
        this.personpermissionDatestart = this.personpermissionMenuelement.datestartString;
        this.personpermissionDateend = this.personpermissionMenuelement.dateendString;
        this.personpermissionNumber = this.personpermissionMenuelement.number;

        this.personpermissionMenuvisible = true;
    }

    completePersonpermissionButton(person: Person) {
        if (this.personpermissionMenuelement == null) {
            this.addPersonpermission();
        } else {

            this.personpermissionMenuelement.permissiontype = this.prepareNumToExport(this.personpermissionPermissiontype);
            this.personpermissionMenuelement.datestartString = this.personpermissionDatestart;
            this.personpermissionMenuelement.dateendString = this.personpermissionDateend;
            this.personpermissionMenuelement.number = this.personpermissionNumber;



            this.updatePersonpermission(person, this.personpermissionMenuelement);
        }

        this.personpermissionMenuvisible = false;
    }

    addPersonprivelege() {
        //this.prepareToExport(person);
        fetch('/api/Personprivelege', {
            method: 'post',
            body: JSON.stringify(<Personprivelege>{
                person: this.person.id,
                name: this.personprivelegeName,
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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonprivelege(person: Person, personprivelege: Personprivelege) {
        this.prepareToExport(person);
        fetch('/api/Personprivelege', {
            method: 'post',
            body: JSON.stringify(personprivelege),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonprivelege(person: Person, personprivelege: Personprivelege) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personprivelege.id = -personprivelege.id;
        fetch('/api/Personprivelege', {
            method: 'post',
            body: JSON.stringify(personprivelege),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersonprivelegeUpdateName(): string {
        if (this.personprivelegeMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonprivelegeUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonprivelegeButton(person: Person) {
        this.personprivelegeMenuvisible = true;
        this.personprivelegeMenuelement = null;
    }

    updatePersonprivelegeButton(person: Person, personprivelege: Personprivelege) {

        this.personprivelegeMenuelement = personprivelege;

        this.personprivelegeName = this.personprivelegeMenuelement.name;


        this.personprivelegeMenuvisible = true;
    }

    completePersonprivelegeButton(person: Person) {
        if (this.personprivelegeMenuelement == null) {
            this.addPersonprivelege();
        } else {

            this.personprivelegeMenuelement.name = this.personprivelegeName;



            this.updatePersonprivelege(person, this.personprivelegeMenuelement);
        }

        this.personprivelegeMenuvisible = false;
    }

    addPersondispanserization() {
        //this.prepareToExport(person);
        fetch('/api/Persondispanserization', {
            method: 'post',
            body: JSON.stringify(<Persondispanserization>{
                person: this.person.id,
                group: this.prepareNumToExport(this.persondispanserizationGroup),
                result: this.persondispanserizationResult,
                date: this.prepareDateToExport(this.persondispanserizationDate),

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersondispanserization(person: Person, persondispanserization: Persondispanserization) {
        this.prepareToExport(person);
        fetch('/api/Persondispanserization', {
            method: 'post',
            body: JSON.stringify(persondispanserization),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersondispanserization(person: Person, persondispanserization: Persondispanserization) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        persondispanserization.id = -persondispanserization.id;
        fetch('/api/Persondispanserization', {
            method: 'post',
            body: JSON.stringify(persondispanserization),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    } 

    getPersondispanserizationUpdateName(): string {
        if (this.persondispanserizationMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersondispanserizationUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersondispanserizationButton(person: Person) {
        this.persondispanserizationMenuvisible = true;
        this.persondispanserizationMenuelement = null;
    }

    updatePersondispanserizationButton(person: Person, persondispanserization: Persondispanserization) {

        this.persondispanserizationMenuelement = persondispanserization;

        this.persondispanserizationGroup = this.persondispanserizationMenuelement.group;
        this.persondispanserizationResult = this.persondispanserizationMenuelement.result;
        this.persondispanserizationDate = this.persondispanserizationMenuelement.dateString;

        this.persondispanserizationMenuvisible = true;
    }

    completePersondispanserizationButton(person: Person) {
        if (this.persondispanserizationMenuelement == null) {
            this.addPersondispanserization();
        } else {
            this.persondispanserizationMenuelement.group = this.prepareNumToExport(this.persondispanserizationGroup);
            this.persondispanserizationMenuelement.result = this.persondispanserizationResult;
            this.persondispanserizationMenuelement.dateString = this.persondispanserizationDate;


            this.updatePersondispanserization(person, this.persondispanserizationMenuelement);
        }

        this.persondispanserizationMenuvisible = false;
    }

    addPersonvvk() {
        //this.prepareToExport(person);
        fetch('/api/Personvvk', {
            method: 'post',
            body: JSON.stringify(<Personvvk>{
                person: this.person.id,
                number: this.personvvkNumber,
                result: this.personvvkResult,
                date: this.prepareDateToExport(this.personvvkDate),

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
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(this.person.id);
            });
    }

    updatePersonvvk(person: Person, personvvk: Personvvk) {
        this.prepareToExport(person);
        fetch('/api/Personvvk', {
            method: 'post',
            body: JSON.stringify(personvvk),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    deletePersonvvk(person: Person, personvvk: Personvvk) {
        let confirmaction: boolean = confirm("Вы уверены?");
        if (!confirmaction) {
            return;
        }
        personvvk.id = -personvvk.id;
        fetch('/api/Personvvk', {
            method: 'post',
            body: JSON.stringify(personvvk),
            credentials: 'include',
            headers: new Headers({
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            })
        })
            .then(response => { return response.json(); })
            .then((response) => {
                (<any>Vue).notify(response);
            })
            .then(x => {
                this.rerenderSearch();
                this.selectPerson(person.id);
            });
    }

    getPersonvvkUpdateName(): string {
        if (this.personvvkMenuelement == null) {
            return "Добавить";
        } else {
            return EDIT_LABEL;
        }
    }

    getPersonvvkUpdateButtonName(): string {
        return SAVE_LABEL;
    }

    addPersonvvkButton(person: Person) {
        this.personvvkMenuvisible = true;
        this.personvvkMenuelement = null;
    }

    updatePersonvvkButton(person: Person, personvvk: Personvvk) {

        this.personvvkMenuelement = personvvk;

        this.personvvkNumber = this.personvvkMenuelement.number;
        this.personvvkResult = this.personvvkMenuelement.result;
        this.personvvkDate = this.personvvkMenuelement.dateString;

        this.personvvkMenuvisible = true;
    }

    completePersonvvkButton(person: Person) {
        if (this.personvvkMenuelement == null) {
            this.addPersonvvk();
        } else {
            this.personvvkMenuelement.number = this.personvvkNumber;
            this.personvvkMenuelement.result = this.personvvkResult;
            this.personvvkMenuelement.dateString = this.personvvkDate;

            this.updatePersonvvk(person, this.personvvkMenuelement);
        }

        this.personvvkMenuvisible = false;
    }

    mainEducation() {
        this.personeducationMain = 1;
        this.personeducationUcp = 0;
    }

    secondaryEducation() {
        this.personeducationMain = 0;
        this.personeducationUcp = 0;
    }

    ucpEducation() {
        this.personeducationMain = 1;
        this.personeducationUcp = 1;
    }

    personotherDriver() {
        this.personotherMenu = 1;
    }

    personotherPermission() {
        this.personotherMenu = 2;
    }

    personotherPrivelege() {
        this.personotherMenu = 3;
    }

    personotherWound() {
        this.personotherMenu = 4;
    }

    personhealthIll() {
        this.personhealthMenu = 1;
    }

    personhealthDispanserization() {
        this.personhealthMenu = 2;
    }

    personhealthVvk() {
        this.personhealthMenu = 3;
    }

    personhealthPfl() {
        this.personhealthMenu = 4;
    }

    personjobJob() {
        this.personjobMenu = 1;
    }

    personjobPrivelege() {
        this.personjobMenu = 2;
    }

    personjobPension() {
        this.personjobMenu = 3;
    }

    personcontractContract() {
        this.personcontractMenu = 1;
    }

    personcontractCivil() {
        this.personcontractMenu = 2;
    }

    personcontractStatecivil() {
        this.personcontractMenu = 3;
    }

    /**
     * Убираем null значения у num и меняем их на 0
     * @param num
     */
    prepareNumToExport(num: number): number {
        if (num == null || num.toString().length == 0) {
            return 0;
        }
        return num;
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

    getIllcode(illcode: number): string {
        if (illcode == null || illcode == 0) {
            return "";
        }
        let illcodeObject: Illcode = this.illcodes.find(i => i.id == illcode);
        if (illcodeObject != null) {
            return illcodeObject.name;
        }
        return "";
    }

    getNormativ(normativ: number): string {
        if (normativ == null || normativ == 0) {
            return "";
        }
        let normativObject: Normativ = this.normativs.find(i => i.id == normativ);
        if (normativObject != null) {
            return normativObject.name;
        }
        return "";
    }

    getNumud(numud: string): string {
        if (numud == null || numud.length == 0) {
            return "";
        }
        let zeros: string = "";
        for (let i: number = 0; i < 5 - numud.length; i++) {
            zeros += "0";
        }
        return zeros + numud;
    }

    addPhysicalfield() {
        let physicalfield: Physicalfield = new Physicalfield();
        physicalfield.normativ = this.physicalfieldNormativ;
        physicalfield.result = this.physicalfieldResult;
        this.personphysicalPhysicalfields.push(physicalfield);
    }

    deletePhysicalfield(physicalfield: Physicalfield) {

        this.personphysicalPhysicalfields = this.personphysicalPhysicalfields.filter(v => v != physicalfield);
    }

    printServlist() {
        //this.modalAutobiographyVisible = true;
        //// ./css/styles.css
        //// printAutobiography
        //setTimeout(x => { printJS({ printable: 'printAutobiography', type: 'html' }) }, 1000);

        //printJS({ printable: 'printServlist', type: 'html', css: 'eld-print.css', targetStyles: ['*'] })
        
        //printJS({ printable: 'printServlist', type: 'html', css: '../../css/print.css', targetStyles: ['*'] })
        printJS({ printable: 'printServlist', type: 'html', css: 'print.css', targetStyles: ['*'] })
    }


    printEducation(person: Person): string {
        let str: string = "";
        person.personeducations.forEach(e => {
            str += e.name + " " + e.dateend + " " + e.speciality + " eol ";
        })
        return str;
    }

    printRankName(person: Person): string {
        let str: string = "";
        person.personranks.forEach(e => {
            str += e.rankstring + " eol ";
            //str += this.ranks.find(r => r.id == e.rank).name + " eol ";
        })
        return str;
    }

    printRankDatenum(person: Person): string {
        let str: string = "";
        person.personranks.forEach(e => {
            str += this.printDate(e.decreedate) + " " + e.decreenumber + " eol ";
        })
        return str;
    }


    printRank(person: Person, fontsize: number): string[] {
        let rankString: string[] = new Array();
        rankString.push(this.printRankName(person));
        rankString.push(this.printRankDatenum(person));
        let rowWidths: number[] = new Array();
        rowWidths.push(60);
        rowWidths.push(50);

        rankString = this.prepareTableRows(rankString, rowWidths, fontsize);
        return rankString;
    }

    printJobStart(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 1) { // Только работа. Служба сюда не вписывается
                str += this.printDate(e.start) + " eol ";
            }
            
        })
        return str;
    }

    printJobEnd(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 1) { // Только работа. Служба сюда не вписывается
                str += this.printDate(e.end) + " eol ";
            }
        })
        return str;
    }

    printJobPlace(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 1) { // Только работа. Служба сюда не вписывается
                str += e.jobplace + " eol ";
            }
        })
        return str;
    }

    printJobPosition(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 1) { // Только работа. Служба сюда не вписывается
                str += e.jobposition + " eol ";
            }
        })
        return str;
    }

    printJob(person: Person, fontsize: number): string[] {
        let jobString: string[] = new Array();
        jobString.push(this.printJobStart(person));
        jobString.push(this.printJobEnd(person));
        jobString.push(this.printJobPlace(person));
        jobString.push(this.printJobPosition(person));


        let rowWidths: number[] = new Array();
        rowWidths.push(20);
        rowWidths.push(20);
        rowWidths.push(120);
        rowWidths.push(50);
        jobString = this.prepareTableRows(jobString, rowWidths, fontsize);
        //rankString = this.prepareTableRows(rankString, rowWidths, fontsize);

        return jobString;
    }

    printServiceStart(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += this.printDate(e.start) + " eol ";
            }

        })
        return str;
    }

    printServiceEnd(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += this.printDate(e.end) + " eol ";
            }
        })
        return str;
    }

    printServicePosition(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += e.jobposition + " eol ";
            }

        })
        return str;
    }

    printServicePlace(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += e.serviceplace + " eol ";
            }

        })
        return str;
    }
    printServiceOrder(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += this.printDate(e.orderdate) + " " + e.ordernumber + " " + e.serviceorder + " eol ";
            }

        })
        return str;
    }

    printLanguages(person: Person): string {
        let str: string = "";
        person.personlanguages.forEach(e => {
            str += this.languagetypes.find(l => l.id == e.languagetype).name + ", ";
        })
        return str;
    }

    printSciences(person: Person): string {
        let str: string = "";
        person.personsciences.forEach(e => {
            str += this.sciences.find(l => l.id == e.sciencetype).name + " eol ";
        })
        return str;
    }

    printService(person: Person, fontsize: number): string[] {
        let serviceString: string[] = new Array();
        serviceString.push(this.printServiceStart(person));
        serviceString.push(this.printServiceEnd(person));
        serviceString.push(this.printServicePosition(person));
        serviceString.push(this.printServicePlace(person));
        serviceString.push(this.printServiceOrder(person));

        let rowWidths: number[] = new Array();
        // 20 - 20 - 50 - 80 40
        rowWidths.push(20);
        rowWidths.push(20);
        rowWidths.push(50);
        rowWidths.push(80);
        rowWidths.push(40);
        serviceString = this.prepareTableRows(serviceString, rowWidths, fontsize);
        //rankString = this.prepareTableRows(rankString, rowWidths, fontsize);

        return serviceString;
    }

    printServiceFeatureStart(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += this.printDate(e.start) + " eol ";
            }

        })
        return str;
    }

    printServiceFeatureOrder(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2 && e.servicetype == 2 && e.servicefeature == 2) { // Только контрактная служба . Работа не вписывается
                str += this.printDate(e.orderdate) + " " + e.ordernumber + " " + e.serviceorder + " eol ";
            }

        })
        return str;
    }

    printServiceFeatureEnd(person: Person): string {
        let str: string = "";
        person.personjobs.forEach(e => {
            if (e.jobtype == 2) { // Только служба. Работа не вписывается
                str += this.printDate(e.end) + " eol ";
            }
        })
        return str;
    }

    printServiceFeature(person: Person, fontsize: number): string[] {
        let serviceString: string[] = new Array();
        serviceString.push(this.printServiceStart(person));
        serviceString.push(this.printServiceEnd(person));
        serviceString.push(this.printServicePosition(person));
        serviceString.push(this.printServicePlace(person));
        serviceString.push(this.printServiceOrder(person));

        let rowWidths: number[] = new Array();
        // 20 - 20 - 50 - 80 40
        rowWidths.push(20);
        rowWidths.push(20);
        rowWidths.push(50);
        rowWidths.push(80);
        rowWidths.push(40);
        serviceString = this.prepareTableRows(serviceString, rowWidths, fontsize);
        //rankString = this.prepareTableRows(rankString, rowWidths, fontsize);

        return serviceString;
    }


    // 'eol' - служебное слово, помечающее, что конец строки. А если конец строки, то все
    printToRow(text: string, rownum: number, rowwidth: number, fontsize: number): string{
        let str: string = "";
        let lettersperrow: number = rowwidth / fontsize;
        let passedLetters: number = 0;
        let currentRow: number = 1;

        let words: string[] = text.split(' ');
        let acceptedWords: string[] = new Array();
        let end: boolean = false;

        words.forEach(w => {
            if (!end) {
                passedLetters = passedLetters + w.length;

                if ((passedLetters > lettersperrow || w.startsWith('eol')) && passedLetters > 0 && passedLetters != w.length) { // Превысили лимит
                    if (currentRow == rownum) { // достигли нужной строки
                        end = true;
                    } else { // не достигли нужной строки
                        passedLetters = 0; // обнуляем счетчик
                        passedLetters = passedLetters + w.length; // снова засовываем то что не вошло в него
                        currentRow += 1; // переходим на следующую строку.
                        if (currentRow == rownum && !w.startsWith('eol')) { // достигли нужной строки
                            acceptedWords.push(w);
                        }
                    }
                } else { // не превысили лимита
                    if (currentRow == rownum && !w.startsWith('eol')) { // достигли нужной строки
                        acceptedWords.push(w);
                    } else { // не достигли нужной строки

                    }
                }
            }
        })

        acceptedWords.forEach(aw => str = str + aw + " ");
        
        return str;
    }


    /**
     * Делит текст на строки. Не воспринимает конец строки.
     * @param text
     * @param rownum
     * @param rowwidth
     * @param fontsize
     */
    splitTextByRows(text: string, rowwidth: number, fontsize: number): string[] {
        let rows: string[] = new Array();
        let str: string = ""; // current row
        let lettersperrow: number = rowwidth / fontsize;
        let passedLetters: number = 0;

        let words: string[] = text.split(' ');
        let acceptedWords: string[] = new Array();

        words.forEach(w => {
            passedLetters = passedLetters + w.length;
            if (passedLetters > lettersperrow && passedLetters > 0 && passedLetters != w.length) { // Превысили лимит 
                passedLetters = 0; // обнуляем счетчик
                acceptedWords.forEach(aw => str = str + aw + " ");
                acceptedWords = new Array(); // очищаем слова на строке.
                rows.push(str); // засовываем заполненную строку.
                str = ""; // Очищаем строку

                passedLetters = passedLetters + w.length; // снова засовываем то что не вошло в него
                acceptedWords.push(w); // идет в новую строку
                    
            } else { // не превысили лимита
                acceptedWords.push(w);
            }
        })

        acceptedWords.forEach(aw => str = str + aw + " ");
        rows.push(str); // засовываем остатки строки.
        return rows;
    }

    /**
     * Принимается массив колонок с eol. Выдается массив колонок с eol, 
     * @param inputRows
     */
    prepareTableRows(inputRows: string[], rowwidth: number[], fontsize: number): string[] {
        let output: string[] = new Array();
        let tempArray: string[][] = new Array();
        let index: number = 0;
        inputRows.forEach(s => {
            let tempElement: string[] = s.split(' eol ');
            tempArray.push(tempElement);
            //tempArray[index] = 
            index++;
        })

        if (tempArray.length > 0) { // Если у массива есть хоть один столбец
            for (let n: number = 0; n < tempArray[0].length; n++) { // // смотрим строкам. 
                let maxRows: number = 0;

                for (let i: number = 0; i < tempArray.length; i++) { // смотрим по столбцам
                    let subrows: string[] = this.splitTextByRows(tempArray[i][n], rowwidth[i], fontsize);
                    let recordLength: number = subrows.length; // Получаем длину записи.
                    if (recordLength > maxRows) {
                        maxRows = recordLength; // Находим самую длинную запись , чтобы все записи с остальных столбцов, относящиеся к этой, подстроились под нее
                    }
                }

                for (let i: number = 0; i < tempArray.length; i++) { // смотрим по столбцам
                    let subrows: string[] = this.splitTextByRows(tempArray[i][n], rowwidth[i], fontsize);
                    let recordLength: number = subrows.length; // Получаем длину записи.
                    if (maxRows > recordLength) {
                        let difference: number = maxRows - recordLength;
                        for (let d: number = 0; d < difference; d++) {
                            tempArray[i][n] = tempArray[i][n] + " eol "; // Добавляем недостающую запись
                        }
                    }
                }
            }

            let str: string = "";
            for (let i: number = 0; i < tempArray.length; i++) {
                str = "";
                for (let n: number = 0; n < tempArray[i].length; n++) {
                    str = str + tempArray[i][n] + " eol ";
                }
                output.push(str);
            }
        }
        
        return output;
    }

    currentYearStart(): Date {
        return new Date(new Date().getFullYear(), 0, 1);
    }

    currentYearEnd(): Date {
        return new Date(new Date().getFullYear(), 11, 31);
    }

    getExperienceFullOld(days: number): string {

        let years: number = ~~(days / 365);
        days = days - (years * 365);
        let months: number = ~~(days / 31);
        days = days - (months * 31);

        let experienceString: string = "";
        if (years > 0) {
            experienceString += years + " " + this.getYearString(years) + " ";
        }
        if (months > 0) {
            experienceString += months + " " + this.getMonthString(months) + " ";
        }
        if (days > 0) {
            experienceString += days + " " + this.getDayString(days);
        }
        return experienceString;
        //return years + " лет, " + months + " месяцев, " + days + " дней."; 
    }

    getExperienceFull(years: number, months: number, days: number): string {


        let experienceString: string = "";
        if (years > 0) {
            experienceString += years + " " + this.getYearString(years) + " ";
        }
        if (months > 0) {
            experienceString += months + " " + this.getMonthString(months) + " ";
        }
        if (days > 0) {
            experienceString += days + " " + this.getDayString(days);
        }
        return experienceString;
        //return years + " лет, " + months + " месяцев, " + days + " дней."; 
    }

    getExperienceFullPension(years: number, months: number, days: number): string {
        let experienceString: string = "";

        if (years > 9) {
            experienceString += years;
        } else if (years > 0) {
            experienceString += "0" + years;
        } else {
            experienceString += "00";
        }
        experienceString += "-";

        if (months > 9) {
            experienceString += months;
        } else if (months > 0) {
            experienceString += "0" + months;
        } else {
            experienceString += "00";
        }

        experienceString += "-";

        if (days > 9) {
            experienceString += days;
        } else if (days > 0) {
            experienceString += "0" + days;
        } else {
            experienceString += "00";
        }

        let time_list = this.person.pension_A.split(' ');
        this.pension_A = (parseInt(time_list[0]) == 0 ? '' : (time_list[0] + ' ' + this.getYearString(parseInt(time_list[0])) + ' ')) +
            (parseInt(time_list[1]) == 0 ? '' : (time_list[1] + ' ' + this.getMonthString(parseInt(time_list[1])) + ' ')) +
            (parseInt(time_list[2]) == 0 ? '' : (time_list[2] + ' ' + this.getDayString(parseInt(time_list[2]))));//this.person.pension_A;
        time_list = this.person.pension_B.split(' ');
        this.pension_B = (parseInt(time_list[0]) == 0 ? '' : (time_list[0] + ' ' + this.getYearString(parseInt(time_list[0])) + ' ')) +
            (parseInt(time_list[1]) == 0 ? '' : (time_list[1] + ' ' + this.getMonthString(parseInt(time_list[1])) + ' ')) +
            (parseInt(time_list[2]) == 0 ? '' : (time_list[2] + ' ' + this.getDayString(parseInt(time_list[2]))));

        return experienceString;
    }

    calendar_diff(date_1: string, date_2: string): string {
        if (date_1.valueOf() < date_2.valueOf())
            return this.calendar_diff(date_2, date_1);
        var days: number = parseInt(date_1.split('T')[0].split('-')[2]) - parseInt(date_2.split('T')[0].split('-')[2]);
        let month: number = 0;
        if (days < 0) {
            days += 30;
            month -= 1;
        }
        month += parseInt(date_1.split('T')[0].split('-')[1]) - parseInt(date_2.split('T')[0].split('-')[1]);
        let year: number = 0;
        if (month < 0) {
            month += 12;
            year -= 1;
        }
        year += parseInt(date_1.split('T')[0].split('-')[0]) - parseInt(date_2.split('T')[0].split('-')[0]);
        return this.getExperienceFullPension(year, month, days);
    }

    getDataToString(data_period: string): string {
        let time_list = data_period.split(' ');
        return (parseInt(time_list[0]) == 0 ? '' : (time_list[0] + ' ' + this.getYearString(parseInt(time_list[0])) + ' ')) +
            (parseInt(time_list[1]) == 0 ? '' : (time_list[1] + ' ' + this.getMonthString(parseInt(time_list[1])) + ' ')) +
            (parseInt(time_list[2]) == 0 ? '' : (time_list[2] + ' ' + this.getDayString(parseInt(time_list[2]))));
    }

    getSeniorityMARK(type_a: string, type_b: string): string {
        let time_list = type_a.split(' ');
        let value_a = 30 * (12 * parseInt(time_list[0]) + parseInt(time_list[1])) + parseInt(time_list[2]);
        time_list = type_b.split(' ');
        let value_b = 30 * (12 * parseInt(time_list[0]) + parseInt(time_list[1])) + parseInt(time_list[2]);
        if (value_a > value_b) {
            return this.getDataToString(type_a);
        }
        return this.getDataToString(type_b);
    }

    differenceBetweenTwoDays(date1: Date, date2: Date): number {
        if (date1 == null || date2 == null) {
            return 0;
        }
        let date1Moment = moment(date1);
        let date2Moment = moment(date2);
        let diffDays: number = date2Moment.diff(date1Moment, "days");
        return diffDays;
    }

    differenceBetweenTwoDaysString(date1string: string, date2string: string): number {
        let date1: Date = new Date(date1string);
        let date2: Date = new Date(date2string);
        if (date1 == null || date2 == null) {
            return 0;
        }
        let date1Moment = moment(date1);
        let date2Moment = moment(date2);
        let diffDays: number = date2Moment.diff(date1Moment, "days");
        return diffDays;
    }

    getVacationdaysleft(person: Person): number {
        if (person != null && person.jobperiodcurrent != null) {
            return person.jobperiodcurrent.vacationdaysgiven - person.jobperiodcurrent.vacationdaysconsumed;
        }
        return 0;
    }

    selectPosition() {

        let appearance = {
            positioncompact: this.$store.state.positioncompact,
            sidebardisplay: 1,
        }

        this.$store.commit("setEldVisible", 0);
        this.$store.commit("setModeappointperson", true);
        

        this.$store.commit("updateUserAppearance", appearance);
    }

    appointPosition() {
        if (this.$store.state.modeappointedperson > 0) {

            fetch('/api/Personjob', {
                method: 'post',
                body: JSON.stringify(<Personjob>{
                    person: this.person.id,
                    positiontoselect: this.$store.state.modeappointedperson,

                }),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
                .then(response => { return response.json() as Promise<Personjob>; })
                .then((response) => {
                    this.personjobJobposition = response.jobposition;
                    this.personjobJobplace = response.jobplace;
                    this.personjobJobpositionplace = response.jobpositionplace;
                    //this.personjobServiceplace = response.serviceplace;
                    this.personjobPosition = response.position;
                    this.personjobServicetype = 2;
                    this.personjobServicecoef = 1;
                    this.personjobServicefeature = 1;
                    this.personjobMchs = true;
                    this.personjobJobtype = 2;
                    this.personjobManual = false;

                    if (this.personjobMenuelement != null) {
                        this.personjobMenuelement.jobposition = response.jobposition;
                        this.personjobMenuelement.jobplace = response.jobplace;
                        this.personjobMenuelement.jobpositionplace = response.jobpositionplace;
                        //this.personjobMenuelement.serviceplace = response.serviceplace;
                        this.personjobMenuelement.position = response.position;
                        this.personjobMenuelement.servicetype = 2;
                        this.personjobMenuelement.servicecoef = 1;
                        this.personjobMenuelement.servicefeature = 1;
                        this.personjobMenuelement.mchs = 1;
                        this.personjobMenuelement.manual = 0;
                        this.personjobMenuelement.jobtype = 2;
                    }
                    //alert(this.personjobPosition);
                })
                .then(x => {
                    this.rerenderSearch();
                    this.selectPerson(this.person.id);
                });

            this.$store.commit("setModeappointedperson", 0);
        }
    }

    manualJob() {
        if (!this.personjobManual) {
            this.personjobJobplace = "";
            this.personjobServiceplace = "";
            this.personjobServicetype = null;
            this.personjobServicecoef = null;
            this.personjobServicefeature = null;
            this.personjobMchs = false;
            this.personjobJobtype = null;
        }
        

        this.personjobPosition = 0;

        if (this.personjobMenuelement != null) {
            //this.personjobMenuelement.jobposition = response.jobposition;
            this.personjobMenuelement.jobplace = "";
            this.personjobMenuelement.serviceplace = "";
            this.personjobMenuelement.position = 0;
            this.personjobMenuelement.servicetype = 0;
            this.personjobMenuelement.servicecoef = 0;
            this.personjobMenuelement.servicefeature = 0;
            this.personjobMenuelement.mchs = 0;
            this.personjobMenuelement.jobtype = 0;
        }
    }

    personSurnameChange(person: Person) {
        if (person.surname.length < 3){
            return;
        }
        if (person.gender.startsWith('М')) { // Мужчина
            person.surname2 = person.surname + 'а';
            person.surname3 = person.surname + 'у';
            person.surname4 = person.surname + 'а';
            person.surname5 = person.surname + 'ым';
            person.surname6 = person.surname + 'е';
        } else { // Женщина
            person.surname2 = person.surname.slice(0, -1) + 'ой';
            person.surname3 = person.surname.slice(0, -1) + 'е';
            person.surname4 = person.surname.slice(0, -1) + 'у';
            person.surname5 = person.surname.slice(0, -1) + 'ой';
            person.surname6 = person.surname.slice(0, -1) + 'е';
        }
    }

    personNameChange(person: Person) {
        let containsName: boolean = false;
        
        let genderMale: boolean = true;
        if (!person.gender.startsWith('М')) { // Если пол НЕ начинается на слово М, то женщина 
            genderMale = false;
        }
        this.subjects.forEach(s => {
            if ((genderMale && s.category == 3) || (!genderMale && s.category == 5)) {
                if (s.name1 === person.name) {
                    containsName = true;
                    person.name2 = s.name2;
                    person.name3 = s.name3;
                    person.name4 = s.name4;
                    person.name5 = s.name5;
                    person.name6 = s.name6;
                }
            }
        })

        /**
         * Если имя нашлось в таблице падежей, то мы оканчиваем операцию и не пытаемся подобрать окончания автоматически 
         */
        if (containsName) {
            return;
        }
        if (person.name.length < 3) {
            return;
        }
        if (genderMale) { // Мужчина
            person.name2 = person.name + 'а';
            person.name3 = person.name + 'у';
            person.name4 = person.name + 'а';
            person.name5 = person.name + 'ом';
            person.name6 = person.name + 'е';
        } else { // Женщина
            person.name2 = person.name.slice(0, -1) + 'ой';
            person.name3 = person.name.slice(0, -1) + 'е';
            person.name4 = person.name.slice(0, -1) + 'у';
            person.name5 = person.name.slice(0, -1) + 'ой';
            person.name6 = person.name.slice(0, -1) + 'е';
        }
    }

    personFathernameChange(person: Person) {
        let containsName: boolean = false;

        let genderMale: boolean = true;
        if (!person.gender.startsWith('М')) { // Если пол НЕ начинается на слово М, то женщина 
            genderMale = false;
        }
        this.subjects.forEach(s => {
            if ((genderMale && s.category == 4) || (!genderMale && s.category == 6)) {
                if (s.name1 === person.fathername) {
                    containsName = true;
                    person.fathername2 = s.name2;
                    person.fathername3 = s.name3;
                    person.fathername4 = s.name4;
                    person.fathername5 = s.name5;
                    person.fathername6 = s.name6;
                }
            }
        })

        /**
         * Если имя нашлось в таблице падежей, то мы оканчиваем операцию и не пытаемся подобрать окончания автоматически 
         */
        if (containsName) {
            return;
        }
        if (person.name.length < 3) {
            return;
        }
        if (person.gender.startsWith('М')) { // Мужчина
            person.fathername2 = person.fathername + 'а';
            person.fathername3 = person.fathername + 'у';
            person.fathername4 = person.fathername + 'а';
            person.fathername5 = person.fathername + 'ем';
            person.fathername6 = person.fathername + 'е';
        } else { // Женщина
            person.fathername2 = person.fathername.slice(0, -1) + 'ой';
            person.fathername3 = person.fathername.slice(0, -1) + 'е';
            person.fathername4 = person.fathername.slice(0, -1) + 'у';
            person.fathername5 = person.fathername.slice(0, -1) + 'ой';
            person.fathername6 = person.fathername.slice(0, -1) + 'е';
        }
    }

    fetchStructureRewards() {
        fetch('api/DetailedStructure/Rewards', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                this.structuresReward = data;
            });
    }

    fetchStructureRewardsAllowed() {
        fetch('api/DetailedStructure/Rewardsallowed', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                //alert(JSON.stringify(data));
                this.structuresRewardAllowedToSelect = data;
            });
    }

    fetchStructureElders() {
        fetch('api/DetailedStructure/Elders', { credentials: 'include' })
            .then(response => response.json() as Promise<Structure[]>)
            .then(data => {
                //alert(JSON.stringify(data));
                this.structuresElders = data;
            });
    }

    getRewardmoneytype(rewardmoneyid: number): string {
        if (rewardmoneyid == null || rewardmoneyid == 0) {
            return "";
        }
        let rewardmoney: Rewardmoney = this.rewardmoneys.find(t => t.id == rewardmoneyid);
        if (rewardmoney != null) {
            return rewardmoney.rewardmoneytype;
        } else {
            return "";
        }
    }

    getRewardmoney(rewardmoneyid: number): string {
        if (rewardmoneyid == null || rewardmoneyid == 0) {
            return "";
        }
        let rewardmoney: Rewardmoney = this.rewardmoneys.find(t => t.id == rewardmoneyid);
        if (rewardmoney != null) {
            return rewardmoney.name;
        } else {
            return "";
        }
    }

    getStructureName(structureid: number): string {
        if (structureid == null || structureid == 0) {
            return "";
        }
        let structure: Structure = this.structuresReward.find(t => t.id == structureid);
        if (structure != null) {
            return structure.name1;
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

    /**
     * Определяем, выводить день, дня или дней
     * @param count
     */
    getDayString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "дней";
        } else if (lastLetter == 1) {
            return "день";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "дня";
        } else {
            return "дней";
        }
    }

    /**
     * Определяем, выводить месяц, месяца или месяцев
     * @param count
     */
    getMonthString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "месяцев";
        } else if (lastLetter == 1) {
            return "месяц";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "месяца";
        } else {
            return "месяцев";
        }
    }

    /**
     * Определяем, выводить год, года или лет
     * @param count
     */
    getYearString(count: number): string {
        let lastLetter: number = Number.parseInt(count.toString().slice(-1));
        if (count > 10 && count < 21) {
            return "лет";
        } else if (lastLetter == 1) {
            return "год";
        } else if (lastLetter > 1 && lastLetter < 5) {
            return "года";
        } else {
            return "лет";
        }
    }

    externalorderwhotypeSearch(queryString, cb) {
        var links = this.ExternalorderwhotypesToArrayFilterable();
        var results = queryString ? links.filter(this.createExternalorderwhotypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createExternalorderwhotypeFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    ExternalorderwhotypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.externalorderwhotypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    rankSearch(queryString, cb) {
        var links = this.RanksToArrayFilterable();
        var results = queryString ? links.filter(this.createRankFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createRankFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    RanksToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.ranks.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    streettypeSearch(queryString, cb) {
        var links = this.StreettypesToArrayFilterable();
        var results = queryString ? links.filter(this.createStreettypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createStreettypeFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    StreettypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.streettypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    citytypeSearch(queryString, cb) {
        var links = this.CitytypesToArrayFilterable();
        var results = queryString ? links.filter(this.createCitytypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createCitytypeFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    CitytypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.citytypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    personrelativeliveareaSearch(queryString, cb) {
        var links = this.PersonrelativeLiveAreasToArrayFilterable();
        var results = queryString ? links.filter(this.createPersonrelativeLiveAreaFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createPersonrelativeLiveAreaFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    PersonrelativeLiveAreasToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.areas.forEach(o => {
            if (this.person != null) {
                let selectedRegion: Region = this.regions.find(r => r.name == this.personrelativeLivestate);
                if (selectedRegion != null) {
                    if (o.region == selectedRegion.id) {
                        let link: Link = new Link();
                        link.value = o.name;
                        links.push(link);
                    }
                }
            }
        })
        return links;
    }

    personrelativedeathareaSearch(queryString, cb) {
        var links = this.PersonrelativeDeathAreasToArrayFilterable();
        var results = queryString ? links.filter(this.createPersonrelativeDeathAreaFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createPersonrelativeDeathAreaFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    PersonrelativeDeathAreasToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.areas.forEach(o => {
            if (this.person != null) {
                let selectedRegion: Region = this.regions.find(r => r.name == this.personrelativeDeathstate);
                if (selectedRegion != null) {
                    if (o.region == selectedRegion.id) {
                        let link: Link = new Link();
                        link.value = o.name;
                        links.push(link);
                    }
                }
            }
        })
        return links;
    }


    personrelativebirthareaSearch(queryString, cb) {
        var links = this.PersonrelativeBirthAreasToArrayFilterable();
        var results = queryString ? links.filter(this.createPersonrelativeBirthAreaFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createPersonrelativeBirthAreaFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    PersonrelativeBirthAreasToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.areas.forEach(o => {
            if (this.person != null) {
                let selectedRegion: Region = this.regions.find(r => r.name == this.personrelativeBirthstate);
                if (selectedRegion != null) {
                    if (o.region == selectedRegion.id) {
                        let link: Link = new Link();
                        link.value = o.name;
                        links.push(link);
                    }
                }
            }
        })
        return links;
    }

    birthareaSearch(queryString, cb) {
        //var links = this.ordernumbertypes;
        var links = this.BirthAreasToArrayFilterable();
        var results = queryString ? links.filter(this.createBirthAreaFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createBirthAreaFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    BirthAreasToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.areas.forEach(o => {
            if (this.person != null) {
                let selectedRegion: Region = this.regions.find(r => r.name == this.person.birthstate);
                if (selectedRegion != null) {
                    if (o.region == selectedRegion.id) {
                        let link: Link = new Link();
                        link.value = o.name;
                        links.push(link);
                    }
                }
            }

            
        })
        return links;
    }

    regionSearch(queryString, cb) {
        //var links = this.ordernumbertypes;
        var links = this.RegionsToArrayFilterable();
        var results = queryString ? links.filter(this.createRegionFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createRegionFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    RegionsToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.regions.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    countrySearch(queryString, cb) {
        //var links = this.ordernumbertypes;
        var links = this.CountriesToArrayFilterable();
        var results = queryString ? links.filter(this.createCountryFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createCountryFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    CountriesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.countries.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    servicetypeSearch(queryString, cb) {
        //var links = this.ordernumbertypes;
        var links = this.ServicetypesToArrayFilterable();
        var results = queryString ? links.filter(this.createServicetypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createServicetypeFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
    }

    ServicetypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.servicetypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }

    ordernumbertypeSearch(queryString, cb) {
        //var links = this.ordernumbertypes;
        var links = this.OrdernumbertypesToArrayFilterable();
        var results = queryString ? links.filter(this.createNumbertypeFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createNumbertypeFilter(queryString) {
        return (link) => {
            return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
        };
    }

    OrdernumbertypesToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.ordernumbertypes.forEach(o => {
            let link: Link = new Link();
            link.value = o.name;
            links.push(link);
        })
        return links;
    }


    structuresalldocumentSearch(queryString, cb) {
        var links = this.StructuresalldocumentToArrayFilterable();
        var results = queryString ? links.filter(this.createStructuresalldocumentFilter(queryString)) : links;
        // call callback function to return suggestions
        cb(results);
    }

    createStructuresalldocumentFilter(queryString) {
        return (link) => {
            //return (link.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0);
            return (link.value.toLowerCase().includes(queryString.toLowerCase()));
        };
    }

    StructuresalldocumentToArrayFilterable(): Link[] {
        let links: Link[] = new Array();
        this.structuresalldocument.forEach(o => {
            let link: Link = new Link();
            link.value = o; 
            links.push(link);
        })
        return links;
    }

    numpersonalchange(event: Event, person: Person) {
        let input = event.target as any;
        //alert(person.numpersonal);
        if (person.numpersonal == null) {
            return;
        }
        person.numpersonal = person.numpersonal.toUpperCase();
        if (person.numpersonal.length == 1) {
            person.numpersonal += "-";
        }
        if (person.numpersonal.length == 2) {
            person.numpersonal = person.numpersonal.substring(0, 1) + "-";
        }
        if (person.numpersonal.length > 2) {
            person.numpersonal = person.numpersonal.substring(0, 1) + "-" + person.numpersonal.substring(2);
        }

        if (person.numpersonal.length > 0 && !this.isLetterCyrillic(person.numpersonal[0])) {
            person.numpersonal = person.numpersonal.substring(0, 0);
        }
        if (person.numpersonal.length > 2 && !this.isNum(person.numpersonal[2])) {
            person.numpersonal = person.numpersonal.substring(0, 2);
        }
        if (person.numpersonal.length > 3 && !this.isNum(person.numpersonal[3])) {
            person.numpersonal = person.numpersonal.substring(0, 3);
        }
        if (person.numpersonal.length > 4 && !this.isNum(person.numpersonal[4])) {
            person.numpersonal = person.numpersonal.substring(0, 4);
        }
        if (person.numpersonal.length > 5 && !this.isNum(person.numpersonal[5])) {
            person.numpersonal = person.numpersonal.substring(0, 5);
        }
        if (person.numpersonal.length > 6 && !this.isNum(person.numpersonal[6])) {
            person.numpersonal = person.numpersonal.substring(0, 6);
        }
        //alert(input.value);
        input.value = person.numpersonal;

        //persondecreeblock.optionnumber1 = 5;
    }

    StatecivilSelectButton() {
        this.personcontractStatecivilSelect = !this.personcontractStatecivilSelect;
    }

    StatecivilAddButton(person: Person) {
        //this.prep
        //this.person.personjobs.forEach(p => {
        //    if (p.statecivilBool )
        //})
        this.prepareToExport(person);
        let count: number = this.person.personjobs.length;
        let index: number = 0;
        this.person.personjobs.forEach(p => {
            
            fetch('/api/Personjob', {
                method: 'post',
                body: JSON.stringify(p),
                credentials: 'include',
                headers: new Headers({
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                })
            })
                .then(response => { return response.json(); })
                .then((response) => {
                    index++;
                    if (index == count) {
                        (<any>Vue).notify(response);
                    }
                    
                })
                .then(x => {
                    if (index == count) {
                        this.rerenderSearch();
                        this.selectPerson(person.id);
                        this.personcontractStatecivilSelect = false;
                    }
                    
                });
        })

    }

    StatecivilRemoveButton(person: Person, personjob: Personjob) {
        personjob.statecivilBool = false;
        personjob.statecivilstartString = "";
        personjob.statecivilendString = "";
        this.updatePersonjob(person, personjob);
    }

    birthadditionaldisplayClick(person: Person) {
        person.birthadditionaldisplay = true;
        person.birthadditional = "";
    }

    /**
     * Автоматически обновляем данные (пока что только в паспорте) каждые N секунд, чтобы они не потерялись. 
     * 
     */
    autoupdatePerson() {
        if (this.person != null) {
            this.saveChanges(this.person, true);
        }
    }

    onPersonrewardRewardtypeChange() {
        this.personrewardReward = null;
        this.personrewardReason = "";
        //this.personrewardOrderwhoid = null;
        this.personrewardOrder = "";
        this.personrewardOrdernumbertype = "";
        this.personrewardOrderwho = null;
        this.personrewardDate = "";
        this.personrewardOptionnumber1 = null;
        this.personrewardOptionnumber2 = null;
        this.personrewardOptionstring1 = "";
        this.personrewardOptionstring2 = "";
        this.personrewardArea = null;
        this.personrewardAreaother = null;
        this.personrewardAreaotherdisplay = false;
        this.personrewardExternalorderwhotype = "";
        this.personrewardExternalordertype = null;
    }

    onPersonrewardAreaChange() {
        if (this.personrewardArea == null || this.personrewardArea == 0) {
            this.personrewardAreaother = null;
            this.personrewardAreaotherdisplay = false;
            return;
        }
        let areaObject: Area = this.areas.find(a => a.id == this.personrewardArea);
        if (areaObject != null && areaObject.other == 1) {
            this.personrewardAreaotherdisplay = true;
        } else {
            this.personrewardAreaother = null;
            this.personrewardAreaotherdisplay = false;
        }
    }

    isMaternity(vacationid: number): boolean {
        if (vacationid == null || vacationid == 0) {
            return false;
        }
        let vacationtype: Vacationtype = this.getVacationtypeObject(vacationid);
        if (vacationtype == null) {
            return false;
        }
        if (vacationtype.maternity == 1) {
            return true;
        }
    }

    isBelarusCapital(region: string): boolean {
        return region == "г. Минск";
        //if (region == null || region.length == 0) {
        //    return false;
        //}
        //this.regions.forEach(c => {
            
        //    if (c.city > 0 && c.name == region) {
        //        alert('Minsk!');
        //        return true;
        //    }
        //});
        //return false;
    }

    isBelarusCapitalNum(regionid: number): boolean {
        let regionObj = this.getRegionObject(regionid);
        if (regionObj == null) {
            return false;
        } else {
            if (regionObj.city > 0) {
                return true;
            } else {
                return false;
            }
        }
    }

    getEducationtypeObject(educationtype: number): Educationtype {
        if (educationtype == null || educationtype == 0) {
            return null;
        }
        let element: Educationtype = this.educationtypes.find(t => t.id == educationtype);
        if (element != null) {
            return element;
        } else {
            return null;
        }
    }



    getEducationpositiontypeObject(educationpositiontype: number): Educationpositiontype {
        if (educationpositiontype == null || educationpositiontype == 0) {
            return null;
        }
        let element: Educationpositiontype = this.educationpositiontypes.find(t => t.id == educationpositiontype);
        if (element != null) {
            return element; 
        } else {
            return null;
        }
    } 

    /**
     * Если должность при обучении курсант, то может обучаться только на дневном
     * @param educationtype
     * @param educationpositiontype
     */
    cadetpositiontypeforbidden(educationtype: Educationtype, educationpositiontype: Educationpositiontype) {
        if (educationpositiontype.fulltimeonly > 0 && educationtype.id > 1) {
            return true;
        }
        return false;
    }

    /**
     * Период учебы является службой можно ставить только когда должность курсант.
     * @param educationperiod
     * @param educationpositiontype
     */
    serviceperiodforbidden(educationperiod: Educationperiod, educationpositiontype: Educationpositiontype) {
        if (educationperiod.serviceBool && (educationpositiontype.id == null || educationpositiontype.id != 1)) {
            return true;
        }
        return false;
    }

    forceUpdate() {
        this.$forceUpdate();
    }

    /**
     * В образовании в общей вкладке может быть в одном образовании несколько периодов с разными формами обучения, но нам надо выводить только последнюю
     * @param educationperiods
     */
    getEducationtypeOfParts(personeducationparts: Personeducationpart[]): string {
        let educationperiodname: string = "";
        let latestDate: Date = null;
        personeducationparts.forEach(e => {
            if (e.educationtypeblock != null) {
                if (latestDate == null) {
                    latestDate = e.start;
                }
                if (e.start != null && e.start >= latestDate) {
                    
                    latestDate = e.end;
                    educationperiodname = this.getEducationtype(e.educationtypeblock.educationtype);
                }
                

            }
        })
        return educationperiodname;
    }

    /**
     * В образовании в общей вкладке может быть в одном образовании несколько периодов с разными должностями, но нам надо выводить только последнюю
     * @param educationperiods
     */
    getEducationpositionOfParts(personeducationparts: Personeducationpart[]): string {
        let educationpositionname: string = "";
        let latestDate: Date = null;
        personeducationparts.forEach(e => {
            //alert(e.educationperiod);
            if (e.educationperiod != null) {
                if (latestDate == null) {
                    latestDate = e.start;
                }
                if (e.start != null && e.start >= latestDate) {

                    latestDate = e.end;
                    //educationperiodname = this.getEducationtype(e.educationtypeblock.educationtype);
                    educationpositionname = this.getEducationpositiontype(e.educationperiod.educationpositiontype);
                    
                }


            }
        })
        return educationpositionname;
    }

    onPersoneducationName2Input() {
        this.personeducationInterruptorderwho = this.personeducationName2;
        this.personeducationAcademicvacations.forEach(av => {
            av.orderwho = this.personeducationName2;
        })
        this.personeducationEducationmaternities.forEach(em => {
            em.orderwho = this.personeducationName2;
        })
    }

    getPersonjobById(jobid: number): Personjob {
        if (this.person == null || this.person.personjobs == null) {
            return null;
        }
        let job: Personjob = null;
        this.person.personjobs.forEach(p => {
            if (p.id == jobid) {
                job = p;
            }
        })
        return job;
    }

    printPersonjobJobtype(personjob: Personjob): string {
        if (personjob == null) {
            return "";
        }

        if (personjob.jobtype != null && (personjob.jobtype == 1 || personjob.jobtype == 3)) {
            return this.getJobtype(personjob.jobtype);
        } else if (personjob.jobtype != null && personjob.jobtype == 2) {
            if (personjob.servicetypestr != null && personjob.servicetypestr.length > 0) {
                return "служба в " + personjob.servicetypestr;
            } else {
                return "служба";
            }
        }
        if (personjob.jobtype == 4){
            return "учёба";
        }
    }

    printPersonjobPositionplace(personjob: Personjob): string {
        if (personjob == null) {
            return "";
        }
        if (personjob.manual == 1) {
            return personjob.jobposition + " " + personjob.jobplace;
        } else {
            return personjob.jobpositionplace
        }
    }

    loadPeopleWhithoutJobPlace() {
        let devizor: number = 3;
        let output: (Person[])[] = [];
        let time_list: Person[] = [];
        fetch('api/Person/WithoutJobPlace/' + devizor, { credentials: 'include' })
            .then(response => {
                return response.json() as Promise<Person[]>;
            })
            .then(result => {
                let count: number = 0
                while (count < 1 + result.length / devizor) {
                    let j: number = 0;
                    time_list = []
                    while ((j < devizor) && (j + count < result.length)) {
                        time_list.push(result[j + count]);
                        j++;
                    }
                    output.push(time_list);
                    count += devizor;
                }
                this.peoplewhothoutjobplace = output;
            })
    }
}