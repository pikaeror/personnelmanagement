using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PersonnelManagement.USERS
{
    public partial class userContext : DbContext
    {
        public virtual DbSet<Rights> Rights { get; set; }
        public virtual DbSet<Rightsstructure> Rightsstructure { get; set; }
        public virtual DbSet<Session> Session { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySql("server=localhost;port=3306;userid=org_admin;treattinyasboolean=True;password=EldConnection22Standart;database=user;convert zero datetime=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
