using Domain.Contracts;
namespace Domain.Entities;
public class Examination : BaseEntity<int>
{
    public int ReadingRawScore { get; set; }

    public int MathRawScore { get; set; }

    public int ScienceRawScore { get; set; }
    public int IntelligenceRawScore { get; set; }
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public DateTime DateRecorded { get; set; }
    public DateTime? DateUpdated { get; set; }
    public string RecordedBy { get; set; }
    public string? UpdatedBy { get; set; }

    public Examination Update(int readingRawScore, int mathRawScore, int scienceRawScore, int intelligenceRawScore, string updatedBy)
    {
        ReadingRawScore = readingRawScore;
        MathRawScore = mathRawScore;
        ScienceRawScore = scienceRawScore;
        IntelligenceRawScore = intelligenceRawScore;
        DateUpdated = DateTime.Now;
        UpdatedBy = updatedBy;
        return this;
    }
}
