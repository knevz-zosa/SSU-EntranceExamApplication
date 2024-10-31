using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.Registered;
public class RegisteredService : IRegisteredService
{
    private readonly IMediator _mediator;

    public RegisteredService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(RegisteredRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateRegisteredCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
