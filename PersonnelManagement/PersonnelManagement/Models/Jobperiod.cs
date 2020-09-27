using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    /// <summary>
    /// Для каждого сотрудника мы всю его последнюю непрерывную службу или работу в МЧС делим на периоды службы по годам.
    /// В каждом периоде высчитываем сколько отпуска израсходовано, сколько положено, а сколько осталось
    /// </summary>
    public class Jobperiod
    {
        public DateTime? Start { get; set; } = null; // Начало периода. У военных с начала службы или начала календарного года. У гражданских в дату устройства на работу  
        public DateTime? End { get; set; } = null; // Конец периода. У военных конец календарного года. У гражданских в день до даты устройства на работу. 
                                                   // Но со временем из-за длительного отпуска по личным причинам год может быть смещен.
        public bool Actual { get; set; } = false; // актуальный ли период. Период актуальный, если текущее число попадает в рамки этого периода
        public int Vacationdaysgiven { get; set; } = 0; // Высчитывает сколько было в период положено дней отпуска.
        public int Vacationdaysgivenclear { get; set; } = 0; // Сколько дней отпуска положено без учета переноса с прошлого года.
        public int Vacationdaysconsumed { get; set; } = 0; // Сколько было потрачено дней отпуска.
        public int Vacationdaystransfer { get; set; } = 0; // Сколько дней отпуска было выделено с этого периода на следующий.
        //public int Vacationdaysconsumedcurrent { get; set; } = 0; // Сколько было потрачено дней отпуска, расчитанных на этот год.
        public int Vacationdaysconsumedprevious { get; set; } = 0; // Сколько было потрачено дней отпуска, расчитанных на прошлый год.
        public bool Vacationcount { get; set; } = false; // Учитывать ли данный период при подсчете переноса отпусков
                                                         // Не записанные периоды в разделе отпусков мы учитывать не будем
        public int Vacationdaystransferleft { get; set; } // Сколько дней из перенесенных осталось. Обычно сгорают в 0, если не было использовано.
        public DateTime? Vacationshiftdate { get; set; } // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату
        public int Vacationshiftbefore { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения.
        public int Vacationshiftafter { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения.

        public int Vacationselected { get; set; } // Для отпусков, выбран ли период в проекте приказа. 

        public int Experience { get; set; } = 0; // Стаж работы/выслуга лет в днях всего
        public int ExperienceYears { get; set; } = 0; // выслуга в годах на текущий период
        public int ExperienceMonths { get; set; } = 0; // выслуга в месяцах за вычетом лет на текущий период
        public int ExperienceDays { get; set; } = 0; // выслуга в днях за вычетом лет и месяцев на текущий период

        public int Stateserviceyears { get; set; } = 0; // Выслуга лет госслужащим за все периоды работы
        public int Stateservicemonths { get; set; } = 0; // Выслуга месяцев госслужащим за все периоды работы
        public int Stateservicedays { get; set; } = 0; // Выслуга дней госслужащим за все периоды работы

        public DateTime? VacationshiftdateStateservice { get; set; } // Если в текущем году происходит увеличение дней отпуска, записывать сюда дату госслужба
        public int VacationshiftbeforeStateservice { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней до увеличения госслужба
        public int VacationshiftafterStateservice { get; set; } = 0; // Если в текущем году происходит увеличение дней отпуска, сколько дней после увеличения госслужба
    }
}
