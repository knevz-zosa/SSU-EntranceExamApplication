using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.RegistrationServices.FirstApplicationInfos;
public interface IFirstApplicationInfoService
{
    Task<ResponseWrapper<int>> Create(FirstApplicationInfoRequest request);
    Task<ResponseWrapper<FirstApplicationInfoResponse>> Get(int id);

}
