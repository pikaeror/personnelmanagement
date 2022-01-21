import Person from '../../classes/person';

module formatting {
    export function formatter_el_table_fullname(row, column, cellValue, index): string {
        var person: Person = cellValue;
        return person.surname + " " + person.name + " " + person.fathername;
    }

    export function formatter_el_table_collumn_date(row, column, cellValue, index): string {
        var output = "";
        try {
            var date = new Date(cellValue);
            output = date.toLocaleDateString('ru-Ru', { day: '2-digit', month: '2-digit', year: 'numeric' });
        } catch (e) {
            output = cellValue;
        }
        return output;
    }

    export function dateFormater(date: Date): string {
        var output = "";
        try {
            output = date.toLocaleDateString('ru-Ru', { day: '2-digit', month: '2-digit', year: 'numeric' });
        } catch (e) {
        }
        return output;
    }
}

export default formatting;