using Itenso.TimePeriod;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Personjobprivelege
    {
        [NotMapped]
        public List<Personjobprivelegeperiod> Personjobprivelegeperiods { get; set; } = new List<Personjobprivelegeperiod>();
        [NotMapped]
        public int Daysbeforecoef { get; set; } = 0;
        [NotMapped]
        public int Monthsbeforecoef { get; set; } = 0;
        [NotMapped]
        public int Yearsbeforecoef { get; set; }
        [NotMapped]
        public int Daysaftercoef { get; set; }
        [NotMapped]
        public int Monthsaftercoef { get; set; }
        [NotMapped]
        public int Yearsaftercoef { get; set; }

        /// <summary>
        /// Клонирует Personjobprivelege. Можно вставить свои даты начала и конца
        /// </summary>
        /// <param name="personjobprivelege"></param>
        /// <param name="personjobprivelegeperiod"></param>
        /// <returns></returns>
        public Personjobprivelege Clone(DateTime? start = null, DateTime? end = null)
        {
            Personjobprivelege newPersonjobprivelege = new Personjobprivelege();
            newPersonjobprivelege.Personjob = Personjob;
            newPersonjobprivelege.Start = Start;
            newPersonjobprivelege.End = End;
            if (start != null)
            {
                newPersonjobprivelege.Start = start.GetValueOrDefault();
            }
            if (end != null)
            {
                newPersonjobprivelege.End = end.GetValueOrDefault();
            }
            newPersonjobprivelege.Coef = Coef;
            newPersonjobprivelege.Prooftype = Prooftype;
            newPersonjobprivelege.Proofdate = Proofdate;
            newPersonjobprivelege.Proofnumber = Proofnumber;
            newPersonjobprivelege.Prooftext = Prooftext;
            newPersonjobprivelege.Documentorder = Documentorder;
            newPersonjobprivelege.Documentdate = Documentdate;
            newPersonjobprivelege.Documentnumber = Documentnumber;
            newPersonjobprivelege.Ordernumbertype = Ordernumbertype;
            newPersonjobprivelege.Orderwho = Orderwho;
            newPersonjobprivelege.Orderwhoid = Orderwhoid;
            newPersonjobprivelege.Orderid = Orderid;
            newPersonjobprivelege.Ordertype = Ordertype;

            DateDiff dateDiff = new DateDiff(newPersonjobprivelege.Start.GetValueOrDefault(), newPersonjobprivelege.End.GetValueOrDefault());
            newPersonjobprivelege.Daysbeforecoef = dateDiff.ElapsedDays;
            newPersonjobprivelege.Monthsbeforecoef = dateDiff.ElapsedMonths;
            newPersonjobprivelege.Yearsbeforecoef = dateDiff.ElapsedYears;

            int daysmultiplied = dateDiff.ElapsedYears * 365 + dateDiff.ElapsedMonths * 30 + dateDiff.ElapsedDays;
            daysmultiplied = (int)(daysmultiplied * newPersonjobprivelege.Coef);
            int afteryears = daysmultiplied / 365;
            daysmultiplied = daysmultiplied - (afteryears * 365);
            int aftermonths = daysmultiplied / 30;
            daysmultiplied = daysmultiplied - (aftermonths * 30);
            newPersonjobprivelege.Daysaftercoef = daysmultiplied;
            newPersonjobprivelege.Monthsaftercoef = aftermonths;
            newPersonjobprivelege.Yearsaftercoef = afteryears;

            return newPersonjobprivelege;
        }
    }
}
