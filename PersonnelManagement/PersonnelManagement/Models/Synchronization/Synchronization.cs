using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class Synchronization
    {
        private List<Altrank> altranks { get; set; }
        private List<Altrankcondition> altrankconditions { get; set; }
        private List<Altrankconditiongroup> altrankconditiongroups { get; set; }
        private List<Area> areas { get; set; }
        private List<Areaother> areaothers { get; set; }
        private List<Citysubstate> citysubstates { get; set; }
        private List<Citytype> citytypes { get; set; }
        private List<Civildecree> civildecrees { get; set; }
        private List<Country> countries { get; set; }
        private List<Decree> decrees { get; set; }
        private List<Decreeoperation> decreeoperations { get; set; }
        private List<Department> departments { get; set; }
        private List<Departmentrename> departmentrenames { get; set; }
        private List<Dismissalclauses> dismissalclauses { get; set; }
        private List<Drivercategory> drivercategories { get; set; }
        private List<Drivertype> drivertypes { get; set; }
        private List<Elementsubject> elementsubjects { get; set; }
        private List<Externalorderwhotype> externalorderwhotypes { get; set; }
        private List<Mrd> mrds { get; set; }
        private List<Ordernumbertype> ordernumbertypes { get; set; }
        private List<Position> positions { get; set; }
        private List<Positioncategory> positioncategories { get; set; }
        private List<Positioncategoryrank> positioncategoryranks { get; set; }
        private List<Positionhistory> positionhistories { get; set; }
        private List<Positionmrd> positionmrds { get; set; }
        private List<Positiontype> positiontypes { get; set; }
        private List<Prooftype> prooftypes { get; set; }
        private List<Rank> ranks { get; set; }
        private List<Region> regions { get; set; }
        private List<Rights> rights { get; set; }
        private List<Rightsstructure> rightsstructures { get; set; }
        private List<Role> roles { get; set; }
        private List<Session> sessions { get; set; }
        private List<Setpersondatatype> setpersondatatypes { get; set; }
        private List<Sourceoffinancing> sourceoffinancings { get; set; }
        private List<Staffcomission> staffcomissions { get; set; }
        private List<Streettype> streettypes { get; set; }
        private List<Structure> structures { get; set; }
        private List<Structureregion> structureregions { get; set; }
        private List<Structuretype> structuretypes { get; set; }
        private List<Subject> subjects { get; set; }
        private List<Subjectcategory> subjectcategories { get; set; }
        private List<Subjectexport> subjectexports { get; set; }
        private List<Subjectgender> subjectgenders { get; set; }
        private List<Transfertype> transfertypes { get; set; }
        private List<User> users { get; set; }
        public Synchronization()
        {
            altranks = new List<Altrank>() { };
            altrankconditions = new List<Altrankcondition>() { };
            altrankconditiongroups = new List<Altrankconditiongroup>() { };
            areas = new List<Area>() { };
            areaothers = new List<Areaother>() { };
            citysubstates = new List<Citysubstate>() { };
            citytypes = new List<Citytype>() { };
            civildecrees = new List<Civildecree>() { };
            countries = new List<Country>() { };
            decrees = new List<Decree>() { };
            decreeoperations = new List<Decreeoperation>() { };
            departments = new List<Department>() { };
            departmentrenames = new List<Departmentrename>() { };
            dismissalclauses = new List<Dismissalclauses>() { };
            drivercategories = new List<Drivercategory>() { };
            drivertypes = new List<Drivertype>() { };
            elementsubjects = new List<Elementsubject>() { };
            externalorderwhotypes = new List<Externalorderwhotype>() { };
            mrds = new List<Mrd>() { };
            ordernumbertypes = new List<Ordernumbertype>() { };
            positions = new List<Position>() { };
            positioncategories = new List<Positioncategory>() { };
            positioncategoryranks = new List<Positioncategoryrank>() { };
            positionhistories = new List<Positionhistory>() { };
            positionmrds = new List<Positionmrd>() { };
            positiontypes = new List<Positiontype>() { };
            prooftypes = new List<Prooftype>() { };
            ranks = new List<Rank>() { };
            regions = new List<Region>() { };
            rights = new List<Rights>() { };
            rightsstructures = new List<Rightsstructure>() { };
            roles = new List<Role>() { };
            sessions = new List<Session>() { };
            setpersondatatypes = new List<Setpersondatatype>() { };
            sourceoffinancings = new List<Sourceoffinancing>() { };
            staffcomissions = new List<Staffcomission>() { };
            streettypes = new List<Streettype>() { };
            structures = new List<Structure>() { };
            structureregions = new List<Structureregion>() { };
            structuretypes = new List<Structuretype>() { };
            subjects = new List<Subject>() { };
            subjectcategories = new List<Subjectcategory>() { };
            subjectexports = new List<Subjectexport>() { };
            subjectgenders = new List<Subjectgender>() { };
            transfertypes = new List<Transfertype>() { };
            users = new List<User>() { };
        }
        public Synchronization(Synchronization synchronization)
        {
            altranks = synchronization.altranks;
            altrankconditions = synchronization.altrankconditions;
            altrankconditiongroups = synchronization.altrankconditiongroups;
            areas = synchronization.areas;
            areaothers = synchronization.areaothers;
            citysubstates = synchronization.citysubstates;
            citytypes = synchronization.citytypes;
            civildecrees = synchronization.civildecrees;
            countries = synchronization.countries;
            decrees = synchronization.decrees;
            decreeoperations = synchronization.decreeoperations;
            departments = synchronization.departments;
            departmentrenames = synchronization.departmentrenames;
            dismissalclauses = synchronization.dismissalclauses;
            drivercategories = synchronization.drivercategories;
            drivertypes = synchronization.drivertypes;
            elementsubjects = synchronization.elementsubjects;
            externalorderwhotypes = synchronization.externalorderwhotypes;
            mrds = synchronization.mrds;
            ordernumbertypes = synchronization.ordernumbertypes;
            positions = synchronization.positions;
            positioncategories = synchronization.positioncategories;
            positioncategoryranks = synchronization.positioncategoryranks;
            positionhistories = synchronization.positionhistories;
            positionmrds = synchronization.positionmrds;
            positiontypes = synchronization.positiontypes;
            prooftypes = synchronization.prooftypes;
            ranks = synchronization.ranks;
            regions = synchronization.regions;
            rights = synchronization.rights;
            rightsstructures = synchronization.rightsstructures;
            roles = synchronization.roles;
            sessions = synchronization.sessions;
            setpersondatatypes = synchronization.setpersondatatypes;
            sourceoffinancings = synchronization.sourceoffinancings;
            staffcomissions = synchronization.staffcomissions;
            streettypes = synchronization.streettypes;
            structures = synchronization.structures;
            structureregions = synchronization.structureregions;
            structuretypes = synchronization.structuretypes;
            subjects = synchronization.subjects;
            subjectcategories = synchronization.subjectcategories;
            subjectexports = synchronization.subjectexports;
            subjectgenders = synchronization.subjectgenders;
            transfertypes = synchronization.transfertypes;
            users = synchronization.users;
        }
        public Synchronization(pmContext pmContext)
        {
            altranks = pmContext.Altrank.ToList();
            altrankconditions = pmContext.Altrankcondition.ToList();
            altrankconditiongroups = pmContext.Altrankconditiongroup.ToList();
            areas = pmContext.Area.ToList();
            areaothers = pmContext.Areaother.ToList();
            citysubstates = pmContext.Citysubstate.ToList();
            citytypes = pmContext.Citytype.ToList();
            civildecrees = pmContext.Civildecree.ToList();
            countries = pmContext.Country.ToList();
            decrees = pmContext.Decree.ToList();
            decreeoperations = pmContext.Decreeoperation.ToList();
            departments = pmContext.Department.ToList();
            departmentrenames = pmContext.Departmentrename.ToList();
            dismissalclauses = pmContext.Dismissalclauses.ToList();
            drivercategories = pmContext.Drivercategory.ToList();
            drivertypes = pmContext.Drivertype.ToList();
            elementsubjects = pmContext.Elementsubject.ToList();
            externalorderwhotypes = pmContext.Externalorderwhotype.ToList();
            mrds = pmContext.Mrd.ToList();
            ordernumbertypes = pmContext.Ordernumbertype.ToList();
            positions = pmContext.Position.ToList();
            positioncategories = pmContext.Positioncategory.ToList();
            positioncategoryranks = pmContext.Positioncategoryrank.ToList();
            positionhistories = pmContext.Positionhistory.ToList();
            positionmrds = pmContext.Positionmrd.ToList();
            positiontypes = pmContext.Positiontype.ToList();
            prooftypes = pmContext.Prooftype.ToList();
            ranks = pmContext.Rank.ToList();
            regions = pmContext.Region.ToList();
            rights = pmContext.Rights.ToList();
            rightsstructures = pmContext.Rightsstructure.ToList();
            roles = pmContext.Role.ToList();
            sessions = pmContext.Session.ToList();
            setpersondatatypes = pmContext.Setpersondatatype.ToList();
            sourceoffinancings = pmContext.Sourceoffinancing.ToList();
            staffcomissions = pmContext.Staffcomission.ToList();
            streettypes = pmContext.Streettype.ToList();
            structures = pmContext.Structure.ToList();
            structureregions = pmContext.Structureregion.ToList();
            structuretypes = pmContext.Structuretype.ToList();
            subjects = pmContext.Subject.ToList();
            subjectcategories = pmContext.Subjectcategory.ToList();
            subjectexports = pmContext.Subjectexport.ToList();
            subjectgenders = pmContext.Subjectgender.ToList();
            transfertypes = pmContext.Transfertype.ToList();
            users = pmContext.User.ToList();
        }

        public void update(Repository repository)
        {
            pmContext context = repository.GetContext();
            context.Altrank = Checker(context.Altrank, altranks);
            context.Altrankcondition = Checker(context.Altrankcondition, altrankconditions);
            context.Altrankconditiongroup = Checker(context.Altrankconditiongroup, altrankconditiongroups);
            context.Area = Checker(context.Area, areas);
            context.Areaother = Checker(context.Areaother, areaothers);
            context.Citysubstate = Checker(context.Citysubstate, citysubstates);
            context.Citytype = Checker(context.Citytype, citytypes);
            context.Civildecree = Checker(context.Civildecree, civildecrees);
            context.Country = Checker(context.Country, countries);
            context.Decree = Checker(context.Decree, decrees);
            context.Decreeoperation = Checker(context.Decreeoperation, decreeoperations);
            context.Department = Checker(context.Department, departments);
            context.Departmentrename = Checker(context.Departmentrename, departmentrenames);
            context.Dismissalclauses = Checker(context.Dismissalclauses, dismissalclauses);
            context.Drivercategory = Checker(context.Drivercategory, drivercategories);
            context.Drivertype = Checker(context.Drivertype, drivertypes);
            context.Elementsubject = Checker(context.Elementsubject, elementsubjects);
            context.Externalorderwhotype = Checker(context.Externalorderwhotype, externalorderwhotypes);
            context.Mrd = Checker(context.Mrd, mrds);
            context.Ordernumbertype = Checker(context.Ordernumbertype, ordernumbertypes);
            context.Position = Checker(context.Position, positions);
            context.Positioncategory = Checker(context.Positioncategory, positioncategories);
            context.Positioncategoryrank = Checker(context.Positioncategoryrank, positioncategoryranks);
            context.Positionhistory = Checker(context.Positionhistory, positionhistories);
            context.Positionmrd = Checker(context.Positionmrd, positionmrds);
            context.Positiontype = Checker(context.Positiontype, positiontypes);
            context.Prooftype = Checker(context.Prooftype, prooftypes);
            context.Rank = Checker(context.Rank, ranks);
            context.Region = Checker(context.Region, regions);
            context.Rights = Checker(context.Rights, rights);
            context.Rightsstructure = Checker(context.Rightsstructure, rightsstructures);
            context.Role = Checker(context.Role, roles);
            context.Session = Checker(context.Session, sessions);
            context.Setpersondatatype = Checker(context.Setpersondatatype, setpersondatatypes);
            context.Sourceoffinancing = Checker(context.Sourceoffinancing, sourceoffinancings);
            context.Staffcomission = Checker(context.Staffcomission, staffcomissions);
            context.Streettype = Checker(context.Streettype, streettypes);
            context.Structure = Checker(context.Structure, structures);
            context.Structureregion = Checker(context.Structureregion, structureregions);
            context.Structuretype = Checker(context.Structuretype, structuretypes);
            context.Subject = Checker(context.Subject, subjects);
            context.Subjectcategory = Checker(context.Subjectcategory, subjectcategories);
            context.Subjectexport = Checker(context.Subjectexport, subjectexports);
            context.Subjectgender = Checker(context.Subjectgender, subjectgenders);
            context.Transfertype = Checker(context.Transfertype, transfertypes);
            context.User = Checker(context.User, users);

            context.SaveChangesAsync();
        }

        private Microsoft.EntityFrameworkCore.DbSet<Altrank> Checker(Microsoft.EntityFrameworkCore.DbSet<Altrank> database, List<Altrank> update)
        {
            List<Altrank> for_update = new List<Altrank>() { },
                for_add = new List<Altrank>() { };
            Dictionary<int, Altrank> dick = database.ToDictionary(r => r.Id);
            foreach(Altrank iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Altrankcondition> Checker(Microsoft.EntityFrameworkCore.DbSet<Altrankcondition> database, List<Altrankcondition> update)
        {
            List<Altrankcondition> for_update = new List<Altrankcondition>() { },
                for_add = new List<Altrankcondition>() { };
            Dictionary<int, Altrankcondition> dick = database.ToDictionary(r => r.Id);
            foreach (Altrankcondition iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Altrankconditiongroup> Checker(Microsoft.EntityFrameworkCore.DbSet<Altrankconditiongroup> database, List<Altrankconditiongroup> update)
        {
            List<Altrankconditiongroup> for_update = new List<Altrankconditiongroup>() { },
                for_add = new List<Altrankconditiongroup>() { };
            Dictionary<int, Altrankconditiongroup> dick = database.ToDictionary(r => r.Id);
            foreach (Altrankconditiongroup iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Area> Checker(Microsoft.EntityFrameworkCore.DbSet<Area> database, List<Area> update)
        {
            List<Area> for_update = new List<Area>() { },
                for_add = new List<Area>() { };
            Dictionary<int, Area> dick = database.ToDictionary(r => r.Id);
            foreach (Area iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Areaother> Checker(Microsoft.EntityFrameworkCore.DbSet<Areaother> database, List<Areaother> update)
        {
            List<Areaother> for_update = new List<Areaother>() { },
                for_add = new List<Areaother>() { };
            Dictionary<int, Areaother> dick = database.ToDictionary(r => r.Id);
            foreach (Areaother iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Citysubstate> Checker(Microsoft.EntityFrameworkCore.DbSet<Citysubstate> database, List<Citysubstate> update)
        {
            List<Citysubstate> for_update = new List<Citysubstate>() { },
                for_add = new List<Citysubstate>() { };
            Dictionary<int, Citysubstate> dick = database.ToDictionary(r => r.Id);
            foreach (Citysubstate iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Citytype> Checker(Microsoft.EntityFrameworkCore.DbSet<Citytype> database, List<Citytype> update)
        {
            List<Citytype> for_update = new List<Citytype>() { },
                for_add = new List<Citytype>() { };
            Dictionary<int, Citytype> dick = database.ToDictionary(r => r.Id);
            foreach (Citytype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Civildecree> Checker(Microsoft.EntityFrameworkCore.DbSet<Civildecree> database, List<Civildecree> update)
        {
            List<Civildecree> for_update = new List<Civildecree>() { },
                for_add = new List<Civildecree>() { };
            Dictionary<int, Civildecree> dick = database.ToDictionary(r => r.Id);
            foreach (Civildecree iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Country> Checker(Microsoft.EntityFrameworkCore.DbSet<Country> database, List<Country> update)
        {
            List<Country> for_update = new List<Country>() { },
                for_add = new List<Country>() { };
            Dictionary<int, Country> dick = database.ToDictionary(r => r.Id);
            foreach (Country iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Decree> Checker(Microsoft.EntityFrameworkCore.DbSet<Decree> database, List<Decree> update)
        {
            List<Decree> for_update = new List<Decree>() { },
                for_add = new List<Decree>() { };
            Dictionary<int, Decree> dick = database.ToDictionary(r => r.Id);
            foreach (Decree iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Decreeoperation> Checker(Microsoft.EntityFrameworkCore.DbSet<Decreeoperation> database, List<Decreeoperation> update)
        {
            List<Decreeoperation> for_update = new List<Decreeoperation>() { },
                for_add = new List<Decreeoperation>() { };
            Dictionary<int, Decreeoperation> dick = database.ToDictionary(r => r.Id);
            foreach (Decreeoperation iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Department> Checker(Microsoft.EntityFrameworkCore.DbSet<Department> database, List<Department> update)
        {
            List<Department> for_update = new List<Department>() { },
                for_add = new List<Department>() { };
            Dictionary<int, Department> dick = database.ToDictionary(r => r.Id);
            foreach (Department iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Departmentrename> Checker(Microsoft.EntityFrameworkCore.DbSet<Departmentrename> database, List<Departmentrename> update)
        {
            List<Departmentrename> for_update = new List<Departmentrename>() { },
                for_add = new List<Departmentrename>() { };
            Dictionary<int, Departmentrename> dick = database.ToDictionary(r => r.Id);
            foreach (Departmentrename iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Dismissalclauses> Checker(Microsoft.EntityFrameworkCore.DbSet<Dismissalclauses> database, List<Dismissalclauses> update)
        {
            List<Dismissalclauses> for_update = new List<Dismissalclauses>() { },
                for_add = new List<Dismissalclauses>() { };
            Dictionary<int, Dismissalclauses> dick = database.ToDictionary(r => r.Id);
            foreach (Dismissalclauses iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Drivercategory> Checker(Microsoft.EntityFrameworkCore.DbSet<Drivercategory> database, List<Drivercategory> update)
        {
            List<Drivercategory> for_update = new List<Drivercategory>() { },
                for_add = new List<Drivercategory>() { };
            Dictionary<int, Drivercategory> dick = database.ToDictionary(r => r.Id);
            foreach (Drivercategory iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Drivertype> Checker(Microsoft.EntityFrameworkCore.DbSet<Drivertype> database, List<Drivertype> update)
        {
            List<Drivertype> for_update = new List<Drivertype>() { },
                for_add = new List<Drivertype>() { };
            Dictionary<int, Drivertype> dick = database.ToDictionary(r => r.Id);
            foreach (Drivertype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Elementsubject> Checker(Microsoft.EntityFrameworkCore.DbSet<Elementsubject> database, List<Elementsubject> update)
        {
            List<Elementsubject> for_update = new List<Elementsubject>() { },
                for_add = new List<Elementsubject>() { };
            Dictionary<uint, Elementsubject> dick = database.ToDictionary(r => r.Id);
            foreach (Elementsubject iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Externalorderwhotype> Checker(Microsoft.EntityFrameworkCore.DbSet<Externalorderwhotype> database, List<Externalorderwhotype> update)
        {
            List<Externalorderwhotype> for_update = new List<Externalorderwhotype>() { },
                for_add = new List<Externalorderwhotype>() { };
            Dictionary<int, Externalorderwhotype> dick = database.ToDictionary(r => r.Id);
            foreach (Externalorderwhotype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Mrd> Checker(Microsoft.EntityFrameworkCore.DbSet<Mrd> database, List<Mrd> update)
        {
            List<Mrd> for_update = new List<Mrd>() { },
                for_add = new List<Mrd>() { };
            Dictionary<int, Mrd> dick = database.ToDictionary(r => r.Id);
            foreach (Mrd iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Ordernumbertype> Checker(Microsoft.EntityFrameworkCore.DbSet<Ordernumbertype> database, List<Ordernumbertype> update)
        {
            List<Ordernumbertype> for_update = new List<Ordernumbertype>() { },
                for_add = new List<Ordernumbertype>() { };
            Dictionary<int, Ordernumbertype> dick = database.ToDictionary(r => r.Id);
            foreach (Ordernumbertype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Position> Checker(Microsoft.EntityFrameworkCore.DbSet<Position> database, List<Position> update)
        {
            List<Position> for_update = new List<Position>() { },
                for_add = new List<Position>() { };
            Dictionary<int, Position> dick = database.ToDictionary(r => r.Id);
            foreach (Position iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Positioncategory> Checker(Microsoft.EntityFrameworkCore.DbSet<Positioncategory> database, List<Positioncategory> update)
        {
            List<Positioncategory> for_update = new List<Positioncategory>() { },
                for_add = new List<Positioncategory>() { };
            Dictionary<int, Positioncategory> dick = database.ToDictionary(r => r.Id);
            foreach (Positioncategory iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Positioncategoryrank> Checker(Microsoft.EntityFrameworkCore.DbSet<Positioncategoryrank> database, List<Positioncategoryrank> update)
        {
            List<Positioncategoryrank> for_update = new List<Positioncategoryrank>() { },
                for_add = new List<Positioncategoryrank>() { };
            Dictionary<int, Positioncategoryrank> dick = database.ToDictionary(r => r.Id);
            foreach (Positioncategoryrank iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Positionhistory> Checker(Microsoft.EntityFrameworkCore.DbSet<Positionhistory> database, List<Positionhistory> update)
        {
            List<Positionhistory> for_update = new List<Positionhistory>() { },
                for_add = new List<Positionhistory>() { };
            Dictionary<int, Positionhistory> dick = database.ToDictionary(r => r.Id);
            foreach (Positionhistory iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Positionmrd> Checker(Microsoft.EntityFrameworkCore.DbSet<Positionmrd> database, List<Positionmrd> update)
        {
            List<Positionmrd> for_update = new List<Positionmrd>() { },
                for_add = new List<Positionmrd>() { };
            Dictionary<int, Positionmrd> dick = database.ToDictionary(r => r.Id);
            foreach (Positionmrd iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Positiontype> Checker(Microsoft.EntityFrameworkCore.DbSet<Positiontype> database, List<Positiontype> update)
        {
            List<Positiontype> for_update = new List<Positiontype>() { },
                for_add = new List<Positiontype>() { };
            Dictionary<int, Positiontype> dick = database.ToDictionary(r => r.Id);
            foreach (Positiontype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Prooftype> Checker(Microsoft.EntityFrameworkCore.DbSet<Prooftype> database, List<Prooftype> update)
        {
            List<Prooftype> for_update = new List<Prooftype>() { },
                for_add = new List<Prooftype>() { };
            Dictionary<int, Prooftype> dick = database.ToDictionary(r => r.Id);
            foreach (Prooftype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Rank> Checker(Microsoft.EntityFrameworkCore.DbSet<Rank> database, List<Rank> update)
        {
            List<Rank> for_update = new List<Rank>() { },
                for_add = new List<Rank>() { };
            Dictionary<int, Rank> dick = database.ToDictionary(r => r.Id);
            foreach (Rank iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Region> Checker(Microsoft.EntityFrameworkCore.DbSet<Region> database, List<Region> update)
        {
            List<Region> for_update = new List<Region>() { },
                for_add = new List<Region>() { };
            Dictionary<int, Region> dick = database.ToDictionary(r => r.Id);
            foreach (Region iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Rights> Checker(Microsoft.EntityFrameworkCore.DbSet<Rights> database, List<Rights> update)
        {
            List<Rights> for_update = new List<Rights>() { },
                for_add = new List<Rights>() { };
            Dictionary<int, Rights> dick = database.ToDictionary(r => r.Id);
            foreach (Rights iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Rightsstructure> Checker(Microsoft.EntityFrameworkCore.DbSet<Rightsstructure> database, List<Rightsstructure> update)
        {
            List<Rightsstructure> for_update = new List<Rightsstructure>() { },
                for_add = new List<Rightsstructure>() { };
            Dictionary<int, Rightsstructure> dick = database.ToDictionary(r => r.Id);
            foreach (Rightsstructure iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Role> Checker(Microsoft.EntityFrameworkCore.DbSet<Role> database, List<Role> update)
        {
            List<Role> for_update = new List<Role>() { },
                for_add = new List<Role>() { };
            Dictionary<int, Role> dick = database.ToDictionary(r => r.Id);
            foreach (Role iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Session> Checker(Microsoft.EntityFrameworkCore.DbSet<Session> database, List<Session> update)
        {
            List<Session> for_update = new List<Session>() { },
                for_add = new List<Session>() { };
            Dictionary<string, Session> dick = database.ToDictionary(r => r.Id);
            foreach (Session iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Setpersondatatype> Checker(Microsoft.EntityFrameworkCore.DbSet<Setpersondatatype> database, List<Setpersondatatype> update)
        {
            List<Setpersondatatype> for_update = new List<Setpersondatatype>() { },
                for_add = new List<Setpersondatatype>() { };
            Dictionary<int, Setpersondatatype> dick = database.ToDictionary(r => r.Id);
            foreach (Setpersondatatype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Sourceoffinancing> Checker(Microsoft.EntityFrameworkCore.DbSet<Sourceoffinancing> database, List<Sourceoffinancing> update)
        {
            List<Sourceoffinancing> for_update = new List<Sourceoffinancing>() { },
                for_add = new List<Sourceoffinancing>() { };
            Dictionary<int, Sourceoffinancing> dick = database.ToDictionary(r => r.Id);
            foreach (Sourceoffinancing iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Staffcomission> Checker(Microsoft.EntityFrameworkCore.DbSet<Staffcomission> database, List<Staffcomission> update)
        {
            List<Staffcomission> for_update = new List<Staffcomission>() { },
                for_add = new List<Staffcomission>() { };
            Dictionary<int, Staffcomission> dick = database.ToDictionary(r => r.Id);
            foreach (Staffcomission iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Streettype> Checker(Microsoft.EntityFrameworkCore.DbSet<Streettype> database, List<Streettype> update)
        {
            List<Streettype> for_update = new List<Streettype>() { },
                for_add = new List<Streettype>() { };
            Dictionary<int, Streettype> dick = database.ToDictionary(r => r.Id);
            foreach (Streettype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Structure> Checker(Microsoft.EntityFrameworkCore.DbSet<Structure> database, List<Structure> update)
        {
            List<Structure> for_update = new List<Structure>() { },
                for_add = new List<Structure>() { };
            Dictionary<int, Structure> dick = database.ToDictionary(r => r.Id);
            foreach (Structure iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Structureregion> Checker(Microsoft.EntityFrameworkCore.DbSet<Structureregion> database, List<Structureregion> update)
        {
            List<Structureregion> for_update = new List<Structureregion>() { },
                for_add = new List<Structureregion>() { };
            Dictionary<int, Structureregion> dick = database.ToDictionary(r => r.Id);
            foreach (Structureregion iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Structuretype> Checker(Microsoft.EntityFrameworkCore.DbSet<Structuretype> database, List<Structuretype> update)
        {
            List<Structuretype> for_update = new List<Structuretype>() { },
                for_add = new List<Structuretype>() { };
            Dictionary<int, Structuretype> dick = database.ToDictionary(r => r.Id);
            foreach (Structuretype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Subject> Checker(Microsoft.EntityFrameworkCore.DbSet<Subject> database, List<Subject> update)
        {
            List<Subject> for_update = new List<Subject>() { },
                for_add = new List<Subject>() { };
            Dictionary<int, Subject> dick = database.ToDictionary(r => r.Id);
            foreach (Subject iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Subjectcategory> Checker(Microsoft.EntityFrameworkCore.DbSet<Subjectcategory> database, List<Subjectcategory> update)
        {
            List<Subjectcategory> for_update = new List<Subjectcategory>() { },
                for_add = new List<Subjectcategory>() { };
            Dictionary<int, Subjectcategory> dick = database.ToDictionary(r => r.Id);
            foreach (Subjectcategory iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Subjectexport> Checker(Microsoft.EntityFrameworkCore.DbSet<Subjectexport> database, List<Subjectexport> update)
        {
            List<Subjectexport> for_update = new List<Subjectexport>() { },
                for_add = new List<Subjectexport>() { };
            Dictionary<int, Subjectexport> dick = database.ToDictionary(r => r.Id);
            foreach (Subjectexport iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Subjectgender> Checker(Microsoft.EntityFrameworkCore.DbSet<Subjectgender> database, List<Subjectgender> update)
        {
            List<Subjectgender> for_update = new List<Subjectgender>() { },
                for_add = new List<Subjectgender>() { };
            Dictionary<int, Subjectgender> dick = database.ToDictionary(r => r.Id);
            foreach (Subjectgender iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<Transfertype> Checker(Microsoft.EntityFrameworkCore.DbSet<Transfertype> database, List<Transfertype> update)
        {
            List<Transfertype> for_update = new List<Transfertype>() { },
                for_add = new List<Transfertype>() { };
            Dictionary<int, Transfertype> dick = database.ToDictionary(r => r.Id);
            foreach (Transfertype iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }

        private Microsoft.EntityFrameworkCore.DbSet<User> Checker(Microsoft.EntityFrameworkCore.DbSet<User> database, List<User> update)
        {
            List<User> for_update = new List<User>() { },
                for_add = new List<User>() { };
            Dictionary<int, User> dick = database.ToDictionary(r => r.Id);
            foreach (User iter in update)
            {
                if (dick.ContainsKey(iter.Id))
                    for_update.Add(iter);
                else
                    for_add.Add(iter);
            }
            if (for_update.Count != 0)
                database.UpdateRange(for_update);
            if (for_add.Count != 0)
                database.AddRange(for_add);
            return database;
        }
    }
}
