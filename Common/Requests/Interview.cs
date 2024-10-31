using System.ComponentModel.DataAnnotations;
namespace Common.Requests;

public class InterviewRequest
{
    public DateTime InterviewDate { get; set; }
    public int CourseId { get; set; }
    public int InterviewReading { get; set; }

    public int InterviewCommunication { get; set; }

    public int InterviewAnalytical { get; set; }
    public int ApplicantId { get; set; }
    public bool IsUse { get; set; } = false;
    public DateTime DateRecorded { get; set; } = DateTime.Now;
    public string RecordedBy { get; set; }
    [Required(ErrorMessage = "Interview name is required")]

    public string Interviewer { get; set; }
}

public class InterviewRatingUpdate
{
    public int Id { get; set; }
    public int InterviewReading { get; set; }

    public int InterviewCommunication { get; set; }

    public int InterviewAnalytical { get; set; }
    public string UpdatedBy { get; set; }
    [Required(ErrorMessage = "Interview name is required")]
    public string Interviewer { get; set; }
}

public class InterviewActiveUpdate
{
    public int Id { get; set; }
    public bool IsUse { get; set; }
    public string UpdatedBy { get; set; }
}
