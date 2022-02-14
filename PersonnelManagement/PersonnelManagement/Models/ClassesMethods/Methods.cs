using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public partial class Decree
    {
        public Decree()
        {
            Name = "";
            Signed = 0;
            Declined = 0;
            Dateactive = new DateTime();
            Datesigned = new DateTime();
            User = 0;
            Nickname = "";
            Number = "";
            Historycal = 0;
        }

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

    public partial class Decreeoperation
    {
        public Decreeoperation() { }
        public Decreeoperation(Decreeoperation decreeoperation)
        {
            Id = decreeoperation.Id;
            Decree = decreeoperation.Decree;
            Subject = decreeoperation.Subject;
            Created = decreeoperation.Created;
            Deleted = decreeoperation.Deleted;
            Changed = decreeoperation.Changed;
            Changedtype = decreeoperation.Changedtype;
            Dateactive = decreeoperation.Dateactive;
            Datecustom = decreeoperation.Datecustom;
        }
    }

    public partial class Position
    {
        public Position() { }

        public Position(Position position)
        {
            Id = position.Id;
            Cap = position.Cap;
            Dateactive = position.Dateactive;
            Dateinactive = position.Dateinactive;
            Sourceoffinancing = position.Sourceoffinancing;
            Positiontype = position.Positiontype;
            Notice = position.Notice;
            Positioncategory = position.Positioncategory;
            Replacedbycivil = position.Replacedbycivil;
            Replacedbycivilpositioncategory = position.Replacedbycivilpositioncategory;
            Replacedbycivilpositiontype = position.Replacedbycivilpositiontype;
            Altrank = position.Altrank;
            Origin = position.Origin;
            Decertificate = position.Decertificate;
            Decertificatedate = position.Decertificatedate;
            Civilranklow = position.Civilranklow;
            Civilrankhigh = position.Civilrankhigh;
            Replacedbycivildatelimit = position.Replacedbycivildatelimit;
            Replacedbycivildate = position.Replacedbycivildate;
            Structure = position.Structure;
            Civildecree = position.Civildecree;
            Civildecreenumber = position.Civildecreenumber;
            Civildecreedate = position.Civildecreedate;
            Curator = position.Curator;
            Head = position.Head;
            Curatorlist = position.Curatorlist;
            Headid = position.Headid;
            Opchs = position.Opchs;
            Part = position.Part;
            Partval = position.Partval;
            Subject1 = position.Subject1;
            Subject2 = position.Subject2;
            Subject3 = position.Subject3;
            Subject4 = position.Subject4;
            Subject5 = position.Subject5;
            Subject6 = position.Subject6;
            Subject7 = position.Subject7;
            Subject8 = position.Subject8;
            Subject9 = position.Subject9;
            Subject10 = position.Subject10;
            Subject11 = position.Subject11;
            Subject12 = position.Subject12;
            Subject13 = position.Subject13;
            Subject14 = position.Subject14;
            Subject15 = position.Subject15;
            Subject16 = position.Subject16;
            Subject17 = position.Subject17;
            Subject18 = position.Subject18;
            Subject19 = position.Subject19;
            Subject20 = position.Subject20;
            Name1 = position.Name1;
            Name2 = position.Name2;
            Name3 = position.Name3;
            Name4 = position.Name4;
            Name5 = position.Name5;
            Name6 = position.Name6;
        }

        public List<int> getSubjectsList()
        {
            List<int> output = new List<int> { Subject1,
                Subject2,
                Subject3,
                Subject4,
                Subject5,
                Subject6,
                Subject7,
                Subject8,
                Subject9,
                Subject10,
                Subject11,
                Subject12,
                Subject13,
                Subject14,
                Subject15,
                Subject16,
                Subject17,
                Subject18,
                Subject19,
                Subject20 };
            return output;
        }
    }

    public partial class Subject
    {
        public Subject() { }

        public Subject(Subject subject)
        {
            Id = subject.Id;
            Name = subject.Name;
            Name1 = subject.Name1;
            Name2 = subject.Name2;
            Name3 = subject.Name3;
            Name4 = subject.Name4;
            Name5 = subject.Name5;
            Name6 = subject.Name6;
            Category = subject.Category;
            Gender = subject.Gender;
            Dropword = subject.Dropword;
        }

        public string getSubjectCase(int case_value = 1)
        {
            switch(case_value)
            {
                case 1:
                    return Name1;
                case 2:
                    return Name2;
                case 3:
                    return Name3;
                case 4:
                    return Name4;
                case 5:
                    return Name5;
                case 6:
                    return Name6;
            }
            return "";
        }
    }

    public partial class Rank
    {
        public Rank() { }

        public Rank(Rank rank)
        {
            Id = rank.Id;
            Name = rank.Name;
            Order = rank.Order;
            Dateactive = rank.Dateactive;
            Dateinactive = rank.Dateinactive;
            Notlogged = rank.Notlogged;
            Positioncategory = rank.Positioncategory;
            Decreeupone = rank.Decreeupone;
            Decreeupfast = rank.Decreeupfast;
            Name1 = rank.Name1;
            Name2 = rank.Name2;
            Name3 = rank.Name3;
            Name4 = rank.Name4;
            Name5 = rank.Name5;
            Name6 = rank.Name6;
        }

        public string getSubjectCase(int case_value = 1)
        {
            switch (case_value)
            {
                case 1:
                    return Name1;
                case 2:
                    return Name2;
                case 3:
                    return Name3;
                case 4:
                    return Name4;
                case 5:
                    return Name5;
                case 6:
                    return Name6;
            }
            return "";
        }
    }

    public partial class Positiontype
    {
        public Positiontype() { }

        public Positiontype(Positiontype positiontype)
        {
            Id = positiontype.Id;
            Name = positiontype.Name;
            Priority = positiontype.Priority;
            Nameshort = positiontype.Nameshort;
            Name1 = positiontype.Name1;
            Name2 = positiontype.Name2;
            Name3 = positiontype.Name3;
            Name4 = positiontype.Name4;
            Name5 = positiontype.Name5;
            Name6 = positiontype.Name6;
            Subject1 = positiontype.Subject1;
            Subject2 = positiontype.Subject2;
            Subject3 = positiontype.Subject3;
            Subject4 = positiontype.Subject4;
            Subject5 = positiontype.Subject5;
            Subject6 = positiontype.Subject6;
            Subject7 = positiontype.Subject7;
            Subject8 = positiontype.Subject8;
            Subject9 = positiontype.Subject9;
            Subject10 = positiontype.Subject10;
            Subject11 = positiontype.Subject11;
            Subject12 = positiontype.Subject12;
            Subject13 = positiontype.Subject13;
            Subject14 = positiontype.Subject14;
            Subject15 = positiontype.Subject15;
            Subject16 = positiontype.Subject16;
            Subject17 = positiontype.Subject17;
            Subject18 = positiontype.Subject18;
            Subject19 = positiontype.Subject19;
            Subject20 = positiontype.Subject20;
        }

        public string getSubjectCase(int case_value = 1)
        {
            switch (case_value)
            {
                case 1:
                    return Name1;
                case 2:
                    return Name2;
                case 3:
                    return Name3;
                case 4:
                    return Name4;
                case 5:
                    return Name5;
                case 6:
                    return Name6;
            }
            return "";
        }

        public List<int> getSubjectsList()
        {
            List<int> output = new List<int> { Subject1,
                Subject2,
                Subject3,
                Subject4,
                Subject5,
                Subject6,
                Subject7,
                Subject8,
                Subject9,
                Subject10,
                Subject11,
                Subject12,
                Subject13,
                Subject14,
                Subject15,
                Subject16,
                Subject17,
                Subject18,
                Subject19,
                Subject20 };
            return output;
        }
    }
}
