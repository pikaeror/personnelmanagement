export default class Countrycities {
    country: number = null;
    cities: string[] = new Array();
    citytoadd: string = "";


    addCity() {
        alert(this.citytoadd);
        this.cities.push(this.citytoadd);
        this.citytoadd = "";
    }

    /**
     * Превращаем объект в строку 
     * 
     */
    countrycitiesToString(): string {
        let str: string = "";
        if (this.country != null) {
            str += this.country.toString();
        } else {
            str += 0;
        }
        
        this.cities.forEach(c => {
            str += "&";
            str += c;
        })
        if (this.citytoadd != null && this.citytoadd.length > 0) {
            str += "&";
            str += this.citytoadd;
        }
        return str;
    }


    /**
     * Превращаем строку в объект
     * @param str
     */
    static stringToCountrycities(str: string): Countrycities {
        let countrycities: Countrycities = new Countrycities();
        let parts: string[] = str.split('&');
        let first: boolean = true;
        parts.forEach(p => {
            // Записываем id страны
            if (first) {
                countrycities.country = Number.parseInt(p);
                // Превращаем в null для визуального отображения в input;
                if (countrycities.country == 0) {
                    countrycities.country = null;
                }
                first = false;
            // Записываем название города
            } else {
                countrycities.cities.push(p);
            }
        })
        return countrycities;
    }

    /**
     * Превращаем список объектов в строку
     * @param list
     */
    static countrycitiesListToString(list: Countrycities[]): string {
        let str: string = "";
        list.forEach(c => {
            if (str.length > 0) {
                str += "%";
            }
            str += c.countrycitiesToString();
        })

        return str;
    }

    static stringToCountrycitiesList(str: string): Countrycities[] {
        let list: Countrycities[] = new Array();
        let bigparts: string[] = str.split('%');
        bigparts.forEach(s => {
            list.push(Countrycities.stringToCountrycities(s));
        })

        return list;
    }
}