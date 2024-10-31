namespace Common.Responses;
public class DepartmentResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CampusId { get; set; }
    public CampusResponse Campus { get; set; }
    public List<CourseResponse> Courses { get; set; }
    public string CreatedBy { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? DateUpdated { get; set; }
}
