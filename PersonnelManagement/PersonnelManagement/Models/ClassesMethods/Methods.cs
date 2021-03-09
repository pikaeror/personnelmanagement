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
                Subject15 };
            return output;
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

    public partial class Person
    {
        public Person() { }

        public Person(Person person)
        {
            Id = person.Id;
            Position = person.Position;
            Structure = person.Structure;
            Name = person.Name;
            Surname = person.Surname;
            Fathername = person.Fathername;
            Birthdate = person.Birthdate;
            Photo = person.Photo;
            Gender = person.Gender;
            Passportid = person.Passportid;
            Passportnum = person.Passportnum;
            Passportdatestart = person.Passportdatestart;
            Passportdateend = person.Passportdateend;
            Birthlocation = person.Birthlocation;
            Registercountry = person.Registercountry;
            Registerstate = person.Registerstate;
            Registersubstate = person.Registersubstate;
            Registercitysubstate = person.Registercitysubstate;
            Registercitytype = person.Registercitytype;
            Registercity = person.Registercity;
            Registerstreettype = person.Registerstreettype;
            Registerstreet = person.Registerstreet;
            Registerhouse = person.Registerhouse;
            Registerhousing = person.Registerhousing;
            Registerflat = person.Registerflat;
            Registeradditional = person.Registeradditional;
            Livecountry = person.Livecountry;
            Livestate = person.Livestate;
            Livesubstate = person.Livesubstate;
            Livecitysubstate = person.Livecitysubstate;
            Livecitytype = person.Livecitytype;
            Livecity = person.Livecity;
            Livestreettype = person.Livestreettype;
            Livestreet = person.Livestreet;
            Livehouse = person.Livehouse;
            Livehousing = person.Livehousing;
            Liveflat = person.Liveflat;
            Liveadditional = person.Liveadditional;
            Nationality = person.Nationality;
            Maritalstatus = person.Maritalstatus;
            Science = person.Science;
            Numpersonal = person.Numpersonal;
            Wound = person.Wound;
            Sciencerank = person.Sciencerank;
            Surnameother = person.Surnameother;
            Name2 = person.Name2;
            Surname2 = person.Surname2;
            Fathername2 = person.Fathername2;
            Name3 = person.Name3;
            Surname3 = person.Surname3;
            Fathername3 = person.Fathername3;
            Name4 = person.Name4;
            Fathername4 = person.Fathername4;
            Surname4 = person.Surname4;
            Name5 = person.Name5;
            Fathername5 = person.Fathername5;
            Surname5 = person.Surname5;
            Name6 = person.Name6;
            Surname6 = person.Surname6;
            Fathername6 = person.Fathername6;
            Removed = person.Removed;
            Registerstatenum = person.Registerstatenum;
            Registersubstatenum = person.Registersubstatenum;
            Livestatenum = person.Livestatenum;
            Livesubstatenum = person.Livesubstatenum;
            Birthcountry = person.Birthcountry;
            Birthstate = person.Birthstate;
            Birthsubstate = person.Birthsubstate;
            Birthcitysubstate = person.Birthcitysubstate;
            Birthcitytype = person.Birthcitytype;
            Birthcity = person.Birthcity;
            Birthadditional = person.Birthadditional;
            Namesubject = person.Namesubject;
            Fathernamesubject = person.Fathernamesubject;
            Surnamesubject = person.Surnamesubject;
            Gendersubject = person.Gendersubject;
        }

        public string getSubjectCase(int case_value = 1)
        {
            switch (case_value)
            {
                case 1:
                    return Surname + " " + Name + " " + Fathername;
                case 2:
                    return Surname2 + " " + Name2 + " " + Fathername2;
                case 3:
                    return Surname3 + " " + Name3 + " " + Fathername3;
                case 4:
                    return Surname4 + " " + Name4 + " " + Fathername4;
                case 5:
                    return Surname5 + " " + Name5 + " " + Fathername5;
                case 6:
                    return Surname6 + " " + Name6 + " " + Fathername6;
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
