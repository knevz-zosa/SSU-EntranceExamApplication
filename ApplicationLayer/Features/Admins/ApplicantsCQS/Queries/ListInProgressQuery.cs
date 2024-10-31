using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
public class ListInProgressQuery : BaseListQuery<ApplicantResponse>
{
    public string Access { get; set; }
}
public class ListInProgressQueryHandler : BaseListQueryHandler<ListInProgressQuery, ApplicantResponse>
{
    public ListInProgressQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<PagedList<ApplicantResponse>>> Handle(ListInProgressQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Applicant>().Entities;
        var query = repository;

        if (list.Access != "All")
        {
            query = query
           .Where(x => x.Schedule.Campus.Name == list.Access)
          .Include(x => x.Schedule)
               .ThenInclude(s => s.Campus)
                   .ThenInclude(c => c.Courses)
          .Include(x => x.PersonalInformation)
          .Include(x => x.Spouse)
          .Include(x => x.SoloParent)
          .Include(x => x.AcademicBackground)
          .Include(x => x.ParentGuardianInformation)
          .Include(x => x.FamilyRelations)
          .Include(x => x.PersonalityProfile)
          .Include(x => x.PhysicalHealth)
          .Include(x => x.PsychiatristConsultation)
          .Include(x => x.PsychologistConsultation)
          .Include(x => x.CouncelorConsultation)
          .Include(x => x.EmergencyContact)
          .Include(x => x.Transfers)
          .Include(x => x.Interviews)
          .Include(x => x.Examination)
          .Include(x => x.FirstApplicationInfo);
        }
        else
        {
            query = query
              .Include(x => x.Schedule)
                   .ThenInclude(s => s.Campus)
                       .ThenInclude(c => c.Courses)
              .Include(x => x.PersonalInformation)
              .Include(x => x.Spouse)
              .Include(x => x.SoloParent)
              .Include(x => x.AcademicBackground)
              .Include(x => x.ParentGuardianInformation)
              .Include(x => x.FamilyRelations)
              .Include(x => x.PersonalityProfile)
              .Include(x => x.PhysicalHealth)
              .Include(x => x.PsychiatristConsultation)
              .Include(x => x.PsychologistConsultation)
              .Include(x => x.CouncelorConsultation)
              .Include(x => x.EmergencyContact)
              .Include(x => x.Transfers)
              .Include(x => x.Interviews)
              .Include(x => x.Examination)
              .Include(x => x.FirstApplicationInfo);
        }

        query = query.Where(a => a.Registered == null);

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(c =>
                 c.PersonalInformation.LastName.Contains(list.GridQuery.Search) ||
                 c.PersonalInformation.FirstName.Contains(list.GridQuery.Search) ||
                 c.Schedule.Campus.Name.Contains(list.GridQuery.Search) ||
                 c.Schedule.Campus.Courses
                    .Any(course => course.Id == c.CourseId && course.Name.Contains(list.GridQuery.Search)));
        }

        var totalCount = await query.CountAsync();

        var sortField = list.GridQuery.SortField ?? nameof(Applicant.TransactionDate);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync();

        var modelResponses = _mapper.Map<List<ApplicantResponse>>(models);

        var pagedList = new PagedList<ApplicantResponse>(totalCount, modelResponses);

        return new ResponseWrapper<PagedList<ApplicantResponse>>().Success(pagedList);
    }
    IQueryable<Applicant> QuerySort(IQueryable<Applicant> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case "Name":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
            case "Schedule.Campus.Name":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Schedule.Campus.Name)
                    : query.OrderByDescending(c => c.Schedule.Campus.Name);
            case "Schedule.Campus.Courses":
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Schedule.Campus.Courses.Select(x => x.Name).SingleOrDefault())
                    : query.OrderByDescending(c => c.Schedule.Campus.Courses.Select(x => x.Name).SingleOrDefault());
            case nameof(Applicant.TransactionDate):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.TransactionDate)
                    : query.OrderByDescending(c => c.TransactionDate);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }

       
    }
}
