using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class PositionhistoryManagement
    {

        public int Id { get; set; }
        public int Position { get; set; }
        public int Previous { get; set; }
        public int Decreeoperation { get; set; }
        public int Decree { get; set; }
        public sbyte Created { get; set; }
        public sbyte Deleted { get; set; }

        public int Origin { get; set; } = 0; // 0 if false, 1 if true
        public DateTime Decreedate { get; set; }
        public string Decreename { get; set; }
        public string Decreenumber { get; set; }
        public string Decreenickname { get; set; }

        public void Fulfill(Positionhistory positionhistory)
        {
            Id = positionhistory.Id;
            Position = positionhistory.Position;
            Previous = positionhistory.Previous;
            Decreeoperation = positionhistory.Decreeoperation;
            Decree = positionhistory.Decree;
            Created = positionhistory.Created;
            Deleted = positionhistory.Deleted;
        }

        //decreedate: Date;
        //decreename: string;
        //decreenumber: string;
    }
}
