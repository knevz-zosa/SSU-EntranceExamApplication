﻿using Domain.Contracts;
namespace Domain.Entities;
public class PsychiatristConsultation : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public DateTime? Start { get; set; }
    public DateTime? End { get; set; }
    public int? Sessions { get; set; }
    public string? Reasons { get; set; }
}

