using ApplicationLayer.Features.Admins.CoursesCQS.Commands;
using ApplicationLayer.Features.Admins.CoursesCQS.Queries;
using ApplicationLayer.Features.Admins.DepartmentsCQS.Commands;
using ApplicationLayer.Features.Admins.DepartmentsCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.CourseServices;
public class CourseService : ICourseService
{
    private readonly IMediator _mediator;

    public CourseService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(CourseRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Course request cannot be null.");
        }

        var command = new CreateCourseCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteCourseCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<CourseResponse>> Get(int id)
    {
        var query = new GetCourseQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PagedList<CourseResponse>>> List(DataGridQuery query, string access)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListCourseQuery { GridQuery = compactQuery.ToQuery() };
        listQuery.Access = access;  
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<CourseResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<CourseResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<int>> Update(CourseUpdate update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Course update cannot be null.");
        }

        var command = new UpdateCourseCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
