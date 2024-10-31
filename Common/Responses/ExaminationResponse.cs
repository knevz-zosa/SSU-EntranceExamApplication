namespace Common.Responses;
public class ExaminationResponse
{
    public int Id { get; set; }
    public int ReadingRawScore { get; set; }
    public int MathRawScore { get; set; }
    public int ScienceRawScore { get; set; }
    public int IntelligenceRawScore { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }
}
