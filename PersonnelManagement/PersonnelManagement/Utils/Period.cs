using Itenso.TimePeriod;
using PersonnelManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Utils
{

    /// <summary>
    /// Класс по работе с периодами и датами. Есть как статические методы для расчета разницы в днях/месяцах/годах между датами, 
    /// так и работать внутри класса, добавляя к периоду дни/месяцы/годы.
    /// </summary>
    public class Period
    {
        public DateTime? Start { get; set; } = null;
        public DateTime? End { get; set; } = null;
        public int Days { get; set; }
        public int Months { get; set; }
        public int Years { get; set; }
        /// <summary>
        /// Сплиттер - период, которым мы делим другой (более большой) период. В результате образуется 1-3 новых периода после деления. 
        /// Пометка splitter говорит о том, какой из этих образованных периодов был делителем. Остальные - части первоначального большого периода.
        /// </summary>
        public bool Splitter { get; set; } = false;

        /// <summary>
        /// Связь периода с трудовой деятельностью
        /// </summary>
        public Personjob Personjob { get; set; }
        /// <summary>
        /// Связь периода с льготным периодом.
        /// </summary>
        public Personjobprivelege Personjobprivelege { get; set; }

        public Period()
        {

        }

        /// <summary>
        /// Конструктор для работы без дат, только с числами
        /// </summary>
        /// <param name="years"></param>
        /// <param name="months"></param>
        /// <param name="days"></param>
        public Period(int years, int months, int days)
        {
            Years = years;
            Months = months;
            Days = days;
        }

        /// <summary>
        /// Конструктор, если есть начало и конец периода.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public Period(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Разделяет один период внутренним периодом. В результате получается 1-3 новых периода, один из которых сплиттер.
        /// </summary>
        /// <param name="splitStart"></param>
        /// <param name="splitEnd"></param>
        /// <returns></returns>
        public List<Period> Split(DateTime splitStart, DateTime splitEnd)
        {
            List<Period> periods = new List<Period>();
            if (Start == null || End == null)
            {
                return periods;
            }

            Period splitPeriod = InnerPart(splitStart, splitEnd);
            if (splitPeriod == null)
            {
                return periods;
            }
            splitPeriod.Splitter = true;
            periods.Add(splitPeriod);

            // Этот период и разделитель совпадают по времени
            if (splitPeriod.Start.GetValueOrDefault() == Start.GetValueOrDefault() && splitPeriod.End.GetValueOrDefault() == End.GetValueOrDefault())
            {
                return periods;
            // Совпадает начало у разделителя и периода
            } else if (splitPeriod.Start.GetValueOrDefault() == Start.GetValueOrDefault())
            {
                DateTime dateAfterSplit = splitEnd.AddDays(1);
                Period afterPeriod = new Period(dateAfterSplit, End.GetValueOrDefault());
                periods.Add(afterPeriod);
                return periods;
            // Совпадает конец у разделителя и периода
            } else if (splitPeriod.End.GetValueOrDefault() == End.GetValueOrDefault())
            {
                DateTime dateBeforeSplit = splitStart.AddDays(-1);
                Period beforePeriod = new Period(Start.GetValueOrDefault(), dateBeforeSplit);
                periods.Add(beforePeriod);
                return periods;
            // Разделитель находится внутри этого периода.
            } else
            {
                DateTime dateAfterSplit = splitEnd.AddDays(1);
                Period afterPeriod = new Period(dateAfterSplit, End.GetValueOrDefault());
                periods.Add(afterPeriod);

                DateTime dateBeforeSplit = splitStart.AddDays(-1);
                Period beforePeriod = new Period(Start.GetValueOrDefault(), dateBeforeSplit);
                periods.Add(beforePeriod);
                return periods;
            }

            return periods;
        }

        /// <summary>
        /// Определяет, находится ли период СНАРУЖИ указанных начальной и конечной даты (другого периода)
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public bool Surround(DateTime start, DateTime end)
        {
            if (Start <= start && End >= end)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Находит внутреннюю часть внешнего периода в этом периоде. Возвращает null, если не пересекаются.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Period InnerPart(DateTime start, DateTime end)
        {
            if (Start == null || End == null)
            {
                return null;
            }

            if (start > end)
            {
                (start, end) = (end, start);
            }

            // Внешний период раньше этого периода
            if (start < Start && end < Start)
            {
                return null;
            }
            // Внешний период позже этого периода
            if (start > End && end > End)
            {
                return null;
            }

            // Внешний период оказался больше текущего периода
            if (start < Start && end > End)
            {
                return new Period(Start.GetValueOrDefault(), End.GetValueOrDefault()); 
            }

            // Внешний период целиком в этом периоде 
            if (Start <= start && End >= end)
            {
                Period period = new Period(start, end);
                return period;
            } else if (Start > start && End >= end)
            {
                Period period = new Period(Start.GetValueOrDefault(), end);
                return period;
            } else if (Start <= start && End < end)
            {
                Period period = new Period(start, End.GetValueOrDefault());
                return period;
            }
            return null;
        }

        public void AddDays(int days)
        {
            Days += days;
            Recalculate();
        }

        public void AddMonths(int months)
        {
            Months += months;
            Recalculate();
        }

        public void AddYears(int years)
        {
            Years += years;
        }

        /// <summary>
        /// Пересчитывает дни, месяцы и годы внутри класса. Например, если 45 дней и 1 месяц, то станет 15 дней и 2 месяца 
        /// </summary>
        public void Recalculate()
        {
            while (Days > 29)
            {
                Days -= 30;
                Months += 1;
            }
            while (Months > 11)
            {
                Months -= 12;
                Years += 1;
            }
        }

        /// <summary>
        /// Вывод лет для продолжительности периода, где start - начало периода, а end - конец. Выводит отбрасывая лишние месяцы и годы
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int YearDiff(DateTime start, DateTime end)
        {
            DateDiff dateDiff = new DateDiff(start.AddDays(-1), end);
            return dateDiff.ElapsedYears;
        }

        /// <summary>
        /// Вывод месяцев для продолжительности периода, где start - начало периода, а end - конец. Выводит не суммарное количество дней, а за вычетом лет
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int MonthDiff(DateTime start, DateTime end)
        {
            DateDiff dateDiff = new DateDiff(start.AddDays(-1), end);
            return dateDiff.ElapsedMonths;
        }

        /// <summary>
        /// Вывод дней для продолжительности периода, где start - начало периода, а end - конец. Выводит не суммарное количество дней, а за вычетом месяцев и лет
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static int DayDiff(DateTime start, DateTime end)
        {
            DateDiff dateDiff = new DateDiff(start.AddDays(-1), end);
            return dateDiff.ElapsedDays;
        }
    }
}
