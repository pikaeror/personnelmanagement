using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonnelManagement.Models
{
    public partial class orgContext : DbContext
    {
        public virtual DbSet<Altrank> Altrank { get; set; }
        public virtual DbSet<Altrankcondition> Altrankcondition { get; set; }
        public virtual DbSet<Altrankconditiongroup> Altrankconditiongroup { get; set; }
        public virtual DbSet<Area> Area { get; set; }
        public virtual DbSet<Areaother> Areaother { get; set; }
        public virtual DbSet<Citysubstate> Citysubstate { get; set; }
        public virtual DbSet<Citytype> Citytype { get; set; }
        public virtual DbSet<Country> Country { get; set; }
        public virtual DbSet<Decree> Decree { get; set; }
        public virtual DbSet<Decreeoperation> Decreeoperation { get; set; }
        public virtual DbSet<Elementsubject> Elementsubject { get; set; }
        public virtual DbSet<Externalorderwhotype> Externalorderwhotype { get; set; }
        public virtual DbSet<Mrd> Mrd { get; set; }
        public virtual DbSet<Ordernumbertype> Ordernumbertype { get; set; }
        public virtual DbSet<Position> Position { get; set; }
        public virtual DbSet<Positioncategory> Positioncategory { get; set; }
        public virtual DbSet<Positioncategoryrank> Positioncategoryrank { get; set; }
        public virtual DbSet<Positiondecreeoperation> Positiondecreeoperation { get; set; }
        public virtual DbSet<Positionhistory> Positionhistory { get; set; }
        public virtual DbSet<Positionmrd> Positionmrd { get; set; }
        public virtual DbSet<Positiontype> Positiontype { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Sourceoffinancing> Sourceoffinancing { get; set; }
        public virtual DbSet<Streettype> Streettype { get; set; }
        public virtual DbSet<Structure> Structure { get; set; }
        public virtual DbSet<Structuredecreeoperation> Structuredecreeoperation { get; set; }
        public virtual DbSet<Structureregion> Structureregion { get; set; }
        public virtual DbSet<Structuretype> Structuretype { get; set; }
        public virtual DbSet<Subject> Subject { get; set; }
        public virtual DbSet<Subjectcategory> Subjectcategory { get; set; }
        public virtual DbSet<Subjectexport> Subjectexport { get; set; }
        public virtual DbSet<Subjectgender> Subjectgender { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;userid=org_admin;treattinyasboolean=True;password=EldConnection22Standart;database=org;convert zero datetime=True;");
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
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(1500);

                entity.Property(e => e.Nickname)
                    .IsRequired()
                    .HasColumnName("nickname")
                    .HasMaxLength(900);

                entity.Property(e => e.Number)
                    .IsRequired()
                    .HasColumnName("number")
                    .HasMaxLength(400);

                entity.Property(e => e.Signed)
                    .HasColumnName("signed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.User)
                    .HasColumnName("user")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");
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

                entity.Property(e => e.Subjectindex).HasColumnName("subjectindex");
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

            modelBuilder.Entity<Positiondecreeoperation>(entity =>
            {
                entity.ToTable("positiondecreeoperation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Changed)
                    .HasColumnName("changed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Currentposition).HasColumnName("currentposition");

                entity.Property(e => e.Currentstructure).HasColumnName("currentstructure");

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

                entity.Property(e => e.Priviuseosition).HasColumnName("priviuseosition");
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

                entity.HasIndex(e => e.Subjectindex)
                    .HasName("subject_idx");

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

                entity.Property(e => e.Subjectindex).HasColumnName("subjectindex");

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

            modelBuilder.Entity<Structuredecreeoperation>(entity =>
            {
                entity.ToTable("structuredecreeoperation");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Changed)
                    .HasColumnName("changed")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Created)
                    .HasColumnName("created")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Currentstructure).HasColumnName("currentstructure");

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

                entity.Property(e => e.Priveusestructure).HasColumnName("priveusestructure");
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
        }
    }
}
