using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.CouncelorConsultations;
public interface ICouncelorConsultationService
{
    Task<ResponseWrapper<int>> Create(CouncelorConsultationRequest request);
}

