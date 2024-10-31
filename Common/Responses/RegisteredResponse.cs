namespace Common.Responses;
public class RegisteredResponse
{
    public int Id { get; set; }
    public int ApplicantId { get; set; }
    public ApplicantResponse Applicant { get; set; }

    public DateTime RegistrationDate { get; set; }
}
