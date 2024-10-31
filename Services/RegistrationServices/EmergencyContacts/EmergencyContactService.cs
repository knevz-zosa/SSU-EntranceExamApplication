using ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.EmergencyContacts;
public class EmergencyContactService : IEmergencyContactService
{
    private readonly IMediator _mediator;

    public EmergencyContactService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(EmergencyContactRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateEmergencyContactCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Update(EmergencyContactUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateEmergencyContactCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
