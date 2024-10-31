using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.PersonalityProfiles;
public class PersonalityProfileService : IPersonalityProfileService
{
    private readonly IMediator _mediator;

    public PersonalityProfileService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(PersonalityProfileRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreatePersonalityProfileCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}