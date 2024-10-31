using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
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

namespace ApplicationLayer.Features.Admins.DepartmentsCQS.Queries;

public class ListDepartmentQuery : BaseListQuery<DepartmentResponse> { }

public class ListDepartmentQueryHandler : BaseListQueryHandler<ListDepartmentQuery, DepartmentResponse>
{
    public ListDepartmentQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<DepartmentResponse>>> Handle(ListDepartmentQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Department>().Entities
            .Include(x => x.Campus)
            .Include(x => x.Courses)
            .AsNoTracking();

        var query = repository;
        
        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(c => c.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync();

        var sortField = list.GridQuery.SortField ?? nameof(Department.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync();

        var response = _mapper.Map<List<DepartmentResponse>>(models);

        var pagedList = new PagedList<DepartmentResponse>(totalCount, response);
        return new ResponseWrapper<PagedList<DepartmentResponse>>().Success(pagedList);
    }
    IQueryable<Department> QuerySort(IQueryable<Department> query, string sortField, DataGridQuerySortDirection sortDirection)
    {        
        switch (sortField)
        {
            case nameof(Department.Name):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name);
            case nameof(Department.DateCreated):
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
