import Educationtypeblock from './educationtypeblock';
import Academicvacation from './academicvacation';
import Educationmaternity from './educationmaternity';
import Personeducationpart from './personeducationpart';

export default class Personeducation {
    id: number;
    person: number;
    main: number;
    educationlevel: number;
    educationstage: number;
    name: string;
    name2: string;
    name3: string;
    location: string;
    city: string;
    faculty: string;
    educationtype: number;
    datestart: number;
    dateend: number;
    speciality: string;
    documentseries: string;
    documentnumber: string;
    cadet: number;
    qualification: string;
    start: Date;
    end: Date;
    interrupted: number;
    interruptorderdate: Date;
    interruptorderwho: string;
    interruptordernumber: string;
    interruptordernumbertype: string;
    interruptorderreason: string;
    educationdocument: number;
    ordernumber: string;
    ordernumbertype: string;
    orderdate: Date;
    orderwho: string;
    orderwhoid: number;
    orderid: number;
    nameasjobfull: string;
    nameasjobposition: string;
    nameasjobplace: string;
    educationadditionaltype: number;
    ucp: number;
    academicvacation: number;
    maternityvacation: number;
    rating: number;
    state: string;
    citytype: string;

    cadetBool: boolean;
    startString: string;
    endString: string;
    interruptedBool: boolean;
    orderdateString: string;
    interruptorderdateString: string;
    academicvacationBool: boolean;
    maternityvacationBool: boolean;

    educationtypeblocks: Educationtypeblock[];
    academicvacations: Academicvacation[];
    educationmaternities: Educationmaternity[];
    personeducationparts: Personeducationpart[]; // Образование делится на части с учетом периодов. Для вкладки УГЗ
    personeducationpartsCommon: Personeducationpart[]; // Образование делится на части с учетом одного большого периода + возможно отдельно академ отпуск и декрет
}