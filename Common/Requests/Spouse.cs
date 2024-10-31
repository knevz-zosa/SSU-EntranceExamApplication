using System.ComponentModel.DataAnnotations;
namespace Common.Requests;
public class SpouseRequest
{
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Spouse's full name is required.")]
    public string FullName { get; set; }
    public DateTime Birthday { get; set; }
    [Required(ErrorMessage = "Spouse's birth place is required.")]
    public string BirthPlace { get; set; }
    public string? Occupation { get; set; } = string.Empty;
    [Required(ErrorMessage = "Spouse's educational attainment is required.")]
    public string Education { get; set; }
    public string? ContactNumber { get; set; } = string.Empty;
    public string? OfficeAddress { get; set; } = string.Empty;
    [Required(ErrorMessage = "Spouse's barangay address is required.")]
    public string Barangay { get; set; }
    [Required(ErrorMessage = "Spouse's municipality is required.")]
    public string Municipality { get; set; }
    [Required(ErrorMessage = "Spouse's province address is required.")]
    public string Province { get; set; }
    [Required(ErrorMessage = "Spouse's zip code is required.")]
    public string ZipCode { get; set; }
}
