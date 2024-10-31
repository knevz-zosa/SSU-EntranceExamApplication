using Domain.Contracts;
namespace Domain.Entities;
public class SoloParent : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public string? SoloParentId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
