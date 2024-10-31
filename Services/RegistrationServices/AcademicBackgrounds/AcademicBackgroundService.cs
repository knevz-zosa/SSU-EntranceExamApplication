using ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
using ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.AcademicBackgrounds;

public class AcademicBackgroundService : IAcademicBackgroundService
{
    private readonly IMediator _mediator;

    public AcademicBackgroundService(IMediator mediator)
    {
        _mediator = mediator;
    }   

    public async Task<ResponseWrapper<int>> Create(AcademicBackgroundRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateAcademicBackgroundCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<LrnResponse>> Get(int id)
    {
        var query = new GetLrnQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Update(LrnUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateLrnCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
