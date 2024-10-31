using Domain.Contracts;
namespace Domain.Entities;
public class FamilyRelation : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public string? FullName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? Sex { get; set; }
    public string? GradeCourse { get; set; }
    public string? SchoolOccupation { get; set; }
    public string? MonthlyIncome { get; set; }
}
