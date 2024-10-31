using Domain.Contracts;
namespace Domain.Entities;
public class Course : BaseEntity<int>
{
    public string Name { get; set; }
    public int? DepartmentId { get; set; }
    public Department? Department { get; set; }
    public int CampusId { get; set; }
    public Campus Campus { get; set; }
    public bool IsOpen { get; set; } = true;
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Course Update(int campusId, int? departmentId, string name, string updatedBy, bool isOpen)
    {
        Name = name;
        CampusId = campusId;
        DepartmentId = departmentId;
        IsOpen = isOpen;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        return this;
    }
}
