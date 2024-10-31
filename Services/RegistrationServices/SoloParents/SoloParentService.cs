using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.SoloParents;
public class SoloParentService : ISoloParentService
{
    private readonly IMediator _mediator;

    public SoloParentService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(SoloParentRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateSoloParentCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}