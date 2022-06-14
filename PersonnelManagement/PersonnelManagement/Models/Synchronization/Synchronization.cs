using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonnelManagement.Models
{
    public class Synchronization
    {
        public List<Altrank> altranks { get; set; }
        public List<Altrankcondition> altrankconditions { get; set; }
        public List<Altrankconditiongroup> altrankconditiongroups { get; set; }
        public List<Area> areas { get; set; }
        public List<Areaother> areaothers { get; set; }
        public List<Citysubstate> citysubstates { get; set; }
        public List<Citytype> citytypes { get; set; }
        public List<Civildecree> civildecrees { get; set; }
        public List<Country> countries { get; set; }
        public List<Decree> decrees { get; set; }
        public List<Decreeoperation> decreeoperations { get; set; }
        public List<Department> departments { get; set; }
        public List<Departmentrename> departmentrenames { get; set; }
        public List<Dismissalclauses> dismissalclauses { get; set; }
        public List<Drivercategory> drivercategories { get; set; }
        public List<Drivertype> drivertypes { get; set; }
        public List<Elementsubject> elementsubjects { get; set; }
        public List<Externalorderwhotype> externalorderwhotypes { get; set; }
        public List<Mrd> mrds { get; set; }
        public List<Ordernumbertype> ordernumbertypes { get; set; }
        public List<Position> positions { get; set; }
        public List<Positioncategory> positioncategories { get; set; }
        public List<Positioncategoryrank> positioncategoryranks { get; set; }
        public List<Positionhistory> positionhistories { get; set; }
        public List<Positionmrd> positionmrds { get; set; }
        public List<Positiontype> positiontypes { get; set; }
        public List<Prooftype> prooftypes { get; set; }
        public List<Rank> ranks { get; set; }
        public List<Region> regions { get; set; }
        public List<Rights> rights { get; set; }
        public List<Rightsstructure> rightsstructures { get; set; }
        public List<Role> roles { get; set; }
        public List<Session> sessions { get; set; }
        public List<Setpersondatatype> setpersondatatypes { get; set; }
        public List<Sourceoffinancing> sourceoffinancings { get; set; }
        public List<Staffcomission> staffcomissions { get; set; }
        public List<Streettype> streettypes { get; set; }
        public List<Structure> structures { get; set; }
        public List<Structureregion> structureregions { get; set; }
        public List<Structuretype> structuretypes { get; set; }
        public List<Subject> subjects { get; set; }
        public List<Subjectcategory> subjectcategories { get; set; }
        public List<Subjectexport> subjectexports { get; set; }
        public List<Subjectgender> subjectgenders { get; set; }
        public List<Transfertype> transfertypes { get; set; }
        public List<User> users { get; set; }
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
            altranks                = new List<Altrank>(synchronization.altranks);
            altrankconditions       = new List<Altrankcondition>(synchronization.altrankconditions);
            altrankconditiongroups  = new List<Altrankconditiongroup>(synchronization.altrankconditiongroups);
            areas                   = new List<Area>(synchronization.areas);
            areaothers              = new List<Areaother>(synchronization.areaothers);
            citysubstates           = new List<Citysubstate>(synchronization.citysubstates);
            citytypes               = new List<Citytype>(synchronization.citytypes);
            civildecrees            = new List<Civildecree>(synchronization.civildecrees);
            countries               = new List<Country>(synchronization.countries);
            decrees                 = new List<Decree>(synchronization.decrees);
            decreeoperations        = new List<Decreeoperation>(synchronization.decreeoperations);
            departments             = new List<Department>(synchronization.departments);
            departmentrenames       = new List<Departmentrename>(synchronization.departmentrenames);
            dismissalclauses        = new List<Dismissalclauses>(synchronization.dismissalclauses);
            drivercategories        = new List<Drivercategory>(synchronization.drivercategories);
            drivertypes             = new List<Drivertype>(synchronization.drivertypes);
            elementsubjects         = new List<Elementsubject>(synchronization.elementsubjects);
            externalorderwhotypes   = new List<Externalorderwhotype>(synchronization.externalorderwhotypes);
            mrds                    = new List<Mrd>(synchronization.mrds);
            ordernumbertypes        = new List<Ordernumbertype>(synchronization.ordernumbertypes);
            positions               = new List<Position>(synchronization.positions);
            positioncategories      = new List<Positioncategory>(synchronization.positioncategories);
            positioncategoryranks   = new List<Positioncategoryrank>(synchronization.positioncategoryranks);
            positionhistories       = new List<Positionhistory>(synchronization.positionhistories);
            positionmrds            = new List<Positionmrd>(synchronization.positionmrds);
            positiontypes           = new List<Positiontype>(synchronization.positiontypes);
            prooftypes              = new List<Prooftype>(synchronization.prooftypes);
            ranks                   = new List<Rank>(synchronization.ranks);
            regions                 = new List<Region>(synchronization.regions);
            rights                  = new List<Rights>(synchronization.rights);
            rightsstructures        = new List<Rightsstructure>(synchronization.rightsstructures);
            roles                   = new List<Role>(synchronization.roles);
            sessions                = new List<Session>(synchronization.sessions);
            setpersondatatypes      = new List<Setpersondatatype>(synchronization.setpersondatatypes);
            sourceoffinancings      = new List<Sourceoffinancing>(synchronization.sourceoffinancings);
            staffcomissions         = new List<Staffcomission>(synchronization.staffcomissions);
            streettypes             = new List<Streettype>(synchronization.streettypes);
            structures              = new List<Structure>(synchronization.structures);
            structureregions        = new List<Structureregion>(synchronization.structureregions);
            structuretypes          = new List<Structuretype>(synchronization.structuretypes);
            subjects                = new List<Subject>(synchronization.subjects);
            subjectcategories       = new List<Subjectcategory>(synchronization.subjectcategories);
            subjectexports          = new List<Subjectexport>(synchronization.subjectexports);
            subjectgenders          = new List<Subjectgender>(synchronization.subjectgenders);
            transfertypes           = new List<Transfertype>(synchronization.transfertypes);
            users                   = new List<User>(synchronization.users);
        }
        public Synchronization(pmContext pmContext)
        {
            altranks                = new List<Altrank>(pmContext.Altrank.ToList());
            altrankconditions       = new List<Altrankcondition>(pmContext.Altrankcondition.ToList());
            altrankconditiongroups  = new List<Altrankconditiongroup>(pmContext.Altrankconditiongroup.ToList());
            areas                   = new List<Area>(pmContext.Area.ToList());
            areaothers              = new List<Areaother>(pmContext.Areaother.ToList());
            citysubstates           = new List<Citysubstate>(pmContext.Citysubstate.ToList());
            citytypes               = new List<Citytype>(pmContext.Citytype.ToList());
            civildecrees            = new List<Civildecree>(pmContext.Civildecree.ToList());
            countries               = new List<Country>(pmContext.Country.ToList());
            decrees                 = new List<Decree>(pmContext.Decree.ToList());
            decreeoperations        = new List<Decreeoperation>(pmContext.Decreeoperation.ToList());
            departments             = new List<Department>(pmContext.Department.ToList());
            departmentrenames       = new List<Departmentrename>(pmContext.Departmentrename.ToList());
            dismissalclauses        = new List<Dismissalclauses>(pmContext.Dismissalclauses.ToList());
            drivercategories        = new List<Drivercategory>(pmContext.Drivercategory.ToList());
            drivertypes             = new List<Drivertype>(pmContext.Drivertype.ToList());
            elementsubjects         = new List<Elementsubject>(pmContext.Elementsubject.ToList());
            externalorderwhotypes   = new List<Externalorderwhotype>(pmContext.Externalorderwhotype.ToList());
            mrds                    = new List<Mrd>(pmContext.Mrd.ToList());
            ordernumbertypes        = new List<Ordernumbertype>(pmContext.Ordernumbertype.ToList());
            positions               = new List<Position>(pmContext.Position.ToList());
            positioncategories      = new List<Positioncategory>(pmContext.Positioncategory.ToList());
            positioncategoryranks   = new List<Positioncategoryrank>(pmContext.Positioncategoryrank.ToList());
            positionhistories       = new List<Positionhistory>(pmContext.Positionhistory.ToList());
            positionmrds            = new List<Positionmrd>(pmContext.Positionmrd.ToList());
            positiontypes           = new List<Positiontype>(pmContext.Positiontype.ToList());
            prooftypes              = new List<Prooftype>(pmContext.Prooftype.ToList());
            ranks                   = new List<Rank>(pmContext.Rank.ToList());
            regions                 = new List<Region>(pmContext.Region.ToList());
            rights                  = new List<Rights>(pmContext.Rights.ToList());
            rightsstructures        = new List<Rightsstructure>(pmContext.Rightsstructure.ToList());
            roles                   = new List<Role>(pmContext.Role.ToList());
            sessions                = new List<Session>(pmContext.Session.ToList());
            setpersondatatypes      = new List<Setpersondatatype>(pmContext.Setpersondatatype.ToList());
            sourceoffinancings      = new List<Sourceoffinancing>(pmContext.Sourceoffinancing.ToList());
            staffcomissions         = new List<Staffcomission>(pmContext.Staffcomission.ToList());
            streettypes             = new List<Streettype>(pmContext.Streettype.ToList());
            structures              = new List<Structure>(pmContext.Structure.ToList());
            structureregions        = new List<Structureregion>(pmContext.Structureregion.ToList());
            structuretypes          = new List<Structuretype>(pmContext.Structuretype.ToList());
            subjects                = new List<Subject>(pmContext.Subject.ToList());
            subjectcategories       = new List<Subjectcategory>(pmContext.Subjectcategory.ToList());
            subjectexports          = new List<Subjectexport>(pmContext.Subjectexport.ToList());
            subjectgenders          = new List<Subjectgender>(pmContext.Subjectgender.ToList());
            transfertypes           = new List<Transfertype>(pmContext.Transfertype.ToList());
            users                   = new List<User>(pmContext.User.ToList());
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
