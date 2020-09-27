using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    /**
     * For decree enum printing
     */
    public class PrintEnumElement
    {
        /**
         * На следующий день - сделать отдельный класс для каждого визуального элемента, который будет в перечне, чтобы запилить всю иерархию.
         */

        public string Name { get; set; }
        public bool InEnum { get; set; }
        public string FullTree { get; set; }
        public Structure Structure { get; set; }
        public Position Position { get; set; }
        public DecreeoperationManagement DecreeoperationManagement { get; set; }

        /**
         * For structures
         */
        public int Level { get; set; } = 0; // Уровень подразделения. 0 - самый высокий. 
        public int LevelDifference { get; set; } = 0; // Ставить разницу уровней, если следом идет структура, которая по уровню ниже.
        public List<double> CountByLevels { get; set; } = new List<double>(); // Список в котором хранится суммарная численность подразделения, а также подразделений, которым данное подразделение подчинено
                                                                        // Например 0 - 5 (свое), 1 - 9 (4 + предыдущие 5).
        public double CountByLevelsWithChildren { get; set; } = 0; // если у подразделения есть подчиненные подразделения, закидывать их численность
        public double PureCount { get; set; } = 0; // Pure count of positions in structure. SELF ONLY.

        /**
         * For structures change negative
         */
        public int LevelNegative { get; set; } = 0; // Уровень подразделения. 0 - самый высокий. 
        public int LevelDifferenceNegative { get; set; } = 0; // Ставить разницу уровней, если следом идет структура, которая по уровню ниже.
        public List<double> CountByLevelsNegative { get; set; } = new List<double>(); // Список в котором хранится суммарная численность подразделения, а также подразделений, которым данное подразделение подчинено
                                                                              // Например 0 - 5 (свое), 1 - 9 (4 + предыдущие 5).
        public double CountByLevelsWithChildrenNegative { get; set; } = 0; // если у подразделения есть подчиненные подразделения, закидывать их численность
        public double PureCountNegative { get; set; } = 0; // Pure count of positions in structure. SELF ONLY.

        /**
         * For positions and categories
         */
        public double Count { get; set; } = 0;
        public Dictionary<int, double> Sofs { get; set; } = new Dictionary<int, double>();
        public Dictionary<int, double> SofsFuture { get; set; } = new Dictionary<int, double>(); // Если должность еще только появится, помечать количество должностей, которые только будут введены отдельно
        public bool Civil { get; set; }
        public bool Prolonged { get; set; } = false;
        public DateTime ProlongDate { get; set; }
    }
}
