using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public interface IRange<T>
    {
        T minDate { get; }
        T maxDate { get; }
        bool Includes(T value);
        bool Includes(IRange<T> range);
    }

    public class DateRange : IRange<DateTime?>
    {
        public DateRange(DateTime? minDate = new DateTime?(), DateTime? maxDate = new DateTime?())
        {
            this.minDate = minDate;
            this.maxDate = maxDate;
        }
        public DateRange(List<DateTime?> dateTimes)
        {
            minDate = null;
            maxDate = null;
            if (dateTimes != null && dateTimes.Count == 2)
            {
                minDate = dateTimes.First();
                maxDate = dateTimes.Last();
            }
        }

        public DateTime? minDate { get; private set; }
        public DateTime? maxDate { get; private set; }

        public bool Includes(DateTime? value)
        {
            if(minDate.HasValue && maxDate.HasValue)
                return (minDate <= value) && (value <= maxDate);
            else if(minDate.HasValue)
                return (minDate <= value);
            else if(maxDate.HasValue)
                return (value <= maxDate);
            return true;
        }

        public bool Includes(IRange<DateTime?> range)
        {
            return (minDate <= range.minDate) && (range.maxDate <= maxDate);
        }
    }
    public class DecreeFinder
    {
        public DecreeFinder()
        {
            dates = new DateRange();
            number = "";
            name = "";
            date_started = new DateRange();
            nickname = "";
        }

        public DateRange dates { get; set; }
        public string number { get; set; }
        public string name { get; set; }
        public DateRange date_started { get; set; }
        public string nickname { get; set; }

        public bool rewrite { get; set; }
    }
}
