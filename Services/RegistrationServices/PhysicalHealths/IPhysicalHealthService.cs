using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.PhysicalHealths;
public interface IPhysicalHealthService
{
    Task<ResponseWrapper<int>> Create(PhysicalHealthRequest request);

}
