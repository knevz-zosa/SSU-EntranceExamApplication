using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.RegistrationServices.Applicants;
public interface IApplicantService
{
    Task<ResponseWrapper<int>> Create(ApplicantRequest request);
    Task<ResponseWrapper<int>> Transfer(ApplicantTransfer update);
    Task<ResponseWrapper<int>> UpdateGWAStatusTrack(ApplicantUpdateGwaStatusTrack update);
    Task<ResponseWrapper<int>> UpdateStudentId(ApplicantUpdateStudentId update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<ApplicantResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<ApplicantResponse>>> List(DataGridQuery query, int? Id, string access);
    Task<ResponseWrapper<PagedList<ApplicantResponse>>> InProgress(DataGridQuery query, string access);
}