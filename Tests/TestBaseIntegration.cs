using ApplicationLayer;
using ApplicationLayer.Features.Login;
using ApplicationLayer.IRepositories;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Security;
using Common.Wrapper;
using DataAccessLayer.Context;
using DataAccessLayer.Repositories;
using Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
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
using System;
using System.Data.Common;

namespace Tests;

public class TestBaseIntegration : IDisposable
{
    protected readonly IServiceScope Scope;
    protected readonly ApplicationDbContext Context;
    protected readonly DbConnection Connection;
    protected readonly IPasswordHasher PasswordHasher;
    protected readonly IConnectionService Connect;   

    public TestBaseIntegration()
    {
        // Setup SQLite in-memory database
        Connection = new SqliteConnection("Filename=:memory:");
        Connection.Open();

        var services = new ServiceCollection();
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(Connection);
        });

        // Register UnitOfWork and Repositories
        services.AddScoped(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        services.AddScoped(typeof(IReadRepositoryAsync<,>), typeof(ReadRepositoryAsync<,>));
        services.AddScoped(typeof(IWriteRepositoryAsync<,>), typeof(WriteRepositoryAsync<,>));
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICampusService, CampusService>();
        services.AddScoped<IDepartmentService, DepartmentService>();
        services.AddScoped<ICourseService, CourseService>();
        services.AddScoped<IScheduleService, ScheduleService>();
        services.AddScoped<IApplicantService, ApplicantService>();
        services.AddScoped<IPersonalInformationService, PersonalInformationService>();
        services.AddScoped<IAcademicBackgroundService, AcademicBackgroundService>();
        services.AddScoped<IParentGuardianInformationService, ParentGuardianInformationService>();
        services.AddScoped<ICouncelorConsultationService, CouncelorConsultationService>();
        services.AddScoped<IEmergencyContactService, EmergencyContactService>();
        services.AddScoped<IFamilyRelationService, FamilyRelationService>();
        services.AddScoped<IPersonalityProfileService, PersonalityProfileService>();
        services.AddScoped<IPhysicalHealthService, PhysicalHealthService>();
        services.AddScoped<IPsychiatristConsultationService, PsychiatristConsultationService>();
        services.AddScoped<IPsychologistConsultationService, PsychologistConsultationService>();
        services.AddScoped<ISoloParentService, SoloParentService>();
        services.AddScoped<ISpouseService, SpouseService>();
        services.AddScoped<IFirstApplicationInfoService, FirstApplicationInfoService>();
        services.AddScoped<IExaminationService, ExaminationService>();
        services.AddScoped<IInterviewService, InterviewService>();
        services.AddScoped<IRegisteredService, RegisteredService>();
        services.AddScoped<ITransferService, TransferService>();
        services.AddAutoMapper(typeof(Startup).Assembly);
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(LoginCommandHandler).Assembly));

        services.AddScoped<IConnectionService, ConnectionService>();

        var serviceProvider = services.BuildServiceProvider();
        Scope = serviceProvider.CreateScope();
        Context = Scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        // Ensure the database is created
        Context.Database.EnsureCreated();
        Connect = Scope.ServiceProvider.GetRequiredService<IConnectionService>();
        PasswordHasher = Scope.ServiceProvider.GetRequiredService<IPasswordHasher>();      

        // Seed the database with initial data
        SeedUserSuperAdmin();
        SeedForScheduleTest();
        SeedForRegistrationTest();
    }
    private void SeedUserSuperAdmin()
    {
        var adminUser = new User
        {
            FirstName = "Admin",
            LastName = "User",
            Username = "admin",
            Password = PasswordHasher.Hash("admin"),
            Role = "SuperAdmin",
            Access = "All"
        };
        Context.Users.Add(adminUser);
        Context.SaveChanges();
    }  

    private void SeedForScheduleTest()
    {
        var user = Context.Users.FirstOrDefault();
        var createdBy = user != null
        ? $"{user.FirstName} {user.LastName}"
        : "Default";

        var campus = new Campus
        {
            Name = "Test Campus",
            Address = "Test Address",
            HasDepartment = true,
            DateCreated = DateTime.Now,
            CreatedBy = createdBy
        };
        Context.Campuses.Add(campus);
        Context.SaveChanges();

        var department = new Department
        {
            Name = "Test Department",
            CampusId = campus.Id,
            Campus = campus,
            DateCreated = DateTime.Now,
            CreatedBy = createdBy
        };
        Context.Departments.Add(department);
        Context.SaveChanges();

        var course = new Course
        {
            Name = "Test Course",
            CampusId = campus.Id,
            Campus = campus,
            DepartmentId = department.Id,
            Department = department,
            DateCreated = DateTime.Now,
            CreatedBy = createdBy
        };
        Context.Courses.Add(course);

        var course2 = new Course
        {
            Name = "Test Course2",
            CampusId = campus.Id,
            Campus = campus,
            DepartmentId = department.Id,
            Department = department,
            DateCreated = DateTime.Now,
            CreatedBy = createdBy
        };
        Context.Courses.Add(course2);


        var schedule = new Schedule
        {
            Campus = campus,
            CampusId = campus.Id,
            ScheduleDate = DateTime.Now.AddDays(10),
            Time = "8:00am",
            Slot = 400,
            Venue = "Test Venue",
            SchoolYear = $"{DateTime.Now.AddDays(10).Year.ToString()} - {(DateTime.Now.AddDays(10).Year + 1).ToString()}",
            DateCreated = DateTime.Now,
            CreatedBy = createdBy,
        };
        Context.Schedules.Add(schedule);

        var schedule2 = new Schedule
        {
            Campus = campus,
            CampusId = campus.Id,
            ScheduleDate = DateTime.Now.AddDays(10),
            Time = "1:00am",
            Slot = 400,
            Venue = "Test Venue",
            SchoolYear = $"{DateTime.Now.AddDays(10).Year.ToString()} - {(DateTime.Now.AddDays(10).Year + 1).ToString()}",
            DateCreated = DateTime.Now,
            CreatedBy = createdBy,
        };
        Context.Schedules.Add(schedule2);
        Context.SaveChanges();
    }

    private void SeedForRegistrationTest()
    {
        var schedule = Context.Schedules.FirstOrDefault();

        var applicant = new Applicant
        {
            ScheduleId = schedule.Id,
            CourseId = schedule.Campus.Schedules.Select(x => x.Id).FirstOrDefault(),
            TransactionDate = DateTime.Now,
            ApplicantStatus = "New",
            Track = "GAS",
            ControlNo = "123",
            
        };
        Context.Applicants.Add(applicant);
        Context.SaveChanges();

        var firstAppRequest = new FirstApplicationInfo
        {
            ApplicantId = applicant.Id,
            CourseId = applicant.CourseId,
            ScheduleId = applicant.ScheduleId,
            ApplicantStatus = applicant.ApplicantStatus,
            TransactionDate = DateTime.Now,
            Track = applicant.Track
        };
        Context.FirstApplicationInfos.Add(firstAppRequest);

        var perInfRequest = new PersonalInformation
        {
            ApplicantId = applicant.Id,
            FirstName = "Albert",
            MiddleName = "Tupaz",
            LastName = "Lamadrid",
            NickName = "Bert",
            Sex = "Male",
            CivilStatus = "Married",
            PlaceOfBirth = "Catbalogan City",
            Citizenship = "Filipino",
            Religion = "Catholic",
            Email = "test@email.com",
            ContactNumber = "09998798521",
            DateofBirth = new DateTime(1990, 1, 1),
            HouseNo = "163",
            Street = "San Francisco St",
            Barangay = "08",
            Purok = "1",
            Municipality = "Catbalogan City",
            Province = "Samar",
            ZipCode = "6700",
            CurrentHouseNo = "163",
            CurrentStreet = "San Francisco St",
            CurrentBarangay = "08",
            CurrentPurok = "1",
            CurrentMunicipality = "Catbalogan City",
            CurrentProvince = "Samar",
            CurrentZipCode = "6700",
            Dialect = "Waray",
            IsIndigenous = false,
            TribalAffiliation = "N/A",
            HouseHold4psNumber = "N/A",
            BirthOrder = 1,
            Brothers = 3,
            Sisters = 0,
            Is4psMember = false,
            NameExtension = null
        };
        Context.PersonalInformations.Add(perInfRequest);
        Context.SaveChanges();

        var spouseRequest = new Spouse
        {
            ApplicantId = applicant.Id,
            FullName = "Analyn U. Lamadrid",
            ContactNumber = "09998789632",
            Education = "High School Graduate",
            Occupation = "None",
            OfficeAddress = "N/A",
            Barangay = "08",
            Municipality = "Catbalogan City",
            Province = "Samar",
            ZipCode = "6700",
            Birthday = new DateTime(1990, 5, 5),
            BirthPlace = "Catbalogan City"
        };
        Context.Spouses.Add(spouseRequest);
        Context.SaveChanges();

        var acadRequest = new AcademicBackground
        {
            ApplicantId = applicant.Id,
            SchoolAttended = "Samar National School",
            SchoolAddress = "Catbalogan City",
            LRN = "012345678912",
            Awards = "N/A",
            YearGraduated = 2023,
            SchoolSector = "Government",
            ElementarySchoolAttended = "Catbalogan II Central School",
            ElementaryInclusiveYear = "2010 - 2016",
            ElementarySchoolAddress = "Catbalogan City",
            ElementaryAwards = "N/A",
            JuniorSchoolAttended = "Samar National School",
            JuniorInclusiveYear = "2017-2022",
            JuniorSchoolAddress = "Catbalogan City",
            JuniorAward = "N/A",
            SeniorSchoolAttended = "Samar National School",
            SeniorInclusiveYear = "2021-2023",
            SeniorSchoolAddress = "Catbalogan City",
            SeniorAward = "N/A",
            CollegeSchoolAttended = "N/A",
            CollegeSchoolAddress = "N/A",
            CollegeAwards = "N/A",
            CollegeInclusiveYear = "N/A",
            GraduateSchoolAttended = "N/A",
            GraduateInclusiveYear = "N/A",
            GraduateSchoolAddress = "N/A",
            GraduateAwards = "N/A",
            PostGraduateSchoolAttended = "N/A",
            PostGraduaterInclusiveYear = "N/A",
            PostGraduateSchoolAddress = "N/A",
            PostGraduateAward = "N/A",
            BenefactorRelation = "N/A",
            Organization = "N/A",
            EducationBenefactor = "N/A",
            Scholarship = "N/A",
            Motto = "N/A",
            PlanAfterCollege = "Work",
            Skills = "Programming",
            LastAttended = "Samar National School",
            LastSchoolAddress = "Catbalogan City",
            LastAwards = "N/A",
            LastInclusiveYear = "2021-2023",
        };
        Context.AcademicBackgrounds.Add(acadRequest);

        var parRequest = new ParentGuardianInformation
        {
            ApplicantId = applicant.Id,
            FatherFirstName = "Johny",
            FatherMiddleName = "Melborne",
            FatherLastName = "Adams",
            FatherContactNo = "123-456-7890",
            FatherCitizenship = "American",
            FatherEmail = "john.doe@example.com",
            FatherOccupation = "Engineer",
            MotherFirstName = "Jane",
            MotherMiddleName = "Quincy",
            MotherLastName = "Adams",
            MotherContactNo = "098-765-4321",
            MotherCitizenship = "American",
            MotherEmail = "jane.doe@example.com",
            MotherOccupation = "Teacher",
            GuardianFirstName = "Jack",
            GuardianMiddleName = "B",
            GuardianLastName = "Smith",
            GuardianContactNo = "555-123-4567",
            GuardianCitizenship = "American",
            GuardianEmail = "jack.smith@example.com",
            GuardianOccupation = "Accountant",
            FatherBirthday = new DateTime(1970, 1, 15),
            FatherBirthPlace = "New York",
            FatherReligion = "Christian",
            FatherMaritalStatus = "Married",
            FatherDialect = "English",
            FatherPermanentAddress = "123 Elm Street, New York, NY 10001",
            FatherEducation = "Bachelor's Degree",
            FatherEstimatedMonthly = "5000",
            FatherOtherIncome = "200",
            MotherBirthday = new DateTime(1972, 5, 22),
            MotherBirthPlace = "Los Angeles",
            MotherReligion = "Christian",
            MotherMaritalStatus = "Married",
            MotherDialect = "English",
            MotherPermanentAddress = "123 Elm Street, New York, NY 10001",
            MotherEducation = "Master's Degree",
            MotherEstimatedMonthly = "4500",
            MotherOtherIncome = "150",
            GuardianBirthday = new DateTime(1965, 9, 30),
            GuardianBirthPlace = "Chicago",
            GuardianReligion = "Christian",
            GuardianMaritalStatus = "Single",
            GuardianDialect = "English",
            GuardianPermanentAddress = "456 Oak Avenue, Chicago, IL 60614",
            GuardianEducation = "Associate Degree",
            GuardianEstimatedMonthly = "3000",
            GuardianOtherIncome = "100"
        };
        Context.ParentGuardianInformations.Add(parRequest);

        for (int i = 1; i <= (applicant.PersonalInformation.Brothers + applicant.PersonalInformation.Sisters); i++)
        {
            var famRequest = new FamilyRelation
            {
                ApplicantId = applicant.Id,
                FullName = "Brother" + i,
                GradeCourse = "Grade" + i,
                MonthlyIncome = "N/A",
                SchoolOccupation = "N/A",
                Sex = "Male",
                Birthday = new DateTime(2000 + i, 5 + i, 25 + i),
            };
            Context.FamilyRelations.Add(famRequest);
        }

        var physicalRequest = new PhysicalHealth
        {
            ApplicantId = applicant.Id,
            AccidentsExperienced = "None",
            ChronicIllnesses = "None",
            IsWithDisability = false,
            Medicines = "None",
            OperationsExperienced = "None",
        };
        Context.PhysicalHealths.Add(physicalRequest);

        Random random = new Random();
        bool response = random.Next(2) == 0;
        if (response == true)
        {
            var councelorRequest = new CouncelorConsultation
            {
                ApplicantId = applicant.Id,
                Start = new DateTime(2023, 5, 5),
                End = new DateTime(2023, 11, 6),
                Sessions = 2,
                Reasons = "Too Personal"
            };
            Context.CouncelorConsultations.Add(councelorRequest);
        }

        response = random.Next(2) == 0;
        if (response == true)
        {
            var psychiatristRequest = new PsychiatristConsultation
            {
                ApplicantId = applicant.Id,
                Start = new DateTime(2023, 5, 5),
                End = new DateTime(2023, 11, 6),
                Sessions = 2,
                Reasons = "Too Personal"
            };
            Context.PsychiatristConsultations.Add(psychiatristRequest);  
        }

        response = random.Next(2) == 0;
        if (response == true)
        {
            var psychologistRequest = new PsychologistConsultation
            {
                ApplicantId = applicant.Id,
                Start = new DateTime(2023, 5, 5),
                End = new DateTime(2023, 11, 6),
                Sessions = 2,
                Reasons = "Too Personal"
            };
            Context.PsychologistConsultations.Add(psychologistRequest);
        }

        var personalityRequest = new PersonalityProfile
        {
            ApplicantId = applicant.Id,
            Active = true,
            Adaptable = true,
            Cautious = true,
            Confident = true,
            Conforming = true,
            Creative = true,
            Conscientious = true,
            Friendly = true,
            Generous = true,
            GoodNatured = true,
            EmotionallyStable = true,
            HabituallySilent = true,
            Industrious = true,
            Organized = true,
            PreferredByGroups = true,
            Polite = true,
            Resourceful = true,
            Outgoing = false,
            TakesChargeWhenAssigned = true,
            Truthful = true,
            VolunteersToLead = false,
            WellGroomed = true,
            WorksWillWithOthers = true,
            WorksPromptly = true,
            SelfControl = true,
            Studies = false,
            Family = false,
            Friend = false,
            Problems = "",
            ComfortableDiscussing = "Sample",
            Self = false,
            Specify = "Sample"
        };
        Context.PersonalityProfiles.Add(personalityRequest);

        var emergencyRequest = new EmergencyContact
        {
            ApplicantId = applicant.Id,
            Address = "Catbalogan City",
            ContactNo = "09998887456",
            Name = "Mikee Doe",
            Relationship = "Relatives"
        };
        Context.EmergencyContacts.Add(emergencyRequest);
       

        var registeredRequest = new Registered
        {
            ApplicantId = applicant.Id,
            RegistrationDate = DateTime.Now,
        };
        Context.Registereds.Add(registeredRequest);

        Context.SaveChanges();
    }

    protected async Task<ResponseWrapper<UserResponse>> LoginDefault()
    {
        var model = new LoginRequest { Username = "admin", Password = "admin" };
        var loginResult = await Connect.Authentication.Login(model);
        return loginResult;
    }

    protected async Task<ResponseWrapper<PagedList<ScheduleResponse>>> SchedulesDefault()
    {
        var user = await LoginDefault();
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 100,
            SortField = nameof(Schedule.ScheduleDate),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var scheduleModels = await Connect.Schedule.List(listQuery, user.Data.Access);
        return scheduleModels;
    }

    protected async Task<ResponseWrapper<PagedList<ApplicantResponse>>> ApplicantsDefault()
    {
        var user = await LoginDefault();
        var listQuery = new DataGridQuery
        {
            Page = 0,
            PageSize = 100,
            SortField = nameof(Applicant.PersonalInformation.LastName),
            SortDir = DataGridQuerySortDirection.Ascending
        };

        var applicantModels = await Connect.Applicant.List(listQuery, null, user.Data.Access);
        return applicantModels;
    }

    public void Dispose()
    {
        Context?.Dispose();
        Connection?.Dispose();
        Scope?.Dispose();
    }
}
