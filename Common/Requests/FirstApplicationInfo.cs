namespace Common.Requests;
public class FirstApplicationInfoRequest
{
    public int ApplicantId { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string Track { get; set; }
    public string ApplicantStatus { get; set; }
    public DateTime TransactionDate { get; set; }
}
