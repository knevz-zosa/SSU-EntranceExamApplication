using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.Spouses;
public interface ISpouseService
{
    Task<ResponseWrapper<int>> Create(SpouseRequest request);
}
