using Domain.Contracts;
namespace Domain.Entities;
public class FirstApplicationInfo : BaseEntity<int>
{
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    public string Track { get; set; }
    public string ApplicantStatus { get; set; }
    public DateTime TransactionDate { get; set; }
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
}