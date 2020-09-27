using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class AltrankPrintable
    {
        public Altrankconditiongroup Altrankconditiongroup { get; set; }
        public List<string> Altrankconditionnames { get; set; } // 1-ый разряд, 2ой разряд
        public Dictionary<int, List<string>> Altranknames { get; set; } // Номер должности - Подполковник, майор
    }
}
