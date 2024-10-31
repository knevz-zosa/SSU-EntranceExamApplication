using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.EmergencyContacts;
public interface IEmergencyContactService
{
    Task<ResponseWrapper<int>> Create(EmergencyContactRequest request);
    Task<ResponseWrapper<int>> Update(EmergencyContactUpdate update);
}
