using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.InterviewsCQS.Queries;
public class ListInterviewQuery : BaseListQuery<InterviewResponse> { }
public class ListInterviewQueryHandler : BaseListQueryHandler<ListInterviewQuery, InterviewResponse>
{
    public ListInterviewQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<PagedList<InterviewResponse>>> Handle(ListInterviewQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Interview>();

        var query = repository.Entities;

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(c => c.InterviewDate.ToShortDateString().Contains(list.GridQuery.Search));
        }
        var totalCount = await query.CountAsync();

        var sortField = list.GridQuery.SortField ?? nameof(Department.DateCreated);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync();

        var modelResponses = _mapper.Map<List<InterviewResponse>>(models);

        var pagedList = new PagedList<InterviewResponse>(totalCount, modelResponses);

        return new ResponseWrapper<PagedList<InterviewResponse>>().Success(pagedList);
    }
    IQueryable<Interview> QuerySort(IQueryable<Interview> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(Interview.InterviewDate):
              return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.InterviewDate)
                    : query.OrderByDescending(c => c.InterviewDate);
            case nameof(Interview.DateRecorded):
               return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.DateRecorded)
                    : query.OrderByDescending(c => c.DateRecorded);
            default:
               return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
