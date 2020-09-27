using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PersonnelManagement.Models;

namespace PersonnelManagement.Utils
{
    public static class PositionUtils
    {
        public static long CalculateHash(Position position, DecreeoperationManagement decreeoperationManagement)
        {
            return CalculateHash(position, decreeoperationManagement.Dateactive.GetValueOrDefault());
        }

        public static long CalculateHash(Position position, DateTime date)
        {
            //return position.Sourceoffinancing * 10000000000000 +  date.Year * 1000000000 + date.Month * 10000000 + date.Day * 100000 + position.Positiontype * 100 + position.Positioncategory * 10 + position.Replacedbycivil;
            return date.Year * 1000000000 + date.Month * 10000000 + date.Day * 100000 + position.Positiontype * 100 + position.Positioncategory * 10 + position.Replacedbycivil;
            //if (element.DecreeoperationManagement != null && element.DecreeoperationManagement.Datecustom > 0)
            //{
            //    datecustom = true;
            //    dateactive = element.DecreeoperationManagement.Dateactive.GetValueOrDefault();
            //}
        }
    }
}
