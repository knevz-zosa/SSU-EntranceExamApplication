using System.ComponentModel.DataAnnotations;

namespace Common.Requests;
public class PersonalInformationRequest
{
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "Firstname is required.")]
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    [Required(ErrorMessage = "Lastname is required.")]
    public string LastName { get; set; }
    public string? NameExtension { get; set; }
    [Required(ErrorMessage = "Nickname is required.")]
    public string NickName { get; set; }

    [Required(ErrorMessage = "Sex is required.")]
    public string Sex { get; set; }
    [Required(ErrorMessage = "Civil status is required.")]
    public string CivilStatus { get; set; }
    [Required(ErrorMessage = "Place of birth is required.")]
    public string PlaceOfBirth { get; set; }
    [Required(ErrorMessage = "Citizenship is required.")]
    public string Citizenship { get; set; }
    [Required(ErrorMessage = "Religion is required.")]
    public string Religion { get; set; }
    public string? Email { get; set; } = string.Empty;
    public string? ContactNumber { get; set; } = string.Empty;
    public DateTime DateofBirth { get; set; }
    public string? HouseNo { get; set; } = string.Empty;
    [Required(ErrorMessage = "Street address is required.")]
    public string Street { get; set; }
    [Required(ErrorMessage = "Barangay address is required.")]
    public string Barangay { get; set; }
    public string? Purok { get; set; } = string.Empty;

    [Required(ErrorMessage = "Municipality address is required.")]
    public string Municipality { get; set; }
    [Required(ErrorMessage = "Province address is required.")]
    public string Province { get; set; }
    [Required(ErrorMessage = "Zip code is required.")]
    public string ZipCode { get; set; }
    public string? CurrentPurok { get; set; } = string.Empty;
    [Required(ErrorMessage = "Current street address is required.")]
    public string CurrentStreet { get; set; }
    [Required(ErrorMessage = "Current barangay address is required.")]
    public string CurrentBarangay { get; set; }
    public string? CurrentHouseNo { get; set; } = string.Empty;
    [Required(ErrorMessage = "Current municipality address is required.")]
    public string CurrentMunicipality { get; set; }
    [Required(ErrorMessage = "Current province address is required.")]
    public string CurrentProvince { get; set; }

    [Required(ErrorMessage = "Current zip code address is required.")]
    public string CurrentZipCode { get; set; }
    [Required(ErrorMessage = "Dialect is required.")]
    public string Dialect { get; set; }
    public bool IsIndigenous { get; set; } = false;
    public string? TribalAffiliation { get; set; } = string.Empty;
    public bool Is4psMember { get; set; } = false;
    public string? HouseHold4psNumber { get; set; } = string.Empty;
    [Required(ErrorMessage = "Number of siblings (brother) is required.")]
    public int Brothers { get; set; }
    [Required(ErrorMessage = "Number of sibling (sister) is required.")]
    public int Sisters { get; set; }
    [Required(ErrorMessage = "Birth order is required")]
    public int BirthOrder { get; set; }
}

public class PersonalInformationUpdate
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    [Required(ErrorMessage = "First name  is required.")]
    public string FirstName { get; set; }
    [Required(ErrorMessage = "This field is required.")]

    public string? MiddleName { get; set; }
    [Required(ErrorMessage = "Last name is required.")]
    public string LastName { get; set; }
    public string? NameExtension { get; set; }
    [Required(ErrorMessage = "Nick name is required.")]
    public string NickName { get; set; }
    public string Sex { get; set; }
    public DateTime DateofBirth { get; set; }
    [Required(ErrorMessage = "Place of birth is required.")]
    public string PlaceOfBirth { get; set; }
    [Required(ErrorMessage = "Citizenship is required.")]
    public string Citizenship { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public string? HouseNo { get; set; }
    [Required(ErrorMessage = "Street address is required.")]
    public string Street { get; set; }
    [Required(ErrorMessage = "Barangay address is required.")]
    public string Barangay { get; set; }
    public string? Purok { get; set; }
    [Required(ErrorMessage = "Municipality address is required.")]
    public string Municipality { get; set; }
    [Required(ErrorMessage = "Province address is required.")]
    public string Province { get; set; }
    [Required(ErrorMessage = "Zip code is required.")]
    public string ZipCode { get; set; }
}
