﻿namespace Common.Requests;
public class CouncelorConsultationRequest
{
    public int ApplicantId { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? Sessions { get; set; }
    public string? Reasons { get; set; }
}
