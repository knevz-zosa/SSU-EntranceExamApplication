using ApplicationLayer.Features.Admins.UsersCQS.Commands;
using ApplicationLayer.Features.Admins.UsersCQS.Queries;
using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using MediatR;

namespace Services.UsersServices;

public class UserService : IUserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ResponseWrapper<int>> Create(UserRequest request)
    {      
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request), "User request cannot be null.");
        }

        var command = new CreateUserCommand(request);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> Delete(int id)
    {
        var delete = new DeleteUserCommand()
        {
            Id = id
        };
        var result = await _mediator.Send(delete);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<UserResponse>> Get(int id)
    {
        var query = new GetUserQuery(id);
        var result = await _mediator.Send(query);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> UpdateProfile(UserUpdateProfile update)
    {
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "User update cannot be null.");
        }

        var command = new UpdateUserProfileCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> UpdateAccess(UserUpdateAccessRole update)
    {       
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "User update cannot be null.");
        }

        var command = new UpdateUserAccessRoleCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<int>> UpdateCredential(UserUpdateCredential update)
    {       
        if (update == null)
        {
            throw new ArgumentNullException(nameof(update), "User update cannot be null.");
        }

        var command = new UpdateUserCredentialCommand(update);
        var result = await _mediator.Send(command);
        result.EnsureSuccess();
        return result;
    }

    public async Task<ResponseWrapper<PagedList<UserResponse>>> List(DataGridQuery query)
    {
        var compactQuery = new CompactDataGridQuery
        {
            s = query.Search,
            p = query.Page,
            ps = query.PageSize,
            sf = query.SortField,
            sd = query.SortDir
        };

        var listQuery = new ListUserQuery { GridQuery = compactQuery.ToQuery() };
        var result = await _mediator.Send(listQuery);
        result.EnsureSuccess();

        var pagedList = new PagedList<UserResponse>(result.Data.Count, result.Data.List);
        return new ResponseWrapper<PagedList<UserResponse>>().Success(pagedList);
    }
}

