using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    /**
     * Backend only
     */
    public class AltrankInner
    {
        public Altrankconditiongroup Altrankconditiongroup { get; set; }
        public List<Altrankcondition> Conditions { get; set; }
        public List<Rank> Ranks { get; set; }
        public HashSet<Rank> RanksAll { get; set; }
        public KeyValue<Rank, Rank> RanksMinMax { get; set; }

        public AltrankInner(Position position, Repository repository)
        {
            if (position.Altrank == 0)
            {
                return;
            }

            List<Altrank> altranks = repository.Altranks.Where(a => a.Position == position.Id).ToList();
            Conditions = new List<Altrankcondition>();
            Ranks = new List<Rank>();
            foreach (Altrank ar in altranks)
            {
                Altrankcondition condition = repository.Altrankconditions.First(a => a.Id == ar.Altrankcondition);
                Conditions.Add(condition);
                Ranks.Add(repository.Ranks.First(r => r.Id == ar.Rank));
            }

            RanksAll = new HashSet<Rank>(Ranks);
            RanksAll.Add(repository.Ranks.First(r => r.Id == position.Cap.GetValueOrDefault()));
            Rank rankMin = null;
            Rank rankMax = null;
            foreach (Rank rank in RanksAll)
            {
                if (rankMin == null)
                {
                    rankMin = rank;
                }
                if (rankMax == null)
                {
                    rankMax = rank;
                }

                if (rankMax.Order < rank.Order)
                {
                    rankMax = rank;
                }

                if (rankMin.Order > rank.Order)
                {
                    rankMin = rank;
                }
            }
            RanksMinMax = new KeyValue<Rank, Rank>(rankMin, rankMax);

            Altrankconditiongroup = repository.Altrankconditiongroups.First(acg => acg.Id == Conditions.First().Group);
        }
    }
}
