using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.Admins.DepartmentsCQS.Commands;
using ApplicationLayer.Features.Admins.DepartmentsCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.DepartmentsServices;
public class DepartmentService : IDepartmentService
{
    private readonly IMediator _mediator;

    public DepartmentService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(DepartmentRequest request)
    {

        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Department request cannot be null.");
        }

        var command = new CreateDepartmentCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteDepartmentCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<DepartmentResponse>> Get(int id)
    {     
        var query = new GetDepartmentQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }


    public async Task<ResponseWrapper<PagedList<DepartmentResponse>>> List(DataGridQuery query)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListDepartmentQuery { GridQuery = compactQuery.ToQuery() };
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<DepartmentResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<DepartmentResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<int>> Update(DepartmentUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Department update cannot be null.");
        }

        var command = new UpdateDepartmentCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
