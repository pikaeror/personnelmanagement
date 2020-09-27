import Educationtypeblock from './educationtypeblock';
import Academicvacation from './academicvacation';
import Educationmaternity from './educationmaternity';
import Educationperiod from './educationperiod';

export default class Personeducationpart {
    start: Date;
    end: Date;

    educationtypeblock: Educationtypeblock;
    academicvacation: Academicvacation;
    educationmaternity: Educationmaternity;
    educationperiod: Educationperiod;
}