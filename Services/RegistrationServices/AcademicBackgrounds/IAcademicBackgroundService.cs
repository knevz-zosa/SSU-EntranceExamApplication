using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.RegistrationServices.AcademicBackgrounds;
public interface IAcademicBackgroundService
{
    Task<ResponseWrapper<int>> Create(AcademicBackgroundRequest request);
    Task<ResponseWrapper<int>> Update(LrnUpdate update);
    Task<ResponseWrapper<LrnResponse>> Get(int id);
}
