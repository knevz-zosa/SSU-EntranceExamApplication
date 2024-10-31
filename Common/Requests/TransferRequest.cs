namespace Common.Requests;
public class TransferRequest
{
    public int ApplicantId { get; set; }
    public int UserId { get; set; }
    public string FromCampus { get; set; }
    public string FromProgram { get; set; }
    public string ToCampus { get; set; }
    public string ToProgram { get; set; }
    public DateTime TransferDate { get; set; } = DateTime.Now;
}
