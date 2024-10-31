using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.Admins.ExaminationsCQS.Commands;
using ApplicationLayer.Features.Admins.ExaminationsCQS.Queries;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.ExaminationServices;
public class ExaminationService : IExaminationService
{
    private readonly IMediator _mediator;

    public ExaminationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(ExaminationRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateExaminationCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<ExaminationResponse>> Get(int id)
    {
        var query = new GetExaminationQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Update(ExaminationUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateExaminationCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
