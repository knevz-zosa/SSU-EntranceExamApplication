using System.ComponentModel.DataAnnotations;

namespace Common.Requests;
public class ExaminationRequest
{
    [Required(ErrorMessage = "Scores in reading is required.")]
    public int ReadingRawScore { get; set; }
    [Required(ErrorMessage = "Scores in math is required.")]
    public int MathRawScore { get; set; }
    [Required(ErrorMessage = "Scores in science is required.")]
    public int ScienceRawScore { get; set; }
    [Required(ErrorMessage = "Scores in intelligence test is required.")]
    public int IntelligenceRawScore { get; set; }
    public DateTime DateRecorded { get; set; } = DateTime.Now;
    public string RecordedBy { get; set; }
    public int ApplicantId { get; set; }
}

public class ExaminationUpdate
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Scores in reading is required.")]
    public int ReadingRawScore { get; set; }
    [Required(ErrorMessage = "Scores in math is required.")]
    public int MathRawScore { get; set; }
    [Required(ErrorMessage = "Scores in science is required.")]
    public int ScienceRawScore { get; set; }
    [Required(ErrorMessage = "Scores in intelligence test is required.")]
    public int IntelligenceRawScore { get; set; }
    public string UpdatedBy { get; set; }

}
