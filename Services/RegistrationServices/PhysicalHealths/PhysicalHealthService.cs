using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.PhysicalHealths;
public class PhysicalHealthService : IPhysicalHealthService
{
    private readonly IMediator _mediator;

    public PhysicalHealthService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(PhysicalHealthRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreatePhysicalHealthCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
