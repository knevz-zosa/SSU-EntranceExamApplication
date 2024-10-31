using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.CampusesCQS.Queries;
public class ListCampusQuery : BaseListQuery<CampusResponse> { }

public class ListCampusQueryHandler : BaseListQueryHandler<ListCampusQuery, CampusResponse>
{
    public ListCampusQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<PagedList<CampusResponse>>> Handle(ListCampusQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Campus>().Entities
            .Include(x => x.Departments)
            .Include(x => x.Courses)
            .Include(x => x.Schedules)
            .AsNoTracking();

        var query = repository;

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(u => u.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(Campus.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var response = _mapper.Map<List<CampusResponse>>(models);

        var pagedList = new PagedList<CampusResponse>(totalCount, response);
        return new ResponseWrapper<PagedList<CampusResponse>>().Success(pagedList);
    }

    IQueryable<Campus> QuerySort(IQueryable<Campus> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(Campus.Name):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name);
            case nameof(Campus.DateCreated):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.DateCreated)
                    : query.OrderByDescending(c => c.DateCreated);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}