using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonnelManagement.Udostoverenia
{
    public partial class certContext : DbContext
    {
        public virtual DbSet<Agency> Agency { get; set; }
        public virtual DbSet<Base> Base { get; set; }
        public virtual DbSet<Blankform> Blankform { get; set; }
        public virtual DbSet<Certificate> Certificate { get; set; }
        public virtual DbSet<Issuingauthority> Issuingauthority { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Rank> Rank { get; set; }
        public virtual DbSet<Rejectreason> Rejectreason { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=192.168.1.16;port=3306;userid=admin;treattinyasboolean=True;password=qazwsxedc098;database=cert;convert zero datetime=True;");
            }
        }

        public certContext(DbContextOptions<certContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Agency>(entity =>
            {
                entity.ToTable("agency");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(450);

                entity.Property(e => e.Parent)
                    .HasColumnName("parent")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.PreferredSigner1)
                    .IsRequired()
                    .HasColumnName("preferred_signer1")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");

                entity.Property(e => e.PreferredSigner2)
                    .IsRequired()
                    .HasColumnName("preferred_signer2")
                    .HasMaxLength(450)
                    .HasDefaultValueSql("''");
            });

            modelBuilder.Entity<Base>(entity =>
            {
                entity.ToTable("base");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Blankform>(entity =>
            {
                entity.ToTable("blankform");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Agency)
                    .HasColumnName("agency")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasMaxLength(240);

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Certificate>(entity =>
            {
                entity.ToTable("certificate");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.NumBlank)
                    .HasName("num_blank_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Agency)
                    .HasColumnName("agency")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Base)
                    .HasColumnName("base")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Blank)
                    .HasColumnName("blank")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Byadmin)
                    .HasColumnName("byadmin")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.CertificateCommitedBy1)
                    .HasColumnName("certificate_commited_by1")
                    .HasMaxLength(240);

                entity.Property(e => e.CertificateCommitedBy2)
                    .HasColumnName("certificate_commited_by2")
                    .HasMaxLength(240);

                entity.Property(e => e.CertificateCommitedDate)
                    .HasColumnName("certificate_commited_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateConferment)
                    .HasColumnName("date_conferment")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateOrderNazn)
                    .HasColumnName("date_order_nazn")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpirationDate)
                    .HasColumnName("expiration_date")
                    .HasColumnType("datetime");

                entity.Property(e => e.Exclusive)
                    .HasColumnName("exclusive")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Extra)
                    .HasColumnName("extra")
                    .HasMaxLength(1200);

                entity.Property(e => e.FullName)
                    .HasColumnName("full_name")
                    .HasMaxLength(60);

                entity.Property(e => e.Issuingauthority)
                    .HasColumnName("issuingauthority")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'2'");

                entity.Property(e => e.NameConferment)
                    .HasColumnName("name_conferment")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NameOrganNazn)
                    .HasColumnName("name_organ_nazn")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumUd)
                    .HasColumnName("num_ud")
                    .HasColumnType("int(5)");

                entity.Property(e => e.NumBlank)
                    .HasColumnName("num_blank")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NumOrderConferment)
                    .HasColumnName("num_order_conferment")
                    .HasMaxLength(60);

                entity.Property(e => e.NumOrderNazn)
                    .HasColumnName("num_order_nazn")
                    .HasMaxLength(60);

                entity.Property(e => e.NumPersonal)
                    .HasColumnName("num_personal")
                    .HasMaxLength(60);

                entity.Property(e => e.Post)
                    .HasColumnName("post")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Rank)
                    .HasColumnName("rank")
                    .HasColumnType("int(11)");

                entity.Property(e => e.RankInspector)
                    .HasColumnName("rank_inspector")
                    .HasMaxLength(160);

                entity.Property(e => e.Rejectreason)
                    .HasColumnName("rejectreason")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<Issuingauthority>(entity =>
            {
                entity.ToTable("issuingauthority");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("post");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(120);
            });

            modelBuilder.Entity<Rank>(entity =>
            {
                entity.ToTable("rank");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Rejectreason>(entity =>
            {
                entity.ToTable("rejectreason");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasMaxLength(45);
            });

            modelBuilder.Entity<Session>(entity =>
            {
                entity.ToTable("session");

                entity.Property(e => e.Sessionid)
                    .HasColumnName("sessionid")
                    .HasMaxLength(256);

                entity.Property(e => e.Registration)
                    .HasColumnName("registration")
                    .HasColumnType("datetime");

                entity.Property(e => e.Userid)
                    .HasColumnName("userid")
                    .HasColumnType("int(11)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.HasIndex(e => e.Id)
                    .HasName("id_UNIQUE")
                    .IsUnique();

                entity.HasIndex(e => e.Name)
                    .HasName("name_UNIQUE")
                    .IsUnique();

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Admin)
                    .HasColumnName("admin")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Agency)
                    .HasColumnName("agency")
                    .HasColumnType("int(11)")
                    .HasDefaultValueSql("'0'");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasMaxLength(45);

                entity.Property(e => e.Password)
                    .HasColumnName("password")
                    .HasMaxLength(256);

                entity.Property(e => e.Salt)
                    .HasColumnName("salt")
                    .HasMaxLength(256);

                entity.Property(e => e.Weapon)
                    .HasColumnName("weapon")
                    .HasColumnType("tinyint(1)")
                    .HasDefaultValueSql("'0'");
            });
        }
    }
}
