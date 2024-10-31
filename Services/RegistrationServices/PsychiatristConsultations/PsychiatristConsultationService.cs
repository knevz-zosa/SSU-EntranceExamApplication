using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.PsychiatristConsultations;
public class PsychiatristConsultationService : IPsychiatristConsultationService
{
    private readonly IMediator _mediator;

    public PsychiatristConsultationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ResponseWrapper<int>> Create(PsychiatristConsultationRequest request)
    {
        var command = new CreatePsychiatristConsultationCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
