using Domain.Contracts;
namespace Domain.Entities;
public class Transfer : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
    public string FromCampus { get; set; }
    public string FromProgram { get; set; }
    public string ToCampus { get; set; }
    public string ToProgram { get; set; }
    public DateTime TransferDate { get; set; }
}
