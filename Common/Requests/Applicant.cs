using System.ComponentModel.DataAnnotations;

namespace Common.Requests;
public class ApplicantRequest
{
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
    [Required(ErrorMessage = "Applicant status is required.")]
    public string ApplicantStatus { get; set; }
    [Required(ErrorMessage = "Track is required.")]
    public string Track { get; set; }
    [Required(ErrorMessage = "Time is required.")]
    public DateTime TransactionDate { get; set; } = DateTime.Now;
}

public class ApplicantTransfer
{
    public int Id { get; set; }
    public int CourseId { get; set; }
    public int ScheduleId { get; set; }
}

public class ApplicantUpdateGwaStatusTrack
{
    public int Id { get; set; }
    public double GWA { get; set; }
    public string ApplicantStatus { get; set; }
    public string Track { get; set; }
}

public class ApplicantUpdateStudentId
{
    public int Id { get; set; }
    public string? StudentId { get; set; }
}
