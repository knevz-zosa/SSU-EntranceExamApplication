using ApplicationLayer.Features.Admins.SchedulesCQS.Commands;
using ApplicationLayer.Features.Admins.SchedulesCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.ScheduleServices;
public class ScheduleService : IScheduleService
{
    private readonly IMediator _mediator;

    public ScheduleService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(ScheduleRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Schedule request cannot be null.");
        }

        var command = new CreateScheduleCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteScheduleCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<ScheduleResponse>> Get(int id)
    {
        var query = new GetScheduleQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PagedList<ScheduleResponse>>> List(DataGridQuery query, string access)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListScheduleQuery { GridQuery = compactQuery.ToQuery() };
        listQuery.Access = access;
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<ScheduleResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<ScheduleResponse>>().Success(pagedList);
    }
}
