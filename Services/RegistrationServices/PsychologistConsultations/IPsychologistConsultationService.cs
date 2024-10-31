using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.PsychologistConsultations;
public interface IPsychologistConsultationService
{
    Task<ResponseWrapper<int>> Create(PsychologistConsultationRequest request);

}
