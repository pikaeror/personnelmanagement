using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class Personeducationpart
    {
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public Educationperiod Educationperiod { get; set; }
        public Academicvacation Academicvacation { get; set; }
        public Educationmaternity Educationmaternity { get; set; }
        public Educationtypeblock Educationtypeblock { get; set; }
        /// <summary>
        /// Номер группы для слияния. Используется для выслуги лет, когда разные периоды УГЗ сливаются в одну запись
        /// </summary>
        public int Group { get; set; } = 0;
        /// <summary>
        /// Ссылка на предыдущий по времени кусок обучения, который не является академ отпуском или декретом.
        /// </summary>
        [JsonIgnore]
        public Personeducationpart PreviousEducationpartWithEducationperiod { get; set; } = null;
    }
}
