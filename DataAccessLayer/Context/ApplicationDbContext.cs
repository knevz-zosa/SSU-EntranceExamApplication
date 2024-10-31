using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace DataAccessLayer.Context;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Applicant> Applicants { get; set; }
    public DbSet<Schedule> Schedules { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Campus> Campuses { get; set; }
    public DbSet<PersonalInformation> PersonalInformations { get; set; }
    public DbSet<AcademicBackground> AcademicBackgrounds { get; set; }
    public DbSet<ParentGuardianInformation> ParentGuardianInformations { get; set; }
    public DbSet<Transfer> Transfers { get; set; }
    public DbSet<FamilyRelation> FamilyRelations { get; set; }
    public DbSet<CouncelorConsultation> CouncelorConsultations { get; set; }
    public DbSet<PsychiatristConsultation> PsychiatristConsultations { get; set; }
    public DbSet<PsychologistConsultation> PsychologistConsultations { get; set; }
    public DbSet<PersonalityProfile> PersonalityProfiles { get; set; }
    public DbSet<PhysicalHealth> PhysicalHealths { get; set; }
    public DbSet<EmergencyContact> EmergencyContacts { get; set; }
    public DbSet<Spouse> Spouses { get; set; }
    public DbSet<SoloParent> SoloParents { get; set; }
    public DbSet<Examination> Examinations { get; set; }
    public DbSet<Interview> Interviews { get; set; }
    public DbSet<FirstApplicationInfo> FirstApplicationInfos { get; set; }
    public DbSet<Registered> Registereds { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.PersonalInformation)
            .WithOne(pi => pi.Applicant)
            .HasForeignKey<PersonalInformation>(pi => pi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for personal information

        modelBuilder.Entity<Applicant>()
            .HasOne(pi => pi.SoloParent)
            .WithOne(sp => sp.Applicant)
            .HasForeignKey<SoloParent>(pi => pi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.AcademicBackground)
            .WithOne(eb => eb.Applicant)
            .HasForeignKey<AcademicBackground>(eb => eb.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for educational background

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.ParentGuardianInformation)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<ParentGuardianInformation>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade); // Cascade delete for parent guardian information          

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.PersonalityProfile)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<PersonalityProfile>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.CouncelorConsultation)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<CouncelorConsultation>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.PsychiatristConsultation)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<PsychiatristConsultation>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.PsychologistConsultation)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<PsychologistConsultation>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.PhysicalHealth)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<PhysicalHealth>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.EmergencyContact)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<EmergencyContact>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.EmergencyContact)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<EmergencyContact>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.Examination)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<Examination>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.Spouse)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<Spouse>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.SoloParent)
            .WithOne(pgi => pgi.Applicant)
            .HasForeignKey<SoloParent>(pgi => pgi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.FirstApplicationInfo)
            .WithOne(pi => pi.Applicant)
            .HasForeignKey<FirstApplicationInfo>(pi => pi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasOne(a => a.Registered)
            .WithOne(pi => pi.Applicant)
            .HasForeignKey<Registered>(pi => pi.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasMany(a => a.Interviews)
            .WithOne(si => si.Applicant)
            .HasForeignKey(si => si.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Applicant>()
            .HasMany(a => a.FamilyRelations)
            .WithOne(si => si.Applicant)
            .HasForeignKey(si => si.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);


        modelBuilder.Entity<Applicant>()
            .HasMany(a => a.Transfers)
            .WithOne(si => si.Applicant)
            .HasForeignKey(si => si.ApplicantId)
            .OnDelete(DeleteBehavior.Cascade);


        base.OnModelCreating(modelBuilder);
    }
}
