namespace Common.Responses;
public class ScheduleResponse
{
    public int Id { get; set; }
    public DateTime ScheduleDate { get; set; }
    public string SchoolYear { get; set; }
    public string Venue { get; set; }
    public string Time { get; set; }
    public int Slot { get; set; }
    public int CampusId { get; set; }
    public CampusResponse Campus { get; set; }
    public List<ApplicantResponse> Applicants { get; set; }
    public DateTime DateCreated { get; set; }
    public string CreatedBy { get; set; }
}
