using ApplicationLayer.IRepositories;
using Common.Security;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Services._Connector;
using Services.AuthServices;
using Services.CampusesServices;
using Services.CourseServices;
using Services.DepartmentsServices;
using Services.ExaminationServices;
using Services.InterviewServices;
using Services.RegistrationServices.AcademicBackgrounds;
using Services.RegistrationServices.Applicants;
using Services.RegistrationServices.CouncelorConsultations;
using Services.RegistrationServices.EmergencyContacts;
using Services.RegistrationServices.FamilyRelations;
using Services.RegistrationServices.FirstApplicationInfos;
using Services.RegistrationServices.ParentGuardianInformations;
using Services.RegistrationServices.PersonalInformations;
using Services.RegistrationServices.PersonalityProfiles;
using Services.RegistrationServices.PhysicalHealths;
using Services.RegistrationServices.PsychiatristConsultations;
using Services.RegistrationServices.PsychologistConsultations;
using Services.RegistrationServices.Registered;
using Services.RegistrationServices.SoloParents;
using Services.RegistrationServices.Spouses;
using Services.RegistrationServices.Transfers;
using Services.ScheduleServices;
using Services.UsersServices;

namespace DataAccessLayer;
public static class StartUp
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services
           .AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("MyConnection"), sqlOptions =>
               {
                   sqlOptions.EnableRetryOnFailure(); 
               }), ServiceLifetime.Transient
           );
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>))
            .AddScoped(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>))
            .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>))
            .AddScoped<IPasswordHasher, PasswordHasher>()
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICampusService, CampusService>()
            .AddScoped<IDepartmentService, DepartmentService>()
            .AddScoped<ICourseService, CourseService>()
            .AddScoped<IScheduleService, ScheduleService>()
            .AddScoped<IApplicantService, ApplicantService>()
            .AddScoped<IPersonalInformationService, PersonalInformationService>()
            .AddScoped<IAcademicBackgroundService, AcademicBackgroundService>()
            .AddScoped<IParentGuardianInformationService, ParentGuardianInformationService>()
            .AddScoped<ICouncelorConsultationService, CouncelorConsultationService>()
            .AddScoped<IEmergencyContactService, EmergencyContactService>()
            .AddScoped<IFamilyRelationService, FamilyRelationService>()
            .AddScoped<IPersonalityProfileService, PersonalityProfileService>()
            .AddScoped<IPhysicalHealthService, PhysicalHealthService>()
            .AddScoped<IPsychiatristConsultationService, PsychiatristConsultationService>()
            .AddScoped<IPsychologistConsultationService, PsychologistConsultationService>()
            .AddScoped<ISoloParentService, SoloParentService>()
            .AddScoped<ISpouseService, SpouseService>()
            .AddScoped<IFirstApplicationInfoService, FirstApplicationInfoService>()
            .AddScoped<IExaminationService, ExaminationService>()
            .AddScoped<IInterviewService, InterviewService>()
            .AddScoped<ITransferService, TransferService>()
            .AddScoped<IRegisteredService, RegisteredService>()
            .AddScoped<IConnectionService, ConnectionService>();
    }
}
