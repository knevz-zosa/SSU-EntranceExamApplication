using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.CouncelorConsultations;
public class CouncelorConsultationService : ICouncelorConsultationService
{
    private readonly IMediator _mediator;

    public CouncelorConsultationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(CouncelorConsultationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateCouncelorConsultationCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
