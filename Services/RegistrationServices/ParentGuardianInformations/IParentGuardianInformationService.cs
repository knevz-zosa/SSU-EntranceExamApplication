using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.ParentGuardianInformations;
public interface IParentGuardianInformationService
{
    Task<ResponseWrapper<int>> Create(ParentGuardianInformationRequest request);
}
