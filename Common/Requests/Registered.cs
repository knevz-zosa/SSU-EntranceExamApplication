namespace Common.Requests;
public class RegisteredRequest
{
    public int ApplicantId { get; set; }
    public DateTime RegistrationDate { get; set; } = DateTime.Now;
}
