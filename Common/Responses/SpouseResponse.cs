namespace Common.Responses;
public class SpouseResponse
{
    public int Id { get; set; }
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
