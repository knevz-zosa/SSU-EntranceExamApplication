using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.UsersServices;
public interface IUserService
{
    Task<ResponseWrapper<int>> Create(UserRequest request);
    Task<ResponseWrapper<int>> UpdateProfile(UserUpdateProfile update);
    Task<ResponseWrapper<int>> UpdateAccess(UserUpdateAccessRole update);
    Task<ResponseWrapper<int>> UpdateCredential(UserUpdateCredential update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<UserResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<UserResponse>>> List(DataGridQuery query);

}
