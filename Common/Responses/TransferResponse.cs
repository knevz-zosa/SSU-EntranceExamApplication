namespace Common.Responses;
public class TransferResponse
{
    public int Id { get; set; }
    public string FromCampus { get; set; }
    public string FromProgram { get; set; }
    public string ToCampus { get; set; }
    public string ToProgram { get; set; }
    public DateTime TransferDate { get; set; }
}
