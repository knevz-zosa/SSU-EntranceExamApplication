using ApplicationLayer.Features.Admins.ApplicantsCQS.Commands;
using ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.Registration.Undergrad.Commands;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.RegistrationServices.Applicants;
public class ApplicantService : IApplicantService
{
    private readonly IMediator _mediator;

    public ApplicantService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(ApplicantRequest request)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "Entity cannot be null.");
        }

        var command = new CreateApplicantCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteApplicantCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<ApplicantResponse>> Get(int id)
    {
        var query = new GetApplicantQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PagedList<ApplicantResponse>>> InProgress(DataGridQuery query, string access)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListInProgressQuery { GridQuery = compactQuery.ToQuery() };
        listQuery.Access = access;
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<ApplicantResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<ApplicantResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<PagedList<ApplicantResponse>>> List(DataGridQuery query, int? Id, string access)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListApplicantQuery { GridQuery = compactQuery.ToQuery() };
        listQuery.ScheduleId = Id;
        listQuery.Access = access;
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<ApplicantResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<ApplicantResponse>>().Success(pagedList);
    }

    public async Task<ResponseWrapper<int>> Transfer(ApplicantTransfer update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateTransferCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> UpdateGWAStatusTrack(ApplicantUpdateGwaStatusTrack update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateTrackStatusGWACommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> UpdateStudentId(ApplicantUpdateStudentId update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "Entity cannot be null.");
        }

        var command = new UpdateStudentIdCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }
}
