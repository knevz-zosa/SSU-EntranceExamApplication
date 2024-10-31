using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
namespace ApplicationLayer.Features.Admins.CoursesCQS.Queries;
public class ListCourseQuery : BaseListQuery<CourseResponse> 
{
    public string Access { get; set; }
}

public class ListCourseQueryHandler : BaseListQueryHandler<ListCourseQuery, CourseResponse>
{
    public ListCourseQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<CourseResponse>>> Handle(ListCourseQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Course>().Entities;
        var query = repository;
        if(list.Access == "All")
        {
             query = repository
                .Include(x => x.Campus)
                .Include(x => x.Department)
                .AsNoTracking();
        }
        else
        {
             query = repository
                .Where(x => x.Campus.Name == list.Access)
                .Include(x => x.Campus)
                .Include(x => x.Department)
                .AsNoTracking();
        }

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(u => u.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(Course.Name);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var response = _mapper.Map<List<CourseResponse>>(models);

        var pagedList = new PagedList<CourseResponse>(totalCount, response);
        return new ResponseWrapper<PagedList<CourseResponse>>().Success(pagedList);
    }
    IQueryable<Course> QuerySort(IQueryable<Course> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(Course.Name):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Name)
                    : query.OrderByDescending(c => c.Name);
            case nameof(Course.DateCreated):
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
