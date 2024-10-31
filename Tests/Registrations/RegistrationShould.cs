using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;

namespace Tests.Registrations;
public class RegistrationShould : TestBaseIntegration
{
    [Fact]
    public async Task PerfomRegistrationMethods()
    {
        var listSchedule = await SchedulesDefault();
        var sched = listSchedule.Data.List.FirstOrDefault();
        var applicants = await ApplicantsDefault();
        var applicant = applicants.Data.List.FirstOrDefault();

        if(sched != null)
        {
            var appRequest = new ApplicantRequest
            {
                ScheduleId = sched.Id,
                CourseId = sched.Campus.Schedules.Select(x => x.Id).FirstOrDefault(),
                TransactionDate = DateTime.Now,
                ApplicantStatus = "New",
                Track = "GAS", 
                
            };

            var appResult = await Connect.Applicant.Create(appRequest);
            Assert.True(appResult.IsSuccessful);
            var appId = appResult.Data;

            // Act & Assert: Verify applicant was created
            var applModel = await Connect.Applicant.Get(appId);
            Assert.NotNull(applModel.Data);
            Assert.Equal(2, applModel.Data.Id);

            var firstAppRequest = new FirstApplicationInfoRequest
            {
                ApplicantId = appId,
                CourseId = applModel.Data.CourseId,
                ScheduleId = applModel.Data.ScheduleId,
                ApplicantStatus = applModel.Data.ApplicantStatus,
                TransactionDate = DateTime.Now,
                Track = applModel.Data.Track
            };

            var firstappResult = await Connect.FirstApplicationInfo.Create(firstAppRequest);
            Assert.True(firstappResult.IsSuccessful);
            var firstappId = firstappResult.Data;

            // Act & Assert: Verify first application info was created
            var firstAppModel = await Connect.FirstApplicationInfo.Get(firstappId);
            Assert.NotNull(firstAppModel.Data);
            Assert.Equal(2, firstAppModel.Data.Id);

            // Validation: Try creating existing personal information's Firstname, Middlename, Lastname, and birthday for the same school year, expect failure
            var existingapplicant = new PersonalInformationRequest
            {
                ApplicantId = appId,
                FirstName = applicant.PersonalInformation.FirstName,
                MiddleName = applicant.PersonalInformation.MiddleName,
                LastName = applicant.PersonalInformation.LastName,
                NickName = "Ja",
                Sex = "Male",
                CivilStatus = "Married",
                PlaceOfBirth = "Catbalogan City",
                Citizenship = "Filipino",
                Religion = "Catholic",
                Email = "test@email.com",
                ContactNumber = "09998798521",
                DateofBirth = applicant.PersonalInformation.DateofBirth.Date,
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

            // Act & Assert: Expect a ResponseException to be thrown for the duplicate
            var exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
                await Connect.PersonalInformation.Create(existingapplicant));

            // Assert: Check the exception message
            Assert.Contains("Applicant with this name already exists.", exceptionCreate.Message);

            var perInfRequest = new PersonalInformationRequest
            {
                ApplicantId = appId,
                FirstName = "John",
                MiddleName = "Quincy",
                LastName = "Adams",
                NickName = "Ja",
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

            var perInfResult = await Connect.PersonalInformation.Create(perInfRequest);
            Assert.True(perInfResult.IsSuccessful);
            var perInfId = perInfResult.Data;

            // Act & Assert: Verify applicant personal information was created
            var perInfModel = await Connect.PersonalInformation.Get(perInfId);
            Assert.NotNull(perInfModel.Data);
            Assert.Equal(2, perInfModel.Data.Id);

            if (perInfModel.Data.CivilStatus == "Married" || perInfModel.Data.CivilStatus == "Living-In")
            {
                
                var spouseRequest = new SpouseRequest()
                {
                    ApplicantId = appId,
                    FullName = "Lyn Jamison Adams",
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
                var spouseResult = await Connect.Spouse.Create(spouseRequest);
                Assert.True(spouseResult.IsSuccessful);
            }

            // Validation: Try creating existing LRN, expect failure
            var existingLrn = new AcademicBackgroundRequest
            {
                ApplicantId = appId,
                SchoolAttended = "Samar National School",
                SchoolAddress = "Catbalogan City",
                LRN = applicant.AcademicBackground.LRN,
                Awards = "N/A",
                YearGraduated = 2023,
                SchoolSector = "Government",
                ElementarySchoolAttended = "Catbalogan II Central School",
                ElementaryInclusiveYear = "2010 - 2016",
                ElementarySchoolAddress = "Catbalogan City",
                ElementaryAward = "N/A",
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
                CollegeAward = "N/A",
                CollegeInclusiveYear = "N/A",
                GraduateSchoolAttended = "N/A",
                GraduateInclusiveYear = "N/A",
                GraduateSchoolAddress = "N/A",
                GraduateAward = "N/A",
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

            // Act & Assert: Expect a ResponseException to be thrown for the duplicate
            exceptionCreate = await Assert.ThrowsAsync<ResponseException>(async () =>
                await Connect.AcademicBackground.Create(existingLrn));

            // Assert: Check the exception message
            Assert.Contains("Applicant with the same LRN already exists.", exceptionCreate.Message);

            var acadRequest = new AcademicBackgroundRequest
            {
                ApplicantId = appId,
                SchoolAttended = "Samar National School",
                SchoolAddress = "Catbalogan City",
                LRN = "012345678913",
                Awards = "N/A",
                YearGraduated = 2023,
                SchoolSector = "Government",
                ElementarySchoolAttended = "Catbalogan II Central School",
                ElementaryInclusiveYear = "2010 - 2016",
                ElementarySchoolAddress = "Catbalogan City",
                ElementaryAward = "N/A",
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
                CollegeAward = "N/A",
                CollegeInclusiveYear = "N/A",
                GraduateSchoolAttended = "N/A",
                GraduateInclusiveYear = "N/A",
                GraduateSchoolAddress = "N/A",
                GraduateAward = "N/A",
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
            var acadResult = await Connect.AcademicBackground.Create(acadRequest);
            Assert.True(acadResult.IsSuccessful);

            var parRequest = new ParentGuardianInformationRequest
            {
                ApplicantId = appId,
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

            var parResult = await Connect.ParentGuardianInformation.Create(parRequest);
            Assert.True(parResult.IsSuccessful);

            for (int i = 1; i <= 3; i++)
            {
                var famRequest = new FamilyRelationRequest
                {
                    ApplicantId = appId,
                    FullName = "Brother" + i,
                    GradeCourse = "Grade" + i,
                    MonthlyIncome = "N/A",
                    SchoolOccupation = "N/A",
                    Sex = "Male",
                    Birthday = new DateTime(2000 + i, 5 + i, 25 + i),
                };
                var famResult = await Connect.FamilyRelation.Create(famRequest);
                Assert.True(famResult.IsSuccessful);
            }

            var physicalRequest = new PhysicalHealthRequest
            {
                ApplicantId = appId,
                AccidentsExperienced = "None",
                ChronicIllnesses = "None",
                IsWithDisability = false,
                Medicines = "None",
                OperationsExperienced = "None",
            };

            var physicalResult = await Connect.PhysicalHealth.Create(physicalRequest);
            Assert.True(physicalResult.IsSuccessful);

            Random random = new Random();
            bool response = random.Next(2) == 0;
            if(response == true)
            {
                var councelorRequest = new CouncelorConsultationRequest
                {
                    ApplicantId = appId,
                    Start = new DateTime(2023, 5, 5),
                    End = new DateTime(2023, 11, 6),
                    Sessions = 2,
                    Reasons = "Too Personal"
                };
                var councelorResult = await Connect.CouncelorConsultation.Create(councelorRequest);
                Assert.True(councelorResult.IsSuccessful);
            }


            response = random.Next(2) == 0;
            if(response == true)
            {
                var psychiatristRequest = new PsychiatristConsultationRequest
                {
                    ApplicantId = appId,
                    Start = new DateTime(2023, 5, 5),
                    End = new DateTime(2023, 11, 6),
                    Sessions = 2,
                    Reasons = "Too Personal"
                };
                var psychiatristResult = await Connect.PsychiatristConsultation.Create(psychiatristRequest);
                Assert.True(psychiatristResult.IsSuccessful);
            }
            response = random.Next(2) == 0;

            if( response == true)
            {
                var psychologistRequest = new PsychologistConsultationRequest
                {
                    ApplicantId = appId,
                    Start = new DateTime(2023, 5, 5),
                    End = new DateTime(2023, 11, 6),
                    Sessions = 2,
                    Reasons = "Too Personal"
                };
                var psychologistResult = await Connect.PsychologistConsultation.Create(psychologistRequest);
                Assert.True(psychologistResult.IsSuccessful);

            }

            var personalityRequest = new PersonalityProfileRequest
            {
                ApplicantId = appId,
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
            var personalityResult = await Connect.PersonalityProfile.Create(personalityRequest);
            Assert.True(personalityResult.IsSuccessful);

            var emergencyRequest = new EmergencyContactRequest
            {
                ApplicantId = appId,
                Address = "Catbalogan City",
                ContactNo = "09998887456",
                Name = "Mikee Doe",
                Relationship = "Relatives"
            };

            var emergencyResult = await Connect.EmergencyContact.Create(emergencyRequest);
            Assert.True(emergencyResult.IsSuccessful);

            var registeredRequest = new RegisteredRequest
            {
                ApplicantId = appId,
                RegistrationDate = DateTime.Now,
            };

            var regestiredResult = await Connect.Registered.Create(registeredRequest);
            Assert.True(regestiredResult.IsSuccessful);


            // Arrange: check for applicant for successfull registration
            var listQuery = new DataGridQuery
            {
                Page = 0,
                PageSize = 10,
                SortField = nameof(Applicant.PersonalInformation.LastName),
                SortDir = DataGridQuerySortDirection.Ascending
            };
            var Models = await Connect.Applicant.List(listQuery, sched.Id, "All");
            Assert.NotNull(Models);
            Assert.True(Models.IsSuccessful);
            Assert.True(Models.Data.List.Any());
            Assert.Contains(Models.Data.List, x => x.Id == applModel.Data.Id);
        }
        
    }
}
