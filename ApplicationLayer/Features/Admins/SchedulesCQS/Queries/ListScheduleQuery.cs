using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.SchedulesCQS.Queries;
public class ListScheduleQuery : BaseListQuery<ScheduleResponse> 
{
    public int? ScheduleId { get; set; }
    public string Access { get; set; }
}

public class ListScheduleQueryHandler : BaseListQueryHandler<ListScheduleQuery, ScheduleResponse>
{
    public ListScheduleQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<PagedList<ScheduleResponse>>> Handle(ListScheduleQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Schedule>().Entities;
        var query = repository;
        if (list.Access == "All")
        {
            query = repository
              .Include(x => x.Campus)
                .ThenInclude(c => c.Courses)
             .Include(x => x.Applicants)
                .ThenInclude(pi => pi.PersonalInformation)
              .Include(x => x.Applicants)
                .ThenInclude(ab => ab.AcademicBackground)
             .Include(x => x.Applicants)
                .ThenInclude(pgi => pgi.ParentGuardianInformation)
             .Include(x => x.Applicants)
                .ThenInclude(ph => ph.PhysicalHealth)
             .Include(x => x.Applicants)
                .ThenInclude(pf => pf.PersonalityProfile)
             .Include(x => x.Applicants)
                .ThenInclude(ec => ec.EmergencyContact)
             .AsNoTracking();
        }
        else
        {
            query = query
             .Where(x => x.Campus.Name == list.Access)
                .Include(x => x.Campus)
                    .ThenInclude(c => c.Courses)
                .Include(x => x.Applicants)
                    .ThenInclude(pi => pi.PersonalInformation)
                 .Include(x => x.Applicants)
                    .ThenInclude(ab => ab.AcademicBackground)
                .Include(x => x.Applicants)
                    .ThenInclude(pgi => pgi.ParentGuardianInformation)
                .Include(x => x.Applicants)
                    .ThenInclude(ph => ph.PhysicalHealth)
                .Include(x => x.Applicants)
                    .ThenInclude(pf => pf.PersonalityProfile)
                .Include(x => x.Applicants)
                    .ThenInclude(ec => ec.EmergencyContact)
                .AsNoTracking();
        }

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(u => u.Campus.Name.Contains(list.GridQuery.Search));
        }

        var totalCount = await query.CountAsync(cancellationToken);

        var sortField = list.GridQuery.SortField ?? nameof(Schedule.ScheduleDate);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync(cancellationToken);

        var response = _mapper.Map<List<ScheduleResponse>>(models);

        var pagedList = new PagedList<ScheduleResponse>(totalCount, response);
        return new ResponseWrapper<PagedList<ScheduleResponse>>().Success(pagedList);
    }
    IQueryable<Schedule> QuerySort(IQueryable<Schedule> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(Schedule.ScheduleDate):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.ScheduleDate)
                    : query.OrderByDescending(c => c.ScheduleDate);
            case nameof(Schedule.DateCreated):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.DateCreated)
                    : query.OrderByDescending(c => c.DateCreated);
            case nameof(Schedule.Campus):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Campus.Name)
                    : query.OrderByDescending(c => c.Campus.Name);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
