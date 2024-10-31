namespace Common.Responses;
public class CampusResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public bool HasDepartment { get; set; }
    public List<DepartmentResponse> Departments { get; set; } = new List<DepartmentResponse>();
    public List<CourseResponse> Courses { get; set; } = new List<CourseResponse>();
    public List<ScheduleResponse> Schedules { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
