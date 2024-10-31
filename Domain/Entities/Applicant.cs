using Domain.Contracts;

namespace Domain.Entities;
public class Applicant : BaseEntity<int>
{
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public Schedule Schedule { get; set; }
    public PersonalInformation PersonalInformation { get; set; }
    public Spouse? Spouse { get; set; }
    public SoloParent? SoloParent { get; set; }
    public AcademicBackground AcademicBackground { get; set; }
    public ParentGuardianInformation ParentGuardianInformation { get; set; }
    public PersonalityProfile PersonalityProfile { get; set; }
    public PhysicalHealth PhysicalHealth { get; set; }
    public PsychiatristConsultation? PsychiatristConsultation { get; set; }
    public CouncelorConsultation? CouncelorConsultation { get; set; }
    public PsychologistConsultation? PsychologistConsultation { get; set; }
    public EmergencyContact EmergencyContact { get; set; }

    public List<FamilyRelation> FamilyRelations { get; set; }
    public List<Interview>? Interviews { get; set; }
    public List<Transfer>? Transfers { get; set; }
    public Examination? Examination { get; set; }
    public FirstApplicationInfo FirstApplicationInfo { get; set; }
    public Registered? Registered { get; set; }
    public string ControlNo { get; set; }
    public DateTime TransactionDate { get; set; }
    public double? GWA { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
    public string? StudentId { get; set; }    

    public Applicant Transfer(int courseId, int scheduleId, Schedule schedule, Course course)
    {
        CourseId = courseId;
        ScheduleId = scheduleId;
        Schedule = schedule;
        return this;
    }

    public Applicant UpdateGwaStatusTrack(double gwa, string status, string track)
    {
        GWA = gwa;
        ApplicantStatus = status;
        Track = track;
        return this;
    }

    public Applicant UpdateStudentId(string? studentId)
    {
        StudentId = studentId;
        return this;
    }

}
