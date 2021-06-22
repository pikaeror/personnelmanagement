using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonnelManagement.Models
{
    public partial class pmContext : DbContext
    {
        public virtual DbSet<Academicvacation> Academicvacation { get; set; }
        public virtual DbSet<Altrank> Altrank { get; set; }
        public virtual DbSet<Altrankcondition> Altrankcondition { get; set; }
        public virtual DbSet<Altrankconditiongroup> Altrankconditiongroup { get; set; }
        public virtual DbSet<Appointtype> Appointtype { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Areaother> Areaother { get; set; }
        public virtual DbSet<Attestationtype> Attestationtype { get; set; }
        public virtual DbSet<Autobiographydata> Autobiographydata { get; set; }
        public virtual DbSet<Cabinetdata> Cabinetdata { get; set; }
        public virtual DbSet<Changedocumentstype> Changedocumentstype { get; set; }
        public virtual DbSet<Citysubstate> Citysubstate { get; set; }
        public virtual DbSet<Citytype> Citytype { get; set; }
        public virtual DbSet<Civildecree> Civildecree { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Declarationdata> Declarationdata { get; set; }
        public virtual DbSet<Declarationrelative> Declarationrelative { get; set; }
        public virtual DbSet<Declarationtabledata> Declarationtabledata { get; set; }
        public virtual DbSet<Decree> Decree { get; set; }
        public virtual DbSet<Decreeoperation> Decreeoperation { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Departmentrename> Departmentrename { get; set; }
        public virtual DbSet<Dismissalclauses> Dismissalclauses { get; set; }
        public virtual DbSet<Drivercategory> Drivercategory { get; set; }
        public virtual DbSet<Drivertype> Drivertype { get; set; }
        public virtual DbSet<Educationadditionaltype> Educationadditionaltype { get; set; }
        public virtual DbSet<Educationdocument> Educationdocument { get; set; }
        public virtual DbSet<Educationlevel> Educationlevel { get; set; }
        public virtual DbSet<Educationmaternity> Educationmaternity { get; set; }
        public virtual DbSet<Educationperiod> Educationperiod { get; set; }
        public virtual DbSet<Educationpositiontype> Educationpositiontype { get; set; }
        public virtual DbSet<Educationstage> Educationstage { get; set; }
        public virtual DbSet<Educationtype> Educationtype { get; set; }
        public virtual DbSet<Educationtypeblock> Educationtypeblock { get; set; }
        public virtual DbSet<Externalorderwhotype> Externalorderwhotype { get; set; }
        public virtual DbSet<Fire> Fire { get; set; }
        public virtual DbSet<Holiday> Holiday { get; set; }
        public virtual DbSet<Illcode> Illcode { get; set; }
        public virtual DbSet<Illregime> Illregime { get; set; }
        public virtual DbSet<Interrupttype> Interrupttype { get; set; }
        public virtual DbSet<Jobtype> Jobtype { get; set; }
        public virtual DbSet<Languageskill> Languageskill { get; set; }
        public virtual DbSet<Languagetype> Languagetype { get; set; }
        public virtual DbSet<Mailexplorer> Mailexplorer { get; set; }
        public virtual DbSet<Mailfolder> Mailfolder { get; set; }
        public virtual DbSet<Mrd> Mrd { get; set; }
        public virtual DbSet<Normativ> Normativ { get; set; }
        public virtual DbSet<Ordernumbertype> Ordernumbertype { get; set; }
        public virtual DbSet<Penalty> Penalty { get; set; }
        public virtual DbSet<Permissiontype> Permissiontype { get; set; }
        public virtual DbSet<Person> Person { get; set; }
        public virtual DbSet<Personadditionalagreement> Personadditionalagreement { get; set; }
        public virtual DbSet<Personattestation> Personattestation { get; set; }
        public virtual DbSet<Personchangedocuments> Personchangedocuments { get; set; }
        public virtual DbSet<Personcontract> Personcontract { get; set; }
        public virtual DbSet<Persondecree> Persondecree { get; set; }
        public virtual DbSet<Persondecreeblock> Persondecreeblock { get; set; }
        public virtual DbSet<Persondecreeblockintro> Persondecreeblockintro { get; set; }
        public virtual DbSet<Persondecreeblocksub> Persondecreeblocksub { get; set; }
        public virtual DbSet<Persondecreeblocksubtype> Persondecreeblocksubtype { get; set; }
        public virtual DbSet<Persondecreeblocktype> Persondecreeblocktype { get; set; }
        public virtual DbSet<Persondecreeexcerpt> Persondecreeexcerpt { get; set; }
        public virtual DbSet<Persondecreelevel> Persondecreelevel { get; set; }
        public virtual DbSet<Persondecreeoperation> Persondecreeoperation { get; set; }
        public virtual DbSet<Persondecreetype> Persondecreetype { get; set; }
        public virtual DbSet<Persondecreeunit> Persondecreeunit { get; set; }
        public virtual DbSet<Persondecreeuserhistory> Persondecreeuserhistory { get; set; }
        public virtual DbSet<Persondispanserization> Persondispanserization { get; set; }
        public virtual DbSet<Persondriver> Persondriver { get; set; }
        public virtual DbSet<Personeducation> Personeducation { get; set; }
        public virtual DbSet<Personelection> Personelection { get; set; }
        public virtual DbSet<Personfire> Personfire { get; set; }
        public virtual DbSet<Personill> Personill { get; set; }
        public virtual DbSet<Personillcode> Personillcode { get; set; }
        public virtual DbSet<Personjob> Personjob { get; set; }
        public virtual DbSet<Personjobprivelege> Personjobprivelege { get; set; }
        public virtual DbSet<Personjobprivelegeperiod> Personjobprivelegeperiod { get; set; }
        public virtual DbSet<Personlanguage> Personlanguage { get; set; }
        public virtual DbSet<Personpenalty> Personpenalty { get; set; }
        public virtual DbSet<Personpermission> Personpermission { get; set; }
        public virtual DbSet<Personpfl> Personpfl { get; set; }
        public virtual DbSet<Personphoto> Personphoto { get; set; }
        public virtual DbSet<Personphysical> Personphysical { get; set; }
        public virtual DbSet<Personprivelege> Personprivelege { get; set; }
        public virtual DbSet<Personrank> Personrank { get; set; }
        public virtual DbSet<Personrelative> Personrelative { get; set; }
        public virtual DbSet<Personreward> Personreward { get; set; }
        public virtual DbSet<Personscience> Personscience { get; set; }
        public virtual DbSet<Persontransfer> Persontransfer { get; set; }
        public virtual DbSet<Personvacation> Personvacation { get; set; }
        public virtual DbSet<Personvvk> Personvvk { get; set; }
        public virtual DbSet<Personworktrip> Personworktrip { get; set; }
        public virtual DbSet<Physicalfield> Physicalfield { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Positioncategory> Positioncategory { get; set; }
        public virtual DbSet<Positioncategoryrank> Positioncategoryrank { get; set; }
        public virtual DbSet<Positionhistory> Positionhistory { get; set; }
        public virtual DbSet<Positionmrd> Positionmrd { get; set; }
        public virtual DbSet<Positiontype> Positiontype { get; set; }
        public virtual DbSet<Profiledata> Profiledata { get; set; }
        public virtual DbSet<Profilerelatives> Profilerelatives { get; set; }
        public virtual DbSet<Prooftype> Prooftype { get; set; }
        public virtual DbSet<Pseducation> Pseducation { get; set; }
        public virtual DbSet<Pswork> Pswork { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Relativetype> Relativetype { get; set; }
        public virtual DbSet<Reward> Reward { get; set; }
        public virtual DbSet<Rewardmoney> Rewardmoney { get; set; }
        public virtual DbSet<Rewardtype> Rewardtype { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<Rightsstructure> Rightsstructure { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Servicecoef> Servicecoef { get; set; }
        public virtual DbSet<Servicefeature> Servicefeature { get; set; }
        public virtual DbSet<Servicetype> Servicetype { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Setpersondatatype> Setpersondatatype { get; set; }
        public virtual DbSet<Sheetdata> Sheetdata { get; set; }
        public virtual DbSet<Sheetpolitics> Sheetpolitics { get; set; }
        public virtual DbSet<Sourceoffinancing> Sourceoffinancing { get; set; }
        public virtual DbSet<Streettype> Streettype { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<Structureregion> Structureregion { get; set; }
        public virtual DbSet<Structuretype> Structuretype { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Subjectcategory> Subjectcategory { get; set; }
        public virtual DbSet<Subjectexport> Subjectexport { get; set; }
        public virtual DbSet<Subjectgender> Subjectgender { get; set; }
        public virtual DbSet<Transfertype> Transfertype { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Vacationmilitary> Vacationmilitary { get; set; }
        public virtual DbSet<Vacationtype> Vacationtype { get; set; }

        public pmContext(DbContextOptions<pmContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;userid=admin;treattinyasboolean=True;password=FVSH6S6aB4srgReT;database=pm;convert zero datetime=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Academicvacation>(entity =>
            {
                entity.ToTable("academicvacation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Personeducation)
                    .HasColumnName("personeducation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PersoneducationId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Altrank>(entity =>
            {
                entity.ToTable("altrank");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Altrankcondition)
                    .HasColumnName("altrankcondition")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Primary)
                    .HasColumnName("primary")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Altrankcondition>(entity =>
            {
                entity.ToTable("altrankcondition");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Group)
                    .HasColumnName("group")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Altrankconditiongroup>(entity =>
            {
                entity.ToTable("altrankconditiongroup");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(400);
            });

            modelBuilder.Entity<Appointtype>(entity =>
            {
                entity.ToTable("appointtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Civil)
                    .HasColumnName("civil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(135)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Area>(entity =>
            {
                entity.ToTable("area");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Other)
                    .HasColumnName("other")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Region)
                    .HasColumnName("region")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Areaother>(entity =>
            {
                entity.ToTable("areaother");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Attestationtype>(entity =>
            {
                entity.ToTable("attestationtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Autobiographydata>(entity =>
            {
                entity.ToTable("autobiographydata");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Autobiographybiography)
                    .IsRequired()
                    .HasColumnName("autobiographybiography")
                    .HasColumnType("varchar(14000)")
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographyeducationdocnum)
                    .IsRequired()
                    .HasColumnName("autobiographyeducationdocnum")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographyhomephone)
                    .IsRequired()
                    .HasColumnName("autobiographyhomephone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographylockunlock)
                    .HasColumnName("autobiographylockunlock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Autobiographymilitaryid)
                    .IsRequired()
                    .HasColumnName("autobiographymilitaryid")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographymobilephone)
                    .IsRequired()
                    .HasColumnName("autobiographymobilephone")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographypassport)
                    .IsRequired()
                    .HasColumnName("autobiographypassport")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographyregistration)
                    .IsRequired()
                    .HasColumnName("autobiographyregistration")
                    .HasMaxLength(3000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographysignature)
                    .HasColumnName("autobiographysignature")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Autobiographywhogiveeducationdoc)
                    .IsRequired()
                    .HasColumnName("autobiographywhogiveeducationdoc")
                    .HasMaxLength(3000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographywhogivemilitaryid)
                    .IsRequired()
                    .HasColumnName("autobiographywhogivemilitaryid")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographywhogivepassport)
                    .IsRequired()
                    .HasColumnName("autobiographywhogivepassport")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Autobiographyworkphone)
                    .IsRequired()
                    .HasColumnName("autobiographyworkphone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Cabinetdata>(entity =>
            {
                entity.ToTable("cabinetdata");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Accesscode)
                    .IsRequired()
                    .HasColumnName("accesscode")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Consent)
                    .HasColumnName("consent")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Creationdate)
                    .HasColumnName("creationdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Creatorid)
                    .HasColumnName("creatorid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Denyreason)
                    .IsRequired()
                    .HasColumnName("denyreason")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Employeesid)
                    .HasColumnName("employeesid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Reasonid)
                    .HasColumnName("reasonid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Userind)
                    .IsRequired()
                    .HasColumnName("userind")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasColumnName("username")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Userpatronymic)
                    .IsRequired()
                    .HasColumnName("userpatronymic")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Usersurname)
                    .IsRequired()
                    .HasColumnName("usersurname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Changedocumentstype>(entity =>
            {
                entity.ToTable("changedocumentstype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changefathername)
                    .HasColumnName("changefathername")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changename)
                    .HasColumnName("changename")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changesurname)
                    .HasColumnName("changesurname")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Citysubstate>(entity =>
            {
                entity.ToTable("citysubstate");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Citytype>(entity =>
            {
                entity.ToTable("citytype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Civildecree>(entity =>
            {
                entity.ToTable("civildecree");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Civildecree1)
                    .HasColumnName("civildecree")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Civildecreedate)
                    .HasColumnName("civildecreedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Civildecreenumber)
                    .IsRequired()
                    .HasColumnName("civildecreenumber")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Civilrankhigh)
                    .HasColumnName("civilrankhigh")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Civilranklow)
                    .HasColumnName("civilranklow")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Decertificate)
                    .HasColumnName("decertificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decertificatedate)
                    .HasColumnName("decertificatedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Delete)
                    .HasColumnName("delete")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Replacedbycivil)
                    .HasColumnName("replacedbycivil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Replacedbycivildate)
                    .HasColumnName("replacedbycivildate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Replacedbycivildatelimit)
                    .HasColumnName("replacedbycivildatelimit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Country>(entity =>
            {
                entity.ToTable("country");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Declarationdata>(entity =>
            {
                entity.ToTable("declarationdata");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationaddress)
                    .IsRequired()
                    .HasColumnName("declarationaddress")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationcabinetid)
                    .HasColumnName("declarationcabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationdateindocument)
                    .HasColumnName("declarationdateindocument")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declarationdob)
                    .HasColumnName("declarationdob")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declarationdocnumber)
                    .IsRequired()
                    .HasColumnName("declarationdocnumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationdocseries)
                    .IsRequired()
                    .HasColumnName("declarationdocseries")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationhomephonenumber)
                    .IsRequired()
                    .HasColumnName("declarationhomephonenumber")
                    .HasMaxLength(15)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationind)
                    .IsRequired()
                    .HasColumnName("declarationind")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationlockunlock)
                    .HasColumnName("declarationlockunlock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationmobilephonenumber)
                    .IsRequired()
                    .HasColumnName("declarationmobilephonenumber")
                    .HasMaxLength(20)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationname)
                    .IsRequired()
                    .HasColumnName("declarationname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationpatronymic)
                    .IsRequired()
                    .HasColumnName("declarationpatronymic")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationsignature)
                    .HasColumnName("declarationsignature")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declarationsurname)
                    .IsRequired()
                    .HasColumnName("declarationsurname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationtypedoc)
                    .IsRequired()
                    .HasColumnName("declarationtypedoc")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationtypeform)
                    .HasColumnName("declarationtypeform")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationwhogivedoc)
                    .IsRequired()
                    .HasColumnName("declarationwhogivedoc")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationwork)
                    .IsRequired()
                    .HasColumnName("declarationwork")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationworkphonenumber)
                    .IsRequired()
                    .HasColumnName("declarationworkphonenumber")
                    .HasMaxLength(15)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F1s1p31)
                    .IsRequired()
                    .HasColumnName("f1s1p31")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F1s1p32)
                    .IsRequired()
                    .HasColumnName("f1s1p32")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F1s1p33)
                    .IsRequired()
                    .HasColumnName("f1s1p33")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F1s3p1)
                    .IsRequired()
                    .HasColumnName("f1s3p1")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F1s3p2)
                    .HasColumnName("f1s3p2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.F2s4p1)
                    .IsRequired()
                    .HasColumnName("f2s4p1")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F2s5p1)
                    .IsRequired()
                    .HasColumnName("f2s5p1")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.F3s3p1)
                    .IsRequired()
                    .HasColumnName("f3s3p1")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Declarationrelative>(entity =>
            {
                entity.ToTable("declarationrelative");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationdobrelative)
                    .HasColumnName("declarationdobrelative")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declarationfullnamerelative)
                    .IsRequired()
                    .HasColumnName("declarationfullnamerelative")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationrelativerelation)
                    .IsRequired()
                    .HasColumnName("declarationrelativerelation")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Declarationtabledata>(entity =>
            {
                entity.ToTable("declarationtabledata");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationamount)
                    .IsRequired()
                    .HasColumnName("declarationamount")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationarea)
                    .IsRequired()
                    .HasColumnName("declarationarea")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationcabinetid)
                    .HasColumnName("declarationcabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationcardate)
                    .HasColumnName("declarationcardate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationcarmodel)
                    .IsRequired()
                    .HasColumnName("declarationcarmodel")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationcurrency)
                    .IsRequired()
                    .HasColumnName("declarationcurrency")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationdatein)
                    .HasColumnName("declarationdatein")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationform)
                    .HasColumnName("declarationform")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationfulldate)
                    .HasColumnName("declarationfulldate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declarationlocation)
                    .IsRequired()
                    .HasColumnName("declarationlocation")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationnameorganization)
                    .IsRequired()
                    .HasColumnName("declarationnameorganization")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationpoint)
                    .HasColumnName("declarationpoint")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationsection)
                    .HasColumnName("declarationsection")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Declarationtypeincome)
                    .IsRequired()
                    .HasColumnName("declarationtypeincome")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationwhattime)
                    .IsRequired()
                    .HasColumnName("declarationwhattime")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Declarationwhoprovided)
                    .IsRequired()
                    .HasColumnName("declarationwhoprovided")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Decree>(entity =>
            {
                entity.ToTable("decree");

                entity.HasIndex(e => e.Dateactive)
                    .HasName("DATEACTIVE");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Signed)
                    .HasName("SIGNED");

                entity.HasIndex(e => new { e.Signed, e.Dateactive })
                    .HasName("SDA");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateactive)
                    .HasColumnName("dateactive")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datesigned)
                    .HasColumnName("datesigned")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Declined)
                    .HasColumnName("declined")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Historycal)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(1500);

                entity.Property(e => e.Nickname)
                    .HasColumnName("nickname")
                    .HasMaxLength(900);

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasMaxLength(400);

                entity.Property(e => e.Signed)
                    .HasColumnName("signed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Decreeoperation>(entity =>
            {
                entity.ToTable("decreeoperation");

                entity.HasIndex(e => e.Decree)
                    .HasName("DECREE");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Subject)
                    .HasName("SUBJECT");

                entity.HasIndex(e => new { e.Deleted, e.Subject })
                    .HasName("DS");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changed)
                    .HasColumnName("changed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changedtype)
                    .HasColumnName("changedtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Dateactive)
                    .HasColumnName("dateactive")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datecustom)
                    .HasColumnName("datecustom")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject)
                    .HasColumnName("subject")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Department>(entity =>
            {
                entity.ToTable("department");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateactive)
                    .HasColumnName("dateactive")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Dateinactive)
                    .HasColumnName("dateinactive")
                    .HasColumnType("datetime");

                entity.Property(e => e.Department1)
                    .HasColumnName("department")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(800);

                entity.Property(e => e.Nameshort)
                    .IsRequired()
                    .HasColumnName("nameshort")
                    .HasMaxLength(800)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Notlogged)
                    .HasColumnName("notlogged")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Departmentrename>(entity =>
            {
                entity.ToTable("departmentrename");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decreeoperation)
                    .HasColumnName("decreeoperation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Newname)
                    .IsRequired()
                    .HasColumnName("newname")
                    .HasMaxLength(800);

                entity.Property(e => e.Oldname)
                    .IsRequired()
                    .HasColumnName("oldname")
                    .HasMaxLength(800);
            });

            modelBuilder.Entity<Dismissalclauses>(entity =>
            {
                entity.ToTable("dismissalclauses");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Paragraph)
                    .HasColumnName("paragraph")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Persondecreeblocktype)
                    .HasColumnName("persondecreeblocktype")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Subparagraph)
                    .HasColumnName("subparagraph")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Titleofarticles)
                    .HasColumnName("titleofarticles")
                    .HasMaxLength(350);

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Drivercategory>(entity =>
            {
                entity.ToTable("drivercategory");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Drivertype>(entity =>
            {
                entity.ToTable("drivertype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationadditionaltype>(entity =>
            {
                entity.ToTable("educationadditionaltype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationdocument>(entity =>
            {
                entity.ToTable("educationdocument");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationlevel>(entity =>
            {
                entity.ToTable("educationlevel");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Levelcomment)
                    .IsRequired()
                    .HasColumnName("levelcomment")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Levelname)
                    .IsRequired()
                    .HasColumnName("levelname")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationmaternity>(entity =>
            {
                entity.ToTable("educationmaternity");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Personeducation)
                    .HasColumnName("personeducation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PersoneducationId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Educationperiod>(entity =>
            {
                entity.ToTable("educationperiod");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Course)
                    .HasColumnName("course")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationpositiontype)
                    .HasColumnName("educationpositiontype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationtypeblock)
                    .HasColumnName("educationtypeblock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.EducationtypeblockId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Ordernum)
                    .IsRequired()
                    .HasColumnName("ordernum")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Platoon)
                    .HasColumnName("platoon")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rank)
                    .IsRequired()
                    .HasColumnName("rank")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ranktype)
                    .IsRequired()
                    .HasColumnName("ranktype")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Service)
                    .HasColumnName("service")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Educationpositiontype>(entity =>
            {
                entity.ToTable("educationpositiontype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fulltimeonly)
                    .HasColumnName("fulltimeonly")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationstage>(entity =>
            {
                entity.ToTable("educationstage");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationtype>(entity =>
            {
                entity.ToTable("educationtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Educationtypeblock>(entity =>
            {
                entity.ToTable("educationtypeblock");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Educationtype)
                    .HasColumnName("educationtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.IsEnded)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Personeducation)
                    .HasColumnName("personeducation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PersoneducationId)
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Externalorderwhotype>(entity =>
            {
                entity.ToTable("externalorderwhotype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Fire>(entity =>
            {
                entity.ToTable("fire");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Civil)
                    .HasColumnName("civil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Point)
                    .HasColumnName("point")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pointsubpoint)
                    .IsRequired()
                    .HasColumnName("pointsubpoint")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Selectdescription)
                    .IsRequired()
                    .HasColumnName("selectdescription")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subpoint)
                    .HasColumnName("subpoint")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Type)
                    .IsRequired()
                    .HasColumnName("type")
                    .HasMaxLength(60)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Holiday>(entity =>
            {
                entity.ToTable("holiday");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Permanent)
                    .HasColumnName("permanent")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");
            });

            modelBuilder.Entity<Illcode>(entity =>
            {
                entity.ToTable("illcode");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(800)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Illregime>(entity =>
            {
                entity.ToTable("illregime");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Interrupttype>(entity =>
            {
                entity.ToTable("interrupttype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Point)
                    .HasColumnName("point")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pointsubpoint)
                    .IsRequired()
                    .HasColumnName("pointsubpoint")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Selectdescription)
                    .IsRequired()
                    .HasColumnName("selectdescription")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subpoint)
                    .HasColumnName("subpoint")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Jobtype>(entity =>
            {
                entity.ToTable("jobtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Languageskill>(entity =>
            {
                entity.ToTable("languageskill");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Languagetype>(entity =>
            {
                entity.ToTable("languagetype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Mailexplorer>(entity =>
            {
                entity.ToTable("mailexplorer");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.AccessForReading)
                    .HasColumnName("access_for_reading")
                    .HasMaxLength(450);

                entity.Property(e => e.DetaSend)
                    .HasColumnName("deta_send")
                    .HasMaxLength(450);

                entity.Property(e => e.FolderCreator).HasColumnName("folder_creator");

                entity.Property(e => e.FolderOwner).HasColumnName("folder_owner");

                entity.Property(e => e.LastCountOwner)
                    .HasColumnName("last_count_owner")
                    .HasMaxLength(450);

                entity.Property(e => e.LastDateOpen)
                    .HasColumnName("last_date_open")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Mailfolder>(entity =>
            {
                entity.HasKey(e => e.Idmailfolder);

                entity.ToTable("mailfolder");

                entity.HasIndex(e => e.Idmailfolder)
                    .HasName("idmailfolder_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Idmailfolder)
                    .HasColumnName("idmailfolder")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Mrd>(entity =>
            {
                entity.ToTable("mrd");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300);

                entity.Property(e => e.Short)
                    .HasColumnName("short")
                    .HasMaxLength(90);
            });

            modelBuilder.Entity<Normativ>(entity =>
            {
                entity.ToTable("normativ");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Ordernumbertype>(entity =>
            {
                entity.ToTable("ordernumbertype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Penalty>(entity =>
            {
                entity.ToTable("penalty");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Permissiontype>(entity =>
            {
                entity.ToTable("permissiontype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450);
            });

            modelBuilder.Entity<Person>(entity =>
            {
                entity.ToTable("person");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Birthadditional)
                    .IsRequired()
                    .HasColumnName("birthadditional")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcity)
                    .IsRequired()
                    .HasColumnName("birthcity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcitysubstate)
                    .HasColumnName("birthcitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Birthcitytype)
                    .IsRequired()
                    .HasColumnName("birthcitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcountry)
                    .IsRequired()
                    .HasColumnName("birthcountry")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'Республика Беларусь'");

                entity.Property(e => e.Birthdate)
                    .HasColumnName("birthdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Birthlocation)
                    .IsRequired()
                    .HasColumnName("birthlocation")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthstate)
                    .IsRequired()
                    .HasColumnName("birthstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthsubstate)
                    .IsRequired()
                    .HasColumnName("birthsubstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername)
                    .IsRequired()
                    .HasColumnName("fathername")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername2)
                    .IsRequired()
                    .HasColumnName("fathername2")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername3)
                    .IsRequired()
                    .HasColumnName("fathername3")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername4)
                    .IsRequired()
                    .HasColumnName("fathername4")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername5)
                    .IsRequired()
                    .HasColumnName("fathername5")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathername6)
                    .IsRequired()
                    .HasColumnName("fathername6")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fathernamesubject)
                    .HasColumnName("fathernamesubject")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Gender)
                    .IsRequired()
                    .HasColumnName("gender")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("'Мужской'");

                entity.Property(e => e.Gendersubject)
                    .HasColumnName("gendersubject")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Liveadditional)
                    .IsRequired()
                    .HasColumnName("liveadditional")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecity)
                    .IsRequired()
                    .HasColumnName("livecity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecitysubstate)
                    .HasColumnName("livecitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Livecitytype)
                    .IsRequired()
                    .HasColumnName("livecitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecountry)
                    .IsRequired()
                    .HasColumnName("livecountry")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'Республика Беларусь'");

                entity.Property(e => e.Liveflat)
                    .IsRequired()
                    .HasColumnName("liveflat")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livehouse)
                    .IsRequired()
                    .HasColumnName("livehouse")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livehousing)
                    .IsRequired()
                    .HasColumnName("livehousing")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestate)
                    .IsRequired()
                    .HasColumnName("livestate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestatenum)
                    .HasColumnName("livestatenum")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Livestreet)
                    .IsRequired()
                    .HasColumnName("livestreet")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestreettype)
                    .IsRequired()
                    .HasColumnName("livestreettype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livesubstate)
                    .IsRequired()
                    .HasColumnName("livesubstate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livesubstatenum)
                    .HasColumnName("livesubstatenum")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Maritalstatus)
                    .IsRequired()
                    .HasColumnName("maritalstatus")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Namesubject)
                    .HasColumnName("namesubject")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Nationality)
                    .IsRequired()
                    .HasColumnName("nationality")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("'Белорус'");

                entity.Property(e => e.Numpersonal)
                    .IsRequired()
                    .HasColumnName("numpersonal")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Passportdateend)
                    .HasColumnName("passportdateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Passportdatestart)
                    .HasColumnName("passportdatestart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Passportid)
                    .IsRequired()
                    .HasColumnName("passportid")
                    .HasMaxLength(135)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Passportnum)
                    .IsRequired()
                    .HasColumnName("passportnum")
                    .HasMaxLength(135)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Photo)
                    .HasColumnName("photo")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Registeradditional)
                    .IsRequired()
                    .HasColumnName("registeradditional")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registercity)
                    .IsRequired()
                    .HasColumnName("registercity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registercitysubstate)
                    .HasColumnName("registercitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Registercitytype)
                    .IsRequired()
                    .HasColumnName("registercitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registercountry)
                    .IsRequired()
                    .HasColumnName("registercountry")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'Республика Беларусь'");

                entity.Property(e => e.Registerflat)
                    .IsRequired()
                    .HasColumnName("registerflat")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registerhouse)
                    .IsRequired()
                    .HasColumnName("registerhouse")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registerhousing)
                    .IsRequired()
                    .HasColumnName("registerhousing")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registerstate)
                    .IsRequired()
                    .HasColumnName("registerstate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registerstatenum)
                    .HasColumnName("registerstatenum")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Registerstreet)
                    .IsRequired()
                    .HasColumnName("registerstreet")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registerstreettype)
                    .IsRequired()
                    .HasColumnName("registerstreettype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registersubstate)
                    .IsRequired()
                    .HasColumnName("registersubstate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Registersubstatenum)
                    .HasColumnName("registersubstatenum")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Removed)
                    .HasColumnName("removed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Science)
                    .IsRequired()
                    .HasColumnName("science")
                    .HasMaxLength(2000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sciencerank)
                    .IsRequired()
                    .HasColumnName("sciencerank")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surname2)
                    .IsRequired()
                    .HasColumnName("surname2")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surname3)
                    .IsRequired()
                    .HasColumnName("surname3")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surname4)
                    .IsRequired()
                    .HasColumnName("surname4")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surname5)
                    .IsRequired()
                    .HasColumnName("surname5")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surname6)
                    .IsRequired()
                    .HasColumnName("surname6")
                    .HasMaxLength(130)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surnameother)
                    .IsRequired()
                    .HasColumnName("surnameother")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Surnamesubject)
                    .HasColumnName("surnamesubject")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Wound)
                    .IsRequired()
                    .HasColumnName("wound")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personadditionalagreement>(entity =>
            {
                entity.ToTable("personadditionalagreement");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Contract)
                    .HasColumnName("contract")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.PersoncontractId)
                    .HasColumnName("personcontractId")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personattestation>(entity =>
            {
                entity.ToTable("personattestation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Attestationtype)
                    .HasColumnName("attestationtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Recomendation)
                    .IsRequired()
                    .HasColumnName("recomendation")
                    .HasMaxLength(4500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasMaxLength(4500)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personchangedocuments>(entity =>
            {
                entity.ToTable("personchangedocuments");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changedocumentstype)
                    .HasColumnName("changedocumentstype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Introtext)
                    .IsRequired()
                    .HasColumnName("introtext")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Text)
                    .IsRequired()
                    .HasColumnName("text")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personcontract>(entity =>
            {
                entity.ToTable("personcontract");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateend)
                    .HasColumnName("dateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pay)
                    .HasColumnName("pay")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Payvalue)
                    .HasColumnName("payvalue")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sourceoffinancing)
                    .HasColumnName("sourceoffinancing")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Stateservicedays)
                    .HasColumnName("stateservicedays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Stateservicemonths)
                    .HasColumnName("stateservicemonths")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Stateserviceyears)
                    .HasColumnName("stateserviceyears")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Vacationdays)
                    .HasColumnName("vacationdays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecree>(entity =>
            {
                entity.ToTable("persondecree");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Datecreated)
                    .HasColumnName("datecreated")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datesigned)
                    .HasColumnName("datesigned")
                    .HasColumnType("datetime");

                entity.Property(e => e.Mailexplorerid)
                    .HasColumnName("mailexplorerid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Numbertype)
                    .IsRequired()
                    .HasColumnName("numbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Owner)
                    .HasColumnName("owner")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreelevel)
                    .HasColumnName("persondecreelevel")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Signed)
                    .HasColumnName("signed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Transfer)
                    .HasColumnName("transfer")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecreeblock>(entity =>
            {
                entity.ToTable("persondecreeblock");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Intro)
                    .IsRequired()
                    .HasColumnName("intro")
                    .HasMaxLength(3000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nonperson)
                    .IsRequired()
                    .HasColumnName("nonperson")
                    .HasMaxLength(1350)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionarray1)
                    .IsRequired()
                    .HasColumnName("optionarray1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionarrayperson)
                    .IsRequired()
                    .HasColumnName("optionarrayperson")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optiondate1)
                    .HasColumnName("optiondate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate2)
                    .HasColumnName("optiondate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate3)
                    .HasColumnName("optiondate3")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate4)
                    .HasColumnName("optiondate4")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate5)
                    .HasColumnName("optiondate5")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate6)
                    .HasColumnName("optiondate6")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate7)
                    .HasColumnName("optiondate7")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate8)
                    .HasColumnName("optiondate8")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optionnumber1)
                    .HasColumnName("optionnumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber10)
                    .HasColumnName("optionnumber10")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber11)
                    .HasColumnName("optionnumber11")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber2)
                    .HasColumnName("optionnumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber3)
                    .HasColumnName("optionnumber3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber4)
                    .HasColumnName("optionnumber4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber5)
                    .HasColumnName("optionnumber5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber6)
                    .HasColumnName("optionnumber6")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber7)
                    .HasColumnName("optionnumber7")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber8)
                    .HasColumnName("optionnumber8")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber9)
                    .HasColumnName("optionnumber9")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionstring1)
                    .IsRequired()
                    .HasColumnName("optionstring1")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring2)
                    .IsRequired()
                    .HasColumnName("optionstring2")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring3)
                    .IsRequired()
                    .HasColumnName("optionstring3")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring4)
                    .IsRequired()
                    .HasColumnName("optionstring4")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring5)
                    .IsRequired()
                    .HasColumnName("optionstring5")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring6)
                    .IsRequired()
                    .HasColumnName("optionstring6")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring7)
                    .IsRequired()
                    .HasColumnName("optionstring7")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring8)
                    .IsRequired()
                    .HasColumnName("optionstring8")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Persondecree)
                    .HasColumnName("persondecree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocksub)
                    .HasColumnName("persondecreeblocksub")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocktype)
                    .HasColumnName("persondecreeblocktype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber1)
                    .HasColumnName("subvaluenumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber2)
                    .HasColumnName("subvaluenumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluestring1)
                    .IsRequired()
                    .HasColumnName("subvaluestring1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subvaluestring2)
                    .IsRequired()
                    .HasColumnName("subvaluestring2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondecreeblockintro>(entity =>
            {
                entity.ToTable("persondecreeblockintro");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Persondecree)
                    .HasColumnName("persondecree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblock)
                    .HasColumnName("persondecreeblock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecreeblocksub>(entity =>
            {
                entity.ToTable("persondecreeblocksub");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Intro)
                    .HasColumnName("intro")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Introtext)
                    .IsRequired()
                    .HasColumnName("introtext")
                    .HasMaxLength(3000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Parentpersondecreeblocksub)
                    .HasColumnName("parentpersondecreeblocksub")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecree)
                    .HasColumnName("persondecree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblock)
                    .HasColumnName("persondecreeblock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblockintro)
                    .HasColumnName("persondecreeblockintro")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocksubtype)
                    .HasColumnName("persondecreeblocksubtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluedate1)
                    .HasColumnName("subvaluedate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Subvaluedate2)
                    .HasColumnName("subvaluedate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.Subvaluenumber1)
                    .HasColumnName("subvaluenumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber2)
                    .HasColumnName("subvaluenumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber3)
                    .HasColumnName("subvaluenumber3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber4)
                    .HasColumnName("subvaluenumber4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluestring1)
                    .IsRequired()
                    .HasColumnName("subvaluestring1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subvaluestring2)
                    .IsRequired()
                    .HasColumnName("subvaluestring2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subvaluestring3)
                    .IsRequired()
                    .HasColumnName("subvaluestring3")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subvaluestring4)
                    .IsRequired()
                    .HasColumnName("subvaluestring4")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondecreeblocksubtype>(entity =>
            {
                entity.ToTable("persondecreeblocksubtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(135)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocktype)
                    .HasColumnName("persondecreeblocktype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecreeblocktype>(entity =>
            {
                entity.ToTable("persondecreeblocktype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecreeexcerpt>(entity =>
            {
                entity.ToTable("persondecreeexcerpt");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CreatorId)
                    .HasColumnName("creator_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Datacreated)
                    .HasColumnName("datacreated")
                    .HasColumnType("datetime");

                entity.Property(e => e.Dataopens)
                    .HasColumnName("dataopens")
                    .HasColumnType("datetime");

                entity.Property(e => e.Datasend)
                    .HasColumnName("datasend")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.FirstOpensId)
                    .HasColumnName("first_opens_id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Openflags)
                    .HasColumnName("openflags")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeoperations)
                    .IsRequired()
                    .HasColumnName("persondecreeoperations")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persondecreelevel>(entity =>
            {
                entity.ToTable("persondecreelevel");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondecreeoperation>(entity =>
            {
                entity.ToTable("persondecreeoperation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Creator)
                    .HasColumnName("creator")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decreeexcerpt)
                    .IsRequired()
                    .HasColumnName("decreeexcerpt")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Index)
                    .HasColumnName("index")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Intro)
                    .IsRequired()
                    .HasColumnName("intro")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nonperson)
                    .IsRequired()
                    .HasColumnName("nonperson")
                    .HasMaxLength(1350)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionarray1)
                    .IsRequired()
                    .HasColumnName("optionarray1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionarrayperson)
                    .IsRequired()
                    .HasColumnName("optionarrayperson")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optiondate1)
                    .HasColumnName("optiondate1")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate2)
                    .HasColumnName("optiondate2")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate3)
                    .HasColumnName("optiondate3")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate4)
                    .HasColumnName("optiondate4")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate5)
                    .HasColumnName("optiondate5")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate6)
                    .HasColumnName("optiondate6")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate7)
                    .HasColumnName("optiondate7")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optiondate8)
                    .HasColumnName("optiondate8")
                    .HasColumnType("datetime");

                entity.Property(e => e.Optionnumber1)
                    .HasColumnName("optionnumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber10)
                    .HasColumnName("optionnumber10")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber11)
                    .HasColumnName("optionnumber11")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber2)
                    .HasColumnName("optionnumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber3)
                    .HasColumnName("optionnumber3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber4)
                    .HasColumnName("optionnumber4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber5)
                    .HasColumnName("optionnumber5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber6)
                    .HasColumnName("optionnumber6")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber7)
                    .HasColumnName("optionnumber7")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber8)
                    .HasColumnName("optionnumber8")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber9)
                    .HasColumnName("optionnumber9")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionstring1)
                    .IsRequired()
                    .HasColumnName("optionstring1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring2)
                    .IsRequired()
                    .HasColumnName("optionstring2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring3)
                    .IsRequired()
                    .HasColumnName("optionstring3")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring4)
                    .IsRequired()
                    .HasColumnName("optionstring4")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring5)
                    .IsRequired()
                    .HasColumnName("optionstring5")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring6)
                    .IsRequired()
                    .HasColumnName("optionstring6")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring7)
                    .IsRequired()
                    .HasColumnName("optionstring7")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring8)
                    .IsRequired()
                    .HasColumnName("optionstring8")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring9)
                    .IsRequired()
                    .HasColumnName("optionstring9")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecree)
                    .HasColumnName("persondecree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblock)
                    .HasColumnName("persondecreeblock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblockintro)
                    .HasColumnName("persondecreeblockintro")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocksub)
                    .HasColumnName("persondecreeblocksub")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocksubtype)
                    .HasColumnName("persondecreeblocksubtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Persondecreeblocktype)
                    .HasColumnName("persondecreeblocktype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Priorityintro)
                    .HasColumnName("priorityintro")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subjectid)
                    .HasColumnName("subjectid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subjecttype)
                    .HasColumnName("subjecttype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber1)
                    .HasColumnName("subvaluenumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluenumber2)
                    .HasColumnName("subvaluenumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subvaluestring1)
                    .IsRequired()
                    .HasColumnName("subvaluestring1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subvaluestring2)
                    .IsRequired()
                    .HasColumnName("subvaluestring2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondecreetype>(entity =>
            {
                entity.ToTable("persondecreetype");

                entity.HasIndex(e => e.Id)
                    .HasName("шв_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondecreeunit>(entity =>
            {
                entity.ToTable("persondecreeunit");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Decrees)
                    .HasColumnName("decrees")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Unitdecree)
                    .HasColumnName("unitdecree")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Persondecreeuserhistory>(entity =>
            {
                entity.ToTable("persondecreeuserhistory");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasMaxLength(45);

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Persondispanserization>(entity =>
            {
                entity.ToTable("persondispanserization");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Group)
                    .HasColumnName("group")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Persondriver>(entity =>
            {
                entity.ToTable("persondriver");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateend)
                    .HasColumnName("dateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Drivercategory)
                    .HasColumnName("drivercategory")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Drivertype)
                    .HasColumnName("drivertype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Series)
                    .IsRequired()
                    .HasColumnName("series")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personeducation>(entity =>
            {
                entity.ToTable("personeducation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Academicvacation)
                    .HasColumnName("academicvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Cadet)
                    .HasColumnName("cadet")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Citytype)
                    .IsRequired()
                    .HasColumnName("citytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Dateend)
                    .HasColumnName("dateend")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Documentnumber)
                    .IsRequired()
                    .HasColumnName("documentnumber")
                    .HasMaxLength(145)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Documentseries)
                    .IsRequired()
                    .HasColumnName("documentseries")
                    .HasMaxLength(145)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Educationadditionaltype)
                    .HasColumnName("educationadditionaltype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationdocument)
                    .HasColumnName("educationdocument")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationlevel)
                    .HasColumnName("educationlevel")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationstage)
                    .HasColumnName("educationstage")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Educationtype)
                    .HasColumnName("educationtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Faculty)
                    .IsRequired()
                    .HasColumnName("faculty")
                    .HasMaxLength(300)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Interrupted)
                    .HasColumnName("interrupted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Interruptorderdate)
                    .HasColumnName("interruptorderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Interruptordernumber)
                    .IsRequired()
                    .HasColumnName("interruptordernumber")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Interruptordernumbertype)
                    .IsRequired()
                    .HasColumnName("interruptordernumbertype")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Interruptorderreason)
                    .IsRequired()
                    .HasColumnName("interruptorderreason")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Interruptorderwho)
                    .IsRequired()
                    .HasColumnName("interruptorderwho")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(200)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Main)
                    .HasColumnName("main")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Maternityvacation)
                    .HasColumnName("maternityvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nameasjobfull)
                    .IsRequired()
                    .HasColumnName("nameasjobfull")
                    .HasMaxLength(1255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nameasjobplace)
                    .IsRequired()
                    .HasColumnName("nameasjobplace")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nameasjobposition)
                    .IsRequired()
                    .HasColumnName("nameasjobposition")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasColumnName("qualification")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rating)
                    .HasColumnName("rating")
                    .HasColumnType("decimal(10,2)")
                    .HasDefaultValueSql("'0.00'");

                entity.Property(e => e.Speciality)
                    .IsRequired()
                    .HasColumnName("speciality")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasColumnName("state")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ucp)
                    .HasColumnName("ucp")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personelection>(entity =>
            {
                entity.ToTable("personelection");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Electiondate)
                    .HasColumnName("electiondate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Electiondateend)
                    .HasColumnName("electiondateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Electionwhat)
                    .IsRequired()
                    .HasColumnName("electionwhat")
                    .HasMaxLength(555)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Electionwho)
                    .IsRequired()
                    .HasColumnName("electionwho")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Location)
                    .IsRequired()
                    .HasColumnName("location")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personfire>(entity =>
            {
                entity.ToTable("personfire");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cloth)
                    .HasColumnName("cloth")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Firetype)
                    .HasColumnName("firetype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Reward)
                    .HasColumnName("reward")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personill>(entity =>
            {
                entity.ToTable("personill");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateend)
                    .HasColumnName("dateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Illcode)
                    .HasColumnName("illcode")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Illregime)
                    .HasColumnName("illregime")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Illtype)
                    .HasColumnName("illtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Illwho)
                    .IsRequired()
                    .HasColumnName("illwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Privelege)
                    .HasColumnName("privelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personillcode>(entity =>
            {
                entity.ToTable("personillcode");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Illcode)
                    .HasColumnName("illcode")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Personill)
                    .HasColumnName("personill")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personjob>(entity =>
            {
                entity.ToTable("personjob");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Actual)
                    .HasColumnName("actual")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fireorderdate)
                    .HasColumnName("fireorderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Fireorderid)
                    .HasColumnName("fireorderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Fireordernumber)
                    .IsRequired()
                    .HasColumnName("fireordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fireordernumbertype)
                    .IsRequired()
                    .HasColumnName("fireordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fireorderwho)
                    .IsRequired()
                    .HasColumnName("fireorderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fireorderwhoid)
                    .HasColumnName("fireorderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Jobplace)
                    .IsRequired()
                    .HasColumnName("jobplace")
                    .HasMaxLength(800)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Jobposition)
                    .IsRequired()
                    .HasColumnName("jobposition")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Jobpositionplace)
                    .IsRequired()
                    .HasColumnName("jobpositionplace")
                    .HasMaxLength(1250)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Jobtype)
                    .HasColumnName("jobtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Manual)
                    .HasColumnName("manual")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mchs)
                    .HasColumnName("mchs")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Positionnametree)
                    .IsRequired()
                    .HasColumnName("positionnametree")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Positiontoselect)
                    .HasColumnName("positiontoselect")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Privelege)
                    .HasColumnName("privelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Servicecoef)
                    .HasColumnName("servicecoef")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Servicefeature)
                    .HasColumnName("servicefeature")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Serviceorder)
                    .IsRequired()
                    .HasColumnName("serviceorder")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Serviceplace)
                    .IsRequired()
                    .HasColumnName("serviceplace")
                    .HasMaxLength(800)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Servicetype)
                    .HasColumnName("servicetype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Servicetypestr)
                    .IsRequired()
                    .HasColumnName("servicetypestr")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Startcustom)
                    .HasColumnName("startcustom")
                    .HasColumnType("datetime");

                entity.Property(e => e.Statecivil)
                    .HasColumnName("statecivil")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Statecivilend)
                    .HasColumnName("statecivilend")
                    .HasColumnType("datetime");

                entity.Property(e => e.Statecivilstart)
                    .HasColumnName("statecivilstart")
                    .HasColumnType("datetime");

                entity.Property(e => e.Vacationdays)
                    .HasColumnName("vacationdays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'28'");
            });

            modelBuilder.Entity<Personjobprivelege>(entity =>
            {
                entity.ToTable("personjobprivelege");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Coef)
                    .HasColumnName("coef")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Documentdate)
                    .HasColumnName("documentdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Documentnumber)
                    .IsRequired()
                    .HasColumnName("documentnumber")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Documentorder)
                    .IsRequired()
                    .HasColumnName("documentorder")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordertype)
                    .HasColumnName("ordertype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Personjob)
                    .HasColumnName("personjob")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Proofdate)
                    .HasColumnName("proofdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Proofnumber)
                    .IsRequired()
                    .HasColumnName("proofnumber")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Prooftext)
                    .IsRequired()
                    .HasColumnName("prooftext")
                    .HasMaxLength(2000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Prooftype)
                    .HasColumnName("prooftype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Personjobprivelegeperiod>(entity =>
            {
                entity.ToTable("personjobprivelegeperiod");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Personjobprivelege)
                    .HasColumnName("personjobprivelege")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<Personlanguage>(entity =>
            {
                entity.ToTable("personlanguage");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Languageskill)
                    .HasColumnName("languageskill")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Languagetype)
                    .HasColumnName("languagetype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personpenalty>(entity =>
            {
                entity.ToTable("personpenalty");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255);

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Penalty)
                    .HasColumnName("penalty")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Violation)
                    .IsRequired()
                    .HasColumnName("violation")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personpermission>(entity =>
            {
                entity.ToTable("personpermission");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateend)
                    .HasColumnName("dateend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Permissiontype)
                    .HasColumnName("permissiontype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personpfl>(entity =>
            {
                entity.ToTable("personpfl");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Document).HasColumnName("document");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person64header)
                    .IsRequired()
                    .HasColumnName("person64header")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personphoto>(entity =>
            {
                entity.ToTable("personphoto");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person64header)
                    .IsRequired()
                    .HasColumnName("person64header")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Photo).HasColumnName("photo");
            });

            modelBuilder.Entity<Personphysical>(entity =>
            {
                entity.ToTable("personphysical");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Physicaldate)
                    .HasColumnName("physicaldate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<Personprivelege>(entity =>
            {
                entity.ToTable("personprivelege");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personrank>(entity =>
            {
                entity.ToTable("personrank");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Datestart)
                    .HasColumnName("datestart")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decreedate)
                    .HasColumnName("decreedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Decreeid)
                    .HasColumnName("decreeid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decreenumber)
                    .IsRequired()
                    .HasColumnName("decreenumber")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Decreenumbertype)
                    .IsRequired()
                    .HasColumnName("decreenumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rankstring)
                    .IsRequired()
                    .HasColumnName("rankstring")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Structure)
                    .IsRequired()
                    .HasColumnName("structure")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Structureid)
                    .HasColumnName("structureid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personrelative>(entity =>
            {
                entity.ToTable("personrelative");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Birthadditional)
                    .IsRequired()
                    .HasColumnName("birthadditional")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcity)
                    .IsRequired()
                    .HasColumnName("birthcity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcitysubstate)
                    .HasColumnName("birthcitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Birthcitytype)
                    .IsRequired()
                    .HasColumnName("birthcitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthcountry)
                    .IsRequired()
                    .HasColumnName("birthcountry")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'Республика Беларусь'");

                entity.Property(e => e.Birthday)
                    .HasColumnName("birthday")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Birthplace)
                    .IsRequired()
                    .HasColumnName("birthplace")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthstate)
                    .IsRequired()
                    .HasColumnName("birthstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Birthsubstate)
                    .IsRequired()
                    .HasColumnName("birthsubstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Death)
                    .HasColumnName("death")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Deathadditional)
                    .IsRequired()
                    .HasColumnName("deathadditional")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Deathcity)
                    .IsRequired()
                    .HasColumnName("deathcity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Deathcitysubstate)
                    .HasColumnName("deathcitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Deathcitytype)
                    .IsRequired()
                    .HasColumnName("deathcitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Deathcountry)
                    .IsRequired()
                    .HasColumnName("deathcountry")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("'Республика Беларусь'");

                entity.Property(e => e.Deathnodata)
                    .HasColumnName("deathnodata")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Deathstate)
                    .IsRequired()
                    .HasColumnName("deathstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Deathsubstate)
                    .IsRequired()
                    .HasColumnName("deathsubstate")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fio)
                    .IsRequired()
                    .HasColumnName("fio")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fioother)
                    .IsRequired()
                    .HasColumnName("fioother")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Jobplace)
                    .IsRequired()
                    .HasColumnName("jobplace")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Jobposition)
                    .IsRequired()
                    .HasColumnName("jobposition")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecity)
                    .IsRequired()
                    .HasColumnName("livecity")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecitysubstate)
                    .HasColumnName("livecitysubstate")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Livecitytype)
                    .IsRequired()
                    .HasColumnName("livecitytype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livecountry)
                    .IsRequired()
                    .HasColumnName("livecountry")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Liveflat)
                    .IsRequired()
                    .HasColumnName("liveflat")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livehouse)
                    .IsRequired()
                    .HasColumnName("livehouse")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livehousing)
                    .IsRequired()
                    .HasColumnName("livehousing")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestate)
                    .IsRequired()
                    .HasColumnName("livestate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestreet)
                    .IsRequired()
                    .HasColumnName("livestreet")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livestreettype)
                    .IsRequired()
                    .HasColumnName("livestreettype")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Livesubstate)
                    .IsRequired()
                    .HasColumnName("livesubstate")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nodata)
                    .HasColumnName("nodata")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Relativetype)
                    .HasColumnName("relativetype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personreward>(entity =>
            {
                entity.ToTable("personreward");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Area)
                    .HasColumnName("area")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Areaother)
                    .HasColumnName("areaother")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Externalordertype)
                    .HasColumnName("externalordertype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Externalorderwhotype)
                    .IsRequired()
                    .HasColumnName("externalorderwhotype")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionnumber1)
                    .HasColumnName("optionnumber1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionnumber2)
                    .HasColumnName("optionnumber2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Optionstring1)
                    .IsRequired()
                    .HasColumnName("optionstring1")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Optionstring2)
                    .IsRequired()
                    .HasColumnName("optionstring2")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Order)
                    .IsRequired()
                    .HasColumnName("order")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Reward)
                    .HasColumnName("reward")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rewarddate)
                    .HasColumnName("rewarddate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Rewardtype)
                    .HasColumnName("rewardtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personscience>(entity =>
            {
                entity.ToTable("personscience");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sciencedate)
                    .HasColumnName("sciencedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Sciencedescription)
                    .IsRequired()
                    .HasColumnName("sciencedescription")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sciencediplom)
                    .IsRequired()
                    .HasColumnName("sciencediplom")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sciencetype)
                    .HasColumnName("sciencetype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Persontransfer>(entity =>
            {
                entity.ToTable("persontransfer");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.End)
                    .HasColumnName("end")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasMaxLength(1350)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Start)
                    .HasColumnName("start")
                    .HasColumnType("datetime");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Structuretoselect)
                    .HasColumnName("structuretoselect")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Transfertype)
                    .HasColumnName("transfertype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personvacation>(entity =>
            {
                entity.ToTable("personvacation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Allowend)
                    .HasColumnName("allowend")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Allowstart)
                    .HasColumnName("allowstart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Cancel)
                    .HasColumnName("cancel")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Cancelcontinue)
                    .HasColumnName("cancelcontinue")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Canceldate)
                    .HasColumnName("canceldate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Canceldateend)
                    .HasColumnName("canceldateend")
                    .HasColumnType("datetime");

                entity.Property(e => e.Compensation)
                    .HasColumnName("compensation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Compensationdate)
                    .HasColumnName("compensationdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Compensationdays)
                    .HasColumnName("compensationdays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Compensationnumber)
                    .IsRequired()
                    .HasColumnName("compensationnumber")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Duration)
                    .HasColumnName("duration")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Holidays)
                    .HasColumnName("holidays")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Trip)
                    .HasColumnName("trip")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Vacationmilitary)
                    .HasColumnName("vacationmilitary")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Vacationtype)
                    .HasColumnName("vacationtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Personvvk>(entity =>
            {
                entity.ToTable("personvvk");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Personworktrip>(entity =>
            {
                entity.ToTable("personworktrip");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Country)
                    .HasColumnName("country")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Days)
                    .HasColumnName("days")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orderdate)
                    .HasColumnName("orderdate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Orderid)
                    .HasColumnName("orderid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ordernumber)
                    .IsRequired()
                    .HasColumnName("ordernumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Ordernumbertype)
                    .IsRequired()
                    .HasColumnName("ordernumbertype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwho)
                    .IsRequired()
                    .HasColumnName("orderwho")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Orderwhoid)
                    .HasColumnName("orderwhoid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Person)
                    .HasColumnName("person")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Privelege)
                    .HasColumnName("privelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Reason)
                    .IsRequired()
                    .HasColumnName("reason")
                    .HasMaxLength(2000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Tripdate)
                    .HasColumnName("tripdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<Physicalfield>(entity =>
            {
                entity.ToTable("physicalfield");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Normativ)
                    .HasColumnName("normativ")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Personphysical)
                    .HasColumnName("personphysical")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Result)
                    .IsRequired()
                    .HasColumnName("result")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("position");

                entity.HasIndex(e => e.Positiontype)
                    .HasName("POSITIONTYPE");

                entity.HasIndex(e => e.Structure)
                    .HasName("STRUCTURE");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Altrank)
                    .HasColumnName("altrank")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Cap)
                    .HasColumnName("cap")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Civildecree)
                    .HasColumnName("civildecree")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Civildecreedate)
                    .HasColumnName("civildecreedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Civildecreenumber)
                    .IsRequired()
                    .HasColumnName("civildecreenumber")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Civilrankhigh)
                    .HasColumnName("civilrankhigh")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Civilranklow)
                    .HasColumnName("civilranklow")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Curator)
                    .HasColumnName("curator")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Curatorlist)
                    .IsRequired()
                    .HasColumnName("curatorlist")
                    .HasMaxLength(3000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Dateactive)
                    .HasColumnName("dateactive")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Dateinactive)
                    .HasColumnName("dateinactive")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decertificate)
                    .HasColumnName("decertificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decertificatedate)
                    .HasColumnName("decertificatedate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Head)
                    .HasColumnName("head")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Headid)
                    .HasColumnName("headid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Notice)
                    .IsRequired()
                    .HasColumnName("notice")
                    .HasMaxLength(3600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Opchs)
                    .HasColumnName("opchs")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Origin)
                    .HasColumnName("origin")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Part)
                    .HasColumnName("part")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Partval)
                    .HasColumnName("partval")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Positioncategory)
                    .HasColumnName("positioncategory")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Positiontype)
                    .HasColumnName("positiontype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Replacedbycivil)
                    .HasColumnName("replacedbycivil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Replacedbycivildate)
                    .HasColumnName("replacedbycivildate")
                    .HasColumnType("datetime");

                entity.Property(e => e.Replacedbycivildatelimit)
                    .HasColumnName("replacedbycivildatelimit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Replacedbycivilpositioncategory)
                    .HasColumnName("replacedbycivilpositioncategory")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Replacedbycivilpositiontype)
                    .HasColumnName("replacedbycivilpositiontype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sourceoffinancing)
                    .HasColumnName("sourceoffinancing")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject1)
                    .HasColumnName("subject1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject10)
                    .HasColumnName("subject10")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject11)
                    .HasColumnName("subject11")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject12)
                    .HasColumnName("subject12")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject13)
                    .HasColumnName("subject13")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject14)
                    .HasColumnName("subject14")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject15)
                    .HasColumnName("subject15")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject16)
                    .HasColumnName("subject16")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject17)
                    .HasColumnName("subject17")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject18)
                    .HasColumnName("subject18")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject19)
                    .HasColumnName("subject19")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject2)
                    .HasColumnName("subject2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject20)
                    .HasColumnName("subject20")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject3)
                    .HasColumnName("subject3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject4)
                    .HasColumnName("subject4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject5)
                    .HasColumnName("subject5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject6)
                    .HasColumnName("subject6")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject7)
                    .HasColumnName("subject7")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject8)
                    .HasColumnName("subject8")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject9)
                    .HasColumnName("subject9")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Positioncategory>(entity =>
            {
                entity.ToTable("positioncategory");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Categoryranklink)
                    .HasColumnName("categoryranklink")
                    .HasColumnType("int(10)")
                    .HasDefaultValueSql("'6'");

                entity.Property(e => e.Civil)
                    .HasColumnName("civil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Classcap)
                    .HasColumnName("classcap")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(600)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Officer)
                    .HasColumnName("officer")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Replacenonofficer)
                    .HasColumnName("replacenonofficer")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Replaceofficer)
                    .HasColumnName("replaceofficer")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Variable)
                    .HasColumnName("variable")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Positioncategoryrank>(entity =>
            {
                entity.ToTable("positioncategoryrank");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Rank)
                    .HasName("rank_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(600);

                entity.Property(e => e.Rank).HasColumnName("rank");
            });

            modelBuilder.Entity<Positionhistory>(entity =>
            {
                entity.ToTable("positionhistory");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decreeoperation)
                    .HasColumnName("decreeoperation")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Deleted)
                    .HasColumnName("deleted")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Previous)
                    .HasColumnName("previous")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Positionmrd>(entity =>
            {
                entity.ToTable("positionmrd");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Mrd)
                    .HasColumnName("mrd")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Positiontype>(entity =>
            {
                entity.ToTable("positiontype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(900);

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nameshort)
                    .IsRequired()
                    .HasColumnName("nameshort")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'50'");

                entity.Property(e => e.Subject1)
                    .HasColumnName("subject1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject10)
                    .HasColumnName("subject10")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject11)
                    .HasColumnName("subject11")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject12)
                    .HasColumnName("subject12")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject13)
                    .HasColumnName("subject13")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject14)
                    .HasColumnName("subject14")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject15)
                    .HasColumnName("subject15")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject16)
                    .HasColumnName("subject16")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject17)
                    .HasColumnName("subject17")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject18)
                    .HasColumnName("subject18")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject19)
                    .HasColumnName("subject19")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject2)
                    .HasColumnName("subject2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject20)
                    .HasColumnName("subject20")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject3)
                    .HasColumnName("subject3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject4)
                    .HasColumnName("subject4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject5)
                    .HasColumnName("subject5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject6)
                    .HasColumnName("subject6")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject7)
                    .HasColumnName("subject7")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject8)
                    .HasColumnName("subject8")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject9)
                    .HasColumnName("subject9")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Profiledata>(entity =>
            {
                entity.ToTable("profiledata");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Profileaddress)
                    .IsRequired()
                    .HasColumnName("profileaddress")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilearmy)
                    .IsRequired()
                    .HasColumnName("profilearmy")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profileawards)
                    .IsRequired()
                    .HasColumnName("profileawards")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profiledob)
                    .HasColumnName("profiledob")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Profiledriverlicensecategory)
                    .IsRequired()
                    .HasColumnName("profiledriverlicensecategory")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profiledriverlicensedate)
                    .HasColumnName("profiledriverlicensedate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Profiledriverlicenseexperience)
                    .IsRequired()
                    .HasColumnName("profiledriverlicenseexperience")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profiledriverlicensenumber)
                    .IsRequired()
                    .HasColumnName("profiledriverlicensenumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profiledriverlicenseseries)
                    .IsRequired()
                    .HasColumnName("profiledriverlicenseseries")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profileeducation)
                    .IsRequired()
                    .HasColumnName("profileeducation")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilehomephone)
                    .IsRequired()
                    .HasColumnName("profilehomephone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilelanguage)
                    .IsRequired()
                    .HasColumnName("profilelanguage")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilelockunlock)
                    .HasColumnName("profilelockunlock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Profilemobilephone)
                    .IsRequired()
                    .HasColumnName("profilemobilephone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilename)
                    .IsRequired()
                    .HasColumnName("profilename")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilenationality)
                    .IsRequired()
                    .HasColumnName("profilenationality")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profileothersurnames)
                    .IsRequired()
                    .HasColumnName("profileothersurnames")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepassportdate)
                    .HasColumnName("profilepassportdate")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Profilepassportind)
                    .IsRequired()
                    .HasColumnName("profilepassportind")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepassportnumber)
                    .IsRequired()
                    .HasColumnName("profilepassportnumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepassportseries)
                    .IsRequired()
                    .HasColumnName("profilepassportseries")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepassportwhogive)
                    .IsRequired()
                    .HasColumnName("profilepassportwhogive")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepatronymic)
                    .IsRequired()
                    .HasColumnName("profilepatronymic")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepob)
                    .IsRequired()
                    .HasColumnName("profilepob")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilepolitics)
                    .IsRequired()
                    .HasColumnName("profilepolitics")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profileresponsibility)
                    .IsRequired()
                    .HasColumnName("profileresponsibility")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilescholasticdegree)
                    .IsRequired()
                    .HasColumnName("profilescholasticdegree")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilesignature)
                    .HasColumnName("profilesignature")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Profilesport)
                    .IsRequired()
                    .HasColumnName("profilesport")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilesurname)
                    .IsRequired()
                    .HasColumnName("profilesurname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profiletreatise)
                    .IsRequired()
                    .HasColumnName("profiletreatise")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profileworkphone)
                    .IsRequired()
                    .HasColumnName("profileworkphone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Profilerelatives>(entity =>
            {
                entity.ToTable("profilerelatives");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Profilerelativesdegree)
                    .IsRequired()
                    .HasColumnName("profilerelativesdegree")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilerelativesdob)
                    .HasColumnName("profilerelativesdob")
                    .HasColumnType("datetime");

                entity.Property(e => e.Profilerelativesfullname)
                    .IsRequired()
                    .HasColumnName("profilerelativesfullname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilerelativeslocation)
                    .IsRequired()
                    .HasColumnName("profilerelativeslocation")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilerelativespob)
                    .IsRequired()
                    .HasColumnName("profilerelativespob")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Profilerelativeswork)
                    .IsRequired()
                    .HasColumnName("profilerelativeswork")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Prooftype>(entity =>
            {
                entity.ToTable("prooftype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Pseducation>(entity =>
            {
                entity.ToTable("pseducation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Facultyeducation)
                    .IsRequired()
                    .HasColumnName("facultyeducation")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Formeducation)
                    .IsRequired()
                    .HasColumnName("formeducation")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Organization)
                    .IsRequired()
                    .HasColumnName("organization")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Organizationcity)
                    .IsRequired()
                    .HasColumnName("organizationcity")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Specialtyeducation)
                    .IsRequired()
                    .HasColumnName("specialtyeducation")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Yearend)
                    .HasColumnName("yearend")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Yearstart)
                    .HasColumnName("yearstart")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Pswork>(entity =>
            {
                entity.ToTable("pswork");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Locationwork)
                    .IsRequired()
                    .HasColumnName("locationwork")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Organizationwork)
                    .IsRequired()
                    .HasColumnName("organizationwork")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Workend)
                    .HasColumnName("workend")
                    .HasColumnType("datetime");

                entity.Property(e => e.Workendday)
                    .IsRequired()
                    .HasColumnName("workendday")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Workendmonth)
                    .IsRequired()
                    .HasColumnName("workendmonth")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Workendyear)
                    .HasColumnName("workendyear")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Workstart)
                    .HasColumnName("workstart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Workstartday)
                    .IsRequired()
                    .HasColumnName("workstartday")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Workstartmonth)
                    .IsRequired()
                    .HasColumnName("workstartmonth")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Workstartyear)
                    .HasColumnName("workstartyear")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("rank");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Order)
                    .HasName("ORDER");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dateactive)
                    .HasColumnName("dateactive")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Dateinactive)
                    .HasColumnName("dateinactive")
                    .HasColumnType("datetime");

                entity.Property(e => e.Decreeupfast)
                    .HasColumnName("decreeupfast")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Decreeupone)
                    .HasColumnName("decreeupone")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(90);

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Notlogged)
                    .HasColumnName("notlogged")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Positioncategory)
                    .HasColumnName("positioncategory")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("region");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.City)
                    .HasColumnName("city")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(300)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Relativetype>(entity =>
            {
                entity.ToTable("relativetype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Reward>(entity =>
            {
                entity.ToTable("reward");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rewardtype)
                    .HasColumnName("rewardtype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Rewardmoney>(entity =>
            {
                entity.ToTable("rewardmoney");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rewardmoneytype)
                    .IsRequired()
                    .HasColumnName("rewardmoneytype")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rewardmoneytypeplural)
                    .IsRequired()
                    .HasColumnName("rewardmoneytypeplural")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Rewardtype>(entity =>
            {
                entity.ToTable("rewardtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Certificate)
                    .HasColumnName("certificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Chestsign)
                    .HasColumnName("chestsign")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Honorcertificate)
                    .HasColumnName("honorcertificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mchs)
                    .HasColumnName("mchs")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Medal)
                    .HasColumnName("medal")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Money)
                    .HasColumnName("money")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Penalty)
                    .HasColumnName("penalty")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rankfast)
                    .HasColumnName("rankfast")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Ranknext)
                    .HasColumnName("ranknext")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Thanks)
                    .HasColumnName("thanks")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Rights>(entity =>
            {
                entity.ToTable("rights");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Candidateblock)
                    .HasColumnName("candidateblock")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Candidateedit)
                    .HasColumnName("candidateedit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Candidateread)
                    .HasColumnName("candidateread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orgedit)
                    .HasColumnName("orgedit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orgread)
                    .HasColumnName("orgread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Orgreadall)
                    .HasColumnName("orgreadall")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopledecreeedit)
                    .HasColumnName("peopledecreeedit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopledecreeread)
                    .HasColumnName("peopledecreeread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleedit)
                    .HasColumnName("peopleedit")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditattestation)
                    .HasColumnName("peopleeditattestation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditcertificate)
                    .HasColumnName("peopleeditcertificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditcontract)
                    .HasColumnName("peopleeditcontract")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditcontractstate)
                    .HasColumnName("peopleeditcontractstate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditcontractvacation)
                    .HasColumnName("peopleeditcontractvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditdispanserization)
                    .HasColumnName("peopleeditdispanserization")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditdriver)
                    .HasColumnName("peopleeditdriver")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleediteducation)
                    .HasColumnName("peopleediteducation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleediteducationucp)
                    .HasColumnName("peopleediteducationucp")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditelection)
                    .HasColumnName("peopleeditelection")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditill)
                    .HasColumnName("peopleeditill")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditjob)
                    .HasColumnName("peopleeditjob")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditjobpension)
                    .HasColumnName("peopleeditjobpension")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditjobprivelege)
                    .HasColumnName("peopleeditjobprivelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditlanguage)
                    .HasColumnName("peopleeditlanguage")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditmain)
                    .HasColumnName("peopleeditmain")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditpassport)
                    .HasColumnName("peopleeditpassport")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditpenalty)
                    .HasColumnName("peopleeditpenalty")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditpermission)
                    .HasColumnName("peopleeditpermission")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditpfl)
                    .HasColumnName("peopleeditpfl")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditphoto)
                    .HasColumnName("peopleeditphoto")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditphysical)
                    .HasColumnName("peopleeditphysical")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditprivelege)
                    .HasColumnName("peopleeditprivelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditrank)
                    .HasColumnName("peopleeditrank")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditrelative)
                    .HasColumnName("peopleeditrelative")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditreward)
                    .HasColumnName("peopleeditreward")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditscience)
                    .HasColumnName("peopleeditscience")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditvacation)
                    .HasColumnName("peopleeditvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditvvk)
                    .HasColumnName("peopleeditvvk")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditworktrip)
                    .HasColumnName("peopleeditworktrip")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleeditwound)
                    .HasColumnName("peopleeditwound")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleorgread)
                    .HasColumnName("peopleorgread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleorgreadall)
                    .HasColumnName("peopleorgreadall")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peopleread)
                    .HasColumnName("peopleread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadall)
                    .HasColumnName("peoplereadall")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadattestation)
                    .HasColumnName("peoplereadattestation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadcertificate)
                    .HasColumnName("peoplereadcertificate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadcontract)
                    .HasColumnName("peoplereadcontract")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadcontractstate)
                    .HasColumnName("peoplereadcontractstate")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadcontractvacation)
                    .HasColumnName("peoplereadcontractvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereaddispanserization)
                    .HasColumnName("peoplereaddispanserization")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereaddriver)
                    .HasColumnName("peoplereaddriver")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadeducation)
                    .HasColumnName("peoplereadeducation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadeducationucp)
                    .HasColumnName("peoplereadeducationucp")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadelection)
                    .HasColumnName("peoplereadelection")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadill)
                    .HasColumnName("peoplereadill")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadjob)
                    .HasColumnName("peoplereadjob")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadjobpension)
                    .HasColumnName("peoplereadjobpension")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadjobprivelege)
                    .HasColumnName("peoplereadjobprivelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadlanguage)
                    .HasColumnName("peoplereadlanguage")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadmain)
                    .HasColumnName("peoplereadmain")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadpassport)
                    .HasColumnName("peoplereadpassport")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadpenalty)
                    .HasColumnName("peoplereadpenalty")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadpermission)
                    .HasColumnName("peoplereadpermission")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadpfl)
                    .HasColumnName("peoplereadpfl")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadphoto)
                    .HasColumnName("peoplereadphoto")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadphysical)
                    .HasColumnName("peoplereadphysical")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadprivelege)
                    .HasColumnName("peoplereadprivelege")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadrank)
                    .HasColumnName("peoplereadrank")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadrelative)
                    .HasColumnName("peoplereadrelative")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadreward)
                    .HasColumnName("peoplereadreward")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadscience)
                    .HasColumnName("peoplereadscience")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadvacation)
                    .HasColumnName("peoplereadvacation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadvvk)
                    .HasColumnName("peoplereadvvk")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadworktrip)
                    .HasColumnName("peoplereadworktrip")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Peoplereadwound)
                    .HasColumnName("peoplereadwound")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Position)
                    .HasColumnName("position")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Role)
                    .HasColumnName("role")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Rightsstructure>(entity =>
            {
                entity.ToTable("rightsstructure");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Allowed)
                    .HasColumnName("allowed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Org)
                    .HasColumnName("org")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.People)
                    .HasColumnName("people")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Peopleorg)
                    .HasColumnName("peopleorg")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Rights)
                    .HasColumnName("rights")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("role");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Servicecoef>(entity =>
            {
                entity.ToTable("servicecoef");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Servicefeature>(entity =>
            {
                entity.ToTable("servicefeature");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Servicetype>(entity =>
            {
                entity.ToTable("servicetype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(512);

                entity.Property(e => e.Expires)
                    .HasColumnName("expires")
                    .HasColumnType("datetime");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Setpersondatatype>(entity =>
            {
                entity.ToTable("setpersondatatype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(270)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Sheetdata>(entity =>
            {
                entity.ToTable("sheetdata");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sheetaddress)
                    .IsRequired()
                    .HasColumnName("sheetaddress")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetarmy)
                    .IsRequired()
                    .HasColumnName("sheetarmy")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetarmyeducation)
                    .IsRequired()
                    .HasColumnName("sheetarmyeducation")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetaward)
                    .IsRequired()
                    .HasColumnName("sheetaward")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetdegree)
                    .IsRequired()
                    .HasColumnName("sheetdegree")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetdob)
                    .HasColumnName("sheetdob")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Sheeteducation)
                    .IsRequired()
                    .HasColumnName("sheeteducation")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetfamily)
                    .IsRequired()
                    .HasColumnName("sheetfamily")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheethomephone)
                    .IsRequired()
                    .HasColumnName("sheethomephone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetinventions)
                    .IsRequired()
                    .HasColumnName("sheetinventions")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetlanguage)
                    .IsRequired()
                    .HasColumnName("sheetlanguage")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetlockunlock)
                    .HasColumnName("sheetlockunlock")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sheetmobilephone)
                    .IsRequired()
                    .HasColumnName("sheetmobilephone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetname)
                    .IsRequired()
                    .HasColumnName("sheetname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetnationality)
                    .IsRequired()
                    .HasColumnName("sheetnationality")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetotherdata)
                    .IsRequired()
                    .HasColumnName("sheetotherdata")
                    .HasMaxLength(1500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpassportdatein)
                    .HasColumnName("sheetpassportdatein")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Sheetpassportind)
                    .IsRequired()
                    .HasColumnName("sheetpassportind")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpassportnumber)
                    .IsRequired()
                    .HasColumnName("sheetpassportnumber")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpassportseries)
                    .IsRequired()
                    .HasColumnName("sheetpassportseries")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpatronymic)
                    .IsRequired()
                    .HasColumnName("sheetpatronymic")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpob)
                    .IsRequired()
                    .HasColumnName("sheetpob")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetsex)
                    .IsRequired()
                    .HasColumnName("sheetsex")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetsignature)
                    .HasColumnName("sheetsignature")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Sheetsurname)
                    .IsRequired()
                    .HasColumnName("sheetsurname")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheettradeunion)
                    .IsRequired()
                    .HasColumnName("sheettradeunion")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetwhogivepassport)
                    .IsRequired()
                    .HasColumnName("sheetwhogivepassport")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetworkphone)
                    .IsRequired()
                    .HasColumnName("sheetworkphone")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Sheetpolitics>(entity =>
            {
                entity.ToTable("sheetpolitics");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cabinetid)
                    .HasColumnName("cabinetid")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Sheetpoliticsend)
                    .HasColumnName("sheetpoliticsend")
                    .HasColumnType("datetime");

                entity.Property(e => e.Sheetpoliticslocation)
                    .IsRequired()
                    .HasColumnName("sheetpoliticslocation")
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpoliticsnameorganization)
                    .IsRequired()
                    .HasColumnName("sheetpoliticsnameorganization")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpoliticspost)
                    .IsRequired()
                    .HasColumnName("sheetpoliticspost")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Sheetpoliticsstart)
                    .HasColumnName("sheetpoliticsstart")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");
            });

            modelBuilder.Entity<Sourceoffinancing>(entity =>
            {
                entity.ToTable("sourceoffinancing");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.External)
                    .HasColumnName("external")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(290)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Streettype>(entity =>
            {
                entity.ToTable("streettype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(100)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Structure>(entity =>
            {
                entity.ToTable("structure");

                entity.HasIndex(e => e.Changeorigin)
                    .HasName("CHANGEORIGIN");

                entity.HasIndex(e => e.Changestructurelast)
                    .HasName("CHANGESTRUCTURELAST");

                entity.HasIndex(e => e.Parentstructure)
                    .HasName("PARENT");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Changeorigin)
                    .HasColumnName("changeorigin")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructureall)
                    .HasColumnName("changestructureall")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructurelast)
                    .HasColumnName("changestructurelast")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructurelocation)
                    .HasColumnName("changestructurelocation")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructureparent)
                    .HasColumnName("changestructureparent")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructurerank)
                    .HasColumnName("changestructurerank")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Changestructurerename)
                    .HasColumnName("changestructurerename")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasColumnName("city")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Curator)
                    .HasColumnName("curator")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Department)
                    .HasColumnName("department")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Featured)
                    .HasColumnName("featured")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Head)
                    .HasColumnName("head")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Main)
                    .HasColumnName("main")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(720);

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Nameshortened)
                    .HasColumnName("nameshortened")
                    .HasMaxLength(720);

                entity.Property(e => e.Parentstructure)
                    .HasColumnName("parentstructure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Printreward)
                    .HasColumnName("printreward")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Priority)
                    .HasColumnName("priority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Separatestructure)
                    .HasColumnName("separatestructure")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Street)
                    .IsRequired()
                    .HasColumnName("street")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Structureregion)
                    .HasColumnName("structureregion")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Structuretype)
                    .HasColumnName("structuretype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject1)
                    .HasColumnName("subject1")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject10)
                    .HasColumnName("subject10")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject11)
                    .HasColumnName("subject11")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject12)
                    .HasColumnName("subject12")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject13)
                    .HasColumnName("subject13")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject14)
                    .HasColumnName("subject14")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject15)
                    .HasColumnName("subject15")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject2)
                    .HasColumnName("subject2")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject3)
                    .HasColumnName("subject3")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject4)
                    .HasColumnName("subject4")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject5)
                    .HasColumnName("subject5")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject6)
                    .HasColumnName("subject6")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject7)
                    .HasColumnName("subject7")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject8)
                    .HasColumnName("subject8")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subject9)
                    .HasColumnName("subject9")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subjectgender)
                    .HasColumnName("subjectgender")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Subjectnotice)
                    .IsRequired()
                    .HasColumnName("subjectnotice")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subjectnumber)
                    .HasColumnName("subjectnumber")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<Structureregion>(entity =>
            {
                entity.ToTable("structureregion");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Structuretype>(entity =>
            {
                entity.ToTable("structuretype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Subject>(entity =>
            {
                entity.ToTable("subject");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Dropword)
                    .HasColumnName("dropword")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name1)
                    .IsRequired()
                    .HasColumnName("name1")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name3)
                    .IsRequired()
                    .HasColumnName("name3")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name4)
                    .IsRequired()
                    .HasColumnName("name4")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name5)
                    .IsRequired()
                    .HasColumnName("name5")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name6)
                    .IsRequired()
                    .HasColumnName("name6")
                    .HasMaxLength(720)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Subjectcategory>(entity =>
            {
                entity.ToTable("subjectcategory");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Subjectexport>(entity =>
            {
                entity.ToTable("subjectexport");

                entity.HasIndex(e => e.Category)
                    .HasName("category_index");

                entity.HasIndex(e => e.Dropword)
                    .HasName("dropword_index");

                entity.HasIndex(e => e.Gender)
                    .HasName("gender_index");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Category)
                    .HasColumnName("category")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Dropword)
                    .HasColumnName("dropword")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Gender)
                    .HasColumnName("gender")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(255);

                entity.Property(e => e.Name1)
                    .HasColumnName("name1")
                    .HasMaxLength(255);

                entity.Property(e => e.Name2)
                    .HasColumnName("name2")
                    .HasMaxLength(255);

                entity.Property(e => e.Name3)
                    .HasColumnName("name3")
                    .HasMaxLength(255);

                entity.Property(e => e.Name4)
                    .HasColumnName("name4")
                    .HasMaxLength(255);

                entity.Property(e => e.Name5)
                    .HasColumnName("name5")
                    .HasMaxLength(255);

                entity.Property(e => e.Name6)
                    .HasColumnName("name6")
                    .HasMaxLength(255);
            });

            modelBuilder.Entity<Subjectgender>(entity =>
            {
                entity.ToTable("subjectgender");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Transfertype>(entity =>
            {
                entity.ToTable("transfertype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnName("description")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Point)
                    .HasColumnName("point")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Pointsubpoint)
                    .IsRequired()
                    .HasColumnName("pointsubpoint")
                    .HasMaxLength(45)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Selectdescription)
                    .IsRequired()
                    .HasColumnName("selectdescription")
                    .HasMaxLength(500)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Subpoint)
                    .HasColumnName("subpoint")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Currentstructuretree)
                    .IsRequired()
                    .HasColumnName("currentstructuretree")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Date)
                    .HasColumnName("date")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("'CURRENT_TIMESTAMP'");

                entity.Property(e => e.Decree)
                    .HasColumnName("decree")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasColumnName("firstname")
                    .HasMaxLength(90)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Fullmode)
                    .HasColumnName("fullmode")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Masterpersonneleditor)
                    .HasColumnName("masterpersonneleditor")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Mode)
                    .HasColumnName("mode")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(120);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(512);

                entity.Property(e => e.Patronymic)
                    .IsRequired()
                    .HasColumnName("patronymic")
                    .HasMaxLength(180)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Personneleditor)
                    .HasColumnName("personneleditor")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Personnelread)
                    .HasColumnName("personnelread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Positioncompact)
                    .HasColumnName("positioncompact")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Positiontype)
                    .HasColumnName("positiontype")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(512);

                entity.Property(e => e.Sidebardisplay)
                    .HasColumnName("sidebardisplay")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Structure)
                    .HasColumnName("structure")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Structureeditor)
                    .HasColumnName("structureeditor")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Structureread)
                    .HasColumnName("structureread")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'1'");

                entity.Property(e => e.Surname)
                    .IsRequired()
                    .HasColumnName("surname")
                    .HasMaxLength(180)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Vacationmilitary>(entity =>
            {
                entity.ToTable("vacationmilitary");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Vacationtype>(entity =>
            {
                entity.ToTable("vacationtype");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cadet)
                    .HasColumnName("cadet")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Civil)
                    .HasColumnName("civil")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Durationmax)
                    .HasColumnName("durationmax")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Main)
                    .HasColumnName("main")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Maternity)
                    .HasColumnName("maternity")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Military)
                    .HasColumnName("military")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Name2)
                    .IsRequired()
                    .HasColumnName("name2")
                    .HasMaxLength(900)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Order)
                    .HasColumnName("order")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Social)
                    .HasColumnName("social")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Transferworkyear)
                    .HasColumnName("transferworkyear")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });
        }
    }
}
