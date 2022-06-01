import Personrank from '../classes/personrank';
import Personcontract from './personcontract';
import Personrelative from './personrelative';
import Personattestation from './personattestation';
import Personvacation from './personvacation';
import Personlanguage from './personlanguage';
import Personjob from './personjob';
import Personpenalty from './personpenalty';
import Personworktrip from './personworktrip';
import Personelection from './personelection';
import Personscience from './personscience';
import Personreward from './personreward';
import Personill from './personill';
import Personeducation from './personeducation';
import Personphysical from './personphysical';
import Persondriver from './persondriver';
import Personpermission from './personpermission';
import Personprivelege from './personprivelege';
import Persondispanserization from './persondispanserization';
import Personvvk from './personvvk';
import Personjobprivelege from './personjobprivelege';
import Jobperiod from './jobperiod';
import Rank from './rank';
import Pension from './pension';

export default class Person {
    id: number;
    position: number;
    structure: number;
    surname: string;
    name: string;
    fathername: string;
    birthdate: Date;
    photo: number;
    gender: string;
    passportid: string;
    passportnum: string;
    passportdatestart: Date;
    passportdateend: Date;
    birthlocation: string;
    registercountry: string;
    registerstate: string;
    registersubstate: string;
    registercitysubstate: number;
    registercitytype: string;
    registercity: string;
    registerstreettype: string;
    registerstreet: string;
    registerhouse: string;
    registerhousing: string;
    registerflat: string;
    registeradditional: string;
    livecountry: string;
    livestate: string;
    livesubstate: string;
    livecitysubstate: number;
    livecitytype: string;
    livecity: string;
    livestreettype: string;
    livestreet: string;
    livehouse: string;
    livehousing: string;
    liveflat: string;
    liveadditional: string;
    maritalstatus: string;
    nationality: string;
    science: string;
    numpersonal: string;
    wound: string;
    sciencerank: string;
    surnameother: string;
    surname2: string;
    name2: string;
    fathername2: string;
    surname3: string;
    name3: string;
    fathername3: string;
    surname4: string;
    name4: string;
    fathername4: string;
    surname5: string;
    name5: string;
    fathername5: string;
    surname6: string;
    name6: string;
    fathername6: string;
    removed: number;
    registerstatenum: number;
    registersubstatenum: number;
    livestatenum: number;
    livesubstatenum: number;
    birthcountry: string;
    birthstate: string;
    birthsubstate: string;
    birthcitysubstate: number;
    birthcitytype: string;
    birthcity: string;
    birthadditional: string;
    birthadditionaldisplay: boolean; // отображать ли текстовое поле "край, станица" и др.
    age: string;
    namesubject: number;
    fathernamesubject: number;
    surnamesubject: number;
    gendersubject: number;
    fullbirthlocation: string;
    language: string;
    phonenumber: string;
    citizenship: string;
    issuedby: string;
    sportsmanship: string;

    // Краткое наименование подразделения сотрудника - например, управление кадров
    structurename: string;
    structurename1: string;
    structurename2: string;
    structurename3: string;
    structurename4: string;
    structurename5: string;
    structurename6: string;
    // Полное наименование подразделения сотрудника - например, управление кадров Министерства по чрезвычайным ситуациям
    structuretree: string;
    structuretree1: string;
    structuretree2: string;
    structuretree3: string;
    structuretree4: string;
    structuretree5: string;
    structuretree6: string;
    // Полное наименование должности сотрудника - например, главный специалист отдела организационно-штатной работы управления кадров Министерства по чрезвычайным ситуациям
    positiontree: string;
    positiontree1: string;
    positiontree2: string;
    positiontree3: string;
    positiontree4: string;
    positiontree5: string;
    positiontree6: string;
    // Наименование должности сотрудника - например, главный специалист
    positiontypestring: string;
    positiontype1string: string;
    positiontype2string: string; // Родительный падеж
    positiontype3string: string; // Дательный падеж
    positiontype4string: string;
    positiontype5string: string;
    positiontype6string: string;

    birthdateString: string;
    passportdatestartString: string;
    passportdateendString: string;

    // Здесь хранится вычисляемая инфа. То есть информация, не хранящая в базе данных, но вычисляющаяся динамически на ее основе
    experience: number; // Стаж 
    experienceDays: number; // выслуга в днях за вычетом лет и месяцев на текущий период
    experienceMonths: number; // выслуга в месяцах за вычетом лет на текущий период
    experienceYears: number; // выслуга в годах на текущий период
    experienceprivelege: number; // Суммарное количество льготных дней
    vacationdayscurrentyear: number; // Для военных лиц, сколько дней отпуска выделено в этом году, с учетом возможных льготных условий
    vacationdaysleft: number; // Для военных лиц, сколько дней отпуска осталось с прошлого года
    vacationdaysused: number; // Сколько в этом году дней отпуска уже использовано из положенного
    vacationshiftdate: Date; // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату
    vacationshiftbefore: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения.
    vacationshiftafter: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения.

    privelegesmissed: number; // Сколько дней/месяцев/лет льготной выслуги потеряно из-за отпусков и больничных
    military: boolean; // Высчитывает, является ли человек военным или нет
    jobstart: Date; // С какой даты вообще считать, откуда могли пойти первые переносы отпуска. Если гражданский, то еще считать откуда начинается рабочий год.

    servicestart: Date;

    servicestartString: string;
    actualRank: Rank;
    actualRankExperience: number; // стаж в днях на последней занимаемой должности.
    major: number; // Может ли капитан быть повышен до майора при увольнении.
    stateserviceyears: number; // Выслуга лет госслужащим за все периоды работы
    stateservicemonths: number; // Выслуга месяцев госслужащим за все периоды работы
    stateservicedays: number; // Выслуга дней госслужащим за все периоды работы
    pensioncivilyears: number; 
    pensioncivilmonths: number;
    pensioncivildays: number;
    pensionmilitaryyears: number;
    pensionmilitarymonths: number;
    pensionmilitarydays: number;

    personranks: Personrank[];
    personcontracts: Personcontract[];
    personrelatives: Personrelative[];
    personattestations: Personattestation[];
    personvacations: Personvacation[];
    personlanguages: Personlanguage[];
    personjobs: Personjob[];
    personpenalties: Personpenalty[];
    personworktrips: Personworktrip[];
    personelections: Personelection[];
    personsciences: Personscience[];
    personrewards: Personreward[];
    personills: Personill[];
    personeducations: Personeducation[];
    personphysicals: Personphysical[];
    persondrivers: Persondriver[];
    personpermissions: Personpermission[];
    personpriveleges: Personprivelege[];
    persondispanserizations: Persondispanserization[];
    personvvks: Personvvk[];
    personjobpriveleges: Personjobprivelege[];
    jobperiods: Jobperiod[];
    pensions: Pension[];

    jobperiodcurrent: Jobperiod;
    jobperiodprevious: Jobperiod;

    pension_A: string;
    pension_B: string;

    appending_days: number;

    pension_A_with: string;
    pension_B_with: string;

    toDateInputValue(date: Date): string {
        var local = new Date(date);
        local.setMinutes(date.getMinutes() - date.getTimezoneOffset());
        return local.toJSON().slice(0, 10);
    }

}