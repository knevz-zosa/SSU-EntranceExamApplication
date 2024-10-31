namespace Common.Responses;
public class FirstApplicationInfoResponse
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string Track { get; set; }
    public string ApplicantStatus { get; set; }
    public DateTime TransactionDate { get; set; }
}
