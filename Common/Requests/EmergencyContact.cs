using System.ComponentModel.DataAnnotations;
namespace Common.Requests;
public class EmergencyContactRequest
{
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Contact person name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Contact person number is required.")]
    public string ContactNo { get; set; }
    [Required(ErrorMessage = "Contact person address is required.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Relationship to contact is required.")]
    public string Relationship { get; set; }
}

public class EmergencyContactUpdate
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Contact person name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Contact person number is required.")]
    public string ContactNo { get; set; }
    [Required(ErrorMessage = "Contact person address is required.")]
    public string Address { get; set; }
    [Required(ErrorMessage = "Relationship to contact is required.")]
    public string Relationship { get; set; }
}
