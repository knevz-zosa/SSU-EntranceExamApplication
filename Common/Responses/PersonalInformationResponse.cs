namespace Common.Responses;

public class PersonalInformationResponse
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string LastName { get; set; }
    public string? NameExtension { get; set; }
    public string NickName { get; set; }
    public string Sex { get; set; }
    public string CivilStatus { get; set; }
    public string PlaceOfBirth { get; set; }
    public string Citizenship { get; set; }
    public string Religion { get; set; }
    public string? Email { get; set; }
    public string? ContactNumber { get; set; }
    public DateTime DateofBirth { get; set; }
    public string? HouseNo { get; set; }
    public string Street { get; set; }
    public string Barangay { get; set; }
    public string? Purok { get; set; }
    public string Municipality { get; set; }
    public string Province { get; set; }
    public string ZipCode { get; set; }
    public string? CurrentPurok { get; set; }
    public string CurrentStreet { get; set; }
    public string CurrentBarangay { get; set; }
    public string? CurrentHouseNo { get; set; }
    public string CurrentMunicipality { get; set; }
    public string CurrentProvince { get; set; }
    public string CurrentZipCode { get; set; }
    public string Dialect { get; set; }
    public bool IsIndigenous { get; set; }
    public string? TribalAffiliation { get; set; }
    public bool Is4psMember { get; set; }
    public string? HouseHold4psNumber { get; set; }
    public int Brothers { get; set; }
    public int Sisters { get; set; }
    public int BirthOrder { get; set; }
}
