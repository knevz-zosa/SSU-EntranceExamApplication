using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.PersonalityProfiles;
public interface IPersonalityProfileService
{
    Task<ResponseWrapper<int>> Create(PersonalityProfileRequest request);

}
