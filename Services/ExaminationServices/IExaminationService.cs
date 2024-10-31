using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.ExaminationServices;
public interface IExaminationService
{
    Task<ResponseWrapper<int>> Create(ExaminationRequest request);
    Task<ResponseWrapper<int>> Update(ExaminationUpdate update);
    Task<ResponseWrapper<ExaminationResponse>> Get(int id);
}
