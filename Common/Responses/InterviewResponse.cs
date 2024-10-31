namespace Common.Responses;
public class InterviewResponse
{
    public int Id { get; set; }
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }
    public int InterviewCommunication { get; set; }
    public int InterviewAnalytical { get; set; }
    public bool IsUse { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public string Interviewer { get; set; }
}
