using Domain.Contracts;
namespace Domain.Entities;
public class PhysicalHealth : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public bool IsWithDisability { get; set; } 
    public string? Disability { get; set; }
    public string? ChronicIllnesses { get; set; }
    public string? Medicines { get; set; }
    public string? AccidentsExperienced { get; set; }
    public string? OperationsExperienced { get; set; }
    public string? PWDId { get; set; }
    public DateTime? DateIssued { get; set; }
    public DateTime? Expiration { get; set; }
}
