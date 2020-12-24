using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Decree
    {
        public Decree() { }

        public Decree(Decree decree)
        {
            Id = decree.Id;
            Name = decree.Name;
            Signed = decree.Signed;
            Declined = decree.Declined;
            Dateactive = decree.Dateactive;
            Datesigned = decree.Datesigned;
            User = decree.User;
            Nickname = decree.Nickname;
            Number = decree.Number;
            Historycal = decree.Historycal;
        }
    }

    public partial class Structure
    {
        public Structure() { }

        public Structure(Structure structure)
        {
            Id = structure.Id;
            Name = structure.Name;
            Parentstructure = structure.Parentstructure;
            Featured = structure.Featured;
            Nameshortened = structure.Nameshortened;
            Structuretype = structure.Structuretype;
            Structureregion = structure.Structureregion;
            City = structure.City;
            Street = structure.Street;
            Rank = structure.Rank;
            Department = structure.Department;
            Curator = structure.Curator;
            Head = structure.Head;
            Changeorigin = structure.Changeorigin;
            Changestructurelast = structure.Changestructurelast;
            Changestructurerename = structure.Changestructurerename;
            Changestructureall = structure.Changestructureall;
            Changestructurerank = structure.Changestructurerank;
            Changestructurelocation = structure.Changestructurelocation;
            Changestructureparent = structure.Changestructureparent;
            Priority = structure.Priority;
            Printreward = structure.Printreward;
            Main = structure.Main;
            Name1 = structure.Name1;
            Name2 = structure.Name2;
            Name3 = structure.Name3;
            Name4 = structure.Name4;
            Name5 = structure.Name5;
            Name6 = structure.Name6;
            Separatestructure = structure.Separatestructure;
            Subject1 = structure.Subject1;
            Subject2 = structure.Subject2;
            Subject3 = structure.Subject3;
            Subject4 = structure.Subject4;
            Subject5 = structure.Subject5;
            Subject6 = structure.Subject6;
            Subject7 = structure.Subject7;
            Subject8 = structure.Subject8;
            Subject9 = structure.Subject9;
            Subject10 = structure.Subject10;
            Subject11 = structure.Subject11;
            Subject12 = structure.Subject12;
            Subject13 = structure.Subject13;
            Subject14 = structure.Subject14;
            Subject15 = structure.Subject15;
            Subjectnumber = structure.Subjectnumber;
            Subjectnotice = structure.Subjectnotice;
            Subjectgender = structure.Subjectgender;
        }
    }
}
