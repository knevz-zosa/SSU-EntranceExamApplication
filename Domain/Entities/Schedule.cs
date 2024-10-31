using Domain.Contracts;
namespace Domain.Entities;
public class Schedule : BaseEntity<int>
{
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public List<Applicant> Applicants { get; set; }
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
}
