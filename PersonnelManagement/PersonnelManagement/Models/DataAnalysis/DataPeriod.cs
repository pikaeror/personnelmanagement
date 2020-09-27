using DocumentFormat.OpenXml.Drawing.Charts;
using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class CustomDate
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public CustomDate(DateTime date)
        {
            Year = date.Year;
            Month = date.Month;
            Day = date.Day;
        }

        public CustomDate(int Year = 0, int Month = 0, int Day = 0)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        public CustomDate(double Year = 0, double Month = 0, double Day = 0)
        {
            this.rebase(Year, Month, Day);
        }

        public static CustomDate add(CustomDate first, CustomDate second)
        {
            double Year_d = first.Year + second.Year;
            double Month_d = first.Month + second.Month;
            double Day_d = first.Day + second.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            int Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            int Month = (int)Math.Floor(Month_d) - mult * 12;
            int Year = (int)Year_d + mult;

            return new CustomDate(Year, Month, Day);
        }

        public static CustomDate add(DateTime first, DateTime second)
        {
            double Year_d = first.Year + second.Year;
            double Month_d = first.Month + second.Month;
            double Day_d = first.Day + second.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            int Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            int Month = (int)Math.Floor(Month_d) - mult * 12;
            int Year = (int)Year_d + mult;

            return new CustomDate(Year, Month, Day);
        }

        public void add(CustomDate first)
        {
            double Year_d = first.Year + this.Year;
            double Month_d = first.Month + this.Month;
            double Day_d = first.Day + this.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            this.Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            this.Month = (int)Math.Floor(Month_d) - mult * 12;
            this.Year = (int)Year_d + mult;

            return;
        }

        public void add(DateTime first)
        {
            double Year_d = first.Year + this.Year;
            double Month_d = first.Month + this.Month;
            double Day_d = first.Day + this.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            this.Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            this.Month = (int)Math.Floor(Month_d) - mult * 12;
            this.Year = (int)Year_d + mult;

            return;
        }

        public void add_whithout(CustomDate first)
        {
            this.Year += first.Year;
            this.Month += first.Month;
            this.Day += first.Day;
        }

        public void add_whithout(DateTime first)
        {
            this.Year += first.Year;
            this.Month += first.Month;
            this.Day += first.Day;
        }

        public void rebase()
        {
            double Year_d = this.Year;
            double Month_d = this.Month;
            double Day_d = this.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            this.Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            this.Month = (int)Math.Floor(Month_d) - mult * 12;
            this.Year = (int)Year_d + mult;
        }

        public void rebase(double Year_d = 0, double Month_d = 0, double Day_d = 0)
        {
            int mult = (int)Math.Floor((double)Day_d / 30);
            this.Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            this.Month = (int)Math.Floor(Month_d) - mult * 12;
            this.Year = (int)Year_d + mult;
        }

        public static CustomDate difference(CustomDate first, CustomDate second)
        {
            if (second.Year < first.Year)
                return difference(second, first);
            if (second.Year == first.Year)
            {
                if (second.Month < first.Month)
                    return difference(second, first);
                if (second.Month == first.Month)
                {
                    if (second.Day < first.Day)
                        return difference(second, first);
                }
            }

            int Year = 0;
            int Month = 0;
            int Day = second.Day - first.Day;
            if (Day < 0)
            {
                Day += 30;
                Month--;
            }
            Month += second.Month - first.Month;
            if (Month < 0)
            {
                Month += 12;
                Year--;
            }
            Year += second.Year - first.Year;
            return new CustomDate(Year: Year, Month: Month, Day: Day);
        }

        public static CustomDate difference(DateTime first, DateTime second)
        {
            if (second.Ticks < first.Ticks)
                return difference(second, first);

            int Year = 0;
            int Month = 0;
            int Day = second.Day - first.Day;
            if (Day <= 0)
            {
                Day += 30;
                Month--;
            }
            Month += second.Month - first.Month;
            if (Month <= 0)
            {
                Month += 12;
                Year--;
            }
            Year += second.Year - first.Year;
            return new CustomDate(Year: Year, Month: Month, Day: Day);
        }
    }

    public class DataPeriod
    {
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }

        public double multiply_coef { get; set; }

        public CustomDate period { get; set; }

        public Personjob job { get; set; }
        public Personeducation education { get; set; }
        public Personill ill { get; set; }
        public Personworktrip worktrip { get; set; }
        public Personvacation vacation { get; set; }

        public DataPeriod(DateTime start_date,
            DateTime? end_date = null,
            double coef = 1,
            Personjob job = null,
            Personeducation education = null,
            Personill ill = null,
            Personworktrip worktrip = null,
            Personvacation vacation = null)
        {
            this.job = job;
            this.education = education;
            this.ill = ill;
            this.worktrip = worktrip;
            this.vacation = vacation;
            this.start_date = start_date;
            this.end_date = end_date == null ? DateTime.Today : end_date.GetValueOrDefault();
            this.multiply_coef = coef;
            equalCalculationEndDate();
        }
        public DataPeriod(DateTime start_date,
            int delta_days = 1,
            double coef = 1,
            Personjob job = null,
            Personeducation education = null,
            Personill ill = null,
            Personworktrip worktrip = null,
            Personvacation vacation = null)
        {
            this.job = job;
            this.education = education;
            this.ill = ill;
            this.worktrip = worktrip;
            this.vacation = vacation;
            this.start_date = start_date;
            this.multiply_coef = coef;
            this.end_date = this.start_date.AddDays(delta_days);
            equalCalculationEndDate();
        }

        public CustomDate equalCalculationEndDate()
        {
            CustomDate diff = CustomDate.difference(start_date, end_date);
            this.period = multiply_value(diff, multiply_coef);
            return multiply_value(diff, multiply_coef);
        }

        public static DateTime difference(DateTime first, DateTime second)
        {
            if(second.Ticks < first.Ticks)
                return difference(second, first);

            int Year = 0;
            int Month = 0;
            int Day = second.Day - first.Day;
            if(Day < 0)
            {
                Day += 30;
                Month--;
            }
            Month += second.Month - first.Month;
            if (Month < 0)
            {
                Month += 12;
                Year--;
            }
            Year += second.Year - first.Year;

            return new DateTime(year: Year, month: Month, day: Day);
        }

        private CustomDate multiply_value(CustomDate date, double value)
        {
            int Year, Month, Day;
            double Month_d, Day_d, property;
            Tuple<int, double> time = multiply_value(date.Year, value, 12);
            Year = time.Item1;
            Month_d = time.Item2;

            time = multiply_value(date.Month, value, 30);
            Month = time.Item1 + (int)Math.Floor(Month_d);
            Month_d -= (int)Math.Floor(Month_d);
            property = Month_d * 30;
            Day_d = time.Item2 + (int)Math.Floor(property);
            property -= (int)Math.Floor(property);

            time = multiply_value(date.Day, value, 1);
            Day = time.Item1 + (int)Math.Floor(Day_d);
            Day_d -= (int)Math.Floor(Day_d);
            property += time.Item2 + Day_d;

            Day += (int)Math.Round(property);
            int mult = (int)Math.Floor((double)Day / 30);
            Day -= mult * 30;
            Month += mult;
            mult = (int)Math.Floor((double)Month / 12);
            Month -= mult * 12;
            Year += mult;

            return new CustomDate(Year, Month, Day);
        }

        public static List<double> multiply_value(double Year, double Month, double Day, double value)
        {
            //int Year, Month, Day;
            double Month_d, Day_d, property;
            Tuple<int, double> time = multiply_value(Year, value, 12);
            Year = time.Item1;
            Month_d = time.Item2;

            time = multiply_value(Month, value, 30);
            Month = time.Item1 + (int)Math.Floor(Month_d);
            Month_d -= (int)Math.Floor(Month_d);
            property = Month_d * 30;
            Day_d = time.Item2 + (int)Math.Floor(property);
            property -= (int)Math.Floor(property);

            time = multiply_value(Day, value, 1);
            Day = time.Item1 + (int)Math.Floor(Day_d);
            Day_d -= (int)Math.Floor(Day_d);
            property += time.Item2 + Day_d;

            Day += (int)Math.Round(property);
            int mult = (int)Math.Floor((double)Day / 30);
            Day -= mult * 30;
            Month += mult;
            mult = (int)Math.Floor((double)Month / 12);
            Month -= mult * 12;
            Year += mult;

            return new List<double>() { Year, Month, Day };
        }

        public static Tuple<int, double> multiply_value(double input, double value, int devizor)
        {
            double time = input * value;
            int first_output = (int)Math.Floor(time);
            double second_output = (time - first_output) * devizor;
            return new Tuple<int, double>(first_output, second_output);
        }

        public static DateTime add(DateTime first, DateTime second)
        {
            double Year_d = first.Year + second.Year;
            double Month_d = first.Month + second.Month;
            double Day_d = first.Day + second.Day;

            int mult = (int)Math.Floor((double)Day_d / 30);
            int Day = (int)Math.Floor(Day_d) - mult * 30;
            Month_d += mult;
            mult = (int)Math.Floor((double)Month_d / 12);
            int Month = (int)Math.Floor(Month_d) - mult * 12;
            int Year = (int)Year_d + mult;

            return new DateTime(Year, Month, Day);
        }
    }
}
