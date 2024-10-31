using Domain.Contracts;
namespace Domain.Entities;
public class Interview : BaseEntity<int>
{
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }
    public int InterviewCommunication { get; set; }
    public int InterviewAnalytical { get; set; }
    public int ApplicantId { get; set; }
    public bool IsUse { get; set; }
    public Applicant Applicant { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string Interviewer { get; set; }

    public Interview UpdateRating(int interviewReading, int interviewCommunication, int interviewAnalytical, string? updatedBy, string interviewer)
    {
        InterviewReading = interviewReading;
        InterviewCommunication = interviewCommunication;
        InterviewAnalytical = interviewAnalytical;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        Interviewer = interviewer;
        return this;
    }

    public Interview UpdateIsActive(bool isUse, string? updatedBy)
    {
        IsUse = isUse;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        return this;
    }
}
