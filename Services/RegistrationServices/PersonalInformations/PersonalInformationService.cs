using ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
using ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.PersonalInformations;
public class PersonalInformationService : IPersonalInformationService
{
    private readonly IMediator _mediator;

    public PersonalInformationService(IMediator mediator)
    {
        _mediator = mediator;
    }   
    public async Task<ResponseWrapper<int>> Create(PersonalInformationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreatePersonalInformationCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PersonalInformationResponse>> Get(int id)
    {
        var query = new GetPersonalInformationQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Update(PersonalInformationUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdatePersonalInformationCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
