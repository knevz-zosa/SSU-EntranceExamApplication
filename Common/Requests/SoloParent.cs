namespace Common.Requests;
public class SoloParentRequest
{
    public int ApplicantId { get; set; }
    public string? SoloParentId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
}
