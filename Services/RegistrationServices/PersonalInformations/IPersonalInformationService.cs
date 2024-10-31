using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.RegistrationServices.PersonalInformations;
public interface IPersonalInformationService
{
    Task<ResponseWrapper<int>> Create(PersonalInformationRequest request);
    Task<ResponseWrapper<PersonalInformationResponse>> Get(int id);
    Task<ResponseWrapper<int>> Update(PersonalInformationUpdate update);
}
