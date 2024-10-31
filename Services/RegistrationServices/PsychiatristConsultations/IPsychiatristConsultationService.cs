using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.PsychiatristConsultations;
public interface IPsychiatristConsultationService
{
    Task<ResponseWrapper<int>> Create(PsychiatristConsultationRequest request);

}
