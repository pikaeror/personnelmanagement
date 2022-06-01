using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonnelManagement.Models
{
    public partial class pmContext : DbContext
    {
        public virtual DbSet<Altrank> Altrank { get; set; }
        public virtual DbSet<Altrankcondition> Altrankcondition { get; set; }
        public virtual DbSet<Altrankconditiongroup> Altrankconditiongroup { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Areaother> Areaother { get; set; }
        public virtual DbSet<Citysubstate> Citysubstate { get; set; }
        public virtual DbSet<Citytype> Citytype { get; set; }
        public virtual DbSet<Civildecree> Civildecree { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Decree> Decree { get; set; }
        public virtual DbSet<Decreeoperation> Decreeoperation { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Departmentrename> Departmentrename { get; set; }
        public virtual DbSet<Dismissalclauses> Dismissalclauses { get; set; }
        public virtual DbSet<Drivercategory> Drivercategory { get; set; }
        public virtual DbSet<Drivertype> Drivertype { get; set; }
        public virtual DbSet<Elementsubject> Elementsubject { get; set; }
        public virtual DbSet<Externalorderwhotype> Externalorderwhotype { get; set; }
        public virtual DbSet<Mrd> Mrd { get; set; }
        public virtual DbSet<Ordernumbertype> Ordernumbertype { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Positioncategory> Positioncategory { get; set; }
        public virtual DbSet<Positioncategoryrank> Positioncategoryrank { get; set; }
        public virtual DbSet<Positionhistory> Positionhistory { get; set; }
        public virtual DbSet<Positionmrd> Positionmrd { get; set; }
        public virtual DbSet<Positiontype> Positiontype { get; set; }
        public virtual DbSet<Prooftype> Prooftype { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<Rightsstructure> Rightsstructure { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<Setpersondatatype> Setpersondatatype { get; set; }
        public virtual DbSet<Sourceoffinancing> Sourceoffinancing { get; set; }
        public virtual DbSet<Staffcomission> Staffcomission { get; set; }
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

        public pmContext(DbContextOptions<pmContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;userid=admin;treattinyasboolean=True;password=FVSH6S6aB4srgReT;database=pm_org;convert zero datetime=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            modelBuilder.Entity<Elementsubject>(entity =>
            {
                entity.ToTable("elementsubject");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Subject1).HasColumnName("subject1");

                entity.Property(e => e.Subject10).HasColumnName("subject10");

                entity.Property(e => e.Subject11).HasColumnName("subject11");

                entity.Property(e => e.Subject12).HasColumnName("subject12");

                entity.Property(e => e.Subject13).HasColumnName("subject13");

                entity.Property(e => e.Subject14).HasColumnName("subject14");

                entity.Property(e => e.Subject15).HasColumnName("subject15");

                entity.Property(e => e.Subject16).HasColumnName("subject16");

                entity.Property(e => e.Subject17).HasColumnName("subject17");

                entity.Property(e => e.Subject18).HasColumnName("subject18");

                entity.Property(e => e.Subject19).HasColumnName("subject19");

                entity.Property(e => e.Subject2).HasColumnName("subject2");

                entity.Property(e => e.Subject20).HasColumnName("subject20");

                entity.Property(e => e.Subject3).HasColumnName("subject3");

                entity.Property(e => e.Subject4).HasColumnName("subject4");

                entity.Property(e => e.Subject5).HasColumnName("subject5");

                entity.Property(e => e.Subject6).HasColumnName("subject6");

                entity.Property(e => e.Subject7).HasColumnName("subject7");

                entity.Property(e => e.Subject8).HasColumnName("subject8");

                entity.Property(e => e.Subject9).HasColumnName("subject9");
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

                entity.Property(e => e.MaxPeriod).HasColumnName("max_period");

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

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasMaxLength(512);

                entity.Property(e => e.Expires)
                    .HasColumnName("expires")
                    .HasColumnType("datetime");

                entity.Property(e => e.LastPidrequest)
                    .HasColumnName("lastPIDrequest")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'-1'");

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

            modelBuilder.Entity<Staffcomission>(entity =>
            {
                entity.ToTable("staffcomission");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Fio)
                    .HasColumnName("fio")
                    .HasMaxLength(255)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");
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

                entity.Property(e => e.Onlyreadflagtoeditor)
                    .HasColumnName("onlyreadflagtoeditor")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

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
        }
    }
}
