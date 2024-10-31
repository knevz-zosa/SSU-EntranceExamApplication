using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Campuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HasDepartment = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Access = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Schedules",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SchoolYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Venue = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Slot = table.Column<int>(type: "int", nullable: false),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedules", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedules_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentId = table.Column<int>(type: "int", nullable: true),
                    CampusId = table.Column<int>(type: "int", nullable: false),
                    IsOpen = table.Column<bool>(type: "bit", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Courses_Campuses_CampusId",
                        column: x => x.CampusId,
                        principalTable: "Campuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Courses_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Applicants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    ControlNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GWA = table.Column<double>(type: "float", nullable: true),
                    ApplicantStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applicants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applicants_Schedules_ScheduleId",
                        column: x => x.ScheduleId,
                        principalTable: "Schedules",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AcademicBackgrounds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    SchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LRN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Awards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearGraduated = table.Column<int>(type: "int", nullable: false),
                    SchoolSector = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementarySchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementaryInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementarySchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ElementaryAwards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JuniorSchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JuniorInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JuniorSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JuniorAward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeniorSchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeniorInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeniorSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeniorAwards = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAttended = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastAwards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeSchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CollegeAwards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateSchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GraduateAwards = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGraduateSchoolAttended = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGraduaterInclusiveYear = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGraduateSchoolAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostGraduateAward = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SY = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Semester = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Scholarship = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EducationBenefactor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BenefactorRelation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Organization = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PlanAfterCollege = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Motto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Skills = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AcademicBackgrounds", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AcademicBackgrounds_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CouncelorConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sessions = table.Column<int>(type: "int", nullable: true),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouncelorConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CouncelorConsultations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContacts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Relationship = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContacts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmergencyContacts_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Examinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReadingRawScore = table.Column<int>(type: "int", nullable: false),
                    MathRawScore = table.Column<int>(type: "int", nullable: false),
                    ScienceRawScore = table.Column<int>(type: "int", nullable: false),
                    IntelligenceRawScore = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Examinations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Examinations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FamilyRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradeCourse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SchoolOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MonthlyIncome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyRelations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FirstApplicationInfos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    ScheduleId = table.Column<int>(type: "int", nullable: false),
                    Track = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ScheduleTime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicantStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FirstApplicationInfos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FirstApplicationInfos_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Interviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterviewDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    InterviewReading = table.Column<int>(type: "int", nullable: false),
                    InterviewCommunication = table.Column<int>(type: "int", nullable: false),
                    InterviewAnalytical = table.Column<int>(type: "int", nullable: false),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    IsUse = table.Column<bool>(type: "bit", nullable: false),
                    DateRecorded = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DateUpdated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RecordedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Interviewer = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Interviews_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ParentGuardianInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    FatherFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherCitizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FatherBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FatherBirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherReligion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherMaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherDialect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherPermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherEstimatedMonthly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherOtherIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherCitizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MotherBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MotherBirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherReligion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherMaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherDialect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherPermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherEstimatedMonthly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherOtherIncome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianFirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianMiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianLastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianContactNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianCitizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianOccupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GuardianBirthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    GuardianBirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianReligion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianMaritalStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianDialect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianPermanentAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianEducation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianEstimatedMonthly = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GuardianOtherIncome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ParentGuardianInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ParentGuardianInformations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalInformations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameExtension = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NickName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CivilStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlaceOfBirth = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Citizenship = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Religion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateofBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HouseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Purok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentPurok = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentStreet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentBarangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentHouseNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CurrentMunicipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentProvince = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Dialect = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsIndigenous = table.Column<bool>(type: "bit", nullable: false),
                    TribalAffiliation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Is4psMember = table.Column<bool>(type: "bit", nullable: false),
                    HouseHold4psNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Brothers = table.Column<int>(type: "int", nullable: false),
                    Sisters = table.Column<int>(type: "int", nullable: false),
                    BirthOrder = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalInformations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalInformations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalityProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    WellGroomed = table.Column<bool>(type: "bit", nullable: false),
                    Friendly = table.Column<bool>(type: "bit", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Confident = table.Column<bool>(type: "bit", nullable: false),
                    Polite = table.Column<bool>(type: "bit", nullable: false),
                    SelfControl = table.Column<bool>(type: "bit", nullable: false),
                    WorksPromptly = table.Column<bool>(type: "bit", nullable: false),
                    Adaptable = table.Column<bool>(type: "bit", nullable: false),
                    Outgoing = table.Column<bool>(type: "bit", nullable: false),
                    Organized = table.Column<bool>(type: "bit", nullable: false),
                    Creative = table.Column<bool>(type: "bit", nullable: false),
                    Truthful = table.Column<bool>(type: "bit", nullable: false),
                    HabituallySilent = table.Column<bool>(type: "bit", nullable: false),
                    Generous = table.Column<bool>(type: "bit", nullable: false),
                    Conforming = table.Column<bool>(type: "bit", nullable: false),
                    Resourceful = table.Column<bool>(type: "bit", nullable: false),
                    Cautious = table.Column<bool>(type: "bit", nullable: false),
                    Conscientious = table.Column<bool>(type: "bit", nullable: false),
                    GoodNatured = table.Column<bool>(type: "bit", nullable: false),
                    Industrious = table.Column<bool>(type: "bit", nullable: false),
                    EmotionallyStable = table.Column<bool>(type: "bit", nullable: false),
                    WorksWillWithOthers = table.Column<bool>(type: "bit", nullable: false),
                    VolunteersToLead = table.Column<bool>(type: "bit", nullable: false),
                    PreferredByGroups = table.Column<bool>(type: "bit", nullable: false),
                    TakesChargeWhenAssigned = table.Column<bool>(type: "bit", nullable: false),
                    Problems = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComfortableDiscussing = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Studies = table.Column<bool>(type: "bit", nullable: false),
                    Family = table.Column<bool>(type: "bit", nullable: false),
                    Friend = table.Column<bool>(type: "bit", nullable: false),
                    Self = table.Column<bool>(type: "bit", nullable: false),
                    Specify = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalityProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PersonalityProfiles_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalHealths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    IsWithDisability = table.Column<bool>(type: "bit", nullable: false),
                    Disability = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ChronicIllnesses = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Medicines = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AccidentsExperienced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OperationsExperienced = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PWDId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateIssued = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Expiration = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalHealths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalHealths_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychiatristConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sessions = table.Column<int>(type: "int", nullable: true),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychiatristConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychiatristConsultations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PsychologistConsultations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Sessions = table.Column<int>(type: "int", nullable: true),
                    Reasons = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PsychologistConsultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PsychologistConsultations_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SoloParents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    SoloParentId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    End = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoloParents", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoloParents_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spouses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BirthPlace = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Occupation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Education = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OfficeAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Barangay = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Municipality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Province = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spouses_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transfers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApplicantId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FromCampus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FromProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToCampus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ToProgram = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TransferDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transfers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transfers_Applicants_ApplicantId",
                        column: x => x.ApplicantId,
                        principalTable: "Applicants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Transfers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AcademicBackgrounds_ApplicantId",
                table: "AcademicBackgrounds",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Applicants_ScheduleId",
                table: "Applicants",
                column: "ScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_CouncelorConsultations_ApplicantId",
                table: "CouncelorConsultations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_CampusId",
                table: "Courses",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_DepartmentId",
                table: "Courses",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_CampusId",
                table: "Departments",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmergencyContacts_ApplicantId",
                table: "EmergencyContacts",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Examinations_ApplicantId",
                table: "Examinations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FamilyRelations_ApplicantId",
                table: "FamilyRelations",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_FirstApplicationInfos_ApplicantId",
                table: "FirstApplicationInfos",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Interviews_ApplicantId",
                table: "Interviews",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_ParentGuardianInformations_ApplicantId",
                table: "ParentGuardianInformations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalInformations_ApplicantId",
                table: "PersonalInformations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PersonalityProfiles_ApplicantId",
                table: "PersonalityProfiles",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalHealths_ApplicantId",
                table: "PhysicalHealths",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsychiatristConsultations_ApplicantId",
                table: "PsychiatristConsultations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PsychologistConsultations_ApplicantId",
                table: "PsychologistConsultations",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Schedules_CampusId",
                table: "Schedules",
                column: "CampusId");

            migrationBuilder.CreateIndex(
                name: "IX_SoloParents_ApplicantId",
                table: "SoloParents",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Spouses_ApplicantId",
                table: "Spouses",
                column: "ApplicantId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_ApplicantId",
                table: "Transfers",
                column: "ApplicantId");

            migrationBuilder.CreateIndex(
                name: "IX_Transfers_UserId",
                table: "Transfers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AcademicBackgrounds");

            migrationBuilder.DropTable(
                name: "CouncelorConsultations");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "EmergencyContacts");

            migrationBuilder.DropTable(
                name: "Examinations");

            migrationBuilder.DropTable(
                name: "FamilyRelations");

            migrationBuilder.DropTable(
                name: "FirstApplicationInfos");

            migrationBuilder.DropTable(
                name: "Interviews");

            migrationBuilder.DropTable(
                name: "ParentGuardianInformations");

            migrationBuilder.DropTable(
                name: "PersonalInformations");

            migrationBuilder.DropTable(
                name: "PersonalityProfiles");

            migrationBuilder.DropTable(
                name: "PhysicalHealths");

            migrationBuilder.DropTable(
                name: "PsychiatristConsultations");

            migrationBuilder.DropTable(
                name: "PsychologistConsultations");

            migrationBuilder.DropTable(
                name: "SoloParents");

            migrationBuilder.DropTable(
                name: "Spouses");

            migrationBuilder.DropTable(
                name: "Transfers");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Applicants");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Schedules");

            migrationBuilder.DropTable(
                name: "Campuses");
        }
    }
}
