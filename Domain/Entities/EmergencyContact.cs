using Domain.Contracts;
namespace Domain.Entities;
public class EmergencyContact : BaseEntity<int>
{
    public int ApplicantId { get; set; }
    public Applicant Applicant { get; set; }
    public string Name { get; set; }
    public string ContactNo { get; set; }
    public string Address { get; set; }
    public string Relationship { get; set; }

    public EmergencyContact Update(string name, string contactNo, string address, string relationship)
    {
        Name = name;
        ContactNo = contactNo;
        Address = address;
        Relationship = relationship;
        return this;
    }
}

