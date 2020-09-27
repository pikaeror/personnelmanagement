using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Personworktrip
    {
        /// <summary>
        /// Отображать в льготной выслуге лет. Отображаем только те, которые затрагивают льготные периоды
        /// </summary>
        [NotMapped]
        public bool DisplayPrivelege { get; set; } = false;
    }
}
