namespace Common.Responses;
public class ApplicantResponse
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public ScheduleResponse Schedule { get; set; }
    public PersonalInformationResponse PersonalInformation { get; set; }
    public SpouseResponse? Spouse { get; set; }
    public SoloParentResponse? SoloParent { get; set; }
    public AcademicBackgroundResponse AcademicBackground { get; set; }
    public ParentGuardianInformationResponse ParentGuardianInformation { get; set; }
    public PersonalityProfileResponse PersonalityProfile { get; set; }
    public PhysicalHealthResponse PhysicalHealth { get; set; }
    public PsychiatristConsultationResponse? PsychiatristConsultation { get; set; }
    public CouncelorConsultationResponse? CouncelorConsultation { get; set; }
    public PsychologistConsultationResponse? PsychologistConsultation { get; set; }
    public EmergencyContactResponse EmergencyContact { get; set; }
    public List<FamilyRelationResponse> FamilyRelations { get; set; }
    public List<InterviewResponse>? Interviews { get; set; }
    public List<TransferResponse>? Transfers { get; set; }
    public ExaminationResponse? Examination { get; set; }
    public FirstApplicationInfoResponse FirstApplicationInfo { get; set; }
    public RegisteredResponse Registered { get; set; }
    public string ControlNo { get; set; }
    public DateTime TransactionDate { get; set; }

    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public double? GWA { get; set; }
    public bool IsAdmitted { get; set; }
    public string? StudentId { get; set; }  
}
