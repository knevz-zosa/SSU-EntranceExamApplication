using Domain.Contracts;
namespace Domain.Entities;
public class Department : BaseEntity<int>
{
    public string Name { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public List<Course> Courses { get; set; } = new List<Course>();
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public Department Update(int campusId, string name, string updatedBy)
    {
        Name = name;
        CampusId = campusId;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        return this;
    }
}
