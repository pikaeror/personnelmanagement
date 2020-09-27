export default class Jobperiod {
    start: Date;
    end: Date;
    actual: boolean; // актуальный ли период. Период актуальный, если текущее число попадает в рамки этого периода
    vacationdaysgiven: number;
    vacationdaysgivenclear: number;
    vacationdaysconsumed: number; // Сколько было потрачено дней отпуска.
    vacationdaystransfer: number; // Сколько дней отпуска было выделено с этого периода на следующий.
    vacationdaysconsumedprevious: number; // Сколько было потрачено дней отпуска, расчитанных на прошлый год.
    vacationcount: boolean; // Учитывать ли данный период при подсчете переноса отпусков
                            // Не записанные периоды в разделе отпусков мы учитывать не будем
    vacationdaystransferleft: number; // Сколько дней из перенесенных осталось. Обычно сгорают в 0, если не было использовано.
    vacationshiftdate: Date; // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату
    vacationshiftbefore: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения.
    vacationshiftafter: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения.

    vacationselected: number; // Для отпусков, выбран ли период в проекте приказа. 
    vacationselecteddays: number; // Для отпусков в проекте приказа, сколько дней отпуска берем с периода
    experience: number;
    experienceDays: number; // выслуга в днях за вычетом лет и месяцев на текущий период
    experienceMonths: number; // выслуга в месяцах за вычетом лет на текущий период
    experienceYears: number; // выслуга в годах на текущий период

    // Только front
    vacationselectedshow: boolean = true; // Для отпусков, показывать ли свич, который определяет, выбран ли период или нет
    vacationmax: number; // Для clientside проверки, смотреть, чтобы значение устанавливаемое пользователем не могло быть больше максимально доступного

    stateserviceyears: number; // Выслуга лет госслужащим за все периоды работы
    stateservicemonths: number; // Выслуга месяцев госслужащим за все периоды работы
    stateservicedays: number; // Выслуга дней госслужащим за все периоды работы
    vacationshiftdateStateservice: Date; // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату
    vacationshiftbeforeStateservice: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения.
    vacationshiftafterStateservice: number; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения.
}