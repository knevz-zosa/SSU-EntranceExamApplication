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

namespace Services._Connector;
public interface IConnectionService
{
    public IAuthService Authentication { get; }
    public ICampusService Campus { get; }
    public IDepartmentService Department { get; }
    public ICourseService Course { get; }
    public IScheduleService Schedule { get; }
    public IApplicantService Applicant { get; }
    public IPersonalInformationService PersonalInformation { get; }
    public IAcademicBackgroundService AcademicBackground { get; }
    public IParentGuardianInformationService ParentGuardianInformation { get; }
    public IPhysicalHealthService PhysicalHealth { get; }
    public IFamilyRelationService FamilyRelation { get; }
    public IPersonalityProfileService PersonalityProfile { get; }
    public ICouncelorConsultationService CouncelorConsultation { get; }
    public IPsychiatristConsultationService PsychiatristConsultation { get; }
    public IPsychologistConsultationService PsychologistConsultation { get; }
    public IFirstApplicationInfoService FirstApplicationInfo { get; }
    public ISoloParentService SoloParent { get; }
    public IUserService User { get; }
    public ISpouseService Spouse { get; }
    public ITransferService Transfer { get; }
    public IRegisteredService Registered { get; }
    public IInterviewService Interview { get; }
    public IExaminationService Examination { get; }
    public IEmergencyContactService EmergencyContact { get; }
}
