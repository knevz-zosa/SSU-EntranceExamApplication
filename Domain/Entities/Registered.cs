using Domain.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Registered : BaseEntity<int>
{
    [Required]
    public int ApplicantId { get; set; } 
    public Applicant Applicant { get; set; } 

    public DateTime RegistrationDate { get; set; }
}
