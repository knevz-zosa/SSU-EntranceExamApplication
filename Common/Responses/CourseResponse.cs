namespace Common.Responses;
public class CourseResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? DepartmentId { get; set; }
    public DepartmentResponse? Department { get; set; }
    public int CampusId { get; set; }
    public CampusResponse Campus { get; set; }
    public bool IsOpen { get; set; } = false;
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }

}
