using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.SoloParents;
public interface ISoloParentService
{
    Task<ResponseWrapper<int>> Create(SoloParentRequest request);
}

