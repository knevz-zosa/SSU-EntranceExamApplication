using Domain.Contracts;
namespace Domain.Entities;
public class Campus : BaseEntity<int>
{
    public string Name { get; set; }
    public string Address { get; set; }
    public bool HasDepartment { get; set; } = false;
    public List<Department> Departments { get; set; } = new List<Department>();
    public List<Course> Courses { get; set; } = new List<Course>();
    public List<Schedule> Schedules { get; set; } = new List<Schedule>();
    public DateTime DateCreated { get; set; } = DateTime.Now;
    public DateTime? DateUpdated { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Campus Update(string name, string address, bool hasDepartment, string updatedBy)
    {
        Name = name;
        Address = address;
        HasDepartment = hasDepartment;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        return this;
    }
}
