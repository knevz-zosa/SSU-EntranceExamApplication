namespace Common.Responses;
public class PhysicalHealthResponse
{
    public int Id { get; set; }
    public bool IsWithDisability { get; set; }
    public string? Disability { get; set; }
    public string? ChronicIllnesses { get; set; }
    public string? Medicines { get; set; }
    public string? AccidentsExperienced { get; set; }
    public string? OperationsExperienced { get; set; }
    public string? PWDId { get; set; }
    public DateTime? DateIssued { get; set; }
    public DateTime? Expiration { get; set; }
}
