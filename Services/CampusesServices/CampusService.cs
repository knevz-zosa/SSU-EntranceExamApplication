using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.CampusesServices;
public class CampusService : ICampusService
{
    private readonly IMediator _mediator;

    public CampusService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(CampusRequest request)
    {      
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateCampusCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteCampusCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<CampusResponse>> Get(int id)
    {
        var query = new GetCampusQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PagedList<CampusResponse>>> List(DataGridQuery query)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListCampusQuery { GridQuery = compactQuery.ToQuery() };
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<CampusResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<CampusResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<int>> Update(CampusUpdate update)
    {       
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateCampusCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
