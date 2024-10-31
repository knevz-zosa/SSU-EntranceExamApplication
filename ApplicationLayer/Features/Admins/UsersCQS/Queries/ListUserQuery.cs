using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.UsersCQS.Queries;
public class ListUserQuery : BaseListQuery<UserResponse> { }

public class ListUserQueryHandler : BaseListQueryHandler<ListUserQuery, UserResponse>
{
    public ListUserQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<UserResponse>>> Handle(ListUserQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<User>().Entities;
        var query = repository;

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(u => u.FirstName.Contains(list.GridQuery.Search) ||
                u.LastName.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(Campus.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var response = _mapper.Map<List<UserResponse>>(models);

        var pagedList = new PagedList<UserResponse>(totalCount, response);
        return new ResponseWrapper<PagedList<UserResponse>>().Success(pagedList);
    }
    IQueryable<User> QuerySort(IQueryable<User> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(User.FirstName):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.FirstName)
                    : query.OrderByDescending(c => c.FirstName);
            case nameof(User.LastName):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.LastName)
                    : query.OrderByDescending(c => c.LastName);
            case nameof(User.Username):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Username)
                    : query.OrderByDescending(c => c.Username);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}

