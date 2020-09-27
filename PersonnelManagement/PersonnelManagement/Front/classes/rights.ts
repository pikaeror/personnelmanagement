import Rightsstructure from './rightsstructure';

export default class Rights {
    id: number;
    user: number;
    position: number;
    role: number;
    admin: number;
    orgedit: number;
    orgread: number;
    orgreadall: number;
    peopleedit: number;
    peopleread: number;
    peoplereadall: number;
    candidateedit: number;
    candidateblock: number;
    candidateread: number;
    peopleorgread: number;
    peopleorgreadall: number;
    peopledecreeread: number;
    peopledecreeedit: number;
    peopleeditmain: number;
    peoplereadmain: number;
    peopleeditpassport: number;
    peoplereadpassport: number;
    peopleeditphoto: number;
    peoplereadphoto: number;
    peopleeditcertificate: number;
    peoplereadcertificate: number;
    peopleeditrelative: number;
    peoplereadrelative: number;
    peopleediteducation: number;
    peoplereadeducation: number;
    peopleediteducationucp: number;
    peoplereadeducationucp: number;
    peopleeditjob: number;
    peoplereadjob: number;
    peopleeditjobprivelege: number;
    peoplereadjobprivelege: number;
    peopleeditjobpension: number;
    peoplereadjobpension: number;
    peopleeditill: number;
    peoplereadill: number;
    peopleeditdispanserization: number;
    peoplereaddispanserization: number;
    peopleeditvvk: number;
    peoplereadvvk: number;
    peopleeditpfl: number;
    peoplereadpfl: number;
    peopleeditrank: number;
    peoplereadrank: number;
    peopleeditcontract: number;
    peoplereadcontract: number;
    peopleeditcontractvacation: number;
    peoplereadcontractvacation: number;
    peopleeditcontractstate: number;
    peoplereadcontractstate: number;
    peopleeditvacation: number;
    peoplereadvacation: number;
    peopleeditreward: number;
    peoplereadreward: number;
    peopleeditattestation: number;
    peoplereadattestation: number;
    peopleeditlanguage: number;
    peoplereadlanguage: number;
    peopleeditscience: number;
    peoplereadscience: number;
    peopleeditelection: number;
    peoplereadelection: number;
    peopleeditworktrip: number;
    peoplereadworktrip: number;
    peopleeditpenalty: number;
    peoplereadpenalty: number;
    peopleeditphysical: number;
    peoplereadphysical: number;
    peopleeditdriver: number;
    peoplereaddriver: number;
    peopleeditpermission: number;
    peoplereadpermission: number;
    peopleeditprivelege: number;
    peoplereadprivelege: number;
    peopleeditwound: number;
    peoplereadwound: number;

    rightsstructures: Rightsstructure[];

    menu: number = 1; // ID открытого меню (раздела) в панеле редактирования и добавления пользователей, так как права разбиваем по группам в отдельных разделах


    //constructor(menu?: number) {
    //    if (menu == null) {
    //        menu = 1;
    //    }
    //    this.menu = menu;
    //}
}