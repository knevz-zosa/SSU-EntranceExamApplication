using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.Admins.InterviewsCQS.Commands;
using ApplicationLayer.Features.Admins.InterviewsCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.InterviewServices;
public class InterviewService : IInterviewService
{
    private readonly IMediator _mediator;

    public InterviewService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Activate(InterviewActiveUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateInterviewActiveCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
    public async Task<ResponseWrapper<int>> Create(InterviewRequest request)
    {
       if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateInterviewCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<InterviewResponse>> Get(int id)
    {
        var query = new GetInterviewQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }
    public async Task<ResponseWrapper<PagedList<InterviewResponse>>> List(DataGridQuery query)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListInterviewQuery { GridQuery = compactQuery.ToQuery() };
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<InterviewResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<InterviewResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<int>> UpdateRating(InterviewRatingUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateInterviewRatingCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
