using Domain.Contracts;
namespace Domain.Entities;
public class Spouse : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public string FullName { get; set; }
    public DateTime Birthday { get; set; }
    public string BirthPlace { get; set; }
    public string? Occupation { get; set; }
    public string Education { get; set; }
    public string? ContactNumber { get; set; }
    public string? OfficeAddress { get; set; }
    public string Barangay { get; set; }
    public string Municipality { get; set; }
    public string Province { get; set; }
    public string ZipCode { get; set; }
}