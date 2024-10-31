using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.AuthServices;
public interface IAuthService
{
    Task<ResponseWrapper<UserResponse>> Login(LoginRequest request);
}
