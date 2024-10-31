using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.Registered;
public interface IRegisteredService
{
    Task<ResponseWrapper<int>> Create(RegisteredRequest request);
}
